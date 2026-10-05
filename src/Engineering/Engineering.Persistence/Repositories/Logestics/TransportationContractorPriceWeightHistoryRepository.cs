using Engineering.Application.Services.TransportationContractors.Contracts.GetsPriceWeightHistory;
using Engineering.Domain.Entities.Logistics;

namespace Engineering.Persistence.Repositories.TransportationContractors;

public class TransportationContractorPriceWeightHistoryRepository : BaseRepository<
    EngineeringDBContext, TransportationContractorPriceWeightHistory>, ITransportationContractorPriceWeightHistoryRepository
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public TransportationContractorPriceWeightHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }
    public async Task<(List<GetsPriceWeightHistoryResponseModel> Data, int RowCount)> GetsPriceWeightHistory(
    long id,
    int pageIndex,
    int pageSize,
    CT ct)
    {
        var query = DbSet
            .Where(x => x.TransportationContractorPriceWeight.Id == id)
            .Select(z => new GetsPriceWeightHistoryResponseModel
            {
                Id = z.Id,
                CreatorId = z.CreatorId,
                IsFixed = z.IsFixed,
                Created = z.Created,
                Price = z.Price,
                PriceWeightId = z.TransportationContractorPriceWeightId,
                UntilWeight = z.UntilWeight,
            });

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

}