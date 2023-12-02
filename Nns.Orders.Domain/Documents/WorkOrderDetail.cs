using Nns.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Domain.Documents
{
    public class WorkOrderDetail:BaseEntity
    {
        public int WorkOrderId { get; set; }

        public long WorkKindId { get; set; }

        public virtual WorkKind WorkKind { get; }

        public uint OrderNumber { get; set; }
    }
}
