using Engineering.Application.Services.ProjectOperations.Models.GetPOActionByPOId;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Abstractions.Data.ProjectOperations;

public interface IProjectOperationActionRepository : IBaseRepository<ProjectOperationAction>
{
    Task<List<GetPOActionByPOIdModel>> GetPOActionByPOId(
    long id,
    CancellationToken ct);
    Task<List<ProjectOperationAction>> GetPOActionsByIds(
    List<long> ids,
    CancellationToken ct);
}