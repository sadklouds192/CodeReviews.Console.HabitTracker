using HabitTracker.Core.Models;

namespace HabitTracker.Core.Interfaces;

public interface IDataAccess
{
    public void InitializeDb(string connectionString);
    public List<Habit> GetHabits(string connectionString);
    public Habit GetHabit(int id, string connectionString);
    public void InsertHabit(Habit habit, string connectionString);
    public void UpdateHabit(Habit habit, int id, string connectionString);
    public void DeleteHabit(int id, string connectionString);
}