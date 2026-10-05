using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.GetPOActionByPOId;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Persistence.Repositories.ProjectOperations;

public class ProjectOperationActionRepository : BaseRepository<EngineeringDBContext, ProjectOperationAction>, IProjectOperationActionRepository
{
    public ProjectOperationActionRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<GetPOActionByPOIdModel>> GetPOActionByPOId(
    long id,
    CancellationToken ct)
    {
        return await DbSet
            .Where(x => x.ProjectOperationId == id &&
            !x.IsDeleted)
            .Select(x => new GetPOActionByPOIdModel
            {
                Id = x.Id,
                OInfoActionId = x.OperationInfoActionId,
                Price = x.Price,
                ActionId = x.OperationInfoAction.ActionId,
                ActionName = x.OperationInfoAction.Action.ActionName,
                ActionCode = x.OperationInfoAction.Action.ActionCode
            })
            .ToListAsync(ct);
    }

    public async Task<List<ProjectOperationAction>> GetPOActionsByIds(
    List<long> ids,
    CancellationToken ct)
    {
        return await DbSet
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(ct);
    }
}