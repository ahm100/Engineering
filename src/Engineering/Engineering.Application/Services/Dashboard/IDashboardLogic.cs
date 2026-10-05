using Engineering.Application.Services.Dashboard.Contracts.GetMainDashboard;
using Engineering.Application.Services.Dashboard.Contracts.GetProjectDashboard;

namespace Engineering.Application.Services.Dashboard;

public interface IDashboardLogic
{
    Task<Result<GetMainDashboardResponse?>> GetMainDashboard(
        GetMainDashboardRequest request, CT ct);

    Task<Result<GetProjectDashboardResponse?>> GetProjectDashboard(
        GetProjectDashboardRequest request, CT ct);
}
