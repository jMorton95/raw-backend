using RawPlatform.Data;

namespace RawPlatform.Services;

public class LoggingService(string categoryName, IServiceProvider serviceProvider) : ILogger
{
    private readonly string _categoryName = categoryName;

    public IDisposable BeginScope<TState>(TState state) => null!;

    public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        var message = formatter(state, exception);
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();

        context.LogEntries.Add(new LogEntry
        {
            LogLevel = logLevel,
            Message = message
        });

        context.SaveChanges();
    }
}

public class LoggerProvider : ILoggerProvider
{
    private readonly IServiceProvider _serviceProvider;

    public LoggerProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ILogger CreateLogger(string categoryName) =>
        new LoggingService(categoryName, _serviceProvider);

    public void Dispose() { }
}
