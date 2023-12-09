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
    public class EquipmentApplication :BaseEntity
    {
        public DateTime StartDate { get; set; }     

        public long EquipmentTypeId { get; set; }

        public virtual EquipmentType EquipmentType { get; }

        public long WorkTypeId { get; set; }

        public virtual WorkType WorkType { get; }

        public bool IsActive { get; set; }

    }
}
