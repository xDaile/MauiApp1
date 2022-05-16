using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;
using AutoMapper;
using MauiApp1.DAL.Repositories;
using MauiApp1.DAL.Entities;
using MauiApp1.DAL.Repositories.Interfaces;
using MauiApp1.BL.Facades.Interfaces;

namespace MauiApp1.BL.Facades
{
    public class ExerciseFacade : IExerciseFacade
    {
        private readonly IExerciseRepository exerciseRepository;
        private readonly IMapper mapper;

        public ExerciseFacade(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            this.exerciseRepository = exerciseRepository;
            this.mapper = mapper;
        }

        public async Task<int> Create(ExerciseModel model)
        {
            return await exerciseRepository.Insert(mapper.Map<ExerciseEntity>(model));
        }

        public void Delete(ExerciseModel model)
        {
            exerciseRepository.Delete(mapper.Map<ExerciseEntity>(model));
        }

        public async Task<List<ExerciseModel>> GetAll()
        {
            return mapper.Map<List<ExerciseModel>>(await exerciseRepository.GetAll());
        }

        public async Task<ExerciseModel?> GetById(int id)
        {
            return mapper.Map<ExerciseModel>(await exerciseRepository.GetById(id));
        }

        public async Task<int?> Update(ExerciseModel model)
        {
            return await exerciseRepository.Update(mapper.Map<ExerciseEntity>(model));
        }
    }
}
