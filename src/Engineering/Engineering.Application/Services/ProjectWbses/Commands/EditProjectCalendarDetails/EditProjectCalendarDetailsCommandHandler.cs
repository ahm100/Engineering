using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectCalendarDetails;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectCalendarDetails;

public class EditProjectCalendarDetailsCommandHandler : ICommandHandler<EditProjectCalendarDetailsCommand, EditProjectCalendarDetailsResponse?>
{
    private readonly ILogger<EditProjectCalendarDetailsCommandHandler> _logger;
    private readonly IProjectCalendarRepository _calendarRepository;

    public EditProjectCalendarDetailsCommandHandler(
        ILogger<EditProjectCalendarDetailsCommandHandler> logger,
        IProjectCalendarRepository calendarRepository)
    {
        _logger = logger;
        _calendarRepository = calendarRepository;
    }

    public async Task<Result<EditProjectCalendarDetailsResponse?>> Handle(
        EditProjectCalendarDetailsCommand request, CT ct)
    {
        try
        {
            var calendar = await _calendarRepository.GetById(request.CalendarId, ct);
            if (calendar is null)
                return Result.Failure<EditProjectCalendarDetailsResponse>(ProjectErrors.CalendarNotFound)!;

            if (request.IsDefault && !calendar.IsDefault)
            {
                var siblings = await _calendarRepository.GetByProjectId(calendar.ProjectId, ct) ?? [];
                var currentDefault = siblings.FirstOrDefault(c => c.IsDefault && c.Id != calendar.Id);
                if (currentDefault is not null)
                {
                    currentDefault.SetDefault(false);
                    await _calendarRepository.Update(currentDefault);
                }
            }

            calendar.SetTitleFa(request.TitleFa);
            calendar.SetTitleEn(request.TitleEn);
            calendar.SetMinutesPerDay(request.MinutesPerDay);
            calendar.SetDefault(request.IsDefault);

            await _calendarRepository.Update(calendar);

            return new EditProjectCalendarDetailsResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EditProjectCalendarDetailsResponse>(SharedErrors.UnknownError)!;
        }
    }
}
