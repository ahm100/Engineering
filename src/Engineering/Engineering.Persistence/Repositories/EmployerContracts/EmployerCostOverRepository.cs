using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerCostOverRepository : BaseRepository<EngineeringDBContext, EmployerCostOver>, IEmployerCostOverRepository
{
    public EmployerCostOverRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<EmployerCostOver?> FindByIdWithCostOver(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.CostOver)
            .Include(oo => oo.ChildCostOverImpacts)
                .ThenInclude(oo => oo.ChildCostOver.CostOver)
            .Include(x => x.EmployerContract)
            .Where(oo => oo.Id == id && !oo.CostOver.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<EmployerCostOver?> HaveContractCostOverChild(long ContractCostOverId, CT ct)
    {
        var query = DbSet
            .Include(x => x.CostOver)
            .Include(oo => oo.ChildCostOverImpacts)
                .ThenInclude(oo => oo.ChildCostOver.CostOver)
             .Where(oo => oo.Id == ContractCostOverId);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<EmployerCostOver> Data, int RowCount)> GetContractCostOvers(long? costOverId, long? employerId, string[]? orderBy, long? companyId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.CostOver)
            .Include(x => x.EmployerContract)
            .Include(oo => oo.ChildCostOverImpacts)
                .ThenInclude(oo => oo.ChildCostOver.CostOver).AsQueryable();
        query = query.Where(oo => (costOverId == null || oo.CostOver.Id == costOverId) &&
        (employerId == null || oo.EmployerContract.Id == employerId) &&
        (companyId == null || oo.CostOver.CompanyId == companyId) &&
        !oo.CostOver.IsDeleted)
        .OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var ContractCostOvers = await query.ToListAsync(ct);

        return (ContractCostOvers, count);
    }

    public async Task<(List<EmployerCostOver> Data, int RowCount)> GetByCostOverId(long CostOverId, int pageIndex, int pageSize, CT ct)
    {

        var query = DbSet
            .Include(oo => oo.ChildCostOverImpacts)
                .ThenInclude(oo => oo.ChildCostOver.CostOver)

             .Where(oo => oo.CostOver.Id == CostOverId);

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var ContractCostOvers = await query.ToListAsync(ct);

        return (ContractCostOvers, count);
    }
}