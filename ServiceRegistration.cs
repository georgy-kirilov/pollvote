using Microsoft.EntityFrameworkCore;

using MyWebApp.Data;

namespace MyWebApp;

public static class ServiceRegistration
{
    public static IServiceCollection AddCatalogueDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        const string DatabaseUrlSection = "DATABASE_URL";

        var databaseUrl = configuration.GetValue<string>(DatabaseUrlSection)
            ?? throw new ArgumentException($"Failed to load '{DatabaseUrlSection}' from configuration.");

        var uri = new Uri(databaseUrl);
        var arguments = uri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

        var host = uri.Host;
        var database = uri.AbsolutePath.TrimStart('/');
        var user = arguments[0];
        var password = arguments[1];
        var port = uri.Port.ToString();

        var connectionString = $"Host={host};Database={database};Username={user};Password={password};Port={port}";

        services.AddDbContext<CatalogueDbContext>(options => options
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention());

        return services;
    }
}
