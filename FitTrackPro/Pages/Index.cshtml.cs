using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitTrackPro.Data;
using FitTrackPro.Models;

namespace FitTrackPro.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Dashboard Properties
        public int todayWorkoutCount { get; set; }
        public bool hasCompletedWorkoutToday { get; set; }
        public int todayMealsPlanned { get; set; }
        public int todayMealsCompleted { get; set; }
        public decimal? latestWeight { get; set; }
        public string weightTrend { get; set; } = "stable";
        public int totalRecipes { get; set; }
        public int totalExercises { get; set; }
        public int totalWorkoutRoutines { get; set; }
        public int shoppingListItemsCount { get; set; }

        public async Task OnGetAsync()
        {
            var today = DateTime.Today;

            // Get today's workout sessions
            var todaySessions = await _context.WorkoutSessions
                .Where(s => s.StartTime.Date == today)
                .ToListAsync();

            todayWorkoutCount = todaySessions.Count;
            hasCompletedWorkoutToday = todaySessions.Any(s => s.EndTime.HasValue);

            // Get today's meal plans
            var todayMeals = await _context.mealPlans
                .Where(m => m.date.Date == today)
                .ToListAsync();

            todayMealsPlanned = todayMeals.Count;
            todayMealsCompleted = todayMeals.Count(m => m.isCompleted);

            // Get latest weight from recent workout sessions (simulated from calories burned)
            // In a real app, you'd have a separate Weight tracking model
            var recentSession = await _context.WorkoutSessions
                .Where(s => s.EndTime.HasValue)
                .OrderByDescending(s => s.StartTime)
                .FirstOrDefaultAsync();

            var recentBodyMeasure = await _context.BodyMeasurements
                .OrderByDescending(m => m.Date)
                .FirstOrDefaultAsync();

            if (recentBodyMeasure != null)
            {
                latestWeight = (decimal)recentBodyMeasure.Weight; 
            }

            // Get summary counts
            totalRecipes = await _context.recipes.CountAsync();
            totalExercises = await _context.Exercises.CountAsync();
            totalWorkoutRoutines = await _context.WorkoutRoutines.CountAsync();
            shoppingListItemsCount = await _context.shoppingListItems
                .Where(s => !s.isChecked)
                .CountAsync();
        }
    }
}
