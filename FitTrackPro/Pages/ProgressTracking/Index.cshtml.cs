using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitTrackPro.Data;
using FitTrackPro.Models;

namespace FitTrackPro.Pages.ProgressTracking
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty(SupportsGet = true)]
        public string? TimeRange { get; set; }

        public List<BodyMeasurement> Measurements { get; set; } = new();
        public BodyMeasurement? AverageMeasurement { get; set; }
        public int MeasurementCount { get; set; }

        public async Task OnGetAsync()
        {
            var query = _db.BodyMeasurements.AsQueryable();

            var cutoffDate = (TimeRange?.ToLower()) switch
            {
                "weekly" => DateTime.Now.AddDays(-7),
                "monthly" => DateTime.Now.AddDays(-30),
                "recent" => DateTime.MinValue, 
                _ => DateTime.MinValue
            };

            if (cutoffDate != DateTime.MinValue && TimeRange?.ToLower() != "recent")
            {
                query = query.Where(m => m.Date >= cutoffDate);
            }

            Measurements = await query
                .OrderByDescending(m => m.Date)
                .ToListAsync();

            if (TimeRange?.ToLower() == "recent" && Measurements.Any())
            {
                var latest = Measurements.First();
                AverageMeasurement = latest;
                MeasurementCount = 1;
            }
            else
            {
                MeasurementCount = Measurements.Count;
                if (Measurements.Any())
                {
                    AverageMeasurement = new BodyMeasurement
                    {
                        Date = Measurements.First().Date,
                        Weight = Measurements.Average(m => m.Weight),
                        BodyFat = Measurements.Any(m => m.BodyFat.HasValue)
                            ? Measurements.Where(m => m.BodyFat.HasValue).Average(m => m.BodyFat!.Value)
                            : null,
                        MuscleMass = Measurements.Any(m => m.MuscleMass.HasValue)
                            ? Measurements.Where(m => m.MuscleMass.HasValue).Average(m => m.MuscleMass!.Value)
                            : null,
                        Chest = Measurements.Any(m => m.Chest.HasValue)
                            ? Measurements.Where(m => m.Chest.HasValue).Average(m => m.Chest!.Value)
                            : null,
                        Arms = Measurements.Any(m => m.Arms.HasValue)
                            ? Measurements.Where(m => m.Arms.HasValue).Average(m => m.Arms!.Value)
                            : null,
                        Waist = Measurements.Any(m => m.Waist.HasValue)
                            ? Measurements.Where(m => m.Waist.HasValue).Average(m => m.Waist!.Value)
                            : null,
                        Legs = Measurements.Any(m => m.Legs.HasValue)
                            ? Measurements.Where(m => m.Legs.HasValue).Average(m => m.Legs!.Value)
                            : null
                    };
                }
            }
        }
    }
}
