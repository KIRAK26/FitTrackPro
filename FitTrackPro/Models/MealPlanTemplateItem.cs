using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitTrackPro.Models
{
    public class MealPlanTemplateItem
    {
        [Key]
        public int itemId { get; set; }

        [Required]
        public int templateId { get; set; }

        [Required]
        [Range(0, 6)] // 0=Monday, 6=Sunday
        public int dayOfWeek { get; set; }

        [Required]
        public MealType mealType { get; set; }

        [Required]
        public int recipeId { get; set; }

        // Navigation properties (Foreign Keys)
        [ForeignKey("templateId")]
        public virtual MealPlanTemplate template { get; set; } = null!;

        [ForeignKey("recipeId")]
        public virtual Recipe recipe { get; set; } = null!;

        // Helper property for day name
        [NotMapped]
        public string dayName => dayOfWeek switch
        {
            0 => "Monday",
            1 => "Tuesday",
            2 => "Wednesday",
            3 => "Thursday",
            4 => "Friday",
            5 => "Saturday",
            6 => "Sunday",
            _ => "Unknown"
        };
    }
}
