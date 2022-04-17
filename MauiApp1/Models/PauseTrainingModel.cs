using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    //pauseTrainingModel and restTrainingModel can be deleted, nothing important there, duplicity of data is very small (smaller application instead will be faster)
    public record PauseTrainingModel(int TrainingId, int PauseId, int SecondsDuration, string Description):ModelBase
    {
    }
}
