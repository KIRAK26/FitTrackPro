using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitTrackPro.Models
{
    public class SessionLog
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to WorkoutSession
        public int WorkoutSessionId { get; set; }
        [ForeignKey("WorkoutSessionId")]
        public WorkoutSession ? WorkoutSession { get; set; }

        // The exercise that was performed
        public int ExerciseId { get; set; }

        public int SetNumber { get; set; }
        public decimal? Weight { get; set; }
        public int? Reps { get; set; }
        public string? Notes { get; set; }
    }
}
