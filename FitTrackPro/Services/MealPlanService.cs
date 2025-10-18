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
    // Implementation of IMealPlanService (Demonstrates: Interface implementation, Delegates & Events)
    public class MealPlanService : IMealPlanService
    {
        private readonly ApplicationDbContext context;

        // Event declaration (Assignment requirement: Delegates & Events)
        public event MealPlanUpdatedHandler? onMealPlanUpdated;

        public MealPlanService(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Helper method to trigger event
        private void triggerMealPlanUpdatedEvent(string message, DateTime affectedDate)
        {
            onMealPlanUpdated?.Invoke(message, affectedDate);
        }

        // Get all meal plans for a specific week (LINQ: Where, Include, OrderBy)
        public async Task<List<MealPlan>> getMealPlansForWeekAsync(DateTime startDate)
        {
            // Get Monday of the week
            DateTime weekStart = startDate.Date.AddDays(-(int)startDate.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime weekEnd = weekStart.AddDays(7);

            return await context.mealPlans
                .Include(mp => mp.recipe)
                .ThenInclude(r => r.ingredients)
                .Where(mp => mp.date >= weekStart && mp.date < weekEnd)
                .OrderBy(mp => mp.date)
                .ThenBy(mp => mp.mealType)
                .ToListAsync();
        }

        // Assign a recipe to a specific date and meal type slot
        public async Task<MealPlan> assignRecipeToSlotAsync(DateTime date, MealType mealType, int recipeId, string notes = "")
        {
            // Check if slot already has a meal plan
            var existingPlan = await context.mealPlans
                .FirstOrDefaultAsync(mp => mp.date.Date == date.Date && mp.mealType == mealType);

            if (existingPlan != null)
            {
                // Update existing plan
                existingPlan.recipeId = recipeId;
                existingPlan.notes = notes;
                context.mealPlans.Update(existingPlan);
            }
            else
            {
                // Create new meal plan
                var newPlan = new MealPlan
                {
                    date = date.Date,
                    mealType = mealType,
                    recipeId = recipeId,
                    notes = notes,
                    isCompleted = false,
                    createdDate = DateTime.UtcNow
                };

                context.mealPlans.Add(newPlan);
                existingPlan = newPlan;
            }

            await context.SaveChangesAsync();

            // Trigger event (Demonstrates: Event usage)
            triggerMealPlanUpdatedEvent($"Meal plan updated for {date:MMM dd} - {mealType}", date);

            return existingPlan;
        }

        // Copy all meals from one day to another day (LINQ: Where, Select)
        public async Task<bool> copyDayAsync(DateTime sourceDate, DateTime targetDate)
        {
            // Get all meal plans for source date
            var sourcePlans = await context.mealPlans
                .Include(mp => mp.recipe)
                .Where(mp => mp.date.Date == sourceDate.Date)
                .ToListAsync();

            if (!sourcePlans.Any())
                return false;

            // Remove existing plans for target date
            var targetPlans = await context.mealPlans
                .Where(mp => mp.date.Date == targetDate.Date)
                .ToListAsync();

            context.mealPlans.RemoveRange(targetPlans);

            // Create new plans for target date
            var newPlans = sourcePlans.Select(sp => new MealPlan
            {
                date = targetDate.Date,
                mealType = sp.mealType,
                recipeId = sp.recipeId,
                notes = sp.notes,
                isCompleted = false,
                createdDate = DateTime.UtcNow
            }).ToList();

            context.mealPlans.AddRange(newPlans);
            await context.SaveChangesAsync();

            // Trigger event
            triggerMealPlanUpdatedEvent($"Copied {sourcePlans.Count} meals from {sourceDate:MMM dd} to {targetDate:MMM dd}", targetDate);

            return true;
        }

        // Clear all meal plans for a week
        public async Task<bool> clearWeekAsync(DateTime startDate)
        {
            DateTime weekStart = startDate.Date.AddDays(-(int)startDate.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime weekEnd = weekStart.AddDays(7);

            var plansToRemove = await context.mealPlans
                .Where(mp => mp.date >= weekStart && mp.date < weekEnd)
                .ToListAsync();

            if (!plansToRemove.Any())
                return false;

            context.mealPlans.RemoveRange(plansToRemove);
            await context.SaveChangesAsync();

            // Trigger event
            triggerMealPlanUpdatedEvent($"Cleared all meal plans for week of {weekStart:MMM dd}", weekStart);

            return true;
        }

        // Save current week as a reusable template
        public async Task<MealPlanTemplate> saveWeekAsTemplateAsync(DateTime startDate, string templateName, string description)
        {
            DateTime weekStart = startDate.Date.AddDays(-(int)startDate.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime weekEnd = weekStart.AddDays(7);

            // Get meal plans for the week
            var weekPlans = await context.mealPlans
                .Where(mp => mp.date >= weekStart && mp.date < weekEnd)
                .OrderBy(mp => mp.date)
                .ThenBy(mp => mp.mealType)
                .ToListAsync();

            // Create template
            var template = new MealPlanTemplate
            {
                templateName = templateName,
                description = description,
                createdDate = DateTime.UtcNow,
                timesUsed = 0
            };

            // Create template items
            foreach (var plan in weekPlans)
            {
                int dayOfWeek = ((int)plan.date.DayOfWeek + 6) % 7; // Convert to Monday=0 format

                var templateItem = new MealPlanTemplateItem
                {
                    dayOfWeek = dayOfWeek,
                    mealType = plan.mealType,
                    recipeId = plan.recipeId
                };

                template.templateItems.Add(templateItem);
            }

            context.mealPlanTemplates.Add(template);
            await context.SaveChangesAsync();

            return template;
        }

        // Load a template and apply it to a specific week
        public async Task<bool> loadTemplateAsync(int templateId, DateTime startDate)
        {
            var template = await context.mealPlanTemplates
                .Include(t => t.templateItems)
                .ThenInclude(ti => ti.recipe)
                .FirstOrDefaultAsync(t => t.templateId == templateId);

            if (template == null || !template.templateItems.Any())
                return false;

            DateTime weekStart = startDate.Date.AddDays(-(int)startDate.DayOfWeek + (int)DayOfWeek.Monday);

            // Clear existing plans for the week
            await clearWeekAsync(weekStart);

            // Create meal plans from template
            var newPlans = new List<MealPlan>();
            foreach (var item in template.templateItems)
            {
                DateTime mealDate = weekStart.AddDays(item.dayOfWeek);

                var plan = new MealPlan
                {
                    date = mealDate,
                    mealType = item.mealType,
                    recipeId = item.recipeId,
                    notes = $"From template: {template.templateName}",
                    isCompleted = false,
                    createdDate = DateTime.UtcNow
                };

                newPlans.Add(plan);
            }

            context.mealPlans.AddRange(newPlans);

            // Update times used
            template.timesUsed++;
            context.mealPlanTemplates.Update(template);

            await context.SaveChangesAsync();

            // Trigger event
            triggerMealPlanUpdatedEvent($"Loaded template '{template.templateName}' for week of {weekStart:MMM dd}", weekStart);

            return true;
        }

        // Get all available templates (LINQ: OrderByDescending, Include)
        public async Task<List<MealPlanTemplate>> getAllTemplatesAsync()
        {
            return await context.mealPlanTemplates
                .Include(t => t.templateItems)
                .ThenInclude(ti => ti.recipe)
                .OrderByDescending(t => t.timesUsed)
                .ThenByDescending(t => t.createdDate)
                .ToListAsync();
        }

        // Calculate daily nutrition totals (LINQ: Where, Include, Sum with Extension Methods)
        public async Task<Dictionary<string, decimal>> calculateDailyNutritionAsync(DateTime date)
        {
            var dailyPlans = await context.mealPlans
                .Include(mp => mp.recipe)
                .Where(mp => mp.date.Date == date.Date)
                .ToListAsync();

            if (!dailyPlans.Any())
            {
                return new Dictionary<string, decimal>
                {
                    { "calories", 0 },
                    { "protein", 0 },
                    { "carbs", 0 },
                    { "fats", 0 }
                };
            }

            // Using extension methods to calculate nutrition
            return new Dictionary<string, decimal>
            {
                { "calories", dailyPlans.calculateDailyCalories() },
                { "protein", dailyPlans.calculateDailyProtein() },
                { "carbs", dailyPlans.calculateDailyCarbs() },
                { "fats", dailyPlans.calculateDailyFats() }
            };
        }

        // Calculate weekly nutrition averages (LINQ: Where, Include, Average)
        public async Task<Dictionary<string, decimal>> calculateWeeklyNutritionAsync(DateTime startDate)
        {
            DateTime weekStart = startDate.Date.AddDays(-(int)startDate.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime weekEnd = weekStart.AddDays(7);

            var weekPlans = await context.mealPlans
                .Include(mp => mp.recipe)
                .Where(mp => mp.date >= weekStart && mp.date < weekEnd)
                .ToListAsync();

            if (!weekPlans.Any())
            {
                return new Dictionary<string, decimal>
                {
                    { "avgCalories", 0 },
                    { "avgProtein", 0 },
                    { "avgCarbs", 0 },
                    { "avgFats", 0 },
                    { "totalCalories", 0 },
                    { "totalProtein", 0 },
                    { "totalCarbs", 0 },
                    { "totalFats", 0 }
                };
            }

            // Group by date and calculate daily totals
            var dailyTotals = weekPlans
                .GroupBy(mp => mp.date.Date)
                .Select(g => new
                {
                    date = g.Key,
                    calories = g.Sum(mp => mp.recipe.caloriesPerServing),
                    protein = g.Sum(mp => mp.recipe.proteinGrams),
                    carbs = g.Sum(mp => mp.recipe.carbsGrams),
                    fats = g.Sum(mp => mp.recipe.fatsGrams)
                })
                .ToList();

            int daysWithPlans = dailyTotals.Count;

            return new Dictionary<string, decimal>
            {
                { "avgCalories", (decimal)dailyTotals.Average(d => d.calories) },
                { "avgProtein", dailyTotals.Average(d => d.protein) },
                { "avgCarbs", dailyTotals.Average(d => d.carbs) },
                { "avgFats", dailyTotals.Average(d => d.fats) },
                { "totalCalories", dailyTotals.Sum(d => d.calories) },
                { "totalProtein", dailyTotals.Sum(d => d.protein) },
                { "totalCarbs", dailyTotals.Sum(d => d.carbs) },
                { "totalFats", dailyTotals.Sum(d => d.fats) },
                { "daysPlanned", daysWithPlans }
            };
        }

        // Remove a meal plan entry
        public async Task<bool> removeMealPlanAsync(int mealPlanId)
        {
            var plan = await context.mealPlans.FindAsync(mealPlanId);
            if (plan == null)
                return false;

            context.mealPlans.Remove(plan);
            await context.SaveChangesAsync();

            // Trigger event
            triggerMealPlanUpdatedEvent($"Removed meal plan for {plan.date:MMM dd} - {plan.mealType}", plan.date);

            return true;
        }

        // Toggle meal completion status
        public async Task<bool> toggleMealCompletedAsync(int mealPlanId)
        {
            var plan = await context.mealPlans.FindAsync(mealPlanId);
            if (plan == null)
                return false;

            plan.isCompleted = !plan.isCompleted;
            context.mealPlans.Update(plan);
            await context.SaveChangesAsync();

            // Trigger event
            string status = plan.isCompleted ? "completed" : "uncompleted";
            triggerMealPlanUpdatedEvent($"Marked meal as {status} for {plan.date:MMM dd} - {plan.mealType}", plan.date);

            return true;
        }

        // Delete a template
        public async Task<bool> deleteTemplateAsync(int templateId)
        {
            var template = await context.mealPlanTemplates
                .Include(t => t.templateItems)
                .FirstOrDefaultAsync(t => t.templateId == templateId);

            if (template == null)
                return false;

            // Remove template items first (cascade delete should handle this, but being explicit)
            context.mealPlanTemplateItems.RemoveRange(template.templateItems);

            // Remove template
            context.mealPlanTemplates.Remove(template);

            await context.SaveChangesAsync();

            return true;
        }
    }
}
