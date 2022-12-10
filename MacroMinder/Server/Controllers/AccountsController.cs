namespace MacroMinder.Server.Controllers;

using MacroMinder.Server.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public AccountsController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
}