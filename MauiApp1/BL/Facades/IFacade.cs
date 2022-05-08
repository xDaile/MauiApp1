using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MauiApp1.Models;
using System.Threading.Tasks;

namespace MauiApp1.BL.Facades
{
    public interface IFacade<T>
    {
        public List<T> GetAll();
        public T? GetById(Guid id);
        public Guid Create(T model);
        public Guid? Update(T model);
        public void Delete(Guid id);
    }
}
