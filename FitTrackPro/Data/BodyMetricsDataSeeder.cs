using FitTrackPro.Models;
using System;
using System.Linq;

namespace FitTrackPro.Data
{
    public static class BodyMetricsDataSeeder
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.BodyMeasurements.Any())
            {
                return;
            }
            var measurements = new BodyMeasurement[]
            {
                new BodyMeasurement
                {
                    Date = DateTime.Now.AddDays(-60),
                    Weight = 85.5f,
                    BodyFat = 22.5f,
                    MuscleMass = 35.2f,
                    Chest = 98.5f,
                    Arms = 35.0f,
                    Waist = 88.0f,
                    Legs = 58.0f
                },
                new BodyMeasurement
                {
                    Date = DateTime.Now.AddDays(-42),
                    Weight = 84.2f,
                    BodyFat = 21.8f,
                    MuscleMass = 35.8f,
                    Chest = 99.0f,
                    Arms = 35.5f,
                    Waist = 86.5f,
                    Legs = 58.5f
                },
                new BodyMeasurement
                {
                    Date = DateTime.Now.AddDays(-30),
                    Weight = 83.0f,
                    BodyFat = 20.5f,
                    MuscleMass = 36.5f,
                    Chest = 100.0f,
                    Arms = 36.0f,
                    Waist = 85.0f,
                    Legs = 59.0f
                },
                new BodyMeasurement
                {
                    Date = DateTime.Now.AddDays(-14),
                    Weight = 82.5f,
                    BodyFat = 19.8f,
                    MuscleMass = 37.0f,
                    Chest = 100.5f,
                    Arms = 36.5f,
                    Waist = 84.0f,
                    Legs = 59.5f
                },

                new BodyMeasurement
                {
                    Date = DateTime.Now.AddDays(-7),
                    Weight = 82.0f,
                    BodyFat = 19.2f,
                    MuscleMass = 37.5f,
                    Chest = 101.0f,
                    Arms = 37.0f,
                    Waist = 83.5f,
                    Legs = 60.0f
                },

                new BodyMeasurement
                {
                    Date = DateTime.Now.AddDays(-4),
                    Weight = 81.8f,
                    BodyFat = 19.2f,
                    MuscleMass = 37.5f,
                    Chest = 101.0f,
                    Arms = 37.0f,
                    Waist = 83.5f,
                    Legs = 60.0f
                },

                new BodyMeasurement
                {
                    Date = DateTime.Now.AddDays(-1),
                    Weight = 81.5f,
                    BodyFat = 18.5f,
                    MuscleMass = 38.0f,
                    Chest = 101.5f,
                    Arms = 37.5f,
                    Waist = 82.5f,
                    Legs = 60.5f
                }
            };

            foreach (var measurement in measurements)
            {
                context.BodyMeasurements.Add(measurement);
            }
            context.SaveChanges();
        }
    }
}
