using RawPlatform.Config.Models;

namespace RawPlatform.Config;


public static class ConfigureAppSettings
{
    public static void ConfigureOptions(this WebApplicationBuilder builder)
    {
      builder.Services.Configure<ThirdParty>(builder.Configuration.GetSection("ThirdParty"));  
    }
}
