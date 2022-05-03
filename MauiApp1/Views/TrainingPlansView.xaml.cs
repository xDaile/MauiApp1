using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class TrainingPlansView
{
	public TrainingPlansView(TrainingPlansViewModel trainingPlansViewModel)
		:base(trainingPlansViewModel)
	{
		InitializeComponent();
	}
}