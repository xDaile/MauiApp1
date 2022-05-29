using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using MauiApp1.BL.Facades.Interfaces;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
[QueryProperty(nameof(TrainingId), "trainingId")]
public partial class CreatePauseViewModel : ViewModelBase
{

    public string TrainingId { private get; set; }

    public ITrainingFacade TrainingFacade;


    [ObservableProperty]
    private TimeSpan duration;


    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private PauseModel newPause;

    public CreatePauseViewModel(ITrainingFacade trainingFacade)
    {
        this.TrainingFacade = trainingFacade;
        //Be aware that TrainingPlanID and Order are just temporary in the model, because they are not accessible during constructor
        NewPause = new PauseModel(null, "", "Pause", new TimeSpan(0, 0, 60), 0, Convert.ToInt32(TrainingId));
    }

    [ICommand]
    private async Task CreatePauseAsync()
    {

        int trainingId = Convert.ToInt32(TrainingId);
        var order = await TrainingFacade.GetExistingTrainingItemsCount(trainingId);
        PauseModel model = new PauseModel(null, newPause.Description, newPause.Name, newPause.Duration, order, trainingId);
        if (model.Name.Length < 1)
        {
            ErrorMessage = "Name of training is too short";
            return;
        }
        if (model.Duration.TotalSeconds.Equals(0))
        {
            ErrorMessage = "Pause duration is too short";
            return;
        }

        await TrainingFacade.CreateTrainingItem(model);
        await Shell.Current.GoToAsync("..");
        return;
    }

    [ICommand]
    private async Task GetSecondsForNewPausePromptAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_pause_duration, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, newPause.Duration.TotalSeconds.ToString());
        if (result.Equals(null)) return;
        int duration = Convert.ToInt32(result);
        NewPause = new PauseModel(null, newPause.Name, newPause.Description, new TimeSpan(0, 0, duration), newPause.Order, newPause.TrainingId);
    }


}
