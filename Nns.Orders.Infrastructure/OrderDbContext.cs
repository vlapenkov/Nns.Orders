using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using Nns.Orders.Domain.Entities;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces;

namespace Nns.Orders.Infrastructure
{
    public class OrderDbContext : DbContext, IOrderDbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());          

            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WorkKind>().HasData(
               new WorkKind { Id = 1, Name = "Бурение" },
               new WorkKind { Id = 2, Name = "Крепление" },
               new WorkKind { Id = 3, Name = "Отгрузка" },
               new WorkKind{ Id = 4, Name = "Взрыв" }
                );

            modelBuilder.Entity<MachineKind>().HasData(
              new MachineKind{ Id = 1, Name = "Буровая машина" },
              new MachineKind{ Id = 2, Name = "Машина крепления " },
              new MachineKind{ Id = 3, Name = "Погрузочно-разгрузочная машина" },
              new MachineKind{ Id = 4, Name = "Взрывная техника" },
              new MachineKind{ Id = 5, Name = "Универсальная машина для любых работ" }
               );

            modelBuilder.Entity<Settlement>().HasData(
             new Settlement { Id = 1, Name = "Первая выработка" },
             new Settlement{ Id = 2, Name = "Вторая выработка" },
             new Settlement{ Id = 3, Name = "Третья выработка" }             
              );
        }

        public DbSet<MachineKind> MachineKinds { get; set; }

        public DbSet<Settlement> Settlements { get; set; }

        public DbSet<WorkKind> WorkKindS { get; set; }

        public DbSet<MachineApplication> MachineApplications { get; set; }

        public DbSet<WorkOrder> WorkOrders { get; set; }

        public DbSet<OrderPlan> OrderPlan { get; set; }
       

        public async Task<int> SaveChangesAsync()
        {
           return  await base.SaveChangesAsync();
        }
    }
}