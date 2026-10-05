using Engineering.ClientSdk.Services;
using Gita.Backend.Shared.Application.Middlewares.UnhandledExceptions;
using Gita.Backend.Shared.Domain.Base;
using Gita.Backend.Shared.Domain.Errors;
using System.Text.Json;

namespace Engineering.ClientSdk.Exceptions;

public class EngineeringServiceException : Exception
{
    public Error? Error { get; }

    public EngineeringServiceException(string message) : base(message)
    {
        Error = null;
    }

    public EngineeringServiceException(Error error) : base(error.Message)
    {
        ArgumentNullException.ThrowIfNull(error);

        Error = error;
    }

    public EngineeringServiceException(ErrorModel error) : base(error.Message)
    {
        ArgumentNullException.ThrowIfNull(error);

        Error = new Error(error.Code ?? SharedErrors.UnknownError.Code,
            error.Message ?? SharedErrors.UnknownError.Message,
            error.StatusCode ?? SharedErrors.UnknownError.StatusCode);
    }

    public static async Task<EngineeringServiceException> FromHttpResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return new EngineeringServiceException("No error");
        }

        var model = await JsonSerializer.DeserializeAsync<Error>(await response.Content.ReadAsStreamAsync(),
            EngineeringClientSdkJsonSerializer.Options);

        if (model is null)
        {
            return new EngineeringServiceException("No error");
        }

        return new EngineeringServiceException(model);
    }
}
