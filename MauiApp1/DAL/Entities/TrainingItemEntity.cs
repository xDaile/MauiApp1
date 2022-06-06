using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiApp1.DAL.Entities
{
    public record TrainingItemEntity: EntityBase
    {
        [Indexed]
        public int TrainingId { get; set; }

        public int Order { get; set; }
    }
}
