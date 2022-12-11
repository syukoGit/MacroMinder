namespace MacroMinder.Server.Controllers;

using AutoMapper;
using MacroMinder.Server.Data;
using MacroMinder.Server.Entities;
using MacroMinder.Shared.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _database;

    private readonly IMapper _mapper;

    public UsersController(ApplicationDbContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    [HttpGet("")]
    public ActionResult<IEnumerable<UserDTO>> Get()
    {
        return Ok(_mapper.Map<IEnumerable<UserDTO>>(_database.Users.AsNoTracking()));
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<UserDTO>> Get(string username)
    {
        User? user = await _database.Users.AsNoTracking().FirstOrDefaultAsync(c => c.UserName == username);

        return user == null
                   ? NotFound("User not found")
                   : _mapper.Map<UserDTO>(user);
    }

    [HttpPatch("{username}")]
    public async Task<ActionResult<UserDTO>> Patch(string username, [FromBody] JsonPatchDocument<UserPatchUpdateDTO>? patchDocument)
    {
        if (patchDocument == null)
        {
            return BadRequest();
        }

        User? userFromDb = await _database.Users.FirstOrDefaultAsync(c => c.UserName == username);

        if (userFromDb == null)
        {
            return NotFound("User not found");
        }

        UserPatchUpdateDTO? userDTO = _mapper.Map<UserPatchUpdateDTO>(userFromDb);

        patchDocument.ApplyTo(userDTO);

        bool isValid = TryValidateModel(userDTO);

        if (!isValid)
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(userDTO, userFromDb);

        await _database.SaveChangesAsync();

        return CreatedAtAction("Get", new
        {
            userFromDb.UserName,
        }, _mapper.Map<UserDTO>(userFromDb));
    }
}