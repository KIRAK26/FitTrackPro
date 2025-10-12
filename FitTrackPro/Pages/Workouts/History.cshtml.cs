using FitTrackPro.Models;
using FitTrackPro.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task OnGetAsync()
        {
            SessionHistory = await _workoutService.GetSessionHistoryAsync();
            AllExercises = await _exerciseService.GetAllExercisesAsync();
        }

        public Exercise GetExerciseById(int id)
        {
            return AllExercises.FirstOrDefault(e => e.Id == id);
        }
    }
}