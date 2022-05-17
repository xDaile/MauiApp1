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

            CreateMap<ExerciseEntity, ExerciseModel>().ReverseMap();
           
            CreateMap<TrainingPlanEntity, TrainingPlanModel>().ReverseMap();
            CreateMap<TrainingPlanEntity, TrainingPlanListModel>().ReverseMap();
          //  CreateMap<List<TrainingPlanEntity>, List<TrainingPlanModel>>().ReverseMap();

            CreateMap<ExerciseTrainingEntity, ExerciseTrainingModel>();
            CreateMap<PauseEntity, PauseModel>();
            CreateMap<TrainingEntity, TrainingListModel>().ReverseMap();
            CreateMap<TrainingEntity, TrainingModel>();
            CreateMap<ExerciseModel, ExerciseEntity>();
            
            //CreateMap<List<ExerciseEntity>, List<ExerciseModel>>();

            
            //CreateMap<List<ExerciseModel> ,List<ExerciseEntity>>();

        }

    }
}
