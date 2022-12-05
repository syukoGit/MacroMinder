namespace MacroMinder.Shared.Authentication;

public class LoginResultDTO
{
    public string? Error { get; set; }

    public bool Successful { get; set; }

    public string? Token { get; set; }
}