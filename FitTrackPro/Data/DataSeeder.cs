using FitTrackPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace FitTrackPro.Data
{
    public static class DataSeeder
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if there are any exercises already
            if (context.Exercises.Any())
            {
                return; // DB has been seeded
            }

            var exercises = new Exercise[]
           {
               new Exercise { Name = "Barbell Bench Press", MuscleGroup = "Chest", Difficulty = "Intermediate", Equipment = "Barbell", Instructions = "Lie on a flat bench...", CaloriesBurnedPerRep = 5.7m },
             new Exercise { Name = "Squat", MuscleGroup = "Legs", Difficulty = "Intermediate", Equipment = "Barbell", Instructions = "Stand with your feet shoulder-width apart...", CaloriesBurnedPerRep = 6.3m },
             new Exercise { Name = "Deadlift", MuscleGroup = "Back", Difficulty = "Advanced", Equipment = "Barbell", Instructions = "Stand with your mid-foot under the barbell...", CaloriesBurnedPerRep = 6.8m },
             new Exercise { Name = "Overhead Press", MuscleGroup = "Shoulders", Difficulty = "Intermediate", Equipment = "Barbell", Instructions = "Stand with the bar on your front shoulders...", CaloriesBurnedPerRep = 5.1m },
             new Exercise { Name = "Pull Up", MuscleGroup = "Back", Difficulty = "Intermediate", Equipment = "Pull-up Bar", Instructions = "Grab the bar with a grip slightly wider than shoulder-width...", CaloriesBurnedPerRep = 5.7m },
             new Exercise { Name = "Dumbbell Curl", MuscleGroup = "Biceps", Difficulty = "Beginner", Equipment = "Dumbbell", Instructions = "Stand or sit holding a dumbbell in each hand...", CaloriesBurnedPerRep = 3.4m },
             new Exercise { Name = "Tricep Pushdown", MuscleGroup = "Triceps", Difficulty = "Beginner", Equipment = "Cable Machine", Instructions = "Attach a straight bar to a high pulley...", CaloriesBurnedPerRep = 3.4m },
             new Exercise { Name = "Leg Press", MuscleGroup = "Legs", Difficulty = "Beginner", Equipment = "Leg Press Machine", Instructions = "Sit on the machine with your back and head resting against the padded support...", CaloriesBurnedPerRep = 5.1m },
             new Exercise { Name = "Plank", MuscleGroup = "Core", Difficulty = "Beginner", Equipment = "Bodyweight", Instructions = "...", CaloriesBurnedPerMinute = 5.0m },
             new Exercise { Name = "Running", MuscleGroup = "Cardio", Difficulty = "Beginner", Equipment = "None", Instructions = "...", CaloriesBurnedPerMinute = 12.0m }

                };

            foreach (Exercise e in exercises)
            {
                context.Exercises.Add(e);
            }
            context.SaveChanges();
        }
    }
}
