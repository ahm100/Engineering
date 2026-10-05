using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectCalendarWorkingDays;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectCalendarWorkingDays;

public class EditProjectCalendarWorkingDaysCommandHandler : ICommandHandler<EditProjectCalendarWorkingDaysCommand, EditProjectCalendarWorkingDaysResponse?>
{
    private readonly ILogger<EditProjectCalendarWorkingDaysCommandHandler> _logger;
    private readonly IProjectCalendarRepository _calendarRepository;

    public EditProjectCalendarWorkingDaysCommandHandler(
        ILogger<EditProjectCalendarWorkingDaysCommandHandler> logger,
        IProjectCalendarRepository calendarRepository)
    {
        _logger = logger;
        _calendarRepository = calendarRepository;
    }

    public async Task<Result<EditProjectCalendarWorkingDaysResponse?>> Handle(
        EditProjectCalendarWorkingDaysCommand request, CT ct)
    {
        try
        {
            var calendar = await _calendarRepository.GetById(request.CalendarId, ct);
            if (calendar is null)
                return Result.Failure<EditProjectCalendarWorkingDaysResponse>(ProjectErrors.CalendarNotFound)!;

            foreach (var dayRequest in request.WorkingDays)
            {
                var workingDay = calendar.SetWorkingDay(dayRequest.DayOfWeek, dayRequest.IsWorking);
                workingDay.ClearWorkingTimes();

                if (dayRequest.IsWorking)
                {
                    foreach (var time in dayRequest.WorkingTimes)
                        workingDay.AddWorkingTime(time.From, time.To);
                }
            }

            await _calendarRepository.Update(calendar);

            return new EditProjectCalendarWorkingDaysResponse(true);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, ex.Message);
            return Result.Failure<EditProjectCalendarWorkingDaysResponse>(ProjectErrors.InvalidCalendarDefinition)!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EditProjectCalendarWorkingDaysResponse>(SharedErrors.UnknownError)!;
        }
    }
}
