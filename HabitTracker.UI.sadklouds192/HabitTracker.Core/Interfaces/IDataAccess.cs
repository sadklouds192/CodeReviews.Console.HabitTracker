using HabitTracker.Core.Models;

namespace HabitTracker.Core.Interfaces;

public interface IDataAccess
{
    public void InitializeDb(string connectionString);
    public List<Habit> GetHabits(string connectionString);
    public Habit GetHabit(int id, string connectionString);
    public bool InsertHabit(Habit habit, string connectionString);
    public bool UpdateHabit(Habit habit, int id, string connectionString);
    public bool DeleteHabit(int id, string connectionString);
}