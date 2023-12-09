using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.WebApi.Models
{
    public record EquipmentTypeDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
