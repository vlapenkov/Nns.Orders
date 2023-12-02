using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Interfaces.Models
{
    public record WorkOrderResponse
    {        
        public long Id { get; set; }

        public DateTime StartDate  { get; set; }

        public SettlementDto Settlement { get; set; }

        public WorkKindDto WorkKind { get; set; }

        public uint OrderNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
