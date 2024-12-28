using RawPlatform.Data;

namespace RawPlatform.Services;

public static class MigrationService
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        
        await dbContext.Database.MigrateAsync();
    }
}