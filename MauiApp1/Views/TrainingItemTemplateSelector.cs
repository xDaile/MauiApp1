using MauiApp1.Models;
using MauiApp1.ViewModels;
using MauiApp1.Views;

namespace MauiApp1.Views.Selector;


public class TrainingItemTemplateSelector:DataTemplateSelector
{
    public DataTemplate PauseTemplate { get; set; }
    public DataTemplate ExerciseTrainingTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item.GetType().Equals(typeof(PauseModel)))
            return PauseTemplate;
        return ExerciseTrainingTemplate;
    }
}



