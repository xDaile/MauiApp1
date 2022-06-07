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
[QueryProperty(nameof(Order), "Order")]

public partial class TrainingPlayViewModel : ViewModelBase
{
    private readonly IRoutingService routingService;
    public string? Id { private get; set; }
    public string? Order { private get; set; }
    private int id;

    [ObservableProperty]
    private TrainingModel training;

    [ObservableProperty]
    private double soundLevel;

    [ObservableProperty]
    private string currentPlayStopImage = "/Images/play.png";

    [ObservableProperty]
    private string nextInRow="Next: BenchPRess";

    [ObservableProperty]
    private string currentStatus="Pause";


    [ObservableProperty]
    private List<TrainingItemModel> trainingCurrentItem;

    [ObservableProperty]
    private string timer = "00:15";

    [ObservableProperty]
    private List<TrainingItemModel> trainingItems;

    private List<ExerciseModel> exercises;

    //[ObservableProperty]
    //private List<TrainingListModel> trainings;

    public ITrainingFacade TrainingFacade;
    public IExerciseFacade ExerciseFacade;
    public IPauseFacade PauseFacade;

    public TrainingPlayViewModel(IRoutingService routingService, ITrainingFacade trainingFacade, IExerciseFacade exerciseFacade, IPauseFacade pauseFacade)
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
    private async Task ShowMenuTrainingItemAsync(int orderInTrainingItemsList)
    {
        string promptActionResult = await Shell.Current.DisplayActionSheet(Resources.Texts.Prompt_Training_item_options, Resources.Texts.Prompt_Cancel, null, Resources.Texts.Prompt_Edit, Resources.Texts.Prompt_Delete, Resources.Texts.Prompt_Create_copy);


        if (promptActionResult.Equals(Resources.Texts.Prompt_Edit))
        {
            if (TrainingItems[orderInTrainingItemsList].GetType().Equals(typeof(PauseModel)))
            {

                var route = routingService.GetRouteByViewModel<EditPauseViewModel>();
               // await Shell.Current.GoToAsync($"{route}?pauseId={((PauseModel)TrainingItems[orderInTrainingItemsList]).Id}");
                return;
            }
            else
            {
               // var route = routingService.GetRouteByViewModel<EditExerciseTrainingPlayViewModel>();
               // await Shell.Current.GoToAsync($"{route}?trainingId={((ExerciseTrainingModel)TrainingItems[orderInTrainingItemsList]).TrainingId}&exerciseId={((ExerciseTrainingModel)TrainingItems[orderInTrainingItemsList]).ExerciseId}");
                return;
            }
        }
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
    private async Task PauseStartAsync(int order)
    {
    }

    [ICommand]
    private async Task GoToPreviousAsync(int order)
    {
    }

    [ICommand]
    private async Task GoToNextAsync(int order)
    {
    }

    [ICommand]
    private async Task ExitTrainingSessionAsync(int order)
    {
    }
}