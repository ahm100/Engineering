using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectScheduledTask;
using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.CreateProjectScheduledTask;

public class CreateProjectScheduledTaskCommandHandler : ICommandHandler<CreateProjectScheduledTaskCommand, CreateProjectScheduledTaskResponse?>
{
    private readonly ILogger<CreateProjectScheduledTaskCommandHandler> _logger;
    private readonly IProjectScheduleImportRepository _importRepository;
    private readonly IProjectScheduleTaskRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectCalendarRepository _calendarRepository;

    public CreateProjectScheduledTaskCommandHandler(
        ILogger<CreateProjectScheduledTaskCommandHandler> logger,
        IProjectScheduleTaskRepository repository,
        IProjectScheduleImportRepository importRepository,
        IUnitOfWork unitOfWork,
        IProjectCalendarRepository calendarRepository)
    {
        _logger = logger;
        _repository = repository;
        _importRepository = importRepository;
        _unitOfWork = unitOfWork;
        _calendarRepository = calendarRepository;
    }

    public async Task<Result<CreateProjectScheduledTaskResponse?>> Handle(
        CreateProjectScheduledTaskCommand request, CT ct)
    {
        try
        {
            var import = await _importRepository.GetById(request.ImportId, ct);
            if (import is null)
                return Result.Failure<CreateProjectScheduledTaskResponse>(ProjectErrors.ProjectNotFound)!;

            var allTasks = await _repository.GetByProjectScheduleImportId(import.Id, ct) ?? [];

            var calendars = await _calendarRepository.GetByProjectId(import.ProjectId, ct);
            var minutesPerDay = calendars?.FirstOrDefault(c => c.IsDefault)?.MinutesPerDay ?? 480;

            ProjectScheduleTask? parentTask = null;
            if (request.ParentTaskId.HasValue)
            {
                parentTask = allTasks.FirstOrDefault(t => t.Id == request.ParentTaskId.Value);
                if (parentTask is null)
                    return Result.Failure<CreateProjectScheduledTaskResponse>(ProjectErrors.ProjectTaskNotFound)!;
            }

            bool isMilestone = false;
            var plannedStart = import.ScheduleStartDate ?? DateTime.UtcNow;
            var plannedFinish = plannedStart.AddMinutes(minutesPerDay);
            long durationMinutes = minutesPerDay;
            bool isManual = false;
            var nextUid = await _repository.GetMaxMppUid(import.Id, ct) + 1;
            var nextId = await _repository.GetMaxMppId(import.Id, ct) + 1;
            var live = allTasks.Where(t => !t.IsDeleted).ToList();

            var siblings = live.Where(t => t.ParentId == parentTask?.Id)
                               .OrderBy(t => t.SortOrder).ThenBy(t => t.Id).ToList();

            var position = request.SortOrder is { } requested
                ? Math.Clamp(requested, 1, siblings.Count + 1)
                : (siblings.Count == 0 ? 1 : siblings.Max(t => t.SortOrder) + 1);

            var insertInMiddle = request.SortOrder.HasValue && position <= siblings.Count;

            if (insertInMiddle)
            {
                for (var i = position - 1; i < siblings.Count; i++)
                {
                    siblings[i].SetSortOrder(i + 2);
                    await _repository.Update(siblings[i]);
                }
            }

            var newOutlineLevel = parentTask is null ? 1 : parentTask.OutlineLevel + 1;
            var newOutlineNumber = parentTask is null ? position.ToString() : $"{parentTask.OutlineNumber}.{position}";

            var newTask = new ProjectScheduleTask(
                import,
                projectWbs: null,
                title: request.Title,
                mppUid: nextUid,
                mppId: nextId,
                sortOrder: position,
                outlineLevel: newOutlineLevel,
                outlineNumber: newOutlineNumber,
                plannedStart: plannedStart,
                plannedFinish: plannedFinish,
                plannedDurationMinutes: durationMinutes,
                percentComplete: 0,
                baselineStart: null, baselineFinish: null, baselineDurationMinutes: null,
                actualStart: null, actualFinish: null, actualDurationMinutes: null,
                isMilestone: isMilestone,
                isCritical: false,
                isSummary: false,
                isManuallyScheduled: isManual);

            newTask.SetPlannedDuration(durationMinutes, isEstimated: true);
            newTask.SetPhysicalPercentComplete(0);
            newTask.SetParentTask(parentTask);
            await _repository.Create(newTask, ct);

            if (parentTask is not null && parentTask.IsSummary != true)
            {
                parentTask.SetIsSummary(true);
                await _repository.Update(parentTask);
            }

            await _unitOfWork.CommitAsync(ct);

            if (insertInMiddle)
            {
                live.Add(newTask);

                var changed = new HashSet<ProjectScheduleTask>();
                foreach (var t in TaskOutline.Normalize(live))
                    changed.Add(t);

                foreach (var t in changed)
                    await _repository.Update(t);

                await _unitOfWork.CommitAsync(ct);
            }

            return new CreateProjectScheduledTaskResponse(newTask.Id, newTask.IsSummary ?? false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CreateProjectScheduledTaskResponse>(SharedErrors.UnknownError)!;
        }
    }
}