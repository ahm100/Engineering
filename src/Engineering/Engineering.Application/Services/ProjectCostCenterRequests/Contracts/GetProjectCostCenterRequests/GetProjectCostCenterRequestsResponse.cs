using Engineering.Application.Services.ProjectCostCenterRequests.Models;

namespace Engineering.Application.Services.ProjectCostCenterRequests.Contracts.GetProjectCostCenterRequests;

public record GetProjectCostCenterRequestsResponse(
    List<ProjectCostCenterRequestModel> Data,
    int RowCount);