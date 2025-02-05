using Microsoft.AspNetCore.Mvc;
using RawPlatform.Modules;

namespace RawPlatform.Api.Endpoints.Products;

public class GetAll : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("all", Handler);
    }
    
    private static async Task<Ok<Response>> Handler(
        [FromServices] IProductEtl productEtl)
    {
        var res = await productEtl.ProcessEbayProducts();
        return TypedResults.Ok(new Response("token"));
    }

    private record Response(string TokenResponse);
}