using RawPlatform.Data;
using RawPlatform.Services;

namespace RawPlatform.Config;

public static class ConfigureApp
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
        builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));
        return builder;
    }

    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ProductService>();
        return builder;
    }
}