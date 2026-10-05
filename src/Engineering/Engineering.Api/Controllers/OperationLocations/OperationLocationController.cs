using Engineering.Application.Services.OperationLocations;
using Engineering.Application.Services.OperationLocations.Models.ActiveOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationCodeWithCostCenter;
using Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationCodeWithParent;
using Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationWithCostCenter;
using Engineering.Application.Services.OperationLocations.Models.CreateOperationLocationWithParent;
using Engineering.Application.Services.OperationLocations.Models.DisableOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.GetActiveOperationLocations;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByCode;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationById;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByName;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationCodeByCostCenter;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationNameByCostCenter;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocations;
using Engineering.Application.Services.OperationLocations.Models.GetsByCostCenterId;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationChild;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelEnum;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelExporter;
using Engineering.Application.Services.OperationLocations.Models.GetsWithoutParentOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.InactiveOperationLocation;
using Engineering.Application.Services.OperationLocations.Models.OperationLocationGroupDelete;
using Engineering.Application.Services.OperationLocations.Models.SetPriority;
using Engineering.Application.Services.OperationLocations.Models.StateChangerOperationLocations;
using Engineering.Application.Services.OperationLocations.Models.UpdateOperationLocation;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/OperationLocation")]
public class OperationLocationController : ControllerBase
{
    private readonly IOperationLocationLogic _logic;

    public OperationLocationController(IOperationLocationLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddOperationLocationWithCostCenter")]
    [ResponseSchema<CreateOperationLocationWithCostCenterResponse>]
    public async Task<IResult> AddOperationLocationWithCostCenter([FromBody] CreateOperationLocationWithCostCenterRequest request, CT ct)
    {
        var result = await _logic.CreateOperationLocationWithCostCenter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddOperationLocationWithParent")]
    [ResponseSchema<CreateOperationLocationWithParentResponse>]
    public async Task<IResult> AddOperationLocationWithParent([FromBody] CreateOperationLocationWithParentRequest request, CT ct)
    {
        var result = await _logic.CreateOperationLocationWithParent(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("NewOperationLocationCodeWithCostCenter")]
    [ResponseSchema<CreateOperationLocationCodeWithCostCenterResponse>]
    public async Task<IResult> NewOperationLocationCodeWithCostCenter([FromBody] CreateOperationLocationCodeWithCostCenterRequest request, CT ct)
    {
        var result = await _logic.CreateOperationLocationCodeWithCostCenter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("NewOperationLocationCodeWithParent")]
    [ResponseSchema<CreateOperationLocationCodeWithParentResponse>]
    public async Task<IResult> NewOperationLocationCodeWithParent([FromBody] CreateOperationLocationCodeWithParentRequest request, CT ct)
    {
        var result = await _logic.CreateOperationLocationCodeWithParent(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("OperationLocationGroupDelete")]
    [ResponseSchema<OperationLocationGroupDeleteResponse>]
    public async Task<IResult> OperationLocationGroupDelete([FromBody] OperationLocationGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.OperationLocationGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateOperationLocations")]
    [ResponseSchema<StateChangerOperationLocationsResponse>]
    public async Task<IResult> ActivateOperationLocations([FromBody] ActivateOperationLocationsRequest request, CT ct)
    {
        var result = await _logic.StateChangerOperationLocations(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateOperationLocations")]
    [ResponseSchema<StateChangerOperationLocationsResponse>]
    public async Task<IResult> InactivateOperationLocations([FromBody] InactivateOperationLocationsRequest request, CT ct)
    {
        var result = await _logic.StateChangerOperationLocations(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditOperationLocation")]
    [ResponseSchema<UpdateOperationLocationResponse>]
    public async Task<IResult> EditOperationLocation([FromBody] UpdateOperationLocationRequest request, CT ct)
    {
        var result = await _logic.UpdateOperationLocation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetOperationLocationPriority")]
    [ResponseSchema<SetOperationLocationPriorityResponse>]
    public async Task<IResult> SetOperationLocationPriority([FromBody] SetOperationLocationPriorityRequest request, CT ct)
    {
        var result = await _logic.SetOperationLocationPriority(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveOperationLocation")]
    [ResponseSchema<ActiveOperationLocationResponse>]
    public async Task<IResult> ActiveOperationLocation([FromBody] ActiveOperationLocationRequest request, CT ct)
    {
        var result = await _logic.ActiveOperationLocation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveOperationLocation")]
    [ResponseSchema<InactiveOperationLocationResponse>]
    public async Task<IResult> InactiveOperationLocation([FromBody] InactiveOperationLocationRequest request, CT ct)
    {
        var result = await _logic.InactiveOperationLocation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationLocationById")]
    [ResponseSchema<GetOperationLocationByIdResponse>]
    public async Task<IResult> GetOperationLocationById([FromQuery] GetOperationLocationByIdRequest request, CT ct)
    {
        var result = await _logic.GetOperationLocationById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationLocationCodeByCostCenter")]
    [ResponseSchema<GetOperationLocationCodeByCostCenterResponse>]
    public async Task<IResult> GetOperationLocationCodeByCostCenter([FromQuery] GetOperationLocationCodeByCostCenterRequest request, CT ct)
    {
        var result = await _logic.GetOperationLocationCodeByCostCenter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationLocationNameByCostCenter")]
    [ResponseSchema<GetOperationLocationNameByCostCenterResponse>]
    public async Task<IResult> GetOperationLocationNameByCostCenter([FromQuery] GetOperationLocationNameByCostCenterRequest request, CT ct)
    {
        var result = await _logic.GetOperationLocationNameByCostCenter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationLocationByName")]
    [ResponseSchema<GetOperationLocationByNameResponse>]
    public async Task<IResult> GetOperationLocationByName([FromQuery] GetOperationLocationByNameRequest request, CT ct)
    {
        var result = await _logic.GetOperationLocationByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationLocationByCode")]
    [ResponseSchema<GetOperationLocationByCodeResponse>]
    public async Task<IResult> GetOperationLocationByCode([FromQuery] GetOperationLocationByCodeRequest request, CT ct)
    {
        var result = await _logic.GetOperationLocationByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveOperationLocation")]
    [ResponseSchema<GetActiveOperationLocationsResponse>]
    public async Task<IResult> GetsActiveOperationLocation([FromQuery] GetActiveOperationLocationsRequest request, CT ct)
    {
        var result = await _logic.GetsActiveOperationLocation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationLocation")]
    [ResponseSchema<GetOperationLocationsResponse>]
    public async Task<IResult> GetsOperationLocation([FromQuery] GetOperationLocationsRequest request, CT ct)
    {
        var result = await _logic.GetOperationLocations(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsOperationLocation")]
    [ResponseSchema<GetsOperationLocationResponse>]
    public async Task<IResult> GetsOperationLocationPost([FromBody] GetsOperationLocationRequest request, CT ct)
    {
        var result = await _logic.GetsOperationLocation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationLocationChildList")]
    [ResponseSchema<GetsOperationLocationChildResponse>]
    public async Task<IResult> GetsOperationLocationChildList([FromQuery] GetsOperationLocationChildRequest request, CT ct)
    {
        var result = await _logic.GetsOperationLocationChild(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByCostCenterId")]
    [ResponseSchema<GetsOperationLocationByCostCenterIdResponse>]
    public async Task<IResult> GetsByCostCenterId([FromQuery] GetsOperationLocationByCostCenterIdRequest request, CT ct)
    {
        var result = await _logic.GetsByCostCenterId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsWithoutParentOperationLocation")]
    [ResponseSchema<GetsWithoutParentOperationLocationResponse>]
    public async Task<IResult> GetsWithoutParentOperationLocation([FromQuery] GetsWithoutParentOperationLocationRequest request, CT ct)
    {
        var result = await _logic.GetsWithoutParentOperationLocation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationLocationExcelEnum")]
    [ResponseSchema<GetsOperationLocationExcelEnumResponse>]
    public async Task<IResult> GetsOperationLocationExcelEnum([FromQuery] GetsOperationLocationExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsOperationLocationExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsOperationLocationExcelExporter")]
    [ResponseSchema<GetsOperationLocationExcelExporterResponse>]
    public async Task<IResult> GetsOperationLocationExcelExporter([FromBody] GetsOperationLocationExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsOperationLocationExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableOperationLocation")]
    [ResponseSchema<DisableOperationLocationResponse>]
    public async Task<IResult> DisableOperationLocation([FromQuery] DisableOperationLocationRequest request, CT ct)
    {
        var result = await _logic.DisableOperationLocation(request, ct);
        return result.GetHttpResponse();
    }
}