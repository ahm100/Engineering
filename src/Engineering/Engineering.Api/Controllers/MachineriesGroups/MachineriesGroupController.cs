using Engineering.Application.Services.MachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Models.ActiveMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.CreateMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.DisableMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.GetActiveMachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupByCode;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupById;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupByName;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineriesGroupExcelEnum;
using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineriesGroupExcelExporter;
using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineryGroupsForRequestMachinery;
using Engineering.Application.Services.MachineriesGroups.Models.InactiveMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupCodeCreator;
using Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupGroupDelete;
using Engineering.Application.Services.MachineriesGroups.Models.StateChangerMachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Models.UpdateMachineriesGroup;

namespace Engineering.Api.Controllers.MachineriesGroups;

[ApiController]
[Route("api/engineering/v1/MachineriesGroup")]
public class MachineriesGroupsController : ControllerBase
{
    private readonly IMachineriesGroupLogic _logic;

    public MachineriesGroupsController(IMachineriesGroupLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddMachineriesGroup")]
    [ResponseSchema<CreateMachineriesGroupResponse>]
    public async Task<IResult> AddMachineriesGroup([FromBody] CreateMachineriesGroupRequest request, CT ct)
    {
        var result = await _logic.CreateMachineriesGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("MachineriesGroupCodeCreator")]
    [ResponseSchema<MachineriesGroupCodeCreatorResponse>]
    public async Task<IResult> MachineriesGroupCodeCreator([FromBody] MachineriesGroupCodeCreatorRequest request, CT ct)
    {
        var result = await _logic.MachineriesGroupCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("MachineriesGroupGroupDelete")]
    [ResponseSchema<MachineriesGroupGroupDeleteResponse>]
    public async Task<IResult> MachineriesGroupGroupDelete([FromBody] MachineriesGroupGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.MachineriesGroupGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateMachineriesGroups")]
    [ResponseSchema<StateChangerMachineriesGroupsResponse>]
    public async Task<IResult> ActivateMachineriesGroups([FromBody] ActivateMachineriesGroupsRequest request, CT ct)
    {
        var result = await _logic.StateChangerMachineriesGroups(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateMachineriesGroups")]
    [ResponseSchema<StateChangerMachineriesGroupsResponse>]
    public async Task<IResult> InactivateMachineriesGroups([FromBody] InactivateMachineriesGroupsRequest request, CT ct)
    {
        var result = await _logic.StateChangerMachineriesGroups(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditMachineriesGroup")]
    [ResponseSchema<UpdateMachineriesGroupResponse>]
    public async Task<IResult> EditMachineriesGroup([FromBody] UpdateMachineriesGroupRequest request, CT ct)
    {
        var result = await _logic.UpdateMachineriesGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveMachineriesGroup")]
    [ResponseSchema<ActiveMachineriesGroupResponse>]
    public async Task<IResult> ActiveMachineriesGroup([FromBody] ActiveMachineriesGroupRequest request, CT ct)
    {
        var result = await _logic.ActiveMachineriesGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveMachineriesGroup")]
    [ResponseSchema<InactiveMachineriesGroupResponse>]
    public async Task<IResult> InactiveMachineriesGroup([FromBody] InactiveMachineriesGroupRequest request, CT ct)
    {
        var result = await _logic.InactiveMachineriesGroup(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetMachineriesGroupById")]
    [ResponseSchema<GetMachineriesGroupByIdResponse>]
    public async Task<IResult> GetMachineriesGroupById([FromQuery] GetMachineriesGroupByIdRequest request, CT ct)
    {
        var result = await _logic.GetMachineriesGroupById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetMachineriesGroupByName")]
    [ResponseSchema<GetMachineriesGroupByNameResponse>]
    public async Task<IResult> GetMachineriesGroupByName([FromQuery] GetMachineriesGroupByNameRequest request, CT ct)
    {
        var result = await _logic.GetMachineriesGroupByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetMachineriesGroupByCode")]
    [ResponseSchema<GetMachineriesGroupByCodeResponse>]
    public async Task<IResult> GetMachineriesGroupByCode([FromQuery] GetMachineriesGroupByCodeRequest request, CT ct)
    {
        var result = await _logic.GetMachineriesGroupByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveMachineriesGroup")]
    [ResponseSchema<GetActiveMachineriesGroupsResponse>]
    public async Task<IResult> GetsActiveMachineriesGroup([FromQuery] GetActiveMachineriesGroupsRequest request, CT ct)
    {
        var result = await _logic.GetActiveMachineriesGroups(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsMachineriesGroup")]
    [ResponseSchema<GetMachineriesGroupsResponse>]
    public async Task<IResult> GetsMachineriesGroup([FromQuery] GetMachineriesGroupsRequest request, CT ct)
    {
        var result = await _logic.GetMachineriesGroups(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsMachineryGroupsForRequestMachinery")]
    [ResponseSchema<GetsMachineryGroupsForRequestMachineryResponse>]
    public async Task<IResult> GetsMachineryGroupsForRequestMachinery([FromBody] GetsMachineryGroupsForRequestMachineryRequest request, CT ct)
    {
        var result = await _logic.GetsMachineryGroupsForRequestMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsMachineriesGroupExcelEnum")]
    [ResponseSchema<GetsMachineriesGroupExcelEnumResponse>]
    public async Task<IResult> GetsMachineriesGroupExcelEnum([FromQuery] GetsMachineriesGroupExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsMachineriesGroupExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsMachineriesGroupExcelExporter")]
    [ResponseSchema<GetsMachineriesGroupExcelExporterResponse>]
    public async Task<IResult> GetsMachineriesGroupExcelExporter([FromBody] GetsMachineriesGroupExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsMachineriesGroupExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableMachineriesGroup")]
    [ResponseSchema<DisableMachineriesGroupResponse>]
    public async Task<IResult> DisableMachineriesGroup([FromQuery] DisableMachineriesGroupRequest request, CT ct)
    {
        var result = await _logic.DisableMachineriesGroup(request, ct);
        return result.GetHttpResponse();
    }
}