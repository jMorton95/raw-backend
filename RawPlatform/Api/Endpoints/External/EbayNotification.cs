using System.Text;
using RawPlatform.Modules;

namespace RawPlatform.Api.Endpoints.External;

public class EbayNotification : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("challenge", async (DatabaseLoggingService logger, HttpRequest request) =>
        {
            var body = "";
            try
            {
                body = await new StreamReader(request.Body).ReadToEndAsync();
               
                return Results.Ok();
            }
            catch (Exception ex)
            {
                await logger.LogError<EbayNotification>("Error processing the request", ex);
                return Results.StatusCode(500);
            }
            finally
            {
                var trimmedBody = new string(body.Where(x => !char.IsWhiteSpace(x)).ToArray());
                var metaDataString = CreateMetaDataString(request);
                await logger.LogInformation<EbayNotification>($"Received Challenge Body: {trimmedBody} - With MetaData: {metaDataString}");
            }
        });
    }

    private static string CreateMetaDataString(HttpRequest request)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{request.Host}{request.Path}");
        sb.Append(request.HttpContext.Connection?.RemoteIpAddress?.ToString());
        foreach (var header in request.Headers)
        {
            sb.Append($"{header.Key}: {header.Value}\n");
        }
        
        return sb.ToString();
    }
}