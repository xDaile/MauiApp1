using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class TrainingListView
{
	public TrainingListView(TrainingListViewModel trainingListViewModel)
		:base(trainingListViewModel)
	{
		InitializeComponent();
	}
}