using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetDependencyByPOId;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetFltrDependency;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPODependencies;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPORequirements;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Abstractions.Data.ProjectOperations;

public interface IProjectOperationDependencyRepository : IBaseRepository<ProjectOperationDependency>
{
    Task<ProjectOperationDependency?> GetById(
        long Id, CT ct);

    Task<(List<GetDependencyByPOIdModel>? Data, int RowCount)> GetDependencyByPOId(
        long projectOperationId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetFltrDependencyModel>? Data, int RowCount)> GetFltrDependency(
        List<long>? projectOperationIds,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct);

    Task<List<ProjectOperationDependency>?> GetByPOId(
        long POId, CT ct);

    Task<List<GetPODependenciesSuccessorModel>?> GetPOSuccessorFullRequirements(
    long projectOperationId,
    CT ct);

    Task<List<GetPODependenciesPredecessorModel>?> GetPOPredecessorFullRequirements(
    long projectOperationId,
    CT ct);

    Task<List<GetSuccessorsByPOIdModel>?> GetPOSuccessorRequirements(
    long projectOperationId,
    CT ct);

    Task<List<GetPredecessorByPOIdModel>?> GetPOPredecessorRequirements(
    long projectOperationId,
    CT ct);

    Task<bool> HaveDependency(
        long successorId, long predecessorId, CT ct);

    Task<List<long>> GetAffectedSuccessorIds(
    long operationId,
    CT ct);
}