﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.ViewModels;

public abstract class ViewModelBase :INotifyPropertyChanged, IViewModel
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual Task OnAppearingAsync()
    {
        return Task.CompletedTask;
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}

