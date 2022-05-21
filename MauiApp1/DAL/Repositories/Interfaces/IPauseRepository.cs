using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.DAL.Entities;

namespace MauiApp1.DAL.Repositories.Interfaces
{
    public interface IPauseRepository:IRepository<PauseEntity>
    {
        Task<List<PauseEntity>?> GetByTrainingId(int id);
    }
}
