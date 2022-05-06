using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class ExerciseListView
{
	public ExerciseListView(ExerciseListViewModel viewModel) 
		: base(viewModel)
	{
		InitializeComponent();
	}
}