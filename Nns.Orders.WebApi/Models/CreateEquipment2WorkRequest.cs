using Nns.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.WebApi.Models
{
    public record CreateEquipment2WorkRequest
    {
        public DateOnly StartDate { get; set; }

        public long EquipmentTypeId { get; set; }

        public long WorkTypeId { get; set; }

        public bool IsActive { get; set; }
    }
}
