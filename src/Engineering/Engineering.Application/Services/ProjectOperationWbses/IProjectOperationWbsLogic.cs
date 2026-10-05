using Engineering.Application.Services.ProjectOperationWbses.Contracts.CreateProjectOperationWbs;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.DeleteProjectOperationWbs;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetDetailPOWbsByProjectWbsId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetFltrPOWbs;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByPOId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByProjectWbsId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetProjectOperationWbsById;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.UpdateProjectOperationWbs;

namespace Engineering.Application.Services.ProjectOperationWbses;

public interface IProjectOperationWbsLogic
{
    Task<Result<CreateProjectOperationWbsResponse?>> CreateProjectOperationWbs(
        CreateProjectOperationWbsRequest request, CT ct);

    Task<Result<UpdateProjectOperationWbsResponse?>> UpdateProjectOperationWbs(
        UpdateProjectOperationWbsRequest request, CT ct);

    Task<Result<DeleteProjectOperationWbsResponse?>> DeleteProjectOperationWbs(
        DeleteProjectOperationWbsRequest request, CT ct);

    Task<Result<GetProjectOperationWbsByIdResponse?>> GetProjectOperationWbsById(
        GetProjectOperationWbsByIdRequest request, CT ct);

    Task<Result<GetFltrPOWbsResponse?>> GetFltrPOWbs(
        GetFltrPOWbsRequest request, CT ct);

    Task<Result<GetPOWbsByPOIdResponse?>> GetPOWbsByPOId(
        GetPOWbsByPOIdRequest request, CT ct);

    Task<Result<GetPOWbsByProjectWbsIdResponse?>> GetPOWbsByProjectWbsId(
        GetPOWbsByProjectWbsIdRequest request, CT ct);

    Task<Result<GetDetailPOWbsByProjectWbsIdResponse?>> GetDetailPOWbsByProjectWbsId(
        GetDetailPOWbsByProjectWbsIdRequest request, CT ct);
}
