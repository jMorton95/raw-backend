namespace RawPlatform.Modules;

public class ProductBackgroundService(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _semaphore.WaitAsync(stoppingToken);
            
            try
            {
                var scope = serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IProductEtl>();
                var result = await service.ProcessEbayProducts();
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
            catch(Exception ex)
            {
                
            }
        }
    }
}