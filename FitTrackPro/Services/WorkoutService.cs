using FitTrackPro.Data;
using FitTrackPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitTrackPro.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly ApplicationDbContext _context;

        public WorkoutService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateWorkoutRoutineAsync(WorkoutRoutine routine)
        {
            _context.WorkoutRoutines.Add(routine);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WorkoutRoutine>> GetAllRoutinesAsync()
        {
            return await _context.WorkoutRoutines.Include(r => r.RoutineExercises).ThenInclude(re => re.Exercise).ToListAsync();
        }

        public async Task<WorkoutRoutine> GetRoutineByIdAsync(int id)
        {
            return await _context.WorkoutRoutines
                .Include(r => r.RoutineExercises)
                .ThenInclude(re => re.Exercise)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddSessionLogAsync(WorkoutSession session)
        {
            _context.WorkoutSessions.Add(session);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WorkoutSession>> GetSessionHistoryAsync()
        {
            return await _context.WorkoutSessions.Include(s => s.SessionLogs).ToListAsync();
        }
    }
}
