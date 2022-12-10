namespace MacroMinder.Shared.Authentication;

using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;

public class RegisterDTO
{
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [UsedImplicitly]
    public required string ConfirmPassword { get; init; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public required string Password { get; init; }

    [Required]
    public required string Username { get; init; }
}