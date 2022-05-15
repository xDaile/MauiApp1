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

        List<TrainingPlanListModel> trainingPlans = new List<TrainingPlanListModel>();
        TrainingPlanListModel a = new TrainingPlanListModel(1, "Korte");
//        TrainingPlanListModel b = new TrainingPlanListModel(2, "GVT");
//        TrainingPlanListModel c = new TrainingPlanListModel(3, "Roubik Full Body");
//        TrainingPlanListModel d = new TrainingPlanListModel(4, "Tabata");
//        TrainingPlanListModel e = new TrainingPlanListModel(5, "Zumba");
        trainingPlans.Add(a);
//        trainingPlans.Add(b);
//        trainingPlans.Add(c);
//        trainingPlans.Add(d);
//        trainingPlans.Add(e);
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
