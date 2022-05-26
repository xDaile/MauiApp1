using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class CreateExerciseTrainingView
{
    /* This in view could not work as it was not able to pick seconds, maybe it will be ok in the future
       Instead of this, there is label, that will show prompt after tap
         
        * <TimePicker Grid.Column="2"
                           Format="mm\:ss"
                           Time="{Binding NewPause.Duration}" />
        
        */
	public CreateExerciseTrainingView(CreateExerciseTrainingViewModel viewModel): base(viewModel)
	{
		InitializeComponent();
	}
}

