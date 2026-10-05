
namespace Engineering.Domain.Errors;

public static class CommercialErrors
{
    public static Error ProviderError(string error)
    {
        string message = $"خطا در زیر سیستم بازرگانی رخ داده است {error}";
        return new Error("Bad.Gateway", message, 502);
    }

    public static Error ProviderError(Error? error)
    {
        string message = $"خطا در زیر سیستم بازرگانی رخ داده است {error?.Message}";
        return new Error($"Bad.Gateway.{error?.Code}", message, error?.StatusCode ?? 502);
    }
}
