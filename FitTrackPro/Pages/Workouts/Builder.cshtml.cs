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
        public WorkoutRoutine RoutineInput { get; set; }

        [BindProperty]
        public List<int> SelectedExerciseIds { get; set; }

        public List<Exercise> AllExercises { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AllExercises = await _exerciseService.GetAllExercisesAsync();
            RoutineInput = new WorkoutRoutine(); // Initialize for the form
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // The asp-for tag helpers bind to RoutineInput, so we check that
            if (!ModelState.IsValid || SelectedExerciseIds == null || !SelectedExerciseIds.Any())
            {
                // If something is wrong, reload the page with the list of exercises
                AllExercises = await _exerciseService.GetAllExercisesAsync();
                return Page();
            }

            // --- REVISED LOGIC ---
            // 1. Create a new routine object instead of using the bound one directly.
            var newRoutine = new WorkoutRoutine
            {
                Name = RoutineInput.Name,
                Description = RoutineInput.Description
            };

            // 2. Create the list of exercises for this new routine.
            newRoutine.RoutineExercises = SelectedExerciseIds.Select(id => new RoutineExercise
            {
                ExerciseId = id,
                Sets = 3,
                Reps = "10",
                RestPeriodSeconds = 60
            }).ToList();

            // 3. Save the newly created and fully populated routine.
            await _workoutService.CreateWorkoutRoutineAsync(newRoutine);

            return RedirectToPage("./Index");
        }
    }
}