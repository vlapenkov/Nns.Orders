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
    public class EquipmentTypeConfiguration : BaseEntityTypeConfiguration<EquipmentApplication>
    {

        public override void Configure(EntityTypeBuilder<EquipmentApplication> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.HasOne(x => x.WorkType).WithMany().HasForeignKey(x => x.WorkTypeId);

            entityTypeBuilder.HasOne(x => x.EquipmentType).WithMany().HasForeignKey(x => x.EquipmentTypeId);            

            entityTypeBuilder.HasIndex(p => new { p.StartDate, p.WorkTypeId, p.EquipmentTypeId }).IsUnique();

        }

    }
    
}
