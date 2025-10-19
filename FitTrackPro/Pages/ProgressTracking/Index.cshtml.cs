using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitTrackPro.Data;
using FitTrackPro.Models;

namespace FitTrackPro.Pages.ProgressTracking
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        // Summary Metrics from each feature
        public float? CurrentWeight { get; set; }
        public decimal TotalVolumeLifted { get; set; }
        public int CaloriesConsumed { get; set; }
        public int AverageCaloriesPerDay { get; set; }

        // Chart data for daily calories over the week
        public string DailyCaloriesChartData { get; set; } = "[]";

        public async Task OnGetAsync()
        {
            var today = DateTime.Today;
            var weekAgo = DateTime.Now.AddDays(-7);

            // most recent weight from body measurements
            var latestMeasurement = await _db.BodyMeasurements
                .OrderByDescending(bm => bm.Date)
                .FirstOrDefaultAsync();
            CurrentWeight = latestMeasurement?.Weight;

            var recentSessions = await _db.WorkoutSessions
                .Include(ws => ws.SessionLogs)
                .Where(ws => ws.StartTime >= weekAgo)
                .ToListAsync();

            var allLogs = recentSessions.SelectMany(s => s.SessionLogs).ToList();
            TotalVolumeLifted = allLogs.Sum(log => (log.Weight ?? 0) * (log.Reps ?? 0));

            // Calculate calories consumed in the last 7 days from meal plans
            var recentMealPlans = await _db.mealPlans
                .Include(mp => mp.recipe)
                .Where(mp => mp.date >= weekAgo && mp.date <= today)
                .ToListAsync();

            CaloriesConsumed = recentMealPlans.Sum(mp => mp.recipe.caloriesPerServing);
            AverageCaloriesPerDay = CaloriesConsumed / 7;

            // Calculate daily calories for chart data
            var dailyCalories = new List<object>();
            for (int i = 6; i >= 0; i--)
            {
                var date = today.AddDays(-i);
                var dayCalories = recentMealPlans
                    .Where(mp => mp.date.Date == date)
                    .Sum(mp => mp.recipe.caloriesPerServing);

                dailyCalories.Add(new
                {
                    date = date.ToString("MM/dd"),
                    calories = dayCalories
                });
            }
            DailyCaloriesChartData = System.Text.Json.JsonSerializer.Serialize(dailyCalories);
        }
    }

}
