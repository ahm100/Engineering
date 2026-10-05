
namespace Engineering.Domain.Entities.Projects.ProjectCalendars;

[Description(ProjectCmts.ProjectCalendarWorkingDay)]
public class ProjectCalendarWorkingDay
    : ActivateEntity<ProjectCalendarWorkingDay, long>
{
    [Description(ProjectCmts.DayOfWeek)]
    public DayOfWeek DayOfWeek { get; private set; }

    [Description(ProjectCmts.IsWorking)]
    public bool IsWorking { get; private set; }

    [Description(ProjectCmts.ProjectCalendarId)]
    public long ProjectCalendarId { get; private set; }
    public ProjectCalendar ProjectCalendar { get; private set; } = null!;

    public ProjectCalendarWorkingDay(
        ProjectCalendar projectCalendar,
        DayOfWeek dayOfWeek,
        bool isWorking) : this()
    {
        ProjectCalendar = projectCalendar;
        DayOfWeek = dayOfWeek;
        IsWorking = isWorking;

        SetActive();
    }

    public void SetWorking(bool isWorking)
    {
        IsWorking = isWorking;
    }

    public void MakeNonWorking()
    {
        IsWorking = false;
        _workingTimes.Clear();
    }

    public void MakeWorking(IReadOnlyCollection<(TimeSpan From, TimeSpan To)> times)
    {
        if (times.Count == 0)
            throw new InvalidOperationException("روز کاری باید حداقل یک بازه زمانی کاری داشته باشد.");

        var ordered = times.OrderBy(t => t.From).ToList();
        for (var i = 0; i < ordered.Count; i++)
        {
            if (ordered[i].From >= ordered[i].To)
                throw new ArgumentException("زمان شروع باید قبل از پایان باشد.");
            if (i > 0 && ordered[i].From < ordered[i - 1].To)
                throw new InvalidOperationException("تداخل زمانی وجود دارد.");
        }

        IsWorking = true;
        _workingTimes.Clear();
        foreach (var (from, to) in ordered)
            _workingTimes.Add(new ProjectCalendarWorkingTime(this, from, to));
    }

    public void ClearWorkingTimes()
    {
        _workingTimes.Clear();
    }

    public void AddWorkingTime(TimeSpan from, TimeSpan to)
    {
        _workingTimes.Add(new ProjectCalendarWorkingTime(this, from, to));
    }


    [Description(ProjectCmts.ProjectCalendarWorkingTime)]
    private readonly List<ProjectCalendarWorkingTime> _workingTimes;
    public IReadOnlyList<ProjectCalendarWorkingTime> ProjectCalendarWorkingTimes => _workingTimes;

    private ProjectCalendarWorkingDay()
    {
        _workingTimes = [];
    }
}