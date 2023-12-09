using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nns.Orders.Domain.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Infrastructure.EFCore.Configuration
{
    public class WorkOrderConfiguration :BaseEntityTypeConfiguration<WorkOrder>
    {
        public override void Configure(EntityTypeBuilder<WorkOrder> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.HasOne(x => x.WorkType).WithMany().HasForeignKey(x => x.WorkTypeId);

            entityTypeBuilder.HasOne(x => x.EquipmentType).WithMany().HasForeignKey(x => x.EquipmentTypeId);

            entityTypeBuilder.HasOne(x => x.Excavation).WithMany().HasForeignKey(x => x.ExcavationId);

            entityTypeBuilder.HasIndex(p => new { p.StartDate, p.ExcavationId, p.WorkTypeId, p.EquipmentTypeId }).IsUnique();

        }
    }
}
