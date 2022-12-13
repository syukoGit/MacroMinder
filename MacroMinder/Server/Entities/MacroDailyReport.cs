namespace MacroMinder.Server.Entities;

using System.ComponentModel.DataAnnotations.Schema;

public class MacroDailyReport
{
    [ForeignKey("BreakfastFoods")]
    public IEnumerable<Food>? Breakfast { get; set; }

    public DateTime Date { get; set; }

    [ForeignKey("DinnerFoods")]
    public IEnumerable<Food>? Dinner { get; set; }

    public int Id { get; set; }

    [ForeignKey("LunchFood")]
    public IEnumerable<Food>? Lunch { get; set; }

    [ForeignKey("SnackFood")]
    public IEnumerable<Food>? Snack { get; set; }
}