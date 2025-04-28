namespace HabitTracker.Core.Models;

public class Habit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public DateTime DateTracked { get; set; }
}