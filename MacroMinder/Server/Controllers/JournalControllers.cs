namespace MacroMinder.Server.Controllers;

using AutoMapper;
using MacroMinder.Server.Data;
using MacroMinder.Server.Entities;
using MacroMinder.Shared.MacroDailyReport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("/api/{username}/[Controller]/{date:datetime}")]
[ApiController]
[Authorize]
public class JournalControllers : ControllerBase
{
    private readonly ApplicationDbContext _database;

    private readonly IMapper _mapper;

    public JournalControllers(ApplicationDbContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<MacroDailyReportDTO>> Get(string username, DateTime date)
    {
        User? user = await _database.Users.Include(static c => c.Journal).AsNoTracking().FirstOrDefaultAsync(c => c.UserName == username);

        if (user == null)
        {
            return NotFound("User not found");
        }

        MacroDailyReport? dailyReport = user.Journal.FirstOrDefault(c => c.Date == date);

        return dailyReport == null
                   ? Ok(new MacroDailyReportDTO())
                   : _mapper.Map<MacroDailyReportDTO>(dailyReport);
    }

    [HttpPost]
    public async Task<ActionResult<MacroDailyReportDTO>> Post(string username, DateTime date, [FromBody] CreateMacroDailyReportDTO createMacroDailyReport)
    {
        User? user = await _database.Users.Include(static c => c.Journal).AsNoTracking().FirstOrDefaultAsync(c => c.UserName == username);

        if (user == null)
        {
            return NotFound("User not found");
        }

        MacroDailyReport macroDailyReport = _mapper.Map<MacroDailyReport>(createMacroDailyReport);
        macroDailyReport.Date = date;
        user.Journal.Add(macroDailyReport);

        _database.Entry(user).State = EntityState.Modified;

        await _database.SaveChangesAsync();

        MacroDailyReportDTO? macroDailyReportDTO = _mapper.Map<MacroDailyReportDTO>(macroDailyReport);

        return CreatedAtAction("Get", new
        {
            username = user.UserName,
            date,
        }, macroDailyReportDTO);
    }
}