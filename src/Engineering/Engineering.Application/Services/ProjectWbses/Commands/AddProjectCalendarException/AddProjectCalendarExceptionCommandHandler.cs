using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Services.ProjectWbses.Contracts.AddProjectCalendarException;

namespace Engineering.Application.Services.ProjectWbses.Commands.AddProjectCalendarException;

public class AddProjectCalendarExceptionCommandHandler : ICommandHandler<AddProjectCalendarExceptionCommand, AddProjectCalendarExceptionResponse?>
{
    private readonly ILogger<AddProjectCalendarExceptionCommandHandler> _logger;
    private readonly IProjectCalendarRepository _calendarRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddProjectCalendarExceptionCommandHandler(
        ILogger<AddProjectCalendarExceptionCommandHandler> logger,
        IProjectCalendarRepository calendarRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _calendarRepository = calendarRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AddProjectCalendarExceptionResponse?>> Handle(
        AddProjectCalendarExceptionCommand request, CT ct)
    {
        try
        {
            var calendar = await _calendarRepository.GetById(request.CalendarId, ct);
            if (calendar is null)
                return Result.Failure<AddProjectCalendarExceptionResponse>(ProjectErrors.CalendarNotFound)!;

            calendar.AddException(request.Date, request.IsWorking, request.Description, request.From, request.To);
            await _calendarRepository.Update(calendar);
            await _unitOfWork.CommitAsync(ct);

            return new AddProjectCalendarExceptionResponse(calendar.ProjectCalendarExceptions.Last().Id);
        }
        catch (Exception ex) when (ex is InvalidOperationException or ArgumentException)
        {
            _logger.LogWarning(ex, ex.Message);
            return Result.Failure<AddProjectCalendarExceptionResponse>(ProjectErrors.InvalidCalendarDefinition)!;
        }
    }
}
