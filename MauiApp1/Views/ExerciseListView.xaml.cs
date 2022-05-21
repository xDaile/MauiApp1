using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class ExerciseListView:ContentPageBase
{
	public ExerciseListView(ExerciseListViewModel viewModel) 
		: base(viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}