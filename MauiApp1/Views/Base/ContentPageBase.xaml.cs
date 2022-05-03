using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public abstract partial class ContentPageBase : ContentPage
{
    protected IViewModel viewModel { get; }

    public ContentPageBase(IViewModel viewModel)
    {
        BindingContext = this.viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.OnAppearingAsync();
    }
}