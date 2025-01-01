using RawPlatform.Data;

namespace RawPlatform.Services;

public class DatabaseLoggingService(DataContext db)
{
    public async Task LogInformation<TClass>(string message) => await Log<TClass>(LogLevel.Information, message);
    public async Task LogDebug<TClass>(string message) => await Log<TClass>(LogLevel.Debug, message);
    public async Task LogCritical<TClass>(string message, Exception? ex = null) => await Log<TClass>(LogLevel.Debug, message, ex);

    public async Task LogError<TClass>(string message, Exception ex) => await Log<TClass>(LogLevel.Error, message, ex);
    private async Task Log<TClass>(LogLevel level, string message, Exception? ex = null)
    {
        db.LogEntries.Add(new LogEntry
        {
            LogLevel = level,
            Message = $"[{typeof(TClass).Name}] {message}",
            Exception = ex?.ToString() ?? null,
            SavedAt = DateTime.UtcNow
        });

        await db.SaveChangesAsync();
    }
}