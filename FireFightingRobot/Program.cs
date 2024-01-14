using FireFightingRobot.Commands.Device;
using FireFightingRobot.DAL;
using FireFightingRobot.DAL.Repositories;
using FireFightingRobot.Domain.Interfaces;
using FireFigthingRobot.ReadStack;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddDbContext<DataContext>(x =>
    {
        x.UseSqlServer(connectionString);
    })
    .AddDbContext<ReadContext>(x =>
    {
        x.UseSqlServer(connectionString);
    });

builder.Services.AddMediatR(typeof(RegisterDeviceCommand).Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IDeviceRepository, DeviceRepository>()
                .AddScoped<IDeviceHistoryRepository, DeviceHistoryRepository>();


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
