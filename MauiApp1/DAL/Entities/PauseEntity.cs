using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.DAL.Entities;
using SQLite;

namespace MauiApp1.DAL.Entities
{
    public record PauseEntity : EntityBase
    {
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public int Order { get; set; }
        [Indexed]
        public int TrainingId { get; set; }
    }
}
