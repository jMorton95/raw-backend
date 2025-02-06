using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RawPlatform.Components.Layout;
using RawPlatform.Data;

namespace RawPlatform.Api.Endpoints.Contact;

public class Submit : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("contact", Handler);
    }
    
    private static async Task<RazorComponentResult> Handler(
        [FromForm] Request request, DataContext db)
    {
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(request.MarketingUser, 
            new ValidationContext(request.MarketingUser), validationResults, true);

        if (!isValid)
        {
            return new RazorComponentResult<Components.Layout.Contact>(new {
                request.MarketingUser,
                Errors = validationResults
            });
        }

        await db.AddAsync(request.MarketingUser);
        await db.SaveChangesAsync();

        return new RazorComponentResult<ContactSubmitted>(new {
            UserName = request.MarketingUser.Name
        });
    }

    private record Request(MarketingUser MarketingUser);
}