namespace MacroMinder.Server.Controllers;

using MacroMinder.Server.Entities;
using MacroMinder.Shared.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly SignInManager<User> _signInManager;

    private readonly UserManager<User> _userManager;

    public AuthenticationController(IConfiguration configuration, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _configuration = configuration;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO login)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);

        if (!result.Succeeded)
        {
            return BadRequest(new LoginResultDTO
            {
                Successful = false,
                Error = "Username and password are invalid.",
            });
        }

        Claim[] claims =
        {
            new (ClaimTypes.Name, login.Username),
        };

        SymmetricSecurityKey key = new (Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"] ?? string.Empty));
        SigningCredentials credentials = new (key, SecurityAlgorithms.HmacSha256);
        DateTime expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:ExpiryInDays"]));

        JwtSecurityToken token = new (_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: expiry, signingCredentials: credentials);

        return Ok(new LoginResultDTO
        {
            Successful = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Post([FromBody] RegisterDTO model)
    {
        User newUser = new ()
        {
            UserName = model.Username,
            Objective = new MacroNutriment
            {
                Calories = 300,
                Carbohydrates = 45,
                Proteins = 70,
                Lipids = 35,
            },
        };

        IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);

        if (!result.Succeeded)
        {
            IEnumerable<string> errors = result.Errors.Select(static x => x.Description);

            return BadRequest(new RegisterResultDTO
            {
                Successful = false,
                Errors = errors,
            });
        }

        return Ok(new RegisterResultDTO
        {
            Successful = true,
        });
    }
}