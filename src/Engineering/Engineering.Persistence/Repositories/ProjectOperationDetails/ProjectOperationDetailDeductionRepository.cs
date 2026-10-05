using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails;

public class ProjectOperationDetailDeductionRepository : BaseRepository<EngineeringDBContext, ProjectOperationDetailDeduction>, IProjectOperationDetailDeductionRepository
{
    public ProjectOperationDetailDeductionRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ProjectOperationDetailDeduction?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(c => c.ProjectOperationDetail)
                .ThenInclude(c => c.ProjectOperationDetailDeductions)
            .Include(c => c.ProjectOperationDetail)
                .ThenInclude(c => c.OperationLocation)
            .Where(c => c.Id.Equals(id) && !c.IsDeleted);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<ProjectOperationDetailDeduction?> GetForDelete(long id, CT ct)
    {
        var query = DbSet
            .Include(c => c.ProjectOperationDetail)
            .Where(c => c.Id.Equals(id) && !c.IsDeleted);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<(List<ProjectOperationDetailDeduction> Data, int RowCount)> GetsByProjectOperationDetailId(long projectOperationDetailId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.ProjectOperationDetail)
                .ThenInclude(c => c.OperationLocation)

            .Where(oo => oo.ProjectOperationDetail.Id.Equals(projectOperationDetailId) &&
              !oo.ProjectOperationDetail.IsDeleted &&
              !oo.IsDeleted
            );

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<ProjectOperationDetailDeduction>> GetsProjectOperationDetailDeductionByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.ProjectOperationDetail)
            .Where(oo => ids.Contains(oo.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }


    public async Task<List<decimal>?> GetDeductionAmountsByDetailId(long projectOperationDetailId, CT ct)
    {
        var query = DbSet
           .Where(oo => oo.ProjectOperationDetail.Id.Equals(projectOperationDetailId))
           .Select(oo => oo.FinalAmount);

        var items = await query.ToListAsync(ct);
        return items;
    }

}
