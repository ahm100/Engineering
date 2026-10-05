using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Persistence.Repositories.RequestRewards;

public class RequestRewardDocumentRepository : BaseRepository<EngineeringDBContext, RequestRewardDocument>, IRequestRewardDocumentRepository
{
    public RequestRewardDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }
    public async Task<(List<RequestRewardDocument> Data, int RowCount)> GetsRequestRewardDocumentsByIds(List<long> documentIds, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestReward)
            .Where(oo => documentIds.Contains(oo.Id));

        var count = await query.CountAsync(ct);
        var Documents = await query.ToListAsync(ct);

        return (Documents, count);
    }
}
