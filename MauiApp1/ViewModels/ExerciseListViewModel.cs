using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MauiApp1.BL.Facades.Interfaces;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
public partial class ExerciseListViewModel : ViewModelBase
{

    private readonly IRoutingService routingService;

    public IExerciseFacade ExerciseFacade;

    [ObservableProperty]
    private IList<ExerciseModel>? exercises;

    public ExerciseListViewModel(IRoutingService routingService, IExerciseFacade exerciseFacade)
    {
        this.routingService = routingService;
        this.ExerciseFacade = exerciseFacade;
    }

    public override async Task OnAppearingAsync()
    {
        //Exercises = SeedExercises();
        await base.OnAppearingAsync();
        Exercises = await ExerciseFacade.GetAll();
    }

    [ICommand]
    private async Task GoToDetailAsync(int id)
    {
        var route = routingService.GetRouteByViewModel<DetailExerciseViewModel>();
        await Shell.Current.GoToAsync($"{route}?id={id}");
    }


    //Navigate to page for adding new exercise
    [ICommand]
    private async Task AddNewAsync()
    {
        var route = routingService.GetRouteByViewModel<CreateExerciseViewModel>();
        await Shell.Current.GoToAsync($"{route}");
        return;
    }
}
