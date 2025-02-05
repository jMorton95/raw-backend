using System.Net.Http.Headers;
using System.Text;
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

public class ProductApiAuthenticator(
    IOptions<ThirdParty> settings,
    DataContext db,
    HttpClient httpClient,
    DatabaseLoggingService logger)
    : IProductApiAuthenticator
{
    private readonly ThirdParty _settings = settings.Value;

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
        var token = await db.CommerceTokens.OrderByDescending(x => x.Id).LastOrDefaultAsync();
        
        var result = token != null && token.Expires > DateTime.UtcNow;

        return (result, token?.Token);
    }

    public async Task<AuthTokenResponse?> TryFetchOAuthToken()
    {
        try
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_settings.ApiClientId}:{_settings.ApiClientSecret}")));
            
            var content = new Dictionary<string, string>{
                {"grant_type", $"{_settings.GrantType}"},
                {"scopes", $"{_settings.Scopes}"}
            };
        
            var form = new FormUrlEncodedContent(content);
        
            var response = await httpClient.PostAsync($"{_settings.ApiClientAuthUrl}", form);
        
            var responseObject = await response.Content.ReadFromJsonAsync<AuthTokenResponse>();
            
            await logger.LogInformation<ProductApiAuthenticator>("Successfully Fetched OAuth token");
            
            return responseObject;
        }
        catch (Exception ex)
        {
            await logger.LogError<ProductApiAuthenticator>("Failed to fetch OAuth token", ex);
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
        
        await db.CommerceTokens.AddAsync(dbToken);

        return await db.SaveChangesAsync() > 0;
    }
}