namespace MacroMinder.Shared.Account;

using JetBrains.Annotations;
using System.ComponentModel.DataAnnotations;

public class CreateAccountDTO
{
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [UsedImplicitly]
    public required string ConfirmPassword { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required]
    [EmailAddress]
    public required string UserName { get; set; }
}