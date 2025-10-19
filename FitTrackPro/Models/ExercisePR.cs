namespace FitTrackPro.Models
{
    public class ExercisePR
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
        public decimal MaxWeight { get; set; }
        public int MaxWeightReps { get; set; }
        public decimal MaxVolume { get; set; }
        public decimal MaxVolumeWeight { get; set; }
        public int MaxVolumeReps { get; set; }

        public ExercisePR() {}

        public ExercisePR(int exerciseId, string exerciseName, decimal maxWeight, int maxWeightReps,
                          decimal maxVolume, decimal maxVolumeWeight, int maxVolumeReps)
        {
            ExerciseId = exerciseId;
            ExerciseName = exerciseName;
            MaxWeight = maxWeight;
            MaxWeightReps = maxWeightReps;
            MaxVolume = maxVolume;
            MaxVolumeWeight = maxVolumeWeight;
            MaxVolumeReps = maxVolumeReps;
        }
    }
}
