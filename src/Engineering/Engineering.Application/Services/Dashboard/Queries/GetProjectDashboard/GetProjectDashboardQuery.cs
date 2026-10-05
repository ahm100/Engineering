using Engineering.Application.Services.Dashboard.Contracts.GetProjectDashboard;

namespace Engineering.Application.Services.Dashboard.Queries.GetProjectDashboard;

public record GetProjectDashboardQuery(
    long ProjectId) : IQuery<GetProjectDashboardResponse?>;