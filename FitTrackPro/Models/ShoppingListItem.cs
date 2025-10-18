using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitTrackPro.Models
{
    public class ShoppingListItem
    {
        [Key]
        public int itemId { get; set; }

        [Required]
        [StringLength(200)]
        public string ingredientName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string totalQuantity { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string unit { get; set; } = string.Empty;

        [Required]
        public IngredientCategory category { get; set; }

        public bool isChecked { get; set; } = false;

        public DateTime generatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? checkedDate { get; set; }

        // Date range for which this shopping list was generated
        public DateTime? rangeStartDate { get; set; }

        public DateTime? rangeEndDate { get; set; }

        // Calculated property for display
        [NotMapped]
        public string displayText => $"{totalQuantity} {unit} {ingredientName}";

        // Calculated property for status icon
        [NotMapped]
        public string statusIcon => isChecked ? "✓" : "○";
    }
}
