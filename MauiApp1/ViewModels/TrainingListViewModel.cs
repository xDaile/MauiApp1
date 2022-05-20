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
[QueryProperty(nameof(Id), "id")]
public partial class TrainingListViewModel: ViewModelBase
{
    private readonly IRoutingService routingService;
    public string? Id { private get; set; }
    private int id;

    [ObservableProperty]
    private TrainingPlanModel trainingPlan;

    //[ObservableProperty]
    //private List<TrainingListModel> trainings;

    public ITrainingFacade TrainingFacade;
    public ITrainingPlanFacade TrainingPlanFacade;

    public TrainingListViewModel(IRoutingService routingService, ITrainingFacade trainingFacade, ITrainingPlanFacade trainingPlanFacade)
    {
        this.TrainingFacade = trainingFacade;
        this.TrainingPlanFacade = trainingPlanFacade;
        this.routingService = routingService;
    }

    public override async Task OnAppearingAsync()
    {
        
        this.id = Convert.ToInt32(Id);
        await this.RefreshTrainingPlan();
        await base.OnAppearingAsync();
    }
    
    public async Task RefreshTrainingPlan()
    {
        TrainingPlan = await TrainingPlanFacade.GetById(this.id);
    }
    
    [ICommand]
    private async Task GoToDetailAsync(int id)
    {
        //var route = routingService.GetRouteByViewModel<TrainingViewModel>();
        //await Shell.Current.GoToAsync($"{route}?trainingId={id}");
    }
    
    [ICommand]
    private async Task AddNewAsync()
    {
        var route = routingService.GetRouteByViewModel<CreateTrainingViewModel>();
        int id = Convert.ToInt32(TrainingPlan.Id);
        await Shell.Current.GoToAsync($"{route}?trainingPlanId={id}");
        return;
    }

    [ICommand]
    private async Task EditTrainingAsync(int id)
    {
        var route = routingService.GetRouteByViewModel<DetailTrainingViewModel>();
        await Shell.Current.GoToAsync($"{route}?trainingPlanId={id}");
        return;
    }

    [ICommand]
    private async Task MoveTrainingDownAsync(int id)
    {
        await TrainingFacade.MoveTrainingDown(id);
        await RefreshTrainingPlan();
        return;
    }

    [ICommand]
    private async Task MoveTrainingUpAsync(int id)
    {
        await TrainingFacade.MoveTrainingUp(id);
        await RefreshTrainingPlan();
        return;
    }
}
