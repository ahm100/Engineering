using Engineering.Application.Services.CostCenterAuthorizedUsers;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Models.AuthorizedUserGetsByCostCenterId;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Models.CreateAuthorizedUsers;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Models.DeleteAuthorizedUser;

[ApiController]
[Route("api/engineering/v1/CostCenterAuthorizedUser")]
public class CostCenterAuthorizedUserController : ControllerBase
{
    private readonly IAuthorizedUserLogic _logic;

    public CostCenterAuthorizedUserController(IAuthorizedUserLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddAuthorizedUsers")]
    [ResponseSchema<CreateAuthorizedUsersResponse>]
    public async Task<IResult> AddAuthorizedUsers(
    [FromBody] CreateAuthorizedUsersRequest request, CT ct)
    {
        var result = await _logic.CreateAuthorizedUsers(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByCostCenterId")]
    [ResponseSchema<AuthorizedUserGetsByCostCenterIdResponse>]
    public async Task<IResult> GetsByCostCenterId(
        [FromQuery] AuthorizedUserGetsByCostCenterIdRequest request, CT ct)
    {
        var result = await _logic.AuthorizedUserGetsByCostCenterId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteAuthorizedUser")]
    [ResponseSchema<DeleteAuthorizedUserResponse>]
    public async Task<IResult> DeleteAuthorizedUser(
        [FromBody] DeleteAuthorizedUserRequest request, CT ct)
    {
        var result = await _logic.DeleteAuthorizedUser(request, ct);
        return result.GetHttpResponse();
    }
}