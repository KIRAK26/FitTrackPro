using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitTrackPro.Models;
using Microsoft.EntityFrameworkCore;

namespace FitTrackPro.Data
{
    public static class MealPlanDataSeeder
    {
        public static async Task SeedMealPlansAsync(ApplicationDbContext context)
        {
            if (await context.mealPlans.AnyAsync())
            {
                Console.WriteLine("Meal plans already seeded. Skipping...");
                return;
            }

            Console.WriteLine("Seeding meal plans for the last 7 days...");

            var recipes = await context.recipes.Take(20).ToListAsync();
            if (!recipes.Any())
            {
                Console.WriteLine("No recipes found. Please seed recipes first.");
                return;
            }

            var mealPlans = new List<MealPlan>();
            var random = new Random();

            // Seed meals for the last 7 days
            for (int i = 0; i < 7; i++)
            {
                var date = DateTime.Today.AddDays(-i);
                var breakfastRecipe = recipes.Where(r => r.category == RecipeCategory.Breakfast).OrderBy(x => random.Next()).FirstOrDefault();
                if (breakfastRecipe != null)
                {
                    mealPlans.Add(new MealPlan
                    {
                        date = date,
                        mealType = MealType.Breakfast,
                        recipeId = breakfastRecipe.recipeId,
                        notes = "Auto-seeded meal",
                        isCompleted = true
                    });
                }
                var lunchRecipe = recipes.Where(r => r.category == RecipeCategory.Lunch).OrderBy(x => random.Next()).FirstOrDefault();
                if (lunchRecipe != null)
                {
                    mealPlans.Add(new MealPlan
                    {
                        date = date,
                        mealType = MealType.Lunch,
                        recipeId = lunchRecipe.recipeId,
                        notes = "Auto-seeded meal",
                        isCompleted = true
                    });
                }
                var dinnerRecipe = recipes.Where(r => r.category == RecipeCategory.Dinner).OrderBy(x => random.Next()).FirstOrDefault();
                if (dinnerRecipe != null)
                {
                    mealPlans.Add(new MealPlan
                    {
                        date = date,
                        mealType = MealType.Dinner,
                        recipeId = dinnerRecipe.recipeId,
                        notes = "Auto-seeded meal",
                        isCompleted = true
                    });
                }
            }

            await context.mealPlans.AddRangeAsync(mealPlans);
            await context.SaveChangesAsync();

            Console.WriteLine($"Successfully seeded {mealPlans.Count} meal plans for the last 7 days!");
        }
    }
}
