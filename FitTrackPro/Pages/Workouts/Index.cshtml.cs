using FitTrackPro.Models;
using FitTrackPro.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitTrackPro.Pages.Workouts
{
    public class IndexModel : PageModel
    {
        private readonly IExerciseService _exerciseService;
        private readonly IWorkoutService _workoutService;

        public IndexModel(IExerciseService exerciseService, IWorkoutService workoutService)
        {
            _exerciseService = exerciseService;
            _workoutService = workoutService;
        }

        public IList<Exercise> Exercises { get; set; }
        public IList<WorkoutRoutine> Routines { get; set; }

        public async Task OnGetAsync()
        {
            // Get all exercises for the library view
            Exercises = await _exerciseService.GetAllExercisesAsync();

            // Also get all created routines to display
            Routines = await _workoutService.GetAllRoutinesAsync();
        }
    }
}