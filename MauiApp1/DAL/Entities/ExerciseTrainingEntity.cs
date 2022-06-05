using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.DAL.Entities;
using SQLite;

namespace MauiApp1.DAL.Entities
{
    public record ExerciseTrainingEntity : TrainingItemEntity
    {

        public TimeSpan RestSeconds { get; set; }
        public TimeSpan ExerciseSeconds { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }
        public int Sets { get; set; }
        public bool RestAfterLastSet { get; set; }
        [Indexed]
        public int ExerciseId { get; set; }

        public string ExerciseName { get; set; } 

    }
}
