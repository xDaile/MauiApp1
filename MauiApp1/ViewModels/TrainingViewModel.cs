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

    [ObservableProperty]
    private List<TrainingItemModel> trainingItems;

    //[ObservableProperty]
    //private List<TrainingListModel> trainings;

    public ITrainingFacade TrainingFacade;
    public IExerciseFacade ExerciseFacade;

    public TrainingViewModel(IRoutingService routingService, ITrainingFacade trainingFacade, IExerciseFacade exerciseFacade)
    {
        this.TrainingFacade = trainingFacade;
        this.routingService = routingService;
        this.ExerciseFacade = exerciseFacade;
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
        
        TrainingItems = Training.TrainingItems.OrderBy(t => t.Order).ToList();
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
        var route = routingService.GetRouteByViewModel<CreatePauseViewModel>();
        await Shell.Current.GoToAsync($"{route}?trainingId={id}");

    }

    [ICommand]
    private async Task AddNewExerciseAsync()
    {
        var route = routingService.GetRouteByViewModel<CreateExerciseTrainingViewModel>();
        await Shell.Current.GoToAsync($"{route}?trainingId={id}");

    }

    [ICommand]
    private async Task ShowMenuTrainingItemAsync(int orderInTrainingItemsList)
    {
        string promptActionResult = await Shell.Current.DisplayActionSheet(Resources.Texts.Prompt_Training_options, Resources.Texts.Prompt_Cancel, null, Resources.Texts.Prompt_Edit, Resources.Texts.Prompt_Delete, Resources.Texts.Prompt_Create_copy);


        if (promptActionResult.Equals(Resources.Texts.Prompt_Edit))
        {
            if (TrainingItems[orderInTrainingItemsList].GetType().Equals(typeof(PauseModel)))
            {

                var route = routingService.GetRouteByViewModel<EditPauseViewModel>();
                await Shell.Current.GoToAsync($"{route}?pauseId={((PauseModel)TrainingItems[orderInTrainingItemsList]).Id}");
                return;
            }
            else
            {
                var route = routingService.GetRouteByViewModel<EditExerciseTrainingViewModel>();
                await Shell.Current.GoToAsync($"{route}?trainingId={((ExerciseTrainingModel)TrainingItems[orderInTrainingItemsList]).TrainingId}&exerciseId={((ExerciseTrainingModel)TrainingItems[orderInTrainingItemsList]).ExerciseId}");
                return;
            }
        }

        if (promptActionResult.Equals(Resources.Texts.Prompt_Delete))
        {
            Type TrainingItemType = TrainingItems[orderInTrainingItemsList].GetType();
            bool promptConfirmationResult;
            if (TrainingItemType.Equals(typeof(PauseModel)))
            {
                promptConfirmationResult = await Shell.Current.DisplayAlert($"{Resources.Texts.Prompt_Delete} {((PauseModel)TrainingItems[orderInTrainingItemsList]).Name} {Resources.Texts.Prompt_pause}", Resources.Texts.Prompt_Are_you_sure, Resources.Texts.Prompt_Delete, Resources.Texts.Prompt_Cancel);

            }
            else
            {
                ExerciseModel exercise = await ExerciseFacade.GetById(((ExerciseTrainingModel)TrainingItems[orderInTrainingItemsList]).ExerciseId);
                promptConfirmationResult = await Shell.Current.DisplayAlert($"{Resources.Texts.Prompt_Remove} {exercise.Name} {Resources.Texts.Prompt_exercise}", Resources.Texts.Prompt_Are_you_sure, Resources.Texts.Prompt_Delete, Resources.Texts.Prompt_Cancel);

            }

            if (promptConfirmationResult.Equals(true))
            {
                await TrainingFacade.DeleteTrainingItem(TrainingItems[orderInTrainingItemsList]);

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
