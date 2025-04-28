using HabitTracker.Core.Interfaces;
using HabitTracker.Core.Models;

namespace HabitTracker.Core.Services;

public class SqliteDataService : IDataAccess
{
    private readonly string _connectionString;

    public SqliteDataService(string connectionString)
    {
        _connectionString = connectionString;
    }
    public bool InitializeDb()
    {
        throw new NotImplementedException();
    }

    public List<Habit> GetHabits()
    {
        throw new NotImplementedException();
    }

    public Habit GetHabit(int id)
    {
        throw new NotImplementedException();
    }

    public bool InsertHabit(Habit habit)
    {
        throw new NotImplementedException();
    }

    public bool UpdateHabit(Habit habit, int id)
    {
        throw new NotImplementedException();
    }

    public bool DeleteHabit(int id)
    {
        throw new NotImplementedException();
    }
}