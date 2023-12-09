using Microsoft.EntityFrameworkCore;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Interfaces
{
    public interface IOrderDbContext
    {
        DbSet<EquipmentType> EquipmentTypes { get; }

        DbSet<Excavation> Excavations { get; }

        DbSet<WorkType> WorkTypes { get; }

        DbSet<EquipmentApplication> EquipmentApplications { get;  }

        DbSet<WorkCycle> WorkCycles { get; }

        DbSet<WorkOrder> WorkOrders { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
