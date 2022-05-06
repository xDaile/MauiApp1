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

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
public partial class CreateExerciseViewModel: ViewModelBase
{

    [ICommand]
    private async Task CreateExerciseAsync(String name)
    {
        Console.WriteLine(name);
        //createExercise
        await Shell.Current.GoToAsync("..");
        return;
    }
}
