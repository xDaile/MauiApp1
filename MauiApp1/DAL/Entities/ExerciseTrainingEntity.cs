using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.DAL.Entities;

namespace MauiApp1.DAL.Entities
{
    public record ExerciseTrainingEntity : EntityBase
    {
        public int Seconds { get; set; }
        public int Reps { get; set; }
        public int Weight { get; set; }
        public int Order { get; set; }
        public bool RestAfterLastRep { get; set; }
        public Guid TrainingId { get; set; }
        public TrainingEntity? Training { get; set; }
        public Guid ExerciseId { get; set; }
        public ExerciseEntity? Exercise { get; set; }
    }
}
