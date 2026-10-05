using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Persistence.Repositories.EmployerStatusStatements;

public class EmployerStatusStatementProjectOperationRepository : BaseRepository<EngineeringDBContext, EmployerStatusStatementProjectOperation>, IEmployerStatusStatementProjectOperationRepository
{
    public EmployerStatusStatementProjectOperationRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<EmployerStatusStatementProjectOperation> Data, int RowCount)> GetsEmployerStatusStatementProjectOperation(long employerStatusStatementId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(p => p.EmployerStatusStatementProjectOperationDocuments)
            .Include(p => p.ProjectOperation)
             .ThenInclude(o => o.OperationInfo)
            .Include(i => i.EmployerStatusStatement)
            .Include(i => i.EmployerStatusStatementProjectOperationDetails)

            .Where(oo => oo.EmployerStatusStatement.Id == employerStatusStatementId
            );

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var contracts = await query.ToListAsync(ct);

        return (contracts, count);
    }

    public async Task<(List<EmployerStatusStatementProjectOperation> Data, int RowCount)> GetsESSProjectOperationByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Include(p => p.EmployerStatusStatementProjectOperationDocuments)
            .Include(i => i.EmployerStatusStatement)
            .Include(i => i.EmployerStatusStatementProjectOperationDetails)
            .ThenInclude(i => i.EmployerStatusStatementProjectOperationDetailDailies)

            .Where(oo => ids.Contains(oo.Id));

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        var contracts = await query.ToListAsync(ct);

        return (contracts, count);
    }

}
