using Engineering.Application.Services.OperationInfoGroups;
using Engineering.Application.Services.OperationInfoGroups.Models.ActiveOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.CodeCreator;
using Engineering.Application.Services.OperationInfoGroups.Models.CreateOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.DisableOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.GetActiveOperationInfoGroups;
using Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupByCode;
using Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupById;
using Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupByName;
using Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroupExcelEnum;
using Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroupExcelExporter;
using Engineering.Application.Services.OperationInfoGroups.Models.InactiveOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupGroupDelete;
using Engineering.Application.Services.OperationInfoGroups.Models.StateChangerOperationInfoGroups;
using Engineering.Application.Services.OperationInfoGroups.Models.UpdateOperationInfoGroup;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/OperationInfoGroup")]
public class OperationInfoGroupController : ControllerBase
{
    private readonly IOperationInfoGroupLogic _logic;

    public OperationInfoGroupController(IOperationInfoGroupLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddOperationInfoGroup")]
    [ResponseSchema<CreateOperationInfoGroupResponse>]
    public async Task<IResult> AddOperationInfoGroup([FromBody] CreateOperationInfoGroupRequest request, CT ct)
    {
        var result = await _logic.CreateOperationInfoGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("OperationInfoGroupCodeCreator")]
    [ResponseSchema<OperationInfoGroupCodeCreatorResponse>]
    public async Task<IResult> OperationInfoGroupCodeCreator([FromBody] OperationInfoGroupCodeCreatorRequest request, CT ct)
    {
        var result = await _logic.OperationInfoGroupCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("OperationInfoGroupGroupDelete")]
    [ResponseSchema<OperationInfoGroupGroupDeleteResponse>]
    public async Task<IResult> OperationInfoGroupGroupDelete([FromBody] OperationInfoGroupGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.OperationInfoGroupGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateOperationInfoGroups")]
    [ResponseSchema<StateChangerOperationInfoGroupsResponse>]
    public async Task<IResult> ActivateOperationInfoGroups([FromBody] ActivateOperationInfoGroupsRequest request, CT ct)
    {
        var result = await _logic.StateChangerOperationInfoGroups(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateOperationInfoGroups")]
    [ResponseSchema<StateChangerOperationInfoGroupsResponse>]
    public async Task<IResult> InactivateOperationInfoGroups([FromBody] InactivateOperationInfoGroupsRequest request, CT ct)
    {
        var result = await _logic.StateChangerOperationInfoGroups(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditOperationInfoGroup")]
    [ResponseSchema<UpdateOperationInfoGroupResponse>]
    public async Task<IResult> EditOperationInfoGroup([FromBody] UpdateOperationInfoGroupRequest request, CT ct)
    {
        var result = await _logic.UpdateOperationInfoGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveOperationInfoGroup")]
    [ResponseSchema<ActiveOperationInfoGroupResponse>]
    public async Task<IResult> ActiveOperationInfoGroup([FromBody] ActiveOperationInfoGroupRequest request, CT ct)
    {
        var result = await _logic.ActiveOperationInfoGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveOperationInfoGroup")]
    [ResponseSchema<InactiveOperationInfoGroupResponse>]
    public async Task<IResult> InactiveOperationInfoGroup([FromBody] InactiveOperationInfoGroupRequest request, CT ct)
    {
        var result = await _logic.InactiveOperationInfoGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationInfoGroupById")]
    [ResponseSchema<GetOperationInfoGroupByIdResponse>]
    public async Task<IResult> GetOperationInfoGroupById([FromQuery] GetOperationInfoGroupByIdRequest request, CT ct)
    {
        var result = await _logic.GetOperationInfoGroupById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationInfoGroupByCode")]
    [ResponseSchema<GetOperationInfoGroupByCodeResponse>]
    public async Task<IResult> GetOperationInfoGroupByCode([FromQuery] GetOperationInfoGroupByCodeRequest request, CT ct)
    {
        var result = await _logic.GetOperationInfoGroupByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationInfoGroupByName")]
    [ResponseSchema<GetOperationInfoGroupByNameResponse>]
    public async Task<IResult> GetOperationInfoGroupByName([FromQuery] GetOperationInfoGroupByNameRequest request, CT ct)
    {
        var result = await _logic.GetOperationInfoGroupByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveOperationInfoGroup")]
    [ResponseSchema<GetActiveOperationInfoGroupsResponse>]
    public async Task<IResult> GetsActiveOperationInfoGroup([FromQuery] GetActiveOperationInfoGroupsRequest request, CT ct)
    {
        var result = await _logic.GetActiveOperationInfoGroups(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationInfoGroup")]
    [ResponseSchema<GetsOperationInfoGroupResponse>]
    public async Task<IResult> GetsOperationInfoGroup([FromQuery] GetsOperationInfoGroupRequest request, CT ct)
    {
        var result = await _logic.GetsOperationInfoGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationInfoGroupExcelEnum")]
    [ResponseSchema<GetsOperationInfoGroupExcelEnumResponse>]
    public async Task<IResult> GetsOperationInfoGroupExcelEnum([FromQuery] GetsOperationInfoGroupExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsOperationInfoGroupExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsOperationInfoGroupExcelExporter")]
    [ResponseSchema<GetsOperationInfoGroupExcelExporterResponse>]
    public async Task<IResult> GetsOperationInfoGroupExcelExporter([FromBody] GetsOperationInfoGroupExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsOperationInfoGroupExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableOperationInfoGroup")]
    [ResponseSchema<DisableOperationInfoGroupResponse>]
    public async Task<IResult> DisableOperationInfoGroup([FromQuery] DisableOperationInfoGroupRequest request, CT ct)
    {
        var result = await _logic.DisableOperationInfoGroup(request, ct);
        return result.GetHttpResponse();
    }
}