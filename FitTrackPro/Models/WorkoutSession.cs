using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitTrackPro.Models
{
    public class WorkoutSession
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        // Optional: Link back to the routine that was performed
        public int? WorkoutRoutineId { get; set; }

        // Navigation property for all the detailed logs in this session
        public ICollection<SessionLog> SessionLogs { get; set; }
    }
}