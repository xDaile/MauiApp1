using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.DAL.Entities;

namespace MauiApp1.DAL.Repositories.Interfaces
{
    public interface ITrainingRepository : IRepository<TrainingEntity>
    {
        Task<int> AddExercise(ExerciseTrainingEntity exerciseTraining);
        Task<int> UpdateExercise(ExerciseTrainingEntity exerciseTraining);
        void DeleteExercise(ExerciseTrainingEntity exerciseTraining);

    }
}
