using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectCalendar;
using Engineering.Domain.Entities.Projects.ProjectCalendars;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectCalendar;

public class CreateProjectCalendarCommandHandler : ICommandHandler<CreateProjectCalendarCommand, CreateProjectCalendarResponse?>
{
    private readonly ILogger<CreateProjectCalendarCommandHandler> _logger;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectCalendarRepository _calendarRepository;

    public CreateProjectCalendarCommandHandler(
        ILogger<CreateProjectCalendarCommandHandler> logger,
        IProjectRepository projectRepository,
        IProjectCalendarRepository calendarRepository)
    {
        _logger = logger;
        _projectRepository = projectRepository;
        _calendarRepository = calendarRepository;
    }

    public async Task<Result<CreateProjectCalendarResponse?>> Handle(
        CreateProjectCalendarCommand request, CT ct)
    {
        try
        {
            var project = await _projectRepository.GetProjectByIdIncludeLess(request.ProjectId, ct);
            if (project is null)
                return Result.Failure<CreateProjectCalendarResponse>(ProjectErrors.ProjectNotFound)!;

            // فقط یک تقویم پیش‌فرض در هر پروژه مجاز است
            if (request.IsDefault)
            {
                var existingCalendars = await _calendarRepository.GetByProjectId(request.ProjectId, ct) ?? [];
                var currentDefault = existingCalendars.FirstOrDefault(c => c.IsDefault);
                if (currentDefault is not null)
                {
                    currentDefault.SetDefault(false);
                    await _calendarRepository.Update(currentDefault);
                }
            }

            var calendar = new ProjectCalendar(
                project,
                request.TitleFa,
                request.TitleEn,
                request.IsDefault,
                request.MinutesPerDay);

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

            if (request.Exceptions is not null)
            {
                foreach (var exception in request.Exceptions)
                {
                    calendar.AddException(
                        exception.Date,
                        exception.IsWorking,
                        exception.Description,
                        exception.From,
                        exception.To);
                }
            }

            await _calendarRepository.Create(calendar, ct);

            return new CreateProjectCalendarResponse(calendar.Id);
        }
        catch (InvalidOperationException ex)
        {
            // خطاهای دامنه‌ای مثل «روز تکراری» یا «استثنای تکراری برای یک تاریخ»
            _logger.LogWarning(ex, ex.Message);
            return Result.Failure<CreateProjectCalendarResponse>(ProjectErrors.InvalidCalendarDefinition)!;
        }
        catch (ArgumentException ex)
        {
            // خطای ProjectCalendarException وقتی From/To نامعتبر باشد
            _logger.LogWarning(ex, ex.Message);
            return Result.Failure<CreateProjectCalendarResponse>(ProjectErrors.InvalidCalendarDefinition)!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CreateProjectCalendarResponse>(SharedErrors.UnknownError)!;
        }
    }
}