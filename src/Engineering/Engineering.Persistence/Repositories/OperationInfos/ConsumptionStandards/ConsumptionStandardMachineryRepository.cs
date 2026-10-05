using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardMachinery = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardMachinery;

namespace Engineering.Persistence.Repositories.OperationInfos.ConsumptionStandards;

public class ConsumptionStandardMachineryRepository : BaseRepository<EngineeringDBContext, ConsumptionStandardMachinery>, IConsumptionStandardMachineryRepository
{
    public ConsumptionStandardMachineryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ConsumptionStandardMachinery?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)
            .Where(oo => oo.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<(List<ConsumptionStandardMachinery> Data, int RowCount)> MachineryGetByOprationInfoId(long oprationInfoId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.OperationInfo.Id == oprationInfoId)
            .Include(x => x.Machinery);

        var count = await query.CountAsync(ct);
        var experts = await query
            .Page(pageIndex, pageSize)
            .ToListAsync(ct);

        return (experts, count);
    }

    public async Task<(List<ConsumptionStandardMachinery> Data, int RowCount)> MachineriesByOprationInfoId(long oprationInfoId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.OperationInfo.Id == oprationInfoId)
            .Include(x => x.Machinery);

        var count = await query.CountAsync(ct);
        var experts = await query
            .ToListAsync(ct);

        return (experts, count);
    }
}