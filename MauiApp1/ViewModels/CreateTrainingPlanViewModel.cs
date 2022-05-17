using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using MauiApp1.BL.Facades.Interfaces;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
public partial class CreateTrainingPlanViewModel:ViewModelBase
{
    [ObservableProperty]
    private TrainingPlanListModel newTrainingPlan;

    public ITrainingPlanFacade TrainingPlanFacade;

    [ObservableProperty]
    private string errorMessage;

    public CreateTrainingPlanViewModel(ITrainingPlanFacade trainingPlanFacade)
    {
        this.TrainingPlanFacade= trainingPlanFacade;
        NewTrainingPlan = new TrainingPlanListModel(null,"","");
    }

    [ICommand]
    private async Task CreateTrainingPlanAsync(String name)
    {

        if (newTrainingPlan.Name.Length < 1)
        {
            ErrorMessage = "Name of training plan is too short";
            return;
        }
        var result = await TrainingPlanFacade.CreateFromListModel(NewTrainingPlan);
        await Shell.Current.GoToAsync("..");
        return;
    }


}
