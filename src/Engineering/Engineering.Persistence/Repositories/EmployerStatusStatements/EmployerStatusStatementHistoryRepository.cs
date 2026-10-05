using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Persistence.Repositories.EmployerStatusStatements;

public class EmployerStatusStatementHistoryRepository : BaseRepository<EngineeringDBContext, EmployerStatusStatementHistory>, IEmployerStatusStatementHistoryRepository
{
    public EmployerStatusStatementHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<EmployerStatusStatementHistory> Data, int RowCount)> GetsEmployerStatusStatementHistory(
        long id,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(oo =>
                oo.EmployerStatusStatement.Id == id);

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var contracts = await query.ToListAsync(ct);

        return (contracts, count);
    }

}
