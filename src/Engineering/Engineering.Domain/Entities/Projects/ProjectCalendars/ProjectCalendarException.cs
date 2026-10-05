
namespace Engineering.Domain.Entities.Projects.ProjectCalendars;

[Description(ProjectCmts.ProjectCalendarException)]
public class ProjectCalendarException
    : ActivateEntity<ProjectCalendarException, long>
{
    [Description(ProjectCmts.ExceptionDate)]
    public DateTime Date { get; private set; }

    [Description(ProjectCmts.ExceptionIsWorking)]
    public bool IsWorking { get; private set; }

    [Description(ProjectCmts.ExceptionFrom)]
    public TimeSpan? From { get; private set; }

    [Description(ProjectCmts.ExceptionTo)]
    public TimeSpan? To { get; private set; }

    [Description(ProjectCmts.ExceptionDescription)]
    public string? Description { get; private set; }

    [Description(ProjectCmts.ProjectCalendarId)]
    public long ProjectCalendarId { get; private set; }
    public ProjectCalendar ProjectCalendar { get; private set; } = null!;

    public ProjectCalendarException(
        ProjectCalendar projectCalendar,
        DateTime date,
        bool isWorking,
        string? description = null,
        TimeSpan? from = null,
        TimeSpan? to = null)
    {
        if (isWorking && (from.HasValue != to.HasValue))
            throw new ArgumentException("وارد کردن هر دو تاریخ آغاز و پایان الزامی می‌باشد.");
        if (isWorking &&
            from.HasValue &&
            to.HasValue &&
            from >= to)
        {
            throw new ArgumentException(
                "زمان شروع باید قبل از زمان پایان باشد.");
        }

        ProjectCalendar = projectCalendar;
        Date = date.Date;
        IsWorking = isWorking;

        From = from;
        To = to;

        Description = description;

        SetActive();
    }

    private ProjectCalendarException()
    {
    }

}