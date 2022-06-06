using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class TrainingView:ContentPageBase
{
	public TrainingView(TrainingViewModel trainingViewModel)
		:base(trainingViewModel)
	{
		BindingContext = trainingViewModel;
		InitializeComponent();
	}

    private void Switch_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {

    }
}