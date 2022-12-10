namespace MacroMinder.Shared.Food;

using System.ComponentModel.DataAnnotations;

public class CreateFoodDTO
{
    public string? Brand { get; set; }

    [Required]
    public required int Calories { get; set; }

    [Required]
    public required double Carbohydrates { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required double Lipids { get; set; }

    [Required]
    public double Portion { get; set; }

    [Required]
    public required double Proteins { get; set; }

    public string? Unit { get; set; }
}