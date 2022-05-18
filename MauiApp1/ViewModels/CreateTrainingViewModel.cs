using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using MauiApp1.BL.Facades.Interfaces;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
[QueryProperty(nameof(TrainingPlanId), "id")]
public partial class CreateTrainingViewModel:ViewModelBase
{
    [ObservableProperty]
    private TrainingListModel newTraining;

    public string? TrainingPlanId { private get; set; }

    public ITrainingFacade TrainingFacade;

    [ObservableProperty]
    private string errorMessage;

    public CreateTrainingViewModel(ITrainingFacade trainingFacade)
    {
        this.TrainingFacade= trainingFacade;
        NewTraining = new TrainingListModel(null,"","",0);
    }

    [ICommand]
    private async Task CreateTrainingPlanAsync(String name)
    {

        if (newTraining.Name.Length < 1)
        {
            ErrorMessage = "Name of training plan is too short";
            return;
        }
       // var result = await TrainingFacade.CreateLM(NewTraining);
        await Shell.Current.GoToAsync("..");
        return;
    }


}
