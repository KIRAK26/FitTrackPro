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
        public DbSet<BodyMeasurement> BodyMeasurements { get; set; }
    }
}