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

namespace Engineering.Application.Services.ProjectWbses;

public interface IProjectWbsLogic
{
    Task<Result<ImportMppFileResponse?>> ImportMppFile(
        ImportMppFileRequest request, CT ct);

    Task<Result<GetProjectScheduleResponse?>> GetProjectSchedule(
        GetProjectScheduleRequest request, CT ct);

    Task<Result<GetExportMppFileResponse?>> ExportMppFile(
        GetExportMppFileRequest request, CT ct);

    Task<Result<EditProjectScheduleTaskTitleResponse>> EditProjectScheduleTaskTitle(
        EditProjectScheduleTaskTitleRequest request, CT ct);

    Task<Result<EditProjectScheduleTaskDurationDaysResponse>> EditProjectScheduleTaskDurationDays(
        EditProjectScheduleTaskDurationDaysRequest request, CT ct);

    Task<Result<EditActualFinishProjectScheduleTaskResponse?>> EditActualFinishProjectScheduleTask(
        EditActualFinishProjectScheduleTaskRequest request, CT ct);

    Task<Result<EditActualStartProjectScheduleTaskResponse?>> EditActualStartProjectScheduleTask(
        EditActualStartProjectScheduleTaskRequest request, CT ct);

    Task<Result<EditPercentCompleteResponse>> EditPercentComplete(
        EditPercentCompleteRequest request, CT ct);

    Task<Result<EditPhysicalPercentCompleteResponse>> EditPhysicalPercentComplete(
        EditPhysicalPercentCompleteRequest request, CT ct);

    Task<Result<EditPlannedStartProjectScheduleTaskResponse>> EditPlannedStartProjectScheduleTask(
        EditPlannedStartProjectScheduleTaskRequest request, CT ct);

    Task<Result<EditPlannedFinishProjectScheduleTaskResponse>> EditPlannedFinishProjectScheduleTask(
        EditPlannedFinishProjectScheduleTaskRequest request, CT ct);

    Task<Result<EditProjectTasksPredecessorsResponse>> EditProjectTasksPredecessors(
        EditProjectTasksPredecessorsRequest request, CT ct);

    Task<Result<ReSchheduledProjectScheduleResponse>> ReScheduleProjectSchedule(
            ReSchheduledProjectScheduleRequest request, CT ct);

    Task<Result<CreateProjectScheduledTaskResponse>> CreateProjectScheduledTask(
        CreateProjectScheduledTaskRequest request, CT ct);

    Task<Result<RemoveProjectScheduledTaskResponse>> RemoveProjectScheduledTask(
        RemoveProjectScheduledTaskRequest request, CT ct);

    Task<Result<CreateProjectScheduleResponse>> CreateProjectSchedule(
        CreateProjectScheduleRequest request, CT ct);

    Task<Result<RemoveProjectScheduleResponse>> RemoveProjectSchedule(
        RemoveProjectScheduleRequest request, CT ct);

    Task<Result<CreateProjectCalendarResponse>> CreateProjectCalendar(
        CreateProjectCalendarRequest request, CT ct);

    Task<Result<CloneProjectCalendarResponse>> CloneProjectCalendar(
        CloneProjectCalendarRequest request, CT ct);

    Task<Result<EditProjectCalendarWorkingDaysResponse>> EditProjectCalendarWorkingDays(
        EditProjectCalendarWorkingDaysRequest request, CT ct);

    Task<Result<RemoveProjectCalendarExceptionResponse>> RemoveProjectCalendarException(
        RemoveProjectCalendarExceptionRequest request, CT ct);

    Task<Result<EditProjectCalendarDetailsResponse>> EditProjectCalendarDetails(
        EditProjectCalendarDetailsRequest request, CT ct);

    Task<Result<SetDefaultProjectCalendarResponse>> SetDefaultProjectCalendar(
        SetDefaultProjectCalendarRequest request, CT ct);

    Task<Result<AddProjectCalendarExceptionResponse>> AddProjectCalendarException(
        AddProjectCalendarExceptionRequest request, CT ct);

    Task<Result<EditProjectScheduleStartDateResponse>> EditProjectScheduleStartDate(
        EditProjectScheduleStartDateRequest request, CT ct);

    Task<Result<MoveProjectScheduledTaskResponse>> MoveProjectScheduledTask(
        MoveProjectScheduledTaskRequest request, CT ct);

    Task<Result<CreateProjectScheduleColumnResponse>> CreateProjectScheduleColumn(
        CreateProjectScheduleColumnRequest request, CT ct);

    Task<Result<RemoveProjectScheduleColumnResponse>> RemoveProjectScheduleColumn(
        RemoveProjectScheduleColumnRequest request, CT ct);

    Task<Result<EditProjectScheduleColumnResponse>> EditProjectScheduleColumn(
        EditProjectScheduleColumnRequest request, CT ct);

    Task<Result<SetProjectScheduleTaskValueResponse>> SetProjectScheduleTaskValue(
        SetProjectScheduleTaskValueRequest request, CT ct);
}
