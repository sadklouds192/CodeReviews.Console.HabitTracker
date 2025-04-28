using HabitTracker.Core.Models;

namespace HabitTracker.Core.Interfaces;

public interface IDataAccess
{
    public void InitializeDb(string connectionString);
    public List<Habit> GetHabits();
    public Habit GetHabit(int id);
    public bool InsertHabit(Habit habit);
    public bool UpdateHabit(Habit habit, int id);
    public bool DeleteHabit(int id);
    
}