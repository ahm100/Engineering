namespace Engineering.Application.Services.Dashboard.Contracts.GetProjectDashboard;

public record GetProjectDashboardRequest(
    long ProjectId) : IHttpRequest;