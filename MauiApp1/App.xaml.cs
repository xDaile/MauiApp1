using MauiApp1.Shells;

namespace MauiApp1;

public partial class App
{
	public App(IServiceProvider serviceProvider)
	{
		InitializeComponent();
		MainPage = new AppShellPhone();
		//		MainPage = new NavigationPage(new MainPageView()) { Title = "Training list" };
	}
}