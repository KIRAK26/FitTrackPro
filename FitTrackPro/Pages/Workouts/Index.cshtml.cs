using FitTrackPro.Models;
using FitTrackPro.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; // Needed for SelectListItem
using System.Collections.Generic;
using System.Linq; // Needed for .Select
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

        // --- ADDED PROPERTIES FOR FILTERING AND SEARCH ---

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedMuscleGroup { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedDifficulty { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedEquipment { get; set; }

        // Lists to populate the <select> dropdowns
        public List<SelectListItem> MuscleGroups { get; set; }
        public List<SelectListItem> Difficulties { get; set; }
        public List<SelectListItem> Equipment { get; set; }


        public async Task OnGetAsync()
        {
            // --- 1. Populate the dropdown menus ---

            // Get unique data from the service
            var muscleGroupsData = await _exerciseService.GetUniqueMuscleGroupsAsync();
            var difficultiesData = await _exerciseService.GetUniqueDifficultiesAsync();
            var equipmentData = await _exerciseService.GetUniqueEquipmentAsync();

            // Convert string lists to List<SelectListItem>
            MuscleGroups = muscleGroupsData.Select(m => new SelectListItem(m, m)).ToList();
            Difficulties = difficultiesData.Select(d => new SelectListItem(d, d)).ToList();
            Equipment = equipmentData.Select(eq => new SelectListItem(eq, eq)).ToList();

            // Add the "All" option to the beginning of each list
            MuscleGroups.Insert(0, new SelectListItem("All Muscle Groups", ""));
            Difficulties.Insert(0, new SelectListItem("All Difficulties", ""));
            Equipment.Insert(0, new SelectListItem("All Equipment", ""));


            // --- 2. Get main data ---

            // Get routines (unchanged)
            Routines = await _workoutService.GetAllRoutinesAsync();

            // Get exercises (now filtered using the bound properties)
            Exercises = await _exerciseService.GetExercisesFilteredAsync(SearchTerm, SelectedMuscleGroup, SelectedDifficulty, SelectedEquipment);
        }
    }
}