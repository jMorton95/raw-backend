using RawPlatform.Services;

namespace RawPlatform.Api.Endpoints.External;

public class EbayNotification : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/third-party/challenge", async (DatabaseLoggingService logger, HttpRequest request) =>
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
                await logger.LogInformation<EbayNotification>($"Received Challenge Body: {body}");
            }
        });
    }
}