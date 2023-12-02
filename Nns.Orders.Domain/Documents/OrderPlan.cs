using Nns.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Domain.Documents
{
    public class OrderPlan : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public long SettlementId { get; set; }

        public virtual Settlement Settlement { get; }

        public long WorkKindId { get; set; }

        public virtual WorkKind WorkKind { get; }

        public long MachineKindId { get; set; }

        public virtual MachineKind MachineKind { get; }

        public uint Value { get; set; }

        public uint OrderNumber { get; set; }

        public bool IsComplete { get; set; }
    }
}
