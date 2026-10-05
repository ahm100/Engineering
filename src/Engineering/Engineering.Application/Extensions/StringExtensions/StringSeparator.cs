using System.Text.RegularExpressions;

namespace Engineering.Application.Extensions.StringExtensions;

public static class StringSeparator
{
    public static string WithDash(List<string>? strings)
    {
        var result = "";
        if (strings is not null && strings.Any())
            result = string.Join(" - ", strings!);

        return result;
    }

    public static string JoinWithDash<T>(IEnumerable<T> items, Func<T, string> selector)
    {
        var details = items.Select(selector);
        return string.Join(" - ", details);
    }

    public static string WithComma(List<string>? strings)
    {
        var result = "";
        if (strings is not null && strings.Any())
            result = string.Join(" , ", strings!);

        return result;
    }

    public static string? DashJoinList(this IEnumerable<string?>? source)
    {
        if (source is not null && source.Count() > 0)
        {
            return string.Join("-", source);
        }
        return null;
    }

    public static string? CommaJoinList(this IEnumerable<string?>? source)
    {
        if (source is not null && source.Count() > 0)
        {
            return string.Join(",", source);
        }
        return null;
    }

    public static string Normalize(string input)
    {
        return Regex.Replace(input ?? "", @"[ \-_/]", "");
    }

    public static bool LikeMatch(this string? source, string? filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
            return true;

        if (string.IsNullOrWhiteSpace(source))
            return false;

        var terms = filter
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return terms.All(t =>
            source.Contains(t, StringComparison.OrdinalIgnoreCase));
    }
}
