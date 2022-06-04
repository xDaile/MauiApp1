using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public record ExerciseTrainingModel(int? Id, TimeSpan RestSeconds, TimeSpan ExerciseSeconds, int Reps, int Weight, int Sets, int Order, bool RestAfterLastSet, string Description, int ExerciseId, int TrainingId, string ExerciseName) : TrainingItemModel(Order)
    {
    }
}
