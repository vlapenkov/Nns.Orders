using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Domain.Entities
{
    public class Settlement :BaseEntity
    {
        
        public string Name { get; set; }
    }
}
