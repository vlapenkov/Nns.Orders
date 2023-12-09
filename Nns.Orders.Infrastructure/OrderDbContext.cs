using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using Nns.Orders.Common.Exceptions;
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

            modelBuilder.Entity<WorkType>().HasData(
               new WorkType { Id = 1, Name = "Бурение" },
               new WorkType { Id = 2, Name = "Крепление" },
               new WorkType { Id = 3, Name = "Отгрузка" },
               new WorkType{ Id = 4, Name = "Взрыв" }
                );

            modelBuilder.Entity<EquipmentType>().HasData(
              new EquipmentType{ Id = 1, Name = "Буровая машина" },
              new EquipmentType{ Id = 2, Name = "Машина крепления " },
              new EquipmentType{ Id = 3, Name = "Погрузочно-разгрузочная машина" },
              new EquipmentType{ Id = 4, Name = "Взрывная техника" },
              new EquipmentType{ Id = 5, Name = "Универсальная машина для любых работ" }
               );

            modelBuilder.Entity<Excavation>().HasData(
             new Excavation { Id = 1, Name = "Первая выработка" },
             new Excavation{ Id = 2, Name = "Вторая выработка" },
             new Excavation{ Id = 3, Name = "Третья выработка" }             
              );
        }      

        public DbSet<EquipmentType> EquipmentTypes { get; set; }

        public DbSet<Excavation> Excavations { get; set; }

        public DbSet<WorkType> WorkTypes { get; set; }

        public DbSet<EquipmentApplication> EquipmentApplications { get; set; }

        public DbSet<WorkCycle> WorkCycles { get; set; }

        public DbSet<WorkOrder> WorkOrders { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken =default)
        {
            try
            {                
                return await base.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("FOREIGN KEY", StringComparison.InvariantCultureIgnoreCase))
                    throw new EntityNotFoundException("Указанный ключ отсутствует в базе");
                else throw;
            }
        }
      
    }
}