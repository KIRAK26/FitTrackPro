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
    public class ShoppingListModel : PageModel
    {
        private readonly IShoppingListService shoppingListService;

        public ShoppingListModel(IShoppingListService shoppingListService)
        {
            this.shoppingListService = shoppingListService;
        }

        public Dictionary<IngredientCategory, List<ShoppingListItem>> groupedItems { get; set; } =
            new Dictionary<IngredientCategory, List<ShoppingListItem>>();

        public decimal completionPercentage { get; set; }
        public int totalItems { get; set; }
        public int checkedItems { get; set; }

        public async Task OnGetAsync()
        {
            groupedItems = await shoppingListService.getGroupedShoppingListAsync();
            completionPercentage = await shoppingListService.getCompletionPercentageAsync();

            totalItems = groupedItems.Values.SelectMany(list => list).Count();
            checkedItems = groupedItems.Values.SelectMany(list => list).Count(item => item.isChecked);
        }

        public async Task<IActionResult> OnPostGenerateAsync(DateTime? weekStart)
        {
            var startDate = weekStart ?? DateTime.Today;

            var items = await shoppingListService.generateShoppingListAsync(startDate);

            if (items.Any())
            {
                await shoppingListService.saveShoppingListAsync(items);
                TempData["SuccessMessage"] = $"Shopping list generated with {items.Count} items!";
            }
            else
            {
                TempData["ErrorMessage"] = "No meal plans found for the selected week. Please add meals to your planner first.";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleItemAsync(int itemId)
        {
            await shoppingListService.toggleItemCheckedAsync(itemId);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostClearAllAsync()
        {
            await shoppingListService.clearShoppingListAsync();
            TempData["SuccessMessage"] = "Shopping list cleared!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostClearCheckedAsync()
        {
            await shoppingListService.clearCheckedItemsAsync();
            TempData["SuccessMessage"] = "Checked items removed!";
            return RedirectToPage();
        }
    }
}
