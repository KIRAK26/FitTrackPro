using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitTrackPro.Models
{
    // Inherits from abstract NutritionItem (Polymorphism)
    public class Recipe : NutritionItem
    {
        [Key]
        public int recipeId { get; set; }

        [Required]
        [StringLength(200)]
        public string recipeName { get; set; } = string.Empty;

        [Required]
        public RecipeCategory category { get; set; }

        [Range(1, 1440)]
        public int prepTimeMinutes { get; set; }

        [Range(1, 100)]
        public int servings { get; set; }

        [Required]
        public string instructions { get; set; } = string.Empty;

        [Range(0, 10000)]
        public int caloriesPerServing { get; set; }

        [Range(0, 1000)]
        public decimal proteinGrams { get; set; }

        [Range(0, 1000)]
        public decimal carbsGrams { get; set; }

        [Range(0, 1000)]
        public decimal fatsGrams { get; set; }

        public bool isCustom { get; set; } = false;

        // Navigation properties (Entity Framework relationships)
        public virtual ICollection<Ingredient> ingredients { get; set; } = new List<Ingredient>();
        public virtual ICollection<MealPlan> mealPlans { get; set; } = new List<MealPlan>();

        // Calculated property without backing field
        [NotMapped]
        public int totalCalories => caloriesPerServing * servings;

        // Calculated property for macro summary
        [NotMapped]
        public string macroSummary
        {
            get
            {
                decimal totalMacros = proteinGrams + carbsGrams + fatsGrams;
                if (totalMacros == 0) return "N/A";

                int proteinPercent = (int)((proteinGrams / totalMacros) * 100);
                int carbsPercent = (int)((carbsGrams / totalMacros) * 100);
                int fatsPercent = (int)((fatsGrams / totalMacros) * 100);

                return $"P: {proteinPercent}% | C: {carbsPercent}% | F: {fatsPercent}%";
            }
        }

        // Calculated property for display
        [NotMapped]
        public string prepTimeDisplay
        {
            get
            {
                if (prepTimeMinutes < 60)
                    return $"{prepTimeMinutes} min";
                else
                {
                    int hours = prepTimeMinutes / 60;
                    int mins = prepTimeMinutes % 60;
                    return mins > 0 ? $"{hours}h {mins}m" : $"{hours}h";
                }
            }
        }

        // Override abstract method from NutritionItem (Polymorphism)
        public override decimal calculateNutritionalValue(string macroType)
        {
            return macroType.ToLower() switch
            {
                "protein" => proteinGrams * servings,
                "carbs" => carbsGrams * servings,
                "fats" => fatsGrams * servings,
                "calories" => caloriesPerServing * servings,
                _ => 0
            };
        }

        // Override virtual method from base class
        public override string getDisplayName()
        {
            return $"{recipeName} ({category})";
        }
    }
}
