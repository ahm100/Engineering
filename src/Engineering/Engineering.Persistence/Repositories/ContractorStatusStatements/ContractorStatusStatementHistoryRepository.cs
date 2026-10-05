using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementHistory;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Repositories.ContractorStatusStatements;

public class ContractorStatusStatementHistoryRepository : BaseRepository<EngineeringDBContext, ContractorStatusStatementHistory>, IContractorStatusStatementHistoryRepository
{
    public ContractorStatusStatementHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetsContractorStatusStatementHistoryModel>? Data, int RowCount)> GetsContractorStatusStatementHistory(
        long id,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet

            .Where(oo => oo.ContractorStatusStatement.Id == id)

            .Select(x => new GetsContractorStatusStatementHistoryModel()
            {
                Id = x.Id,
                Code = x.Code,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status,
                Description = x.Description,
                CreatorId = x.CreatorId,
                Created = x.Created,
            });

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

}
