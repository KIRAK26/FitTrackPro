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
    public class CreateModel : PageModel
    {
        private readonly IRecipeService recipeService;

        public CreateModel(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        [BindProperty]
        public Recipe recipe { get; set; } = new Recipe();

        [BindProperty]
        public string ingredientsInput { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Parse ingredients from input (format: "quantity|unit|name|category" per line)
            var ingredients = new List<Ingredient>();

            if (!string.IsNullOrWhiteSpace(ingredientsInput))
            {
                var lines = ingredientsInput.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines)
                {
                    var parts = line.Trim().Split('|');
                    if (parts.Length >= 4)
                    {
                        var ingredient = new Ingredient
                        {
                            quantity = parts[0].Trim(),
                            unit = parts[1].Trim(),
                            ingredientName = parts[2].Trim(),
                            category = Enum.TryParse<IngredientCategory>(parts[3].Trim(), out var cat) ? cat : IngredientCategory.Other
                        };
                        ingredients.Add(ingredient);
                    }
                }
            }

            if (!ingredients.Any())
            {
                ModelState.AddModelError("ingredientsInput", "Please add at least one ingredient.");
                return Page();
            }

            await recipeService.createRecipeAsync(recipe, ingredients);

            TempData["SuccessMessage"] = "Recipe created successfully!";
            return RedirectToPage("./Index");
        }
    }
}
