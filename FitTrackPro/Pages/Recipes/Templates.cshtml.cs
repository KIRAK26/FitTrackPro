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
    public class TemplatesModel : PageModel
    {
        private readonly IMealPlanService mealPlanService;

        public TemplatesModel(IMealPlanService mealPlanService)
        {
            this.mealPlanService = mealPlanService;
        }

        public List<MealPlanTemplate> templates { get; set; } = new List<MealPlanTemplate>();

        public async Task OnGetAsync()
        {
            templates = await mealPlanService.getAllTemplatesAsync();
        }

        public async Task<IActionResult> OnPostDeleteTemplateAsync(int templateId)
        {
            var success = await mealPlanService.deleteTemplateAsync(templateId);

            if (success)
            {
                TempData["SuccessMessage"] = "Template deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete template.";
            }

            return RedirectToPage();
        }
    }
}
