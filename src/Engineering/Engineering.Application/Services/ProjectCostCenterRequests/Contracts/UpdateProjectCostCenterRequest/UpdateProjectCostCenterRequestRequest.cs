namespace Engineering.Application.Services.ProjectCostCenterRequests.Contracts.UpdateProjectCostCenterRequest;

public record UpdateProjectCostCenterRequestRequest(long Id, string? Name, string Description) : IHttpRequest;
