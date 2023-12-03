using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nns.Orders.Domain.Entities;

namespace Nns.Orders.Domain.Documents
{
    /// <summary>
    /// Порядок работ по выработке (производственный цикл)
    /// </summary>
    public class WorkOrder : BaseEntity
    {
        public DateTime StartDate { get; set; }

        public long SettlementId { get; set; }
        public virtual Settlement Settlement { get; }

        public long WorkKindId { get; set; }

        public virtual WorkKind WorkKind { get; }

        public uint OrderNumber { get; set; }

        public bool IsActive { get; set; }

      
    }
}
