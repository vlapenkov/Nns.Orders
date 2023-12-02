using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nns.Orders.Domain.Entities;

namespace Nns.Orders.Infrastructure.EFCore.Configuration
{
    public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TBase> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.Created)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("getdate()");

            entityTypeBuilder.HasQueryFilter(p => p.EndDate == null);
        }
    }
}
