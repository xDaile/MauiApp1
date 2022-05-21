using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MauiApp1.BL.Facades.Interfaces;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
public partial class ExerciseListViewModel : ViewModelBase
{

    private readonly IRoutingService routingService;

    public IExerciseFacade ExerciseFacade;

    [ObservableProperty]
    private IList<ExerciseModel>? exercises;

    public ExerciseListViewModel(IRoutingService routingService, IExerciseFacade exerciseFacade)
    {
        this.routingService = routingService;
        this.ExerciseFacade = exerciseFacade;
    }

    public override async Task OnAppearingAsync()
    {
        //Exercises = SeedExercises();
        await base.OnAppearingAsync();
        Exercises = await ExerciseFacade.GetAll();
    }

    [ICommand]
    private async Task ShowMenuExerciseAsync(int id)
    {
        //var route = routingService.GetRouteByViewModel<EditExerciseViewModel>();
        //await Shell.Current.GoToAsync($"{route}?id={id}");

        string promptActionResult = await Shell.Current.DisplayActionSheet(Resources.Texts.Prompt_Exercise_options, Resources.Texts.Prompt_Cancel, null, Resources.Texts.Prompt_Edit, Resources.Texts.Prompt_Delete, Resources.Texts.Prompt_Create_copy);


        if (promptActionResult.Equals(Resources.Texts.Prompt_Edit))
        {
            var route = routingService.GetRouteByViewModel<EditExerciseViewModel>();
            await Shell.Current.GoToAsync($"{route}?Id={id}");
            return;
        }

        if (promptActionResult.Equals(Resources.Texts.Prompt_Delete))
        {
            ExerciseModel selectedExercise = await ExerciseFacade.GetById(id);
            bool promptConfirmationResult = await Shell.Current.DisplayAlert($"{Resources.Texts.Prompt_Delete} {selectedExercise.Name} {Resources.Texts.Prompt_exercise}", Resources.Texts.Prompt_Are_you_sure, Resources.Texts.Prompt_Delete, Resources.Texts.Prompt_Cancel);
            if (promptConfirmationResult.Equals(true))
            {
                await ExerciseFacade.Delete(selectedExercise);
            }
            await this.OnAppearingAsync();
            return;
        }
        if (promptActionResult.Equals(Resources.Texts.Prompt_Create_copy))
        {
            return;
        }


    }


    //Navigate to page for adding new exercise
    [ICommand]
    private async Task AddNewAsync()
    {
        var route = routingService.GetRouteByViewModel<CreateExerciseViewModel>();
        await Shell.Current.GoToAsync($"{route}");
        return;
    }
}
