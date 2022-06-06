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
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly IMapper mapper;
        private readonly IStorage storage;
        public ExerciseRepository(IMapper mapper, IStorage storage)
        {
            this.mapper = mapper;
            this.storage = storage;
        }
        public async Task Delete(ExerciseEntity exercise)
        {
            await storage.DeleteAsync(exercise);
            //string Query = String.Format("Delete from exercise where exercise.id ={0}", exercise.Id);
        }


        public async Task<bool> Exists(ExerciseEntity exercise)
        {
            //string Query = String.Format("Select 1 from exercise where exercise.id ={0}", exercise.Id);
            throw new NotImplementedException();
        }


        public async Task<List<ExerciseEntity>> GetAll()
        {
            return await storage.GetAllAsync<ExerciseEntity>();
            //string Query = String.Format("Select * from exercise");

        }

        public async Task<ExerciseEntity?> GetById(int id)
        {

            return await storage.GetByIdAsync<ExerciseEntity>(id);
            //string Query = String.Format("Select * from exercise where exercise.id = {0}", id);

        }

        public async Task<int> Insert(ExerciseEntity exercise)
        {
            return await storage.SetAsync(exercise);
            //string Query = String.Format("Insert into exercise (name, description) values ('{0}', '{1}')", exercise.Name, exercise.Description);
        }

        public async Task<int?> Update(ExerciseEntity exercise)
        {
            return await storage.SetAsync(exercise);
            //string Query = String.Format("Update exercise (name, description) values ('{0}', '{1}') where exercise.id={2}",exercise.Name, exercise.Description, exercise.Id);
        }


    }
}
