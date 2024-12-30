namespace RawPlatform.Api.Endpoints.External;

public class EbayNotification : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/third-party/challenge", TypedResults.Ok);
    }
}