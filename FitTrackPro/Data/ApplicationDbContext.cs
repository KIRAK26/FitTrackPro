// FitTrackPro/Data/ApplicationDbContext.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitTrackPro.Models;

namespace FitTrackPro.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Workout feature DbSets (existing)
        public DbSet<Exercise> Exercises { get; set; }

        // Nutrition feature DbSets (new)
        public DbSet<Recipe> recipes { get; set; }
        public DbSet<Ingredient> ingredients { get; set; }
        public DbSet<MealPlan> mealPlans { get; set; }
        public DbSet<MealPlanTemplate> mealPlanTemplates { get; set; }
        public DbSet<MealPlanTemplateItem> mealPlanTemplateItems { get; set; }
        public DbSet<ShoppingListItem> shoppingListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Recipe entity with Fluent API
            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(r => r.recipeId);

                entity.Property(r => r.proteinGrams)
                    .HasPrecision(6, 2);

                entity.Property(r => r.carbsGrams)
                    .HasPrecision(6, 2);

                entity.Property(r => r.fatsGrams)
                    .HasPrecision(6, 2);

                // One-to-many relationship: Recipe -> Ingredients (Cascade delete)
                entity.HasMany(r => r.ingredients)
                    .WithOne(i => i.recipe)
                    .HasForeignKey(i => i.recipeId)
                    .OnDelete(DeleteBehavior.Cascade);

                // One-to-many relationship: Recipe -> MealPlans (Restrict delete)
                entity.HasMany(r => r.mealPlans)
                    .WithOne(mp => mp.recipe)
                    .HasForeignKey(mp => mp.recipeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Ingredient entity
            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasKey(i => i.ingredientId);

                entity.Property(i => i.ingredientName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            // Configure MealPlan entity
            modelBuilder.Entity<MealPlan>(entity =>
            {
                entity.HasKey(mp => mp.mealPlanId);

                entity.Property(mp => mp.date)
                    .IsRequired();

                entity.Property(mp => mp.mealType)
                    .IsRequired();

                // Index for faster queries by date
                entity.HasIndex(mp => mp.date);
            });

            // Configure MealPlanTemplate entity
            modelBuilder.Entity<MealPlanTemplate>(entity =>
            {
                entity.HasKey(t => t.templateId);

                entity.Property(t => t.templateName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(t => t.description)
                    .HasMaxLength(500)
                    .IsRequired(false);

                // One-to-many relationship: Template -> TemplateItems (Cascade delete)
                entity.HasMany(t => t.templateItems)
                    .WithOne(ti => ti.template)
                    .HasForeignKey(ti => ti.templateId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure MealPlanTemplateItem entity
            modelBuilder.Entity<MealPlanTemplateItem>(entity =>
            {
                entity.HasKey(ti => ti.itemId);

                // Relationship with Recipe (Restrict delete)
                entity.HasOne(ti => ti.recipe)
                    .WithMany()
                    .HasForeignKey(ti => ti.recipeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure ShoppingListItem entity
            modelBuilder.Entity<ShoppingListItem>(entity =>
            {
                entity.HasKey(s => s.itemId);

                entity.Property(s => s.ingredientName)
                    .IsRequired()
                    .HasMaxLength(200);

                // Index for faster queries by category
                entity.HasIndex(s => s.category);
            });
        }
    }
}