using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MauiApp1.DAL.Repositories;
using MauiApp1.DAL.Entities;
using MauiApp1.BL.Facades.Interfaces;

namespace MauiApp1.BL.Facades
{
    public class TrainingPlanFacade : ITrainingPlanFacade
    {
        private readonly TrainingPlanRepository trainingPlanRepository;
        private readonly IMapper mapper;

        public TrainingPlanFacade(TrainingPlanRepository trainingPlanRepository, IMapper mapper)
        {
            this.trainingPlanRepository = trainingPlanRepository;
            this.mapper = mapper;
        }
        public async Task<int> Create(TrainingPlanModel model)
        {
            return await trainingPlanRepository.Insert(mapper.Map<TrainingPlanEntity>(model));
        }

        public void Delete(TrainingPlanModel model
            )
        {
            trainingPlanRepository.Delete(mapper.Map<TrainingPlanEntity>(model));
        }

        public async Task<List<TrainingPlanListModel>> GetAllList()
        {
            return mapper.Map<List<TrainingPlanListModel>>(await trainingPlanRepository.GetAll());
        }

        public async Task<List<TrainingPlanModel>> GetAll()
        {
            return mapper.Map<List<TrainingPlanModel>>(await trainingPlanRepository.GetAll());
        }

        public async Task<TrainingPlanModel?> GetById(int id)
        {
            return mapper.Map<TrainingPlanModel>(await trainingPlanRepository.GetById(id));
        }

        public async Task<int?> Update(TrainingPlanModel model)
        {
            return await trainingPlanRepository.Update(mapper.Map<TrainingPlanEntity>(model));
        }

    }
}
