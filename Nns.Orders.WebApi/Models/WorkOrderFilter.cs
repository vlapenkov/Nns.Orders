using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.WebApi.Models
{
    public class WorkOrderFilter : BaseFilter
    {
        public long? ExcavationId { get; set; }

        public long? WorkTypeId { get; set; }
    }
}
