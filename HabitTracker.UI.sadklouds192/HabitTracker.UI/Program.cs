using HabitTracker.Core.Interfaces;
using HabitTracker.Core.Services;
using HabitTracker.UI.sadklouds192;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .Build();

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        // Register IConfiguration with the DI container
        services.AddSingleton<IConfiguration>(configuration);
        services.AddTransient<IDataAccess, SqliteDataService>(); // DataAccess service
        services.AddTransient<HabitApplication>();
    })
    .Build();


// See https://aka.ms/new-console-template for more information
var app = host.Services.GetService<HabitApplication>();
app.Run();


