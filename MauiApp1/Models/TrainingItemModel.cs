using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public record TrainingItemModel(int Order, int TrainingId) : ModelBase
    {
        public static implicit operator List<object>(TrainingItemModel v)
        {
            throw new NotImplementedException();
        }
    }
}
