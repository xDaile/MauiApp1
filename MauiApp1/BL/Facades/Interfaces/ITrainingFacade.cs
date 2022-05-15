using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace MauiApp1.BL.Facades.Interfaces
{
    public interface ITrainingFacade:IFacade<TrainingModel>
    {
        Task<int> AddExercise(ExerciseTrainingModel exerciseTraining);
        Task<int> UpdateExercise(ExerciseTrainingModel exerciseTraining);
        void DeleteExercise(ExerciseTrainingModel exerciseTraining);
    }
}
