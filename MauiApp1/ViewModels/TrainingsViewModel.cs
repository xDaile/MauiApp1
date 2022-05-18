using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.BL.Facades.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace MauiApp1.ViewModels;


[INotifyPropertyChanged]
[QueryProperty(nameof(Id), "id")]
public partial class TrainingsViewModel: ViewModelBase
{
    public string? Id { private get; set; }

    private readonly IRoutingService routingService;

    [ObservableProperty]
    private TrainingPlanModel trainingPlan;

    public ITrainingFacade TrainingFacade;
    public ITrainingPlanFacade TrainingPlanFacade;

    public TrainingsViewModel(IRoutingService routingService, ITrainingFacade trainingFacade, ITrainingPlanFacade trainingPlanFacade)
    {
        this.TrainingFacade = trainingFacade;
        this.TrainingPlanFacade = trainingPlanFacade;
        this.routingService = routingService;

    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        int id = Convert.ToInt32(Id);
        trainingPlan = await TrainingPlanFacade.GetById(2);
    }

    [ICommand]
    private async Task GoToDetailAsync(int id)
    {
        var route = routingService.GetRouteByViewModel<TrainingViewModel>();
        await Shell.Current.GoToAsync($"{route}?trainingId={id}");
    }

    [ICommand]
    private async Task AddNewAsync()
    {
        var route = routingService.GetRouteByViewModel<CreateTrainingViewModel>();
        await Shell.Current.GoToAsync($"{route}");
        return;
    }

    [ICommand]
    private async Task EditTrainingAsync(int id)
    {
        var route = routingService.GetRouteByViewModel<DetailTrainingViewModel>();
        await Shell.Current.GoToAsync($"{route}?trainingPlanId={id}");
        return;
    }



}
