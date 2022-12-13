namespace MacroMinder.Server.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public List<Food> Foods { get; set; } = new ();

    public List<MacroDailyReport> Journal { get; set; } = new ();

    public MacroNutriment? Objective { get; set; }
}