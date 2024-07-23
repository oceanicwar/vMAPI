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

        builder.Services.AddTransient<MangosDbFactory>();

        builder.Services.AddControllers();

        builder.Services.AddResponseCaching();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();
        app.UseResponseCaching();

        app.MapControllers();

        app.Run();
    }
}
