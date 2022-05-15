using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Enums;

namespace MauiApp1.Models
{
    public record ExerciseModel(int? Id, string Description, string Name) :ModelBase
    {}
}
