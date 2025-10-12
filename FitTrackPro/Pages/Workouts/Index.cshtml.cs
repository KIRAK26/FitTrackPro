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

        public IndexModel(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public IList<Exercise> Exercises { get; set; }

        public async Task OnGetAsync()
        {
            // Use the service to get all exercises from the database
            Exercises = await _exerciseService.GetAllExercisesAsync();
        }
    }
}
