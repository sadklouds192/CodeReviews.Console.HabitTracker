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
                Unit TEXT NOT NULL,
                Quantity INTEGER NOT NULL,
                Date TEXT NOT NULL
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
        try
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var habits = new List<Habit>();
                var getHabitsCommand = @"SELECT * FROM Habits;";

                using (var command = new SQLiteCommand(getHabitsCommand, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        habits.Add(new Habit
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Unit = reader["Unit"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            Date = Convert.ToDateTime(reader["Date"])
                        });
                }

                return habits;
            }
        }
        catch (SQLiteException ex)
        {
            _logger.LogError($"Error retrieving habits from database: {ex.Message}");
            throw;
        }
    }

    public Habit GetHabit(int id, string connectionString)
    {
        try
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var getHabitCommand = @"SELECT * FROM Habits WHERE Id = @Id;";
                using (var command = new SQLiteCommand(getHabitCommand, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return new Habit
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Unit = reader["Unit"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Date = Convert.ToDateTime(reader["Date"])
                            };
                    }
                    return null;
                }
            }
        }catch (SQLiteException ex)
        {
            _logger.LogError($"Error retrieving habit: {ex.Message}");
            throw;
        }
    }

        public void InsertHabit(Habit habit, string connectionString)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    var query = @"INSERT INTO Habits (Name, Unit, Quantity, Date)
                              VALUES (@Name, @Unit, @Quantity, @Date)";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", habit.Name);
                        command.Parameters.AddWithValue("@Unit", habit.Unit);
                        command.Parameters.AddWithValue("@Quantity", habit.Quantity);
                        command.Parameters.AddWithValue("@Date", habit.Date.ToString("yyyy-MM-dd"));
                        command.ExecuteNonQuery();
                    }

                    _logger.LogInformation($"Inserted {habit.Name} successfully");
                }
            }
            catch (SQLiteException ex)
            {
                _logger.LogError($"Error inserting habit: {ex.Message}");
            }
        }

        public void UpdateHabit(Habit habit, int id, string connectionString)
        {
            throw new NotImplementedException();
        }

        public void DeleteHabit(int id, string connectionString)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    var query = @"DELETE FROM Habits WHERE Id = @Id;";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        int rowsEffected = command.ExecuteNonQuery();
                        if (rowsEffected == 0)
                            throw new InvalidOperationException($"Habit with id {id} does not exist");
                    }
                }
            }
            catch (SQLiteException ex)
            {
                _logger.LogError($"Error deleting habit: {ex.Message}");
                throw;
            }
        }
    }