using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RawPlatform.Config.Models;

namespace RawPlatform.Api.Endpoints.External;

public class EbayChallenge : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/third-party/challenge", Handler);
    }

    private static Task<Results<Ok<Response>, UnauthorizedHttpResult>> Handler([FromQuery] string challenge_code)
    {
        
    }

    private record Response(string ChallengeResponse);
}

public class EbayChallengeService(IOptions<ThirdParty> apiSettings)
{
    
}