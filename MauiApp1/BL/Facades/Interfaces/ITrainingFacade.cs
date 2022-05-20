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
       /* Task<int> AddExercise(ExerciseTrainingModel exerciseTraining);
        Task<int> UpdateExercise(ExerciseTrainingModel exerciseTraining);
        void DeleteExercise(ExerciseTrainingModel exerciseTraining);
       */
        Task<List<TrainingListModel>> GetAllLM();
        Task<TrainingListModel> GetByIdLM(int id);
        Task<int> CreateLM(TrainingListModel model);
        Task<int?> UpdateLM(TrainingListModel model);
        Task DeleteLM(TrainingListModel model);
        Task<int> GetExistingTrainingsCount(int trainingPlanId);
        Task<List<TrainingListModel>> GetAllLMByPlanId(int trainingPlanId);

        Task MoveTrainingUp(int trainingId);
        Task MoveTrainingDown(int trainingId);
    }
}
