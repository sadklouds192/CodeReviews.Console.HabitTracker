using HabitTracker.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HabitTracker.UI.sadklouds192;

public class HabitApplication
{
    private readonly IDataAccess _dataAccess;
    private readonly string? _connectionString;

    public HabitApplication(IDataAccess dataAccess, IConfiguration configuration)
    {
        _dataAccess = dataAccess;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        
    }

    public void Run()
    {
        if (string.IsNullOrEmpty(_connectionString))
        {
            Console.WriteLine("ERROR: Connection string is missing. Please check your configuration.");
            Environment.Exit(1); // Exit with error code
        }
        _dataAccess.InitializeDb(connectionString: _connectionString);
        
    }
    
}