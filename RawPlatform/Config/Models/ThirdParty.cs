namespace RawPlatform.Config.Models;

public class ThirdParty
{
    public string? ApiClientId { get; init; }
    public string? ApiClientSecret { get; init; }
    public string? ApiClientAuthUrl {get; init; }
    public string? ValidationToken { get; init; }
    public string? HostedEndpoint {get; init; }
    public string? ElectedSellerId { get; init; }
    public string? ProductQueryUrl { get; init; }
    public string? ProductFilterString { get; init; }
    public string? ProductMarketPlaceId { get; init; }
    public string? GrantType { get; init; }
    public string? Scopes { get; init; }
}