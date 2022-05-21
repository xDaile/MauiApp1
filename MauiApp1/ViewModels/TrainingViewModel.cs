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
[QueryProperty(nameof(Id), "trainingId")]
public partial class TrainingViewModel : ViewModelBase
{
    private readonly IRoutingService routingService;
    public string? Id { private get; set; }
    private int id;

    [ObservableProperty]
    private TrainingModel training;

    //[ObservableProperty]
    //private List<TrainingListModel> trainings;

    public ITrainingFacade TrainingFacade;

    public TrainingViewModel(IRoutingService routingService, ITrainingFacade trainingFacade)
    {
        this.TrainingFacade = trainingFacade;
        this.routingService = routingService;
    }

    public override async Task OnAppearingAsync()
    {

        this.id = Convert.ToInt32(Id);
        await this.RefreshTraining();
        await base.OnAppearingAsync();
    }

    public async Task RefreshTraining()
    {
        Training = await TrainingFacade.GetById(this.id);
    }

    [ICommand]
    private async Task GoToDetailAsync(int id)
    {
        //var route = routingService.GetRouteByViewModel<TrainingViewModel>();
        //await Shell.Current.GoToAsync($"{route}?trainingId={id}");
    }

    [ICommand]
    private async Task AddNewPauseAsync()
    {
        
        return;
    }

    [ICommand]
    private async Task AddNewExerciseAsync()
    {
        //we need trainingPlanId because it is referenced int training
        return;
    }

    [ICommand]
    private async Task ShowMenuTrainingAsync(int id)
    {
        string promptActionResult = await Shell.Current.DisplayActionSheet(Resources.Texts.Prompt_Training_options, Resources.Texts.Prompt_Cancel, null, Resources.Texts.Prompt_Edit, Resources.Texts.Prompt_Delete, Resources.Texts.Prompt_Create_copy);


        if (promptActionResult.Equals(Resources.Texts.Prompt_Edit))
        {
            var route = routingService.GetRouteByViewModel<EditTrainingViewModel>();
            await Shell.Current.GoToAsync($"{route}?trainingId={id}");
            return;
        }

        if (promptActionResult.Equals(Resources.Texts.Prompt_Delete))
        {
            TrainingListModel selectedTraining = await TrainingFacade.GetByIdLM(id);
            bool promptConfirmationResult = await Shell.Current.DisplayAlert($"{Resources.Texts.Prompt_Delete} {selectedTraining.Name} {Resources.Texts.Prompt_training}", Resources.Texts.Prompt_Are_you_sure, Resources.Texts.Prompt_Delete, Resources.Texts.Prompt_Cancel);
            if (promptConfirmationResult.Equals(true))
            {
                await TrainingFacade.DeleteLM(selectedTraining);
            }
            await this.OnAppearingAsync();
            return;
        }
        if (promptActionResult.Equals(Resources.Texts.Prompt_Create_copy))
        {
            return;
        }

    }

    [ICommand]
    private async Task MoveTrainingItemDownAsync(int id)
    {
       // await TrainingFacade.MoveTrainingDown(id);
       // await RefreshTrainingPlan();
        return;
    }

    [ICommand]
    private async Task MoveTrainingItemUpAsync(int id)
    {
       // await TrainingFacade.MoveTrainingUp(id);
       // await RefreshTrainingPlan();
        return;
    }
}
