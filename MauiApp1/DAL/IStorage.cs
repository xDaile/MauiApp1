using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.DAL.Entities;
using SQLite;

namespace MauiApp1.DAL
{
    public interface IStorage
    {
        IList<ExerciseEntity> Exercise { get; }
        IList<TrainingEntity> Training { get; }
        IList<PauseEntity> Pause { get; }
        IList<TrainingPlanEntity> TrainingPlan { get; }
        IList<ExerciseTrainingEntity> ExerciseTraining { get; }

       

        Task<CreateTableResult> CreateTableAsync<T>() where T : EntityBase, new();

        Task<List<T>> GetAllAsync<T>() where T : new();

        Task<T> GetByIdAsync<T>(int id) where T : EntityBase, new();

        Task<int> SetAsync<T>(T entity) where T : EntityBase, new();

        Task DeleteAsync<T>(T entity) where T : EntityBase, new();

    }
}
