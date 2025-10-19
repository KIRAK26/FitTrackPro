using FitTrackPro.Models;
using FitTrackPro.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; // 1. Add LINQ for Sum() and ToDictionary()

namespace FitTrackPro.Pages.Workouts
{
    public class HistoryModel : PageModel
    {
        private readonly IWorkoutService _workoutService;
        private readonly IExerciseService _exerciseService;

        public HistoryModel(IWorkoutService workoutService, IExerciseService exerciseService)
        {
            _workoutService = workoutService;
            _exerciseService = exerciseService;
        }

        public IList<WorkoutSession> SessionHistory { get; set; }
        public IList<Exercise> AllExercises { get; set; }

        // 2. Add a property to store the calculated training volume
        public Dictionary<int, decimal> SessionTotalVolumes { get; set; }

        public async Task OnGetAsync()
        {
            SessionHistory = await _workoutService.GetSessionHistoryAsync();
            AllExercises = await _exerciseService.GetAllExercisesAsync();

            // 3. Calculate the total volume for each session
            // We use ToDictionary to create a dictionary,
            // Key is the SessionId, Value is the total volume for that Session
            SessionTotalVolumes = SessionHistory.ToDictionary(
                session => session.Id, // Dictionary Key
                session => session.SessionLogs // Dictionary Value
                                               // Ensure Weight and Reps have values
                    .Where(log => log.Weight.HasValue && log.Reps.HasValue)
                    // Calculate (Weight * Reps) and sum them up
                    .Sum(log => log.Weight.Value * log.Reps.Value)
            );
        }

        public Exercise GetExerciseById(int id)
        {
            return AllExercises.FirstOrDefault(e => e.Id == id);
        }
    }
}