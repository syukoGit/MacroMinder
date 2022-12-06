namespace MacroMinder.Shared.Authentication;

public class RegisterResultDTO
{
    public IEnumerable<string>? Errors { get; set; }

    public bool Successful { get; set; }
}