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
[QueryProperty(nameof(Id), "trainingId")]
public partial class EditTrainingViewModel : ViewModelBase
{
    public string? Id { private get; set; }

    public ITrainingFacade TrainingFacade;

    [ObservableProperty]
    private TrainingListModel existingTraining;

    [ObservableProperty]
    private string errorMessage;

    public EditTrainingViewModel(ITrainingFacade trainingFacade)
    {
        TrainingFacade = trainingFacade;
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        int id = Convert.ToInt32(Id);
        ExistingTraining = await TrainingFacade.GetByIdLM(id);
    }

    [ICommand]
    private async Task SaveTrainingAsync()
    {
        if (existingTraining.Name.Length < 1)
        {
            ErrorMessage = "Name of the training is too short";
            return;
        }
        await TrainingFacade.UpdateLM(existingTraining);
        await Shell.Current.GoToAsync("..");
        return;
    }

    //TODO remove descendants if there are some
    [ICommand]
    private async Task DeleteTrainingAsync()
    {
        await TrainingFacade.DeleteLM(existingTraining);
        await Shell.Current.GoToAsync("..");
        return;
    }
}
