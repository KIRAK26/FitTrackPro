using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitTrackPro.Models;

namespace FitTrackPro.Extensions
{
    // Extension methods for nutrition-related calculations
    public static class NutritionExtensions
    {
        // Extension method for Recipe
        public static decimal calculateTotalProtein(this Recipe recipe)
        {
            return recipe.proteinGrams * recipe.servings;
        }

        // Extension method for Recipe
        public static decimal calculateTotalCarbs(this Recipe recipe)
        {
            return recipe.carbsGrams * recipe.servings;
        }

        // Extension method for Recipe
        public static decimal calculateTotalFats(this Recipe recipe)
        {
            return recipe.fatsGrams * recipe.servings;
        }

        // Extension method for Recipe collection
        public static int getTotalCalories(this IEnumerable<Recipe> recipes)
        {
            return recipes.Sum(r => r.totalCalories);
        }

        // Extension method for MealPlan collection
        public static decimal calculateDailyProtein(this IEnumerable<MealPlan> mealPlans)
        {
            return mealPlans.Sum(mp => mp.recipe?.proteinGrams ?? 0);
        }

        // Extension method for MealPlan collection
        public static decimal calculateDailyCarbs(this IEnumerable<MealPlan> mealPlans)
        {
            return mealPlans.Sum(mp => mp.recipe?.carbsGrams ?? 0);
        }

        // Extension method for MealPlan collection
        public static decimal calculateDailyFats(this IEnumerable<MealPlan> mealPlans)
        {
            return mealPlans.Sum(mp => mp.recipe?.fatsGrams ?? 0);
        }

        // Extension method for MealPlan collection
        public static int calculateDailyCalories(this IEnumerable<MealPlan> mealPlans)
        {
            return mealPlans.Sum(mp => mp.recipe?.caloriesPerServing ?? 0);
        }

        // Extension method to check if recipe is high protein
        public static bool isHighProtein(this Recipe recipe)
        {
            decimal totalMacros = recipe.proteinGrams + recipe.carbsGrams + recipe.fatsGrams;
            if (totalMacros == 0) return false;
            decimal proteinPercentage = (recipe.proteinGrams / totalMacros) * 100;
            return proteinPercentage >= 30; // 30% or more protein
        }

        // Extension method to check if recipe is low carb
        public static bool isLowCarb(this Recipe recipe)
        {
            return recipe.carbsGrams <= 20; // 20g or less per serving
        }
    }
}
