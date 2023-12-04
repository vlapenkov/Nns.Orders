using Nns.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nns.Orders.Interfaces.Models
{
    public record CreateWorkOrderRequest
    {        
        
        public DateOnly StartDate { get; set; }
        public long SettlementId { get; set; }        

        public long WorkKindId { get; set; }        

        public uint OrderNumber { get; set; }

        public bool IsActive { get; set; }
      
    }
}
