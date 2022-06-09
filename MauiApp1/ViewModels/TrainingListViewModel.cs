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
[QueryProperty(nameof(Id), "Id")]
public partial class TrainingListViewModel : ViewModelBase
{
    private readonly IRoutingService routingService;
    public string? Id { private get; set; }
    private int id;

    [ObservableProperty]
    private TrainingPlanModel trainingPlan;

    //[ObservableProperty]
    //private List<TrainingListModel> trainings;

    public ITrainingFacade TrainingFacade;
    public ITrainingPlanFacade TrainingPlanFacade;

    public TrainingListViewModel(IRoutingService routingService, ITrainingFacade trainingFacade, ITrainingPlanFacade trainingPlanFacade)
    {
        this.TrainingFacade = trainingFacade;
        this.TrainingPlanFacade = trainingPlanFacade;
        this.routingService = routingService;
    }

    public override async Task OnAppearingAsync()
    {

        this.id = Convert.ToInt32(Id);
        await this.RefreshTrainingPlan();
        await base.OnAppearingAsync();
    }

    public async Task RefreshTrainingPlan()
    {
        TrainingPlan = await TrainingPlanFacade.GetById(this.id);
    }

    [ICommand]
    private async Task GoToDetailAsync(int id)
    {
        var route = routingService.GetRouteByViewModel<TrainingViewModel>();
       await Shell.Current.GoToAsync($"{route}?trainingId={id}");
    }

    [ICommand]
    private async Task AddNewAsync()
    {
        var route = routingService.GetRouteByViewModel<CreateTrainingViewModel>();
        int id = Convert.ToInt32(TrainingPlan.Id);
        //we need trainingPlanId because it is referenced int training
        await Shell.Current.GoToAsync($"{route}?trainingPlanId={id}");
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
    private async Task MoveTrainingDownAsync(int id)
    {
        await TrainingFacade.MoveTrainingDown(id);
        await RefreshTrainingPlan();
        return;
    }

    [ICommand]
    private async Task MoveTrainingUpAsync(int id)
    {
        await TrainingFacade.MoveTrainingUp(id);
        await RefreshTrainingPlan();
        return;
    }


    [ICommand]
    private async Task StartTrainingAsync(int id)
    {
        var route = routingService.GetRouteByViewModel<TrainingPlayViewModel>();
        await Shell.Current.GoToAsync($"{route}?trainingId={id}&Order=0");
        return;
    }
}
