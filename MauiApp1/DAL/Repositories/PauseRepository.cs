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
    public class PauseRepository : IPauseRepository
    {
        private readonly IMapper mapper;
        private readonly IStorage storage;
        public PauseRepository(IMapper mapper, IStorage storage)
        {
            this.mapper = mapper;
            this.storage = storage;
        }

        public async Task Delete(PauseEntity entity)
        {
            await storage.DeleteAsync(entity);
            //string Query = String.Format("Delete from pause where pause.id ={0}", entity.Id);
        }

        public async Task<bool> Exists(PauseEntity entity)
        {
            //string Query = String.Format("Select 1 from pause where pause.id ={0}", entity.Id);
            throw new NotImplementedException();
        }

        public async Task<List<PauseEntity>> GetAll()
        {
            return await storage.GetAllAsync<PauseEntity>();
            //string Query = String.Format("Select * from pause");
        }

        public async Task<PauseEntity?> GetById(int id)
        {
            return await storage.GetByIdAsync<PauseEntity>(id);
            //string Query = String.Format("Select * from pause where pause.id = {0}", id);
        }

        public async Task<int> Insert(PauseEntity pause)
        {
            return await storage.SetAsync<PauseEntity>(pause);
            //string Query = String.Format("Insert into pause (name, seconds, training_id, order_, description) values ('{0}', {1}, {2}, {3}, '{4}')", pause.Name, pause.Seconds, pause.TrainingId, pause.Order, pause.Description);
        }

        public async Task<int?> Update(PauseEntity pause)
        {
            return await storage.SetAsync<PauseEntity>(pause);
            //string Query = String.Format("Update pause (name, seconds, training_id, order_, description) values ('{0}', {1}, {2}, {3}, '{4}') where pause.id={5}", pause.Name,pause.Seconds,pause.TrainingId,pause.Order,pause.Description, pause.Id);
        }
    }
}
