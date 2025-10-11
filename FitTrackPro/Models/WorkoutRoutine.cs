using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FitTrackPro.Models
{
    public class WorkoutRoutine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        // Navigation property for all the exercises in this routine
        public ICollection<RoutineExercise> RoutineExercises { get; set; }
    }
}
