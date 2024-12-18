namespace RawPlatform.Api.Endpoints;

public class Health : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/health", () => "Running");
    }
}