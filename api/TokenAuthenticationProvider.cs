using Microsoft.Graph;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using System.Net.Http;
using System.Threading.Tasks;

namespace Company.Function;

public class TokenAuthenticationProvider : IAuthenticationProvider
{
    private readonly string _accessToken;

    public TokenAuthenticationProvider(string accessToken)
    {
        _accessToken = accessToken;
    }

    public Task AuthenticateRequestAsync(HttpRequestMessage request)
    {
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
        return Task.CompletedTask;
    }

    public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        request.Headers["Authorization"] = [$"Bearer {_accessToken}"];
        return Task.CompletedTask;
    }
}
