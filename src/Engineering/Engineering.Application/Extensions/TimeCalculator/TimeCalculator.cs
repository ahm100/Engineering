using System.Globalization;

namespace Engineering.Application.Extensions.TimeCalculator;

public class TimeCalculator
{
    public static string TimeConsumption(TimeSpan workTime, decimal finalAmount)
    {
        var totalWorkTime = workTime.Minutes * finalAmount;
        var totalWorkHours = String.Format("{0:0.00}", (totalWorkTime / 60));
        TimeSpan timespan = TimeSpan.FromHours(double.Parse(totalWorkHours));
        return string.Format("{0}:{1}:{2}", timespan.Days * 24 + timespan.Hours, timespan.Minutes, timespan.Seconds);
    }

    public static TimeSpan StringToTimeSpan(string time)
    {
        return new TimeSpan(int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]), 0);
    }

    public static long StringToTicks(string? time)
    {
        if (string.IsNullOrEmpty(time))
            return 0;
        return new TimeSpan(int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]), 0).Ticks;
    }

    public static long DayAndHourToTicks(int day, int hour)
    {
        var dayTicks = 864000000000 * day;
        var hourTicks = 36000000000 * hour;
        return dayTicks + hourTicks;
    }

    public static long StringToTicksWithoutSplit(string time)
    {
        return TimeSpan.Parse(time).Ticks;
    }

    public static TimeSpan TicksToTimeSpan(long ticks)
    {
        return new TimeSpan(ticks);
    }

    public static string TicksToStringHMS(long ticks)
    {
        TimeSpan newTimeSpan = new TimeSpan(ticks);
        return string.Format("{0}:{1}:{2}", newTimeSpan.Days * 24 + newTimeSpan.Hours, newTimeSpan.Minutes, newTimeSpan.Seconds);
    }

    public static string TicksToStringHM(long? ticks)
    {
        if (ticks == null || ticks <= 0)
            return "00:00";
        TimeSpan newTimeSpan = new TimeSpan((long)ticks);
        var time = string.Format("{0}:{1}", (newTimeSpan.Days * 24 + newTimeSpan.Hours).ToString().PadLeft(2, '0'), newTimeSpan.Minutes.ToString().PadLeft(2, '0'));
        return time;
    }

    public static string TimeSpanToString(TimeSpan time)
    {
        return string.Format("{0}:{1}", time.Hours, time.Minutes);
    }

    public static long StandardTimeConsumption(long workTime, decimal finalAmount)
    {
        return workTime * Convert.ToInt64(finalAmount);
    }

    public static string? DatePiker(DateTime? value)
    {
        if (value is null)
            return null;
        else
        {
            DateTime dateTime = (DateTime)value;
            var date = dateTime.ToString("yyyy-MM-dd");
            return date;
        }
    }

    public static string? ConvertToShamsi(DateTime? date)
    {
        PersianCalendar pc = new PersianCalendar();
        if (date is null)
            return null;
        else
            return pc.GetYear((DateTime)date) + "/" + pc.GetMonth((DateTime)date).ToString("00") + "/" + pc.GetDayOfMonth((DateTime)date).ToString("00");
    }

    public static (string? ShamsiDate, string? Time) ConvertUtcToTehranTimeAndShamsi(DateTime utcDate)
    {
        try
        {
            var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            var utcDateTime = DateTime.SpecifyKind(utcDate, DateTimeKind.Utc);
            var tehranDateTime = TimeZoneInfo.ConvertTime(utcDateTime, tehranTimeZone);
            var shamsiDate = TimeCalculator.ConvertToShamsi(tehranDateTime);
            var time = tehranDateTime.ToString("HH:mm:ss");
            return (shamsiDate, time);
        }
        catch (TimeZoneNotFoundException)
        {
            throw new Exception("منطقه زمانی 'Iran Standard Time' پیدا نشد.");
        }
        catch (InvalidTimeZoneException)
        {
            throw new Exception("منطقه زمانی 'Iran Standard Time' معتبر نیست.");
        }
        catch (Exception ex)
        {
            throw new Exception($": {ex.Message}");
        }
    }

    public static DateTime ToGeorgianDateTime(string persianDate)
    {
        int year = Convert.ToInt32(persianDate.Substring(0, 4));
        int month = Convert.ToInt32(persianDate.Substring(5, 2));
        int day = Convert.ToInt32(persianDate.Substring(8, 2));
        DateTime georgianDateTime = new DateTime(year, month, day, new System.Globalization.PersianCalendar());
        return georgianDateTime;
    }

    public static int GetPersianYear(DateTime gregorianDate)
    {
        PersianCalendar pc = new PersianCalendar();
        return pc.GetYear(gregorianDate);
    }

    public static int GetPersianYear(int gregorianYear, int month = 1, int day = 1)
    {
        PersianCalendar pc = new PersianCalendar();
        var date = new DateTime(gregorianYear, month, day);
        return pc.GetYear(date);
    }
}
