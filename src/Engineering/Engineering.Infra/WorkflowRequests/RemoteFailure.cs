using System.Text.Json;

namespace Engineering.Infra.WorkflowRequests;

/// <summary>خطای مقصد را بدون ذخیره بدنه خام پاسخ به اطلاعات قابل پیگیری تبدیل می کند.</summary>
internal static class RemoteFailure
{
    /// <summary>تنها قرارداد خطای JSON و شناسه پیگیری مقصد را با طول محدود می خواند.</summary>
    public static async Task<HttpRequestException> Read(HttpResponseMessage response, CancellationToken ct)
    {
        var detail = "پاسخ ناموفق مقصد";
        try
        {
            await using var stream = await response.Content.ReadAsStreamAsync(ct);
            var buffer = new byte[16385];
            var count = 0;
            while (count < buffer.Length)
            {
                var read = await stream.ReadAsync(buffer.AsMemory(count), ct);
                if (read == 0) break;
                count += read;
            }
            if (count < buffer.Length)
            {
                using var json = JsonDocument.Parse(buffer.AsMemory(0, count));
                var root = json.RootElement;
                if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("error", out var error) && error.ValueKind == JsonValueKind.Object)
                {
                    var code = Field(error, "code");
                    var message = (int)response.StatusCode < 500 ? Field(error, "message") : "خطای داخلی مقصد؛ لاگ مقصد بررسی شود";
                    detail = $"{code}: {message}";
                }
            }
        }
        catch (JsonException) { }
        catch (IOException) { }
        var trace = response.Headers.TryGetValues("X-Correlation-Id", out var values) ? Clean(values.FirstOrDefault()) : "unavailable";
        return new RemoteHttpException($"[HTTP:{(int)response.StatusCode}] {detail}; TraceId={trace}", null, response.StatusCode);
    }

    /// <summary>مقدار متنی شناخته شده را از قرارداد خطا دریافت می کند.</summary>
    private static string Field(JsonElement value, string name) =>
        value.TryGetProperty(name, out var field) && field.ValueKind == JsonValueKind.String ? Clean(field.GetString()) : "unknown";

    /// <summary>طول و نویسه های کنترلی متن مقصد را محدود می کند.</summary>
    private static string Clean(string? value) => new((value ?? "unknown").Where(c => !char.IsControl(c)).Take(500).ToArray());
}

/// <summary>خطای HTTP حاوی فقط اطلاعات پالایش شده قرارداد مقصد است.</summary>
internal sealed class RemoteHttpException(string message, Exception? inner, System.Net.HttpStatusCode status)
    : HttpRequestException(message, inner, status);

