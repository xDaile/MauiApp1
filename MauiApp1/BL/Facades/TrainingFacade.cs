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

    public class TrainingFacade : ITrainingFacade
    {
        private readonly TrainingRepository trainingRepository;
        private readonly IMapper mapper;

        public TrainingFacade(TrainingRepository trainingRepository, IMapper mapper)
        {
            this.trainingRepository = trainingRepository;
            this.mapper = mapper;
        }
        public async Task<int> Create(TrainingModel model)
        {
            return await trainingRepository.Insert(mapper.Map<TrainingEntity>(model));
        }

        public void Delete(TrainingModel model)
        {
            trainingRepository.Delete(mapper.Map<TrainingEntity>(model));
        }

        public async Task<List<TrainingModel>> GetAll()
        {
            return mapper.Map<List<TrainingModel>>(await trainingRepository.GetAll());
        }

        public async Task<TrainingModel?> GetById(int id)
        {
            return mapper.Map<TrainingModel>(await trainingRepository.GetById(id));
        }

        public async Task<int?> Update(TrainingModel model)
        {
            return await trainingRepository.Update(mapper.Map<TrainingEntity>(model));
        }

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
    }
}
