using FitTrackPro.Models;
using FitTrackPro.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitTrackPro.Pages.Workouts
{
    public class BuilderModel : PageModel
    {
        private readonly IExerciseService _exerciseService;
        private readonly IWorkoutService _workoutService;

        public BuilderModel(IExerciseService exerciseService, IWorkoutService workoutService)
        {
            _exerciseService = exerciseService;
            _workoutService = workoutService;
        }

        [BindProperty]
        public WorkoutRoutine WorkoutRoutine { get; set; }

        [BindProperty]
        public List<int> SelectedExerciseIds { get; set; }

        public List<Exercise> AllExercises { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AllExercises = await _exerciseService.GetAllExercisesAsync();
            WorkoutRoutine = new WorkoutRoutine(); // Initialize for the form
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || SelectedExerciseIds == null || !SelectedExerciseIds.Any())
            {
                // If something is wrong, reload the page with the list of exercises
                AllExercises = await _exerciseService.GetAllExercisesAsync();
                return Page();
            }

            // Create RoutineExercise objects for each selected exercise
            WorkoutRoutine.RoutineExercises = SelectedExerciseIds.Select(id => new RoutineExercise
            {
                ExerciseId = id,
                // Default values, can be edited later
                Sets = 3,
                Reps = "10",
                RestPeriodSeconds = 60
            }).ToList();

            await _workoutService.CreateWorkoutRoutineAsync(WorkoutRoutine);

            // For now, redirect back to the exercise library
            return RedirectToPage("./Index");
        }
    }
}
