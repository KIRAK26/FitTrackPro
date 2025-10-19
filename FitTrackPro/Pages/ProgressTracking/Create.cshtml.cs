using FitTrackPro.Data;
using FitTrackPro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;

namespace FitTrackPro.Pages.ProgressTracking
{
    public class CreateModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        [BindProperty]
        public BodyMeasurement BodyMeasurement { get; set; }

        public CreateModel(ApplicationDbContext context)
        {
            _db = context;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            BodyMeasurement.Date = DateTime.Now;

            _db.BodyMeasurements.Add(BodyMeasurement);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
