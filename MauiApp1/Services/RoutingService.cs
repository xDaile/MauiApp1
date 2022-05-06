﻿using System;
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
        new ("//settings", typeof(SettingsView), typeof(SettingsViewModel)),
        new ("//training_plans", typeof(TrainingPlansView), typeof(TrainingPlansViewModel)),
        new ("//exercises", typeof(ExerciseListView), typeof(ExerciseListViewModel)),
        new ("//user", typeof(UserView), typeof(UserViewModel)),
        new ("//create_exercise", typeof(CreateExerciseView), typeof(CreateExerciseViewModel)),
        new ("//create_training_plan", typeof(CreateTrainingPlanView), typeof(CreateTrainingPlanViewModel))

    };

    public string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}
