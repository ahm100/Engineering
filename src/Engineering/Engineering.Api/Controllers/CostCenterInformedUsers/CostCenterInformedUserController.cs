using Engineering.Application.Services.CostCenterInformedUsers;
using Engineering.Application.Services.CostCenterInformedUsers.Models.CreateInformedUsers;
using Engineering.Application.Services.CostCenterInformedUsers.Models.DeleteInformedUser;
using Engineering.Application.Services.CostCenterInformedUsers.Models.InformedUserGetsByCostCenterId;

[ApiController]
[Route("api/engineering/v1/CostCenterInformedUser")]
public class CostCenterInformedUserController : ControllerBase
{
    private readonly IInformedUserLogic _logic;

    public CostCenterInformedUserController(IInformedUserLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddInformedUsers")]
    [ResponseSchema<CreateInformedUsersResponse>]
    public async Task<IResult> AddInformedUsers(
    [FromBody] CreateInformedUsersRequest request,
    CT ct)
    {
        var result = await _logic.CreateInformedUsers(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByCostCenterId")]
    [ResponseSchema<InformedUserGetsByCostCenterIdResponse>]
    public async Task<IResult> GetsByCostCenterId(
        [FromQuery] InformedUserGetsByCostCenterIdRequest request,
        CT ct)
    {
        var result = await _logic.InformedUserGetsByCostCenterId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteInformedUser")]
    [ResponseSchema<DeleteInformedUserResponse>]
    public async Task<IResult> DeleteInformedUser(
        [FromBody] DeleteInformedUserRequest request,
        CT ct)
    {
        var result = await _logic.DeleteInformedUser(request, ct);
        return result.GetHttpResponse();
    }
}