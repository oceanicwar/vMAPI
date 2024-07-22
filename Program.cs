using Microsoft.EntityFrameworkCore;

using vMAPI.Database;

namespace vMAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<MangosRealmDbContext>(db =>
        {
            string? mySqlConn = builder.Configuration.GetConnectionString("MySqlRealm");
            if (string.IsNullOrEmpty(mySqlConn))
            {
                throw new NullReferenceException("MySql Connection String was null or empty.");
            }

            ServerVersion? mySqlVersion = ServerVersion.AutoDetect(mySqlConn);
            if (mySqlVersion is null)
            {
                throw new NullReferenceException("Failed to auto-detect MySql version.");
            }

            db.UseMySql(mySqlConn, mySqlVersion);
        });

        builder.Services.AddDbContext<MangosCharactersDbContext>(db =>
        {
            string? mySqlConn = builder.Configuration.GetConnectionString("MySqlCharacters");
            if (string.IsNullOrEmpty(mySqlConn))
            {
                throw new NullReferenceException("MySql Connection String was null or empty.");
            }

            ServerVersion? mySqlVersion = ServerVersion.AutoDetect(mySqlConn);
            if (mySqlVersion is null)
            {
                throw new NullReferenceException("Failed to auto-detect MySql version.");
            }

            db.UseMySql(mySqlConn, mySqlVersion);
        });

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
