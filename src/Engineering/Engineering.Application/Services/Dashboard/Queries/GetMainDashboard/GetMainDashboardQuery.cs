using Engineering.Application.Services.Dashboard.Contracts.GetMainDashboard;

namespace Engineering.Application.Services.Dashboard.Queries.GetMainDashboard;

public record GetMainDashboardQuery(
    ) : IQuery<GetMainDashboardResponse?>;