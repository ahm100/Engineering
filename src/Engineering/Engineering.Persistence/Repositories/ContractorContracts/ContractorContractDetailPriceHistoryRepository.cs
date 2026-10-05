using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorPriceHistory;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Persistence.Repositories.ContractorContracts;

public class ContractorContractDetailPriceHistoryRepository : BaseRepository<EngineeringDBContext, ContractorContractDetailPriceHistory>, IContractorContractDetailPriceHistoryRepository
{
    public ContractorContractDetailPriceHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetContractorPriceHistoryModel> Data, int RowCount)> GetContractorPriceHistory(
    long id,
    int pageIndex,
    int pageSize,
    CT ct)
    {
        var data = await DbSet
            .Where(c =>
                c.ContractorContractDetailPrice.Id == id &&
                !c.IsDeleted)
            .ToListAsync(ct);

        var query = data
            .GroupBy(item => new
            {
                item.StartDate,
                item.EndDate,
                item.Price,
                item.CurrencyId,
                item.IsActive,
                item.CreatorId
            })
            .Select(g => g
                .OrderByDescending(x => x.Created)
                .First())
            .Select(item => new GetContractorPriceHistoryModel()
            {
                Id = item.Id,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Price = item.Price,
                CurrencyId = item.CurrencyId,
                IsActive = item.IsActive,
                Created = item.Created,
                CreatorId = item.CreatorId,
            })
            .OrderByDescending(c => c.Created);

        var count = query.Count();

        IEnumerable<GetContractorPriceHistoryModel> models = [];
        if (pageIndex > 0 || pageSize > 0)
            models = query.Page(pageIndex, pageSize);

        var entities = models.ToList();

        return (entities, count);
    }


}
