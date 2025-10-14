using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitTrackPro.Models
{
    // Inherits from abstract NutritionItem (Polymorphism - second derived class)
    public class MealPlan : NutritionItem
    {
        [Key]
        public int mealPlanId { get; set; }

        [Required]
        public DateTime date { get; set; }

        [Required]
        public MealType mealType { get; set; }

        [Required]
        public int recipeId { get; set; }

        [StringLength(500)]
        public string notes { get; set; } = string.Empty;

        public bool isCompleted { get; set; } = false;

        // Navigation property (Foreign Key)
        [ForeignKey("recipeId")]
        public virtual Recipe recipe { get; set; } = null!;

        // Calculated property - day of week
        [NotMapped]
        public string dayOfWeek => date.ToString("dddd");

        // Calculated property - meal slot identifier
        [NotMapped]
        public string mealSlot => $"{date:yyyy-MM-dd}_{mealType}";

        // Override abstract method from NutritionItem (Polymorphism)
        public override decimal calculateNutritionalValue(string macroType)
        {
            if (recipe == null) return 0;

            return macroType.ToLower() switch
            {
                "protein" => recipe.proteinGrams,
                "carbs" => recipe.carbsGrams,
                "fats" => recipe.fatsGrams,
                "calories" => recipe.caloriesPerServing,
                _ => 0
            };
        }

        // Override virtual method from base class
        public override string getDisplayName()
        {
            return $"{mealType} on {date:MMM dd} - {recipe?.recipeName ?? "Unknown"}";
        }
    }
}
