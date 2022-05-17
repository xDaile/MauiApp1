using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace MauiApp1.Models
{ 
    //page1 mainly
    public record TrainingPlanModel(int? Id, string Description, string Name, IList<TrainingListModel>? Trainings) : ModelBase
    {
    }
}
