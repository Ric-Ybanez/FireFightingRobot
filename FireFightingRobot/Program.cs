using FireFightingRobot.Commands.Device;
using FireFightingRobot.DAL;
using FireFightingRobot.DAL.Repositories;
using FireFightingRobot.Domain.Interfaces;
using FireFigthingRobot.ReadStack;
using FireFigthingRobot.ReadStack.DeviceHistory;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();


try
{
    Log.Information("Starting up");
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));


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

    builder.Services.AddMediatR(typeof(RegisterDeviceCommand).Assembly, typeof(GetDeviceRecentHistoriesQuery).Assembly);
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>()
                    .AddScoped<IDeviceRepository, DeviceRepository>()
                    .AddScoped<IDeviceHistoryRepository, DeviceHistoryRepository>();


    var app = builder.Build();

    app.UseSerilogRequestLogging();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}


