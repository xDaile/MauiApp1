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
    public class PauseFacade : IPauseFacade
    {
        private readonly IPauseRepository exerciseRepository;
        private readonly IMapper mapper;
        public PauseFacade(IPauseRepository exerciseRepository, IMapper mapper)
        {
            this.exerciseRepository = exerciseRepository;
            this.mapper = mapper;
        }
        public async Task<int> Create(PauseModel model)
        {
            return await exerciseRepository.Insert(mapper.Map<PauseEntity>(model));
        }

        public async Task Delete(PauseModel model)
        {
            await exerciseRepository.Delete(mapper.Map<PauseEntity>(model));
            return;
        }

        public async Task<List<PauseModel>> GetAll()
        {
            return mapper.Map<List<PauseModel>>(await exerciseRepository.GetAll());
        }

        public async Task<PauseModel?> GetById(int id)
        {
            return mapper.Map<PauseModel>(await exerciseRepository.GetById(id));
        }

        public async Task<int?> Update(PauseModel model)
        {
            return await exerciseRepository.Update(mapper.Map<PauseEntity>(model));
        }
    }
}
