using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
public partial class ExerciseListViewModel:ViewModelBase
{
    private readonly IRoutingService routingService;

    [ObservableProperty]
    private IList<ExerciseModel>? exercises;

    public override async Task OnAppearingAsync()
    {
        Exercises = SeedExercises();
    }

    public ExerciseListViewModel(IRoutingService routingService)
    {
        this.routingService = routingService;
    }

    private IList<ExerciseModel> SeedExercises()
    {
        List<ExerciseModel> exercises = new();
        ExerciseModel a = new (System.Guid.NewGuid(), "Bench Press");
        ExerciseModel b = new (System.Guid.NewGuid(), "Deadlift");
        ExerciseModel c = new (System.Guid.NewGuid(), "Squat");
        ExerciseModel d = new (System.Guid.NewGuid(), "Biceps curls");
        ExerciseModel e = new (System.Guid.NewGuid(), "Triceps extensions");
        exercises.Add(a);
        exercises.Add(b);
        exercises.Add(c);
        exercises.Add(d);
        exercises.Add(e);
        return exercises;

    }

    [ICommand]
    private async Task GoToDetailAsync(Guid id)
    {
        Console.WriteLine("HEREEEEEEEEEEEEEEEEE");
        Console.WriteLine(id);
        var route = routingService.GetRouteByViewModel<ExerciseViewModel>();
        await Shell.Current.GoToAsync($"{route}?id={id}");
    }

    [ICommand]
    private async Task AddNewAsync()
    {
        var route = routingService.GetRouteByViewModel<CreateExerciseViewModel>();
        await Shell.Current.GoToAsync($"{route}");
        return;
    }
}
