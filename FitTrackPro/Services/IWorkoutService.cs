using FitTrackPro.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitTrackPro.Services
{
    public interface IWorkoutService
    {
        Task CreateWorkoutRoutineAsync(WorkoutRoutine routine);
        Task<List<WorkoutRoutine>> GetAllRoutinesAsync();
        Task<WorkoutRoutine> GetRoutineByIdAsync(int id);
        Task AddSessionLogAsync(WorkoutSession session);
        Task<List<WorkoutSession>> GetSessionHistoryAsync();
    }
}
