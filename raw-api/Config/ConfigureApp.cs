namespace RAWAPI.Config;

public static class ConfigureApp
{
    public static void AddDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
        builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));
    }
}