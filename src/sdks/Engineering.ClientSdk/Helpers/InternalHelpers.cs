using Engineering.ClientSdk.Exceptions;
using Engineering.ClientSdk.Models;
using Gita.Backend.Shared.Domain.Base;
using Gita.Backend.Shared.Domain.Errors;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Engineering.ClientSdk.Helpers;

internal static class InternalHelpers
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

    public static async ValueTask EnsureSuccessfulServiceCall(this HttpResponseMessage response, ILogger logger, CancellationToken cancellationToken)
    {
        if (!response.IsSuccessStatusCode)
        {
            string? content = null;
            if (response.RequestMessage?.Method != HttpMethod.Get)
            {
                content = await response.Content.ReadAsStringAsync(cancellationToken);
            }

            Error? error = null;
            if (string.IsNullOrEmpty(content))
            {
                content = "[EMPTY_CONTENT]";
            }
            else
            {
                logger.LogError($"A request to Engineering was not successful: {response.ReasonPhrase}, content: {content}");

                var errorResult = JsonSerializer.Deserialize<EngineeringCanonicalResult<object>>(content, JsonSerializerOptions);
                error = errorResult?.Error is null ? null : new Error(
                    errorResult.Error.Code ?? SharedErrors.UnknownError.Code,
                    errorResult.Error.Message ?? SharedErrors.UnknownError.Message,
                    (int)(errorResult.Error.StatusCode ?? SharedErrors.UnknownError.StatusCode)
                );
            }

            if (error is null)
            {
                throw new EngineeringServiceException(
                    $"A request to Engineering was not successful: {response.ReasonPhrase}, content: {content}");
            }

            throw new EngineeringServiceException(error);
        }
    }

    public static string GetCanonicalBaseAddress(string url)
    {
        var uri = new Uri(url);

        if (uri.Port == 80 || uri.Port == 443)
        {
            return $"{uri.Scheme}://{uri.Host}";
        }

        return $"{uri.Scheme}://{uri.Host}:{uri.Port}";
    }

    /// <summary>
    /// Append parameters from an object onto a base URI string.
    /// Handles whether the base URI already has query parameters.
    /// </summary>
    public static string ToUriParameters(string baseUri, object? parameters)
    {
        if (string.IsNullOrWhiteSpace(baseUri))
            throw new ArgumentNullException(nameof(baseUri));

        string paramString = parameters.ToUriParameters();

        if (string.IsNullOrEmpty(paramString))
            return baseUri; // nothing to append

        // paramString starts with "?"
        if (baseUri.Contains("?"))
        {
            // Already has parameters → replace leading "?" with "&"
            return baseUri.TrimEnd('&') + "&" + paramString.Substring(1);
        }
        else
        {
            return baseUri + paramString;
        }
    }

    /// <summary>
    /// Create a URI query string (starting with '?') from an object.
    /// Returns empty string when there is no parameter.
    /// Supports: POCO objects, IDictionary<string, object>, IDictionary<string, string>, and enumerables (creates repeated keys).
    /// </summary>
    public static string ToUriParameters(this object? source)
    {
        if (source == null) return string.Empty;

        IEnumerable<KeyValuePair<string, object?>> pairs = ExtractPairs(source);
        var sb = new StringBuilder();

        bool first = true;
        foreach (var kv in pairs)
        {
            // skip null values
            if (kv.Value == null) continue;

            // handle enumerables (except string)
            if (kv.Value is string)
            {
                AppendParam(kv.Key, kv.Value, ref first, sb);
            }
            else if (kv.Value is IEnumerable enumerable && !(kv.Value is byte[]))
            {
                foreach (var item in enumerable)
                {
                    if (item == null) continue;
                    AppendParam(kv.Key, item, ref first, sb);
                }
            }
            else
            {
                AppendParam(kv.Key, kv.Value, ref first, sb);
            }
        }

        return sb.Length == 0 ? string.Empty : sb.ToString();
    }

    private static void AppendParam(string key, object? value, ref bool first, StringBuilder sb)
    {
        if (value == null) return;

        string stringValue = ConvertValueToString(value);

        if (string.IsNullOrEmpty(stringValue)) return;

        var encodedKey = Uri.EscapeDataString(key);
        var encodedValue = Uri.EscapeDataString(stringValue);

        if (first)
        {
            sb.Append('?');
            first = false;
        }
        else
        {
            sb.Append('&');
        }

        sb.Append(encodedKey).Append('=').Append(encodedValue);
    }

    private static string ConvertValueToString(object value)
    {
        switch (value)
        {
            case DateTime dt:
                return dt.ToString("o"); // ISO 8601
            case DateTimeOffset dto:
                return dto.ToString("o");
            case bool b:
                return b ? "true" : "false";
            case Enum e:
                return e.ToString();
            case IFormattable f:
                // numbers, decimals, etc. use invariant culture
                return f.ToString(null, System.Globalization.CultureInfo.InvariantCulture);
            default:
                return value.ToString() ?? string.Empty;
        }
    }

    private static IEnumerable<KeyValuePair<string, object?>> ExtractPairs(object source)
    {
        // If it's a dictionary-like object
        if (source is IDictionary<string, object?> dictObj)
        {
            foreach (var kv in dictObj) yield return kv;
            yield break;
        }

        if (source is IDictionary<string, string> dictStr)
        {
            foreach (var kv in dictStr) yield return new KeyValuePair<string, object?>(kv.Key, kv.Value);
            yield break;
        }

        // If it's an anonymous/object with properties -> use reflection to read public instance properties
        var type = source.GetType();

        // If source itself is a simple value (string, number) then treat as single unnamed parameter (not typical) — we'll return empty
        if (IsSimpleType(type)) yield break;

        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(p => p.CanRead);

        foreach (var p in props)
        {
            object? val;
            try { val = p.GetValue(source); }
            catch { val = null; } // skip properties that throw
            yield return new KeyValuePair<string, object?>(p.Name, val);
        }
    }

    private static bool IsSimpleType(Type type)
    {
        return
            type.IsPrimitive ||
            type.IsEnum ||
            type == typeof(string) ||
            type == typeof(decimal) ||
            type == typeof(DateTime) ||
            type == typeof(DateTimeOffset) ||
            type == typeof(Guid) ||
            type == typeof(Uri) ||
            type == typeof(TimeSpan);
    }
}
