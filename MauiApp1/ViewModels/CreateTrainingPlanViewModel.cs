using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
public partial class CreateTrainingPlanViewModel:ViewModelBase
{

    [ICommand]
    private async Task CreateTrainingPlanAsync(String name)
    {
        Console.WriteLine(name);
        //createTrainingPlan
        await Shell.Current.GoToAsync("..");
        return;
    }
}
