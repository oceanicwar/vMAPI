using Microsoft.EntityFrameworkCore;

namespace vMAPI.Database;

public class MangosDbFactory
{
    private readonly IConfiguration config;

    public MangosDbFactory(IConfiguration config)
    {
        this.config = config;
    }

    public MangosCharactersDbContext? CreateCharactersDbContext(int realmId)
    {
        var connectionString = config[$"Realms:{realmId}:ConnectionStrings:MySqlCharacters"];
        if(connectionString is null)
        {
            return null;
        }

        var optionsBuilder = new DbContextOptionsBuilder<MangosCharactersDbContext>();
        var dbContext = new MangosCharactersDbContext(connectionString, optionsBuilder.Options);

        return dbContext;
    }
}
