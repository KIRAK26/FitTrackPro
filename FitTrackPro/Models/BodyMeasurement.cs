using System.ComponentModel.DataAnnotations;

namespace FitTrackPro.Models
{
    public class BodyMeasurement
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [Range(20, 500)]
        public float Weight { get; set; }

        [Range(0, 100)]
        public float? BodyFat { get; set; }
        [Range(0, 100)]
        public float? MuscleMass { get; set; }

        public float? Chest { get; set; }
        public float? Arms { get; set; }
        public float? Waist { get; set; }
        public float? Legs { get; set; }
    }
}