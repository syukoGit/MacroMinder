namespace MacroMinder.Shared.Authentication;

using System.ComponentModel.DataAnnotations;

public class LoginDTO
{
    [Required]
    public required string Password { get; set; }

    public bool RememberMe { get; set; }

    [Required]
    public required string UserName { get; set; }
}