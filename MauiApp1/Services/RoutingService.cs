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
        new ("//exercises/create", typeof(CreateExerciseView), typeof(CreateExerciseViewModel)),
        new ("//exercises/detail", typeof(DetailExerciseView), typeof(DetailExerciseViewModel)),

        new ("//training_plans", typeof(TrainingPlansView), typeof(TrainingPlansViewModel)),
        new ("//training_plans/create", typeof(CreateTrainingPlanView), typeof(CreateTrainingPlanViewModel)),
        new ("//training_plans/detail", typeof(DetailTrainingPlanView), typeof(DetailTrainingPlanViewModel)),

        new ("//trainings/", typeof(TrainingsView), typeof(TrainingsViewModel)),
        //new ("//trainings/create", typeof(CreateTrainingView), typeof(CreateTrainingViewModel)),
        //new ("//trainings/detail", typeof(DetailTrainingView), typeof(DetailTrainingViewModel)),

        new ("//settings", typeof(SettingsView), typeof(SettingsViewModel)),
        new ("//user", typeof(UserView), typeof(UserViewModel)),
        //DetailTrainingPlanViewModel

    };

    public string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}
