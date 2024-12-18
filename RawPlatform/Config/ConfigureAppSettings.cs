namespace RawPlatform.Config;

public static class ConfigureAppSettings
{
    public static void ConfigureOptions(this WebApplicationBuilder builder)
    {
        //builder.Services.Configure<ConnectionStringSettings>(builder.Configuration.GetSection("ConnectionString"));
    }
}
