using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Persistence.Repositories.EmployerStatusStatements;

public class EmployerStatusStatementProjectOperationDetailRepository : BaseRepository<EngineeringDBContext, EmployerStatusStatementProjectOperationDetail>, IEmployerStatusStatementProjectOperationDetailRepository
{
    public EmployerStatusStatementProjectOperationDetailRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<EmployerStatusStatementProjectOperationDetail> Data, int RowCount)> GetsEmployerStatusStatementProjectOperationDetail(
        long employerStatusStatementId,
        long? employerStatusStatementProjectOperationId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.EmployerStatusStatementProjectOperation)
                .ThenInclude(p => p.EmployerStatusStatement)
            .Include(p => p.ProjectOperationDetail)
                .ThenInclude(p => p.ProjectOperation)
            .Include(p => p.ProjectOperationDetail)
                .ThenInclude(p => p.OperationLocation)

            .Where(oo => oo.EmployerStatusStatementProjectOperation.EmployerStatusStatement.Id == employerStatusStatementId &&
                (employerStatusStatementProjectOperationId == null || oo.EmployerStatusStatementProjectOperation.Id == employerStatusStatementProjectOperationId)
            );

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var contracts = await query.ToListAsync(ct);

        return (contracts, count);
    }

}
