using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditTaskPlannedDateTimes;

public class EditTaskPlannedDateTimesCommandHandler : ICommandHandler<EditTaskPlannedDateTimesCommand, ProjectScheduleTask>
{
    private readonly ILogger<EditTaskPlannedDateTimesCommandHandler> _logger;
    private readonly IProjectScheduleTaskRepository _repository;
    private readonly IProjectCalendarRepository _calendarRepository;

    public EditTaskPlannedDateTimesCommandHandler(
        ILogger<EditTaskPlannedDateTimesCommandHandler> logger,
        IProjectScheduleTaskRepository repository,
        IProjectCalendarRepository calendarRepository)
    {
        _logger = logger;
        _repository = repository;
        _calendarRepository = calendarRepository;
    }

    public async Task<Result<ProjectScheduleTask?>> Handle(
        EditTaskPlannedDateTimesCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectScheduleTask>(ProjectErrors.ProjectTaskNotFound)!;

            if (request.IsStart)
            {
                entity.SetPlannedStart(request.DateTime);
                entity.SetSchedulingMode(true);
            }
            else
            {
                var calendars = await _calendarRepository.GetByProjectId(entity.ProjectScheduleImport!.ProjectId, ct) ?? [];
                var calendar = calendars.FirstOrDefault(c => c.IsDefault) ?? calendars.FirstOrDefault();
                if (calendar is null)
                    return Result.Failure<ProjectScheduleTask>(ProjectErrors.CalendarNotFound)!;

                var start = entity.PlannedStart ?? request.DateTime;
                if (request.DateTime < start)
                    return Result.Failure<ProjectScheduleTask>(ProjectErrors.WorkingTimeInvalid)!;

                entity.SetPlannedDuration(
                    WorkCalendar.From(calendar).WorkingMinutesBetween(start, request.DateTime),
                    isEstimated: false);
            }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectScheduleTask>(SharedErrors.UnknownError)!;
        }
    }

}
