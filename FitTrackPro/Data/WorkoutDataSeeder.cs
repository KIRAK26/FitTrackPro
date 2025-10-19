using FitTrackPro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FitTrackPro.Data
{
    public static class WorkoutDataSeeder
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.WorkoutSessions.Any())
            {
                return; 
            }
            var benchPress = context.Exercises.FirstOrDefault(e => e.Name == "Barbell Bench Press");
            var squat = context.Exercises.FirstOrDefault(e => e.Name == "Squat");
            var deadlift = context.Exercises.FirstOrDefault(e => e.Name == "Deadlift");
            var overheadPress = context.Exercises.FirstOrDefault(e => e.Name == "Overhead Press");
            var pullUp = context.Exercises.FirstOrDefault(e => e.Name == "Pull Up");
            var dumbbellCurl = context.Exercises.FirstOrDefault(e => e.Name == "Dumbbell Curl");

            if (benchPress == null || squat == null || deadlift == null)
            {
                return;
            }

            var pushRoutine = new WorkoutRoutine
            {
                Name = "Push Day",
                Description = "Chest, shoulders, and triceps workout",
                RoutineExercises = new List<RoutineExercise>
                {
                    new RoutineExercise { ExerciseId = benchPress.Id, Sets = 4, Reps = "8-10", RestPeriodSeconds = 90 },
                    new RoutineExercise { ExerciseId = overheadPress.Id, Sets = 3, Reps = "10-12", RestPeriodSeconds = 75 }
                }
            };

            var pullRoutine = new WorkoutRoutine
            {
                Name = "Pull Day",
                Description = "Back and biceps workout",
                RoutineExercises = new List<RoutineExercise>
                {
                    new RoutineExercise { ExerciseId = deadlift.Id, Sets = 3, Reps = "5-8", RestPeriodSeconds = 120 },
                    new RoutineExercise { ExerciseId = pullUp.Id, Sets = 3, Reps = "8-12", RestPeriodSeconds = 90 },
                    new RoutineExercise { ExerciseId = dumbbellCurl.Id, Sets = 3, Reps = "10-15", RestPeriodSeconds = 60 }
                }
            };

            var legRoutine = new WorkoutRoutine
            {
                Name = "Leg Day",
                Description = "Lower body strength training",
                RoutineExercises = new List<RoutineExercise>
                {
                    new RoutineExercise { ExerciseId = squat.Id, Sets = 4, Reps = "8-12", RestPeriodSeconds = 120 }
                }
            };

            context.WorkoutRoutines.AddRange(pushRoutine, pullRoutine, legRoutine);
            context.SaveChanges();

            var session1 = new WorkoutSession
            {
                StartTime = DateTime.Now.AddDays(-10),
                EndTime = DateTime.Now.AddDays(-10).AddMinutes(65),
                WorkoutRoutineId = pushRoutine.Id,
                SessionLogs = new List<SessionLog>
                {
                    new SessionLog { ExerciseId = benchPress.Id, SetNumber = 1, Weight = 80, Reps = 10 },
                    new SessionLog { ExerciseId = benchPress.Id, SetNumber = 2, Weight = 85, Reps = 8 },
                    new SessionLog { ExerciseId = benchPress.Id, SetNumber = 3, Weight = 85, Reps = 8 },
                    new SessionLog { ExerciseId = benchPress.Id, SetNumber = 4, Weight = 80, Reps = 9 },
                    new SessionLog { ExerciseId = overheadPress.Id, SetNumber = 1, Weight = 50, Reps = 12 },
                    new SessionLog { ExerciseId = overheadPress.Id, SetNumber = 2, Weight = 50, Reps = 11 },
                    new SessionLog { ExerciseId = overheadPress.Id, SetNumber = 3, Weight = 50, Reps = 10 }
                }
            };

            var session2 = new WorkoutSession
            {
                StartTime = DateTime.Now.AddDays(-7),
                EndTime = DateTime.Now.AddDays(-7).AddMinutes(70),
                WorkoutRoutineId = pullRoutine.Id,
                SessionLogs = new List<SessionLog>
                {
                    new SessionLog { ExerciseId = deadlift.Id, SetNumber = 1, Weight = 120, Reps = 8 },
                    new SessionLog { ExerciseId = deadlift.Id, SetNumber = 2, Weight = 130, Reps = 6 },
                    new SessionLog { ExerciseId = deadlift.Id, SetNumber = 3, Weight = 130, Reps = 5 },
                    new SessionLog { ExerciseId = pullUp.Id, SetNumber = 1, Weight = 0, Reps = 12 },
                    new SessionLog { ExerciseId = pullUp.Id, SetNumber = 2, Weight = 0, Reps = 10 },
                    new SessionLog { ExerciseId = pullUp.Id, SetNumber = 3, Weight = 0, Reps = 8 },
                    new SessionLog { ExerciseId = dumbbellCurl.Id, SetNumber = 1, Weight = 15, Reps = 15 },
                    new SessionLog { ExerciseId = dumbbellCurl.Id, SetNumber = 2, Weight = 15, Reps = 13 },
                    new SessionLog { ExerciseId = dumbbellCurl.Id, SetNumber = 3, Weight = 15, Reps = 12 }
                }
            };

            var session3 = new WorkoutSession
            {
                StartTime = DateTime.Now.AddDays(-4),
                EndTime = DateTime.Now.AddDays(-4).AddMinutes(55),
                WorkoutRoutineId = legRoutine.Id,
                SessionLogs = new List<SessionLog>
                {
                    new SessionLog { ExerciseId = squat.Id, SetNumber = 1, Weight = 100, Reps = 12 },
                    new SessionLog { ExerciseId = squat.Id, SetNumber = 2, Weight = 110, Reps = 10 },
                    new SessionLog { ExerciseId = squat.Id, SetNumber = 3, Weight = 110, Reps = 9 },
                    new SessionLog { ExerciseId = squat.Id, SetNumber = 4, Weight = 100, Reps = 11 }
                }
            };

            var session4 = new WorkoutSession
            {
                StartTime = DateTime.Now.AddDays(-2),
                EndTime = DateTime.Now.AddDays(-2).AddMinutes(68),
                WorkoutRoutineId = pushRoutine.Id,
                SessionLogs = new List<SessionLog>
                {
                    new SessionLog { ExerciseId = benchPress.Id, SetNumber = 1, Weight = 85, Reps = 10 },
                    new SessionLog { ExerciseId = benchPress.Id, SetNumber = 2, Weight = 90, Reps = 8 },
                    new SessionLog { ExerciseId = benchPress.Id, SetNumber = 3, Weight = 90, Reps = 7 },
                    new SessionLog { ExerciseId = benchPress.Id, SetNumber = 4, Weight = 85, Reps = 9 },
                    new SessionLog { ExerciseId = overheadPress.Id, SetNumber = 1, Weight = 52.5m, Reps = 12 },
                    new SessionLog { ExerciseId = overheadPress.Id, SetNumber = 2, Weight = 52.5m, Reps = 11 },
                    new SessionLog { ExerciseId = overheadPress.Id, SetNumber = 3, Weight = 52.5m, Reps = 10 }
                }
            };
            context.WorkoutSessions.AddRange(session1, session2, session3, session4);
            context.SaveChanges();
        }
    }
}
