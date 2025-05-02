using System.Globalization;

namespace HabitTracker.UI.sadklouds192;

public class UserInput
{
    private const string DateFormat = "yyyy/MM/dd";

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
            userDate = Console.ReadLine()?.Trim();
        }

        return date;
    }

    public static string GetUserInput(string prompt)
    {
        Console.Write($"\n{prompt}");
        var userInput = Console.ReadLine()?.Trim();
        while (string.IsNullOrWhiteSpace(userInput))
        {
            Console.WriteLine("Input cannot be empty. Please try again.");
            userInput = Console.ReadLine()?.Trim();
        }

        return userInput;
    }

    public static int GetIntInput(string prompt)
    {
        Console.Write($"\n{prompt}");
        var userInput = Console.ReadLine()?.Trim();
        int output;
        while (!int.TryParse(userInput, out output))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            userInput = Console.ReadLine()?.Trim();
        }

        return output;
    }
}