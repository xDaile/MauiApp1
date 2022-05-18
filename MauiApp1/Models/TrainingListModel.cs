using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{   
    //page1 mainly
    public record TrainingListModel(int? Id, string Name, string Description, int Order ):ModelBase
    {
    }
}
