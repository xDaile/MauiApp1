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
    private int exerciseIndex;

    [ObservableProperty]
    private ExerciseTrainingModel newExerciseTraining;

    [ObservableProperty]
    private TimeSpan duration;

    public CreateExerciseTrainingViewModel(ITrainingFacade trainingFacade, IExerciseFacade exerciseFacade)
    {
        this.TrainingFacade = trainingFacade;
        this.ExerciseFacade = exerciseFacade;
        ExerciseIndex = 0;
        //Be aware that TrainingPlanID and Order are just temporary in the model, because they are not accessible during constructor
        newExerciseTraining = new ExerciseTrainingModel(null, new TimeSpan(), new TimeSpan(), 0, 0, 0, 0, true, "Description", 0, 0,null);
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        this.ExerciseList = await ExerciseFacade.GetAll();
    }

   


    [ICommand]
    private async Task SetRepsForNewExerciseTrainingPromptAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_reps, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, NewExerciseTraining.Reps.ToString());
        if (result.Equals(null)) return;
        int reps = Convert.ToInt32(result);
        NewExerciseTraining = NewExerciseTraining with { Reps = reps };
    }

    [ICommand]
    private async Task SetSetsForNewExerciseTrainingPromptAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_sets, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, NewExerciseTraining.Sets.ToString());
        if (result.Equals(null)) return;
        int sets = Convert.ToInt32(result);
        NewExerciseTraining = NewExerciseTraining with { Sets = sets };
    }


    [ICommand]
    private async Task SetWeightForNewExerciseTrainingPromptAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_weight, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, NewExerciseTraining.Weight.ToString());
        if (result.Equals(null)) return;
        Console.WriteLine(result);
        int weight = Convert.ToInt32(result);
        NewExerciseTraining = NewExerciseTraining with { Weight = weight };
    }

    [ICommand]
    private async Task SetRepDurationForNewExerciseTrainingPromptAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_exercise_seconds, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, NewExerciseTraining.ExerciseSeconds.TotalSeconds.ToString());
        if (result.Equals(null)) return;
        int exerciseSeconds = Convert.ToInt32(result);

        NewExerciseTraining = NewExerciseTraining with { ExerciseSeconds = new TimeSpan(0, 0, exerciseSeconds) };
    }

    [ICommand]
    private async Task SetRestDurationForNewExerciseTrainingPromptAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_rest_seconds, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, NewExerciseTraining.RestSeconds.TotalSeconds.ToString());
        if (result.Equals(null)) return;
        int restSeconds = Convert.ToInt32(result);
        NewExerciseTraining = NewExerciseTraining with { RestSeconds = new TimeSpan(0, 0, restSeconds) };
    }

    [ICommand]
    private async Task AddWeightAsync()
    {
        NewExerciseTraining = NewExerciseTraining with { Weight = NewExerciseTraining.Weight+1 };
    }

    [ICommand]
    private async Task RemoveWeightAsync()
    {
        if (NewExerciseTraining.Weight - 1 < 0) return;
        NewExerciseTraining = NewExerciseTraining with { Weight = NewExerciseTraining.Weight - 1 };
    }

    [ICommand]
    private async Task AddRepAsync()
    {
        NewExerciseTraining = NewExerciseTraining with { Reps = NewExerciseTraining.Reps + 1 };
    }

    [ICommand]
    private async Task RemoveRepAsync()
    {
        if (NewExerciseTraining.Reps - 1 < 0) return;
        NewExerciseTraining = NewExerciseTraining with { Reps = NewExerciseTraining.Reps - 1 };
    }

    [ICommand]
    private async Task AddSetAsync()
    {
        NewExerciseTraining = NewExerciseTraining with { Sets = NewExerciseTraining.Sets + 1 };
    }

    [ICommand]
    private async Task RemoveSetAsync()
    {
        if (NewExerciseTraining.Sets - 1 < 0) return;
        NewExerciseTraining = NewExerciseTraining with { Sets = NewExerciseTraining.Sets - 1 };
    }

    [ICommand]
    private async Task CreateExerciseTrainingAsync()
    {
        int trainingId = Convert.ToInt32(TrainingId);
        var order = await TrainingFacade.GetExistingTrainingItemsCount(trainingId);
        if (ExerciseIndex < 1)
        {
            ErrorMessage = "You have to pick some exercise";
            return;
        }
        ExerciseTrainingModel model = new ExerciseTrainingModel(null, 
            NewExerciseTraining.RestSeconds,
            NewExerciseTraining.ExerciseSeconds, 
            NewExerciseTraining.Reps, 
            NewExerciseTraining.Weight, 
            NewExerciseTraining.Sets,  
            order, 
            NewExerciseTraining.RestAfterLastSet, 
            NewExerciseTraining.Description,
            ExerciseIndex,
            trainingId,
            exerciseList[ExerciseIndex].Name);
       

        await TrainingFacade.CreateTrainingItem(model);
        await Shell.Current.GoToAsync("..");
        return;
    }


}
