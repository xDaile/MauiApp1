﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace MauiApp1.BL.Facades.Interfaces
{
    public interface ITrainingPlanFacade:IFacade<TrainingPlanModel>
    {
        public Task<List<TrainingPlanListModel>> GetAllList();
        public Task<int> CreateFromListModel(TrainingPlanListModel model);

    }
}
