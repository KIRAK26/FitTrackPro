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
    }
}
