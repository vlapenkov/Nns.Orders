using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nns.Orders.Domain.Entities;

namespace Nns.Orders.Domain.Documents
{
    /// <summary>
    /// Применяемость видов техники по видам работ в рамках выработки
    /// </summary>
    public class MachineApplication :BaseEntity
    {
        public DateTime StartDate { get; set; }
        public long SettlementId { get; set; }

        public virtual Settlement Settlement { get; }

        public long MachineKindId { get; set; }

        public virtual MachineKind MachineKind { get; }

        public long WorkKindId { get; set; }

        public virtual WorkKind WorkKind { get; }

        public bool IsActive { get; set; }

    }
}
