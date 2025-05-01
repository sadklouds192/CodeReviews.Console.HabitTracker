using System.Data.SQLite;
using HabitTracker.Core.Interfaces;
using HabitTracker.Core.Models;
using Microsoft.Extensions.Logging;

namespace HabitTracker.Core.Services;

public class SqliteDataService : IDataAccess
{
    private readonly ILogger<SqliteDataService> _logger;

    public SqliteDataService(ILogger<SqliteDataService> logger)
    {
        _logger = logger;
    }
    public void InitializeDb(string connectionString)
    {
        _logger.LogInformation("Initializing database");
        // Extract the file path from the connection string
        var databaseFilePath = new SQLiteConnectionStringBuilder(connectionString).DataSource;

        // Ensure the folder exists
        var directory = Path.GetDirectoryName(databaseFilePath);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory!);

        try
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Habits (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Quantity INTEGER NOT NULL,
                DateTracked TEXT NOT NULL
            );";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (SQLiteException ex)
        {
            _logger.LogError($"Error initializing database: {ex.Message}");
            throw;
        }
    }


    public List<Habit> GetHabits(string connectionString)
    {
        throw new NotImplementedException();
    }

    public Habit GetHabit(int id, string connectionString)
    {
        throw new NotImplementedException();
    }

    public bool InsertHabit(Habit habit, string connectionString)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var query = @"INSERT INTO Habits (Id, Name, Quantity, DateTracked)";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }

            return true;
        }
    }

    public bool UpdateHabit(Habit habit, int id, string connectionString)
    {
        throw new NotImplementedException();
    }

    public bool DeleteHabit(int id, string connectionString)
    {
        throw new NotImplementedException();
    }
}