using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class TrainingPlayView:ContentPageBase
{
	public TrainingPlayView(TrainingPlayViewModel trainingPlayViewModel)
		:base(trainingPlayViewModel)
	{
		InitializeComponent();
	}
}