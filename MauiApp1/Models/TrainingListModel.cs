using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{   
    //page1 mainly
    public record TrainingListModel(Guid Id, string Name, int Order ):ModelBase
    {
    }
}
