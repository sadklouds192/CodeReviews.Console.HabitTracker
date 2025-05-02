using System.Globalization;

namespace HabitTracker.UI.sadklouds192;

public class UserInput
{
    private const string DateFormat = "yyyy/MM/dd";

    
    //For updating existing record date
    public static DateTime GetOptionalUserDate(string prompt, DateTime currentDate)
    {
        Console.Write($"\n{prompt} (Leave blank to keep {currentDate:yyyy/MM/dd})");
        var userDate = Console.ReadLine()?.Trim();

        while (true)
        {
            if (string.IsNullOrEmpty(userDate))
            {
                return currentDate;
            }
            
            if (DateTime.TryParseExact(userDate, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;
            
            Console.WriteLine($"Invalid date. Please enter a valid date in format {DateFormat}.");
            Console.Write($"\n{prompt} (Leave blank to keep {currentDate:yyyy/MM/dd}): ");
            userDate = Console.ReadLine()?.Trim();
        }
    }
  
    public static DateTime GetUserDate(string prompt)
    {
        Console.Write($"\n{prompt}");
        var userDate = Console.ReadLine()?.Trim();
        DateTime date;
        while (!DateTime.TryParseExact(userDate, DateFormat, CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out date))
        {
            Console.WriteLine($"Invalid date. Please enter a valid date format ({DateFormat}).");
            Console.Write($"\n{prompt}: ");
            userDate = Console.ReadLine()?.Trim();
        }

        return date;
    }

    public static string GetOptionalUserInput(string prompt, string currentValue)
    {
        Console.Write($"\n{prompt} (Leave blank to keep {currentValue})");
        var userInput = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(userInput))
            return currentValue;
        
        return userInput;
    }
    
    public static string GetUserInput(string prompt)
    {
        Console.Write($"\n{prompt}");
        var userInput = Console.ReadLine()?.Trim();
        while (string.IsNullOrWhiteSpace(userInput))
        {
            Console.WriteLine("Input cannot be empty. Please try again.");
            Console.Write($"\n{prompt}: ");
            userInput = Console.ReadLine()?.Trim();
        }
        return userInput;
    }

    public static int GetOptionalIntInput(string prompt, int currentValue)
    {
        Console.Write($"\n{prompt} (Leave blank to keep {currentValue})");
        var userInput = Console.ReadLine()?.Trim();
        while (true)
        {
            if (string.IsNullOrEmpty(userInput))
                return currentValue;
            
            if (int.TryParse(userInput, out var userInputInt))
                return userInputInt;
            Console.WriteLine($"Invalid input. Please enter a valid integer.");
            Console.Write($"\n{prompt} (Leave blank to keep {currentValue}): ");
            userInput = Console.ReadLine()?.Trim();
        }
    }
    public static int GetIntInput(string prompt)
    {
        Console.Write($"\n{prompt}");
        var userInput = Console.ReadLine()?.Trim();
        int output;
        while (!int.TryParse(userInput, out output))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            Console.Write($"\n{prompt}: ");
            userInput = Console.ReadLine()?.Trim();
        }

        return output;
    }
}