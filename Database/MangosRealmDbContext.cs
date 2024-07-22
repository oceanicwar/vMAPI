using Microsoft.EntityFrameworkCore;

using vMAPI.Database.Models.Realm;

namespace vMAPI.Database;

public class MangosRealmDbContext : DbContext
{
    public DbSet<Realmlist> Realms { get; set; }

    public MangosRealmDbContext(DbContextOptions<MangosRealmDbContext> options) : base(options) { }
}
