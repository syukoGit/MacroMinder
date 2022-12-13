namespace MacroMinder.Server.Data;

using MacroMinder.Server.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618
public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().Navigation(static c => c.Objective).AutoInclude();

        builder.Entity<MacroDailyReport>().Navigation(static c => c.Breakfast).AutoInclude();
        builder.Entity<MacroDailyReport>().Navigation(static c => c.Dinner).AutoInclude();
        builder.Entity<MacroDailyReport>().Navigation(static c => c.Lunch).AutoInclude();
        builder.Entity<MacroDailyReport>().Navigation(static c => c.Snack).AutoInclude();

        base.OnModelCreating(builder);
    }
}