using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class UserView
{
	public UserView(UserViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
	}
}