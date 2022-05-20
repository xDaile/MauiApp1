using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.DAL.Entities;
using AutoMapper;
using MauiApp1.DAL.Repositories.Interfaces;


namespace MauiApp1.DAL.Repositories
{
    public class TrainingPlanRepository : ITrainingPlanRepository
    {
        private readonly IMapper mapper;
        private readonly IStorage storage;
        public TrainingPlanRepository(IMapper mapper, IStorage storage)
        {
            this.mapper = mapper;
            this.storage = storage;
        }

        public async Task Delete(TrainingPlanEntity trainingPlan)
        {
            await storage.DeleteAsync(trainingPlan);
            //string Query = String.Format("Delete from training_plan where training_plan.id ={0}", trainingPlan.Id);
        }

        public async Task<bool> Exists(TrainingPlanEntity trainingPlan)
        {
            //string Query = String.Format("Select 1 from training_plan where training_plan.id ={0}", trainingPlan.Id);
            throw new NotImplementedException();
        }

        public async Task<List<TrainingPlanEntity>> GetAll()
        {
            return await storage.GetAllAsync<TrainingPlanEntity>();
            //string Query = String.Format("Select * from training_plan");
        }

        public async Task<TrainingPlanEntity?> GetById(int id)
        {
            return await storage.GetByIdAsync<TrainingPlanEntity>(id);
            //string Query = String.Format("Select * from training_plan where training_plan.id = {0}", id);

        }

        public async Task<int> Insert(TrainingPlanEntity trainingPlan)
        {
            return await storage.SetAsync<TrainingPlanEntity>(trainingPlan);
            //string Query = String.Format("Insert into training_plan (name, description) values ('{0}', '{1}')",trainingPlan.Name, trainingPlan.Description);     
        }

        public async Task<int?> Update(TrainingPlanEntity trainingPlan)
        {
            return await storage.SetAsync<TrainingPlanEntity>(trainingPlan);
            //string Query = String.Format("Update training_plan (name, description) values ('{0}','{1}') where training_plan.id={2}",trainingPlan.Name, trainingPlan.Description, trainingPlan.Id);
        }
    }
}
