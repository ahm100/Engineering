using Engineering.Application.Services.MachineTypes;
using Engineering.Application.Services.MachineTypes.Models.ActiveMachineType;
using Engineering.Application.Services.MachineTypes.Models.CreateMachineType;
using Engineering.Application.Services.MachineTypes.Models.DisableMachineType;
using Engineering.Application.Services.MachineTypes.Models.GetMachineTypeById;
using Engineering.Application.Services.MachineTypes.Models.GetMachineTypes;
using Engineering.Application.Services.MachineTypes.Models.GetsActiveMachineType;
using Engineering.Application.Services.MachineTypes.Models.GetsMachineTypeExcelEnum;
using Engineering.Application.Services.MachineTypes.Models.GetsMachineTypeExcelExporter;
using Engineering.Application.Services.MachineTypes.Models.InactiveMachineType;
using Engineering.Application.Services.MachineTypes.Models.MachineTypeCodeCreator;
using Engineering.Application.Services.MachineTypes.Models.MachineTypeGroupDelete;
using Engineering.Application.Services.MachineTypes.Models.StateChangerMachineTypes;
using Engineering.Application.Services.MachineTypes.Models.UpdateMachineType;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/MachineType")]
public class MachineTypeController : ControllerBase
{
    private readonly IMachineTypeLogic _logic;

    public MachineTypeController(IMachineTypeLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddMachineType")]
    [ResponseSchema<CreateMachineTypeResponse>]
    public async Task<IResult> AddMachineType([FromBody] CreateMachineTypeRequest request, CT ct)
    {
        var result = await _logic.CreateMachineType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("MachineTypeCodeCreator")]
    [ResponseSchema<MachineTypeCodeCreatorResponse>]
    public async Task<IResult> MachineTypeCodeCreator([FromBody] MachineTypeCodeCreatorRequest request, CT ct)
    {
        var result = await _logic.MachineTypeCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("MachineTypeGroupDelete")]
    [ResponseSchema<MachineTypeGroupDeleteResponse>]
    public async Task<IResult> MachineTypeGroupDelete([FromBody] MachineTypeGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.MachineTypeGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateMachineTypes")]
    [ResponseSchema<StateChangerMachineTypesResponse>]
    public async Task<IResult> ActivateMachineTypes([FromBody] ActivateMachineTypesRequest request, CT ct)
    {
        var result = await _logic.StateChangerMachineTypes(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateMachineTypes")]
    [ResponseSchema<StateChangerMachineTypesResponse>]
    public async Task<IResult> InactivateMachineTypes([FromBody] InactivateMachineTypesRequest request, CT ct)
    {
        var result = await _logic.StateChangerMachineTypes(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditMachineType")]
    [ResponseSchema<UpdateMachineTypeResponse>]
    public async Task<IResult> EditMachineType([FromBody] UpdateMachineTypeRequest request, CT ct)
    {
        var result = await _logic.UpdateMachineType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveMachineType")]
    [ResponseSchema<ActiveMachineTypeResponse>]
    public async Task<IResult> ActiveMachineType([FromBody] ActiveMachineTypeRequest request, CT ct)
    {
        var result = await _logic.ActiveMachineType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InActiveMachineType")]
    [ResponseSchema<InactiveMachineTypeResponse>]
    public async Task<IResult> InActiveMachineType([FromBody] InactiveMachineTypeRequest request, CT ct)
    {
        var result = await _logic.InactiveMachineType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetMachineTypeById")]
    [ResponseSchema<GetMachineTypeByIdResponse>]
    public async Task<IResult> GetMachineTypeById([FromQuery] GetMachineTypeByIdRequest request, CT ct)
    {
        var result = await _logic.GetMachineTypeById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsMachineType")]
    [ResponseSchema<GetMachineTypesResponse>]
    public async Task<IResult> GetsMachineType([FromQuery] GetMachineTypesRequest request, CT ct)
    {
        var result = await _logic.GetMachineTypes(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveMachineType")]
    [ResponseSchema<GetsActiveMachineTypeResponse>]
    public async Task<IResult> GetsActiveMachineType([FromQuery] GetsActiveMachineTypeRequest request, CT ct)
    {
        var result = await _logic.GetsActiveMachineType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsMachineTypeExcelEnum")]
    [ResponseSchema<GetsMachineTypeExcelEnumResponse>]
    public async Task<IResult> GetsMachineTypeExcelEnum([FromQuery] GetsMachineTypeExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsMachineTypeExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsMachineTypeExcelExporter")]
    [ResponseSchema<GetsMachineTypeExcelExporterResponse>]
    public async Task<IResult> GetsMachineTypeExcelExporter([FromBody] GetsMachineTypeExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsMachineTypeExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableMachineType")]
    [ResponseSchema<DisableMachineTypeResponse>]
    public async Task<IResult> DisableMachineType([FromQuery] DisableMachineTypeRequest request, CT ct)
    {
        var result = await _logic.DisableMachineType(request, ct);
        return result.GetHttpResponse();
    }
}