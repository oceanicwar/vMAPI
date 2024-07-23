using Microsoft.EntityFrameworkCore;

using vMAPI.Database.Models.Realm;

namespace vMAPI.Database;

public class MangosRealmDbContext : DbContext
{
    private readonly IConfiguration config;
    private readonly ILogger<MangosRealmDbContext> logger;

    public DbSet<Realmlist> Realms { get; set; }

    public MangosRealmDbContext(DbContextOptions<MangosRealmDbContext> options, 
        IConfiguration config, ILogger<MangosRealmDbContext> logger) : base(options) 
    {
        this.config = config;
        this.logger = logger;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        string? mySqlConn = config.GetConnectionString("MySqlRealm");
        if (string.IsNullOrEmpty(mySqlConn))
        {
            logger.LogCritical("MySqlRealm Connection String was null or empty.");
            return;
        }

#if DEBUG
        logger.LogInformation($"Using: {mySqlConn}");
#endif

        ServerVersion? mySqlVersion = ServerVersion.AutoDetect(mySqlConn);
        if (mySqlVersion is null)
        {
            logger.LogCritical("Failed to auto-detect MySqlRealm MySql version.");
            return;
        }

        builder.UseMySql(mySqlConn, mySqlVersion);
    }
}
