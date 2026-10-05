using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails;

public partial class ProjectOperationDetailHistoryRepository : BaseRepository<EngineeringDBContext, ProjectOperationDetailHistory>, IProjectOperationDetailHistoryRepository
{
    public ProjectOperationDetailHistoryRepository(EngineeringDBContext context) : base(context) { }

    public async Task<(List<ProjectOperationDetailHistory> Data, int RowCount)> GetHistoryByProjectOperationDetailId(
        long? projectOperationDetailId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperationDetail.OperationLocation)
            .Include(x => x.ProjectOperationDetail.ProjectOperation.OperationInfo)
            .Where(x =>
                x.ProjectOperationDetail!.IsDeleted == false &&
                x.ProjectOperationDetail!.Id == projectOperationDetailId);

        query = query.OrderByDescending(x => x.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

}
