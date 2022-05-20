using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    //Third page
    public record TrainingModel(int? Id, string Name, string Description, int TrainingPlanId, int Order, IList<TrainingItemModel> TrainingItems):ModelBase
    {
    }
}
