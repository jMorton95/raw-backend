using System.Security.Cryptography;
using System.Text;
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

    private static Results<Ok<Response>, InternalServerError> Handler([FromQuery] string challenge_code, [FromServices] EbayChallengeService service)
    {
        try
        {
            var challengeResponse = service.VerifyChallenge(challenge_code);
            return TypedResults.Ok(new Response(challengeResponse));
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError();
        }
    }

    private record Response(string ChallengeResponse);
}

public class EbayChallengeService(IOptions<ThirdParty> apiSettings)
{
    private readonly ThirdParty _apiSettings = apiSettings.Value;
    public string VerifyChallenge(string challengeCode)
    {
        if (_apiSettings.VerificationToken is null || _apiSettings.HostedEndpoint is null)
        {
            throw new ArgumentException("Invalid Configuration");   
        }
        
        var challengeHash = IncrementalHash.CreateHash(HashAlgorithmName.SHA256); 
        challengeHash.AppendData(Encoding.UTF8.GetBytes(challengeCode)); 
        challengeHash.AppendData(Encoding.UTF8.GetBytes(_apiSettings.VerificationToken)); 
        challengeHash.AppendData(Encoding.UTF8.GetBytes(_apiSettings.HostedEndpoint)); 
        return Convert.ToHexStringLower(challengeHash.GetHashAndReset());
    }
        
}