using FitTrackPro.Models;
using FitTrackPro.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

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

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _exerciseService.AddExerciseAsync(Exercise);

            return RedirectToPage("./Index");
        }
    }
}
