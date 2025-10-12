using FitTrackPro.Data;
using FitTrackPro.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace FitTrackPro.Tests
{
    [TestFixture]
    public class ExerciseTests
    {
        private ApplicationDbContext _context;
        private DbContextOptions<ApplicationDbContext> _options;

        [SetUp]
        public void Setup()
        {
            // Use an in-memory database for testing
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "FitTrackProTestDb")
                .Options;

            _context = new ApplicationDbContext(_options);

            // Ensure the database is clean before each test
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        // --- ADD THIS TEARDOWN METHOD ---
        [TearDown]
        public void TearDown()
        {
            // Dispose the context to release resources after each test
            _context.Dispose();
        }
        // --------------------------------

        [Test]
        public void DataSeeder_Initialize_AddsExercisesToDatabase()
        {
            // Arrange: The database is empty.

            // Act: Run the DataSeeder
            DataSeeder.Initialize(_context);

            // Assert: Check if the exercises were actually added.
            // We use a new context instance for assertion to ensure data was persisted.
            using (var assertContext = new ApplicationDbContext(_options))
            {
                Assert.That(assertContext.Exercises.Count(), Is.EqualTo(10));
                Assert.That(assertContext.Exercises.Any(e => e.Name == "Squat"), Is.True);
            }
        }
    }
}