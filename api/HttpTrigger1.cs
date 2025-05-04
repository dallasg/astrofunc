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
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        foreach (var header in req.Headers)
        {
            _logger.LogInformation("Header: {Key}: {Value}", header.Key, header.Value);
        }

        string clientId = Environment.GetEnvironmentVariable("CLIENT_ID")!;
        string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET")!;
        string tenantId = Environment.GetEnvironmentVariable("TENANT_ID")!;

        // 1. Extract ID token from incoming headers
        var userToken = req.Headers["X-MS-AUTH-TOKEN"].FirstOrDefault();
        // if (string.IsNullOrEmpty(userToken))
        //     return new UnauthorizedResult();

        var credential = new OnBehalfOfCredential(
            tenantId,
            clientId,
            clientSecret,
            userToken);

        // 3. Create Graph client with required scopes
        var graphClient = new GraphServiceClient(credential, ["https://graph.microsoft.com/.default"]);

        // 4. Call Microsoft Graph
        var me = await graphClient.Me.GetAsync();

        return new OkObjectResult(me);
    }
}
