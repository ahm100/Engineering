using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Repositories.ContractorStatusStatements;

public class ContractorStatusStatementDiscountRepository : BaseRepository<EngineeringDBContext, ContractorStatusStatementDiscount>, IContractorStatusStatementDiscountRepository
{
    public ContractorStatusStatementDiscountRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<ContractorStatusStatementDiscount> Data, int RowCount)> GetsContractorStatusStatementDiscountById(
        long contractorStatusStatementId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet

            .Where(x => x.ContractorStatusStatement.Id.Equals(contractorStatusStatementId));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);
        return (entities, count);
    }
}
