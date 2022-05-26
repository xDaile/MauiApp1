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
        new ("//exercises/edit_e", typeof(EditExerciseView), typeof(EditExerciseViewModel)),

        new ("//training_plans", typeof(TrainingPlanListView), typeof(TrainingPlanListViewModel)),
        new ("//training_plans/create_tp", typeof(CreateTrainingPlanView), typeof(CreateTrainingPlanViewModel)),
        new ("//training_plans/edit_tp", typeof(EditTrainingPlanView), typeof(EditTrainingPlanViewModel)),

        new ("//training_plans/trainings/", typeof(TrainingListView), typeof(TrainingListViewModel)),
        new ("//training_plans/trainings/create_t", typeof(CreateTrainingView), typeof(CreateTrainingViewModel)),
        new ("//training_plans/trainings/edit_t", typeof(EditTrainingView), typeof(EditTrainingViewModel)),

        new ("//training_plans/trainings/training", typeof(TrainingView), typeof(TrainingViewModel)),
        new ("//training_plans/trainings/training/create_p", typeof(CreatePauseView), typeof(CreatePauseViewModel)),
        //new ("//training_plans/trainings/training/edit_p", typeof(EditPauseView), typeof(EditPauseViewModel)),
        new ("//training_plans/trainings/training/create_et", typeof(CreateExerciseTrainingView), typeof(CreateExerciseTrainingViewModel)),
        //new ("//training_plans/trainings/training/edit_et", typeof(EditExerciseTrainingView), typeof(EditExerciseTrainingViewModel)),

        new ("//settings", typeof(SettingsView), typeof(SettingsViewModel)),
        new ("//user", typeof(UserView), typeof(UserViewModel)),

    };

    public string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}
