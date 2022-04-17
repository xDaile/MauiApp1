using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public record RestModel(Guid Id, string Name, string Description, int Seconds, bool IsPlanned, int Order):TrainingItemModel
    {
    }
}
