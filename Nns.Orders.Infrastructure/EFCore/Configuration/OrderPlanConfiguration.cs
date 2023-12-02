using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nns.Orders.Domain.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Infrastructure.EFCore.Configuration
{
    public class OrderPlanConfiguration :BaseEntityTypeConfiguration<OrderPlan>
    {
        public override void Configure(EntityTypeBuilder<OrderPlan> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.HasOne(x => x.MachineKind).WithMany().HasForeignKey(x => x.MachineKindId);

            entityTypeBuilder.HasOne(x => x.WorkKind).WithMany().HasForeignKey(x => x.WorkKindId);

            entityTypeBuilder.HasOne(x => x.Settlement).WithMany().HasForeignKey(x => x.SettlementId);

            //entityTypeBuilder.HasIndex(p => new { p.StartDate, p.SettlementId, p.WorkKindId, p.MachineKindId }).IsUnique();

        }
    }
}
