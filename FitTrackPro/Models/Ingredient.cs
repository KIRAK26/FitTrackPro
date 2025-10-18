using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitTrackPro.Models
{
    public class Ingredient
    {
        [Key]
        public int ingredientId { get; set; }

        [Required]
        public int recipeId { get; set; }

        [Required]
        [StringLength(200)]
        public string ingredientName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string quantity { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string unit { get; set; } = string.Empty;

        [Required]
        public IngredientCategory category { get; set; }

        // Navigation property (Foreign Key)
        [ForeignKey("recipeId")]
        public virtual Recipe recipe { get; set; } = null!;

        // Calculated property for display
        [NotMapped]
        public string displayText => $"{quantity} {unit} {ingredientName}";
    }
}
