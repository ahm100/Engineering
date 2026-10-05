using Engineering.Application.Services.OperationInfos;
using Engineering.Application.Services.OperationInfos.Models.ActiveOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.AddSeasonsToOperationInfos;
using Engineering.Application.Services.OperationInfos.Models.CodeCreator;
using Engineering.Application.Services.OperationInfos.Models.CreateOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.CreateOperationInfoActions;
using Engineering.Application.Services.OperationInfos.Models.DeleteOperationInfoAction;
using Engineering.Application.Services.OperationInfos.Models.DisableOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.GetActiveOperationInfoBySeasonIds;
using Engineering.Application.Services.OperationInfos.Models.GetActiveOperationInfos;
using Engineering.Application.Services.OperationInfos.Models.GetOIActionByOperationInfoId;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoAction;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoByCode;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoById;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoByName;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfoContractors;
using Engineering.Application.Services.OperationInfos.Models.GetOperationInfos;
using Engineering.Application.Services.OperationInfos.Models.GetsBySeasonId;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoByContractorIds;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoExcelEnum;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoExcelExporter;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoHistoryById;
using Engineering.Application.Services.OperationInfos.Models.GetsPrioritizeOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.InactiveOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoActions;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoGroupDelete;
using Engineering.Application.Services.OperationInfos.Models.SetOperationInfoPriority;
using Engineering.Application.Services.OperationInfos.Models.StateChangerOperationInfos;
using Engineering.Application.Services.OperationInfos.Models.UpdateOperationInfo;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/OperationInfo")]
public class OperationInfoController : ControllerBase
{
    private readonly IOperationInfoLogic _logic;

    public OperationInfoController(IOperationInfoLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddOperationInfo")]
    [ResponseSchema<CreateOperationInfoResponse>]
    public async Task<IResult> AddOperationInfo([FromBody] CreateOperationInfoRequest request, CT ct)
    {
        var result = await _logic.CreateOperationInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateOperationInfoAction")]
    [ResponseSchema<CreateOperationInfoActionResponse>]
    public async Task<IResult> CreateOperationInfoAction([FromBody] CreateOperationInfoActionRequest request, CT ct)
    {
        var result = await _logic.CreateOperationInfoAction(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateOperationInfoActions")]
    [ResponseSchema<CreateOperationInfoActionsResponse>]
    public async Task<IResult> CreateOperationInfoActions([FromBody] CreateOperationInfoActionsRequest request, CT ct)
    {
        var result = await _logic.CreateOperationInfoActions(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("OperationInfoCodeCreator")]
    [ResponseSchema<OperationInfoCodeCreatorResponse>]
    public async Task<IResult> OperationInfoCodeCreator([FromBody] OperationInfoCodeCreatorRequest request, CT ct)
    {
        var result = await _logic.CodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("OperationInfoGroupDelete")]
    [ResponseSchema<OperationInfoGroupDeleteResponse>]
    public async Task<IResult> OperationInfoGroupDelete([FromBody] OperationInfoGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.OperationInfoGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateOperationInfos")]
    [ResponseSchema<StateChangerOperationInfosResponse>]
    public async Task<IResult> ActivateOperationInfos([FromBody] ActivateOperationInfosRequest request, CT ct)
    {
        var result = await _logic.StateChangerOperationInfos(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateOperationInfos")]
    [ResponseSchema<StateChangerOperationInfosResponse>]
    public async Task<IResult> InactivateOperationInfos([FromBody] InactivateOperationInfosRequest request, CT ct)
    {
        var result = await _logic.StateChangerOperationInfos(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditOperationInfo")]
    [ResponseSchema<UpdateOperationInfoResponse>]
    public async Task<IResult> EditOperationInfo([FromBody] UpdateOperationInfoRequest request, CT ct)
    {
        var result = await _logic.UpdateOperationInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveOperationInfo")]
    [ResponseSchema<ActiveOperationInfoResponse>]
    public async Task<IResult> ActiveOperationInfo([FromBody] ActiveOperationInfoRequest request, CT ct)
    {
        var result = await _logic.ActiveOperationInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveOperationInfo")]
    [ResponseSchema<InactiveOperationInfoResponse>]
    public async Task<IResult> InactiveOperationInfo([FromBody] InactiveOperationInfoRequest request, CT ct)
    {
        var result = await _logic.InactiveOperationInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateSetPriority")]
    [ResponseSchema<SetOperationInfoPriorityResponse>]
    public async Task<IResult> UpdateSetPriority([FromBody] SetOperationInfoPriorityRequest request, CT ct)
    {
        var result = await _logic.SetOperationInfoPriority(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("AddSeasonsToOperationInfos")]
    [ResponseSchema<AddSeasonsToOperationInfosResponse>]
    public async Task<IResult> AddSeasonsToOperationInfos([FromBody] AddSeasonsToOperationInfosRequest request, CT ct)
    {
        var result = await _logic.AddSeasonsToOperationInfos(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationInfoById")]
    [ResponseSchema<GetOperationInfoByIdResponse>]
    public async Task<IResult> GetOperationInfoById([FromQuery] GetOperationInfoByIdRequest request, CT ct)
    {
        var result = await _logic.GetOperationInfoById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationInfoByName")]
    [ResponseSchema<GetOperationInfoByNameResponse>]
    public async Task<IResult> GetOperationInfoByName([FromQuery] GetOperationInfoByNameRequest request, CT ct)
    {
        var result = await _logic.GetOperationInfoByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationInfoByCode")]
    [ResponseSchema<GetOperationInfoByCodeResponse>]
    public async Task<IResult> GetOperationInfoByCode([FromQuery] GetOperationInfoByCodeRequest request, CT ct)
    {
        var result = await _logic.GetOperationInfoByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationInfoHistoryById")]
    [ResponseSchema<GetsOperationInfoHistoryByIdResponse>]
    public async Task<IResult> GetsOperationInfoHistoryById([FromQuery] GetsOperationInfoHistoryByIdRequest request, CT ct)
    {
        var result = await _logic.GetsOperationInfoHistoryById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOIActionByOperationInfoId")]
    [ResponseSchema<GetOIActionByOperationInfoIdResponse>]
    public async Task<IResult> GetOIActionByOperationInfoId([FromQuery] GetOIActionByOperationInfoIdRequest request, CT ct)
    {
        var result = await _logic.GetOIActionByOperationInfoId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveOperationInfo")]
    [ResponseSchema<GetActiveOperationInfosResponse>]
    public async Task<IResult> GetsActiveOperationInfo([FromQuery] GetActiveOperationInfosRequest request, CT ct)
    {
        var result = await _logic.GetActiveOperationInfos(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationInfo")]
    [ResponseSchema<GetOperationInfosResponse>]
    public async Task<IResult> GetsOperationInfo([FromQuery] GetOperationInfosRequest request, CT ct)
    {
        var result = await _logic.GetOperationInfos(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsBySeasonId")]
    [ResponseSchema<GetsBySeasonIdResponse>]
    public async Task<IResult> GetsBySeasonId([FromQuery] GetsBySeasonIdRequest request, CT ct)
    {
        var result = await _logic.GetsBySeasonId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsPrioritizeOperationInfo")]
    [ResponseSchema<GetsPrioritizeOperationInfoResponse>]
    public async Task<IResult> GetsPrioritizeOperationInfo([FromQuery] GetsPrioritizeOperationInfoRequest request, CT ct)
    {
        var result = await _logic.GetsPrioritizeOperationInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationInfoContractors")]
    [ResponseSchema<GetOperationInfoContractorsResponse>]
    public async Task<IResult> GetOperationInfoContractors([FromQuery] GetOperationInfoContractorsRequest request, CT ct)
    {
        var result = await _logic.GetOperationInfoContractors(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsOperationInfoByContractorIds")]
    [ResponseSchema<GetsOperationInfoByContractorIdsResponse>]
    public async Task<IResult> GetsOperationInfoByContractorIds([FromBody] GetsOperationInfoByContractorIdsRequest request, CT ct)
    {
        var result = await _logic.GetsOperationInfoByContractorIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsOperationInfoExcelExporter")]
    [ResponseSchema<GetsOperationInfoExcelExporterResponse>]
    public async Task<IResult> GetsOperationInfoExcelExporter([FromBody] GetsOperationInfoExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsOperationInfoExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationInfoExcelEnum")]
    [ResponseSchema<GetsOperationInfoExcelEnumResponse>]
    public async Task<IResult> GetsOperationInfoExcelEnum([FromQuery] GetsOperationInfoExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsOperationInfoExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableOperationInfo")]
    [ResponseSchema<DisableOperationInfoResponse>]
    public async Task<IResult> DisableOperationInfo([FromQuery] DisableOperationInfoRequest request, CT ct)
    {
        var result = await _logic.DisableOperationInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetActiveOperationInfoBySeasonIds")]
    [ResponseSchema<GetActiveOperationInfoBySeasonIdsResponse>]
    public async Task<IResult> GetActiveOperationInfoBySeasonIds([FromBody] GetActiveOperationInfoBySeasonIdsRequest request, CT ct)
    {
        var result = await _logic.GetActiveOperationInfoBySeasonIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetOperationInfoActions")]
    [ResponseSchema<GetOperationInfoActionsResponse>]
    public async Task<IResult> GetOperationInfoActions([FromBody] GetOperationInfoActionsRequest request, CT ct)
    {
        var result = await _logic.GetOperationInfoActions(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("DeleteOperationInfoAction")]
    [ResponseSchema<DeleteOperationInfoActionResponse>]
    public async Task<IResult> DeleteOperationInfoAction([FromBody] DeleteOperationInfoActionRequest request, CT ct)
    {
        var result = await _logic.DeleteOperationInfoAction(request, ct);
        return result.GetHttpResponse();
    }
}