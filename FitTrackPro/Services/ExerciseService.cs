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

        // --- ADDED METHODS IMPLEMENTATION ---

        public async Task<List<Exercise>> GetExercisesFilteredAsync(string name, string muscleGroup, string difficulty, string equipment)
        {
            // Start with a base query
            var query = _context.Exercises.AsQueryable();

            // Apply name filter if provided
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(e => e.Name.ToLower().Contains(name.ToLower()));
            }

            // Apply muscle group filter if provided
            if (!string.IsNullOrWhiteSpace(muscleGroup))
            {
                query = query.Where(e => e.MuscleGroup == muscleGroup);
            }

            // Apply difficulty filter if provided
            if (!string.IsNullOrWhiteSpace(difficulty))
            {
                query = query.Where(e => e.Difficulty == difficulty);
            }

            // Apply equipment filter if provided
            if (!string.IsNullOrWhiteSpace(equipment))
            {
                query = query.Where(e => e.Equipment == equipment);
            }

            // Execute the query and return the results
            return await query.ToListAsync();
        }

        public async Task<List<string>> GetUniqueMuscleGroupsAsync()
        {
            return await _context.Exercises
                .Select(e => e.MuscleGroup)
                .Distinct()
                .OrderBy(m => m)
                .ToListAsync();
        }

        public async Task<List<string>> GetUniqueDifficultiesAsync()
        {
            return await _context.Exercises
                .Select(e => e.Difficulty)
                .Distinct()
                .OrderBy(d => d)
                .ToListAsync();
        }

        public async Task<List<string>> GetUniqueEquipmentAsync()
        {
            return await _context.Exercises
                .Select(e => e.Equipment)
                .Distinct()
                .OrderBy(eq => eq)
                .ToListAsync();
        }
    }
}