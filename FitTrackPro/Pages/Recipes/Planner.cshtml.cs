using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitTrackPro.Models;
using FitTrackPro.Services;

namespace FitTrackPro.Pages.Recipes
{
    public class PlannerModel : PageModel
    {
        private readonly IMealPlanService mealPlanService;
        private readonly IRecipeService recipeService;

        public PlannerModel(IMealPlanService mealPlanService, IRecipeService recipeService)
        {
            this.mealPlanService = mealPlanService;
            this.recipeService = recipeService;
        }

        public List<MealPlan> weekMealPlans { get; set; } = new List<MealPlan>();
        public List<Recipe> allRecipes { get; set; } = new List<Recipe>();
        public List<MealPlanTemplate> templates { get; set; } = new List<MealPlanTemplate>();
        public Dictionary<string, decimal> weeklyNutrition { get; set; } = new Dictionary<string, decimal>();
        public DateTime currentWeekStart { get; set; }

        [BindProperty]
        public DateTime selectedDate { get; set; }

        [BindProperty]
        public int selectedMealType { get; set; }

        [BindProperty]
        public int selectedRecipeId { get; set; }

        [BindProperty]
        public DateTime sourceDate { get; set; }

        [BindProperty]
        public DateTime targetDate { get; set; }

        [BindProperty]
        public int templateId { get; set; }

        [BindProperty]
        public string templateName { get; set; } = string.Empty;

        [BindProperty]
        public string templateDescription { get; set; } = string.Empty;

        public async Task OnGetAsync(DateTime? weekStart)
        {
            // Calculate Monday of current or specified week
            var baseDate = weekStart ?? DateTime.Today;
            currentWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek + (int)DayOfWeek.Monday);

            // Load data
            weekMealPlans = await mealPlanService.getMealPlansForWeekAsync(currentWeekStart);
            allRecipes = await recipeService.getAllRecipesAsync();
            templates = await mealPlanService.getAllTemplatesAsync();
            weeklyNutrition = await mealPlanService.calculateWeeklyNutritionAsync(currentWeekStart);
        }

        public async Task<IActionResult> OnPostAssignRecipeAsync()
        {
            var mealType = (MealType)selectedMealType;
            await mealPlanService.assignRecipeToSlotAsync(selectedDate, mealType, selectedRecipeId);

            TempData["SuccessMessage"] = "Recipe assigned successfully!";
            return RedirectToPage(new { weekStart = selectedDate });
        }

        public async Task<IActionResult> OnPostCopyDayAsync()
        {
            var success = await mealPlanService.copyDayAsync(sourceDate, targetDate);

            if (success)
            {
                TempData["SuccessMessage"] = $"Meals copied from {sourceDate:MMM dd} to {targetDate:MMM dd}!";
            }
            else
            {
                TempData["ErrorMessage"] = "No meals found on source date to copy.";
            }

            return RedirectToPage(new { weekStart = sourceDate });
        }

        public async Task<IActionResult> OnPostClearWeekAsync(DateTime weekStart)
        {
            var success = await mealPlanService.clearWeekAsync(weekStart);

            if (success)
            {
                TempData["SuccessMessage"] = "Week cleared successfully!";
            }

            return RedirectToPage(new { weekStart });
        }

        public async Task<IActionResult> OnPostSaveTemplateAsync(DateTime weekStart)
        {
            if (string.IsNullOrWhiteSpace(templateName))
            {
                TempData["ErrorMessage"] = "Template name is required.";
                return RedirectToPage(new { weekStart });
            }

            await mealPlanService.saveWeekAsTemplateAsync(weekStart, templateName, templateDescription);

            TempData["SuccessMessage"] = $"Template '{templateName}' saved successfully!";
            return RedirectToPage(new { weekStart });
        }

        public async Task<IActionResult> OnPostLoadTemplateAsync(DateTime weekStart)
        {
            var success = await mealPlanService.loadTemplateAsync(templateId, weekStart);

            if (success)
            {
                TempData["SuccessMessage"] = "Template loaded successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to load template.";
            }

            return RedirectToPage(new { weekStart });
        }

        public async Task<IActionResult> OnPostRemoveMealAsync(int mealPlanId, DateTime weekStart)
        {
            await mealPlanService.removeMealPlanAsync(mealPlanId);

            TempData["SuccessMessage"] = "Meal removed from plan!";
            return RedirectToPage(new { weekStart });
        }

        // Helper method to get meal plan for specific slot
        public MealPlan? getMealPlanForSlot(DateTime date, MealType mealType)
        {
            return weekMealPlans.FirstOrDefault(mp => mp.date.Date == date.Date && mp.mealType == mealType);
        }
    }
}
