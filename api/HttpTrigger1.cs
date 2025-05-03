using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace Company.Function
{
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

            // string userAccessToken = req.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // string clientId = Environment.GetEnvironmentVariable("CLIENT_ID")!;
            // string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET")!;
            // string tenantId = Environment.GetEnvironmentVariable("TENANT_ID")!;

            // var oboCredential = new OnBehalfOfCredential(
            //     tenantId,
            //     clientId,
            //     clientSecret,
            //     userAccessToken
            // );

            // var graphClient = new GraphServiceClient(oboCredential);

            // var me = await graphClient.Me.GetAsync();

            // return new OkObjectResult(me);

            return new OkObjectResult("Azure Functions");
        }
    }
}
