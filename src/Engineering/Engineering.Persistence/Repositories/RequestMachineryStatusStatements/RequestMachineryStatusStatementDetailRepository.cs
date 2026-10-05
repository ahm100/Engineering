using Engineering.Application.Abstractions.Data.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Persistence.Repositories.RequestMachineryStatusStatementDetails;

public class RequestMachineryStatusStatementDetailRepository : BaseRepository<EngineeringDBContext, RequestMachineryStatusStatementDetail>, IRequestMachineryStatusStatementDetailRepository
{
    public RequestMachineryStatusStatementDetailRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<RequestMachineryStatusStatementDetail?> GetRequestMachineryStatusStatementDetailById(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.Machinery)
            .Include(oo => oo.RequestMachineryStatusStatement)
            .Include(oo => oo.RequestMachinery)
            .Include(oo => oo.StatusStatementDetailProjectOperations)
            .Include(oo => oo.StatusStatementDetailProjectOperationDetails)
            .Where(oo => oo.Id.Equals(id));

        return await query.FirstOrDefaultAsync(ct);
    }
}
