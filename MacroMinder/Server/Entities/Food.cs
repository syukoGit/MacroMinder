namespace MacroMinder.Server.Entities;

public class Food
{
    public string? Brand { get; set; }

    public required string Description { get; set; }

    public int Id { get; set; }

    public required MacroNutriment MacroNutriment { get; set; }

    public required double Portion { get; set; }

    public string? Unit { get; set; }
}