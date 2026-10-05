using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Engineering.Infra.Providers.Warehouse;

public class WarehouseLoggingHandler : DelegatingHandler
{
    private readonly ILogger<WarehouseLoggingHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly string[] _types = { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };

    public WarehouseLoggingHandler(ILogger<WarehouseLoggingHandler> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public WarehouseLoggingHandler(HttpMessageHandler innerHandler, ILogger<WarehouseLoggingHandler> logger, IHttpContextAccessor httpContextAccessor) :
        base(innerHandler)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CT ct)
    {
        using var log = _logger.BeginScope("Meta data service call");

        _logger.LogInformation("{Method} {PathAndQuery} {Scheme}/{Version}",
            request.Method, request.RequestUri?.PathAndQuery, request.RequestUri?.Scheme, request.Version);

        _logger.LogInformation("Host: {Scheme}://{Host}", request.RequestUri?.Scheme, request.RequestUri?.Host);

        if (request.Content != null)
        {
            if (request.Content is StringContent || IsTextBasedContentType(request.Headers) ||
                IsTextBasedContentType(request.Content.Headers))
            {
                var requestContent = await request.Content.ReadAsStringAsync(ct);

                _logger.LogInformation("Request content: {Content}",
                    string.Join("", requestContent.Cast<char>().Take(255)));
            }
        }

        var context = _httpContextAccessor.HttpContext;
        if (context != null && !string.IsNullOrWhiteSpace(context.Request.Headers.Authorization.ToString()))
        {
            var token = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, ct).ConfigureAwait(false);
        if (response.Content is not StringContent && !IsTextBasedContentType(response.Headers) &&
            !IsTextBasedContentType(response.Content.Headers))
        {
            return response;
        }

        var responseContent = await response.Content.ReadAsStringAsync(ct);
        _logger.LogInformation("Response content: {Content}", string.Join("", responseContent.Cast<char>().Take(255)));

        return response;
    }

    private bool IsTextBasedContentType(HttpHeaders headers)
    {
        if (!headers.TryGetValues("Content-Type", out var values))
        {
            return false;
        }

        var header = string.Join(" ", values).ToLowerInvariant();

        return _types.Any(t => header.Contains(t));
    }
}