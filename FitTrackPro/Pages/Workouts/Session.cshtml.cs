using FitTrackPro.Models;
using FitTrackPro.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitTrackPro.Pages.Workouts
{
    public class SessionModel : PageModel
    {

        private readonly IWorkoutService _workoutService;

        public SessionModel(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        public WorkoutRoutine CurrentRoutine { get; set; }

        [BindProperty]
        public List<SessionLog> Logs { get; set; }

        [BindProperty]
        public int RoutineId { get; set; }

        public async Task<IActionResult> OnGetAsync(int routineId)
        {
            RoutineId = routineId;
            CurrentRoutine = await _workoutService.GetRoutineByIdAsync(routineId);

            if (CurrentRoutine == null)
            {
                return NotFound();
            }

            // Pre-populate the Logs list for the form
            Logs = new List<SessionLog>();
            foreach (var routineExercise in CurrentRoutine.RoutineExercises)
            {
                for (int i = 1; i <= routineExercise.Sets; i++)
                {
                    Logs.Add(new SessionLog
                    {
                        ExerciseId = routineExercise.ExerciseId,
                        SetNumber = i
                    });
                }
            }

            return Page();
        }




        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // This part should not be hit anymore for empty inputs, but is good practice to keep
                CurrentRoutine = await _workoutService.GetRoutineByIdAsync(RoutineId);
                return Page();
            }

            CurrentRoutine = await _workoutService.GetRoutineByIdAsync(RoutineId);
            decimal totalCalories = 0;
            var validLogs = new List<SessionLog>();

            foreach (var log in Logs)
            {
                
                if (log.Weight.HasValue || log.Reps.HasValue)
                {
                    validLogs.Add(log);

                    
                    var exercise = CurrentRoutine.RoutineExercises
                        .Select(re => re.Exercise)
                        .FirstOrDefault(ex => ex.Id == log.ExerciseId);

                    if (exercise != null && log.Reps.HasValue)
                    {
                        
                        if (exercise.CaloriesBurnedPerRep.HasValue)
                        {
                            
                            totalCalories += (log.Reps.Value * exercise.CaloriesBurnedPerRep.Value);
                        }

                        
                    }
                    
                }
            }


            var session = new WorkoutSession
            {
                WorkoutRoutineId = RoutineId,
                StartTime = Logs.Any() ? DateTime.UtcNow : DateTime.UtcNow, // For simplicity, we set EndTime to now
                // Filter out logs where the user didn't input any data                                                            
                SessionLogs = Logs.Where(log => log.Weight.HasValue || log.Reps.HasValue).ToList(),
                TotalCaloriesBurned = totalCalories
            };

            // Only save the session if there is at least one valid log entry
            if (session.SessionLogs.Any())
            {

                session.StartTime = DateTime.UtcNow; 
                session.EndTime = DateTime.UtcNow;   
                await _workoutService.AddSessionLogAsync(session);
            }

            return RedirectToPage("./History");
        }
    }
}