using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitTrackPro.Data;
using FitTrackPro.Services; // Kept single using directive
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure Database Context
builder.Services.AddDbContext<FitTrackPro.Data.ApplicationDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Your (HEAD) services
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();

// Your teammate's (main) services
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IMealPlanService, MealPlanService>();
builder.Services.AddScoped<IShoppingListService, ShoppingListService>();

var app = builder.Build();


// Seed the database (Combined)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>(); // Get logger for error handling
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Ensure database is created (from main)
        context.Database.EnsureCreated();

        // Run your (HEAD) seeder
        DataSeeder.Initialize(context);

        // Run your teammate's (main) seeder
        await RecipeDataSeeder.seedRecipesAsync(context);

        logger.LogInformation("Database seeding completed successfully!"); // Use logger
    }
    catch (Exception ex)
    {
        // Log errors from either seeder
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error!");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();