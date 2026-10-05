namespace Engineering.Application.Services.ProjectCostCenterRequests.Contracts.RejectProjectCostCenterRequest;

public record RejectProjectCostCenterRequestRequest(long Id, string Reason) : IHttpRequest;
