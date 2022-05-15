using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiApp1.DAL.Entities
{
    public abstract record EntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; } 
        public string Description { get; init; } 
    }
}
