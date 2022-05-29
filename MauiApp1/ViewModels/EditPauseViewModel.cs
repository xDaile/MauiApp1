using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using MauiApp1.BL.Facades.Interfaces;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
[QueryProperty(nameof(PauseId), "pauseId")]
public partial class EditPauseViewModel : ViewModelBase
{

    public string PauseId { private get; set; }

    public ITrainingFacade TrainingFacade;
    public IPauseFacade PauseFacade;


    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private PauseModel existingPause;

    public EditPauseViewModel(ITrainingFacade trainingFacade, IPauseFacade pauseFacade)
    {
        this.TrainingFacade = trainingFacade;
        this.PauseFacade = pauseFacade;
        //Be aware that TrainingPlanID and Order are just temporary in the model, because they are not accessible during constructor
           }

    public override async Task OnAppearingAsync()
    {
        this.ExistingPause = await PauseFacade.GetById(Convert.ToInt32(PauseId));
        await base.OnAppearingAsync();
        
    }

    [ICommand]
    private async Task EditPauseAsync()
    {

        int pauseId = Convert.ToInt32(PauseId);
       
        PauseModel model = new PauseModel(ExistingPause.Id, ExistingPause.Description, ExistingPause.Name, ExistingPause.Duration, ExistingPause.Order, ExistingPause.TrainingId);
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

        await TrainingFacade.UpdateTrainingItem(model);
        await Shell.Current.GoToAsync("..");
        return;
    }

    [ICommand]
    private async Task GetSecondsForExistingPausePromptAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_pause_duration, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, ExistingPause.Duration.TotalSeconds.ToString());
        if (result.Equals(null)) return;
        int duration = Convert.ToInt32(result);
        ExistingPause = new PauseModel(ExistingPause.Id, ExistingPause.Description, ExistingPause.Name, new TimeSpan(0, 0, duration), ExistingPause.Order, ExistingPause.TrainingId);
    }


}
