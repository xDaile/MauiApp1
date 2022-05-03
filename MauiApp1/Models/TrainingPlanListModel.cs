using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public record TrainingPlanListModel(Guid Id, string Name) : ModelBase
    {
    }
}
