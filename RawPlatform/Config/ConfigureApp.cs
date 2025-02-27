﻿using RawPlatform.Data;
using RawPlatform.Modules;

namespace RawPlatform.Config;

public static class ConfigureApp
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
       
        var dbSettings = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
        var connectionString =
            $"Host={dbSettings?.Host};Port={dbSettings?.Port};Database={dbSettings?.Database};Username={dbSettings?.Username};Password={dbSettings?.Password};Include Error Detail=true";
        builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));
        
        return builder;
    }

    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<DatabaseLoggingService>();
        builder.Services.AddScoped<IProductApiAuthenticator, ProductApiAuthenticator>();
        builder.Services.AddScoped<IProductEtl, ProductEtl>();
        builder.Services.AddScoped<EbayChallenge>();
        return builder;
    }
    
    private class DatabaseSettings()
    {
        public string? Host { get; init; }
        public string? Port { get; init; }
        public string? Database { get; init; }
        public string? Username { get; init; }
        public string? Password { get; init; }
    }
}