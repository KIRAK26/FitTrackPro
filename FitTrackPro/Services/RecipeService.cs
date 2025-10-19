using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitTrackPro.Data;
using FitTrackPro.Models;
using FitTrackPro.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FitTrackPro.Services
{
    // Implementation of IRecipeService (Demonstrates: Interface implementation)
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext context;

        public RecipeService(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Get all recipes with eager loading of ingredients (LINQ: Include)
        public async Task<List<Recipe>> getAllRecipesAsync()
        {
            return await context.recipes
                .Include(r => r.ingredients)
                .OrderBy(r => r.recipeName)
                .ToListAsync();
        }

        // Get recipe by ID with ingredients (LINQ: Include, FirstOrDefaultAsync)
        public async Task<Recipe?> getRecipeByIdAsync(int recipeId)
        {
            return await context.recipes
                .Include(r => r.ingredients)
                .FirstOrDefaultAsync(r => r.recipeId == recipeId);
        }

        // Get recipes filtered by category (LINQ: Where, Include, OrderBy)
        public async Task<List<Recipe>> getRecipesByCategoryAsync(RecipeCategory category)
        {
            return await context.recipes
                .Include(r => r.ingredients)
                .Where(r => r.category == category)
                .OrderBy(r => r.recipeName)
                .ToListAsync();
        }

        // Search recipes by name (LINQ: Where with Contains, Include)
        public async Task<List<Recipe>> searchRecipesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await getAllRecipesAsync();
            }

            return await context.recipes
                .Include(r => r.ingredients)
                .Where(r => r.recipeName.ToLower().Contains(searchTerm.ToLower()))
                .OrderBy(r => r.recipeName)
                .ToListAsync();
        }

        // Create new custom recipe with ingredients
        public async Task<Recipe> createRecipeAsync(Recipe recipe, List<Ingredient> ingredients)
        {
            recipe.isCustom = true;
            recipe.createdDate = DateTime.UtcNow;

            // Add ingredients to recipe
            recipe.ingredients = ingredients;

            context.recipes.Add(recipe);
            await context.SaveChangesAsync();

            return recipe;
        }

        // Update existing recipe
        public async Task<bool> updateRecipeAsync(Recipe recipe)
        {
            var existingRecipe = await context.recipes.FindAsync(recipe.recipeId);
            if (existingRecipe == null)
                return false;

            // Update properties
            existingRecipe.recipeName = recipe.recipeName;
            existingRecipe.category = recipe.category;
            existingRecipe.prepTimeMinutes = recipe.prepTimeMinutes;
            existingRecipe.servings = recipe.servings;
            existingRecipe.instructions = recipe.instructions;
            existingRecipe.caloriesPerServing = recipe.caloriesPerServing;
            existingRecipe.proteinGrams = recipe.proteinGrams;
            existingRecipe.carbsGrams = recipe.carbsGrams;
            existingRecipe.fatsGrams = recipe.fatsGrams;

            context.recipes.Update(existingRecipe);
            await context.SaveChangesAsync();

            return true;
        }

        // Delete recipe (only custom recipes can be deleted)
        public async Task<bool> deleteRecipeAsync(int recipeId)
        {
            var recipe = await context.recipes
                .Include(r => r.mealPlans)
                .FirstOrDefaultAsync(r => r.recipeId == recipeId);

            if (recipe == null || !recipe.isCustom)
                return false;

            // Check if recipe is used in any meal plans
            if (recipe.mealPlans.Any())
                return false;

            context.recipes.Remove(recipe);
            await context.SaveChangesAsync();

            return true;
        }

        // Calculate macro percentages for display
        public Dictionary<string, decimal> calculateMacroPercentages(Recipe recipe)
        {
            decimal totalMacros = recipe.proteinGrams + recipe.carbsGrams + recipe.fatsGrams;

            if (totalMacros == 0)
            {
                return new Dictionary<string, decimal>
                {
                    { "protein", 0 },
                    { "carbs", 0 },
                    { "fats", 0 }
                };
            }

            return new Dictionary<string, decimal>
            {
                { "protein", Math.Round((recipe.proteinGrams / totalMacros) * 100, 1) },
                { "carbs", Math.Round((recipe.carbsGrams / totalMacros) * 100, 1) },
                { "fats", Math.Round((recipe.fatsGrams / totalMacros) * 100, 1) }
            };
        }

        // Get recipe statistics (LINQ: GroupBy, ToDictionary, Count)
        public async Task<Dictionary<string, int>> getRecipeStatisticsAsync()
        {
            var totalCount = await context.recipes.CountAsync();

            // Group recipes by category and count them
            var categoryStats = await context.recipes
                .GroupBy(r => r.category)
                .ToDictionaryAsync(g => g.Key.ToString(), g => g.Count());

            var customCount = await context.recipes
                .Where(r => r.isCustom)
                .CountAsync();

            var result = new Dictionary<string, int>
            {
                { "Total", totalCount },
                { "Custom", customCount },
                { "PreLoaded", totalCount - customCount }
            };

            // Add category counts
            foreach (var stat in categoryStats)
            {
                result.Add(stat.Key, stat.Value);
            }

            return result;
        }
    }
}
