using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Persistence.Repositories.EmployerStatusStatements;

public class EmployerStatusStatementProjectOperationDetailDailyRepository : BaseRepository<EngineeringDBContext, EmployerStatusStatementProjectOperationDetailDaily>, IEmployerStatusStatementProjectOperationDetailDailyRepository
{
    public EmployerStatusStatementProjectOperationDetailDailyRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<EmployerStatusStatementProjectOperationDetailDaily> Data, int RowCount)> GetsEmployerStatusStatementProjectOperationDetailDaily(
        long employerStatusStatementId,
        long? employerStatusStatementProjectOperationId,
        long? employerStatusStatementProjectOperationDetailId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.EmployerStatusStatementDailyDocuments)
            .Include(i => i.EmployerStatusStatementProjectOperationDetail)
                .ThenInclude(p => p.EmployerStatusStatementProjectOperation)
            .Include(p => p.DailyProjectOperation)
                .ThenInclude(p => p.ProjectOperationDetail)
                    .ThenInclude(p => p.ProjectOperation)
            .Include(p => p.DailyProjectOperation)
                .ThenInclude(p => p.ProjectOperationDetail)
                    .ThenInclude(p => p.OperationLocation)

            .Where(oo => oo.EmployerStatusStatementProjectOperationDetail.EmployerStatusStatementProjectOperation.EmployerStatusStatement.Id == employerStatusStatementId &&
                (employerStatusStatementProjectOperationId == null || oo.EmployerStatusStatementProjectOperationDetail.EmployerStatusStatementProjectOperation.Id == employerStatusStatementProjectOperationId) &&
                (employerStatusStatementProjectOperationDetailId == null || oo.EmployerStatusStatementProjectOperationDetail.Id == employerStatusStatementProjectOperationDetailId)
            );

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var contracts = await query.ToListAsync(ct);

        return (contracts, count);
    }

    public async Task<(List<EmployerStatusStatementProjectOperationDetailDaily> Data, int RowCount)> GetsESSProjectOperationDetailDailyByIds(
        List<long> ids,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.EmployerStatusStatementDailyDocuments)
            .Include(i => i.EmployerStatusStatementProjectOperationDetail.EmployerStatusStatementProjectOperation.EmployerStatusStatement)

            .Where(x => ids.Contains(x.Id));

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        var contracts = await query.ToListAsync(ct);

        return (contracts, count);
    }

}
