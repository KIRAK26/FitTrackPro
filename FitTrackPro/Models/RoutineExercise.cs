using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitTrackPro.Models
{
    public class RoutineExercise
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to WorkoutRoutine
        public int WorkoutRoutineId { get; set; }
        [ForeignKey("WorkoutRoutineId")]
        public WorkoutRoutine WorkoutRoutine { get; set; }

        // Foreign key to Exercise
        public int ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }

        public int Sets { get; set; }
        public string Reps { get; set; } // Using string to allow ranges like "8-12"
        public int RestPeriodSeconds { get; set; }
    }
}
