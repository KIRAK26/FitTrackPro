using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitTrackPro.Models;

namespace FitTrackPro.Services
{
    // Delegate for meal plan updated event (Assignment requirement: Delegates)
    public delegate void MealPlanUpdatedHandler(string message, DateTime affectedDate);

    // Interface for Meal Plan service operations (Assignment requirement: Interfaces)
    public interface IMealPlanService
    {
        // Event using the delegate
        event MealPlanUpdatedHandler onMealPlanUpdated;

        // Get all meal plans for a specific week
        Task<List<MealPlan>> getMealPlansForWeekAsync(DateTime startDate);

        // Assign a recipe to a specific date and meal type slot
        Task<MealPlan> assignRecipeToSlotAsync(DateTime date, MealType mealType, int recipeId, string notes = "");

        // Copy all meals from one day to another day
        Task<bool> copyDayAsync(DateTime sourceDate, DateTime targetDate);

        // Clear all meal plans for a week
        Task<bool> clearWeekAsync(DateTime startDate);

        // Save current week as a reusable template
        Task<MealPlanTemplate> saveWeekAsTemplateAsync(DateTime startDate, string templateName, string description);

        // Load a template and apply it to a specific week
        Task<bool> loadTemplateAsync(int templateId, DateTime startDate);

        // Get all available templates
        Task<List<MealPlanTemplate>> getAllTemplatesAsync();

        // Calculate daily nutrition totals for a specific date
        Task<Dictionary<string, decimal>> calculateDailyNutritionAsync(DateTime date);

        // Calculate weekly nutrition averages
        Task<Dictionary<string, decimal>> calculateWeeklyNutritionAsync(DateTime startDate);

        // Remove a meal plan entry
        Task<bool> removeMealPlanAsync(int mealPlanId);
    }
}
