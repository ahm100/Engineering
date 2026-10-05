using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Repositories.TransportationContractors;

public class TransportationContractorPriceWeightRepository : BaseRepository<EngineeringDBContext, TransportationContractorPriceWeight>, ITransportationContractorPriceWeightRepository
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public TransportationContractorPriceWeightRepository(EngineeringDBContext context) : base(context)
    {
    }


    public async Task<List<TransportationContractorPriceWeight>?> GetPriceWeightByContractor(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.PriceWeightHistories)
            .Where(oo => oo.TransportationContractorId == id);

        return await query.ToListAsync(ct);
    }

}