using Engineering.Application.Services.CostCenters;
using Engineering.Application.Services.CostCenters.Models.ActiveCostCenter;
using Engineering.Application.Services.CostCenters.Models.CodeCreator;
using Engineering.Application.Services.CostCenters.Models.CostCenterGroupDelete;
using Engineering.Application.Services.CostCenters.Models.CreateCostCenter;
using Engineering.Application.Services.CostCenters.Models.Delete;
using Engineering.Application.Services.CostCenters.Models.GetActiveCostCenters;
using Engineering.Application.Services.CostCenters.Models.GetCompaniesWork;
using Engineering.Application.Services.CostCenters.Models.GetContractorCostCenters;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterByCode;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterById;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterByName;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterHistories;
using Engineering.Application.Services.CostCenters.Models.GetCostCenters;
using Engineering.Application.Services.CostCenters.Models.GetFilteredCostCenterCities;
using Engineering.Application.Services.CostCenters.Models.GetsActiveAuthorizedCostCenter;
using Engineering.Application.Services.CostCenters.Models.GetsActiveMainWarehouseCostCenter;
using Engineering.Application.Services.CostCenters.Models.GetsAuthorizedCostCenter;
using Engineering.Application.Services.CostCenters.Models.GetsByAuthorizedRoleId;
using Engineering.Application.Services.CostCenters.Models.GetsByAuthorizedUserId;
using Engineering.Application.Services.CostCenters.Models.GetsByCityId;
using Engineering.Application.Services.CostCenters.Models.GetsByEmployerId;
using Engineering.Application.Services.CostCenters.Models.GetsByNameOrCode;
using Engineering.Application.Services.CostCenters.Models.GetsByTypeId;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterByContractorId;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterByIds;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterByProjectManagerId;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterByWarehouse;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterExcelEnum;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterExcelExporter;
using Engineering.Application.Services.CostCenters.Models.InactiveCostCenter;
using Engineering.Application.Services.CostCenters.Models.StateChangerCostCenters;
using Engineering.Application.Services.CostCenters.Models.UpdateCostCenter;

[ApiController]
[Route("api/engineering/v1/CostCenter")]
public class CostCenterController : ControllerBase
{
    private readonly ICostCenterLogic _logic;

    public CostCenterController(ICostCenterLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddCostCenter")]
    [ResponseSchema<CreateCostCenterResponse>]
    public async Task<IResult> AddCostCenter(
        [FromBody] CreateCostCenterRequest request, CT ct)
    {
        var result = await _logic.CreateCostCenter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CostCenterCodeCreator")]
    [ResponseSchema<CostCenterCodeCreatorResponse>]
    public async Task<IResult> CostCenterCodeCreator(
        [FromBody] CostCenterCodeCreatorRequest request, CT ct)
    {
        var result = await _logic.CodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CostCenterGroupDelete")]
    [ResponseSchema<CostCenterGroupDeleteResponse>]
    public async Task<IResult> CostCenterGroupDelete(
        [FromBody] CostCenterGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.CostCenterGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateCostCenters")]
    [ResponseSchema<StateChangerCostCentersResponse>]
    public async Task<IResult> ActivateCostCenters(
        [FromBody] ActivateCostCentersRequest request, CT ct)
    {
        var result = await _logic.StateChangerCostCenters(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateCostCenters")]
    [ResponseSchema<StateChangerCostCentersResponse>]
    public async Task<IResult> InactivateCostCenters(
        [FromBody] InactivateCostCentersRequest request, CT ct)
    {
        var result = await _logic.StateChangerCostCenters(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditCostCenter")]
    [ResponseSchema<UpdateCostCenterResponse>]
    public async Task<IResult> EditCostCenter(
        [FromBody] UpdateCostCenterRequest request, CT ct)
    {
        var result = await _logic.UpdateCostCenter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveCostCenter")]
    [ResponseSchema<ActiveCostCenterResponse>]
    public async Task<IResult> ActiveCostCenter(
        [FromBody] ActiveCostCenterRequest request, CT ct)
    {
        var result = await _logic.ActiveCostCenter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveCostCenter")]
    [ResponseSchema<InactiveCostCenterResponse>]
    public async Task<IResult> InactiveCostCenter(
        [FromBody] InactiveCostCenterRequest request, CT ct)
    {
        var result = await _logic.InactiveCostCenter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCostCenterById")]
    [ResponseSchema<GetCostCenterByIdResponse>]
    public async Task<IResult> GetCostCenterById(
        [FromQuery] GetCostCenterByIdRequest request, CT ct)
    {
        var result = await _logic.GetCostCenterById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCostCenterByName")]
    [ResponseSchema<GetCostCenterByNameResponse>]
    public async Task<IResult> GetCostCenterByName(
        [FromQuery] GetCostCenterByNameRequest request, CT ct)
    {
        var result = await _logic.GetCostCenterByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCostCenterByCode")]
    [ResponseSchema<GetCostCenterByCodeResponse>]
    public async Task<IResult> GetCostCenterByCode(
        [FromQuery] GetCostCenterByCodeRequest request, CT ct)
    {
        var result = await _logic.GetCostCenterByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveCostCenter")]
    [ResponseSchema<GetActiveCostCentersResponse>]
    public async Task<IResult> GetsActiveCostCenter(
        [FromQuery] GetActiveCostCentersRequest request, CT ct)
    {
        var result = await _logic.GetActiveCostCenters(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetContractorCostCenters")]
    [ResponseSchema<GetContractorCostCentersResponse>]
    public async Task<IResult> GetContractorCostCenters(
        [FromQuery] GetContractorCostCentersRequest request, CT ct)
    {
        var result = await _logic.GetContractorCostCenters(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveMainWarehouseCostCenter")]
    [ResponseSchema<GetsActiveMainWarehouseCostCenterResponse>]
    public async Task<IResult> GetsActiveMainWarehouseCostCenter(
        [FromQuery] GetsActiveMainWarehouseCostCenterRequest request, CT ct)
    {
        var result = await _logic.GetsActiveMainWarehouseCostCenter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveAuthorizedCostCenter")]
    [ResponseSchema<GetsActiveAuthorizedCostCenterResponse>]
    public async Task<IResult> GetsActiveAuthorizedCostCenter(
        [FromQuery] GetsActiveAuthorizedCostCenterRequest request, CT ct)
    {
        var result = await _logic.GetsActiveAuthorizedCostCenter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsAuthorizedCostCenter")]
    [ResponseSchema<GetsAuthorizedCostCenterResponse>]
    public async Task<IResult> GetsAuthorizedCostCenter(
        [FromQuery] GetsAuthorizedCostCenterRequest request, CT ct)
    {
        var result = await _logic.GetsAuthorizedCostCenter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCostCenter")]
    [ResponseSchema<GetCostCentersResponse>]
    public async Task<IResult> GetsCostCenter(
        [FromQuery] GetCostCentersRequest request, CT ct)
    {
        var result = await _logic.GetCostCenters(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByTypeId")]
    [ResponseSchema<GetsByTypeIdResponse>]
    public async Task<IResult> GetsByTypeId(
        [FromQuery] GetsByTypeIdRequest request, CT ct)
    {
        var result = await _logic.GetsByTypeId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByAuthorizedUserId")]
    [ResponseSchema<GetsByAuthorizedUserIdResponse>]
    public async Task<IResult> GetsByAuthorizedUserId(
        [FromQuery] GetsByAuthorizedUserIdRequest request, CT ct)
    {
        var result = await _logic.GetsByAuthorizedUserId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCostCenterByWarehouse")]
    [ResponseSchema<GetsCostCenterByWarehouseResponse>]
    public async Task<IResult> GetsCostCenterByWarehouse(
        [FromQuery] GetsCostCenterByWarehouseRequest request, CT ct)
    {
        var result = await _logic.GetsCostCenterByWarehouse(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByAuthorizedRoleId")]
    [ResponseSchema<GetsByAuthorizedRoleIdResponse>]
    public async Task<IResult> GetsByAuthorizedRoleId(
        [FromQuery] GetsByAuthorizedRoleIdRequest request, CT ct)
    {
        var result = await _logic.GetsByAuthorizedRoleId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByEmployerId")]
    [ResponseSchema<GetsCostCenterByEmployerIdResponse>]
    public async Task<IResult> GetsByEmployerId(
        [FromQuery] GetsCostCenterByEmployerIdRequest request, CT ct)
    {
        var result = await _logic.GetsByEmployerId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByCityId")]
    [ResponseSchema<GetsByCityIdResponse>]
    public async Task<IResult> GetsByCityId(
        [FromQuery] GetsByCityIdRequest request, CT ct)
    {
        var result = await _logic.GetsByCityId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByNameOrCode")]
    [ResponseSchema<GetsCostCenterByNameOrCodeResponse>]
    public async Task<IResult> GetsByNameOrCode(
        [FromQuery] GetsCostCenterByNameOrCodeRequest request, CT ct)
    {
        var result = await _logic.GetsByNameOrCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsCostCenterByIds")]
    [ResponseSchema<GetsCostCenterByIdsResponse>]
    public async Task<IResult> GetsCostCenterByIds(
        [FromBody] GetsCostCenterByIdsRequest request, CT ct)
    {
        var result = await _logic.GetsCostCenterByIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCostCenterByContractorId")]
    [ResponseSchema<GetsCostCenterByContractorIdResponse>]
    public async Task<IResult> GetsCostCenterByContractorId(
        [FromQuery] GetsCostCenterByContractorIdRequest request, CT ct)
    {
        var result = await _logic.GetsCostCenterByContractorId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCostCenterByProjectManagerId")]
    [ResponseSchema<GetsCostCenterByProjectManagerIdResponse>]
    public async Task<IResult> GetsCostCenterByProjectManagerId(
        [FromQuery] GetsCostCenterByProjectManagerIdRequest request, CT ct)
    {
        var result = await _logic.GetsCostCenterByProjectManagerId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredCostCenterCities")]
    [ResponseSchema<GetFilteredCostCenterCitiesResponse>]
    public async Task<IResult> GetFilteredCostCenterCities(
        [FromBody] GetFilteredCostCenterCitiesRequest request, CT ct)
    {
        var result = await _logic.GetFilteredCostCenterCities(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsCostCenterExcelExporter")]
    [ResponseSchema<GetsCostCenterExcelExporterResponse>]
    public async Task<IResult> GetsCostCenterExcelExporter(
        [FromBody] GetsCostCenterExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsCostCenterExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCostCenterExcelEnum")]
    [ResponseSchema<GetsCostCenterExcelEnumResponse>]
    public async Task<IResult> GetsCostCenterExcelEnum(
        [FromQuery] GetsCostCenterExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsCostCenterExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetCostCenterHistories")]
    [ResponseSchema<GetCostCenterHistoriesResponse>]
    public async Task<IResult> GetCostCenterHistories(
        [FromBody] GetCostCenterHistoriesRequest request, CT ct)
    {
        var result = await _logic.GetCostCenterHistories(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteCostCenter")]
    [ResponseSchema<DeleteCostCenterResponse>]
    public async Task<IResult> DeleteCostCenter(
        [FromQuery] DeleteCostCenterRequest request, CT ct)
    {
        var result = await _logic.DeleteCostCenter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCompaniesWork")]
    [ResponseSchema<GetCompaniesWorkResponse>]
    public async Task<IResult> GetCompaniesWork(
        [FromQuery] GetCompaniesWorkRequest request, CT ct)
    {
        var result = await _logic.GetCompaniesWork(request, ct);
        return result.GetHttpResponse();
    }
}