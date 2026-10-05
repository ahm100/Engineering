using Engineering.Application.Services.SessionRecords;
using Engineering.Application.Services.SessionRecords.Contracts.CreateSessionRecord;
using Engineering.Application.Services.SessionRecords.Contracts.EditSessionRecord;
using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecordDetail;
using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecords;
using Engineering.Application.Services.SessionRecords.Contracts.GetUserSessionRecordAction;
using Engineering.Application.Services.SessionRecords.Contracts.RemoveSessionRecord;
using Gita.Backend.Shared.Domain.Errors;
using System.ComponentModel;

namespace Engineering.Api.Controllers.SessionRecords;

[ApiController]
[Route("api/engineering/v1/sessionrecord")]
public class SessionRecordController : ControllerBase
{
    private readonly ILogger<SessionRecordController> _logger;
    private readonly ISessionRecordLogic _logic;

    public SessionRecordController(
        ISessionRecordLogic logic,
        ILogger<SessionRecordController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    [HttpPost("CreateSessionRecord")]
    [Description("CreateSessionRecord")]
    [ResponseSchema<CreateSessionRecordResponse>]
    public async Task<IResult> CreateSessionRecord(
        [FromBody] CreateSessionRecordRequest request, CT ct)
    {
        _logger.LogInformation("CreateSessionRecord");
        var result = await _logic.CreateSessionRecord(request, ct);

        if (result is null)
            return Result.Failure<CreateSessionRecordResponse>(SharedErrors.UnknownError).GetHttpResponse();

        return result.GetHttpResponse();
    }

    [HttpGet("GetSessionRecords")]
    [Description("GetSessionRecords")]
    [ResponseSchema<GetSessionRecordsResponse>]
    public async Task<IResult> GetSessionRecords(
        [FromQuery] GetSessionRecordsRequest request, CT ct)
    {
        _logger.LogInformation("GetSessionRecords");
        var result = await _logic.GetSessionRecords(request, ct);

        if (result is null)
            return Result.Failure<GetSessionRecordsResponse>(SharedErrors.UnknownError).GetHttpResponse();

        return result.GetHttpResponse();
    }

    [HttpGet("GetUserSessionRecordAction")]
    [Description("GetUserSessionRecordAction")]
    [ResponseSchema<GetUserSessionRecordActionResponse>]
    public async Task<IResult> GetUserSessionRecordAction(
        [FromQuery] GetUserSessionRecordActionRequest request, CT ct)
    {
        _logger.LogInformation("GetUserSessionRecordAction");
        var result = await _logic.GetUserSessionRecordAction(request, ct);

        if (result is null)
            return Result.Failure<GetUserSessionRecordActionResponse>(SharedErrors.UnknownError).GetHttpResponse();

        return result.GetHttpResponse();
    }

    [HttpPut("EditSessionRecord")]
    [Description("EditSessionRecord")]
    [ResponseSchema<EditSessionRecordResponse>]
    public async Task<IResult> EditSessionRecord(
        [FromBody] EditSessionRecordRequest request, CT ct)
    {
        _logger.LogInformation("EditSessionRecord");
        var result = await _logic.EditSessionRecord(request, ct);

        if (result is null)
            return Result.Failure<EditSessionRecordResponse>(SharedErrors.UnknownError).GetHttpResponse();

        return result.GetHttpResponse();
    }

    [HttpGet("GetSessionRecordDetail")]
    [Description("GetSessionRecordDetail by id")]
    [ResponseSchema<GetSessionRecordDetailResponse>]
    public async Task<IResult> GetSessionRecordDetail(
        [FromQuery] GetSessionRecordDetailRequest request, CT ct)
    {
        _logger.LogInformation("GetSessionRecordDetail");
        var result = await _logic.GetSessionRecordDetail(request, ct);

        if (result is null)
            return Result.Failure<GetSessionRecordDetailResponse>(SharedErrors.UnknownError).GetHttpResponse();

        return result.GetHttpResponse();
    }

    [HttpDelete("RemoveSessionRecord")]
    [Description("RemoveSessionRecord")]
    [ResponseSchema<RemoveSessionRecordResponse>]
    public async Task<IResult> RemoveSessionRecord(
        [FromQuery] RemoveSessionRecordRequest request, CT ct)
    {
        _logger.LogInformation("RemoveSessionRecord");
        var result = await _logic.RemoveSessionRecord(request, ct);

        if (result is null)
            return Result.Failure<RemoveSessionRecordResponse>(SharedErrors.UnknownError).GetHttpResponse();

        return result.GetHttpResponse();
    }
}