using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nns.Orders.Domain;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Infrastructure.EFCore.Configuration
{
    public class WorkOrderConfiguration : BaseEntityTypeConfiguration<WorkOrder>
    {

        public override void Configure(EntityTypeBuilder<WorkOrder> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);


            entityTypeBuilder.HasOne(x => x.WorkKind).WithMany().HasForeignKey(x => x.WorkKindId);

            entityTypeBuilder.HasOne(x => x.Settlement).WithMany().HasForeignKey(x => x.SettlementId);

            entityTypeBuilder.HasIndex(p => new { p.StartDate, p.SettlementId, p.WorkKindId }).IsUnique();

        }

    }
}
