// FitTrackPro/Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using FitTrackPro.Models;

namespace FitTrackPro.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }

        //These Database set are for workout feature 
        public DbSet<WorkoutRoutine> WorkoutRoutines { get; set; }
        public DbSet<RoutineExercise> RoutineExercises { get; set; }
        public DbSet<WorkoutSession> WorkoutSessions { get; set; }
        public DbSet<SessionLog> SessionLogs { get; set; }

        // ------------------------------------------------------



    }
}