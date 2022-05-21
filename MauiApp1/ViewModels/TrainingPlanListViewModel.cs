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
using MauiApp1.BL.Facades;


using MauiApp1.BL.Facades.Interfaces;


namespace MauiApp1.ViewModels;


[INotifyPropertyChanged]
public partial class TrainingPlanListViewModel : ViewModelBase
{
    private readonly IRoutingService routingService;

    [ObservableProperty]
    private IList<TrainingPlanListModel>? trainingPlans;

    public ITrainingPlanFacade TrainingPlanFacade;




    public TrainingPlanListViewModel(IRoutingService routingService, ITrainingPlanFacade trainingPlanFacade)
    {
        this.routingService = routingService;
        this.TrainingPlanFacade = trainingPlanFacade;
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();
        // TrainingPlans = SeedTrainingPlans();
        TrainingPlans = await TrainingPlanFacade.GetAllLM();
    }

    [ICommand]
    private async Task GoToDetailAsync(int id)
    {
        var route = routingService.GetRouteByViewModel<TrainingListViewModel>();
        try
        {
            await Shell.Current.GoToAsync($"{route}?Id={id}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return;
    }

    [ICommand]
    private async Task AddNewAsync()
    {
        var route = routingService.GetRouteByViewModel<CreateTrainingPlanViewModel>();
        await Shell.Current.GoToAsync($"{route}");
        return;
    }

    [ICommand]
    private async Task ShowMenuAsync(int id)
    {
        string promptActionResult = await Shell.Current.DisplayActionSheet(Resources.Texts.Prompt_Training_plan_options, Resources.Texts.Prompt_Cancel, null, Resources.Texts.Prompt_Edit, Resources.Texts.Prompt_Delete, Resources.Texts.Prompt_Create_copy);
        if (promptActionResult.Equals(Resources.Texts.Prompt_Edit))
        {
            var route = routingService.GetRouteByViewModel<EditTrainingPlanViewModel>();
            await Shell.Current.GoToAsync($"{route}?Id={id}");
            return;
        }

        if (promptActionResult.Equals(Resources.Texts.Prompt_Delete))
        {
            TrainingPlanListModel selectedTrainingPlan = await TrainingPlanFacade.GetByIdLM(id);
            bool promptConfirmationResult = await Shell.Current.DisplayAlert($"{Resources.Texts.Prompt_Delete} {selectedTrainingPlan.Name} {Resources.Texts.Prompt_training_plan_}", Resources.Texts.Prompt_Are_you_sure, Resources.Texts.Prompt_Delete, Resources.Texts.Prompt_Cancel);
            if (promptConfirmationResult.Equals(true))
            {
                await TrainingPlanFacade.DeleteLM(selectedTrainingPlan);
            }
            await this.OnAppearingAsync();
            return;
        }
        if (promptActionResult.Equals(Resources.Texts.Prompt_Create_copy))
        {
            return;
        }

        /*case "Change name":
         {
             TrainingPlanListModel selectedTrainingPlan = await TrainingPlanFacade.GetByIdLM(id);
             string newName = await Shell.Current.DisplayPromptAsync($"Edit {selectedTrainingPlan.Name} training", "Write new name", "OK", "Cancel", selectedTrainingPlan.Name, 75);
             TrainingPlanListModel updatedTrainingPlan = new TrainingPlanListModel(selectedTrainingPlan.Id,newName,selectedTrainingPlan.Description);
             await TrainingPlanFacade.UpdateLM(updatedTrainingPlan);
             await this.OnAppearingAsync();
             return;
         }
     case "Change description":
         {
             TrainingPlanListModel selectedTrainingPlan = await TrainingPlanFacade.GetByIdLM(id);
             string newDescription = await Shell.Current.DisplayPromptAsync($"Edit {selectedTrainingPlan.Name} training", "Write new description", "OK", "Cancel", selectedTrainingPlan.Description, 100);
             TrainingPlanListModel updatedTrainingPlan = new TrainingPlanListModel(selectedTrainingPlan.Id, selectedTrainingPlan.Name, newDescription);
             await TrainingPlanFacade.UpdateLM(updatedTrainingPlan);
             await this.OnAppearingAsync();
             return;
         }
     */
    }

}

