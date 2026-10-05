using Engineering.Application.Services.ProjectWarehouses;
using Engineering.Application.Services.ProjectWarehouses.Contracts.CreateProjectWarehouse;
using Engineering.Application.Services.ProjectWarehouses.Contracts.DeleteProjectWarehouse;
using Engineering.Application.Services.ProjectWarehouses.Contracts.DeleteProjectWarehouses;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetDefaultProjectWarehouse;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseAssets;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseById;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseInventory;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehousesByProjectId;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehousesByProjectIds;
using Engineering.Application.Services.ProjectWarehouses.Contracts.SaveProjectWarehouses;
using Engineering.Application.Services.ProjectWarehouses.Contracts.UpdateProjectWarehouse;

namespace Engineering.Api.Controllers.ProjectWarehouses;

[ApiController]
[Route("api/engineering/v1/ProjectWarehouse")]
public class ProjectWarehouseController : ControllerBase
{
    private readonly IProjectWarehouseLogic _logic;

    public ProjectWarehouseController(IProjectWarehouseLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateProjectWarehouse")]
    [ResponseSchema<CreateProjectWarehouseResponse>]
    public async Task<IResult> CreateProjectWarehouse(
        [FromBody] CreateProjectWarehouseRequest request,
        CT ct)
    {
        var result = await _logic.CreateProjectWarehouse(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SaveProjectWarehouses")]
    [ResponseSchema<SaveProjectWarehousesResponse>]
    public async Task<IResult> SaveProjectWarehouses(
        [FromBody] SaveProjectWarehousesRequest request,
        CT ct)
    {
        var result = await _logic.SaveProjectWarehouses(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateProjectWarehouse")]
    [ResponseSchema<UpdateProjectWarehouseResponse>]
    public async Task<IResult> UpdateProjectWarehouse(
        [FromBody] UpdateProjectWarehouseRequest request,
        CT ct)
    {
        var result = await _logic.UpdateProjectWarehouse(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteProjectWarehouse")]
    [ResponseSchema<DeleteProjectWarehouseResponse>]
    public async Task<IResult> DeleteProjectWarehouse(
        [FromBody] DeleteProjectWarehouseRequest request,
        CT ct)
    {
        var result = await _logic.DeleteProjectWarehouse(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteProjectWarehouses")]
    [ResponseSchema<DeleteProjectWarehousesResponse>]
    public async Task<IResult> DeleteProjectWarehouses(
        [FromBody] DeleteProjectWarehousesRequest request,
        CT ct)
    {
        var result = await _logic.DeleteProjectWarehouses(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectWarehouseById")]
    [ResponseSchema<GetProjectWarehouseByIdResponse>]
    public async Task<IResult> GetProjectWarehouseById(
        [FromBody] GetProjectWarehouseByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetProjectWarehouseById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectWarehousesByProjectId")]
    [ResponseSchema<GetProjectWarehousesByProjectIdResponse>]
    public async Task<IResult> GetProjectWarehousesByProjectId(
        [FromBody] GetProjectWarehousesByProjectIdRequest request,
        CT ct)
    {
        var result = await _logic.GetProjectWarehousesByProjectId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectWarehousesByProjectIds")]
    [ResponseSchema<GetProjectWarehousesByProjectIdsResponse>]
    public async Task<IResult> GetProjectWarehousesByProjectIds(
        [FromBody] GetProjectWarehousesByProjectIdsRequest request,
        CT ct)
    {
        var result = await _logic.GetProjectWarehousesByProjectIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetDefaultProjectWarehouse")]
    [ResponseSchema<GetDefaultProjectWarehouseResponse>]
    public async Task<IResult> GetDefaultProjectWarehouse(
        [FromBody] GetDefaultProjectWarehouseRequest request,
        CT ct)
    {
        var result = await _logic.GetDefaultProjectWarehouse(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectWarehouseInventory")]
    [ResponseSchema<GetProjectWarehouseInventoryResponse>]
    public async Task<IResult> GetProjectWarehouseInventory(
        [FromBody] GetProjectWarehouseInventoryRequest request,
        CT ct)
    {
        var result = await _logic.GetProjectWarehouseInventory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetProjectWarehouseAssets")]
    [ResponseSchema<GetProjectWarehouseAssetsResponse>]
    public async Task<IResult> GetProjectWarehouseAssets(
        [FromBody] GetProjectWarehouseAssetsRequest request,
        CT ct)
    {
        var result = await _logic.GetProjectWarehouseAssets(request, ct);
        return result.GetHttpResponse();
    }
}
