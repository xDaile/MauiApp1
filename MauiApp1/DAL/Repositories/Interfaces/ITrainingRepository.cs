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
        Task<List<TrainingEntity>?> GetByTrainingPlanId(int id);
        Task<int> GetExistingTrainingsOfTPCount(int trainingPlanId);

        Task MoveTrainingUp(int trainingId);
        Task MoveTrainingDown(int trainingId);

        Task<List<TrainingItemEntity>> GetAllTrainingItemsByTrainingId(int trainingId);

        Task<int> CreateTrainingItem(TrainingItemEntity entity);
        Task<int> UpdateTrainingItem(TrainingItemEntity entity);
        Task DeleteTrainingItem(TrainingItemEntity entity);
        Task MoveTrainingItemUp(TrainingItemEntity entity);
        Task MoveTrainingItemDown(TrainingItemEntity entity);
        Task<int> GetExistingTrainingItemsCount(int trainingPlanId);
    }
}
