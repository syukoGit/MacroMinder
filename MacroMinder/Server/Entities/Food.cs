namespace MacroMinder.Server.Entities;

public class Food
{
    public string? Brand { get; set; }

    public required int Calories { get; set; }

    public required double Carbohydrates { get; set; }

    public required string Description { get; set; }

    public int Id { get; set; }

    public required double Lipids { get; set; }

    public required double Portion { get; set; }

    public required double Proteins { get; set; }

    public string? Unit { get; set; }
}