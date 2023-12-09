using Nns.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.WebApi.Models
{
    public record CreateWorkOrderRequest
    {
        public DateOnly StartDate { get; set; }

        public long ExcavationId { get; set; }

        public long WorkTypeId { get; set; }

        public long EquipmentTypeId { get; set; }

        public uint Value { get; set; }

        public uint OrderNumber { get; set; }

        public bool IsComplete { get; set; }

    }
}
