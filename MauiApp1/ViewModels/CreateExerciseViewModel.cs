using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using MauiApp1.BL.Facades.Interfaces;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
public partial class CreateExerciseViewModel : ViewModelBase
{
    [ObservableProperty]
    private ExerciseModel newExercise;

    public IExerciseFacade ExerciseFacade;

    [ObservableProperty]
    private string errorMessage;

    public CreateExerciseViewModel(IExerciseFacade exerciseFacade)
    {
        //constructor bug TODO
        ExerciseFacade = exerciseFacade;
        NewExercise = new ExerciseModel(null, "", "");
        ErrorMessage = "";

    }

    [ICommand]
    private async Task CreateExerciseAsync()
    {
        if (newExercise.Name.Length < 1)
        {
            ErrorMessage = "Name of exercise is too short";
            return;
        }
        var result = await ExerciseFacade.Create(NewExercise);
        await Shell.Current.GoToAsync("..");
        return;
    }
}
