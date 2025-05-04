using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;

namespace Company.Function;

public class HttpTrigger1
{
    private readonly ILogger<HttpTrigger1> _logger;

    public HttpTrigger1(ILogger<HttpTrigger1> logger)
    {
        _logger = logger;
    }

    [Function("getme")]
    public async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("Processing request for /getme.");

        // Extract Bearer token from custom header
        if (!req.Headers.TryGetValue("X-MS-AUTH-TOKEN", out var authHeader) || string.IsNullOrWhiteSpace(authHeader))
        {
            _logger.LogWarning("Missing or empty X-MS-AUTH-TOKEN header.");
            return new UnauthorizedResult();
        }

        var token = authHeader.ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);

        try
        {
            _logger.LogInformation("Initializing GraphServiceClient...");

            string userAccessToken = req.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var tenantId = Environment.GetEnvironmentVariable("TENANT_ID");
            var clientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
            var clientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET");
            var userAssertionToken = userAccessToken;

            var onBehalfOfCredential = new OnBehalfOfCredential(
                tenantId,
                clientId,
                clientSecret,
                userAssertionToken
            );

            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var graphClient = new GraphServiceClient(onBehalfOfCredential, scopes);

            _logger.LogInformation("Calling Microsoft Graph /me endpoint...");

            var me = await graphClient.Me.GetAsync();

            _logger.LogInformation("Successfully retrieved user: {DisplayName} ({UserPrincipalName})", me?.DisplayName, me?.UserPrincipalName);

            return new OkObjectResult(me);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing /getme request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
