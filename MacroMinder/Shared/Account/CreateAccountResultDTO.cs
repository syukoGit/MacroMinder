namespace MacroMinder.Shared.Account;

public class CreateAccountResultDTO
{
    public IEnumerable<string>? Errors { get; set; }

    public bool Successful { get; set; }
}