using MauiApp1.ViewModels;
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

namespace MauiApp1.Views;

public partial class TrainingPlansView: ContentPageBase
{
	public TrainingPlansView(TrainingPlansViewModel trainingPlansViewModel)
		:base(trainingPlansViewModel)
	{
        InitializeComponent();
        BindingContext = trainingPlansViewModel;
		
	}

    public async void OnSupportTapped(object sender, EventArgs eventArgs)
    {
        string action = await DisplayActionSheet("Get Help", "Cancel", null, "Email", "Chat", "Phone");
        await DisplayAlert("You Chose", action, "Okay");

    }

}