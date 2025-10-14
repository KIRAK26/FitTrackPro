using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitTrackPro.Models;

namespace FitTrackPro.Services
{
    // Interface for Shopping List service operations (Assignment requirement: Interfaces)
    public interface IShoppingListService
    {
        // Generate shopping list from meal plans for a specific week
        Task<List<ShoppingListItem>> generateShoppingListAsync(DateTime startDate);

        // Save generated shopping list items to database
        Task<bool> saveShoppingListAsync(List<ShoppingListItem> items);

        // Get all shopping list items
        Task<List<ShoppingListItem>> getAllShoppingListItemsAsync();

        // Get shopping list items grouped by category
        Task<Dictionary<IngredientCategory, List<ShoppingListItem>>> getGroupedShoppingListAsync();

        // Toggle checked status of an item
        Task<bool> toggleItemCheckedAsync(int itemId);

        // Get completion percentage of shopping list
        Task<decimal> getCompletionPercentageAsync();

        // Clear all shopping list items
        Task<bool> clearShoppingListAsync();

        // Clear only checked items
        Task<bool> clearCheckedItemsAsync();
    }
}
