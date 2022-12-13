namespace MacroMinder.Shared.MacroDailyReport;

using MacroMinder.Shared.Food;

public class CreateMacroDailyReportDTO
{
    public IEnumerable<FoodDTO>? Breakfast { get; set; }

    public IEnumerable<FoodDTO>? Dinner { get; set; }

    public IEnumerable<FoodDTO>? Lunch { get; set; }

    public IEnumerable<FoodDTO>? Snack { get; set; }
}