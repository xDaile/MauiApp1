using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class TrainingListView:ContentPageBase
{
	public TrainingListView(TrainingListViewModel trainingListViewModel)
		:base(trainingListViewModel)
	{
		BindingContext = trainingListViewModel;
		InitializeComponent();
	}
}