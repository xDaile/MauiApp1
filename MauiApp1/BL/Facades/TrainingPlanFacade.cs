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
    public class TrainingPlanFacade : ITrainingPlanFacade
    {
        private readonly ITrainingPlanRepository trainingPlanRepository;
        private readonly ITrainingRepository trainingRepository;
        private readonly IMapper mapper;

        public TrainingPlanFacade(ITrainingPlanRepository trainingPlanRepository, IMapper mapper, ITrainingRepository trainingRepository)
        {
            this.trainingPlanRepository = trainingPlanRepository;
            this.mapper = mapper;
            this.trainingRepository = trainingRepository;
        }
        public async Task<int> Create(TrainingPlanModel model)
        {
            return await trainingPlanRepository.Insert(mapper.Map<TrainingPlanEntity>(model));
        }

        public async Task<int> CreateLM(TrainingPlanListModel model)
        {
            var entity = mapper.Map<TrainingPlanEntity>(model);
            return await trainingPlanRepository.Insert(entity);
        }

        //TODO - check if delete works
        public void Delete(TrainingPlanModel model)
        {
            //TODO
            trainingPlanRepository.Delete(mapper.Map<TrainingPlanEntity>(model));
        }

        public void DeleteLM(TrainingPlanListModel model)
        {
            //TODO
            trainingPlanRepository.Delete(mapper.Map<TrainingPlanEntity>(model));
        }


        public async Task<List<TrainingPlanModel>> GetAll()
        {
            //TODO
            return mapper.Map<List<TrainingPlanModel>>(await trainingPlanRepository.GetAll());
        }

        public async Task<List<TrainingPlanListModel>> GetAllLM()
        {
            List<TrainingPlanEntity> entity = await trainingPlanRepository.GetAll();
            return mapper.Map<List<TrainingPlanListModel>>(entity);
        }

        //TODO map descendants - trainings
        public async Task<TrainingPlanModel?> GetById(int id)
        {
            var entity = await trainingPlanRepository.GetById(id);
            var trainings = await trainingRepository.GetByTrainingPlanId(id);
            var trainingListModels = mapper.Map<List<TrainingListModel>>(trainings);
            TrainingPlanModel model = new TrainingPlanModel(entity.Id, entity.Name, entity.Description, trainingListModels);

            return model;

        }

        public async Task<TrainingPlanListModel?> GetByIdLM(int id)
        {
            var entity = await trainingPlanRepository.GetById(id);
            TrainingPlanListModel model = new TrainingPlanListModel(entity.Id, entity.Name, entity.Description);
            return model;

        }

        public async Task<int?> Update(TrainingPlanModel model)
        {
            return await trainingPlanRepository.Update(mapper.Map<TrainingPlanEntity>(model));
        }

        public async Task<int?> UpdateLM(TrainingPlanListModel model)
        {
            return await trainingPlanRepository.Update(mapper.Map<TrainingPlanEntity>(model));
        }


    }
}
