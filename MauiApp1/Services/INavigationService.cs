using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.ViewModels;

namespace MauiApp1.Services
{
    public interface INavigationService
    {
        Task PushAsync<TViewModel>(bool animated = default, bool clearHistory = default)
            where TViewModel : class, IViewModel;
/*
        Task PushAsync<TViewModel, TViewModelParameter>(
            TViewModelParameter? viewModelParameter = default, bool animated = default, bool clearHistory = default)
            where TViewModel : class, IViewModel<TViewModelParameter>;
*/
        Task PopAsync(bool animated = default);
        Task PopToRootAsync(bool animated = default);
    }

}
