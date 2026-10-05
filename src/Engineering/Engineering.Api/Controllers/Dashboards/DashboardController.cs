using Engineering.Application.Services.Dashboard;
using Engineering.Application.Services.Dashboard.Contracts.GetMainDashboard;
using Engineering.Application.Services.Dashboard.Contracts.GetProjectDashboard;

namespace Engineering.Api.Controllers.Dashboards;

[ApiController]
[Route("api/engineering/v1/Dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardLogic _logic;

    public DashboardController(IDashboardLogic logic)
    {
        _logic = logic;
    }

    [HttpGet("GetMainDashboard")]
    [ResponseSchema<GetMainDashboardResponse>]
    public async Task<IResult> GetMainDashboard([FromQuery] GetMainDashboardRequest request, CT ct)
    {
        var result = await _logic.GetMainDashboard(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetProjectDashboard")]
    [ResponseSchema<GetProjectDashboardResponse>]
    public async Task<IResult> GetProjectDashboard([FromQuery] GetProjectDashboardRequest request, CT ct)
    {
        var result = await _logic.GetProjectDashboard(request, ct);
        return result.GetHttpResponse();
    }
}