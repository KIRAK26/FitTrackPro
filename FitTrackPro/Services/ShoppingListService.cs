using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitTrackPro.Data;
using FitTrackPro.Models;
using Microsoft.EntityFrameworkCore;

namespace FitTrackPro.Services
{
    // Implementation of IShoppingListService (Demonstrates: Interface implementation, Complex LINQ)
    public class ShoppingListService : IShoppingListService
    {
        private readonly ApplicationDbContext context;

        public ShoppingListService(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Generate shopping list from meal plans for a specific week (LINQ: SelectMany, GroupBy)
        public async Task<List<ShoppingListItem>> generateShoppingListAsync(DateTime startDate)
        {
            DateTime weekStart = startDate.Date.AddDays(-(int)startDate.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime weekEnd = weekStart.AddDays(7);

            // Get all meal plans for the week with recipes and ingredients
            var weekPlans = await context.mealPlans
                .Include(mp => mp.recipe)
                .ThenInclude(r => r.ingredients)
                .Where(mp => mp.date >= weekStart && mp.date < weekEnd)
                .ToListAsync();

            if (!weekPlans.Any())
                return new List<ShoppingListItem>();

            // Extract all ingredients from recipes (LINQ: SelectMany to flatten nested collections)
            var allIngredients = weekPlans
                .SelectMany(mp => mp.recipe.ingredients)
                .ToList();

            // Group ingredients by name and category, then aggregate quantities
            var groupedIngredients = allIngredients
                .GroupBy(i => new { i.ingredientName, i.unit, i.category })
                .Select(g => new ShoppingListItem
                {
                    ingredientName = g.Key.ingredientName,
                    unit = g.Key.unit,
                    category = g.Key.category,
                    totalQuantity = aggregateQuantities(g.Select(i => i.quantity).ToList()),
                    isChecked = false,
                    generatedDate = DateTime.UtcNow
                })
                .OrderBy(item => item.category)
                .ThenBy(item => item.ingredientName)
                .ToList();

            return groupedIngredients;
        }

        // Helper method to aggregate quantities (handles simple numeric values)
        private string aggregateQuantities(List<string> quantities)
        {
            // Try to parse and sum numeric quantities
            decimal total = 0;
            bool allNumeric = true;

            foreach (var qty in quantities)
            {
                // Try to parse fraction strings like "1/2", "1/4"
                if (qty.Contains("/"))
                {
                    var parts = qty.Split('/');
                    if (parts.Length == 2 &&
                        decimal.TryParse(parts[0], out decimal numerator) &&
                        decimal.TryParse(parts[1], out decimal denominator) &&
                        denominator != 0)
                    {
                        total += numerator / denominator;
                        continue;
                    }
                }

                // Try to parse regular numbers
                if (decimal.TryParse(qty, out decimal value))
                {
                    total += value;
                }
                else
                {
                    allNumeric = false;
                    break;
                }
            }

            if (allNumeric && total > 0)
            {
                // Format as fraction if needed or decimal
                return total % 1 == 0 ? total.ToString("F0") : total.ToString("F2");
            }

            // If not all numeric, just show count
            return quantities.Count.ToString();
        }

        // Save generated shopping list items to database
        public async Task<bool> saveShoppingListAsync(List<ShoppingListItem> items)
        {
            if (!items.Any())
                return false;

            // Clear existing shopping list first
            await clearShoppingListAsync();

            context.shoppingListItems.AddRange(items);
            await context.SaveChangesAsync();

            return true;
        }

        // Get all shopping list items (LINQ: OrderBy)
        public async Task<List<ShoppingListItem>> getAllShoppingListItemsAsync()
        {
            return await context.shoppingListItems
                .OrderBy(item => item.category)
                .ThenBy(item => item.ingredientName)
                .ToListAsync();
        }

        // Get shopping list items grouped by category (LINQ: GroupBy, ToDictionaryAsync)
        public async Task<Dictionary<IngredientCategory, List<ShoppingListItem>>> getGroupedShoppingListAsync()
        {
            var items = await getAllShoppingListItemsAsync();

            return items
                .GroupBy(item => item.category)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Toggle checked status of an item
        public async Task<bool> toggleItemCheckedAsync(int itemId)
        {
            var item = await context.shoppingListItems.FindAsync(itemId);
            if (item == null)
                return false;

            item.isChecked = !item.isChecked;
            item.checkedDate = item.isChecked ? DateTime.UtcNow : null;

            context.shoppingListItems.Update(item);
            await context.SaveChangesAsync();

            return true;
        }

        // Get completion percentage of shopping list (LINQ: Count, Where)
        public async Task<decimal> getCompletionPercentageAsync()
        {
            var totalItems = await context.shoppingListItems.CountAsync();
            if (totalItems == 0)
                return 0;

            var checkedItems = await context.shoppingListItems
                .Where(item => item.isChecked)
                .CountAsync();

            return Math.Round((decimal)checkedItems / totalItems * 100, 1);
        }

        // Clear all shopping list items
        public async Task<bool> clearShoppingListAsync()
        {
            var items = await context.shoppingListItems.ToListAsync();
            if (!items.Any())
                return false;

            context.shoppingListItems.RemoveRange(items);
            await context.SaveChangesAsync();

            return true;
        }

        // Clear only checked items (LINQ: Where, RemoveRange)
        public async Task<bool> clearCheckedItemsAsync()
        {
            var checkedItems = await context.shoppingListItems
                .Where(item => item.isChecked)
                .ToListAsync();

            if (!checkedItems.Any())
                return false;

            context.shoppingListItems.RemoveRange(checkedItems);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
