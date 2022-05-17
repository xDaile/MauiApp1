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
public partial class DetailTrainingPlanViewModel : ViewModelBase
{
    public string? Id { private get; set; }

    public ITrainingPlanFacade TrainingPlanFacade;

    [ObservableProperty]
    private TrainingPlanModel existingTrainingPlan;

    [ObservableProperty]
    private string errorMessage;

    public DetailTrainingPlanViewModel(ITrainingPlanFacade trainingPlanFacade)
    {
        TrainingPlanFacade = trainingPlanFacade;
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        int id = Convert.ToInt32(Id);
        ExistingTrainingPlan = await TrainingPlanFacade.GetById(id);
    }

    [ICommand]
    private async Task SaveTrainingPlanAsync()
    {
        if (existingTrainingPlan.Name.Length < 1)
        {
            ErrorMessage = "Name of the trainingPlan is too short";
            return;
        }
        await TrainingPlanFacade.Update(existingTrainingPlan);
        await Shell.Current.GoToAsync("..");
        return;
    }

    //TODO remove descendants if there are some
    [ICommand]
    private async Task DeleteTrainingPlanAsync()
    {
        TrainingPlanFacade.Delete(existingTrainingPlan);
        await Shell.Current.GoToAsync("..");
        return;
    }
}
