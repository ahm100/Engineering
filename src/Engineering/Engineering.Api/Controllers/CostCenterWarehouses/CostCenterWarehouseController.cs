using Engineering.Application.Services.CostCenterWarehouses;
using Engineering.Application.Services.CostCenterWarehouses.Models.CostCenterWarehouseGroupDelete;
using Engineering.Application.Services.CostCenterWarehouses.Models.Create;
using Engineering.Application.Services.CostCenterWarehouses.Models.Creates;
using Engineering.Application.Services.CostCenterWarehouses.Models.Delete;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetById;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetDefaultCostCenterId;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetsByCostCenterId;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseAssets;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseByCostCenterIds;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseInventory;
using Engineering.Application.Services.CostCenterWarehouses.Models.Update;

[ApiController]
[Route("api/engineering/v1/CostCenterWarehouse")]
public class CostCenterWarehouseController : ControllerBase
{
    private readonly ICostCenterWarehouseLogic _logic;

    public CostCenterWarehouseController(ICostCenterWarehouseLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddCostCenterWarehouse")]
    [ResponseSchema<CreateCostCenterWarehouseResponse>]
    public async Task<IResult> AddCostCenterWarehouse(
    [FromBody] CreateCostCenterWarehouseRequest request,
    CT ct)
    {
        var result = await _logic.CreateCostCenterWarehouse(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AddCostCenterWarehouses")]
    [ResponseSchema<CreatesCostCenterWarehouseResponse>]
    public async Task<IResult> AddCostCenterWarehouses(
        [FromBody] CreatesCostCenterWarehouseRequest request,
        CT ct)
    {
        var result = await _logic.CreateCostCenterWarehouses(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CenterWarehouseGroupDelete")]
    [ResponseSchema<CostCenterWarehouseGroupDeleteResponse>]
    public async Task<IResult> CenterWarehouseGroupDelete(
        [FromBody] CostCenterWarehouseGroupDeleteRequest request,
        CT ct)
    {
        var result = await _logic.CostCenterWarehouseGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditCostCenterWarehouse")]
    [ResponseSchema<UpdateCostCenterWarehouseResponse>]
    public async Task<IResult> EditCostCenterWarehouse(
        [FromBody] UpdateCostCenterWarehouseRequest request,
        CT ct)
    {
        var result = await _logic.UpdateCostCenterWarehouse(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetByCostCenterWarehouseId")]
    [ResponseSchema<GetCostCenterWarehouseByIdResponse>]
    public async Task<IResult> GetByCostCenterWarehouseId(
        [FromQuery] GetCostCenterWarehouseByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetCostCenterWarehouseById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsCostCenterWarehouseByCostCenterIds")]
    [ResponseSchema<GetsCostCenterWarehouseByCostCenterIdsResponse>]
    public async Task<IResult> GetsCostCenterWarehouseByCostCenterIds(
        [FromBody] GetsCostCenterWarehouseByCostCenterIdsRequest request,
        CT ct)
    {
        var result = await _logic.GetsCostCenterWarehouseByCostCenterIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByCostCenterId")]
    [ResponseSchema<GetsCostCenterWarehouseByCostCenterIdResponse>]
    public async Task<IResult> GetsByCostCenterId(
        [FromQuery] GetsCostCenterWarehouseByCostCenterIdRequest request,
        CT ct)
    {
        var result = await _logic.GetsCostCenterWarehouseByCostCenterId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetDefaultCostCenterId")]
    [ResponseSchema<GetDefaultCostCenterWarehouseByCostCenterIdResponse>]
    public async Task<IResult> GetDefaultCostCenterId(
        [FromQuery] GetDefaultCostCenterWarehouseByCostCenterIdRequest request,
        CT ct)
    {
        var result = await _logic.GetDefaultCostCenterWarehouseByCostCenterId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCostCenterWarehouseInventory")]
    [ResponseSchema<GetsCostCenterWarehouseInventoryResponse>]
    public async Task<IResult> GetsCostCenterWarehouseInventory(
        [FromQuery] GetsCostCenterWarehouseInventoryRequest request,
        CT ct)
    {
        var result = await _logic.GetsCostCenterWarehouseInventory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCostCenterWarehouseAssets")]
    [ResponseSchema<GetsCostCenterWarehouseAssetsResponse>]
    public async Task<IResult> GetsCostCenterWarehouseAssets(
        [FromQuery] GetsCostCenterWarehouseAssetsRequest request,
        CT ct)
    {
        var result = await _logic.GetsCostCenterWarehouseAssets(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteCostCenterWarehouse")]
    [ResponseSchema<DeleteCostCenterWarehouseResponse>]
    public async Task<IResult> DeleteCostCenterWarehouse(
        [FromBody] DeleteCostCenterWarehouseRequest request,
        CT ct)
    {
        var result = await _logic.DeleteCostCenterWarehouse(request, ct);
        return result.GetHttpResponse();
    }
}