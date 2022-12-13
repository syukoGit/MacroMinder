namespace MacroMinder.Shared.User;

using MacroMinder.Shared.MacroNutriment;

public class UserDTO
{
    public MacroNutrimentDTO? Objective { get; set; }

    public required string Username { get; set; }
}