using Microsoft.AspNetCore.Mvc;
using RawPlatform.Data;
using RawPlatform.Modules;

namespace RawPlatform.Api.Endpoints.Products;

public class GetAll : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("all", Handler);
    }
    
    private static async Task<Ok<Response>> Handler(
        DataContext db)
    {
        var products = await db.Products.ToListAsync();
        return TypedResults.Ok(new Response(products));
    }

    private record Response(List<Product> products);
}