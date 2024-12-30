using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using RawPlatform.Config.Models;

namespace RawPlatform.Services;

public class EbayChallengeService(IOptions<ThirdParty> apiSettings, DatabaseLoggingService logger)
{
    private readonly ThirdParty _apiSettings = apiSettings.Value;
    public string VerifyChallenge(string challengeCode)
    {
        if (_apiSettings.ValidationToken is null || _apiSettings.HostedEndpoint is null)
        {
            _ = logger.LogCritical<EbayChallengeService>("Invalid Configuration - ValidationToken or HostedEndpoint is null");
            throw new ArgumentException("Invalid Configuration");
        }
        
        var challengeHash = IncrementalHash.CreateHash(HashAlgorithmName.SHA256); 
        challengeHash.AppendData(Encoding.UTF8.GetBytes(challengeCode)); 
        challengeHash.AppendData(Encoding.UTF8.GetBytes(_apiSettings.ValidationToken)); 
        challengeHash.AppendData(Encoding.UTF8.GetBytes(_apiSettings.HostedEndpoint));
        
        var challengeString = Convert.ToHexStringLower(challengeHash.GetHashAndReset());
        
        _ = logger.LogInformation<EbayChallengeService>($"Verify challenge: {challengeString}");
        
        return challengeString;
    }
}