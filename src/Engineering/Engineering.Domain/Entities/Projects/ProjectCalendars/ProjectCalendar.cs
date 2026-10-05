using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Domain.Entities.Projects.ProjectCalendars;

[Description(ProjectCmts.ProjectCalendar)]
public class ProjectCalendar : ActivateEntity<ProjectCalendar, long>
{
    [Description(ProjectCmts.ProjectCalendarTitle)]
    public string TitleFa { get; private set; } = string.Empty;

    [Description(ProjectCmts.ProjectCalendarTitle)]
    public string? TitleEn { get; private set; }

    [Description(ProjectCmts.ProjectCalendarMppUid)]
    public int? MppUid { get; private set; }

    [Description(ProjectCmts.ProjectCalendarIsDefault)]
    public bool IsDefault { get; private set; }

    [Description(ProjectCmts.ProjectCalendarMinutesPerDay)]
    public int MinutesPerDay { get; private set; }

    [Description(GlobalCmts.ProjectId)]
    public long ProjectId { get; private set; }
    public Project Project { get; private set; } = null!;

    public ProjectCalendar(
        Project project,
        string titleFa,
        string? titleEn,
        bool isDefault = false,
        int minutesPerDay = 480) : this()
    {
        Project = project;
        ProjectId = project.Id;

        SetTitleFa(titleFa);
        SetTitleEn(titleEn);

        IsDefault = isDefault;
        MinutesPerDay = minutesPerDay;

        SetActive();
    }

    public void SetMppUid(int? value)
    {
        MppUid = value;
    }

    public void SetDefault(bool value)
    {
        IsDefault = value;
    }

    public void SetTitleFa(string value)
    {
        TitleFa = value.Trim();
    }

    public void SetTitleEn(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            TitleEn = value.Trim();
    }

    public void SetMinutesPerDay(int value)
    {
        MinutesPerDay = value;
    }

    public ProjectCalendarWorkingDay AddWorkingDay(
    DayOfWeek dayOfWeek,
    bool isWorking)
    {
        var workingDay =
            new ProjectCalendarWorkingDay(
                this,
                dayOfWeek,
                isWorking);

        _projectCalendarWorkingDaies.Add(workingDay);

        return workingDay;
    }

    public static ProjectCalendar CreateStandard(Project project, string titleFa, bool isDefault)
    {
        var cal = new ProjectCalendar(project, titleFa, null, isDefault, 480);
        foreach (var d in Enum.GetValues<DayOfWeek>())
        {
            var working = d is not (DayOfWeek.Thursday or DayOfWeek.Friday);
            var day = cal.AddWorkingDay(d, working);
            if (working)
            {
                day.AddWorkingTime(TimeSpan.FromHours(8), TimeSpan.FromHours(12));
                day.AddWorkingTime(TimeSpan.FromHours(13), TimeSpan.FromHours(17));
            }
        }
        return cal;
    }

    public ProjectCalendarWorkingDay SetWorkingDay(
        DayOfWeek day, bool isWorking,
        IReadOnlyCollection<(TimeSpan From, TimeSpan To)>? times = null)
    {
        var existing = _projectCalendarWorkingDaies.FirstOrDefault(d => d.DayOfWeek == day)
                       ?? AddWorkingDay(day, false);

        if (isWorking)
            existing.MakeWorking(times!);
        else
            existing.MakeNonWorking();

        return existing;
    }

    public void EnsureHasWorkingTime()
    {
        var hasWork = _projectCalendarWorkingDaies
            .Any(d => d.IsWorking && d.ProjectCalendarWorkingTimes.Count > 0);
    }

    public void RemoveException(long exceptionId)
    {
        var exception = _projectCalendarExceptions
            .First(x => x.Id == exceptionId && !x.IsDeleted);

        exception.SoftDelete();
    }

    public void AddException(
    DateTime date,
    bool isWorking,
    string? description = null,
    TimeSpan? from = null,
    TimeSpan? to = null)
    {
        date = date.Date;

        _projectCalendarExceptions.Add(
            new ProjectCalendarException(
                this,
                date,
                isWorking,
                description,
                from,
                to));
    }


    [Description(ProjectCmts.ProjectCalendarWorkingDay)]
    private readonly List<ProjectCalendarWorkingDay> _projectCalendarWorkingDaies;
    public IReadOnlyList<ProjectCalendarWorkingDay> ProjectCalendarWorkingDaies => _projectCalendarWorkingDaies;


    [Description(ProjectCmts.ProjectCalendarException)]
    private readonly List<ProjectCalendarException> _projectCalendarExceptions;
    public IReadOnlyList<ProjectCalendarException> ProjectCalendarExceptions => _projectCalendarExceptions;

    [Description(WbsCmts.ProjectScheduleTask)]
    private readonly List<ProjectScheduleTask> _projectScheduleTasks;
    public IReadOnlyList<ProjectScheduleTask> ProjectScheduleTasks => _projectScheduleTasks;


    private ProjectCalendar()
    {
        _projectCalendarWorkingDaies = [];
        _projectCalendarExceptions = [];
        _projectScheduleTasks = [];
    }
}