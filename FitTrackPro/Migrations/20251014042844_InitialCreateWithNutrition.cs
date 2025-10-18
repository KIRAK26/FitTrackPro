using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrackPro.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithNutrition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MuscleGroup = table.Column<string>(type: "TEXT", nullable: false),
                    Difficulty = table.Column<string>(type: "TEXT", nullable: false),
                    Equipment = table.Column<string>(type: "TEXT", nullable: false),
                    Instructions = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mealPlanTemplates",
                columns: table => new
                {
                    templateId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    templateName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    createdDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    timesUsed = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mealPlanTemplates", x => x.templateId);
                });

            migrationBuilder.CreateTable(
                name: "recipes",
                columns: table => new
                {
                    recipeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    recipeName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    category = table.Column<int>(type: "INTEGER", nullable: false),
                    prepTimeMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    servings = table.Column<int>(type: "INTEGER", nullable: false),
                    instructions = table.Column<string>(type: "TEXT", nullable: false),
                    caloriesPerServing = table.Column<int>(type: "INTEGER", nullable: false),
                    proteinGrams = table.Column<decimal>(type: "TEXT", precision: 6, scale: 2, nullable: false),
                    carbsGrams = table.Column<decimal>(type: "TEXT", precision: 6, scale: 2, nullable: false),
                    fatsGrams = table.Column<decimal>(type: "TEXT", precision: 6, scale: 2, nullable: false),
                    isCustom = table.Column<bool>(type: "INTEGER", nullable: false),
                    createdDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipes", x => x.recipeId);
                });

            migrationBuilder.CreateTable(
                name: "shoppingListItems",
                columns: table => new
                {
                    itemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ingredientName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    totalQuantity = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    unit = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    category = table.Column<int>(type: "INTEGER", nullable: false),
                    isChecked = table.Column<bool>(type: "INTEGER", nullable: false),
                    generatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    checkedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingListItems", x => x.itemId);
                });

            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    ingredientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    recipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ingredientName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    quantity = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    unit = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    category = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients", x => x.ingredientId);
                    table.ForeignKey(
                        name: "FK_ingredients_recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "recipes",
                        principalColumn: "recipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mealPlans",
                columns: table => new
                {
                    mealPlanId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    mealType = table.Column<int>(type: "INTEGER", nullable: false),
                    recipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    isCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    createdDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mealPlans", x => x.mealPlanId);
                    table.ForeignKey(
                        name: "FK_mealPlans_recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "recipes",
                        principalColumn: "recipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mealPlanTemplateItems",
                columns: table => new
                {
                    itemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    templateId = table.Column<int>(type: "INTEGER", nullable: false),
                    dayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
                    mealType = table.Column<int>(type: "INTEGER", nullable: false),
                    recipeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mealPlanTemplateItems", x => x.itemId);
                    table.ForeignKey(
                        name: "FK_mealPlanTemplateItems_mealPlanTemplates_templateId",
                        column: x => x.templateId,
                        principalTable: "mealPlanTemplates",
                        principalColumn: "templateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mealPlanTemplateItems_recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "recipes",
                        principalColumn: "recipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ingredients_recipeId",
                table: "ingredients",
                column: "recipeId");

            migrationBuilder.CreateIndex(
                name: "IX_mealPlans_date",
                table: "mealPlans",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "IX_mealPlans_recipeId",
                table: "mealPlans",
                column: "recipeId");

            migrationBuilder.CreateIndex(
                name: "IX_mealPlanTemplateItems_recipeId",
                table: "mealPlanTemplateItems",
                column: "recipeId");

            migrationBuilder.CreateIndex(
                name: "IX_mealPlanTemplateItems_templateId",
                table: "mealPlanTemplateItems",
                column: "templateId");

            migrationBuilder.CreateIndex(
                name: "IX_shoppingListItems_category",
                table: "shoppingListItems",
                column: "category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "ingredients");

            migrationBuilder.DropTable(
                name: "mealPlans");

            migrationBuilder.DropTable(
                name: "mealPlanTemplateItems");

            migrationBuilder.DropTable(
                name: "shoppingListItems");

            migrationBuilder.DropTable(
                name: "mealPlanTemplates");

            migrationBuilder.DropTable(
                name: "recipes");
        }
    }
}
