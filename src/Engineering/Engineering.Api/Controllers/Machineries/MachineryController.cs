using Engineering.Application.Services.Machineries;
using Engineering.Application.Services.Machineries.Models.ActiveMachinery;
using Engineering.Application.Services.Machineries.Models.CreateMachinery;
using Engineering.Application.Services.Machineries.Models.DisableMachinery;
using Engineering.Application.Services.Machineries.Models.GetActiveMachineries;
using Engineering.Application.Services.Machineries.Models.GetMachineries;
using Engineering.Application.Services.Machineries.Models.GetMachineryByCode;
using Engineering.Application.Services.Machineries.Models.GetMachineryById;
using Engineering.Application.Services.Machineries.Models.GetMachineryByName;
using Engineering.Application.Services.Machineries.Models.GetsByMachineriesGroupId;
using Engineering.Application.Services.Machineries.Models.GetsMachineryExcelEnum;
using Engineering.Application.Services.Machineries.Models.GetsMachineryExcelExporter;
using Engineering.Application.Services.Machineries.Models.GetsMachineryForRequestMachinery;
using Engineering.Application.Services.Machineries.Models.InactiveMachinery;
using Engineering.Application.Services.Machineries.Models.MachineryCodeCreator;
using Engineering.Application.Services.Machineries.Models.MachineryGroupDelete;
using Engineering.Application.Services.Machineries.Models.StateChangerMachineries;
using Engineering.Application.Services.Machineries.Models.UpdateMachinery;

namespace Engineering.Api.Controllers.Machineries;

[ApiController]
[Route("api/engineering/v1/Machinery")]
public class MachineryController : ControllerBase
{
    private readonly IMachineryLogic _logic;

    public MachineryController(IMachineryLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddMachinery")]
    [ResponseSchema<CreateMachineryResponse>]
    public async Task<IResult> AddMachinery([FromBody] CreateMachineryRequest request, CT ct)
    {
        var result = await _logic.CreateMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("MachineryCodeCreator")]
    [ResponseSchema<MachineryCodeCreatorResponse>]
    public async Task<IResult> MachineryCodeCreator([FromBody] MachineryCodeCreatorRequest request, CT ct)
    {
        var result = await _logic.MachineryCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("MachineryGroupDelete")]
    [ResponseSchema<MachineryGroupDeleteResponse>]
    public async Task<IResult> MachineryGroupDelete([FromBody] MachineryGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.MachineryGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateMachineries")]
    [ResponseSchema<StateChangerMachineriesResponse>]
    public async Task<IResult> ActivateMachineries([FromBody] ActivateMachineriesRequest request, CT ct)
    {
        var result = await _logic.StateChangerMachineries(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateMachineries")]
    [ResponseSchema<StateChangerMachineriesResponse>]
    public async Task<IResult> InactivateMachineries([FromBody] InactivateMachineriesRequest request, CT ct)
    {
        var result = await _logic.StateChangerMachineries(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditMachinery")]
    [ResponseSchema<UpdateMachineryResponse>]
    public async Task<IResult> EditMachinery([FromBody] UpdateMachineryRequest request, CT ct)
    {
        var result = await _logic.UpdateMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveMachinery")]
    [ResponseSchema<ActiveMachineryResponse>]
    public async Task<IResult> ActiveMachinery([FromBody] ActiveMachineryRequest request, CT ct)
    {
        var result = await _logic.ActiveMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveMachinery")]
    [ResponseSchema<InactiveMachineryResponse>]
    public async Task<IResult> InactiveMachinery([FromBody] InactiveMachineryRequest request, CT ct)
    {
        var result = await _logic.InactiveMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetMachineryById")]
    [ResponseSchema<GetMachineryByIdResponse>]
    public async Task<IResult> GetMachineryById([FromQuery] GetMachineryByIdRequest request, CT ct)
    {
        var result = await _logic.GetMachineryById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetMachineryByName")]
    [ResponseSchema<GetMachineryByNameResponse>]
    public async Task<IResult> GetMachineryByName([FromQuery] GetMachineryByNameRequest request, CT ct)
    {
        var result = await _logic.GetMachineryByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetMachineryByCode")]
    [ResponseSchema<GetMachineryByCodeResponse>]
    public async Task<IResult> GetMachineryByCode([FromQuery] GetMachineryByCodeRequest request, CT ct)
    {
        var result = await _logic.GetMachineryByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveMachinery")]
    [ResponseSchema<GetActiveMachineriesResponse>]
    public async Task<IResult> GetsActiveMachinery([FromQuery] GetActiveMachineriesRequest request, CT ct)
    {
        var result = await _logic.GetsActiveMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsMachinery")]
    [ResponseSchema<GetMachineriesResponse>]
    public async Task<IResult> GetsMachinery([FromQuery] GetMachineriesRequest request, CT ct)
    {
        var result = await _logic.GetsMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByMachineriesGroupId")]
    [ResponseSchema<GetsByMachineriesGroupIdResponse>]
    public async Task<IResult> GetsByMachineriesGroupId([FromQuery] GetsByMachineriesGroupIdRequest request, CT ct)
    {
        var result = await _logic.GetsByMachineriesGroupId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsMachineryForRequestMachinery")]
    [ResponseSchema<GetsMachineryForRequestMachineryResponse>]
    public async Task<IResult> GetsMachineryForRequestMachinery([FromQuery] GetsMachineryForRequestMachineryRequest request, CT ct)
    {
        var result = await _logic.GetsMachineryForRequestMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsMachineryExcelExporter")]
    [ResponseSchema<GetsMachineryExcelExporterResponse>]
    public async Task<IResult> GetsMachineryExcelExporter([FromBody] GetsMachineryExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsMachineryExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsMachineryExcelEnum")]
    [ResponseSchema<GetsMachineryExcelEnumResponse>]
    public async Task<IResult> GetsMachineryExcelEnum([FromQuery] GetsMachineryExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsMachineryExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableMachinery")]
    [ResponseSchema<DisableMachineryResponse>]
    public async Task<IResult> DisableMachinery([FromQuery] DisableMachineryRequest request, CT ct)
    {
        var result = await _logic.DisableMachinery(request, ct);
        return result.GetHttpResponse();
    }
}