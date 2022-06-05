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
[QueryProperty(nameof(ExerciseId), "exerciseId")]
public partial class EditExerciseTrainingViewModel : ViewModelBase
{

    public string TrainingId { private get; set; }
    public string ExerciseId { private get; set; }

    public ITrainingFacade TrainingFacade;
    public IExerciseFacade ExerciseFacade;

    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private ExerciseTrainingModel existingExerciseTraining = null;

    [ObservableProperty]
    private int exerciseIndex;

    [ObservableProperty]
    private List<ExerciseModel> exerciseList;

    public EditExerciseTrainingViewModel(ITrainingFacade trainingFacade, IExerciseFacade exerciseFacade)
    {
        this.TrainingFacade = trainingFacade;
        this.ExerciseFacade = exerciseFacade;
        ExerciseIndex = 0;
        ExistingExerciseTraining = new ExerciseTrainingModel(-1, TimeSpan.Zero, TimeSpan.Zero, 0, 0, 0, 0, false, "", 0, 0, "");
        this.OnAppearingAsync();
    }

    public override async Task OnAppearingAsync()
    {
        if (existingExerciseTraining.Id == -1)
        {
            ExerciseList = await ExerciseFacade.GetAll();

            TrainingModel training = await TrainingFacade.GetById(Convert.ToInt32(TrainingId));
            foreach (TrainingItemModel trainingItem in training.TrainingItems)
            {
                if (trainingItem.GetType().Equals(typeof(ExerciseTrainingModel)) && ((ExerciseTrainingModel)trainingItem).ExerciseId.Equals(Convert.ToInt32(this.ExerciseId)))
                {
                    this.ExistingExerciseTraining = (ExerciseTrainingModel)trainingItem;
                    break;
                }
            }
            ExerciseList = ExerciseList.OrderBy(x => x.Id).ToList();
            this.ExerciseIndex = this.ExistingExerciseTraining.ExerciseId;
        }
        await base.OnAppearingAsync();

    }

    [ICommand]
    private async Task EditExerciseTrainingAsync()
    {

        int trainingId = Convert.ToInt32(TrainingId);
        var order = await TrainingFacade.GetExistingTrainingItemsCount(trainingId);
        int ExerciseId = Convert.ToInt32(ExerciseList[ExerciseIndex].Id);
        ExerciseModel exercise = await ExerciseFacade.GetById(ExerciseId);
        ExerciseTrainingModel model = new ExerciseTrainingModel(
            existingExerciseTraining.Id,
            existingExerciseTraining.RestSeconds,
            existingExerciseTraining.ExerciseSeconds,
            existingExerciseTraining.Reps,
            existingExerciseTraining.Weight,
            existingExerciseTraining.Sets,
            order,
            existingExerciseTraining.RestAfterLastSet,
            existingExerciseTraining.Description,
            ExerciseId,
            trainingId,
            exercise.Name);
        if (model.ExerciseId == 0)
        {
            ErrorMessage = "You have to select exercise";
            return;
        }

        await TrainingFacade.UpdateTrainingItem(model);
        await Shell.Current.GoToAsync("..");
        return;
    }


    [ICommand]
    private async Task SetRepsForExistingExerciseTrainingPromptAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_reps, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, ExistingExerciseTraining.Reps.ToString());
        if (result.Equals(null)) return;
        int reps = Convert.ToInt32(result);
        ExistingExerciseTraining = existingExerciseTraining with { Reps = reps };
    }

    [ICommand]
    private async Task SetSetsForExistingExerciseTrainingPromptAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_sets, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, ExistingExerciseTraining.Sets.ToString());
        if (result.Equals(null)) return;
        int sets = Convert.ToInt32(result);
        ExistingExerciseTraining = existingExerciseTraining with { Sets = sets };
    }


    [ICommand]
    private async Task SetWeightForExistingExerciseTrainingPromptAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_weight, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, ExistingExerciseTraining.Weight.ToString());
        if (result.Equals(null)) return;
        Console.WriteLine(result);
        int weight = Convert.ToInt32(result);
        ExistingExerciseTraining = existingExerciseTraining with { Weight = weight };
    }

    [ICommand]
    private async Task SetRepDurationForExistingExerciseTrainingPromptAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_exercise_seconds, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, ExistingExerciseTraining.ExerciseSeconds.TotalSeconds.ToString());
        if (result.Equals(null)) return;
        int exerciseSeconds = Convert.ToInt32(result);

        ExistingExerciseTraining = existingExerciseTraining with { ExerciseSeconds = new TimeSpan(0, 0, exerciseSeconds) };
    }

    [ICommand]
    private async Task SetRestDurationForExistingExerciseTrainingPromptAsync()
    {
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_rest_seconds, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, ExistingExerciseTraining.RestSeconds.TotalSeconds.ToString());
        if (result.Equals(null)) return;
        int restSeconds = Convert.ToInt32(result);
        ExistingExerciseTraining = existingExerciseTraining with { RestSeconds = new TimeSpan(0, 0, restSeconds) };
    }

    [ICommand]
    private async Task AddWeightAsync()
    {
        ExistingExerciseTraining = existingExerciseTraining with { Weight = ExistingExerciseTraining.Weight + 1 };
    }

    [ICommand]
    private async Task RemoveWeightAsync()
    {
        if (ExistingExerciseTraining.Weight - 1 < 0) return;
        ExistingExerciseTraining = existingExerciseTraining with { Weight = ExistingExerciseTraining.Weight - 1 };
    }

    [ICommand]
    private async Task AddRepAsync()
    {
        ExistingExerciseTraining = existingExerciseTraining with { Reps = ExistingExerciseTraining.Reps + 1 };
    }

    [ICommand]
    private async Task RemoveRepAsync()
    {
        if (ExistingExerciseTraining.Reps - 1 < 0) return;
        ExistingExerciseTraining = existingExerciseTraining with { Reps = ExistingExerciseTraining.Reps - 1 };
    }

    [ICommand]
    private async Task AddSetAsync()
    {
        ExistingExerciseTraining = existingExerciseTraining with { Sets = ExistingExerciseTraining.Sets + 1 };
    }

    [ICommand]
    private async Task RemoveSetAsync()
    {
        if (ExistingExerciseTraining.Sets - 1 < 0) return;
        ExistingExerciseTraining = existingExerciseTraining with { Sets = ExistingExerciseTraining.Sets - 1 };
    }

}
