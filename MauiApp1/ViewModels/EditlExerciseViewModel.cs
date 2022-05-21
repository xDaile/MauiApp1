using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.BL.Facades.Interfaces;
using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
[QueryProperty(nameof(Id), "Id")]
public partial class EditExerciseViewModel:ViewModelBase
{
    public string? Id { private get; set; }

    public IExerciseFacade ExerciseFacade;

    [ObservableProperty]
    private ExerciseModel existingExercise;

    [ObservableProperty]
    private string errorMessage;

    public EditExerciseViewModel(IExerciseFacade exerciseFacade)
    {
        ExerciseFacade = exerciseFacade;
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        int id =Convert.ToInt32(Id);    
        ExistingExercise = await ExerciseFacade.GetById(id); 
    }

    [ICommand]
    private async Task SaveExerciseAsync()
    {
        if (existingExercise.Name.Length < 1)
        {
            ErrorMessage = "Name of the exercise is too short";
            return;
        }
        await ExerciseFacade.Update(existingExercise);
        await Shell.Current.GoToAsync("..");
        return;
    }

    [ICommand]
    private async Task DeleteExerciseAsync()
    {
        await ExerciseFacade.Delete(existingExercise);
        await Shell.Current.GoToAsync("..");
        return;
    }
}
