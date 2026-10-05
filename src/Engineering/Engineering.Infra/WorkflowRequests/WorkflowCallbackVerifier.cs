using Microsoft.Extensions.Options;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Engineering.Infra.WorkflowRequests;

/// <summary>امضای قرارداد فعلی HttpResultDelivery را روی بایت های بدنه بررسی می کند.</summary>
public sealed class WorkflowCallbackVerifier
{
    private readonly IOptionsMonitor<WorkflowIntegrationOptions> _options;

    /// <summary>Secret مشترک مقصد نتیجه را از تنظیمات محیط دریافت می کند.</summary>
    public WorkflowCallbackVerifier(IOptionsMonitor<WorkflowIntegrationOptions> options) => _options = options;

    public bool IsConfigured => _options.CurrentValue.CallbackSecret.Length >= 32;

    /// <summary>شناسه رویداد، مهلت پنج دقیقه ای و HMAC را با مقایسه ثابت زمانی کنترل می کند.</summary>
    public bool Verify(string eventId, string timestamp, string signature, string body)
    {
        if (!IsConfigured || !long.TryParse(eventId, NumberStyles.None, CultureInfo.InvariantCulture, out var id) || id <= 0 ||
            !long.TryParse(timestamp, NumberStyles.None, CultureInfo.InvariantCulture, out var seconds))
            return false;

        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        if (seconds < now - 300 || seconds > now + 300 || signature.Length != 64)
            return false;

        byte[] supplied;
        try
        {
            supplied = Convert.FromHexString(signature);
        }
        catch (FormatException)
        {
            return false;
        }

        var expected = HMACSHA256.HashData(Encoding.UTF8.GetBytes(
            _options.CurrentValue.CallbackSecret), Encoding.UTF8.GetBytes($"{timestamp}.{eventId}.{body}"));

        return CryptographicOperations.FixedTimeEquals(expected, supplied);
    }
}
