using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Runtime.CompilerServices;

namespace MauiApp1.ViewModels;

public abstract partial class ViewModelBase: IViewModel
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual Task OnAppearingAsync()
    {
        return Task.CompletedTask;
    }

    /*
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    */

    [ICommand]
    private async Task GoBackAsync()
    {
        if (Shell.Current.Navigation.NavigationStack.Count > 1)
        {
            Shell.Current.SendBackButtonPressed();
        }
    }

}

