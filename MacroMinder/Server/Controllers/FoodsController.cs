namespace MacroMinder.Server.Controllers;

using AutoMapper;
using MacroMinder.Server.Data;
using MacroMinder.Server.Entities;
using MacroMinder.Shared.Food;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/users/{username}/[Controller]")]
[ApiController]
[Authorize]
public class FoodsController : ControllerBase
{
    private readonly ApplicationDbContext _database;

    private readonly IMapper _mapper;

    public FoodsController(ApplicationDbContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FoodDTO>>> Get(string username)
    {
        User? user = await _database.Users.Include(static c => c.Foods).AsNoTracking().FirstOrDefaultAsync(c => c.UserName == username);

        return user == null
                   ? NotFound("User not found")
                   : Ok(_mapper.Map<IEnumerable<FoodDTO>>(user.Foods));
    }

    [HttpGet("{foodId:int}")]
    public async Task<ActionResult<FoodDTO>> Get(string username, int foodId)
    {
        User? user = await _database.Users.Include(static c => c.Foods).AsNoTracking().FirstOrDefaultAsync(c => c.UserName == username);

        if (user == null)
        {
            return NotFound("User not found");
        }

        Food? food = user.Foods.FirstOrDefault(c => c.Id == foodId);

        return food == null
                   ? NotFound("Food not found")
                   : _mapper.Map<FoodDTO>(food);
    }

    [HttpPost]
    public async Task<ActionResult<FoodDTO>> Post(string username, [FromBody] CreateFoodDTO createFoodDTO)
    {
        User? user = _database.Users.FirstOrDefault(c => c.UserName == username);

        if (user == null)
        {
            return NotFound("User not found");
        }

        Food food = _mapper.Map<Food>(createFoodDTO);
        user.Foods.Add(food);

        _database.Entry(user).State = EntityState.Modified;

        await _database.SaveChangesAsync();

        FoodDTO? foodDTO = _mapper.Map<FoodDTO>(food);

        return CreatedAtAction("Get", new
        {
            username = user.UserName,
            foodId = food.Id,
        }, foodDTO);
    }
}