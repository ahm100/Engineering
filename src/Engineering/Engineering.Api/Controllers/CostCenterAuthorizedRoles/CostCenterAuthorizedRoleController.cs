using Engineering.Application.Services.CostCenterAuthorizedRoles;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Models.AuthorizedRoleGetsByCostCenterId;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Models.CreateAuthorizedRoles;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Models.DeleteAuthorizedRole;

[ApiController]
[Route("api/engineering/v1/CostCenterAuthorizedRole")]
public class CostCenterAuthorizedRoleController : ControllerBase
{
    private readonly IAuthorizedRoleLogic _logic;

    public CostCenterAuthorizedRoleController(IAuthorizedRoleLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddAuthorizedRoles")]
    [ResponseSchema<CreateAuthorizedRolesResponse>]
    public async Task<IResult> AddAuthorizedRoles(
    [FromBody] CreateAuthorizedRolesRequest request, CT ct)
    {
        var result = await _logic.CreateAuthorizedRoles(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByCostCenterId")]
    [ResponseSchema<AuthorizedRoleGetsByCostCenterIdResponse>]
    public async Task<IResult> GetsByCostCenterId(
        [FromQuery] AuthorizedRoleGetsByCostCenterIdRequest request, CT ct)
    {
        var result = await _logic.AuthorizedRoleGetsByCostCenterId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteAuthorizedRole")]
    [ResponseSchema<DeleteAuthorizedRoleResponse>]
    public async Task<IResult> DeleteAuthorizedRole(
        [FromBody] DeleteAuthorizedRoleRequest request, CT ct)
    {
        var result = await _logic.DeleteAuthorizedRole(request, ct);
        return result.GetHttpResponse();
    }
}