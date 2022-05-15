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
public partial class CreateExerciseViewModel: ViewModelBase
{
    [ObservableProperty]
    private ExerciseModel newExercise;

    public IExerciseFacade ExerciseFacade { get; }

    public CreateExerciseViewModel(IExerciseFacade exerciseFacade)
    {
        //constructor bug TODO
        ExerciseFacade = exerciseFacade;
        newExercise = new ExerciseModel(null,"","");

    }

    [ICommand]
    private async Task CreateExerciseAsync()
    {
        ExerciseFacade.Create(newExercise);
        //createExercise
        await Shell.Current.GoToAsync("..");
        return;
    }
}
