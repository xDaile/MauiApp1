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
        ExerciseTrainingModel a = new ExerciseTrainingModel(1,10, 4,5, 40,1,true,"descr","BP");
        ExerciseTrainingModel b = new ExerciseTrainingModel(1,10, 5,5, 80, 2,true, "descr", "BP");
        ExerciseTrainingModel c = new ExerciseTrainingModel(1,10, 5,5, 50, 60, true, "descr", "DP");
        exerciseTraining.Add(a);
        exerciseTraining.Add(b);
        exerciseTraining.Add(c);
        return exerciseTraining;

    }
}
