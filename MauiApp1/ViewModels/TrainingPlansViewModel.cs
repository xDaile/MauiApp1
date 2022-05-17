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
using MauiApp1.BL.Facades;
using MauiApp1.BL.Facades.Interfaces;

namespace MauiApp1.ViewModels;


[INotifyPropertyChanged]
public partial class TrainingPlansViewModel : ViewModelBase
{
    private readonly IRoutingService routingService;

    [ObservableProperty]
    private IList<TrainingPlanListModel>? trainingPlans;

    public ITrainingPlanFacade TrainingPlanFacade;



    public TrainingPlansViewModel(IRoutingService routingService, ITrainingPlanFacade trainingPlanFacade)
    {
        this.routingService = routingService;
        this.TrainingPlanFacade = trainingPlanFacade;
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        // TrainingPlans = SeedTrainingPlans();
        TrainingPlans = await TrainingPlanFacade.GetAllList();
    }

    [ICommand]
    private async Task GoToDetailAsync(int id)
    {
        var route = routingService.GetRouteByViewModel<TrainingListViewModel>();
        await Shell.Current.GoToAsync($"{route}?id={id}");
    }

    [ICommand]
    private async Task AddNewAsync()
    {
        var route = routingService.GetRouteByViewModel<CreateTrainingPlanViewModel>();
        await Shell.Current.GoToAsync($"{route}");
        return;
    }

    [ICommand]
    private async Task EditTrainingPlanAsync(int id)
    {
        var route = routingService.GetRouteByViewModel<DetailTrainingPlanViewModel>();
        await Shell.Current.GoToAsync($"{route}?id={id}");
        return;
    }


}
