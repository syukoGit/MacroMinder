namespace MacroMinder.Server.Controllers;

using MacroMinder.Shared.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AccountsController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegisterDTO model)
    {
        IdentityUser newUser = new () { UserName = model.UserName };

        IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);

        if (!result.Succeeded)
        {
            IEnumerable<string> errors = result.Errors.Select(static x => x.Description);

            return Ok(new RegisterResultDTO { Successful = false, Errors = errors });
        }

        return Ok(new RegisterResultDTO { Successful = true });
    }
}