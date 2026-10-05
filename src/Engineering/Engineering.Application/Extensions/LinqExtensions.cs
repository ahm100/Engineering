using System.Data;
using System.Globalization;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace Engineering.Application.Extensions;

public static class LinqExtensions
{
    private static readonly PersianCalendar _persianCalendar = new PersianCalendar();
    public static string? ToShamsi(this DateTime? date)
    {
        if (date is null) return null;

        return _persianCalendar.GetYear(date.Value) + "/" +
               _persianCalendar.GetMonth(date.Value).ToString("00") + "/" +
               _persianCalendar.GetDayOfMonth(date.Value).ToString("00");
    }
    public static string ToShamsi(this DateTime date)
    {

        return _persianCalendar.GetYear(date) + "/" +
               _persianCalendar.GetMonth(date).ToString("00") + "/" +
               _persianCalendar.GetDayOfMonth(date).ToString("00");
    }

    public static TInput? ExtractValue<TInput>(this IEnumerable<TInput> input, long? id) where TInput : class
    {
        return input?.AsQueryable().Where("Id == @0", id).FirstOrDefault();
    }

    public static List<TInput> ToDataList<TInput>(this TInput input)
    {
        return new List<TInput>() { input };
    }

    public static List<TSource> ToPaging<TSource>(this IEnumerable<TSource> source, int pageIndex, int pageSize)
    {
        if (pageIndex < 0)
        {
            pageIndex = 0;
        }

        pageSize = pageSize == 0 ? int.MaxValue : pageSize;

        var result = source.Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize);
        return result.ToList();
    }

    public static string? JoinList(this IEnumerable<string?>? source)
    {
        if (source is not null && source.Count() > 0)
        {
            return string.Join("-", source);
        }
        return null;
    }

    public static string? JoinListDisc(this IEnumerable<string?>? source)
    {
        if (source is not null && source.Count() > 0)
        {
            var disc = source.Distinct().ToList();
            return string.Join("-", disc);
        }
        return null;
    }

    public static string? JoinListComma(this IEnumerable<string?>? source)
    {
        if (source is not null && source.Count() > 0)
        {
            return string.Join(", ", source);
        }
        return null;
    }

    public static string? JoinListDiscComma(this IEnumerable<string?>? source)
    {
        if (source is not null && source.Count() > 0)
        {
            var disc = source.Distinct().ToList();
            return string.Join(", ", disc);
        }
        return null;
    }

    public static List<TSource> ToPaging<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, int pageIndex, int pageSize)
    {
        if (pageIndex < 0)
        {
            pageIndex = 0;
        }
        pageSize = pageSize == 0 ? int.MaxValue : pageSize;

        var result = source.Where(predicate)
                           .Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize);
        return result.ToList();
    }

    public static DataTable ToDataTable<T>(this List<T> data, string name)
    {
        DataTable dataTable = new DataTable(name);

        PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in properties)
        {
            dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }

        foreach (T item in data)
        {
            var values = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                values[i] = properties[i].GetValue(item, null);
#pragma warning restore CS8601 // Possible null reference assignment.
            }
            dataTable.Rows.Add(values);
        }

        return dataTable;
    }

    public static bool HasItems<T>(this IEnumerable<T>? collection)
        => collection != null && collection.Any();

    public static T? ValueIfSuccess<T>(this Result<T> result)
        => result.IsSuccess ? result.Value : default;



    public static List<dynamic>? SelectNotNullData<TInput>(this IEnumerable<TInput?>? input, string field)
    {
        return input?.AsQueryable().Where($"{field} is not null", field).Select($"{field}").ToDynamicList();
    }

    public static bool IsBlank<T>(this IEnumerable<T>? enumerable)
    {
        return enumerable == null || !enumerable.Any();
    }

    public static List<TProperty> Listed<TSource, TProperty>(
        this IEnumerable<TSource> source,
        Func<TSource, TProperty> selector)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (selector == null)
            throw new ArgumentNullException(nameof(selector));

        return source.Select(selector).Distinct().ToList();
    }

    public static List<TProperty> NullListed<TSource, TProperty>(
        this IEnumerable<TSource> source,
        Func<TSource, TProperty?> selector)
        where TProperty : struct
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (selector == null)
            throw new ArgumentNullException(nameof(selector));

        return source
            .Select(selector)
            .Where(x => x.HasValue)
            .Select(x => x.Value)
            .Distinct()
            .ToList();
    }

    public static List<TProperty> NullListedMany<TSource, TProperty>(
    this IEnumerable<TSource> source,
    Func<TSource, IEnumerable<TProperty?>> selector)
    where TProperty : struct
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (selector == null)
            throw new ArgumentNullException(nameof(selector));

        return source
            .SelectMany(selector)
            .Where(x => x.HasValue)
            .Select(x => x!.Value)
            .Distinct()
            .ToList();
    }

    public static List<TProperty> ListedMany<TSource, TProperty>(
    this IEnumerable<TSource> source,
    Func<TSource, IEnumerable<TProperty>?> selector)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (selector == null)
            throw new ArgumentNullException(nameof(selector));

        return source
            .SelectMany(x => selector(x) ?? Enumerable.Empty<TProperty>())
            .Distinct()
            .ToList();
    }

    public static long? ToNullableLong(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        if (long.TryParse(value, out var result))
            return result;

        return null;
    }

    public static bool HasAny<T>(this ICollection<T>? source)
    {
        return source != null && source.Count > 0;
    }

    public static bool IsNullOrEmpty<T>(this ICollection<T>? source)
    {
        return source == null || source.Count == 0;
    }

    public static bool IsBad<T>(this Result<T> response)
    {
        return response.IsFailure || response.Value is null;
    }

    public static long? SetNull(this long? value)
    {
        if (value.HasValue || value is not null)
            value = null;

        return value;
    }

    public static string? SetNullIfEmpty(this string? value)
    {
        if (string.IsNullOrEmpty(value))
            value = null;

        return value;
    }

    public static Result<TOut> Failure<TOut>(this Result result) =>
        Result.Failure<TOut>(result.Error!)!;

}
