using RawPlatform.Config.Models;

namespace RawPlatform.Config;


public static class ConfigureAppSettings
{
    public static WebApplicationBuilder AddOptions(this WebApplicationBuilder builder)
    {
       builder.Services.Configure<ThirdParty>(builder.Configuration.GetSection("ThirdParty"));
       return builder;
    }
}
