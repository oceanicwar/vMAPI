using Microsoft.EntityFrameworkCore;

namespace vMAPI.Database;

public class MangosDbFactory
{
    private readonly IConfiguration config;
    private readonly ILogger<MangosDbFactory> logger;

    public MangosDbFactory(IConfiguration config, ILogger<MangosDbFactory> logger)
    {
        this.config = config;
        this.logger = logger;
    }

    public MangosCharactersDbContext? CreateCharactersDbContext(int realmId)
    {
        var connectionString = config[$"Realms:{realmId}:ConnectionStrings:MySqlCharacters"];
        if(string.IsNullOrWhiteSpace(connectionString))
        {
            logger.LogCritical("Connection String for MysqlCharacters was null or empty, check appsettings.json to ensure the configuration is correct.");
            return null;
        }

        var optionsBuilder = new DbContextOptionsBuilder<MangosCharactersDbContext>();
        var dbContext = new MangosCharactersDbContext(connectionString, optionsBuilder.Options);

        return dbContext;
    }
}
