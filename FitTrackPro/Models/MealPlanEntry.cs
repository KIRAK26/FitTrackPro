// FitTrackPro/Models/MealPlanEntry.cs
using System.ComponentModel.DataAnnotations;

namespace FitTrackPro.Models
{
    public class MealPlanEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string MealType { get; set; } = string.Empty; // breakfast, lunch, dinner

        [Required]
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; } = null!;

        public int Servings { get; set; } = 1; // How many servings of this recipe

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
