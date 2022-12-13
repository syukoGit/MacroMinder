namespace MacroMinder.Shared.Food;

using MacroMinder.Shared.MacroNutriment;

public class FoodDTO
{
    public string? Brand { get; set; }

    public required string Description { get; set; }

    public int Id { get; set; }

    public required MacroNutrimentDTO MacroNutriment { get; set; }

    public required double Portion { get; set; }

    public string? Unit { get; set; }
}