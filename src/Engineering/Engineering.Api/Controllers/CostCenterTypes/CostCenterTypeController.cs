using Engineering.Application.Services.CostCenterTypes;
using Engineering.Application.Services.CostCenterTypes.Models.ActiveCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.CodeCreator;
using Engineering.Application.Services.CostCenterTypes.Models.CostCenterTypeGroupDelete;
using Engineering.Application.Services.CostCenterTypes.Models.CreateCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.DisableCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByCode;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeById;
using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByName;
using Engineering.Application.Services.CostCenterTypes.Models.GetsActiveCostCenterTypes;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterTypeExcelEnum;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterTypeExcelExporter;
using Engineering.Application.Services.CostCenterTypes.Models.InactiveCostCenterType;
using Engineering.Application.Services.CostCenterTypes.Models.StateChangerCostCenterTypes;
using Engineering.Application.Services.CostCenterTypes.Models.UpdateCostCenterType;

[ApiController]
[Route("api/engineering/v1/CostCenterType")]
public class CostCenterTypeController : ControllerBase
{
    private readonly ICostCenterTypeLogic _logic;

    public CostCenterTypeController(ICostCenterTypeLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddCostCenterType")]
    [ResponseSchema<CreateCostCenterTypeResponse>]
    public async Task<IResult> AddCostCenterType(
    [FromBody] CreateCostCenterTypeRequest request,
    CT ct)
    {
        var result = await _logic.CreateCostCenterType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CostCenterTypeCodeCreator")]
    [ResponseSchema<CostCenterTypeCodeCreatorResponse>]
    public async Task<IResult> CostCenterTypeCodeCreator(
        [FromBody] CostCenterTypeCodeCreatorRequest request,
        CT ct)
    {
        var result = await _logic.CostCenterTypeCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CostCenterTypeGroupDelete")]
    [ResponseSchema<CostCenterTypeGroupDeleteResponse>]
    public async Task<IResult> CostCenterTypeGroupDelete(
        [FromBody] CostCenterTypeGroupDeleteRequest request,
        CT ct)
    {
        var result = await _logic.CostCenterTypeGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateCostCenterTypes")]
    [ResponseSchema<StateChangerCostCenterTypesResponse>]
    public async Task<IResult> ActivateCostCenterTypes(
        [FromBody] ActivateCostCenterTypesRequest request,
        CT ct)
    {
        var result = await _logic.StateChangerCostCenterTypes(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateCostCenterTypes")]
    [ResponseSchema<StateChangerCostCenterTypesResponse>]
    public async Task<IResult> InactivateCostCenterTypes(
        [FromBody] InactivateCostCenterTypesRequest request,
        CT ct)
    {
        var result = await _logic.StateChangerCostCenterTypes(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditCostCenterType")]
    [ResponseSchema<UpdateCostCenterTypeResponse>]
    public async Task<IResult> EditCostCenterType(
        [FromBody] UpdateCostCenterTypeRequest request,
        CT ct)
    {
        var result = await _logic.UpdateCostCenterType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveCostCenterType")]
    [ResponseSchema<ActiveCostCenterTypeResponse>]
    public async Task<IResult> ActiveCostCenterType(
        [FromBody] ActiveCostCenterTypeRequest request,
        CT ct)
    {
        var result = await _logic.ActiveCostCenterType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveCostCenterType")]
    [ResponseSchema<InactiveCostCenterTypeResponse>]
    public async Task<IResult> InactiveCostCenterType(
        [FromBody] InactiveCostCenterTypeRequest request,
        CT ct)
    {
        var result = await _logic.InactiveCostCenterType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCostCenterTypeById")]
    [ResponseSchema<GetCostCenterTypeByIdResponse>]
    public async Task<IResult> GetCostCenterTypeById(
        [FromQuery] GetCostCenterTypeByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetCostCenterTypeById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCostCenterTypeByCode")]
    [ResponseSchema<GetCostCenterTypeByCodeResponse>]
    public async Task<IResult> GetCostCenterTypeByCode(
        [FromQuery] GetCostCenterTypeByCodeRequest request,
        CT ct)
    {
        var result = await _logic.GetCostCenterTypeByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCostCenterTypeByName")]
    [ResponseSchema<GetCostCenterTypeByNameResponse>]
    public async Task<IResult> GetCostCenterTypeByName(
        [FromQuery] GetCostCenterTypeByNameRequest request,
        CT ct)
    {
        var result = await _logic.GetCostCenterTypeByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveCostCenterType")]
    [ResponseSchema<GetsActiveCostCenterTypesResponse>]
    public async Task<IResult> GetsActiveCostCenterType(
        [FromQuery] GetsActiveCostCenterTypesRequest request,
        CT ct)
    {
        var result = await _logic.GetsActiveCostCenterTypes(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCostCenterType")]
    [ResponseSchema<GetsCostCenterTypeResponse>]
    public async Task<IResult> GetsCostCenterType(
        [FromQuery] GetsCostCenterTypeRequest request,
        CT ct)
    {
        var result = await _logic.GetsCostCenterType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsCostCenterTypeExcelExporter")]
    [ResponseSchema<GetsCostCenterTypeExcelExporterResponse>]
    public async Task<IResult> GetsCostCenterTypeExcelExporter(
        [FromBody] GetsCostCenterTypeExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsCostCenterTypeExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCostCenterTypeExcelEnum")]
    [ResponseSchema<GetsCostCenterTypeExcelEnumResponse>]
    public async Task<IResult> GetsCostCenterTypeExcelEnum(
        [FromQuery] GetsCostCenterTypeExcelEnumRequest request,
        CT ct)
    {
        var result = await _logic.GetsCostCenterTypeExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableCostCenterType")]
    [ResponseSchema<DisableCostCenterTypeResponse>]
    public async Task<IResult> DisableCostCenterType(
        [FromQuery] DisableCostCenterTypeRequest request,
        CT ct)
    {
        var result = await _logic.DisableCostCenterType(request, ct);
        return result.GetHttpResponse();
    }
}