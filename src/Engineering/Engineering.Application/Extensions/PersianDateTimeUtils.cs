namespace Financial.Application.Extensions;

/// <summary>
/// Represents PersianDateTime utils.
/// </summary>
public static class PersianDateTimeUtils
{

    /// <summary>
    /// تبدیل تاریخ میلادی به شمسی
    /// با قالبی مانند 1395/10/21
    /// </summary>
    /// <returns>تاریخ شمسی</returns>
    public static string ToShortPersianDateString(this DateTime dt)
    {
        return dt.ToPersianDateTimeString(PersianCulture.Instance.DateTimeFormat.ShortDatePattern);
    }

    /// <summary>
    /// تبدیل تاریخ میلادی به شمسی
    /// با قالبی مانند 1395/10/21
    /// </summary>
    /// <returns>تاریخ شمسی</returns>
    public static string ToShortPersianDateString(this DateTime? dt)
    {
        return dt == null ? string.Empty : dt.Value.ToShortPersianDateString();
    }

    /// <summary>
    /// تبدیل تاریخ میلادی به شمسی
    /// با قالبی مانند 1395/10/21 10:20
    /// </summary>
    /// <returns>تاریخ شمسی</returns>
    public static string ToShortPersianDateTimeString(this DateTime dt)
    {
        return dt.ToPersianDateTimeString(
            $"{PersianCulture.Instance.DateTimeFormat.ShortDatePattern} {PersianCulture.Instance.DateTimeFormat.ShortTimePattern}");
    }

    /// <summary>
    /// تبدیل تاریخ میلادی به شمسی
    /// </summary>
    /// <returns>تاریخ شمسی</returns>
    public static string ToPersianDateTimeString(this DateTime dateTime, string format)
    {
        return dateTime.ToString(format, PersianCulture.Instance);
    }

    /// <summary>
    /// تبدیل تاریخ میلادی به شمسی
    /// با قالبی مانند 1395/10/21 10:20
    /// </summary>
    /// <returns>تاریخ شمسی</returns>
    public static string ToShortPersianDateTimeString(this DateTime? dt)
    {
        return dt == null ? string.Empty : dt.Value.ToShortPersianDateTimeString();
    }
}