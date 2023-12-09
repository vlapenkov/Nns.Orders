using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nns.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Infrastructure.EFCore.Configuration
{
    public class MachineKindConfiguration : BaseEntityTypeConfiguration<EquipmentType>
    {

        public override void Configure(EntityTypeBuilder<EquipmentType> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder.Property(x => x.Name).HasMaxLength(255);

        }

    }
}
