using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MauiApp1.Models;
using MauiApp1.Services;


namespace MauiApp1.ViewModels
{
    public class ExerciseViewModel:ViewModelBase
    {
        private readonly IRoutingService routingService;

        public ExerciseViewModel(IRoutingService routingService)
        {
            this.routingService = routingService;
        }
    }
}
