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
        public List<IngredientInput> ingredients { get; set; } = new List<IngredientInput>();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ingredients == null || !ingredients.Any())
            {
                ModelState.AddModelError(string.Empty, "Please add at least one ingredient.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            recipe.isCustom = true;
            recipe.createdDate = DateTime.Now;

            var ingredientEntities = ingredients.Select(input => new Ingredient
            {
                quantity = input.quantity,
                unit = input.unit,
                ingredientName = input.ingredientName,
                category = input.category
            }).ToList();

            await recipeService.createRecipeAsync(recipe, ingredientEntities);

            TempData["SuccessMessage"] = "Recipe created successfully!";
            return RedirectToPage("./Index");
        }

        public class IngredientInput
        {
            public string quantity { get; set; } = string.Empty;
            public string unit { get; set; } = string.Empty;
            public string ingredientName { get; set; } = string.Empty;
            public IngredientCategory category { get; set; }
        }
    }
}
