using Nns.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Domain.Documents
{
    /// <summary>
    /// План работ (наряд-задание)
    /// </summary>
    public class WorkOrder : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public long ExcavationId { get; set; }

        public virtual Excavation Excavation { get; }

        public long WorkTypeId { get; set; }

        public virtual WorkType WorkType { get; }

        public long EquipmentTypeId { get; set; }

        public virtual EquipmentType EquipmentType { get; }

        public uint Value { get; set; }

        public uint OrderNumber { get; set; }

        public bool IsComplete { get; set; }
    }
}
