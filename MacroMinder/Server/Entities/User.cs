namespace MacroMinder.Server.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public List<Food> Foods { get; set; } = new ();
}