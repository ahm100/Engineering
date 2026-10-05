using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using ProjectOperationTemporaryDaily = Engineering.Domain.Entities.ProjectOperations.ProjectOperationTemporaryDaily;

namespace Engineering.Persistence.Repositories.ProjectOperations;

public class ProjectOperationTemporaryDailyRepository : BaseRepository<EngineeringDBContext, ProjectOperationTemporaryDaily>, IProjectOperationTemporaryDailyRepository
{
    public ProjectOperationTemporaryDailyRepository(EngineeringDBContext context) : base(context)
    {
    }


    public async Task<ProjectOperationTemporaryDaily?> GetById(long id, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.CostCenter)
            .Include(oo => oo.Project)
            .Include(oo => oo.ProjectOperationTemporaryDailyDocuments)
            .Include(oo => oo.ProjectOperation)
                .ThenInclude(x => x.OperationInfo)
            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<ProjectOperationTemporaryDaily> Data, int RowCount)> GetProjectOperationTemporaryDailies(
        long? costCenterId,
        long? projectId,
        long? projectOperationId,
        DateTime? startDate,
        DateTime? endTime,
        TemporaryDailyStatus? TemporaryDailyStatus,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.CostCenter)
            .Include(oo => oo.Project)
            .Include(oo => oo.ProjectOperationTemporaryDailyDocuments)
            .Include(oo => oo.ProjectOperation)
                .ThenInclude(x => x.OperationInfo)
            .Where(oo =>
            (costCenterId == null || oo.CostCenter.Id == costCenterId) &&
            (projectId == null || oo.Project.Id == projectId) &&
            (projectOperationId == null || oo.ProjectOperation.Id == projectOperationId) &&
            (TemporaryDailyStatus == null || oo.Status == TemporaryDailyStatus) &&
            (startDate == null || oo.StartDate.Date >= startDate.Value.Date) &&
            (endTime == null || oo.EndDate.Date <= endTime.Value.Date) &&
            (TemporaryDailyStatus == null || oo.Status == TemporaryDailyStatus) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Description, filterData.MakeLikePattern())) &&
            oo.IsDeleted == false);

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
    public async Task<(List<ProjectOperationTemporaryDaily> Data, int RowCount)> GetCurrentUserTemporaryDailies(
        long creatorId,
        long? costCenterId,
        long? projectId,
        long? projectOperationId,
        DateTime? startDate,
        DateTime? endTime,
        TemporaryDailyStatus? TemporaryDailyStatus,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.CostCenter)
            .Include(oo => oo.Project)
            .Include(oo => oo.ProjectOperationTemporaryDailyDocuments)
            .Include(oo => oo.ProjectOperation)
                .ThenInclude(x => x.OperationInfo)
            .Where(oo =>
            (costCenterId == null || oo.CostCenter.Id == costCenterId) &&
            (projectId == null || oo.Project.Id == projectId) &&
            (projectOperationId == null || oo.ProjectOperation.Id == projectOperationId) &&
            (TemporaryDailyStatus == null || oo.Status == TemporaryDailyStatus) &&
            (startDate == null || oo.StartDate.Date >= startDate.Value.Date) &&
            (endTime == null || oo.EndDate.Date <= endTime.Value.Date) &&
            (TemporaryDailyStatus == null || oo.Status == TemporaryDailyStatus) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.Description, filterData.MakeLikePattern())) &&
            oo.CreatorId == creatorId &&
            oo.IsDeleted == false);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

}