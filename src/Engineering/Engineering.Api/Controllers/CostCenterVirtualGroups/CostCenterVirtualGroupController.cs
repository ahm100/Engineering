using Engineering.Application.Services.CostCenterVirtualGroups;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.CreateCostCenterVirtualGroup;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.CreateCostCenterVirtualGroupAdmin;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.DeleteCostCenterVirtualGroup;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.DeleteCostCenterVirtualGroupAdmin;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.GetCostCenterVirtualGroupByCostCenterId;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.UpdateCostCenterVirtualGroup;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.UpdateCostCenterVirtualGroupAdmin;

[ApiController]
[Route("api/engineering/v1/CostCenterVirtualGroup")]
public class CostCenterVirtualGroupController : ControllerBase
{
    private readonly ICostCenterVirtualGroupLogic _logic;

    public CostCenterVirtualGroupController(ICostCenterVirtualGroupLogic logic)
    {
        _logic = logic;
    }

    [HttpGet("GetFiltered")]
    [ResponseSchema<GetCostCenterVirtualGroupByCostCenterIdResponse>]
    public async Task<IResult> GetFiltered(
    [FromQuery] GetCostCenterVirtualGroupByCostCenterIdRequest request,
    CT ct)
    {
        var result = await _logic.GetCostCenterVirtualGroupByCostCenterIdAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("Create")]
    [ResponseSchema<CreateCostCenterVirtualGroupResponse>]
    public async Task<IResult> Create(
        [FromBody] CreateCostCenterVirtualGroupRequest request,
        CT ct)
    {
        var result = await _logic.CreateCostCenterVirtualGroupAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateAdmin")]
    [ResponseSchema<CreateCostCenterVirtualGroupAdminResponse>]
    public async Task<IResult> CreateAdmin(
        [FromBody] CreateCostCenterVirtualGroupAdminRequest request,
        CT ct)
    {
        var result = await _logic.CreateCostCenterVirtualGroupAdminAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("Update")]
    [ResponseSchema<UpdateCostCenterVirtualGroupResponse>]
    public async Task<IResult> Update(
        [FromBody] UpdateCostCenterVirtualGroupRequest request,
        CT ct)
    {
        var result = await _logic.UpdateCostCenterVirtualGroupAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateAdmin")]
    [ResponseSchema<UpdateCostCenterVirtualGroupAdminResponse>]
    public async Task<IResult> UpdateAdmin(
        [FromBody] UpdateCostCenterVirtualGroupAdminRequest request,
        CT ct)
    {
        var result = await _logic.UpdateCostCenterVirtualGroupAdminAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("Delete")]
    [ResponseSchema<DeleteCostCenterVirtualGroupResponse>]
    public async Task<IResult> Delete(
        [FromBody] DeleteCostCenterVirtualGroupRequest request,
        CT ct)
    {
        var result = await _logic.DeleteCostCenterVirtualGroupAsync(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteAdmin")]
    [ResponseSchema<DeleteCostCenterVirtualGroupAdminResponse>]
    public async Task<IResult> DeleteAdmin(
        [FromBody] DeleteCostCenterVirtualGroupAdminRequest request,
        CT ct)
    {
        var result = await _logic.DeleteCostCenterVirtualGroupAdminAsync(request, ct);
        return result.GetHttpResponse();
    }
}