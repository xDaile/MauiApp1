using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.DAL.Entities;
using MauiApp1.Models;
using AutoMapper;

namespace MauiApp1.BL
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<ExerciseEntity, ExerciseEntity>();
            CreateMap<ExerciseEntity, ExerciseModel>();
            CreateMap<ExerciseModel, ExerciseEntity>();

            CreateMap<ExerciseTrainingEntity, ExerciseTrainingModel>();
            CreateMap<PauseEntity, PauseModel>();
            CreateMap<TrainingEntity, TrainingListModel>();
            CreateMap<TrainingEntity, TrainingModel>();
            CreateMap<TrainingPlanEntity, TrainingPlanModel>();
            CreateMap<TrainingPlanEntity, TrainingPlanListModel>();
            CreateMap<ExerciseModel, ExerciseEntity>();
            //CreateMap<List<ExerciseEntity>, List<ExerciseModel>>();

            
            //CreateMap<List<ExerciseModel> ,List<ExerciseEntity>>();

        }

    }
}
