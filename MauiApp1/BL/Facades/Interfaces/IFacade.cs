using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MauiApp1.Models;
using System.Threading.Tasks;

namespace MauiApp1.BL.Facades.Interfaces
{
    public interface IFacade<T>
    {
        public Task<List<T>> GetAll();
        public Task<T?> GetById(int id);
        public Task<int> Create(T model);
        public Task<int?> Update(T model);
        public Task Delete(T model);
    }
}
