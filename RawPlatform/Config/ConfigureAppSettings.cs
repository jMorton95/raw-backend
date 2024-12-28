namespace RawPlatform.Config;

public class ThirdParty()
{
    public string? ApiKey { get; init; }
}


public static class ConfigureAppSettings
{
    public static void ConfigureOptions(this WebApplicationBuilder builder)
    {
      builder.Services.Configure<ThirdParty>(builder.Configuration.GetSection("ThirdParty"));  
    }
}
