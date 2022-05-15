using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiApp1.DAL.Entities
{
    public record ExerciseEntity : EntityBase
    {  
        public string Name { get; set; }

    }
}
