using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Interfaces.Models
{
    public record MachineApplicationResponse
    {
        public long Id { get; set; }
        public SettlementDto Settlement { get; set; }

        public MachineKindDto MachineKind { get; set; }

        public WorkKindDto WorkKind { get; set; }

        public bool IsActive { get; set; }
    }
}
