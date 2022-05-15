using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.DAL.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task<int> Insert(T entity);
        Task<int?> Update(T entity);
        void Delete(T entity);
        Task<bool> Exists(T entity);
    }
}
