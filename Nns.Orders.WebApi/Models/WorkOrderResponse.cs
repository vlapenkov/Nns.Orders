using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.WebApi.Models
{
    public record WorkOrderShortResponse
    {
        
        public DateTime StartDate { get; set; }
        public long Id { get; set; }
        public ExcavationDto Excavation { get; set; }
        public uint Value { get; set; }

        public uint OrderNumber { get; set; }

        public bool IsComplete { get; set; }
    }
}
