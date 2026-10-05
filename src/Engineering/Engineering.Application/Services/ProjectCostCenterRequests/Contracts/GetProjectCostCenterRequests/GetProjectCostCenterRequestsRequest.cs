using Engineering.Domain.Entities.Projects.Enums;
namespace Engineering.Application.Services.ProjectCostCenterRequests.Contracts.GetProjectCostCenterRequests;

public record GetProjectCostCenterRequestsRequest(
    long? ProjectId,
    ProjectCostCenterRequestStatus? Status,
    int PageIndex,
    int PageSize) : IHttpRequest;