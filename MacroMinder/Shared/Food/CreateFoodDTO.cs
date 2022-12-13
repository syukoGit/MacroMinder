namespace MacroMinder.Shared.Food;

using MacroMinder.Shared.MacroNutriment;
using System.ComponentModel.DataAnnotations;

public class CreateFoodDTO
{
    public string? Brand { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required CreateMacroNutrimentDTO MacroNutriment { get; set; }

    [Required]
    public double Portion { get; set; }

    public string? Unit { get; set; }
}