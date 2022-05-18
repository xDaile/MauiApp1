using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace MauiApp1.BL.Facades.Interfaces
{
    public interface ITrainingPlanFacade:IFacade<TrainingPlanModel>
    {
        Task<List<TrainingPlanListModel>> GetAllLM();
        Task<TrainingPlanListModel> GetByIdLM(int id);
        Task<int> CreateLM(TrainingPlanListModel model);
        Task<int?> UpdateLM(TrainingPlanListModel model);
        void DeleteLM(TrainingPlanListModel model);



    }
}
