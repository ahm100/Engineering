using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectCalendarException;

namespace Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectCalendarException;

public class RemoveProjectCalendarExceptionCommandHandler : ICommandHandler<RemoveProjectCalendarExceptionCommand, RemoveProjectCalendarExceptionResponse?>
{
    private readonly ILogger<RemoveProjectCalendarExceptionCommandHandler> _logger;
    private readonly IProjectCalendarRepository _calendarRepository;

    public RemoveProjectCalendarExceptionCommandHandler(
        ILogger<RemoveProjectCalendarExceptionCommandHandler> logger,
        IProjectCalendarRepository calendarRepository)
    {
        _logger = logger;
        _calendarRepository = calendarRepository;
    }

    public async Task<Result<RemoveProjectCalendarExceptionResponse?>> Handle(
        RemoveProjectCalendarExceptionCommand request, CT ct)
    {
        try
        {
            var calendar = await _calendarRepository.GetById(request.CalendarId, ct);
            if (calendar is null)
                return Result.Failure<RemoveProjectCalendarExceptionResponse>(ProjectErrors.CalendarNotFound)!;

            calendar.RemoveException(request.ExceptionId);
            await _calendarRepository.Update(calendar);

            return new RemoveProjectCalendarExceptionResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RemoveProjectCalendarExceptionResponse>(SharedErrors.UnknownError)!;
        }
    }
}
