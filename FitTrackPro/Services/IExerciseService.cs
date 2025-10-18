using FitTrackPro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitTrackPro.Services
{
    public interface IExerciseService
    {
        Task<List<Exercise>> GetAllExercisesAsync();
        Task<List<Exercise>> GetAllExercisesAsync(string searchTerm);
        Task AddExerciseAsync(Exercise exercise);

        // --- ADDED METHODS ---

        /// <summary>
        /// Gets a list of exercises based on multiple filter criteria.
        /// </summary>
        Task<List<Exercise>> GetExercisesFilteredAsync(string name, string muscleGroup, string difficulty, string equipment);

        /// <summary>
        /// Gets a unique list of all muscle groups.
        /// </summary>
        Task<List<string>> GetUniqueMuscleGroupsAsync();

        /// <summary>
        /// Gets a unique list of all difficulties.
        /// </summary>
        Task<List<string>> GetUniqueDifficultiesAsync();

        /// <summary>
        /// Gets a unique list of all equipment.
        /// </summary>
        Task<List<string>> GetUniqueEquipmentAsync();
    }
}