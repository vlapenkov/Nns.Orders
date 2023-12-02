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

        public DbSet<MachineKind> MachineKinds { get; set; }

        public DbSet<Settlement> Settlements { get; set; }

        public DbSet<WorkKind> WorkKindS { get; set; }

        public DbSet<MachineApplication> MachineApplications { get; set; }

        public DbSet<WorkOrder> WorkOrders { get; set; }

        public DbSet<OrderPlan> OrderPlan { get; set; }

        Task<int> SaveChangesAsync();

        
    }
}
