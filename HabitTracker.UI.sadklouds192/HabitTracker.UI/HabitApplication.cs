using HabitTracker.Core.Interfaces;
using HabitTracker.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HabitTracker.UI.sadklouds192;

public class HabitApplication
{
    private readonly string? _connectionString;
    private readonly IDataAccess _dataAccess;
    private readonly ILogger<HabitApplication> _logger;

    public HabitApplication(ILogger<HabitApplication> logger, IDataAccess dataAccess, IConfiguration configuration)
    {
        _logger = logger;
        _dataAccess = dataAccess;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public List<Habit> habbits { get; set; } = new();

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
            _dataAccess.InitializeDb(_connectionString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: Database connection failed. Please check your configuration.");
            Console.WriteLine($"Details: {ex.Message}");
            Environment.Exit(1);
        }

        var running = true;
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
                case "2":
                    GetHabits();
                    break;
                case "5":
                    GetHabit();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }

    public void ShowMainMenu()
    {
        Console.WriteLine("\nMain Menu\n");
        Console.WriteLine("What would you like to do?\n");
        Console.WriteLine("Type '0' to close the application");
        Console.WriteLine("Type '1' to Insert habit");
        Console.WriteLine("Type '2' to View all habits");
        Console.WriteLine("Type '3' to delete habit");
        Console.WriteLine("Type '4' to update habit");
        Console.WriteLine("Type '5' to View habit");
        Console.WriteLine("-----------------------------------\n");
        Console.Write("Enter your choice: ");
    }

    public void InsertHabit()
    {
        var habitName = UserInput.GetUserInput("Enter habit's name: ");
        var habitUnit = UserInput.GetUserInput("Enter habit's unit of measure: ");
        var habitQuantity = UserInput.GetIntInput("Enter habit's quantity (no decimal numbers): ");
        var habitDate = UserInput.GetUserDate("Enter habit's date: ");
        try
        {
            var habit = new Habit
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

    public void GetHabit()
    {
        int userInput = UserInput.GetIntInput("Please enter habit Id:");
        try
        {
            Habit habit = _dataAccess.GetHabit(userInput, _connectionString);
            if (habit == null)
            {
                Console.WriteLine("Habit not found!");
                return;
            }

            Console.WriteLine($"Habit: {habit.Name}, Id: {habit.Id}, Unit: {habit.Unit}, " +
                              $"Quantity: {habit.Quantity}, " +
                              $"Date: {habit.Date}");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: Could not get habit");
        }
        
    }

    public void GetHabits()
    {
        habbits = _dataAccess.GetHabits(_connectionString);
        if (!habbits.Any())
        {
            Console.WriteLine("\nNo Habits found.");
            return;
        }

        foreach (var habit in habbits)
            Console.WriteLine($"Habit: {habit.Name}, Id: {habit.Id}, Date: {habit.Date:yyyy-MM-dd}");
    }
}