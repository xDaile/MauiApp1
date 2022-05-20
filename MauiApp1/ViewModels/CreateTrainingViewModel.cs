using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using MauiApp1.BL.Facades.Interfaces;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
[QueryProperty(nameof(TrainingPlanId), "trainingPlanId")]
public partial class CreateTrainingViewModel : ViewModelBase
{

    public string TrainingPlanId { private get; set; }

    public ITrainingFacade TrainingFacade;

    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private TrainingListModel newTraining;

    public CreateTrainingViewModel(ITrainingFacade trainingFacade)
    {
        this.TrainingFacade = trainingFacade;
        //Be aware that TrainingPlanID and Order are just temporary in the model, because they are not accessible during constructor
        NewTraining = new TrainingListModel(null, "", "", 0, 0);
    }

    [ICommand]
    private async Task CreateTrainingAsync()
    {
        
        int trainingPlanId = Convert.ToInt32(TrainingPlanId);
        var order = await TrainingFacade.GetExistingTrainingsCount(trainingPlanId);
        TrainingListModel model = new TrainingListModel(null, NewTraining.Name, NewTraining.Description, order, trainingPlanId);
        if (model.Name.Length < 1)
        {
            ErrorMessage = "Name of training is too short";
            return;
        }

        await TrainingFacade.CreateLM(model);
        await Shell.Current.GoToAsync("..");
        return;
    }


}
