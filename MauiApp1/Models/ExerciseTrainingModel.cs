﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public record ExerciseTrainingModel(ExerciseModel Exercise, int Reps, int Weight, int PauseDuration, int Order):TrainingItemModel
    {
    }
}
