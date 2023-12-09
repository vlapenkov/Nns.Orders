using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.WebApi.Models
{
    public record WorkCycleResponse
    {
        public long Id { get; set; }

        public DateTime StartDate { get; set; }

        public WorkTypeDto WorkKind { get; set; }

        public uint OrderNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
