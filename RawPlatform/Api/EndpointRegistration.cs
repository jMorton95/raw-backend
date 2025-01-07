using Microsoft.AspNetCore.Mvc;
using RawPlatform.Api.Endpoints;
using RawPlatform.Api.Endpoints.External;

namespace RawPlatform.Api;

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}

public static class EndpointRegistration
{
    public static void MapEndpoints(this WebApplication app)
    {
        var api = app.MapGroup("api/")
            .MapEndpoint<Health>();
            
        api.MapGroup("third-party/")
            .WithMetadata(new IgnoreAntiforgeryTokenAttribute())
                .MapEndpoint<Challenge>()
                .MapEndpoint<Notification>();
    }
    
    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}