using Microsoft.AspNetCore.Mvc;
using RawPlatform.Services;

namespace RawPlatform.Api.Endpoints.External;

public class EbayChallenge : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/third-party/challenge", Handler);
    }

    private static Results<Ok<Response>, InternalServerError> Handler([FromQuery] string challenge_code, [FromServices] EbayChallengeService service)
    {
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

