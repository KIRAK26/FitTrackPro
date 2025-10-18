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
    public class IndexModel : PageModel
    {
        private readonly IRecipeService recipeService;

        public IndexModel(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        public List<Recipe> recipes { get; set; } = new List<Recipe>();
        public Dictionary<string, int> statistics { get; set; } = new Dictionary<string, int>();

        [BindProperty(SupportsGet = true)]
        public string? searchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? categoryFilter { get; set; }

        public async Task OnGetAsync()
        {
            // Get statistics
            statistics = await recipeService.getRecipeStatisticsAsync();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(categoryFilter) && categoryFilter != "All")
            {
                if (Enum.TryParse<RecipeCategory>(categoryFilter, out var category))
                {
                    recipes = await recipeService.getRecipesByCategoryAsync(category);
                }
                else
                {
                    recipes = await recipeService.getAllRecipesAsync();
                }
            }
            else if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                recipes = await recipeService.searchRecipesAsync(searchTerm);
            }
            else
            {
                recipes = await recipeService.getAllRecipesAsync();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int recipeId)
        {
            var success = await recipeService.deleteRecipeAsync(recipeId);

            if (success)
            {
                TempData["SuccessMessage"] = "Recipe deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Cannot delete this recipe. It may be in use or is a pre-loaded recipe.";
            }

            return RedirectToPage();
        }
    }
}
