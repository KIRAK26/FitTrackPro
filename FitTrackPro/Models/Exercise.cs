// FitTrackPro/Models/Exercise.cs
using System.ComponentModel.DataAnnotations;

namespace FitTrackPro.Models
{
    public class Exercise
    {
        [Key]  // This represents the primary key in the database
        public int Id { get; set; }

        [Required] // This indicates that the field is required
        public string Name { get; set; }
        [Required]
        public string MuscleGroup { get; set; }  // Target muscle group
        [Required]
        public string Difficulty { get; set; } // Difficutly 
        [Required]
        public string Equipment { get; set; } // Required equipment
        [Required]
        public string Instructions { get; set; }  // Instructions

        public decimal? CaloriesBurnedPerRep { get; set; }

        public decimal? CaloriesBurnedPerMinute { get; set; }

    }
}