using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Persistence.Repositories.ContractorContracts;

public class ContractorContractHistoryRepository : BaseRepository<EngineeringDBContext, ContractorContractHistory>, IContractorContractHistoryRepository
{
    public ContractorContractHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<ContractorContractHistory> Data, int RowCount)> GetFilteredContractorContractHistory(long contractorContractId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Where(c => c.ContractorContract.Id.Equals(contractorContractId));

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);

        return (entities, count);
    }
}
