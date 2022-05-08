using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public record ExerciseTrainingModel(string ExerciseName, int Reps, int Weight, int Order, int Seconds,bool rest_after_last_rep, string description): ModelBase
    {
    }
}
