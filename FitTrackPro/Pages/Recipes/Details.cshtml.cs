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
    public class DetailsModel : PageModel
    {
        private readonly IRecipeService recipeService;

        public DetailsModel(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        public Recipe recipe { get; set; } = null!;
        public Dictionary<string, decimal> macroPercentages { get; set; } = new Dictionary<string, decimal>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var foundRecipe = await recipeService.getRecipeByIdAsync(id);

            if (foundRecipe == null)
            {
                return NotFound();
            }

            recipe = foundRecipe;
            macroPercentages = recipeService.calculateMacroPercentages(recipe);

            return Page();
        }
    }
}
