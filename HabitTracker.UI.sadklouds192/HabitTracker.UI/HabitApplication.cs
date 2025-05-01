using System.Collections.Concurrent;
using System.Linq.Expressions;
using HabitTracker.Core.Interfaces;
using HabitTracker.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HabitTracker.UI.sadklouds192;

public class HabitApplication
{
    private readonly ILogger<HabitApplication> _logger;
    private readonly IDataAccess _dataAccess;
    private readonly string? _connectionString;

    public HabitApplication(ILogger<HabitApplication> logger,IDataAccess dataAccess, IConfiguration configuration)
    {
        _logger = logger;
        _dataAccess = dataAccess;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        
    }

    public void Run()
    {
        _logger.LogInformation("Application started");
        if (string.IsNullOrEmpty(_connectionString))
        {
            Console.WriteLine("ERROR: Connection string is missing. Please check your configuration.");
            Environment.Exit(1); // Exit with error code
        }
        try
        {
            _dataAccess.InitializeDb(connectionString: _connectionString);

        }
        catch( Exception ex)
        {
            Console.WriteLine("ERROR: Database connection failed. Please check your configuration.");
            Console.WriteLine($"Details: {ex.Message}");
            Environment.Exit(1);
        }
        bool running = true;
        while (running)
        {
            ShowMainMenu();
            var input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    running = false;
                    Console.WriteLine("Closing...");
                    break;
                case "1":
                    InsertHabit();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }

    }
    
    public void ShowMainMenu()
    {
        Console.WriteLine("Main Menu\n");
        Console.WriteLine("What would you like to do?\n");
        Console.WriteLine("Type '0' to close the application");
        Console.WriteLine("Type '1' to Insert habit");
        Console.WriteLine("Type '2' to View all habits");
        Console.WriteLine("Type '3' to delete habit");
        Console.WriteLine("Type '4' to update habit");
        Console.WriteLine("-----------------------------------\n");
        Console.Write("Enter your choice: ");
    }

    public void InsertHabit()
    {
        string habitName = UserInput.GetUserInput("Enter habit's name: ");
        string habitUnit = UserInput.GetUserInput("Enter habit's unit of measure: ");
        int habitQuantity = UserInput.GetIntInput("Enter habit's quantity (no decimal numbers): ");
        DateTime habitDate = UserInput.GetUserDate("Enter habit's date: ");
        try
        {
            Habit habit = new Habit()
            {
                Name = habitName,
                Unit = habitUnit,
                Quantity = habitQuantity,
                Date = habitDate
            };
            _dataAccess.InsertHabit(habit, _connectionString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: Could not insert habit");
            Console.WriteLine($"Details: {ex.Message}");
        }
    }
    
}