using Engineering.Application.Abstractions.Data.Transportations;
using TransportationRequestProject = Engineering.Domain.Entities.Transportations.TransportationRequestProject;

namespace Engineering.Persistence.Repositories.Transportations;

public class TransportationRequestProjectRepository : BaseRepository<EngineeringDBContext, TransportationRequestProject>, ITransportationRequestProjectRepository
{
    public TransportationRequestProjectRepository(EngineeringDBContext context) : base(context)
    {
    }
    public async Task<List<long>> GetByRequestId(long requestId,
        CT ct)
    {
        return await DbSet.
            Where(x => x.TransportationRequestId == requestId)
            .Select(x => x.ProjectId).ToListAsync(ct);
    }

    public async Task<List<long>> GetByRequestIds(List<long> requestIds,
        CT ct)
    {
        return await DbSet.
            Where(x => requestIds.Contains(x.TransportationRequestId))
            .Select(x => x.ProjectId).ToListAsync(ct);
    }
}