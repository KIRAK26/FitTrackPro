using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitTrackPro.Models;

namespace FitTrackPro.Services
{
    // Interface for Recipe service operations (Assignment requirement: Interfaces)
    public interface IRecipeService
    {
        // Get all recipes
        Task<List<Recipe>> getAllRecipesAsync();

        // Get recipe by ID with ingredients
        Task<Recipe?> getRecipeByIdAsync(int recipeId);

        // Get recipes filtered by category
        Task<List<Recipe>> getRecipesByCategoryAsync(RecipeCategory category);

        // Search recipes by name
        Task<List<Recipe>> searchRecipesAsync(string searchTerm);

        // Create new custom recipe
        Task<Recipe> createRecipeAsync(Recipe recipe, List<Ingredient> ingredients);

        // Update existing recipe
        Task<bool> updateRecipeAsync(Recipe recipe);

        // Delete recipe (only custom recipes)
        Task<bool> deleteRecipeAsync(int recipeId);

        // Calculate macro percentages for a recipe
        Dictionary<string, decimal> calculateMacroPercentages(Recipe recipe);

        // Get recipe statistics
        Task<Dictionary<string, int>> getRecipeStatisticsAsync();
    }
}
