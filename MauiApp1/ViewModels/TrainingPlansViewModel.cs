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
public partial class TrainingPlansViewModel : ViewModelBase
{
    private readonly IRoutingService routingService;

    [ObservableProperty]
    private IList<TrainingPlanListModel>? trainingPlans;

    public override async Task OnAppearingAsync()
    {
        TrainingPlans = SeedTrainingPlans();
    }

    public TrainingPlansViewModel(IRoutingService routingService)
    {
        this.routingService = routingService;
    }

    private IList<TrainingPlanListModel> SeedTrainingPlans()
    {
        List<TrainingPlanListModel> trainingPlans = new();
        TrainingPlanListModel a = new TrainingPlanListModel(System.Guid.NewGuid(), "Korte");
        TrainingPlanListModel b = new TrainingPlanListModel(System.Guid.NewGuid(), "GVT");
        TrainingPlanListModel c = new TrainingPlanListModel(System.Guid.NewGuid(), "Roubik Full Body");
        TrainingPlanListModel d = new TrainingPlanListModel(System.Guid.NewGuid(), "Tabata");
        TrainingPlanListModel e = new TrainingPlanListModel(System.Guid.NewGuid(), "Zumba");
        trainingPlans.Add(a);
        trainingPlans.Add(b);
        trainingPlans.Add(c);
        trainingPlans.Add(d);
        trainingPlans.Add(e);
        return trainingPlans;

    }

    [ICommand]
    private async Task GoToDetailAsync(Guid id)
    {
        Console.WriteLine("HEREEEEEEEEEEEEEEEEE");
        Console.WriteLine(id);
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
}
