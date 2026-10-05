using Engineering.Domain.Entities.Projects.ProjectCalendars;

namespace Engineering.Application.Extensions.MPPTools;

public static class ProjectCalendarDateCalculator
{
    /// <summary>
    /// تاریخ پایان را بر اساس تاریخ شروع، مدت زمان (به دقیقه) و تقویم پروژه محاسبه می‌کند.
    /// فقط بازه‌های کاری تقویم (با احتساب استثناها) را می‌شمارد.
    /// </summary>
    public static DateTime AddWorkingMinutes(
        DateTime start,
        long durationMinutes,
        ProjectCalendar calendar)
    {
        if (durationMinutes <= 0)
            return start;

        var exceptionsByDate = calendar.ProjectCalendarExceptions
            .ToDictionary(e => e.Date.Date, e => e);

        var workingDaysByDow = calendar.ProjectCalendarWorkingDaies
            .ToDictionary(d => d.DayOfWeek, d => d);

        var remaining = durationMinutes;
        var cursor = start;

        // safety cap تا از حلقه بی‌نهایت روی تقویم بدون هیچ روز کاری جلوگیری شود
        var maxIterations = 3650;

        while (remaining > 0 && maxIterations-- > 0)
        {
            var dayStart = cursor.Date;
            var intervals = GetWorkingIntervals(dayStart, exceptionsByDate, workingDaysByDow);

            foreach (var (from, to) in intervals)
            {
                var intervalStart = dayStart + from;
                var intervalEnd = dayStart + to;

                if (intervalEnd <= cursor)
                    continue;

                var effectiveStart = cursor > intervalStart ? cursor : intervalStart;
                var availableMinutes = (long)(intervalEnd - effectiveStart).TotalMinutes;

                if (availableMinutes <= 0)
                    continue;

                if (availableMinutes >= remaining)
                {
                    cursor = effectiveStart.AddMinutes(remaining);
                    remaining = 0;
                    break;
                }

                remaining -= availableMinutes;
                cursor = intervalEnd;
            }

            if (remaining > 0)
                cursor = dayStart.AddDays(1);
        }

        return cursor;
    }

    /// <summary>
    /// بازه‌های کاری یک روز مشخص را برمی‌گرداند؛ استثنای تقویم در آن روز اولویت دارد.
    /// </summary>
    private static List<(TimeSpan From, TimeSpan To)> GetWorkingIntervals(
        DateTime date,
        Dictionary<DateTime, ProjectCalendarException> exceptionsByDate,
        Dictionary<DayOfWeek, ProjectCalendarWorkingDay> workingDaysByDow)
    {
        if (exceptionsByDate.TryGetValue(date.Date, out var exception))
        {
            if (!exception.IsWorking)
                return [];

            if (exception.From.HasValue && exception.To.HasValue)
                return [(exception.From.Value, exception.To.Value)];

            // استثنا کاری است ولی بازه ساعتی ندارد -> از تمپلیت روز هفته استفاده کن
        }

        if (!workingDaysByDow.TryGetValue(date.DayOfWeek, out var workingDay) || !workingDay.IsWorking)
            return [];

        return workingDay.ProjectCalendarWorkingTimes
            .Select(t => (t.From, t.To))
            .OrderBy(t => t.From)
            .ToList();
    }
}
