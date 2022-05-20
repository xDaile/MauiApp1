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
using MauiApp1.BL.Facades.Interfaces;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
[QueryProperty(nameof(TrainingId), "trainingId")]
public partial class TrainingViewModel: ViewModelBase
{
    private readonly IRoutingService routingService;
    public string? TrainingId { private get; set; }


    [ObservableProperty]
    private TrainingModel training;

    public ITrainingFacade TrainingFacade;
    public ITrainingPlanFacade TrainingPlanFacade;

    public TrainingViewModel(IRoutingService routingService, ITrainingFacade trainingFacade, ITrainingPlanFacade trainingPlanFacade)
    {
        this.TrainingFacade = trainingFacade;
        this.TrainingPlanFacade = trainingPlanFacade;
        this.routingService = routingService;

    }

    public override async Task OnAppearingAsync()
    {
        base.OnAppearingAsync();
        int id = Convert.ToInt32(TrainingId);
        training = await TrainingFacade.GetById(id);
    }

    [ICommand]
    private async Task GoToDetailAsync(int id)
    {
        var route = routingService.GetRouteByViewModel<TrainingViewModel>();
        await Shell.Current.GoToAsync($"{route}?id={id}");
    }

    [ICommand]
    private async Task AddNewAsync()
    {
        var route = routingService.GetRouteByViewModel<CreateTrainingViewModel>();
        await Shell.Current.GoToAsync($"{route}");
        return;
    }

    [ICommand]
    private async Task EditTrainingPlanAsync(int id)
    {
        var route = routingService.GetRouteByViewModel<EditTrainingViewModel>();
        await Shell.Current.GoToAsync($"{route}?id={id}");
        return;
    }



}
