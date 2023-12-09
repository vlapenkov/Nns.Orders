using Nns.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nns.Orders.WebApi.Models
{
    public record CreateWorkCycleRequest
    {

        public DateOnly StartDate { get; set; }

        public long WorkTypeId { get; set; }

        public uint OrderNumber { get; set; }

        public bool IsActive { get; set; }

    }
}
