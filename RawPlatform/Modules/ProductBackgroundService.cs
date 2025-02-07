namespace RawPlatform.Modules;

public class ProductBackgroundService(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _semaphore.WaitAsync(stoppingToken);
            var scope = serviceProvider.CreateScope();

            try
            {
                var service = scope.ServiceProvider.GetRequiredService<IProductEtl>();
                var result = await service.ProcessEbayProducts();
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<DatabaseLoggingService>();
                await logger.LogError<ProductBackgroundService>("Error occurred within background service", ex);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}