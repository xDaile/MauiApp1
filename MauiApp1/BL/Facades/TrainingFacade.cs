using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MauiApp1.DAL.Repositories.Interfaces;
using MauiApp1.DAL.Entities;
using MauiApp1.BL.Facades.Interfaces;

namespace MauiApp1.BL.Facades
{

    public class TrainingFacade : ITrainingFacade
    {
        private readonly ITrainingRepository trainingRepository;
        private readonly IPauseRepository pauseRepository;
        private readonly IMapper mapper;

        public TrainingFacade(ITrainingRepository trainingRepository, IMapper mapper, IPauseRepository pauseRepository)
        {
            this.trainingRepository = trainingRepository;
            this.pauseRepository = pauseRepository;
            this.mapper = mapper;
        }
        public async Task<int> Create(TrainingModel model)
        {
            return await trainingRepository.Insert(mapper.Map<TrainingEntity>(model));
        }

        public async Task<int> CreateLM(TrainingListModel model)
        {
            var entity = mapper.Map<TrainingEntity>(model);
            return await trainingRepository.Insert(entity);
        }

        public async Task Delete(TrainingModel model)
        {
            //TODO delete training items
            await trainingRepository.Delete(mapper.Map<TrainingEntity>(model));
        }

        public async Task DeleteLM(TrainingListModel model)
        {
            //TODO delete training items
            await trainingRepository.Delete(mapper.Map<TrainingEntity>(model));
            return;
        }

        public async Task<List<TrainingModel>> GetAll()
        {
            return mapper.Map<List<TrainingModel>>(await trainingRepository.GetAll());
        }

        public async Task<List<TrainingListModel>> GetAllLM()
        {
            List<TrainingEntity> entity = await trainingRepository.GetAll();
            return mapper.Map<List<TrainingListModel>>(entity);
        }

        public async Task<List<TrainingListModel>> GetAllLMByPlanId(int trainingPlanId)
        {
            List<TrainingEntity> entity = await trainingRepository.GetByTrainingPlanId(trainingPlanId);
            return mapper.Map<List<TrainingListModel>>(entity);
        }

        public async Task<TrainingModel?> GetById(int id)
        {
            TrainingEntity trainingEntity = await trainingRepository.GetById(id);
            List<ExerciseTrainingEntity> exerciseTrainingEntities = await trainingRepository.GetAllExerciseTrainingByTrainingId(id);
            List<PauseEntity> pauseEntities = await pauseRepository.GetByTrainingId(id);
            List<ExerciseTrainingModel> exerciseTrainingModels = mapper.Map<List<ExerciseTrainingModel>>(exerciseTrainingEntities);
            List<PauseModel> pauseModels = mapper.Map < List<PauseModel>>(pauseEntities);
            List<TrainingItemModel> trainingItems = new List<TrainingItemModel>();
            trainingItems.Concat(pauseModels);
            trainingItems.Concat(trainingItems);
            TrainingModel result = new TrainingModel(trainingEntity.Id, trainingEntity.Name, trainingEntity.Description, trainingEntity.TrainingPlanId, trainingEntity.Order, trainingItems);
            return result;
        }

        public async Task<TrainingListModel?> GetByIdLM(int id)
        {
            var entity = await trainingRepository.GetById(id);
            TrainingListModel model = new TrainingListModel(entity.Id, entity.Name, entity.Description, entity.Order, entity.TrainingPlanId);
            return model;

        }

        public async Task<int?> Update(TrainingModel model)
        {
            return await trainingRepository.Update(mapper.Map<TrainingEntity>(model));
        }

        public async Task<int?> UpdateLM(TrainingListModel model)
        {
            return await trainingRepository.Update(mapper.Map<TrainingEntity>(model));
        }

        public async Task<int> GetExistingTrainingsCount(int trainingPlanId)
        {
            return await trainingRepository.GetExistingTrainingsOfTPCount(trainingPlanId);
        }

        public async Task MoveTrainingUp(int trainingId)
        {
            await trainingRepository.MoveTrainingUp(trainingId);
        }

        public async Task MoveTrainingDown(int trainingId)
        {
            await trainingRepository.MoveTrainingDown(trainingId);
        }

        public async Task MoveTrainingItemUp(TrainingItemModel model)
        {
            if (nameof(model).Equals("ExerciseTrainingModel"))
            {
                Console.WriteLine("ExerciseTraining");
            }
            if (nameof(model).Equals("PauseModel"))
            {
                Console.WriteLine("PauseModel");
            }

           
        }

        public async Task MoveTrainingItemDown(TrainingItemModel model)
        {
            throw new NotImplementedException();
        }

        public async Task CreateTrainingItem(TrainingItemModel model)
        {
            if (model.GetType().Equals(typeof(ExerciseTrainingModel)))
            {
                Console.WriteLine("ExerciseTraining");
            }
            if (model.GetType().Equals(typeof(PauseModel)))
            {
                Console.WriteLine("PauseModel");
            }
            Console.WriteLine(nameof(model));
        }

        public async Task UpdateTrainingItem(TrainingItemModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteTrainingItem(TrainingItemModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetExistingTrainingItemsCount(int trainingPlanId)
        {//TODO
            return 0;
            throw new NotImplementedException();
        }

        /*
        public async Task<int> AddExercise(ExerciseTrainingModel exercise)
        {
            return await trainingRepository.AddExercise(mapper.Map<ExerciseTrainingEntity>(exercise));
        }

        public async Task<int> UpdateExercise(ExerciseTrainingModel exercise)
        {
            return await trainingRepository.UpdateExercise(mapper.Map<ExerciseTrainingEntity>(exercise));
        }

        public void DeleteExercise(ExerciseTrainingModel exercise)
        {
            trainingRepository.DeleteExercise(mapper.Map<ExerciseTrainingEntity>(exercise));
        }
        */
    }
}
