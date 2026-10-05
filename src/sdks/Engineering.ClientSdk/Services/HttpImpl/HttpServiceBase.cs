using Engineering.ClientSdk.Exceptions;
using Engineering.ClientSdk.Helpers;
using Engineering.ClientSdk.Models;
using Gita.Backend.Shared.Domain.Base;
using Gita.Backend.Shared.Domain.Errors;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Engineering.ClientSdk.Services.HttpImpl;

public abstract class HttpServiceBase(
    HttpClient httpClient,
    IEngineeringClientSdkJsonSerializer serializer,
    ILogger logger
)
{
    protected async Task<TResponse> SendWithResponse<TResponse>(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {

        var response = await httpClient.SendAsync(request, cancellationToken);
        await response.EnsureSuccessfulServiceCall(logger, cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<EngineeringCanonicalResult<TResponse>>(cancellationToken);
        if (result?.IsSuccess is null || !result.IsSuccess.Value || result.Value is null)
        {
            if (result?.Error is not null)
            {
                var ex = new EngineeringServiceException(result.Error);
                logger.LogError(
                    ex,
                    "SendWithResponse returned an invalid response, statusCode: {StatusCode}, errorStatus: {ErrorStatusCode}, errorCode: {ErrorCode}, errorMessage: {ErrorMessage}",
                    response.StatusCode,
                    result.Error.StatusCode,
                    result.Error.Code,
                    result.Error.Message
                );
                throw ex;
            }

            throw new EngineeringServiceException(result?.Error.Adapt<Error>() ?? SharedErrors.UnknownError);
        }

        return result.Value;
    }

    protected HttpRequestMessage GetRequest(HttpMethod method, string url, string token, object request)
    {
        if (method == HttpMethod.Get || method == HttpMethod.Delete)
        {
            return new HttpRequestMessage(method, InternalHelpers.ToUriParameters(url, request))
            {
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", token)
                }
            };
        }

        return new HttpRequestMessage(method, url)
        {
            Headers =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", token)
            },
            Content = JsonContent(request)
        };
    }

    protected StringContent JsonContent(object obj)
    {
        return new StringContent(serializer.Serialize(obj), Encoding.UTF8, "application/json");
    }
}
