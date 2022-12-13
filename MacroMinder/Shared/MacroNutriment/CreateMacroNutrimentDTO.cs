namespace MacroMinder.Shared.MacroNutriment;

using System.ComponentModel.DataAnnotations;

public class CreateMacroNutrimentDTO
{
    [Required]
    public required int Calories { get; set; }

    [Required]
    public required double Carbohydrates { get; set; }

    [Required]
    public required double Lipids { get; set; }

    [Required]
    public required double Proteins { get; set; }
}