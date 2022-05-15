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
        ExerciseModel a = new (1, "Bench Press", "Name");
        ExerciseModel b = new (2, "Deadlift", "Name");
        ExerciseModel c = new (3, "Squat","Name");
        ExerciseModel d = new (4, "Biceps curls","Name");
        ExerciseModel e = new (5, "Triceps extensions","Name");
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
