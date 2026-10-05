using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Repositories.RequestMachineries;

public class RequestMachineryProjectOperationDetailRepository : BaseRepository<EngineeringDBContext, RequestMachineryProjectOperationDetail>, IRequestMachineryProjectOperationDetailRepository
{
    public RequestMachineryProjectOperationDetailRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<RequestMachineryProjectOperationDetail> Data, int RowCount)> GetFilteredAsync(long projectOperationDetailId, string? filterData, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Include(oo => oo.ProjectOperationDetail)
                                .ThenInclude(oo => oo.OperationLocation)
                         .Include(oo => oo.ProjectOperationDetail)
                                .ThenInclude(oo => oo.ConsumableVolumeMachineries)
                                    .ThenInclude(oo => oo.Machinery)
                         .Include(oo => oo.RequestMachinery)
                            .ThenInclude(oo => oo.Machinery)
                                .ThenInclude(oo => oo!.MachineriesGroup)
                         .Include(oo => oo.RequestMachinery)
                            .ThenInclude(oo => oo.RequestMachineryAssignments)
                         .Where(oo => oo.ProjectOperationDetail.Id.Equals(projectOperationDetailId) &&
                                  oo.RequestMachinery.Status == Domain.Entities.RequestMachineries.Enums.RequestMachineryStatus.OnProject &&
                                  (filterData == null || string.IsNullOrWhiteSpace(filterData) ||
                                   EF.Functions.Like(oo.RequestMachinery.Machinery!.MachineryName, filterData.MakeLikePattern()) ||
                                   EF.Functions.Like(oo.RequestMachinery.Machinery!.MachineryCode, filterData.MakeLikePattern())));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestMachineryProjectOperationDetail> Data, int RowCount)> GetFilteredProjectOperationDetail(long RequestMachineryId, string? filterData, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Include(oo => oo.ProjectOperationDetail)
                            .ThenInclude(oo => oo.ProjectOperation)
                               .ThenInclude(oo => oo.OperationInfo)
                         .Include(oo => oo.ProjectOperationDetail)
                            .ThenInclude(oo => oo.OperationLocation)
                        .Where(oo =>
                                   (oo.RequestMachinery.Id == RequestMachineryId) &&
                                   (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ProjectOperationDetail.OperationLocation.PublicName, filterData.MakeLikePattern()) ||
                                    string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ProjectOperationDetail.OperationLocation.PublicCode, filterData.MakeLikePattern())));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestMachineryProjectOperationDetail> Data, int RowCount)> GetFilteredProjectOperationDetailByProjectOperation(long RequestMachineryId, long ProjectOperationId, string? filterData, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {

        var query = DbSet.Include(oo => oo.ProjectOperationDetail)
                            .ThenInclude(oo => oo.ProjectOperation)
                                 .ThenInclude(oo => oo.OperationInfo)
                         .Include(oo => oo.RequestMachinery)
                            .ThenInclude(oo => oo.ProjectOperations)
                         .Include(oo => oo.ProjectOperationDetail)
                            .ThenInclude(oo => oo.OperationLocation)
                         .Where(oo =>
                                     (oo.RequestMachinery.Id == RequestMachineryId) &&
                                      oo.ProjectOperationDetail.ProjectOperation.Id == ProjectOperationId &&
                                     (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ProjectOperationDetail.OperationLocation.PublicName, filterData.MakeLikePattern()) ||
                                      string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ProjectOperationDetail.OperationLocation.PublicCode, filterData.MakeLikePattern())));

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
