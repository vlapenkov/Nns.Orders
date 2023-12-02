using IBS.Employees.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Interfaces.Models
{
    public class OrderPlanFilter :BaseFilter
    {
        public long? SettlementId { get; set; }

        public long? WorkKindId { get; set; }
    }
}
