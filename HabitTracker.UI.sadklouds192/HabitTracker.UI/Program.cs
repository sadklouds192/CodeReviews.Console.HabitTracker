
using System.Data;
using System.Data.SQLite;
using HabitTracker.Core.Interfaces;
using HabitTracker.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfiguration>(configuration)
    .AddLogging(builder => builder.AddConsole())
    .AddTransient<IDbConnection>(provider => new SQLiteConnection(configuration.GetConnectionString("DefaultConnection")))
    .AddTransient<IDataAccess, SqliteDataService>()  // DataAccess service
    .BuildServiceProvider();

// See https://aka.ms/new-console-template for more information
IDataAccess dataAccess = serviceProvider.GetService<IDataAccess>();
dataAccess.InitializeDb();
Console.WriteLine(configuration.GetConnectionString("DefaultConnection"));

