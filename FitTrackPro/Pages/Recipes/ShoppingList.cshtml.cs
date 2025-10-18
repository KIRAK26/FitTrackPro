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
        public DateTime? dateRangeStart { get; set; }
        public DateTime? dateRangeEnd { get; set; }

        public async Task OnGetAsync()
        {
            groupedItems = await shoppingListService.getGroupedShoppingListAsync();
            completionPercentage = await shoppingListService.getCompletionPercentageAsync();

            totalItems = groupedItems.Values.SelectMany(list => list).Count();
            checkedItems = groupedItems.Values.SelectMany(list => list).Count(item => item.isChecked);

            // Get date range from first item if available
            var firstItem = groupedItems.Values.SelectMany(list => list).FirstOrDefault();
            if (firstItem != null)
            {
                dateRangeStart = firstItem.rangeStartDate;
                dateRangeEnd = firstItem.rangeEndDate;
            }
        }

        public async Task<IActionResult> OnPostGenerateAsync(DateTime? startDate, DateTime? endDate)
        {
            var start = startDate ?? DateTime.Today;
            var end = endDate ?? DateTime.Today.AddDays(6);

            var items = await shoppingListService.generateShoppingListAsync(start, end);

            if (items.Any())
            {
                await shoppingListService.saveShoppingListAsync(items);
                TempData["SuccessMessage"] = $"Shopping list generated with {items.Count} items from {start:MMM dd} to {end:MMM dd}!";
            }
            else
            {
                TempData["ErrorMessage"] = "No meal plans found for the selected date range. Please add meals to your planner first.";
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
