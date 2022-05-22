using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public record PauseModel(int? Id, string Description, string Name, TimeSpan Duration, int Order, int TrainingId):TrainingItemModel
    {
    }
}
