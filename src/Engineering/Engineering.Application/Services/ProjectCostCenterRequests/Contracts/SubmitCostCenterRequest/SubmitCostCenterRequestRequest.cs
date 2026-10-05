namespace Engineering.Application.Services.ProjectCostCenterRequests.Contracts.SubmitCostCenterRequest;

public record SubmitCostCenterRequestRequest(long ProjectId, string? Name, string Description) : IHttpRequest;
