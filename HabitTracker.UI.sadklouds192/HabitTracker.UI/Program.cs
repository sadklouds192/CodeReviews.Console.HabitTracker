using HabitTracker.Core.Interfaces;
using HabitTracker.Core.Services;
using HabitTracker.UI.sadklouds192;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting up");

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        // Register IConfiguration with the DI container
        services.AddSingleton<IConfiguration>(configuration);
        services.AddTransient<IDataAccess, SqliteDataService>(); // DataAccess service
        services.AddTransient<HabitApplication>();
    })
    .UseSerilog()
    .Build();

var app = host.Services.GetRequiredService<HabitApplication>();
app.Run();


