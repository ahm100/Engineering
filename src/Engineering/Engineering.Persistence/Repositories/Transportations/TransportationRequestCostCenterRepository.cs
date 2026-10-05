using Engineering.Application.Abstractions.Data.Transportations;
using TransportationRequestCostCenter = Engineering.Domain.Entities.Transportations.TransportationRequestCostCenter;

namespace Engineering.Persistence.Repositories.Transportations;

public class TransportationRequestCostCenterRepository : BaseRepository<EngineeringDBContext, TransportationRequestCostCenter>, ITransportationRequestCostCenterRepository
{
    public TransportationRequestCostCenterRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<long>> GetByRequestId(long requestId,
        CT ct)
    {
        return await DbSet.
            Where(x => x.TransportationRequestId == requestId)
            .Select(x => x.CostCenterId).ToListAsync(ct);
    }

    public async Task<List<long>> GetByRequestIds(List<long> requestIds,
        CT ct)
    {
        return await DbSet.
            Where(x => requestIds.Contains(x.TransportationRequestId))
            .Select(x => x.CostCenterId).ToListAsync(ct);
    }
}