using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using RawPlatform.Common.External;
using RawPlatform.Config.Models;
using RawPlatform.Data;

namespace RawPlatform.Modules;


public interface IProductApiAuthenticator
{
    Task<string?> GetOAuthToken();

    Task<(bool, string?)> TryGetDatabaseToken();

    Task<AuthTokenResponse?> TryFetchOAuthToken();
    
    Task<bool> SaveOAuthToken(AuthTokenResponse token);
}

public class ProductApiAuthenticator : IProductApiAuthenticator
{
    private readonly ThirdParty _settings;
    private readonly DataContext _db;
    private readonly HttpClient _httpClient;
    private readonly DatabaseLoggingService _logger;
    
    public ProductApiAuthenticator(
        IOptions<ThirdParty> settings,
        DataContext db,
        HttpClient httpClient,
        DatabaseLoggingService logger)
    {
        httpClient.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue($"Basic {settings.Value.ApiClientId}: {settings.Value.ApiClientSecret}");
        _settings = settings.Value;
        _db = db;
        _httpClient = httpClient;
        _logger = logger;
    }


    public async Task<string?> GetOAuthToken()
    {
        var (unexpired, dbToken) = await TryGetDatabaseToken();

        if (unexpired && dbToken != null) return dbToken;
        
        var fetchedToken = await TryFetchOAuthToken();
        
        if (fetchedToken == null) return null;
        
        await SaveOAuthToken(fetchedToken);
        
        return fetchedToken.AccessToken;
    }

    public async Task<(bool, string?)> TryGetDatabaseToken()
    {
        var token = await _db.CommerceTokens.LastOrDefaultAsync();
        
        var result = token != null && token.Expires < DateTime.UtcNow;

        return (result, token?.Token);
    }

    public async Task<AuthTokenResponse?> TryFetchOAuthToken()
    {
        try
        {
            var content = new Dictionary<string, string>{
                {"grant_type", $"{_settings.GrantType}"},
                {"scopes", $"{_settings.Scopes}"}
            };
        
            var form = new FormUrlEncodedContent(content);
        
            var response = await _httpClient.PostAsync($"{_settings.ApiClientAuthUrl}", form);
        
            var responseObject = await response.Content.ReadFromJsonAsync<AuthTokenResponse>();
            
            await _logger.LogInformation<ProductApiAuthenticator>("Successfully Fetched OAuth token");
            
            return responseObject;
        }
        catch (Exception ex)
        {
            await _logger.LogError<ProductApiAuthenticator>("Failed to fetch OAuth token", ex);
            return null;
        }
        
    }

    public async Task<bool> SaveOAuthToken(AuthTokenResponse token)
    {
        var dbToken = new CommerceToken
        {
            Token = token.AccessToken,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddSeconds(token.ExpiresIn),
        };
        
        await _db.CommerceTokens.AddAsync(dbToken);

        return await _db.SaveChangesAsync() > 0;
    }
}