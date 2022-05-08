using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.DAL.Entities
{
    public abstract record EntityBase
    {
        public Guid Id { get; } 
        public string Description { get; init; } 
    }
}
