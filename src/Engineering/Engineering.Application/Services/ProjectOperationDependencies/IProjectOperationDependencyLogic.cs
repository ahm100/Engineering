using Engineering.Application.Services.ProjectOperationDependencies.Contracts.Create;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.DeleteProjectOperationDependency;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetDependencyByPOId;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetFltrDependency;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPODependencies;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPORequirements;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.UpdateProjectOperationDependency;

namespace Engineering.Application.Services.ProjectOperationDependencies;

public interface IProjectOperationDependencyLogic
{
    Task<Result<CreateProjectOperationDependencyResponse?>> CreateProjectOperationDependency(
        CreateProjectOperationDependencyRequest request, CT ct);

    Task<Result<UpdateProjectOperationDependencyResponse?>> UpdateProjectOperationDependency(
        UpdateProjectOperationDependencyRequest request, CT ct);

    Task<Result<DeleteProjectOperationDependencyResponse?>> DeleteProjectOperationDependency(
        DeleteProjectOperationDependencyRequest request, CT ct);

    Task<Result<GetDependencyByPOIdResponse?>> GetDependencyByPOId(
        GetDependencyByPOIdRequest request, CT ct);

    Task<Result<GetFltrDependencyResponse?>> GetFltrDependency(
        GetFltrDependencyRequest request, CT ct);

    Task<Result<GetPORequirementsResponse?>> GetPORequirements(
        GetPORequirementsRequest request, CT ct);

    Task<Result<GetPODependenciesResponse?>> GetPODependencies(
        GetPODependenciesRequest request, CT ct);
}