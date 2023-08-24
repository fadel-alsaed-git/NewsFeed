using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Entity: Entity<int>
    {
    }

    public class Entity<T>: IEntity
    {
        public T Id { get; set; }
    }
    
}
