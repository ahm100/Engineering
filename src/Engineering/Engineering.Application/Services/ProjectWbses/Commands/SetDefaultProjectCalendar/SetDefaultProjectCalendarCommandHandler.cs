using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Services.ProjectWbses.Contracts.SetDefaultProjectCalendar;

namespace Engineering.Application.Services.ProjectWbses.Commands.SetDefaultProjectCalendar;

public class SetDefaultProjectCalendarCommandHandler
    : ICommandHandler<SetDefaultProjectCalendarCommand, SetDefaultProjectCalendarResponse?>
{
    private readonly ILogger<SetDefaultProjectCalendarCommandHandler> _logger;
    private readonly IProjectCalendarRepository _calendarRepository;

    public SetDefaultProjectCalendarCommandHandler(
        ILogger<SetDefaultProjectCalendarCommandHandler> logger,
        IProjectCalendarRepository calendarRepository)
    {
        _logger = logger;
        _calendarRepository = calendarRepository;
    }

    public async Task<Result<SetDefaultProjectCalendarResponse?>> Handle(
        SetDefaultProjectCalendarCommand request, CT ct)
    {
        try
        {
            var calendar = await _calendarRepository.GetById(request.CalendarId, ct);
            if (calendar is null)
                return Result.Failure<SetDefaultProjectCalendarResponse>(ProjectErrors.CalendarNotFound)!;

            var all = await _calendarRepository.GetByProjectId(calendar.ProjectId, ct) ?? [];
            foreach (var other in all.Where(c => c.IsDefault && c.Id != calendar.Id))
            {
                other.SetDefault(false);
                await _calendarRepository.Update(other);
            }

            calendar.SetDefault(true);
            await _calendarRepository.Update(calendar);

            return new SetDefaultProjectCalendarResponse(calendar.ProjectId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<SetDefaultProjectCalendarResponse>(SharedErrors.UnknownError)!;
        }
    }
}