using Microsoft.EntityFrameworkCore;
using Nns.Orders.Infrastructure;
using Nns.Orders.Interfaces;
using Nns.Orders.Interfaces.Logic;
using Nns.Orders.Logic;


WebApplication.CreateBuilder(args)
    .RunApi((host, configuration, services) =>
{

    services.AddDbContext<IOrderDbContext,OrderDbContext>(
        options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.EnableSensitiveDataLogging(true);
        }

    );

    services.AddScoped<IWorkCycleState, WorkCycleState>();
    services.AddScoped<IEquipmentState, EquipmentState>();

    services.AddScoped<IEquipmentApplicationService, EquipmentApplicationService>();
    services.AddScoped<IWorkCycleService, WorkCycleService>();
    services.AddScoped<IWorkOrderService, WorkOrderService>();

    

});



