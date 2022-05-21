using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.DAL.Entities;
using AutoMapper;
using MauiApp1.DAL.Repositories.Interfaces;
using SQLite;

namespace MauiApp1.DAL.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly IMapper mapper;
        private readonly IStorage storage;
        public TrainingRepository(IMapper mapper, IStorage storage)
        {
            this.mapper = mapper;
            this.storage = storage;
        }
        public async Task Delete(TrainingEntity training)
        {
            SQLiteAsyncConnection connection = await storage.GetConnection();
            var query = connection.Table<TrainingEntity>().Where(trainingTable => trainingTable.TrainingPlanId.Equals(training.TrainingPlanId));
            List<TrainingEntity> result = await query.ToListAsync();
            await connection.CloseAsync();
            foreach (TrainingEntity trainingForUpdate in result)
            {
                if (trainingForUpdate.Order > training.Order)
                {
                    trainingForUpdate.Order = trainingForUpdate.Order - 1;
                    await storage.SetAsync(trainingForUpdate);
                }
            }

            await storage.DeleteAsync(training);
            //string Query = String.Format("Delete from training where training.id ={0}", training.Id);
            //throw new NotImplementedException();
        }

        public async Task<bool> Exists(TrainingEntity training)
        {
            //string Query = String.Format("Select 1 from training where training.id ={0}", training.Id);
            throw new NotImplementedException();
        }

        public async Task<List<TrainingEntity>> GetAll()
        {
            return await storage.GetAllAsync<TrainingEntity>();
            //string Query = String.Format("Select * from training");
        }

        public async Task<TrainingEntity?> GetById(int id)
        {
            return await storage.GetByIdAsync<TrainingEntity>(id);
            //string Query = String.Format("Select * from training where training.id = {0}", id);
        }

        public async Task<List<TrainingEntity>?> GetByTrainingPlanId(int id)
        {
            //TODO get list of training by trainingPlan id
            SQLiteAsyncConnection connection = await storage.GetConnection();
            var query = connection.Table<TrainingEntity>().Where(training => training.TrainingPlanId.Equals(id));
            List<TrainingEntity> result = await query.ToListAsync();
            await connection.CloseAsync();
            result = result.OrderBy(x=> x.Order).ToList();
            
            return result;
            //string Query = String.Format("Select * from training where training.id = {0}", id);
        }

        public async Task<int> GetExistingTrainingsOfTPCount(int trainingPlanId)
        {
            SQLiteAsyncConnection connection = await storage.GetConnection();
            var query = connection.Table<TrainingEntity>().CountAsync(training => training.TrainingPlanId.Equals(trainingPlanId));
            int result = await query;
            await connection.CloseAsync();
            return result;
        }

        public async Task<int> Insert(TrainingEntity training)
        {
            return await storage.SetAsync(training);
            //string Query = String.Format("Insert into training (name, order, description, training_plan) values ('{0}', {1}, '{2}', {3})", training.Name, training.Order, training.Description, training.TrainingPlanId);

        }

        public async Task<int?> Update(TrainingEntity training)
        {
            return await storage.SetAsync(training);
            // string Query = String.Format("Update training (name, order, description, training_plan) values ('{0}', {1}, '{2}', {3}) where training.id={4}", training.Name, training.Order, training.Description, training.TrainingPlanId, training.Id);
        }

        public async Task<int> AddExercise(ExerciseTrainingEntity exerciseTraining)
        {
            return await storage.SetAsync(exerciseTraining);
        }
        public async Task<int> UpdateExercise(ExerciseTrainingEntity exerciseTraining)
        {
            return await storage.SetAsync(exerciseTraining);
        }
        public async Task DeleteExercise(ExerciseTrainingEntity exerciseTraining)
        {
            await storage.DeleteAsync(exerciseTraining);
        }

        public async Task<List<ExerciseTrainingEntity>> GetAllExerciseTrainingByTrainingId(int trainingId)
        {
            SQLiteAsyncConnection connection = await storage.GetConnection();

            var queryGetTraining = connection.Table<ExerciseTrainingEntity>().Where(exerciseTraining => exerciseTraining.TrainingId.Equals(trainingId));
            List<ExerciseTrainingEntity> exerciseTrainingList = await queryGetTraining.ToListAsync();
            await connection.CloseAsync();
            return exerciseTrainingList;
        }

        public async Task MoveTrainingUp(int trainingId)
        {
            SQLiteAsyncConnection connection = await storage.GetConnection();

            var queryGetTraining = connection.Table<TrainingEntity>().Where(trainingTable => trainingTable.Id.Equals(trainingId));
            TrainingEntity originalTraining = await queryGetTraining.FirstAsync();
            if (originalTraining.Order == 0) return;
            var queryForOtherTrainings = connection.Table<TrainingEntity>().Where(trainingTable => trainingTable.TrainingPlanId.Equals(originalTraining.TrainingPlanId));
            List<TrainingEntity> result = await queryForOtherTrainings.ToListAsync();


            TrainingEntity trainingForUpdate = result.Find(x => x.Order.Equals(originalTraining.Order - 1));

            trainingForUpdate.Order = trainingForUpdate.Order + 1;
            originalTraining.Order = originalTraining.Order - 1;
            await connection.UpdateAsync(trainingForUpdate);
            await connection.UpdateAsync(originalTraining);
            //await storage.SetAsync<TrainingEntity>(trainingForUpdate);
            //await storage.SetAsync<TrainingEntity>(originalTraining);
            await connection.CloseAsync();
            return;
        }

        public async Task MoveTrainingDown(int trainingId)
        {
            SQLiteAsyncConnection connection = null;
            while (connection == null)
            {
                connection = await storage.GetConnection();
            }

            var queryGetTraining = connection.Table<TrainingEntity>().Where(trainingTable => trainingTable.Id.Equals(trainingId));
            TrainingEntity originalTraining = await queryGetTraining.FirstAsync();

            var queryForOtherTrainings = connection.Table<TrainingEntity>().Where(trainingTable => trainingTable.TrainingPlanId.Equals(originalTraining.TrainingPlanId));
            List<TrainingEntity> result = await queryForOtherTrainings.ToListAsync();
            if (originalTraining.Order == result.Count() - 1) return;
            TrainingEntity? trainingForUpdate = result.Find(x => x.Order.Equals(originalTraining.Order + 1));

            if (trainingForUpdate == null) return;

            originalTraining.Order = originalTraining.Order + 1;
            trainingForUpdate.Order = trainingForUpdate.Order - 1;

            await connection.UpdateAsync(trainingForUpdate);
            await connection.UpdateAsync(originalTraining);
            await connection.CloseAsync();
            //await storage.SetAsync<TrainingEntity>(trainingForUpdate);
            //await storage.SetAsync<TrainingEntity>(originalTraining);
            return;
        }
    }
}
