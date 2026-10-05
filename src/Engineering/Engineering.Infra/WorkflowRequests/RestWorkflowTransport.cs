using Microsoft.Extensions.Logging;
using IdentityServer.ClientSdk.Services;
using IdentityServer.ClientSdk.IdentityClient;
using System.Net.Http.Headers;
using Engineering.Application.Services.WorkflowRequests;
using Engineering.Domain.Entities.WorkflowRequests;
using Engineering.Domain.Entities.WorkflowRequests.Contracts;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Engineering.Infra.WorkflowRequests;

/// <summary>پیام شروع را با توکن سرویس برای شرکت پیام و بدون ذخیره توکن در Outbox ارسال می کند.</summary>
public sealed class RestWorkflowTransport : IWorkflowTransport
{
    private readonly HttpClient _client;
    private readonly ILogger<RestWorkflowTransport> _logger;
    private readonly IClientCredentialsTokenProvider _tokens;
    private readonly IOptionsMonitor<WorkflowIntegrationOptions> _options;

    /// <summary>HttpClient، تنظیمات و صادرکننده توکن سرویس از SDK هویت را دریافت می کند.</summary>
    public RestWorkflowTransport(
        HttpClient client,
        IOptionsMonitor<WorkflowIntegrationOptions> options,
        IClientCredentialsTokenProvider tokens, ILogger<RestWorkflowTransport> logger)
    {
        _client = client;
        _logger = logger;
        _tokens = tokens;
        _options = options;
    }

    /// <summary>پیام ثابت را با توکن مختص شرکت ارسال می کند؛ کش مشترک SDK بین شرکت ها استفاده نمی شود و رسید با تلاش تطبیق داده می شود.</summary>
    public async Task<WorkflowStartAcceptance> Start(WorkflowOutbox message, CT ct)
    {
        var stage = "Start.Validation";
        try
        {
            if (message.MessageType != WorkflowOutboxTypes.StartWorkflowV1)
                throw new InvalidOperationException("نوع پیام خروجی پشتیبانی نمی شود.");

            if (message.CompanyId <= 0)
                throw new InvalidOperationException("شرکت پیام خروجی معتبر نیست.");

            using var sentBody = JsonDocument.Parse(message.PayloadJson);
            Guid? clientRequestId = null;
            if (TryProperty(sentBody.RootElement, "clientRequestId", out var sentId) && sentId.ValueKind != JsonValueKind.Null)
            {
                if (sentId.ValueKind != JsonValueKind.String || !sentId.TryGetGuid(out var parsed) || parsed != message.RequestId)
                    throw new InvalidOperationException("شناسه تلاش در پیام خروجی سازگار نیست.");
                clientRequestId = parsed;
            }

            var settings = _options.CurrentValue;
            if (!Uri.TryCreate(settings.BaseUrl, UriKind.Absolute, out var baseUri) ||
                (baseUri.Scheme != Uri.UriSchemeHttps && !(baseUri.Scheme == Uri.UriSchemeHttp && baseUri.IsLoopback)) ||
                !Uri.TryCreate(baseUri, settings.StartPath, out var uri) || uri.Authority != baseUri.Authority || uri.Scheme != baseUri.Scheme)
                throw new InvalidOperationException("نشانی Workflow معتبر نیست؛ HTTP فقط برای localhost مجاز است.");

            using var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(message.PayloadJson, Encoding.UTF8, "application/json")
            };
            request.Headers.Add("Idempotency-Key", message.RequestId.ToString("D"));

            stage = "Identity.Token";
            var token = await _tokens.GetToken(new ClientCredentialsTokenRequest
            {
                CompanyId = message.CompanyId,
                Scopes = ["workflow.start"],
                UseCache = false
            }, ct);
            var accessToken = token.AccessToken;
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new InvalidOperationException("توکن سرویس مهندسی دریافت نشد.");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            stage = "Workflow.Start";
            using var response = await _client.SendAsync(request, ct);
            if (!response.IsSuccessStatusCode)
                throw await RemoteFailure.Read(response, ct);

            stage = "Workflow.Acceptance";
            using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync(ct));
            var data = body.RootElement;
            // GetHttpResponse در سرویس مقصد Result<T> را با Value برمی گرداند.
            if (TryProperty(data, "isSuccess", out var success) && success.ValueKind != JsonValueKind.True)
                throw await RemoteFailure.Read(response, ct);

            if (TryProperty(data, "value", out var wrapped))
                data = wrapped;

            if (!TryProperty(data, "id", out var id) || !id.TryGetInt64(out var instanceId) || instanceId <= 0 ||
                !TryProperty(data, "businessKey", out var key) || key.ValueKind != JsonValueKind.String)
                throw new InvalidOperationException("رسید پذیرش Workflow معتبر نیست.");

            Guid? returnedClientId = null;
            if (TryProperty(data, "clientRequestId", out var returnedId) && returnedId.ValueKind != JsonValueKind.Null)
            {
                if (returnedId.ValueKind != JsonValueKind.String || !returnedId.TryGetGuid(out var parsed) || parsed == Guid.Empty)
                    throw new InvalidOperationException("شناسه تلاش در رسید مقصد معتبر نیست.");
                returnedClientId = parsed;
            }
            if (returnedClientId != clientRequestId)
                throw new InvalidOperationException("رسید مقصد متعلق به تلاش ارسال شده نیست.");

            return new WorkflowStartAcceptance(instanceId, key.GetString()!, returnedClientId);
        }
        catch (Exception ex)
        {
            var diagnosticId = Guid.NewGuid().ToString("N");
            ex.Data["Workflow.Diagnostic"] = $"Stage={stage}; DiagnosticId={diagnosticId}";
            _logger.LogError(ex, "Workflow transport failed. Stage={Stage}, DiagnosticId={DiagnosticId}, MessageId={MessageId}, CompanyId={CompanyId}", stage, diagnosticId, message.MessageId, message.CompanyId);
            throw;
        }
    }

    /// <summary>ویژگی JSON را بدون وابستگی به بزرگ یا کوچک بودن حروف می خواند.</summary>
    private static bool TryProperty(JsonElement element, string name, out JsonElement value)
    {
        if (element.ValueKind == JsonValueKind.Object)
            foreach (var property in element.EnumerateObject())
                if (string.Equals(property.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    value = property.Value;
                    return true;
                }

        value = default;
        return false;
    }
}



