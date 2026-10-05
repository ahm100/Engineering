using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardExpert = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardExpert;

namespace Engineering.Persistence.Repositories.OperationInfos.ConsumptionStandards;

public class ConsumptionStandardExpertRepository : BaseRepository<EngineeringDBContext, ConsumptionStandardExpert>, IConsumptionStandardExpertRepository
{
    public ConsumptionStandardExpertRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ConsumptionStandardExpert?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)
            .Where(oo => oo.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<(List<ConsumptionStandardExpert> Data, int RowCount)> ExpertGetByOprationInfoId(long oprationInfoId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.OperationInfo.Id == oprationInfoId);

        query = query.OrderBy(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var experts = await query
            .Page(pageIndex, pageSize)
            .ToListAsync(ct);

        return (experts, count);
    }

    public async Task<(List<ConsumptionStandardExpert> Data, int RowCount)> ExpertsByOprationInfoId(long oprationInfoId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.OperationInfo.Id == oprationInfoId);

        query = query.OrderBy(oo => oo.Created);
        var count = await query.CountAsync(ct);
        var experts = await query
            .ToListAsync(ct);

        return (experts, count);
    }

}