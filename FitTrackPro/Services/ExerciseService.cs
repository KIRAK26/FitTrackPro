using FitTrackPro.Data;
using FitTrackPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace FitTrackPro.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly ApplicationDbContext _context;

        public ExerciseService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<Exercise>> GetAllExercisesAsync()
        {
            return await _context.Exercises.ToListAsync();
        }


        public async Task<List<Exercise>> GetAllExercisesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // If search is empty, just call the other version of this method
                return await GetAllExercisesAsync();
            }

            return await _context.Exercises
                .Where(e => e.Name.ToLower().Contains(searchTerm.ToLower()))
                .ToListAsync();
        }

        public async Task AddExerciseAsync(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
        }
    }
}