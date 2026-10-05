using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Services.ProjectWbses.Contracts.CloneProjectCalendar;
using Engineering.Domain.Entities.Projects.ProjectCalendars;

namespace Engineering.Application.Services.ProjectWbses.Commands.CloneProjectCalendar;

public class CloneProjectCalendarCommandHandler : ICommandHandler<CloneProjectCalendarCommand, CloneProjectCalendarResponse?>
{
    private readonly ILogger<CloneProjectCalendarCommandHandler> _logger;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectCalendarRepository _calendarRepository;

    public CloneProjectCalendarCommandHandler(
        ILogger<CloneProjectCalendarCommandHandler> logger,
        IProjectRepository projectRepository,
        IProjectCalendarRepository calendarRepository)
    {
        _logger = logger;
        _projectRepository = projectRepository;
        _calendarRepository = calendarRepository;
    }

    public async Task<Result<CloneProjectCalendarResponse?>> Handle(
        CloneProjectCalendarCommand request, CT ct)
    {
        try
        {
            var targetProject = await _projectRepository.GetProjectByIdIncludeLess(request.TargetProjectId, ct);
            if (targetProject is null)
                return Result.Failure<CloneProjectCalendarResponse>(ProjectErrors.ProjectNotFound)!;

            var source = await _calendarRepository.GetById(request.SourceCalendarId, ct);
            if (source is null)
                return Result.Failure<CloneProjectCalendarResponse>(ProjectErrors.InvalidCalendarDefinition)!;

            if (request.SetAsDefault)
            {
                var existingCalendars = await _calendarRepository.GetByProjectId(request.TargetProjectId, ct) ?? [];
                var currentDefault = existingCalendars.FirstOrDefault(c => c.IsDefault);
                if (currentDefault is not null)
                {
                    currentDefault.SetDefault(false);
                    await _calendarRepository.Update(currentDefault);
                }
            }

            var clone = new ProjectCalendar(
                targetProject,
                request.NewTitleFa ?? source.TitleFa,
                source.TitleEn,
                request.SetAsDefault,
                source.MinutesPerDay);

            foreach (var day in source.ProjectCalendarWorkingDaies)
            {
                var newDay = clone.AddWorkingDay(day.DayOfWeek, day.IsWorking);

                if (day.IsWorking)
                {
                    foreach (var time in day.ProjectCalendarWorkingTimes)
                        newDay.AddWorkingTime(time.From, time.To);
                }
            }

            foreach (var exception in source.ProjectCalendarExceptions)
            {
                clone.AddException(
                    exception.Date,
                    exception.IsWorking,
                    exception.Description,
                    exception.From,
                    exception.To);
            }

            await _calendarRepository.Create(clone, ct);

            return new CloneProjectCalendarResponse(clone.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CloneProjectCalendarResponse>(SharedErrors.UnknownError)!;
        }
    }
}