using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FitTrackPro.Models
{
    public class MealPlanTemplate
    {
        [Key]
        public int templateId { get; set; }

        [Required]
        [StringLength(200)]
        public string templateName { get; set; } = string.Empty;

        [StringLength(500)]
        public string description { get; set; } = string.Empty;

        public DateTime createdDate { get; set; } = DateTime.UtcNow;

        public int timesUsed { get; set; } = 0;

        // Navigation property - one-to-many relationship
        public virtual ICollection<MealPlanTemplateItem> templateItems { get; set; } = new List<MealPlanTemplateItem>();
    }
}
