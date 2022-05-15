using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MauiApp1.DAL.Entities
{
    public record TrainingPlanEntity:EntityBase
    {
        public string Name { get; set; }
    }
}
