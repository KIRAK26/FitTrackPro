using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitTrackPro.Data;
using FitTrackPro.Models;
using System.Text.Json;

namespace FitTrackPro.Pages.ProgressTracking
{
    public class WorkoutMetricsModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public WorkoutMetricsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        // SupportsGet necessary as it means that the bindProperty supports GET methods aswell as POST. So for things like ?timeRange=weekly  
        [BindProperty(SupportsGet = true)]
        public string? TimeRange { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedExerciseId { get; set; }

        // Overall Stats
        public decimal TotalWeightLifted { get; set; }
        public int TotalWorkoutsCompleted { get; set; }
        public double AverageRestingTime { get; set; }

        // Exercise PRs
        public List<ExercisePR> ExercisePRs { get; set; } = new();
        public ExercisePR? SelectedExercisePR { get; set; }

        // All exercises for dropdown
        public List<Exercise> AllExercises { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Get all exercises for dropdown
            AllExercises = await _db.Exercises
                .OrderBy(e => e.Name)
                .ToListAsync();

            // gets sessin logs (can be used to query data for weight, reps)
            var sessionQuery = _db.WorkoutSessions
                .Include(ws => ws.SessionLogs)
                .AsQueryable();

            var filterPeriod = (TimeRange?.ToLower()) switch
            {
                "weekly" => DateTime.Now.AddDays(-7),
                "monthly" => DateTime.Now.AddDays(-30),
                "recent" => DateTime.MinValue,
                _ => DateTime.MinValue
            };

            // if a time period filter has been set it updates the session query paramters to only retrieve data within the time period.
            if (filterPeriod != DateTime.MinValue && TimeRange?.ToLower() != "recent")
            {
                sessionQuery = sessionQuery.Where(ws => ws.StartTime >= filterPeriod);
            }

            var sessions = await sessionQuery
                .OrderByDescending(ws => ws.StartTime)
                .ToListAsync();

            if (TimeRange?.ToLower() == "recent" && sessions.Any())
            {
                sessions = new List<WorkoutSession> { sessions.First() };
            }

            // Calculate Overall Stats
            TotalWorkoutsCompleted = sessions.Count;

            // get every set from all workouts and sessions 
            var allWorkoutSets = sessions.SelectMany(s => s.SessionLogs).ToList();
            TotalWeightLifted = allWorkoutSets.Sum(log => (log.Weight ?? 0) * (log.Reps ?? 0));

            // Calculate average resting time (time between sets)
            var sessionsWithEndTime = sessions.Where(s => s.EndTime.HasValue).ToList();
            if (sessionsWithEndTime.Any())
            {
                var totalSessionTime = sessionsWithEndTime.Sum(s => (s.EndTime!.Value - s.StartTime).TotalMinutes);
                var totalSets = sessionsWithEndTime.SelectMany(s => s.SessionLogs).Count();

                if (totalSets > 0)
                {
                    AverageRestingTime = totalSessionTime / totalSets;
                }
            }

            // Calculate Exercise PRs
            var setsByExercise = allWorkoutSets.GroupBy(log => log.ExerciseId);

            foreach (var sets in setsByExercise)
            {
                var exerciseId = sets.Key;
                var exercise = await _db.Exercises.FindAsync(exerciseId);

                if (exercise == null) continue;

                var maxWeightSet = sets
                    .Where(log => log.Weight.HasValue && log.Reps.HasValue)
                    .OrderByDescending(log => log.Weight)
                    .ThenByDescending(log => log.Reps)
                    .FirstOrDefault();

                var maxVolumeSet = sets
                    .Where(log => log.Weight.HasValue && log.Reps.HasValue)
                    .OrderByDescending(log => log.Weight!.Value * log.Reps!.Value)
                    .FirstOrDefault();

                if (maxWeightSet != null || maxVolumeSet != null)
                {
                    decimal maxVolume = (maxVolumeSet?.Weight ?? 0) * (maxVolumeSet?.Reps ?? 0);
                    ExercisePRs.Add(new ExercisePR(
                        exerciseId,
                        exercise.Name,
                        maxWeightSet?.Weight ?? 0,
                        maxWeightSet?.Reps ?? 0,
                        maxVolume,
                        maxVolumeSet?.Weight ?? 0,
                        maxVolumeSet?.Reps ?? 0
                    ));
                }
            }

            ExercisePRs = ExercisePRs.OrderBy(pr => pr.ExerciseName).ToList();

            // Set selected exercise PR if an exercise is selected
            if (SelectedExerciseId.HasValue)
            {
                SelectedExercisePR = ExercisePRs.FirstOrDefault(pr => pr.ExerciseId == SelectedExerciseId.Value);
            }
        }
    }
}
