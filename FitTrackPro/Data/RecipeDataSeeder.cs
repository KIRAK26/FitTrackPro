using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitTrackPro.Models;
using Microsoft.EntityFrameworkCore;

namespace FitTrackPro.Data
{
    public static class RecipeDataSeeder
    {
        public static async Task seedRecipesAsync(ApplicationDbContext context)
        {
            // Check if recipes already exist (prevent duplicate seeding)
            if (await context.recipes.AnyAsync())
            {
                Console.WriteLine("Recipes already seeded. Skipping...");
                return;
            }

            Console.WriteLine("Seeding 55 recipes...");

            var recipes = new List<Recipe>();

            // =============== BREAKFAST RECIPES (15) ===============
            recipes.Add(new Recipe
            {
                recipeName = "Protein Pancakes",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 15,
                servings = 2,
                instructions = "1. Mix protein powder, oats, egg, and milk. 2. Cook on griddle until golden. 3. Serve with berries.",
                caloriesPerServing = 320,
                proteinGrams = 25,
                carbsGrams = 35,
                fatsGrams = 8,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Protein Powder", quantity = "1", unit = "scoop", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Oats", quantity = "1/2", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Egg", quantity = "2", unit = "whole", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Milk", quantity = "1/4", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Blueberries", quantity = "1/2", unit = "cup", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Greek Yogurt Parfait",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 5,
                servings = 1,
                instructions = "1. Layer Greek yogurt in a bowl. 2. Add granola and mixed berries. 3. Drizzle with honey.",
                caloriesPerServing = 280,
                proteinGrams = 20,
                carbsGrams = 35,
                fatsGrams = 6,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Greek Yogurt", quantity = "1", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Granola", quantity = "1/4", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Mixed Berries", quantity = "1/2", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Honey", quantity = "1", unit = "tbsp", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Avocado Toast with Eggs",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 10,
                servings = 1,
                instructions = "1. Toast whole grain bread. 2. Mash avocado with lime and salt. 3. Top with poached eggs.",
                caloriesPerServing = 380,
                proteinGrams = 18,
                carbsGrams = 30,
                fatsGrams = 22,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Whole Grain Bread", quantity = "2", unit = "slices", category = IngredientCategory.Bakery },
                    new Ingredient { ingredientName = "Avocado", quantity = "1", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Eggs", quantity = "2", unit = "whole", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Lime", quantity = "1", unit = "wedge", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Overnight Oats",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 5,
                servings = 1,
                instructions = "1. Mix oats, milk, chia seeds, and vanilla. 2. Refrigerate overnight. 3. Top with banana and nuts.",
                caloriesPerServing = 350,
                proteinGrams = 12,
                carbsGrams = 55,
                fatsGrams = 10,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Rolled Oats", quantity = "1/2", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Almond Milk", quantity = "3/4", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Chia Seeds", quantity = "1", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Banana", quantity = "1", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Almonds", quantity = "1/4", unit = "cup", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Spinach and Feta Omelet",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 12,
                servings = 1,
                instructions = "1. Whisk eggs with milk. 2. Cook in pan with spinach. 3. Add feta cheese and fold.",
                caloriesPerServing = 290,
                proteinGrams = 22,
                carbsGrams = 5,
                fatsGrams = 20,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Eggs", quantity = "3", unit = "whole", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Spinach", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Feta Cheese", quantity = "1/4", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Milk", quantity = "2", unit = "tbsp", category = IngredientCategory.Dairy }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Banana Protein Smoothie",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 5,
                servings = 1,
                instructions = "1. Blend banana, protein powder, spinach, and almond milk. 2. Add ice and blend until smooth.",
                caloriesPerServing = 310,
                proteinGrams = 28,
                carbsGrams = 40,
                fatsGrams = 5,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Banana", quantity = "1", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Protein Powder", quantity = "1", unit = "scoop", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Spinach", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Almond Milk", quantity = "1", unit = "cup", category = IngredientCategory.Dairy }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Whole Wheat French Toast",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 15,
                servings = 2,
                instructions = "1. Dip bread in egg mixture. 2. Cook until golden on both sides. 3. Serve with maple syrup.",
                caloriesPerServing = 340,
                proteinGrams = 14,
                carbsGrams = 48,
                fatsGrams = 10,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Whole Wheat Bread", quantity = "4", unit = "slices", category = IngredientCategory.Bakery },
                    new Ingredient { ingredientName = "Eggs", quantity = "2", unit = "whole", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Milk", quantity = "1/4", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Cinnamon", quantity = "1", unit = "tsp", category = IngredientCategory.Spices },
                    new Ingredient { ingredientName = "Maple Syrup", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Breakfast Burrito",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 20,
                servings = 1,
                instructions = "1. Scramble eggs with peppers and onions. 2. Add black beans and cheese. 3. Wrap in tortilla.",
                caloriesPerServing = 420,
                proteinGrams = 24,
                carbsGrams = 45,
                fatsGrams = 16,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Eggs", quantity = "2", unit = "whole", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Whole Wheat Tortilla", quantity = "1", unit = "large", category = IngredientCategory.Bakery },
                    new Ingredient { ingredientName = "Black Beans", quantity = "1/2", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Bell Pepper", quantity = "1/2", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Cheddar Cheese", quantity = "1/4", unit = "cup", category = IngredientCategory.Dairy }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Chia Pudding",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 5,
                servings = 1,
                instructions = "1. Mix chia seeds with coconut milk and vanilla. 2. Refrigerate 4 hours. 3. Top with mango.",
                caloriesPerServing = 270,
                proteinGrams = 8,
                carbsGrams = 30,
                fatsGrams = 14,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Chia Seeds", quantity = "3", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Coconut Milk", quantity = "1", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Vanilla Extract", quantity = "1", unit = "tsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Mango", quantity = "1/2", unit = "cup", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Veggie Breakfast Scramble",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 15,
                servings = 2,
                instructions = "1. Sauté vegetables in olive oil. 2. Add beaten eggs. 3. Scramble until cooked through.",
                caloriesPerServing = 240,
                proteinGrams = 16,
                carbsGrams = 12,
                fatsGrams = 15,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Eggs", quantity = "4", unit = "whole", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Tomatoes", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Mushrooms", quantity = "1/2", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Spinach", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Olive Oil", quantity = "1", unit = "tbsp", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Peanut Butter Banana Wrap",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 5,
                servings = 1,
                instructions = "1. Spread peanut butter on tortilla. 2. Add sliced banana. 3. Roll up and slice.",
                caloriesPerServing = 380,
                proteinGrams = 14,
                carbsGrams = 48,
                fatsGrams = 16,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Whole Wheat Tortilla", quantity = "1", unit = "large", category = IngredientCategory.Bakery },
                    new Ingredient { ingredientName = "Peanut Butter", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Banana", quantity = "1", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Honey", quantity = "1", unit = "tsp", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Cottage Cheese Bowl",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 5,
                servings = 1,
                instructions = "1. Place cottage cheese in bowl. 2. Top with pineapple and walnuts. 3. Drizzle with honey.",
                caloriesPerServing = 320,
                proteinGrams = 26,
                carbsGrams = 28,
                fatsGrams = 12,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Cottage Cheese", quantity = "1", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Pineapple", quantity = "1/2", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Walnuts", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Honey", quantity = "1", unit = "tbsp", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Berry Smoothie Bowl",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 10,
                servings = 1,
                instructions = "1. Blend frozen berries with yogurt. 2. Pour into bowl. 3. Top with granola and coconut.",
                caloriesPerServing = 360,
                proteinGrams = 15,
                carbsGrams = 58,
                fatsGrams = 9,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Mixed Berries", quantity = "1", unit = "cup", category = IngredientCategory.Frozen },
                    new Ingredient { ingredientName = "Greek Yogurt", quantity = "1/2", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Granola", quantity = "1/4", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Coconut Flakes", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Egg White Veggie Muffins",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 25,
                servings = 6,
                instructions = "1. Mix egg whites with vegetables. 2. Pour into muffin tins. 3. Bake at 350°F for 20 minutes.",
                caloriesPerServing = 90,
                proteinGrams = 12,
                carbsGrams = 4,
                fatsGrams = 2,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Egg Whites", quantity = "8", unit = "whole", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Bell Peppers", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Onion", quantity = "1/2", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Spinach", quantity = "1", unit = "cup", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Quinoa Breakfast Bowl",
                category = RecipeCategory.Breakfast,
                prepTimeMinutes = 15,
                servings = 2,
                instructions = "1. Cook quinoa in almond milk. 2. Top with berries and nuts. 3. Drizzle with maple syrup.",
                caloriesPerServing = 340,
                proteinGrams = 12,
                carbsGrams = 54,
                fatsGrams = 10,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Quinoa", quantity = "1/2", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Almond Milk", quantity = "1", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Blueberries", quantity = "1/2", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Pecans", quantity = "1/4", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Maple Syrup", quantity = "1", unit = "tbsp", category = IngredientCategory.Pantry }
                }
            });

            // =============== LUNCH RECIPES (15) ===============
            recipes.Add(new Recipe
            {
                recipeName = "Grilled Chicken Caesar Salad",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 20,
                servings = 1,
                instructions = "1. Grill chicken breast. 2. Toss romaine with Caesar dressing. 3. Top with chicken and parmesan.",
                caloriesPerServing = 420,
                proteinGrams = 38,
                carbsGrams = 18,
                fatsGrams = 22,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Chicken Breast", quantity = "6", unit = "oz", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Romaine Lettuce", quantity = "3", unit = "cups", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Caesar Dressing", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Parmesan Cheese", quantity = "2", unit = "tbsp", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Croutons", quantity = "1/4", unit = "cup", category = IngredientCategory.Bakery }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Turkey and Avocado Wrap",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 10,
                servings = 1,
                instructions = "1. Layer turkey on tortilla. 2. Add avocado, lettuce, and tomato. 3. Roll tightly and slice.",
                caloriesPerServing = 380,
                proteinGrams = 28,
                carbsGrams = 35,
                fatsGrams = 14,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Whole Wheat Tortilla", quantity = "1", unit = "large", category = IngredientCategory.Bakery },
                    new Ingredient { ingredientName = "Turkey Breast", quantity = "4", unit = "oz", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Avocado", quantity = "1/2", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Lettuce", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Tomato", quantity = "1", unit = "whole", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Quinoa Buddha Bowl",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 25,
                servings = 2,
                instructions = "1. Cook quinoa. 2. Roast vegetables. 3. Arrange in bowl with chickpeas and tahini.",
                caloriesPerServing = 450,
                proteinGrams = 18,
                carbsGrams = 62,
                fatsGrams = 16,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Quinoa", quantity = "1", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Chickpeas", quantity = "1", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Sweet Potato", quantity = "1", unit = "medium", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Kale", quantity = "2", unit = "cups", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Tahini", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Tuna Salad Sandwich",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 10,
                servings = 1,
                instructions = "1. Mix tuna with mayo and celery. 2. Spread on bread. 3. Add lettuce and tomato.",
                caloriesPerServing = 360,
                proteinGrams = 30,
                carbsGrams = 32,
                fatsGrams = 12,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Canned Tuna", quantity = "5", unit = "oz", category = IngredientCategory.Seafood },
                    new Ingredient { ingredientName = "Whole Grain Bread", quantity = "2", unit = "slices", category = IngredientCategory.Bakery },
                    new Ingredient { ingredientName = "Mayonnaise", quantity = "1", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Celery", quantity = "1/4", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Lettuce", quantity = "2", unit = "leaves", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Mediterranean Pasta Salad",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 20,
                servings = 4,
                instructions = "1. Cook pasta and cool. 2. Mix with vegetables and feta. 3. Toss with olive oil dressing.",
                caloriesPerServing = 380,
                proteinGrams = 12,
                carbsGrams = 48,
                fatsGrams = 16,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Whole Wheat Pasta", quantity = "2", unit = "cups", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Cherry Tomatoes", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Cucumber", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Feta Cheese", quantity = "1/2", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Olives", quantity = "1/2", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Olive Oil", quantity = "3", unit = "tbsp", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Chicken Pesto Panini",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 15,
                servings = 1,
                instructions = "1. Spread pesto on bread. 2. Add grilled chicken and mozzarella. 3. Press in panini grill.",
                caloriesPerServing = 480,
                proteinGrams = 36,
                carbsGrams = 38,
                fatsGrams = 20,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Ciabatta Bread", quantity = "1", unit = "roll", category = IngredientCategory.Bakery },
                    new Ingredient { ingredientName = "Chicken Breast", quantity = "4", unit = "oz", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Pesto", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Mozzarella", quantity = "2", unit = "oz", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Tomato", quantity = "2", unit = "slices", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Black Bean Burrito Bowl",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 20,
                servings = 2,
                instructions = "1. Cook rice. 2. Season black beans. 3. Layer rice, beans, veggies, and salsa in bowl.",
                caloriesPerServing = 420,
                proteinGrams = 16,
                carbsGrams = 68,
                fatsGrams = 10,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Brown Rice", quantity = "1", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Black Beans", quantity = "1", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Corn", quantity = "1/2", unit = "cup", category = IngredientCategory.Frozen },
                    new Ingredient { ingredientName = "Bell Peppers", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Salsa", quantity = "1/2", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Avocado", quantity = "1", unit = "whole", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Caprese Salad with Balsamic",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 10,
                servings = 1,
                instructions = "1. Slice tomatoes and mozzarella. 2. Layer with basil leaves. 3. Drizzle with balsamic glaze.",
                caloriesPerServing = 320,
                proteinGrams = 18,
                carbsGrams = 12,
                fatsGrams = 24,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Tomatoes", quantity = "2", unit = "large", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Fresh Mozzarella", quantity = "4", unit = "oz", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Fresh Basil", quantity = "1/4", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Balsamic Glaze", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Olive Oil", quantity = "1", unit = "tbsp", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Asian Chicken Lettuce Wraps",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 20,
                servings = 2,
                instructions = "1. Sauté ground chicken with ginger and garlic. 2. Add hoisin sauce. 3. Serve in lettuce cups.",
                caloriesPerServing = 280,
                proteinGrams = 32,
                carbsGrams = 18,
                fatsGrams = 10,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Ground Chicken", quantity = "8", unit = "oz", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Butter Lettuce", quantity = "8", unit = "leaves", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Hoisin Sauce", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Ginger", quantity = "1", unit = "tbsp", category = IngredientCategory.Spices },
                    new Ingredient { ingredientName = "Water Chestnuts", quantity = "1/2", unit = "cup", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Lentil Vegetable Soup",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 35,
                servings = 6,
                instructions = "1. Sauté onions and carrots. 2. Add lentils and broth. 3. Simmer 30 minutes until tender.",
                caloriesPerServing = 220,
                proteinGrams = 12,
                carbsGrams = 38,
                fatsGrams = 2,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Lentils", quantity = "1", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Carrots", quantity = "2", unit = "cups", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Celery", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Onion", quantity = "1", unit = "medium", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Vegetable Broth", quantity = "6", unit = "cups", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Tomatoes", quantity = "1", unit = "can", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Shrimp and Avocado Salad",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 15,
                servings = 2,
                instructions = "1. Cook shrimp with garlic. 2. Toss with greens and avocado. 3. Dress with lime vinaigrette.",
                caloriesPerServing = 320,
                proteinGrams = 28,
                carbsGrams = 14,
                fatsGrams = 18,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Shrimp", quantity = "8", unit = "oz", category = IngredientCategory.Seafood },
                    new Ingredient { ingredientName = "Mixed Greens", quantity = "4", unit = "cups", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Avocado", quantity = "1", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Cherry Tomatoes", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Lime", quantity = "1", unit = "whole", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Veggie Hummus Wrap",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 10,
                servings = 1,
                instructions = "1. Spread hummus on tortilla. 2. Layer with vegetables. 3. Roll tightly and slice.",
                caloriesPerServing = 340,
                proteinGrams = 12,
                carbsGrams = 48,
                fatsGrams = 12,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Whole Wheat Tortilla", quantity = "1", unit = "large", category = IngredientCategory.Bakery },
                    new Ingredient { ingredientName = "Hummus", quantity = "3", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Cucumber", quantity = "1/2", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Bell Peppers", quantity = "1/2", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Shredded Carrots", quantity = "1/4", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Spinach", quantity = "1", unit = "cup", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Chicken Noodle Soup",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 30,
                servings = 6,
                instructions = "1. Boil chicken in broth. 2. Shred chicken and return to pot. 3. Add vegetables and noodles.",
                caloriesPerServing = 260,
                proteinGrams = 24,
                carbsGrams = 28,
                fatsGrams = 6,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Chicken Breast", quantity = "1", unit = "lb", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Egg Noodles", quantity = "2", unit = "cups", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Carrots", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Celery", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Chicken Broth", quantity = "8", unit = "cups", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Greek Chicken Pita",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 20,
                servings = 2,
                instructions = "1. Grill seasoned chicken. 2. Stuff pita with chicken, tzatziki, and vegetables.",
                caloriesPerServing = 420,
                proteinGrams = 34,
                carbsGrams = 42,
                fatsGrams = 12,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Chicken Breast", quantity = "8", unit = "oz", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Pita Bread", quantity = "2", unit = "whole", category = IngredientCategory.Bakery },
                    new Ingredient { ingredientName = "Tzatziki", quantity = "1/4", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Cucumber", quantity = "1/2", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Tomato", quantity = "1/2", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Red Onion", quantity = "1/4", unit = "cup", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Egg Salad Lettuce Boats",
                category = RecipeCategory.Lunch,
                prepTimeMinutes = 15,
                servings = 2,
                instructions = "1. Chop hard-boiled eggs. 2. Mix with mayo and mustard. 3. Serve in lettuce leaves.",
                caloriesPerServing = 240,
                proteinGrams = 16,
                carbsGrams = 4,
                fatsGrams = 18,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Hard-Boiled Eggs", quantity = "6", unit = "whole", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Mayonnaise", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Dijon Mustard", quantity = "1", unit = "tsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Romaine Lettuce", quantity = "6", unit = "leaves", category = IngredientCategory.Produce }
                }
            });

            // =============== DINNER RECIPES (15) ===============
            recipes.Add(new Recipe
            {
                recipeName = "Baked Salmon with Asparagus",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 25,
                servings = 2,
                instructions = "1. Season salmon with lemon and herbs. 2. Bake at 400°F with asparagus. 3. Serve with quinoa.",
                caloriesPerServing = 480,
                proteinGrams = 42,
                carbsGrams = 28,
                fatsGrams = 22,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Salmon Fillet", quantity = "8", unit = "oz", category = IngredientCategory.Seafood },
                    new Ingredient { ingredientName = "Asparagus", quantity = "1", unit = "bunch", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Lemon", quantity = "1", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Olive Oil", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Quinoa", quantity = "1", unit = "cup", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Chicken Stir-Fry",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 20,
                servings = 4,
                instructions = "1. Stir-fry chicken in wok. 2. Add vegetables and sauce. 3. Serve over brown rice.",
                caloriesPerServing = 420,
                proteinGrams = 36,
                carbsGrams = 45,
                fatsGrams = 10,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Chicken Breast", quantity = "1", unit = "lb", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Broccoli", quantity = "2", unit = "cups", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Bell Peppers", quantity = "2", unit = "cups", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Soy Sauce", quantity = "3", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Brown Rice", quantity = "2", unit = "cups", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Ginger", quantity = "1", unit = "tbsp", category = IngredientCategory.Spices }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Spaghetti with Turkey Meatballs",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 35,
                servings = 6,
                instructions = "1. Form turkey meatballs and bake. 2. Cook spaghetti. 3. Simmer in marinara sauce.",
                caloriesPerServing = 460,
                proteinGrams = 32,
                carbsGrams = 56,
                fatsGrams = 12,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Ground Turkey", quantity = "1", unit = "lb", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Whole Wheat Spaghetti", quantity = "1", unit = "lb", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Marinara Sauce", quantity = "3", unit = "cups", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Parmesan Cheese", quantity = "1/2", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Italian Seasoning", quantity = "2", unit = "tsp", category = IngredientCategory.Spices }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Grilled Chicken Fajitas",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 25,
                servings = 4,
                instructions = "1. Marinate chicken in lime and spices. 2. Grill with peppers and onions. 3. Serve in tortillas.",
                caloriesPerServing = 440,
                proteinGrams = 38,
                carbsGrams = 42,
                fatsGrams = 12,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Chicken Breast", quantity = "1", unit = "lb", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Bell Peppers", quantity = "3", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Onion", quantity = "1", unit = "large", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Tortillas", quantity = "8", unit = "small", category = IngredientCategory.Bakery },
                    new Ingredient { ingredientName = "Lime", quantity = "2", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Fajita Seasoning", quantity = "2", unit = "tbsp", category = IngredientCategory.Spices }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Beef and Broccoli",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 25,
                servings = 4,
                instructions = "1. Sear beef strips. 2. Stir-fry with broccoli and sauce. 3. Serve over rice.",
                caloriesPerServing = 480,
                proteinGrams = 36,
                carbsGrams = 42,
                fatsGrams = 18,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Beef Sirloin", quantity = "1", unit = "lb", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Broccoli", quantity = "4", unit = "cups", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Soy Sauce", quantity = "1/4", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Brown Rice", quantity = "2", unit = "cups", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Garlic", quantity = "3", unit = "cloves", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Vegetable Curry with Chickpeas",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 30,
                servings = 6,
                instructions = "1. Sauté onions and curry paste. 2. Add vegetables and coconut milk. 3. Simmer with chickpeas.",
                caloriesPerServing = 380,
                proteinGrams = 14,
                carbsGrams = 52,
                fatsGrams = 14,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Chickpeas", quantity = "2", unit = "cans", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Coconut Milk", quantity = "1", unit = "can", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Cauliflower", quantity = "2", unit = "cups", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Sweet Potato", quantity = "2", unit = "medium", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Curry Paste", quantity = "3", unit = "tbsp", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Spinach", quantity = "2", unit = "cups", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Baked Chicken Parmesan",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 40,
                servings = 4,
                instructions = "1. Bread chicken with parmesan. 2. Bake until crispy. 3. Top with marinara and mozzarella.",
                caloriesPerServing = 520,
                proteinGrams = 46,
                carbsGrams = 32,
                fatsGrams = 22,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Chicken Breast", quantity = "1.5", unit = "lbs", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Breadcrumbs", quantity = "1", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Parmesan Cheese", quantity = "1/2", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Marinara Sauce", quantity = "2", unit = "cups", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Mozzarella", quantity = "1", unit = "cup", category = IngredientCategory.Dairy }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Shrimp Scampi with Zoodles",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 20,
                servings = 2,
                instructions = "1. Sauté shrimp in garlic butter. 2. Add white wine and lemon. 3. Toss with zucchini noodles.",
                caloriesPerServing = 340,
                proteinGrams = 32,
                carbsGrams = 16,
                fatsGrams = 18,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Shrimp", quantity = "12", unit = "oz", category = IngredientCategory.Seafood },
                    new Ingredient { ingredientName = "Zucchini", quantity = "4", unit = "medium", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Butter", quantity = "2", unit = "tbsp", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Garlic", quantity = "4", unit = "cloves", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "White Wine", quantity = "1/4", unit = "cup", category = IngredientCategory.Beverages },
                    new Ingredient { ingredientName = "Lemon", quantity = "1", unit = "whole", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Turkey Chili",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 40,
                servings = 8,
                instructions = "1. Brown turkey with onions. 2. Add beans, tomatoes, and spices. 3. Simmer 30 minutes.",
                caloriesPerServing = 320,
                proteinGrams = 28,
                carbsGrams = 38,
                fatsGrams = 6,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Ground Turkey", quantity = "2", unit = "lbs", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Kidney Beans", quantity = "2", unit = "cans", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Diced Tomatoes", quantity = "2", unit = "cans", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Onion", quantity = "1", unit = "large", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Chili Powder", quantity = "3", unit = "tbsp", category = IngredientCategory.Spices }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Baked Cod with Vegetables",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 30,
                servings = 4,
                instructions = "1. Season cod fillets. 2. Arrange with vegetables on sheet pan. 3. Bake at 400°F for 20 minutes.",
                caloriesPerServing = 320,
                proteinGrams = 38,
                carbsGrams = 22,
                fatsGrams = 8,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Cod Fillets", quantity = "1.5", unit = "lbs", category = IngredientCategory.Seafood },
                    new Ingredient { ingredientName = "Cherry Tomatoes", quantity = "2", unit = "cups", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Zucchini", quantity = "2", unit = "medium", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Red Onion", quantity = "1", unit = "medium", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Olive Oil", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Chicken Teriyaki Bowl",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 25,
                servings = 4,
                instructions = "1. Cook chicken in teriyaki sauce. 2. Serve over rice with steamed broccoli and carrots.",
                caloriesPerServing = 460,
                proteinGrams = 38,
                carbsGrams = 58,
                fatsGrams = 8,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Chicken Thighs", quantity = "1.5", unit = "lbs", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Teriyaki Sauce", quantity = "1/2", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "White Rice", quantity = "2", unit = "cups", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Broccoli", quantity = "2", unit = "cups", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Carrots", quantity = "1", unit = "cup", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Stuffed Bell Peppers",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 45,
                servings = 6,
                instructions = "1. Hollow out peppers. 2. Fill with ground beef, rice, and tomato mixture. 3. Bake 35 minutes.",
                caloriesPerServing = 380,
                proteinGrams = 26,
                carbsGrams = 42,
                fatsGrams = 12,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Bell Peppers", quantity = "6", unit = "large", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Ground Beef", quantity = "1", unit = "lb", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Brown Rice", quantity = "1", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Tomato Sauce", quantity = "1", unit = "can", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Mozzarella", quantity = "1", unit = "cup", category = IngredientCategory.Dairy }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Lemon Herb Roasted Chicken",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 60,
                servings = 6,
                instructions = "1. Season whole chicken with herbs and lemon. 2. Roast at 375°F for 1 hour. 3. Rest before carving.",
                caloriesPerServing = 420,
                proteinGrams = 46,
                carbsGrams = 2,
                fatsGrams = 24,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Whole Chicken", quantity = "1", unit = "4-5 lb", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Lemon", quantity = "2", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Rosemary", quantity = "3", unit = "sprigs", category = IngredientCategory.Spices },
                    new Ingredient { ingredientName = "Thyme", quantity = "3", unit = "sprigs", category = IngredientCategory.Spices },
                    new Ingredient { ingredientName = "Olive Oil", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Vegetarian Tacos",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 20,
                servings = 4,
                instructions = "1. Season black beans and corn. 2. Warm tortillas. 3. Fill with beans, veggies, and toppings.",
                caloriesPerServing = 360,
                proteinGrams = 14,
                carbsGrams = 58,
                fatsGrams = 10,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Black Beans", quantity = "2", unit = "cans", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Corn Tortillas", quantity = "12", unit = "small", category = IngredientCategory.Bakery },
                    new Ingredient { ingredientName = "Corn", quantity = "1", unit = "cup", category = IngredientCategory.Frozen },
                    new Ingredient { ingredientName = "Avocado", quantity = "2", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Lettuce", quantity = "2", unit = "cups", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Salsa", quantity = "1", unit = "cup", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Pork Tenderloin with Apples",
                category = RecipeCategory.Dinner,
                prepTimeMinutes = 35,
                servings = 6,
                instructions = "1. Sear pork tenderloin. 2. Roast with sliced apples and onions. 3. Serve with roasted vegetables.",
                caloriesPerServing = 380,
                proteinGrams = 42,
                carbsGrams = 24,
                fatsGrams = 12,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Pork Tenderloin", quantity = "2", unit = "lbs", category = IngredientCategory.Meat },
                    new Ingredient { ingredientName = "Apples", quantity = "3", unit = "medium", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Onion", quantity = "1", unit = "large", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Brussels Sprouts", quantity = "2", unit = "cups", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Olive Oil", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry }
                }
            });

            // =============== SNACK RECIPES (10) ===============
            recipes.Add(new Recipe
            {
                recipeName = "Protein Energy Balls",
                category = RecipeCategory.Snack,
                prepTimeMinutes = 15,
                servings = 12,
                instructions = "1. Mix oats, protein powder, peanut butter, and honey. 2. Roll into balls. 3. Refrigerate 1 hour.",
                caloriesPerServing = 120,
                proteinGrams = 6,
                carbsGrams = 14,
                fatsGrams = 5,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Oats", quantity = "1", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Protein Powder", quantity = "1/2", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Peanut Butter", quantity = "1/2", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Honey", quantity = "1/4", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Chocolate Chips", quantity = "1/4", unit = "cup", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Trail Mix",
                category = RecipeCategory.Snack,
                prepTimeMinutes = 5,
                servings = 8,
                instructions = "1. Combine all nuts, seeds, and dried fruit. 2. Store in airtight container.",
                caloriesPerServing = 180,
                proteinGrams = 6,
                carbsGrams = 16,
                fatsGrams = 12,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Almonds", quantity = "1", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Cashews", quantity = "1", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Raisins", quantity = "1/2", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Pumpkin Seeds", quantity = "1/2", unit = "cup", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Apple Slices with Almond Butter",
                category = RecipeCategory.Snack,
                prepTimeMinutes = 5,
                servings = 1,
                instructions = "1. Slice apple into wedges. 2. Serve with almond butter for dipping.",
                caloriesPerServing = 220,
                proteinGrams = 6,
                carbsGrams = 28,
                fatsGrams = 10,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Apple", quantity = "1", unit = "large", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Almond Butter", quantity = "2", unit = "tbsp", category = IngredientCategory.Pantry }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Greek Yogurt with Berries",
                category = RecipeCategory.Snack,
                prepTimeMinutes = 3,
                servings = 1,
                instructions = "1. Place Greek yogurt in bowl. 2. Top with mixed berries.",
                caloriesPerServing = 150,
                proteinGrams = 15,
                carbsGrams = 20,
                fatsGrams = 2,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Greek Yogurt", quantity = "1", unit = "cup", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Mixed Berries", quantity = "1/2", unit = "cup", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Hummus with Veggies",
                category = RecipeCategory.Snack,
                prepTimeMinutes = 5,
                servings = 1,
                instructions = "1. Slice vegetables into sticks. 2. Serve with hummus for dipping.",
                caloriesPerServing = 160,
                proteinGrams = 6,
                carbsGrams = 18,
                fatsGrams = 8,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Hummus", quantity = "1/4", unit = "cup", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Carrots", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Celery", quantity = "1", unit = "cup", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Bell Peppers", quantity = "1/2", unit = "cup", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Protein Smoothie",
                category = RecipeCategory.Snack,
                prepTimeMinutes = 5,
                servings = 1,
                instructions = "1. Blend protein powder, banana, berries, and milk. 2. Add ice and blend until smooth.",
                caloriesPerServing = 280,
                proteinGrams = 25,
                carbsGrams = 35,
                fatsGrams = 4,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Protein Powder", quantity = "1", unit = "scoop", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Banana", quantity = "1", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Berries", quantity = "1/2", unit = "cup", category = IngredientCategory.Frozen },
                    new Ingredient { ingredientName = "Almond Milk", quantity = "1", unit = "cup", category = IngredientCategory.Dairy }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Rice Cakes with Avocado",
                category = RecipeCategory.Snack,
                prepTimeMinutes = 5,
                servings = 1,
                instructions = "1. Mash avocado with lime. 2. Spread on rice cakes. 3. Season with salt and pepper.",
                caloriesPerServing = 200,
                proteinGrams = 4,
                carbsGrams = 22,
                fatsGrams = 12,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Rice Cakes", quantity = "2", unit = "whole", category = IngredientCategory.Pantry },
                    new Ingredient { ingredientName = "Avocado", quantity = "1/2", unit = "whole", category = IngredientCategory.Produce },
                    new Ingredient { ingredientName = "Lime", quantity = "1", unit = "wedge", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Hard-Boiled Eggs",
                category = RecipeCategory.Snack,
                prepTimeMinutes = 12,
                servings = 2,
                instructions = "1. Boil eggs for 10 minutes. 2. Cool in ice water. 3. Peel and season with salt.",
                caloriesPerServing = 140,
                proteinGrams = 12,
                carbsGrams = 2,
                fatsGrams = 10,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Eggs", quantity = "4", unit = "whole", category = IngredientCategory.Dairy }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "String Cheese and Grapes",
                category = RecipeCategory.Snack,
                prepTimeMinutes = 2,
                servings = 1,
                instructions = "1. Pair string cheese with grapes for quick snack.",
                caloriesPerServing = 160,
                proteinGrams = 8,
                carbsGrams = 18,
                fatsGrams = 6,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "String Cheese", quantity = "2", unit = "sticks", category = IngredientCategory.Dairy },
                    new Ingredient { ingredientName = "Grapes", quantity = "1", unit = "cup", category = IngredientCategory.Produce }
                }
            });

            recipes.Add(new Recipe
            {
                recipeName = "Edamame Beans",
                category = RecipeCategory.Snack,
                prepTimeMinutes = 8,
                servings = 2,
                instructions = "1. Boil edamame for 5 minutes. 2. Drain and season with sea salt.",
                caloriesPerServing = 120,
                proteinGrams = 11,
                carbsGrams = 10,
                fatsGrams = 5,
                isCustom = false,
                ingredients = new List<Ingredient>
                {
                    new Ingredient { ingredientName = "Edamame", quantity = "2", unit = "cups", category = IngredientCategory.Frozen },
                    new Ingredient { ingredientName = "Sea Salt", quantity = "1", unit = "tsp", category = IngredientCategory.Spices }
                }
            });

            // Add all recipes to database
            await context.recipes.AddRangeAsync(recipes);
            await context.SaveChangesAsync();

            Console.WriteLine($"Successfully seeded {recipes.Count} recipes with ingredients!");
        }
    }
}
