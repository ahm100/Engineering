using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Commands.AddProjectCalendarException;
using Engineering.Application.Services.ProjectWbses.Commands.CloneProjectCalendar;
using Engineering.Application.Services.ProjectWbses.Commands.CreateProjectCalendar;
using Engineering.Application.Services.ProjectWbses.Commands.CreateProjectSchedule;
using Engineering.Application.Services.ProjectWbses.Commands.CreateProjectScheduleColumn;
using Engineering.Application.Services.ProjectWbses.Commands.CreateProjectScheduledTask;
using Engineering.Application.Services.ProjectWbses.Commands.EditPercentComplete;
using Engineering.Application.Services.ProjectWbses.Commands.EditPhysicalPercentComplete;
using Engineering.Application.Services.ProjectWbses.Commands.EditProjectCalendarDetails;
using Engineering.Application.Services.ProjectWbses.Commands.EditProjectCalendarWorkingDays;
using Engineering.Application.Services.ProjectWbses.Commands.EditProjectScheduleColumn;
using Engineering.Application.Services.ProjectWbses.Commands.EditProjectScheduleStartDate;
using Engineering.Application.Services.ProjectWbses.Commands.EditProjectTasksPredecessors;
using Engineering.Application.Services.ProjectWbses.Commands.EditTaskActualDateTimes;
using Engineering.Application.Services.ProjectWbses.Commands.EditTaskPlannedDateTimes;
using Engineering.Application.Services.ProjectWbses.Commands.MoveProjectScheduledTask;
using Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectCalendarException;
using Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectSchedule;
using Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectScheduleColumn;
using Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectScheduledTask;
using Engineering.Application.Services.ProjectWbses.Commands.ReSchheduledProjectSchedule;
using Engineering.Application.Services.ProjectWbses.Commands.SetDefaultProjectCalendar;
using Engineering.Application.Services.ProjectWbses.Commands.SetProjectScheduleTaskValue;
using Engineering.Application.Services.ProjectWbses.Contracts.AddProjectCalendarException;
using Engineering.Application.Services.ProjectWbses.Contracts.CloneProjectCalendar;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectCalendar;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectSchedule;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectScheduleColumn;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectScheduledTask;
using Engineering.Application.Services.ProjectWbses.Contracts.EditActualFinishProjectScheduleTask;
using Engineering.Application.Services.ProjectWbses.Contracts.EditActualStartProjectScheduleTask;
using Engineering.Application.Services.ProjectWbses.Contracts.EditPercentComplete;
using Engineering.Application.Services.ProjectWbses.Contracts.EditPhysicalPercentComplete;
using Engineering.Application.Services.ProjectWbses.Contracts.EditPlannedFinishProjectScheduleTask;
using Engineering.Application.Services.ProjectWbses.Contracts.EditPlannedStartProjectScheduleTask;
using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectCalendarDetails;
using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectCalendarWorkingDays;
using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleColumn;
using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleStartDate;
using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleTaskDurationDays;
using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectScheduleTaskTitle;
using Engineering.Application.Services.ProjectWbses.Contracts.EditProjectTasksPredecessors;
using Engineering.Application.Services.ProjectWbses.Contracts.GetExportMppFile;
using Engineering.Application.Services.ProjectWbses.Contracts.GetProjectSchedule;
using Engineering.Application.Services.ProjectWbses.Contracts.ImportMppFile;
using Engineering.Application.Services.ProjectWbses.Contracts.MoveProjectScheduledTask;
using Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectCalendarException;
using Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectSchedule;
using Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectScheduleColumn;
using Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectScheduledTask;
using Engineering.Application.Services.ProjectWbses.Contracts.ReSchheduledProjectSchedule;
using Engineering.Application.Services.ProjectWbses.Contracts.SetDefaultProjectCalendar;
using Engineering.Application.Services.ProjectWbses.Contracts.SetProjectScheduleTaskValue;
using Engineering.Application.Services.ProjectWbses.Queries.GetProjectByImportId;
using Engineering.Application.Services.ProjectWbses.Scheduling;
using Engineering.Domain.Entities.Projects.Enums;
using IdentityServer.ClientSdk.Services;

namespace Engineering.Application.Services.ProjectWbses;

public partial class ProjectWbsLogic : IProjectWbsLogic
{
    private readonly ILogger<ProjectWbsLogic> _logger;
    private readonly IMediator _mediator;
    private readonly IProjectScheduleColumnRepository _columnRepository;
    private readonly IProjectScheduleTaskValueRepository _valueRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoProvider _userInfoService;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectScheduleImportRepository _importRepository;
    private readonly IProjectScheduleTaskRepository _taskRepository;
    private readonly IProjectScheduleTaskDependencyRepository _dependencyRepository;
    private readonly IProjectCalendarRepository _projectCalendarRepository;
    private readonly ProjectWbstImporter _mppProjectImporter;
    private readonly ScheduleRecalculator _scheduleRecalculator;

    public ProjectWbsLogic(
        ILogger<ProjectWbsLogic> logger,
        IMediator mediator,
        IUnitOfWork unitOfWork,
        IUserInfoProvider userInfoService,
        IProjectScheduleImportRepository importRepository,
        IProjectScheduleTaskRepository taskRepository,
        IProjectScheduleTaskDependencyRepository dependencyRepository,
        IProjectCalendarRepository projectCalendarRepository,
        ProjectWbstImporter mppProjectImporter,
        IProjectRepository projectRepository,
        ScheduleRecalculator scheduleRecalculator,
        IProjectScheduleTaskValueRepository valueRepository,
        IProjectScheduleColumnRepository columnRepository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _mppProjectImporter = mppProjectImporter;
        _importRepository = importRepository;
        _taskRepository = taskRepository;
        _dependencyRepository = dependencyRepository;
        _projectCalendarRepository = projectCalendarRepository;
        _projectRepository = projectRepository;
        _mediator = mediator;
        _scheduleRecalculator = scheduleRecalculator;
        _columnRepository = columnRepository;
        _valueRepository = valueRepository;
    }


    public async Task<Result<ImportMppFileResponse?>> ImportMppFile(
        ImportMppFileRequest request, CT ct)
    {
        _logger.LogInformation("ImportMppFile");
        await using var stream = request.DocumentFile.OpenReadStream();
        var userId = _userInfoService.UserId;

        var result = await _mppProjectImporter.ImportMppFileManager(
            request.ProjectId, request.FileId, request.FileName, userId, stream, ct);
        if (result.IsBad()) return result.Failure<ImportMppFileResponse>()!;

        return new ImportMppFileResponse(true);
    }

    public async Task<Result<GetProjectScheduleResponse?>> GetProjectSchedule(
        GetProjectScheduleRequest request, CT ct)
    {
        var import = await _importRepository.GetByProjectId(request.ProjectId, ct);
        if (import is null)
            return Result.Failure<GetProjectScheduleResponse>(ProjectErrors.ProjectNotFound);

        var tasks = await _taskRepository.GetByProjectScheduleImportId(import.Id, ct) ?? [];
        var dependencies = await _dependencyRepository.GetByTaskIds(
            tasks.Select(x => x.Id).ToList(), ct);
        var calendars = await _projectCalendarRepository.GetByProjectId(import.ProjectId, ct);

        var columns = await _columnRepository.GetByImportId(import.Id, ct);
        var values = await _valueRepository.GetByImportId(import.Id, ct);

        var customColumnIds = columns
            .Where(c => c.ColumnType == ProjectScheduleColumnType.Custom)
            .Select(c => c.Id)
            .ToHashSet();

        var valuesByTask = values
            .Where(v => customColumnIds.Contains(v.ProjectScheduleColumnId))
            .GroupBy(v => v.ProjectScheduleTaskId)
            .ToDictionary(
                g => g.Key,
                g => g.Select(v => new GetProjectScheduleTaskValueModel
                {
                    ColumnId = v.ProjectScheduleColumnId,
                    Value = v.Value,
                    NumberValue = v.NumberValue,
                    DateTimeValue = v.DateTimeValue
                }).ToList());

        var weightByTask = tasks.ToDictionary(t => t.Id, t => t.Weight);

        WorkCalendar? workCal = null;
        var defaultCalendar = calendars?.FirstOrDefault(c => c.IsDefault) ?? calendars?.FirstOrDefault();
        if (defaultCalendar is not null)
        {
            try
            {
                workCal = WorkCalendar.From(defaultCalendar);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Plan % uses clock time because the calendar is invalid. ProjectId:{ProjectId}", request.ProjectId);
            }
        }

        var asOfDate = request.AsOfDate ?? import.StatusDate ?? DateTime.UtcNow;

        var childCounts = tasks.Where(t => t.ParentId != null)
            .GroupBy(t => t.ParentId!.Value)
            .ToDictionary(g => g.Key, g => g.Count());

        var leaves = tasks.Where(t => !childCounts.ContainsKey(t.Id)).ToList();
        var planPercent = CalculateProjectPlanPercent(leaves, asOfDate, workCal);
        var actualPercent = CalculateProjectActualPercent(leaves);
        var rollups = CalculateTaskRollups(tasks, asOfDate, workCal);

        var taskTree = BuildTaskTree(tasks, childCounts, rollups, asOfDate, workCal);
        ApplyColumnData(taskTree, valuesByTask, weightByTask);

        var response = new GetProjectScheduleResponse
        {
            ImportId = import.Id,
            ProjectId = import.ProjectId,
            ScheduleStartDate = import.ScheduleStartDate,
            FileId = import.FileId,
            FileName = import.FileName,
            ImportedBy = import.ImportedBy,
            Status = import.Status.ToString(),
            PlanPercent = planPercent,
            ActualPercent = actualPercent,

            Tasks = taskTree,
            Dependencies = dependencies is null ? new List<GetProjectScheduleDependencyModel>() :
                dependencies
                .Select(x => new GetProjectScheduleDependencyModel
                {
                    Id = x.Id,
                    PredecessorTaskId = x.PredecessorTaskId,
                    SuccessorTaskId = x.SuccessorTaskId,
                    Type = x.Type,
                    LagMinutes = x.LagMinutes
                })
                .ToList(),

            Calendars = calendars is null ? new List<GetProjectScheduleCalendarModel>() :
                calendars
                .OrderByDescending(x => x.IsDefault)
                .ThenBy(x => x.TitleFa)
                .Select(x => new GetProjectScheduleCalendarModel
                {
                    Id = x.Id,
                    MppUid = x.MppUid,
                    Title = x.TitleFa,
                    IsDefault = x.IsDefault,
                    MinutesPerDay = x.MinutesPerDay,
                    WorkingDays = x.ProjectCalendarWorkingDaies
                        .OrderBy(d => d.DayOfWeek)
                        .Select(d => new GetProjectScheduleWorkingDayModel
                        {
                            Id = d.Id,
                            DayOfWeek = d.DayOfWeek,
                            IsWorking = d.IsWorking,
                            WorkingTimes = d.ProjectCalendarWorkingTimes
                                .OrderBy(t => t.From)
                                .Select(t => new GetProjectScheduleWorkingTimeModel
                                {
                                    Id = t.Id,
                                    From = t.From,
                                    To = t.To
                                })
                                .ToList()
                        })
                        .ToList(),
                    Exceptions = x.ProjectCalendarExceptions
                        .OrderBy(e => e.Date)
                        .Select(e => new GetProjectScheduleCalendarExceptionModel
                        {
                            Id = e.Id,
                            Date = e.Date,
                            IsWorking = e.IsWorking,
                            From = e.From,
                            To = e.To,
                            Description = e.Description
                        })
                        .ToList()
                })
                .ToList()
        };

        return response;
    }

    public async Task<Result<GetExportMppFileResponse?>> ExportMppFile(
        GetExportMppFileRequest request, CT ct)
    {
        var project = await _projectRepository.GetById(
            request.ProjectId,
            ct);

        if (project is null)
            return Result.Failure<GetExportMppFileResponse>(
                ProjectErrors.ProjectNotFound);

        var import = await _importRepository.GetByProjectId(
            request.ProjectId,
            ct);

        if (import is null)
            return Result.Failure<GetExportMppFileResponse>(
                ProjectErrors.ProjectNotFound);

        try
        {
            var tasks = await _taskRepository
                .GetByProjectScheduleImportId(import.Id, ct);

            var taskIds = tasks?
                .Select(x => x.Id)
                .ToList() ?? [];

            var dependencies = await _dependencyRepository
                .GetByTaskIds(taskIds, ct);

            var calendars = await _projectCalendarRepository
                .GetByProjectId(request.ProjectId, ct);

            var exportTasks = tasks ?? [];
            var uidById = BuildTaskUidMap(exportTasks);
            var calendarUidById = BuildCalendarUidMap(calendars);

            var customColumns = (await _columnRepository.GetByImportId(import.Id, ct))
                .Where(c => c.IsActive && c.ColumnType == ProjectScheduleColumnType.Custom)
                .OrderBy(c => c.SortOrder)
                .ToList();

            if (MppExportSlots.Exceeds(customColumns.Select(c => c.DataType)))
                return Result.Failure<GetExportMppFileResponse>(ProjectErrors.MppExportTooManyCustomColumns);

            var customColumnIds = customColumns.Select(c => c.Id).ToHashSet();
            var valuesByTask = (await _valueRepository.GetByImportId(import.Id, ct))
                .Where(v => customColumnIds.Contains(v.ProjectScheduleColumnId))
                .GroupBy(v => v.ProjectScheduleTaskId)
                .ToDictionary(g => g.Key, g => g.Select(v => new GetExportMppCustomValueModel
                {
                    ColumnId = v.ProjectScheduleColumnId,
                    StringValue = v.Value,
                    DecimalValue = v.NumberValue,
                    DateTimeValue = v.DateTimeValue
                }).ToList());

            var response = new GetExportMppFileResponse
            {
                ProjectName = project.ProjectName,
                StartDate = import.ScheduleStartDate,
                StatusDate = import.StatusDate,
                Tasks = BuildExportTasks(exportTasks, uidById, calendarUidById, valuesByTask),
                Dependencies = BuildExportDependencies(dependencies ?? [], uidById),
                Calendars = BuildExportCalendars(calendars, calendarUidById),
                CustomColumns = customColumns.Select(c => new GetExportMppCustomColumnModel
                {
                    Id = c.Id,
                    Title = c.TitleFa,
                    SortOrder = c.SortOrder,
                    DataType = c.DataType
                }).ToList()
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "data invalid. ProjectId:{ProjectId}", request.ProjectId);
            throw;
        }
    }

    public async Task<Result<CreateProjectScheduleResponse>> CreateProjectSchedule(
        CreateProjectScheduleRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectSchedule, ProjectId:{ProjectId}", request.ProjectId);

        var response = await _mediator.Send(new CreateProjectScheduleCommand(
            request.ProjectId,
            request.ScheduleStartDate,
            _userInfoService.UserId), ct);

        if (response.IsFailure)
            return Result.Failure<CreateProjectScheduleResponse>(response.Error!)!;

        return response!;
    }

    public async Task<Result<RemoveProjectScheduleResponse>> RemoveProjectSchedule(
        RemoveProjectScheduleRequest request, CT ct)
    {
        _logger.LogInformation("Request for RemoveProjectSchedule, ProjectId:{ProjectId}", request.ProjectId);

        var response = await _mediator.Send(new RemoveProjectScheduleCommand(request.ProjectId));

        if (response.IsFailure)
            return Result.Failure<RemoveProjectScheduleResponse>(response.Error!)!;

        return response!;
    }

    public async Task<Result<CreateProjectScheduledTaskResponse>> CreateProjectScheduledTask(
        CreateProjectScheduledTaskRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectScheduledTask, ProjectId:{ProjectId}", request.ProjectId);

        var import = await _importRepository.GetByProjectId(request.ProjectId, ct);
        if (import is null)
            return Result.Failure<CreateProjectScheduledTaskResponse>(ProjectErrors.ProjectNotFound)!;

        var response = await RunAndRecalculateAsync(request.ProjectId,
            () => _mediator.Send(new CreateProjectScheduledTaskCommand(
                import.Id, request.ParentTaskId, request.Title, request.SortOrder), ct), ct);

        if (response.IsFailure)
            return Result.Failure<CreateProjectScheduledTaskResponse>(response.Error!)!;

        return response!;
    }

    public async Task<Result<RemoveProjectScheduledTaskResponse>> RemoveProjectScheduledTask(
        RemoveProjectScheduledTaskRequest request, CT ct)
    {
        _logger.LogInformation("Request for RemoveProjectScheduledTask, Task:{TaskId}", request.TaskId);

        var projectId = await GetProjectIdByTaskAsync(request.TaskId, ct);
        if (projectId is null)
            return Result.Failure<RemoveProjectScheduledTaskResponse>(ProjectErrors.ProjectTaskNotFound)!;

        var response = await RunAndRecalculateAsync(projectId.Value,
            () => _mediator.Send(new RemoveProjectScheduledTaskCommand(request.TaskId), ct), ct);

        if (response.IsFailure)
            return Result.Failure<RemoveProjectScheduledTaskResponse>(response.Error!)!;
        return response!;
    }

    public async Task<Result<EditProjectScheduleTaskTitleResponse>> EditProjectScheduleTaskTitle(
        EditProjectScheduleTaskTitleRequest request, CT ct)
    {
        _logger.LogInformation("EditProjectScheduleTaskTitle");
        var result = await EditProjectScheduleTaskTitleCommand(request, ct);
        if (result.IsBad()) return result.Failure<EditProjectScheduleTaskTitleResponse>()!;
        return new EditProjectScheduleTaskTitleResponse(true);
    }

    public async Task<Result<EditActualStartProjectScheduleTaskResponse?>> EditActualStartProjectScheduleTask(
        EditActualStartProjectScheduleTaskRequest request, CT ct)
    {
        _logger.LogInformation("Request for Edit ActualStart ProjectScheduleTask, TaskId:{TaskId}", request.Id);

        var projectId = await GetProjectIdByTaskAsync(request.Id, ct);
        if (projectId is null)
            return Result.Failure<EditActualStartProjectScheduleTaskResponse>(ProjectErrors.ProjectTaskNotFound);

        var response = await RunAndRecalculateAsync(projectId.Value,
            () => _mediator.Send(new EditTaskActualDateTimesCommand(
                request.Id, request.DateTime, IsStart: true), ct), ct);

        if (response.IsFailure)
            return Result.Failure<EditActualStartProjectScheduleTaskResponse>(response.Error!);

        return new EditActualStartProjectScheduleTaskResponse(true);
    }

    public async Task<Result<EditActualFinishProjectScheduleTaskResponse?>> EditActualFinishProjectScheduleTask(
        EditActualFinishProjectScheduleTaskRequest request, CT ct)
    {
        _logger.LogInformation("Request for Edit ActualFinish ProjectScheduleTask, TaskId:{TaskId}", request.Id);

        var projectId = await GetProjectIdByTaskAsync(request.Id, ct);
        if (projectId is null)
            return Result.Failure<EditActualFinishProjectScheduleTaskResponse>(ProjectErrors.ProjectTaskNotFound);

        var response = await RunAndRecalculateAsync(projectId.Value,
            () => _mediator.Send(new EditTaskActualDateTimesCommand(
                request.Id, request.DateTime, IsStart: false), ct), ct);

        if (response.IsFailure)
            return Result.Failure<EditActualFinishProjectScheduleTaskResponse>(response.Error!);

        return new EditActualFinishProjectScheduleTaskResponse(true);
    }

    public async Task<Result<EditPercentCompleteResponse>> EditPercentComplete(
        EditPercentCompleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for Edit PercentComplete ProjectScheduleTask, TaskId:{TaskId}", request.Id);

        var projectId = await GetProjectIdByTaskAsync(request.Id, ct);
        if (projectId is null)
            return Result.Failure<EditPercentCompleteResponse>(ProjectErrors.ProjectTaskNotFound)!;

        var response = await RunAndRecalculateAsync(projectId.Value,
            () => _mediator.Send(new EditPercentCompleteCommand(request.Id, request.Percent), ct), ct);

        if (response.IsFailure)
            return Result.Failure<EditPercentCompleteResponse>(response.Error!)!;

        return new EditPercentCompleteResponse(true);
    }

    public async Task<Result<EditPhysicalPercentCompleteResponse>> EditPhysicalPercentComplete(
        EditPhysicalPercentCompleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for Edit PhysicalPercentComplete ProjectScheduleTask, TaskId:{TaskId}", request.Id);

        var response = await _mediator.Send(new EditPhysicalPercentCompleteCommand(
            request.Id, request.Percent), ct);
        if (response.IsFailure)
            return Result.Failure<EditPhysicalPercentCompleteResponse>(response.Error!)!;

        await _unitOfWork.CommitAsync(ct);
        return new EditPhysicalPercentCompleteResponse(true);
    }

    public async Task<Result<EditProjectTasksPredecessorsResponse>> EditProjectTasksPredecessors(
        EditProjectTasksPredecessorsRequest request, CT ct)
    {
        _logger.LogInformation("Request for EditProjectTasksPredecessors, TaskId:{TaskId}", request.Id);

        var projectId = await GetProjectIdByTaskAsync(request.Id, ct);
        if (projectId is null)
            return Result.Failure<EditProjectTasksPredecessorsResponse>(ProjectErrors.ProjectTaskNotFound)!;

        var result = await RunAndRecalculateAsync(projectId.Value,
            () => _mediator.Send(new EditProjectTasksPredecessorsCommand(request.Id, request.Predecessors), ct), ct);

        if (result.IsFailure)
            return Result.Failure<EditProjectTasksPredecessorsResponse>(result.Error!)!;
        return new EditProjectTasksPredecessorsResponse(true);
    }

    public async Task<Result<EditPlannedStartProjectScheduleTaskResponse>> EditPlannedStartProjectScheduleTask(
        EditPlannedStartProjectScheduleTaskRequest request, CT ct)
    {
        var projectId = await GetProjectIdByTaskAsync(request.Id, ct);
        if (projectId is null)
            return Result.Failure<EditPlannedStartProjectScheduleTaskResponse>(ProjectErrors.ProjectTaskNotFound)!;

        var result = await RunAndRecalculateAsync(projectId.Value,
            () => _mediator.Send(new EditTaskPlannedDateTimesCommand(
                request.Id,
                request.DateTime,
                true), ct), ct);

        if (result.IsFailure)
            return Result.Failure<EditPlannedStartProjectScheduleTaskResponse>(result.Error!)!;
        return new EditPlannedStartProjectScheduleTaskResponse(true);
    }

    public async Task<Result<EditPlannedFinishProjectScheduleTaskResponse>> EditPlannedFinishProjectScheduleTask(
        EditPlannedFinishProjectScheduleTaskRequest request, CT ct)
    {
        _logger.LogInformation("Request for Edit PlannedFinish ProjectScheduleTask, TaskId:{TaskId}", request.Id);

        var response = await _mediator.Send(new EditTaskPlannedDateTimesCommand(
            request.Id,
            request.DateTime,
            false), ct);
        if (response.IsFailure)
            return Result.Failure<EditPlannedFinishProjectScheduleTaskResponse>(response.Error!)!;

        await _unitOfWork.CommitAsync(ct);
        return new EditPlannedFinishProjectScheduleTaskResponse(true);
    }

    public async Task<Result<ReSchheduledProjectScheduleResponse>> ReScheduleProjectSchedule(
        ReSchheduledProjectScheduleRequest request, CT ct)
    {
        _logger.LogInformation("Request for ReScheduleProjectSchedule, ProjectId:{ProjectId}", request.Id);

        var project = await _mediator.Send(new GetProjectByImportIdQuery(request.Id), ct);
        if (project.IsFailure)
            return Result.Failure<ReSchheduledProjectScheduleResponse>(project.Error!)!;

        var response = await RunAndRecalculateAsync(project.Value!.Id,
            () => _mediator.Send(new ReSchheduledProjectScheduleCommand(request.Id, request.DateTime), ct), ct);

        if (response.IsFailure)
            return Result.Failure<ReSchheduledProjectScheduleResponse>(response.Error!)!;

        return new ReSchheduledProjectScheduleResponse(true);
    }

    public async Task<Result<EditProjectScheduleTaskDurationDaysResponse>> EditProjectScheduleTaskDurationDays(
        EditProjectScheduleTaskDurationDaysRequest request, CT ct)
    {
        var projectId = await GetProjectIdByTaskAsync(request.TaskId, ct);
        if (projectId is null)
            return Result.Failure<EditProjectScheduleTaskDurationDaysResponse>(ProjectErrors.ProjectTaskNotFound)!;

        return await RunAndRecalculateAsync(projectId.Value,
            () => EditProjectScheduleTaskDurationDaysCommand(request, ct), ct);
    }

    public async Task<Result<CreateProjectCalendarResponse>> CreateProjectCalendar(
        CreateProjectCalendarRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectCalendar, ProjectId:{ProjectId}", request.ProjectId);

        var response = await RunAndRecalculateAsync(request.ProjectId,
            () => _mediator.Send(new CreateProjectCalendarCommand(
                request.ProjectId, request.TitleFa, request.TitleEn, request.IsDefault,
                request.MinutesPerDay, request.WorkingDays, request.Exceptions), ct),
            ct, recalculate: request.IsDefault);

        if (response.IsFailure)
            return Result.Failure<CreateProjectCalendarResponse>(response.Error!)!;

        await _unitOfWork.CommitAsync(ct);
        return response!;
    }

    public async Task<Result<CloneProjectCalendarResponse>> CloneProjectCalendar(
        CloneProjectCalendarRequest request, CT ct)
    {
        _logger.LogInformation("Request for CloneProjectCalendar, TargetProjectId:{ProjectId}", request.TargetProjectId);

        var response = await _mediator.Send(new CloneProjectCalendarCommand(
            request.TargetProjectId,
            request.SourceCalendarId,
            request.NewTitleFa,
            request.SetAsDefault), ct);

        if (response.IsFailure)
            return Result.Failure<CloneProjectCalendarResponse>(response.Error!)!;

        await _unitOfWork.CommitAsync(ct);
        return response!;
    }

    public async Task<Result<EditProjectCalendarWorkingDaysResponse>> EditProjectCalendarWorkingDays(
        EditProjectCalendarWorkingDaysRequest request, CT ct)
    {
        var projectId = await GetProjectIdByCalendarAsync(request.CalendarId, ct);
        if (projectId is null)
            return Result.Failure<EditProjectCalendarWorkingDaysResponse>(ProjectErrors.CalendarNotFound)!;
            
        var response = await RunAndRecalculateAsync(projectId.Value,
            () => _mediator.Send(new EditProjectCalendarWorkingDaysCommand(
                request.CalendarId, request.WorkingDays), ct), ct);

        if (response.IsFailure)
            return Result.Failure<EditProjectCalendarWorkingDaysResponse>(response.Error!)!;

        return response!;
    }

    public async Task<Result<RemoveProjectCalendarExceptionResponse>> RemoveProjectCalendarException(
        RemoveProjectCalendarExceptionRequest request, CT ct)
    {
        var projectId = await GetProjectIdByCalendarAsync(request.CalendarId, ct);
        if (projectId is null)
            return Result.Failure<RemoveProjectCalendarExceptionResponse>(ProjectErrors.CalendarNotFound)!;

        var response = await RunAndRecalculateAsync(projectId.Value,
            () => _mediator.Send(new RemoveProjectCalendarExceptionCommand(
                request.CalendarId, request.ExceptionId), ct), ct);

        if (response.IsFailure)
            return Result.Failure<RemoveProjectCalendarExceptionResponse>(response.Error!)!;

        return response!;
    }

    public async Task<Result<EditProjectCalendarDetailsResponse>> EditProjectCalendarDetails(
        EditProjectCalendarDetailsRequest request, CT ct)
    {
        _logger.LogInformation("Request for EditProjectCalendarDetails, CalendarId:{CalendarId}", request.CalendarId);

        var response = await _mediator.Send(new EditProjectCalendarDetailsCommand(
            request.CalendarId,
            request.TitleFa,
            request.TitleEn,
            request.MinutesPerDay,
            request.IsDefault), ct);

        if (response.IsFailure)
            return Result.Failure<EditProjectCalendarDetailsResponse>(response.Error!)!;

        await _unitOfWork.CommitAsync(ct);
        return response!;
    }

    public async Task<Result<SetDefaultProjectCalendarResponse>> SetDefaultProjectCalendar(
        SetDefaultProjectCalendarRequest request, CT ct)
    {
        _logger.LogInformation("Request for SetDefaultProjectCalendar, CalendarId:{CalendarId}", request.CalendarId);

        var projectId = await GetProjectIdByCalendarAsync(request.CalendarId, ct);
        if (projectId is null)
            return Result.Failure<SetDefaultProjectCalendarResponse>(ProjectErrors.CalendarNotFound)!;

        var response = await RunAndRecalculateAsync(projectId.Value,
            () => _mediator.Send(new SetDefaultProjectCalendarCommand(request.CalendarId), ct), ct);

        if (response.IsFailure)
            return Result.Failure<SetDefaultProjectCalendarResponse>(response.Error!)!;

        return response!;
    }

    public async Task<Result<AddProjectCalendarExceptionResponse>> AddProjectCalendarException(
        AddProjectCalendarExceptionRequest request, CT ct)
    {
        _logger.LogInformation("AddProjectCalendarException, CalendarId:{CalendarId}", request.CalendarId);

        var projectId = await GetProjectIdByCalendarAsync(request.CalendarId, ct);
        if (projectId is null)
            return Result.Failure<AddProjectCalendarExceptionResponse>(ProjectErrors.CalendarNotFound)!;

        var response = await RunAndRecalculateAsync(projectId.Value,
            () => _mediator.Send(new AddProjectCalendarExceptionCommand(
                request.CalendarId, request.Date, request.IsWorking,
                request.Description, request.From, request.To), ct), ct);

        if (response.IsFailure)
            return Result.Failure<AddProjectCalendarExceptionResponse>(response.Error!)!;

        return response!;
    }

    public async Task<Result<EditProjectScheduleStartDateResponse>> EditProjectScheduleStartDate(
        EditProjectScheduleStartDateRequest request, CT ct)
    {
        _logger.LogInformation("EditProjectScheduleStartDate, ProjectSchedule Id:{ProjectId}", request.ProjectId);

        var response = await RunAndRecalculateAsync(request.ProjectId,
            () => _mediator.Send(new EditProjectScheduleStartDateCommand(request.ProjectId, request.StartDate), ct), ct);

        if (response.IsFailure)
            return Result.Failure<EditProjectScheduleStartDateResponse>(response.Error!)!;

        return response!;
    }

    public async Task<Result<MoveProjectScheduledTaskResponse>> MoveProjectScheduledTask(
        MoveProjectScheduledTaskRequest request, CT ct)
    {
        _logger.LogInformation("Request for MoveProjectScheduledTask, TaskId:{TaskId}", request.TaskId);

        var projectId = await GetProjectIdByTaskAsync(request.TaskId, ct);
        if (projectId is null)
            return Result.Failure<MoveProjectScheduledTaskResponse>(ProjectErrors.ProjectTaskNotFound)!;

        var response = await RunAndRecalculateAsync(projectId.Value,
            () => _mediator.Send(new MoveProjectScheduledTaskCommand(request.TaskId, request.NewIndex), ct),
            ct, recalculate: false);

        if (response.IsFailure)
            return Result.Failure<MoveProjectScheduledTaskResponse>(response.Error!)!;
        return response!;
    }

    public async Task<Result<CreateProjectScheduleColumnResponse>> CreateProjectScheduleColumn(
        CreateProjectScheduleColumnRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectScheduleColumn, ProjectId:{ProjectId}", request.ProjectId);

        var response = await _mediator.Send(new CreateProjectScheduleColumnCommand(
            request.ProjectId,
            request.TitleFa,
            request.TitleEn,
            request.DataType,
            request.TargetColumnId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateProjectScheduleColumnResponse>(response.Error!)!;

        return response!;
    }

    public async Task<Result<RemoveProjectScheduleColumnResponse>> RemoveProjectScheduleColumn(
        RemoveProjectScheduleColumnRequest request, CT ct)
    {
        _logger.LogInformation("Request for RemoveProjectScheduleColumn, ColumnId:{ColumnId}", request.Id);

        var response = await _mediator.Send(new RemoveProjectScheduleColumnCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<RemoveProjectScheduleColumnResponse>(response.Error!)!;

        await _unitOfWork.CommitAsync(ct);
        return response!;
    }

    public async Task<Result<EditProjectScheduleColumnResponse>> EditProjectScheduleColumn(
        EditProjectScheduleColumnRequest request, CT ct)
    {
        _logger.LogInformation("Request for EditProjectScheduleColumn, ColumnId:{ColumnId}", request.Id);

        var response = await _mediator.Send(new EditProjectScheduleColumnCommand(
            request.Id,
            request.TitleFa,
            request.TitleEn,
            request.Type,
            request.SortOrder,
            request.DataType), ct);

        if (response.IsFailure)
            return Result.Failure<EditProjectScheduleColumnResponse>(response.Error!)!;

        await _unitOfWork.CommitAsync(ct);
        return response!;
    }

    public async Task<Result<SetProjectScheduleTaskValueResponse>> SetProjectScheduleTaskValue(
        SetProjectScheduleTaskValueRequest request, CT ct)
    {
        _logger.LogInformation(
            "Request for SetProjectScheduleTaskValue, TaskId:{TaskId}, ColumnId:{ColumnId}",
            request.TaskId, request.ColumnId);

        var response = await _mediator.Send(new SetProjectScheduleTaskValueCommand(
            request.TaskId,
            request.ColumnId,
            request.Value,
            request.NumberValue,
            request.DateTimeValue), ct);

        if (response.IsFailure)
            return Result.Failure<SetProjectScheduleTaskValueResponse>(response.Error!)!;

        await _unitOfWork.CommitAsync(ct);
        return response!;
    }
}
