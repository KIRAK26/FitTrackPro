using FitTrackPro.Models;
using FitTrackPro.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering; 
using System.Collections.Generic;      
using System.Linq;

namespace FitTrackPro.Pages.Workouts
{
    public class CreateModel : PageModel
    {
        private readonly IExerciseService _exerciseService;

        public CreateModel(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [BindProperty]
        public Exercise Exercise { get; set; }
        public List<SelectListItem> MuscleGroups { get; set; }
        public List<SelectListItem> Difficulties { get; set; }
        public List<SelectListItem> Equipment { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateDropdownsAsync(); 
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(); // Re-populate lists if validation fails
                return Page();
            }

            await _exerciseService.AddExerciseAsync(Exercise);

            return RedirectToPage("./Index");


        }


        private async Task PopulateDropdownsAsync()
        {
            var muscleGroupsData = await _exerciseService.GetUniqueMuscleGroupsAsync();
            var difficultiesData = await _exerciseService.GetUniqueDifficultiesAsync();
            var equipmentData = await _exerciseService.GetUniqueEquipmentAsync();

            MuscleGroups = muscleGroupsData.Select(m => new SelectListItem(m, m)).ToList();
            Difficulties = difficultiesData.Select(d => new SelectListItem(d, d)).ToList();
            Equipment = equipmentData.Select(eq => new SelectListItem(eq, eq)).ToList();

            // Add a "Please select" option at the top
            MuscleGroups.Insert(0, new SelectListItem("Select Muscle Group", ""));
            Difficulties.Insert(0, new SelectListItem("Select Difficulty", ""));
            Equipment.Insert(0, new SelectListItem("Select Equipment", ""));
        }
    }
}
