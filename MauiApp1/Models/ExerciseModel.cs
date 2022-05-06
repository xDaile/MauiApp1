using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Enums;

namespace MauiApp1.Models
{
    public record ExerciseModel(Guid Id, string Name):ModelBase
    {}
}
