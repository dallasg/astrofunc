using Microsoft.Graph;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Company.Function;

public class SimpleAuthProvider
{
    private readonly string _accessToken;

    public SimpleAuthProvider(string accessToken)
    {
        _accessToken = accessToken;
    }

    public Task AuthenticateRequestAsync(HttpRequestMessage request)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        return Task.CompletedTask;
    }
}
