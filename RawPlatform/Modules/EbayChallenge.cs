﻿using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using RawPlatform.Config.Models;

namespace RawPlatform.Modules;

public class EbayChallenge(IOptions<ThirdParty> apiSettings, DatabaseLoggingService logger)
{
    private readonly ThirdParty _apiSettings = apiSettings.Value;
    public async Task<string> VerifyChallenge(string challengeCode)
    {
        if (_apiSettings.ValidationToken is null || _apiSettings.HostedEndpoint is null)
        {
            await logger.LogCritical<EbayChallenge>("Invalid Configuration - ValidationToken or HostedEndpoint is null");
            throw new ArgumentException("Invalid Configuration");
        }
        
        var challengeHash = IncrementalHash.CreateHash(HashAlgorithmName.SHA256); 
        challengeHash.AppendData(Encoding.UTF8.GetBytes(challengeCode)); 
        challengeHash.AppendData(Encoding.UTF8.GetBytes(_apiSettings.ValidationToken)); 
        challengeHash.AppendData(Encoding.UTF8.GetBytes(_apiSettings.HostedEndpoint));
        
        var challengeString = Convert.ToHexStringLower(challengeHash.GetHashAndReset());
        
        await logger.LogInformation<EbayChallenge>($"Verify challenge: {challengeString}");
        
        return challengeString;
    }
}