
namespace Engineering.Domain.Entities.Projects.ProjectCalendars;

[Description(ProjectCmts.ProjectCalendarWorkingTime)]
public class ProjectCalendarWorkingTime
    : ActivateEntity<ProjectCalendarWorkingTime, long>
{
    [Description(ProjectCmts.From)]
    public TimeSpan From { get; private set; }

    [Description(ProjectCmts.To)]
    public TimeSpan To { get; private set; }

    [Description(ProjectCmts.ProjectCalendarWorkingDayId)]
    public long ProjectCalendarWorkingDayId { get; private set; }
    public ProjectCalendarWorkingDay ProjectCalendarWorkingDay { get; private set; } = null!;

    public ProjectCalendarWorkingTime(
        ProjectCalendarWorkingDay workingDay,
        TimeSpan from,
        TimeSpan to)
    {
        if (from >= to)
            throw new ArgumentException(
                "زمان شروع باید قبل از پایان باشد.");

        ProjectCalendarWorkingDay = workingDay;

        From = from;
        To = to;

        SetActive();
    }

    private ProjectCalendarWorkingTime()
    {
    }

}
