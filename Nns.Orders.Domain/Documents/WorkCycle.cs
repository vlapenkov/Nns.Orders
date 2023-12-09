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
    public class WorkCycle : BaseEntity
    {
        public DateTime StartDate { get; set; }     

        public long WorkTypeId { get; set; }

        public virtual WorkType WorkType { get; }

        public uint OrderNumber { get; set; }

        public bool IsActive { get; set; }

      
    }
}
