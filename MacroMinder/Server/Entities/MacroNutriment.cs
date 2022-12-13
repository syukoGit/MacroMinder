namespace MacroMinder.Server.Entities;

public class MacroNutriment
{
    public required int Calories { get; set; }

    public required double Carbohydrates { get; set; }

    public int Id { get; set; }

    public required double Lipids { get; set; }

    public required double Proteins { get; set; }
}