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
public partial class CreateExerciseTrainingViewModel : ViewModelBase
{

    public string TrainingId { private get; set; }

    public ITrainingFacade TrainingFacade;
    public IExerciseFacade ExerciseFacade;

    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private List<ExerciseModel> exerciseList;
    
    [ObservableProperty]
    private ExerciseTrainingModel newExerciseTraining;

    [ObservableProperty]
    private TimeSpan duration;

    public CreateExerciseTrainingViewModel(ITrainingFacade trainingFacade, IExerciseFacade exerciseFacade)
    {
        this.TrainingFacade = trainingFacade;
        this.ExerciseFacade = exerciseFacade;
        //Be aware that TrainingPlanID and Order are just temporary in the model, because they are not accessible during constructor
        newExerciseTraining = new ExerciseTrainingModel(null, new TimeSpan(), new TimeSpan(), 0, 0, 0, 0, true, "Description", 0, Convert.ToInt32(TrainingId));
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        this.ExerciseList = await ExerciseFacade.GetAll();
    }

    [ICommand]
    private async Task CreateTrainingAsync()
    {

        int trainingId = Convert.ToInt32(TrainingId);
        var order = await TrainingFacade.GetExistingTrainingItemsCount(trainingId);
        ExerciseTrainingModel model = new ExerciseTrainingModel(
            null,
            newExerciseTraining.RestSeconds,
            newExerciseTraining.ExerciseSeconds,
            newExerciseTraining.Reps,
            newExerciseTraining.Weight,
            newExerciseTraining.Sets,
            order,
            newExerciseTraining.RestAfterLastSet,
            newExerciseTraining.Description,
            newExerciseTraining.ExerciseId,
            trainingId);
        if (model.ExerciseId == 0)
        {
            ErrorMessage = "You have to select exercise";
            return;
        }

        await TrainingFacade.CreateTrainingItem(model);
        await Shell.Current.GoToAsync("..");
        return;
    }

    [ICommand]
    private async Task GetSecondsForNewExerciseTrainingPromptAsync()
    {
        //int result = Convert.ToInt32(await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_pause_duration, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_confirm, null, 3, null, newPause.Duration.TotalSeconds.ToString()));
        //NewPause = new PauseModel(null, newPause.Name, newPause.Description, new TimeSpan(0, 0, result), newPause.Order, newPause.TrainingId);
    }
    
    [ICommand]
    private async Task SetRepsForNewExerciseTrainingPromptAsync()
    {
        //int result = Convert.ToInt32(await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_pause_duration, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_confirm, null, 3, null, newPause.Duration.TotalSeconds.ToString()));
        //NewPause = new PauseModel(null, newPause.Name, newPause.Description, new TimeSpan(0, 0, result), newPause.Order, newPause.TrainingId);
    }

    [ICommand]
    private async Task SetSetsForNewExerciseTrainingPromptAsync()
    {
        //int result = Convert.ToInt32(await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_pause_duration, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_confirm, null, 3, null, newPause.Duration.TotalSeconds.ToString()));
        //NewPause = new PauseModel(null, newPause.Name, newPause.Description, new TimeSpan(0, 0, result), newPause.Order, newPause.TrainingId);
    }

    
    [ICommand]
    private async Task SetWeightForNewExerciseTrainingPromptAsync()
    {
        //int result = Convert.ToInt32(await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_pause_duration, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_confirm, null, 3, null, newPause.Duration.TotalSeconds.ToString()));
        //NewPause = new PauseModel(null, newPause.Name, newPause.Description, new TimeSpan(0, 0, result), newPause.Order, newPause.TrainingId);
    }

    [ICommand]
    private async Task SetRepDurationForNewExerciseTrainingPromptAsync()
    {
        //int result = Convert.ToInt32(await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_pause_duration, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_confirm, null, 3, null, newPause.Duration.TotalSeconds.ToString()));
        //NewPause = new PauseModel(null, newPause.Name, newPause.Description, new TimeSpan(0, 0, result), newPause.Order, newPause.TrainingId);
    }

    [ICommand]
    private async Task SetRestDurationForNewExerciseTrainingPromptAsync()
    {
        //int result = Convert.ToInt32(await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_pause_duration, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_confirm, null, 3, null, newPause.Duration.TotalSeconds.ToString()));
        //NewPause = new PauseModel(null, newPause.Name, newPause.Description, new TimeSpan(0, 0, result), newPause.Order, newPause.TrainingId);
    }

    [ICommand]
    private async Task CreateExerciseTrainingAsync()
    {
        return;
    }
}
