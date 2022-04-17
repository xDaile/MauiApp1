namespace MauiApp1;

public class training
{
	private int id = 0;
	public int Id
	{
		get { return id; }
		set { id = value; }
	}
}
public partial class MainPage : ContentPage
{
	int count = 0;
	bool editButtonVisibility = false;
	bool startButtonVisibility = false;
	training[] trainingList = new training[2];
	training selectedTraining;


	public MainPage()
	{
	    //training tr1 = new training();
		//tr1.Id = 1;
		//Console.WriteLine(tr1.Id);
		// traingingList.
		//traingingList.Append(new training());
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;
		//CounterLabel.Text = $"Current count: {count}";

		//SemanticScreenReader.Announce(CounterLabel.Text);
	}

	private void TrainingSelected(object sender, EventArgs e)
    {
		editButtonVisibility = true;
		startButtonVisibility = true;

	}

	private void ExitApp(object sender, EventArgs e)
    {
		System.Environment.Exit(0);
    }
	private void GoToSettings(object sender, EventArgs e)
	{
		Navigation.PushAsync(new SettingsPage());
	}

	private void CreateNewTraining(object sender, EventArgs e)
    {
		Navigation.PushAsync(new CreateTrainingProgramPage());
    }

	private void EditTraining(object sender, EventArgs e)
	{
		Navigation.PushAsync(new EditTrainingProgramPage());
	}
}




