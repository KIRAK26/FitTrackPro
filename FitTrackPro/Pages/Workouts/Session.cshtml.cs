using FitTrackPro.Models;
using FitTrackPro.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FitTrackPro.Pages.Workouts
{
    public class SessionModel : PageModel
    {
        private readonly IWorkoutService _workoutService;

        public SessionModel(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        public WorkoutRoutine CurrentRoutine { get; set; }

        public async Task<IActionResult> OnGetAsync(int routineId)
        {
            CurrentRoutine = await _workoutService.GetRoutineByIdAsync(routineId);

            if (CurrentRoutine == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
