using Microsoft.AspNetCore.Mvc;
using RawPlatform.Services;

namespace RawPlatform.Api.Endpoints.External;

public class EbayChallenge : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/third-party/challenge", Handler);
    }

    private static async Task<Results<Ok<Response>, InternalServerError>> Handler(
        [FromQuery] string challenge_code,
        [FromServices] EbayChallengeService service,
        [FromServices] DatabaseLoggingService logger)
    {
        try
        {
            var challengeResponse = await service.VerifyChallenge(challenge_code);
            return TypedResults.Ok(new Response(challengeResponse));
        }
        catch (Exception ex)
        {
            await logger.LogCritical<EbayChallenge>("Failed to verify EbayChallenge", ex);
            return TypedResults.InternalServerError();
        }
    }

    private record Response(string ChallengeResponse);
}

