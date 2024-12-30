using Microsoft.AspNetCore.Mvc;
using RawPlatform.Services;

namespace RawPlatform.Api.Endpoints.External;

public class EbayChallenge : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/third-party/challenge", Handler);
    }

    private static Results<Ok<Response>, InternalServerError> Handler(HttpContext context, [FromQuery] string challenge_code, [FromServices] EbayChallengeService service)
    {
        // Log the request details
        var request = context.Request;

        // Log essential request metadata
        Console.WriteLine("Request Received:");
        Console.WriteLine($"Time: {DateTime.UtcNow}");
        Console.WriteLine($"Method: {request.Method}");
        Console.WriteLine($"Host: {request.Host}");
        Console.WriteLine($"Path: {request.Path}");
        Console.WriteLine($"Query Parameters: {request.QueryString}");
        Console.WriteLine($"Client IP: {context.Connection.RemoteIpAddress}");
    
        // Log all request headers
        foreach (var header in request.Headers)
        {
            Console.WriteLine($"{header.Key}: {header.Value}");
        }

        // Log the challenge_code query parameter
        Console.WriteLine($"Challenge Code: {challenge_code}");

        // You could also log the user agent and referer (if available)
        Console.WriteLine($"User-Agent: {request.Headers["User-Agent"]}");
        Console.WriteLine($"Referer: {request.Headers["Referer"]}");
        
        try
        {
            var challengeResponse = service.VerifyChallenge(challenge_code);
            return TypedResults.Ok(new Response(challengeResponse));
        }
        catch (Exception ex)
        {
            //TODO: Logger
            return TypedResults.InternalServerError();
        }
    }

    private record Response(string ChallengeResponse);
}

