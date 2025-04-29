using System.Data.SQLite;
using HabitTracker.Core.Interfaces;
using HabitTracker.Core.Models;

namespace HabitTracker.Core.Services;

public class SqliteDataService : IDataAccess
{
    public void InitializeDb(string connectionString)
    {
        // Extract the file path from the connection string
        var databaseFilePath = new SQLiteConnectionStringBuilder(connectionString).DataSource;

        // Ensure the folder exists
        var directory = Path.GetDirectoryName(databaseFilePath);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory!);

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
        throw new NotImplementedException();
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