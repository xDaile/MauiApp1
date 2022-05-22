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

    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private ExerciseTrainingModel newExerciseTraining;

    public CreateExerciseTrainingViewModel(ITrainingFacade trainingFacade)
    {
        this.TrainingFacade = trainingFacade;
        //Be aware that TrainingPlanID and Order are just temporary in the model, because they are not accessible during constructor
        newExerciseTraining = new ExerciseTrainingModel(null, new TimeSpan(), new TimeSpan(), 0, 0, 0,0, true, "Description", 0, Convert.ToInt32(TrainingId));
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


}
