using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.DAL.Entities;

namespace MauiApp1.DAL.Entities
{
    public record PauseEntity : EntityBase
    {
        public string Name { get; set; }
        public int Seconds { get; set; }

        public int Order { get; set; }
        public Guid TrainingId { get; set; }
        public TrainingEntity? Training { get; set; }
    }
}
