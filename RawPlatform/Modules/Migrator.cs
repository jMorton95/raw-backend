using RawPlatform.Data;

namespace RawPlatform.Modules;

public static class Migrator
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        
        await dbContext.Database.MigrateAsync();
    }
}