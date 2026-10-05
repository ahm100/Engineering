using Engineering.Api.Helpers.MppTools;
using Engineering.Api.Helpers.XMLTools;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.ProjectWbses;
using Engineering.Application.Services.ProjectWbses.Contracts.AddProjectCalendarException;
using Engineering.Application.Services.ProjectWbses.Contracts.CloneProjectCalendar;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectCalendar;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectSchedule;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectScheduleColumn;
using Engineering.Application.Services.ProjectWbses.Contracts.CreateProjectScheduledTask;
using Engineering.Application.Services.ProjectWbses.Contracts.EditActualFinishProjectScheduleTask;
using Engineering.Application.Services.ProjectWbses.Contracts.EditActualStartProjectScheduleTask;
using Engineering.Application.Services.ProjectWbses.Contracts.EditPhysicalPercentComplete;
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

namespace Engineering.Api.Controllers.ProjectWbses;

[ApiController]
[Route("api/engineering/v1/projectWbs")]
public class ProjectWbsController : ControllerBase
{
    private readonly ILogger<ProjectWbsController> _logger;
    private readonly IProjectWbsLogic _logic;

    public ProjectWbsController(
        ILogger<ProjectWbsController> logger,
        IProjectWbsLogic logic)
    {
        _logger = logger;
        _logic = logic;
    }

    [HttpPost("ImportMppFile")]
    [ResponseSchema<ImportMppFileResponse>]
    public async Task<IResult> ImportMppFile(
        [FromForm] ImportMppFileRequest request, CT ct)
    {
        var result = await _logic.ImportMppFile(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectSchedule")]
    [ResponseSchema<GetProjectScheduleResponse>]
    public async Task<IResult> GetProjectSchedule(
        [FromQuery] GetProjectScheduleRequest request, CT ct)
    {
        var result = await _logic.GetProjectSchedule(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ExportMppXml")]
    [ResponseSchema<ExportMppXmlResponse>]
    public async Task<IResult> ExportMppXml(
        [FromBody] GetExportMppFileRequest request, CT ct)
    {
        _logger.LogInformation("ExportMppXml");
        var response = await _logic.ExportMppFile(request, ct);
        if (response.IsFailure)
            return Result.Failure<ExportMppXmlResponse>(response.Error!).GetHttpResponse();

        var xml = response.Value!.ExportToMppXml(r => r.ToMppImportModel());
        if (xml.IsFailure)
            return Result.Failure<ExportMppXmlResponse>(xml.Error!).GetHttpResponse();

        var result = new FileContentResult(xml.Value!, "application/xml")
        {
            FileDownloadName = $"Schedule-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xml",
            LastModified = DateTime.UtcNow,
        };

        return Result.Success<ExportMppXmlResponse?>(new(result)).GetHttpResponse();
    }

    [HttpPost("GetExportMppFile")]
    public async Task<IResult> GetExportMppFile(
        [FromBody] GetExportMppFileRequest request,
        CT ct)
    {
        _logger.LogInformation("GetExportMppFile");
        var response = await _logic.ExportMppFile(request, ct);
        if (response.IsFailure)
            return response.GetHttpResponse();

        var exportResult = response.Value!.ExportToMppXml(
            x => x.ToMppImportModel());
        if (exportResult.IsFailure)
            return exportResult.GetHttpResponse();

        return Results.File(
            exportResult.Value!,
            "application/xml",
            $"Project-{request.ProjectId}.xml");
    }

    [HttpPut("EditProjectScheduleTaskDurationDays")]
    [ResponseSchema<EditProjectScheduleTaskDurationDaysResponse>]
    public async Task<IResult> EditProjectScheduleTaskDurationDays(
        [FromBody] EditProjectScheduleTaskDurationDaysRequest request, CT ct)
    {
        _logger.LogInformation("EditProjectScheduleTaskDurationDays");
        var result = await _logic.EditProjectScheduleTaskDurationDays(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditProjectScheduleTaskTitle")]
    [ResponseSchema<EditProjectScheduleTaskTitleResponse>]
    public async Task<IResult> EditProjectScheduleTaskTitle(
        [FromBody] EditProjectScheduleTaskTitleRequest request, CT ct)
    {
        _logger.LogInformation("EditProjectScheduleTaskTitle");
        var result = await _logic.EditProjectScheduleTaskTitle(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditActualFinishProjectScheduleTask")]
    [ResponseSchema<EditActualFinishProjectScheduleTaskResponse>]
    public async Task<IResult> EditActualFinishProjectScheduleTask(
        [FromBody] EditActualFinishProjectScheduleTaskRequest request, CT ct)
    {
        _logger.LogInformation("EditActualFinishProjectScheduleTask");
        var result = await _logic.EditActualFinishProjectScheduleTask(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditActualStartProjectScheduleTask")]
    [ResponseSchema<EditActualStartProjectScheduleTaskResponse>]
    public async Task<IResult> EditActualStartProjectScheduleTask(
        [FromBody] EditActualStartProjectScheduleTaskRequest request, CT ct)
    {
        _logger.LogInformation("EditActualStartProjectScheduleTask");
        var result = await _logic.EditActualStartProjectScheduleTask(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditPhysicalPercentComplete")]
    [ResponseSchema<EditPhysicalPercentCompleteResponse>]
    public async Task<IResult> EditPhysicalPercentComplete(
        [FromBody] EditPhysicalPercentCompleteRequest request, CT ct)
    {
        _logger.LogInformation("EditPhysicalPercentComplete");
        var result = await _logic.EditPhysicalPercentComplete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditProjectTasksPredecessors")]
    [ResponseSchema<EditProjectTasksPredecessorsResponse>]
    public async Task<IResult> EditProjectTasksPredecessors(
        [FromBody] EditProjectTasksPredecessorsRequest request, CT ct)
    {
        _logger.LogInformation("EditProjectTasksPredecessors");
        var result = await _logic.EditProjectTasksPredecessors(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ReSchheduledProjectSchedule")]
    [ResponseSchema<ReSchheduledProjectScheduleResponse>]
    public async Task<IResult> ReSchheduledProjectSchedule(
        [FromBody] ReSchheduledProjectScheduleRequest request, CT ct)
    {
        _logger.LogInformation("ReSchheduledProjectSchedule");
        var result = await _logic.ReScheduleProjectSchedule(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateProjectScheduledTask")]
    [ResponseSchema<CreateProjectScheduledTaskResponse>]
    public async Task<IResult> CreateProjectScheduledTask(
        [FromBody] CreateProjectScheduledTaskRequest request, CT ct)
    {
        _logger.LogInformation("CreateProjectScheduledTask");
        var result = await _logic.CreateProjectScheduledTask(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("RemoveProjectScheduledTask")]
    [ResponseSchema<RemoveProjectScheduledTaskResponse>]
    public async Task<IResult> RemoveProjectScheduledTask(
        [FromBody] RemoveProjectScheduledTaskRequest request, CT ct)
    {
        _logger.LogInformation("RemoveProjectScheduledTask");
        var result = await _logic.RemoveProjectScheduledTask(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateProjectSchedule")]
    [ResponseSchema<CreateProjectScheduleResponse>]
    public async Task<IResult> CreateProjectSchedule(
        [FromBody] CreateProjectScheduleRequest request, CT ct)
    {
        _logger.LogInformation("CreateProjectSchedule");
        var result = await _logic.CreateProjectSchedule(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("RemoveProjectSchedule")]
    [ResponseSchema<RemoveProjectScheduleResponse>]
    public async Task<IResult> RemoveProjectSchedule(
        [FromBody] RemoveProjectScheduleRequest request, CT ct)
    {
        _logger.LogInformation("RemoveProjectSchedule");
        var result = await _logic.RemoveProjectSchedule(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateProjectCalendar")]
    [ResponseSchema<CreateProjectCalendarResponse>]
    public async Task<IResult> CreateProjectCalendar(
        [FromBody] CreateProjectCalendarRequest request, CT ct)
    {
        _logger.LogInformation("CreateProjectCalendar");
        var result = await _logic.CreateProjectCalendar(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CloneProjectCalendar")]
    [ResponseSchema<CloneProjectCalendarResponse>]
    public async Task<IResult> CloneProjectCalendar(
        [FromBody] CloneProjectCalendarRequest request, CT ct)
    {
        _logger.LogInformation("CloneProjectCalendar");
        var result = await _logic.CloneProjectCalendar(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditProjectCalendarWorkingDays")]
    [ResponseSchema<EditProjectCalendarWorkingDaysResponse>]
    public async Task<IResult> EditProjectCalendarWorkingDays(
        [FromBody] EditProjectCalendarWorkingDaysRequest request, CT ct)
    {
        _logger.LogInformation("EditProjectCalendarWorkingDays");
        var result = await _logic.EditProjectCalendarWorkingDays(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("RemoveProjectCalendarException")]
    [ResponseSchema<RemoveProjectCalendarExceptionResponse>]
    public async Task<IResult> RemoveProjectCalendarException(
        [FromBody] RemoveProjectCalendarExceptionRequest request, CT ct)
    {
        _logger.LogInformation("RemoveProjectCalendarException");
        var result = await _logic.RemoveProjectCalendarException(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditProjectCalendarDetails")]
    [ResponseSchema<EditProjectCalendarDetailsResponse>]
    public async Task<IResult> EditProjectCalendarDetails(
        [FromBody] EditProjectCalendarDetailsRequest request, CT ct)
    {
        _logger.LogInformation("EditProjectCalendarDetails");
        var result = await _logic.EditProjectCalendarDetails(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddProjectCalendarException")]
    [ResponseSchema<AddProjectCalendarExceptionResponse>]
    public async Task<IResult> AddProjectCalendarException(
        [FromBody] AddProjectCalendarExceptionRequest request, CT ct)
    {
        _logger.LogInformation("AddProjectCalendarException");
        var result = await _logic.AddProjectCalendarException(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetDefaultProjectCalendar")]
    [ResponseSchema<SetDefaultProjectCalendarResponse>]
    public async Task<IResult> SetDefaultProjectCalendar(
        [FromBody] SetDefaultProjectCalendarRequest request, CT ct)
    {
        _logger.LogInformation("SetDefaultProjectCalendar");
        var result = await _logic.SetDefaultProjectCalendar(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditProjectScheduleStartDate")]
    [ResponseSchema<EditProjectScheduleStartDateResponse>]
    public async Task<IResult> EditProjectScheduleStartDate(
        [FromBody] EditProjectScheduleStartDateRequest request, CT ct)
    {
        _logger.LogInformation("EditProjectScheduleStartDate");
        var result = await _logic.EditProjectScheduleStartDate(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("MoveProjectScheduledTask")]
    [ResponseSchema<MoveProjectScheduledTaskResponse>]
    public async Task<IResult> MoveProjectScheduledTask(
        [FromBody] MoveProjectScheduledTaskRequest request, CT ct)
    {
        _logger.LogInformation("MoveProjectScheduledTask by index and task id");
        var result = await _logic.MoveProjectScheduledTask(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateProjectScheduleColumn")]
    [ResponseSchema<CreateProjectScheduleColumnResponse>]
    public async Task<IResult> CreateProjectScheduleColumn(
        [FromBody] CreateProjectScheduleColumnRequest request, CT ct)
    {
        _logger.LogInformation("CreateProjectScheduleColumn for better scalability");
        var result = await _logic.CreateProjectScheduleColumn(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("RemoveProjectScheduleColumn")]
    [ResponseSchema<RemoveProjectScheduleColumnResponse>]
    public async Task<IResult> RemoveProjectScheduleColumn(
        [FromBody] RemoveProjectScheduleColumnRequest request, CT ct)
    {
        _logger.LogInformation("RemoveProjectScheduleColumn by id");
        var result = await _logic.RemoveProjectScheduleColumn(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditProjectScheduleColumn")]
    [ResponseSchema<EditProjectScheduleColumnResponse>]
    public async Task<IResult> EditProjectScheduleColumn(
        [FromBody] EditProjectScheduleColumnRequest request, CT ct)
    {
        _logger.LogInformation("EditProjectScheduleColumn by id");
        var result = await _logic.EditProjectScheduleColumn(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SetProjectScheduleTaskValue")]
    [ResponseSchema<SetProjectScheduleTaskValueResponse>]
    public async Task<IResult> SetProjectScheduleTaskValue(
        [FromBody] SetProjectScheduleTaskValueRequest request, CT ct)
    {
        _logger.LogInformation("SetProjectScheduleTaskValue by col id & task id");
        var result = await _logic.SetProjectScheduleTaskValue(request, ct);
        return result.GetHttpResponse();
    }
}