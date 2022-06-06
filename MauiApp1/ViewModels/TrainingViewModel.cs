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

    private List<ExerciseModel> exercises;

    //[ObservableProperty]
    //private List<TrainingListModel> trainings;

    public ITrainingFacade TrainingFacade;
    public IExerciseFacade ExerciseFacade;
    public IPauseFacade PauseFacade;

    public TrainingViewModel(IRoutingService routingService, ITrainingFacade trainingFacade, IExerciseFacade exerciseFacade, IPauseFacade pauseFacade)
    {
        this.TrainingFacade = trainingFacade;
        this.routingService = routingService;
        this.ExerciseFacade = exerciseFacade;
        this.PauseFacade = pauseFacade;
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
        string promptActionResult = await Shell.Current.DisplayActionSheet(Resources.Texts.Prompt_Training_item_options, Resources.Texts.Prompt_Cancel, null, Resources.Texts.Prompt_Edit, Resources.Texts.Prompt_Delete, Resources.Texts.Prompt_Create_copy);


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
                if (promptConfirmationResult.Equals(false)) return;
                await TrainingFacade.DeleteTrainingItem(trainingItems[orderInTrainingItemsList]);
                await this.OnAppearingAsync();
                return;
            }
            else
            {
                string exerciseName = ((ExerciseTrainingModel)TrainingItems[orderInTrainingItemsList]).ExerciseName;
                promptConfirmationResult = await Shell.Current.DisplayAlert($"{Resources.Texts.Prompt_Remove} {exerciseName} {Resources.Texts.Prompt_exercise}", Resources.Texts.Prompt_Are_you_sure, Resources.Texts.Prompt_Delete, Resources.Texts.Prompt_Cancel);
                if (promptConfirmationResult.Equals(false)) return;
                await TrainingFacade.DeleteTrainingItem(trainingItems[orderInTrainingItemsList]);
                await this.OnAppearingAsync();
                return;
            }

            
           
        }
        if (promptActionResult.Equals(Resources.Texts.Prompt_Create_copy))
        {
            return;
        }

    }

    [ICommand]
    private async Task MoveTrainingItemDownAsync(int order)
    {
        await TrainingFacade.MoveTrainingItemDown(trainingItems[order]);
        this.OnAppearingAsync();
        return;
    }

    [ICommand]
    private async Task MoveTrainingItemUpAsync(int order)
    {
        await TrainingFacade.MoveTrainingItemUp(trainingItems[order]);
        this.OnAppearingAsync();
        return;
    }

    [ICommand]
    private async Task GetPauseSecondsForListPausePromptAsync(int id)
    {
        PauseModel existingPauseModel = await PauseFacade.GetById(id);
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_pause_duration, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, existingPauseModel.Duration.TotalSeconds.ToString());
        if (result.Equals(null)) return;
        TimeSpan newDuration = new TimeSpan(0, 0, Convert.ToInt32(result));
        existingPauseModel = existingPauseModel with { Duration = newDuration };
        await PauseFacade.Update(existingPauseModel);
        this.OnAppearingAsync();
    }

    [ICommand]
    private async Task GetWorkSecondsForListExerciseTrainingPromptAsync(int order)
    {
        ExerciseTrainingModel existingExerciseTrainingModel = (ExerciseTrainingModel)trainingItems.Find(x=>x.Order.Equals(order));
        
        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_rep_duration, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, existingExerciseTrainingModel.ExerciseSeconds.TotalSeconds.ToString());
        if (result.Equals(null)) return;
        TimeSpan newDuration = new TimeSpan(0, 0, Convert.ToInt32(result));
        existingExerciseTrainingModel = existingExerciseTrainingModel with { ExerciseSeconds = newDuration };
        await TrainingFacade.UpdateTrainingItem(existingExerciseTrainingModel);
        this.OnAppearingAsync();
    }

    [ICommand]
    private async Task GetRestSecondsForListExerciseTrainingPromptAsync(int order)
    {
        ExerciseTrainingModel existingExerciseTrainingModel = (ExerciseTrainingModel)trainingItems.Find(x => x.Order.Equals(order));

        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_rest_duration, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, existingExerciseTrainingModel.ExerciseSeconds.TotalSeconds.ToString());
        if (result.Equals(null)) return;
        TimeSpan newDuration = new TimeSpan(0, 0, Convert.ToInt32(result));
        existingExerciseTrainingModel = existingExerciseTrainingModel with { RestSeconds = newDuration };
        await TrainingFacade.UpdateTrainingItem(existingExerciseTrainingModel);
        this.OnAppearingAsync();
    }

    [ICommand]
    private async Task GetSetsForListExerciseTrainingPromptAsync(int order)
    {
        ExerciseTrainingModel existingExerciseTrainingModel = (ExerciseTrainingModel)trainingItems.Find(x => x.Order.Equals(order));

        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_sets, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, existingExerciseTrainingModel.Sets.ToString());
        if (result.Equals(null)) return;
        existingExerciseTrainingModel = existingExerciseTrainingModel with { Sets = Convert.ToInt32(result) };
        await TrainingFacade.UpdateTrainingItem(existingExerciseTrainingModel);
        this.OnAppearingAsync();
    }

    [ICommand]
    private async Task GetRepsForListExerciseTrainingPromptAsync(int order)
    {
        ExerciseTrainingModel existingExerciseTrainingModel = (ExerciseTrainingModel)trainingItems.Find(x => x.Order.Equals(order));

        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_reps, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 3, null, existingExerciseTrainingModel.Reps.ToString());
        if (result.Equals(null)) return;
        existingExerciseTrainingModel = existingExerciseTrainingModel with { Reps = Convert.ToInt32(result) };
        await TrainingFacade.UpdateTrainingItem(existingExerciseTrainingModel);
        this.OnAppearingAsync();
    }

    [ICommand]
    private async Task GetWeightForListExerciseTrainingPromptAsync(int order)
    {
        ExerciseTrainingModel existingExerciseTrainingModel = (ExerciseTrainingModel)trainingItems.Find(x => x.Order.Equals(order));

        var result = await Shell.Current.DisplayPromptAsync(Resources.Texts.Enter_weight, "", Resources.Texts.Prompt_confirm, Resources.Texts.Prompt_Cancel, null, 7, null, existingExerciseTrainingModel.Weight.ToString());
        if (result.Equals(null)) return;
        float resultValue = float.Parse(result.Replace(".",","));
        existingExerciseTrainingModel = existingExerciseTrainingModel with { Weight = resultValue };
        await TrainingFacade.UpdateTrainingItem(existingExerciseTrainingModel);
        this.OnAppearingAsync();
    }

    [ICommand]
    private async Task ChangeLastPauseForListExerciseTrainingPromptAsync(int order)
    {
        ExerciseTrainingModel existingExerciseTrainingModel = (ExerciseTrainingModel)trainingItems.Find(x => x.Order.Equals(order));
        existingExerciseTrainingModel = existingExerciseTrainingModel with { RestAfterLastSet = !(existingExerciseTrainingModel.RestAfterLastSet) };
        await TrainingFacade.UpdateTrainingItem(existingExerciseTrainingModel);
        this.OnAppearingAsync();
    }



}