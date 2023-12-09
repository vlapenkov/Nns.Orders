using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.WebApi.Models
{
    public record EquipmentApplicationResponse
    {
        public long Id { get; set; }

        public EquipmentTypeDto EquipmentType { get; set; }

        public WorkTypeDto WorkType { get; set; }

        public bool IsActive { get; set; }
    }
}
