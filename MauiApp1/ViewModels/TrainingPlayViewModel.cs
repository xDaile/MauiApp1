using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    private bool paused = false;
    private bool waitingForStart = true;
    private int order=-1;

    private int minutes = 0;
    private int seconds = 0;
    public Stopwatch stopwatch = new();
    private bool isRunning;
    private TimeSpan Remaining;

    [ObservableProperty]
    private TrainingModel? training;

    [ObservableProperty]
    private string remainingTime;

    [ObservableProperty]
    private double soundLevel;

    [ObservableProperty]
    private string currentPlayStopImage = "/Images/play.png";

    [ObservableProperty]
    private string nextInRow = "Next: BenchPRess";

    [ObservableProperty]
    private string currentStatus;

    [ObservableProperty]
    private string currentColor = "Orange";


    [ObservableProperty]
    private List<TrainingItemModel> trainingCurrentItem;

    [ObservableProperty]
    private string timer = "00:00";


    [ObservableProperty]
    private List<TrainingItemModel> trainingItems;

    public ITrainingFacade TrainingFacade;

    public TrainingPlayViewModel(IRoutingService routingService, ITrainingFacade trainingFacade)
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
        if (Training==null)
        {
            Training = await TrainingFacade.GetById(this.id);


            TrainingItems = Training.TrainingItems.OrderBy(t => t.Order).ToList();
        }
        if (order.Equals(-1))
        {
           order = Convert.ToInt32(Order);
        }
        TrainingCurrentItem = new List<TrainingItemModel>();
        
        TrainingCurrentItem.Add(TrainingItems[order]);
        SetupStatus();
        
    }

    public async Task SetupStatus()
    {
        TrainingItemModel currentModel = TrainingCurrentItem[0];
        if (currentModel.GetType().Equals(typeof(PauseModel)))
        {
            CurrentStatus = "Pause";
            CurrentColor = "Orange";
            RemainingTime = StopWatchFormatTime(((PauseModel)currentModel).Duration);
            double totalMinutes = ((PauseModel)TrainingCurrentItem[0]).Duration.TotalMinutes;
            CreateStopWatch(totalMinutes);
        }
    }

    public async Task StartNewTimer()
    {
        
        if (CurrentStatus.Equals("Pause"))
        {
           
            
            StopWatchStart();

        }
        else if (CurrentStatus.Equals("Rest"))
        {
            int totalSeconds = Convert.ToInt32(((ExerciseTrainingModel)TrainingCurrentItem[0]).RestSeconds.TotalSeconds);
            minutes = totalSeconds % 60;
            seconds = totalSeconds - minutes * 60;
            Timer = String.Format("{0}:{1}", minutes, seconds);
        }
        else
        {
            int totalSeconds = Convert.ToInt32(((ExerciseTrainingModel)TrainingCurrentItem[0]).ExerciseSeconds.TotalSeconds);
            minutes = totalSeconds % 60;
            seconds = totalSeconds - minutes * 60;
            Timer = String.Format("{0}:{1}", minutes, seconds);
        }
    }

    [ICommand]
    private async Task PauseStartAsync()
    {
        //We want to start stopwatch
        if (paused)
        {
            isRunning = true;
            StopWatchStart();
            this.CurrentPlayStopImage = "/Images/pause.png"; }
        else{ this.CurrentPlayStopImage = "/Images/play.png";
            StopWatchStop();
        }
        this.paused = !paused;
        await this.OnAppearingAsync();

        return;
        //TODO STOP/START TIMER
    }

    [ICommand]
    private async Task GoToPreviousItemAsync()
    {
        if (order -1 <0) return;
        order = order - 1;
        TrainingCurrentItem = new List<TrainingItemModel>();
        TrainingCurrentItem.Add(TrainingItems[order]);
        await this.OnAppearingAsync();
    }

    [ICommand]
    private async Task GoToNextItemAsync()
    {
        if (order + 1 == TrainingItems.Count()) return;
        order = order + 1;
        TrainingCurrentItem = new List<TrainingItemModel>();
        TrainingCurrentItem.Add(TrainingItems[order]);
        await this.OnAppearingAsync();
    }

    [ICommand]
    private async Task ExitTrainingSessionAsync()
    {
    }

    public void CreateStopWatch(double totalMinutes)
    {
        int minutes = Convert.ToInt32(Math.Floor(totalMinutes));
        double restOfMinute = totalMinutes - minutes;
        int seconds = Convert.ToInt32(Math.Floor(restOfMinute * 60));
        //string q = String.Format("00:{0}:{1}", minutes.ToString(), seconds.ToString());
        Remaining = new TimeSpan(0, minutes, seconds);
        this.RemainingTime = StopWatchFormatTime(Remaining);
        this.isRunning = false;
    }

    public void StopWatchStart()
    {
        stopwatch.Restart();

        isRunning = true;

        Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if(isRunning)
                RemainingTime = StopWatchFormatTime(this.Remaining - stopwatch.Elapsed);
            });
            return isRunning;
        });
    }
    public void StopWatchStop()
    {
        stopwatch.Stop();
        isRunning = false;
    }
    private string StopWatchFormatTime(TimeSpan timeSpan)
    {
        return timeSpan.ToString(@"mm\:ss");
    }

}