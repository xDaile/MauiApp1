using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.BL.Facades
{
    public class TrainingFacade : IFacade<TrainingModel>
    {
        public Guid Create(TrainingModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<TrainingModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public TrainingModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid? Update(TrainingModel model)
        {
            throw new NotImplementedException();
        }

        public Guid AddExercise(ExerciseTrainingModel exercise)
        {
            throw new NotImplementedException();
        }

        public Guid UpdateOrderExercise(ExerciseTrainingModel exercise)
        {
            throw new NotImplementedException();
        }

        public Guid RemoveExercise(ExerciseTrainingModel exercise)
        {
            throw new NotImplementedException();
        }

        public Guid AddPause(PauseModel pause)
        {
            throw new NotImplementedException();
        }

        public Guid UpdateOrderPause(PauseModel pause)
        {
            throw new NotImplementedException();
        }

        public Guid RemovePause(PauseModel pause)
        {
            throw new NotImplementedException();
        }
    }
}
