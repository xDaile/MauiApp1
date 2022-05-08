using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public record PauseModel(Guid Id, string Name, int Seconds, bool IsPlanned, int Order, string description):TrainingItemModel
    {
    }
}
