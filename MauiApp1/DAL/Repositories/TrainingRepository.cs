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
    public class TrainingRepository : ITrainingRepository
    {
        private readonly IMapper mapper;
        private readonly IStorage storage;
        public TrainingRepository(IMapper mapper, IStorage storage)
        {
            this.mapper = mapper;
            this.storage = storage;
        }
        public async void Delete(TrainingEntity training)
        {
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

        public async Task<int> Insert(TrainingEntity training)
        {
            return await storage.SetAsync(training);
            //string Query = String.Format("Insert into training (name, order, description, training_plan) values ('{0}', {1}, '{2}', {3})", training.Name, training.Order, training.Description, training.TraininPlangId);

        }

        public async Task<int?> Update(TrainingEntity training)
        {
            return await storage.SetAsync(training);
            // string Query = String.Format("Update training (name, order, description, training_plan) values ('{0}', {1}, '{2}', {3}) where training.id={4}", training.Name, training.Order, training.Description, training.TraininPlangId, training.Id);
        }

        public async Task<int> AddExercise(ExerciseTrainingEntity exerciseTraining)
        {
            return await storage.SetAsync(exerciseTraining);
        }
        public async Task<int> UpdateExercise(ExerciseTrainingEntity exerciseTraining)
        {
            return await storage.SetAsync(exerciseTraining);
        }
        public async void DeleteExercise(ExerciseTrainingEntity exerciseTraining)
        {
            await storage.DeleteAsync(exerciseTraining);
        }
    }
}
