using Microsoft.AspNetCore.Mvc;
using RawPlatform.Modules;

namespace RawPlatform.Api.Endpoints.Products;

public class GetAll : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("products", Handler);
    }
    
    private static async Task<Ok<Response>> Handler(
        [FromServices] IProductApiAuthenticator authenticator)
    {
        return TypedResults.Ok(new Response("token"));
    }

    private record Response(string TokenResponse);
}