using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.DeleteProjectCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.GetProjectCostCenterRequestById;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.GetProjectCostCenterRequests;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.RejectProjectCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.SubmitCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Contracts.UpdateProjectCostCenterRequest;
using Engineering.Application.Services.ProjectCostCenterRequests.Models;
using Engineering.Application.Services.ProjectOperations.Models.GetsStatus;

namespace Engineering.Application.Services.ProjectCostCenterRequests;

public interface IProjectCostCenterRequestLogic
{
    Task<Result<SubmitCostCenterRequestResponse?>> SubmitCostCenterRequest(
        SubmitCostCenterRequestRequest request, CT ct);
    Task<Result<GetProjectCostCenterRequestsResponse?>> GetProjectCostCenterRequests(
        GetProjectCostCenterRequestsRequest request, CT ct);
    Task<Result<ProjectCostCenterRequestModel?>> GetProjectCostCenterRequestById(
        GetProjectCostCenterRequestByIdRequest request, CT ct);
    Task<Result<UpdateProjectCostCenterRequestResponse?>> UpdateProjectCostCenterRequest(
        UpdateProjectCostCenterRequestRequest request, CT ct);
    Task<Result<DeleteProjectCostCenterRequestResponse?>> DeleteProjectCostCenterRequest(
        DeleteProjectCostCenterRequestRequest request, CT ct);
    Task<Result<RejectProjectCostCenterRequestResponse?>> RejectProjectCostCenterRequest(
        RejectProjectCostCenterRequestRequest request, CT ct);
    Task<Result<GetsProjectStatusResponse?>> GetProjectCostCenterRequestStatuses(
        CT ct);
}