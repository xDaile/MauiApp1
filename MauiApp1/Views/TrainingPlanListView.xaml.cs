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

public partial class TrainingPlanListView: ContentPageBase
{
	public TrainingPlanListView(TrainingPlanListViewModel TrainingPlanListViewModel)
		:base(TrainingPlanListViewModel)
	{
        InitializeComponent();
        BindingContext = TrainingPlanListViewModel;
		
	}

}