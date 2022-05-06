using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
public partial class TrainingListViewModel: ViewModelBase
{
    private readonly IRoutingService routingService;

    [ObservableProperty]
    private IList<ExerciseTrainingModel>? exerciseTraining;

    public override async Task OnAppearingAsync()
    {
        ExerciseTraining = SeedExerciseTraining();
    }

    public TrainingListViewModel(IRoutingService routingService)
    {
        this.routingService = routingService;
    }

    private IList<ExerciseTrainingModel> SeedExerciseTraining()
    {
        List<ExerciseTrainingModel> exerciseTraining = new List<ExerciseTrainingModel>();
        ExerciseTrainingModel a = new ExerciseTrainingModel("BP", 4, 40,60,1);
        ExerciseTrainingModel b = new ExerciseTrainingModel("DL", 5, 80, 60, 2);
        ExerciseTrainingModel c = new ExerciseTrainingModel("Squat", 5, 50, 60, 3);
        exerciseTraining.Add(a);
        exerciseTraining.Add(b);
        exerciseTraining.Add(c);
        return exerciseTraining;

    }
}
