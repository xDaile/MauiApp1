using MauiApp1.Models;
using MauiApp1.ViewModels;

namespace MauiApp1.Services;

public interface IRoutingService
{
    ICollection<RouteModel> Routes { get; }

    string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel;
}