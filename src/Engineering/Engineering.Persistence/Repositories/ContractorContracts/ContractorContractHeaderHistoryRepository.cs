using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Persistence.Repositories.ContractorContracts;

public class ContractorContractHeaderHistoryRepository : BaseRepository<EngineeringDBContext, ContractorContractHeaderHistory>, IContractorContractHeaderHistoryRepository
{
    public ContractorContractHeaderHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<ContractorContractHeaderHistory> Data, int RowCount)> GetsContractorContractHeaderHistory(long contractorContractId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Where(c => c.ContractorContractHeader.Id.Equals(contractorContractId));

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);

        return (entities, count);
    }
}
