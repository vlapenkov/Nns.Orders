using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nns.Orders.Infrastructure;
using Nns.Orders.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<IOrderDbContext, OrderDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        options.EnableSensitiveDataLogging(true);
    }

) ;



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
