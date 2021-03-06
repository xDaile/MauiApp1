using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;
using MauiApp1.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace MauiApp1.ViewModels;

[INotifyPropertyChanged]
public partial class SettingsViewModel : ViewModelBase
{
    private readonly IRoutingService routingService;

    public SettingsViewModel(IRoutingService routingService)
    {
        this.routingService = routingService;
    }
}
