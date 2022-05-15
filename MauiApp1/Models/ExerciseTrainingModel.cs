using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public record ExerciseTrainingModel(int? Id, int Seconds, int Reps, int Weight, int Sets, int Order, bool RestAfterLastSet, string Description, string ExerciseName) : ModelBase
    {
    }
}
