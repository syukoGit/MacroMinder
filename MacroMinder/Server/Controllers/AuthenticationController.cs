namespace MacroMinder.Server.Controllers;

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

    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthenticationController(IConfiguration configuration, SignInManager<IdentityUser> signInManager)
    {
        _configuration = configuration;
        _signInManager = signInManager;
    }

    [HttpPost]
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
}