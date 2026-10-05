using Engineering.Domain.Entities.Projects.ProjectCalendars;

public sealed class WorkCalendar
{
    private const int MaxScanDays = 3660;
    private static readonly (TimeSpan From, TimeSpan To)[] Fallback =
        [(TimeSpan.FromHours(8), TimeSpan.FromHours(12)), (TimeSpan.FromHours(13), TimeSpan.FromHours(17))];

    private readonly Dictionary<DayOfWeek, (TimeSpan From, TimeSpan To)[]> _week;
    private readonly Dictionary<DateTime, (bool IsWorking, TimeSpan? From, TimeSpan? To)> _exceptions;
    public int MinutesPerDay { get; }

    private WorkCalendar(
        Dictionary<DayOfWeek, (TimeSpan, TimeSpan)[]> week,
        Dictionary<DateTime, (bool, TimeSpan?, TimeSpan?)> ex, int minutesPerDay)
        => (_week, _exceptions, MinutesPerDay) = (week, ex, minutesPerDay);

    public static WorkCalendar From(ProjectCalendar c)
    {
        var week = c.ProjectCalendarWorkingDaies.ToDictionary(
            d => d.DayOfWeek,
            d => d.IsWorking
                ? d.ProjectCalendarWorkingTimes.OrderBy(t => t.From).Select(t => (t.From, t.To)).ToArray()
                : Array.Empty<(TimeSpan, TimeSpan)>());

        var ex = c.ProjectCalendarExceptions.ToDictionary(
            e => e.Date.Date, e => (e.IsWorking, e.From, e.To));

        if (week.Values.All(v => v.Length == 0) && !ex.Values.Any(e => e.IsWorking))
            throw new InvalidOperationException("Calendar has no working time.");

        return new WorkCalendar(week, ex, c.MinutesPerDay);
    }

    public (TimeSpan From, TimeSpan To)[] IntervalsFor(DateTime date)
    {
        var weekly = _week.GetValueOrDefault(date.DayOfWeek) ?? [];
        if (!_exceptions.TryGetValue(date.Date, out var ex)) return weekly;

        if (!ex.IsWorking) return [];
        if (ex.From is { } f && ex.To is { } t) return [(f, t)];
        return weekly.Length > 0 ? weekly : Fallback;
    }

    /// Next moment >= dt that is inside working time.
    public DateTime SnapForward(DateTime dt)
    {
        var day = dt.Date;
        for (var i = 0; i < MaxScanDays; i++, day = day.AddDays(1))
            foreach (var (from, to) in IntervalsFor(day))
                if (day + to > dt) return day + from > dt ? day + from : dt;
        throw new InvalidOperationException("No working time found.");
    }

    /// Latest moment <= dt that is inside working time (interval end counts).
    public DateTime SnapBackward(DateTime dt)
    {
        var day = dt.Date;
        for (var i = 0; i < MaxScanDays; i++, day = day.AddDays(-1))
        {
            var iv = IntervalsFor(day);
            for (var k = iv.Length - 1; k >= 0; k--)
                if (day + iv[k].From < dt) return day + iv[k].To < dt ? day + iv[k].To : dt;
        }
        throw new InvalidOperationException("No working time found.");
    }

    public DateTime AddWorkingMinutes(DateTime start, long minutes)
    {
        var cursor = SnapForward(start);
        if (minutes <= 0) return cursor;

        var remaining = minutes;
        var day = cursor.Date;
        for (var i = 0; i < MaxScanDays; i++, day = day.AddDays(1))
            foreach (var (from, to) in IntervalsFor(day))
            {
                var s = day + from; var e = day + to;
                if (e <= cursor) continue;
                var effStart = s > cursor ? s : cursor;
                var available = (long)(e - effStart).TotalMinutes;
                if (remaining <= available) return effStart.AddMinutes(remaining);
                remaining -= available;
            }
        throw new InvalidOperationException("Duration exceeds calendar horizon.");
    }

    public DateTime SubtractWorkingMinutes(DateTime end, long minutes)
    {
        var cursor = SnapBackward(end);
        if (minutes <= 0) return cursor;

        var remaining = minutes;
        var day = cursor.Date;
        for (var i = 0; i < MaxScanDays; i++, day = day.AddDays(-1))
        {
            var iv = IntervalsFor(day);
            for (var k = iv.Length - 1; k >= 0; k--)
            {
                var s = day + iv[k].From; var e = day + iv[k].To;
                if (s >= cursor) continue;
                var effEnd = e < cursor ? e : cursor;
                var available = (long)(effEnd - s).TotalMinutes;
                if (remaining <= available) return effEnd.AddMinutes(-remaining);
                remaining -= available;
            }
        }
        throw new InvalidOperationException("Duration exceeds calendar horizon.");
    }

    public DateTime Shift(DateTime dt, long lagMinutes)
        => lagMinutes >= 0 ? AddWorkingMinutes(dt, lagMinutes) : SubtractWorkingMinutes(dt, -lagMinutes);

    public long WorkingMinutesBetween(DateTime start, DateTime end)
    {
        if (end <= start) return 0;
        long total = 0;
        for (var day = start.Date; day <= end.Date; day = day.AddDays(1))
            foreach (var (from, to) in IntervalsFor(day))
            {
                var s = day + from > start ? day + from : start;
                var e = day + to < end ? day + to : end;
                if (e > s) total += (long)(e - s).TotalMinutes;
            }
        return total;
    }
}