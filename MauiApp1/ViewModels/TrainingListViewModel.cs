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

    [ObservableProperty]
    private TrainingPlanModel trainingPlan;

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
        base.OnAppearingAsync();
        int id = Convert.ToInt32(Id);
        trainingPlan = await TrainingPlanFacade.GetById(id);
    }

}
