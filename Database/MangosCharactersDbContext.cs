using Microsoft.EntityFrameworkCore;

using vMAPI.Database.Models.Characters;

namespace vMAPI.Database;

public class MangosCharactersDbContext : DbContext
{
    public DbSet<Character> Characters { get; set; }

    private string connectionString;

    public MangosCharactersDbContext(string connectionString, DbContextOptions<MangosCharactersDbContext> options) : base(options) 
    {
        this.connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var mysqlVersion = ServerVersion.AutoDetect(connectionString);
        optionsBuilder.UseMySql(connectionString, mysqlVersion);
    }
}
