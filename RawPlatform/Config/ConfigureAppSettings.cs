namespace RawPlatform.Config;

public class TestSettings()
{
    public string TestValue { get; set; }
}

public static class ConfigureAppSettings
{
    public static void ConfigureOptions(this WebApplicationBuilder builder)
    {
      //builder.Services.Configure<ConnectionStringSettings>(builder.Configuration.GetSection("ConnectionString"));
      builder.Services.Configure<TestSettings>(builder.Configuration.GetSection("TestSettings"));   
    }
}
