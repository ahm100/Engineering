using Engineering.Application.Services.RequestMachineryStatusStatements;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.CreateRequestMachineryStatusStatement;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetFilteredRequestMachineryStatusStatement;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetRequestMachineryStatusStatementById;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetRequestMachineryStatusStatementStatus;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetRequestMachineryStatusStatementUnit;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetsRequestMachineryStatusStatementExcelEnum;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetsRequestMachineryStatusStatementExcelExporter;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.RequestMachineryStatusStatementStatusChanger;

namespace Engineering.Api.Controllers.RequestMachineryStatusStatements;

[ApiController]
[Route("api/engineering/v1/RequestMachineryStatusStatement")]
public class RequestMachineryStatusStatementController : ControllerBase
{
    private readonly IRequestMachineryStatusStatementLogic _logic;

    public RequestMachineryStatusStatementController(IRequestMachineryStatusStatementLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateRequestMachineryStatusStatement")]
    [ResponseSchema<CreateRequestMachineryStatusStatementResponse>]
    public async Task<IResult> CreateRequestMachineryStatusStatement(
    [FromBody] CreateRequestMachineryStatusStatementRequest request,
    CT ct)
    {
        var result = await _logic.CreateRequestMachineryStatusStatement(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRequestMachineryStatusStatementById")]
    [ResponseSchema<GetRequestMachineryStatusStatementByIdResponse>]
    public async Task<IResult> GetRequestMachineryStatusStatementById(
        [FromQuery] GetRequestMachineryStatusStatementByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetRequestMachineryStatusStatementById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRequestMachineryStatusStatementStatus")]
    [ResponseSchema<GetRequestMachineryStatusStatementStatusResponse>]
    public async Task<IResult> GetRequestMachineryStatusStatementStatus(
        [FromQuery] GetRequestMachineryStatusStatementStatusRequest request,
        CT ct)
    {
        var result = await _logic.GetRequestMachineryStatusStatementStatus(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRequestMachineryStatusStatementUnit")]
    [ResponseSchema<GetRequestMachineryStatusStatementUnitResponse>]
    public async Task<IResult> GetRequestMachineryStatusStatementUnit(
        [FromQuery] GetRequestMachineryStatusStatementUnitRequest request,
        CT ct)
    {
        var result = await _logic.GetRequestMachineryStatusStatementUnit(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredRequestMachineryStatusStatement")]
    [ResponseSchema<GetFilteredRequestMachineryStatusStatementResponse>]
    public async Task<IResult> GetFilteredRequestMachineryStatusStatement(
        [FromBody] GetFilteredRequestMachineryStatusStatementRequest request,
        CT ct)
    {
        var result = await _logic.GetFilteredRequestMachineryStatusStatement(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsRequestMachineryStatusStatementExcelExporter")]
    [ResponseSchema<GetsRequestMachineryStatusStatementExcelExporterResponse>]
    public async Task<IResult> GetsRequestMachineryStatusStatementExcelExporter(
        [FromBody] GetsRequestMachineryStatusStatementExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsRequestMachineryStatusStatementExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsRequestMachineryStatusStatementExcelEnum")]
    [ResponseSchema<GetsRequestMachineryStatusStatementExcelEnumResponse>]
    public async Task<IResult> GetsRequestMachineryStatusStatementExcelEnum(
        [FromQuery] GetsRequestMachineryStatusStatementExcelEnumRequest request,
        CT ct)
    {
        var result = await _logic.GetsRequestMachineryStatusStatementExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("RequestMachineryStatusStatementStatusChanger")]
    [ResponseSchema<RequestMachineryStatusStatementStatusChangerResponse>]
    public async Task<IResult> RequestMachineryStatusStatementStatusChanger(
        [FromBody] RequestMachineryStatusStatementStatusChangerRequest request,
        CT ct)
    {
        var result = await _logic.RequestMachineryStatusStatementStatusChanger(request, ct);
        return result.GetHttpResponse();
    }
}