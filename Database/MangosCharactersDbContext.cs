using Microsoft.EntityFrameworkCore;

using vMAPI.Database.Models.Characters;

namespace vMAPI.Database;

public class MangosCharactersDbContext : DbContext
{
    public DbSet<Character> Characters { get; set; }

    public MangosCharactersDbContext(DbContextOptions<MangosCharactersDbContext> options) : base(options) { }
}
