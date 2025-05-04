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
        var userToken = req.Headers["X-MS-AUTH-TOKEN"].ToString().Replace("Bearer ", "");

        var authProvider = new TokenAuthenticationProvider(userToken);
        var graphClient = new GraphServiceClient(authProvider);

        // Example: Get current user info
        var me = await graphClient.Me.GetAsync();
        return new OkObjectResult(me);
    }
}
