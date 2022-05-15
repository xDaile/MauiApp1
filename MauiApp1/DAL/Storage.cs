using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.DAL.Entities;
using SQLite;

namespace MauiApp1.DAL
{
    public class Storage : IStorage
    {
        public SQLiteAsyncConnection _storage { get; set; }
        private readonly string databasePath;
        public IList<ExerciseEntity> Exercise { get; }
        public IList<TrainingEntity> Training { get; }
        public IList<PauseEntity> Pause { get; }
        public IList<TrainingPlanEntity> TrainingPlan { get; }
        public IList<ExerciseTrainingEntity> ExerciseTraining { get; }
        public Storage()
        {
            databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3");
            _storage = new SQLiteAsyncConnection(databasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

            CreateTableAsync<ExerciseEntity>();
            CreateTableAsync<TrainingPlanEntity>();
            CreateTableAsync<TrainingEntity>();
            CreateTableAsync<ExerciseTrainingEntity>();
            CreateTableAsync<PauseEntity>();

        }

        public async Task<CreateTableResult> CreateTableAsync<T>()
            where T : EntityBase, new()
        {
            var connection = new SQLiteAsyncConnection(databasePath);
            var result = await connection.CreateTableAsync<T>();
            await connection.CloseAsync();
            return result;
        }

        public async Task<List<T>> GetAllAsync<T>()
            where T : new()
        {
            var connection = new SQLiteAsyncConnection(databasePath);
            var result = await connection.Table<T>().ToListAsync();
            await connection.CloseAsync();
            return result;
        }

        public async Task<T> GetByIdAsync<T>(int id)
            where T : EntityBase, new()
        {
            var connection = new SQLiteAsyncConnection(databasePath);
            var result = await connection.Table<T>().Where(model => model.Id == id).FirstOrDefaultAsync();
            await connection.CloseAsync();
            return result;
        }

        public async Task<int> SetAsync<T>(T entity)
            where T : EntityBase, new()
        {
            int result = 0;
            var connection = new SQLiteAsyncConnection(databasePath);
            if (entity.Id <1)
            {
                result = await connection.InsertAsync(entity);
            }
            else
            {
                result = await connection.UpdateAsync(entity);
            }
            await connection.CloseAsync();
            return result;
        }

        public async Task DeleteAsync<T>(T entity)
            where T : EntityBase, new()
        {
            var connection = new SQLiteAsyncConnection(databasePath);
            await connection.DeleteAsync(entity);
            await connection.CloseAsync();
        }
    }
}
