using RawPlatform.Api.Endpoints;

namespace RawPlatform.Api;

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}

public static class EndpointRegistration
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGroup("api").MapEndpoint<Health>();
            //RequireRateLimiting()
    }
    
    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}