using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Interfaces.Models
{
    public record OrderPlanResponse
    {
        public long Id { get; set; }
        public SettlementDto Settlement { get; set; }

        public MachineKindDto MachineKind { get; set; }

        public WorkKindDto WorkKind { get; set; }

        public uint Value { get; set; }

        public uint OrderNumber { get; set; }

        public bool IsComplete { get; set; }
    }
}
