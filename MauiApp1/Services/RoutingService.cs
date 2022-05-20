using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;
using MauiApp1.Views;
using MauiApp1.ViewModels;

namespace MauiApp1.Services;

public class RoutingService : IRoutingService
{
    public ICollection<RouteModel> Routes => new List<RouteModel>
    {
        new ("//exercises", typeof(ExerciseListView), typeof(ExerciseListViewModel)),
        new ("//exercises/create_e", typeof(CreateExerciseView), typeof(CreateExerciseViewModel)),
        new ("//exercises/detail_e", typeof(DetailExerciseView), typeof(DetailExerciseViewModel)),

        new ("//training_plans", typeof(TrainingPlansView), typeof(TrainingPlansViewModel)),
        new ("//training_plans/create_tp", typeof(CreateTrainingPlanView), typeof(CreateTrainingPlanViewModel)),
        new ("//training_plans/detail_tp", typeof(DetailTrainingPlanView), typeof(DetailTrainingPlanViewModel)),

        new ("//training_plans/trainings/", typeof(TrainingListView), typeof(TrainingListViewModel)),
        new ("//training_plans/trainings/create_t", typeof(CreateTrainingView), typeof(CreateTrainingViewModel)),
        new ("//training_plans/trainings/detail_t", typeof(DetailTrainingView), typeof(DetailTrainingViewModel)),

        new ("//settings", typeof(SettingsView), typeof(SettingsViewModel)),
        new ("//user", typeof(UserView), typeof(UserViewModel)),
        //DetailTrainingPlanViewModel

    };

    public string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}
