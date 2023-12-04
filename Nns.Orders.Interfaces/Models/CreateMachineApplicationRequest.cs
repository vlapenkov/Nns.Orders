using Nns.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Interfaces.Models
{
    public record CreateMachineApplicationRequest
    {
        public DateOnly StartDate { get; set; }
        public long SettlementId { get; set; }        

        public long MachineKindId { get; set; }        

        public long WorkKindId { get; set; }        

        public bool IsActive { get; set; }
    }
}
