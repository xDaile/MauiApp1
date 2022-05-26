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


        //NOT USABLE - waste of time to write that
        public async Task<List<TrainingModel>> GetAll()
        {
            throw new NotImplementedException();
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
            List<TrainingItemEntity> trainingItemEntities = await trainingRepository.GetAllTrainingItemsByTrainingId(id);
            List<TrainingItemModel> trainingItems = new List<TrainingItemModel>();

            foreach (TrainingItemEntity trainingItemEntity in trainingItemEntities)
            {
                trainingItems.Add(MapTrainingItemEntityToModel(trainingItemEntity));
            }

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
            await trainingRepository.MoveTrainingItemUp(MapTrainingItemModelToEntity(model));
        }

        public async Task MoveTrainingItemDown(TrainingItemModel model)
        {
            await trainingRepository.MoveTrainingItemDown(MapTrainingItemModelToEntity(model));
        }

        public async Task<int> CreateTrainingItem(TrainingItemModel model)
        {
            return await trainingRepository.CreateTrainingItem(MapTrainingItemModelToEntity(model));

        }

        public async Task<int> UpdateTrainingItem(TrainingItemModel model)
        {
            return await trainingRepository.UpdateTrainingItem(MapTrainingItemModelToEntity(model));
        }

        public async Task DeleteTrainingItem(TrainingItemModel model)
        {
            await trainingRepository.DeleteTrainingItem(MapTrainingItemModelToEntity(model));
        }

        public async Task<int> GetExistingTrainingItemsCount(int trainingPlanId)
        {
            return await trainingRepository.GetExistingTrainingItemsCount(trainingPlanId);
        }

        private TrainingItemEntity MapTrainingItemModelToEntity(TrainingItemModel model)
        {
            if (model.GetType().Equals(typeof(ExerciseTrainingModel)))
            {
                return mapper.Map<ExerciseTrainingEntity>(model);
            }
            if (model.GetType().Equals(typeof(PauseModel)))
            {
                return mapper.Map<PauseEntity>(model);
            }
            else throw new Exception("Bad usage of MapTrainingItemModelToEntity method");
        }

        private TrainingItemModel MapTrainingItemEntityToModel(TrainingItemEntity entity)
        {
            if (entity.GetType().Equals(typeof(ExerciseTrainingEntity)))
            {
                return mapper.Map<ExerciseTrainingModel>(entity);
            }
            if (entity.GetType().Equals(typeof(PauseEntity)))
            {
                return mapper.Map<PauseModel>(entity);
            }
            else throw new Exception("Bad usage of MapTrainingItemModelToEntity method");
        }

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

