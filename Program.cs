using Microsoft.EntityFrameworkCore;

using vMAPI.Database;

namespace vMAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        builder.Services.AddDbContext<MangosRealmDbContext>();
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
