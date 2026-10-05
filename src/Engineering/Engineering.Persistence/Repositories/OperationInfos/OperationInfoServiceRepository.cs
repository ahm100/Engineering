using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceByProjectId;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Persistence.Repositories.OperationInfos;

public class OperationInfoServiceRepository : BaseRepository<EngineeringDBContext, OperationInfoService>, IOperationInfoServiceRepository
{
    public OperationInfoServiceRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<OperationInfoService> Data, int RowCount)> GetsByOprationInfoId(long oprationInfoId, string? filterData, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfo)
            .Include(x => x.ServiceInfo)

            .Where(oo => oo.OperationInfo.Id == oprationInfoId &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfo.ServiceInfoName, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfo.ServiceInfoCode, filterData.MakeLikePattern()))
            );

        query = query.OrderBy(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfoService> Data, int RowCount)> GetsByOperationInfoIdIncludeless(long oprationInfoId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.ServiceInfo)

            .Where(oo => oo.OperationInfo.Id == oprationInfoId);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<OperationInfoService?> GetOperationInfoServiceByIdForDelete(long oprationInfoId, long serviceInfoId, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperationDetailContractorServices)

            .Where(oo => oo.OperationInfo.Id == oprationInfoId && oo.ServiceInfo.Id == serviceInfoId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<bool> ValidateForContractorServices(long oprationInfoId, long serviceInfoId, CT ct)
    {
        var query = DbSet
            .Where(x => x.OperationInfo.Id == oprationInfoId && x.ServiceInfo.Id == serviceInfoId && x.ProjectOperationDetailContractorServices.Any());

        return await query.AnyAsync(ct);
    }

    public async Task<OperationInfoService?> GetOperationInfoServiceForValidation(long oprationInfoId, long serviceInfoId, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperationDetailContractorServices)
            .Include(x => x.ServiceInfo)
            .Include(x => x.OperationInfo)

            .Where(oo => oo.OperationInfo.Id == oprationInfoId && oo.ServiceInfo.Id == serviceInfoId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<(List<OperationInfoService> Data, int RowCount)> GetsOperationInfoServiceFiltered(long? categoryId, long? brnachId, long? seasonId, string? filterData, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet

            .Include(x => x.ServiceInfo)
            .Include(x => x.OperationInfo)
                .ThenInclude(x => x.OperationInfoSeasons)
                    .ThenInclude(x => x.Season)
                        .ThenInclude(x => x.Branch)
                            .ThenInclude(x => x.Category)

            .Where(oo => (companyId == null || oo.OperationInfo.CompanyId == companyId) &&
            (categoryId == null || oo.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Branch.Category.Id == categoryId)) ||
            (brnachId == null || oo.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Branch.Id == brnachId)) ||
            (seasonId == null || oo.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Id == seasonId)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfo.ServiceInfoName, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfo.ServiceInfoCode, filterData.MakeLikePattern())));

        query = query.OrderBy(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfoService> Data, int RowCount)> GetsOperationInfoServiceByIds(
        List<long> ids,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet

            .Where(o => ids.Contains(o.Id));

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<List<OperationInfoService>> GetsOperationInfoServiceByOIId(
        long id,
        List<long> serviceIds,
        CT ct)
    {
        return await DbSet
            .Include(x => x.ServiceInfo)
            .Where(o => serviceIds.Contains(o.ServiceInfo.Id) && o.OperationInfo.Id == id).ToListAsync(ct);

    }

    public async Task<(List<GetsOperationInfoServiceByProjectIdModel> Data, int RowCount)> GetsOperationInfoServiceByProjectId(
        long projectId,
        long? excludedServiceInfoId,
        string? serviceInfoFilters,
        string? operationInfoFilters,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet

            .Where(o =>
                o.OperationInfo.ProjectOperations.Any(x => x.Project.Id == projectId) &&
                (excludedServiceInfoId == null || o.ServiceInfo.Id != excludedServiceInfoId) &&
                (string.IsNullOrWhiteSpace(serviceInfoFilters) || EF.Functions.Like(o.ServiceInfo.ServiceInfoName, serviceInfoFilters.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(serviceInfoFilters) || EF.Functions.Like(o.ServiceInfo.ServiceInfoCode, serviceInfoFilters.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(operationInfoFilters) || EF.Functions.Like(o.OperationInfo.OperationInfoName, operationInfoFilters.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(operationInfoFilters) || EF.Functions.Like(o.OperationInfo.OperationInfoCode, operationInfoFilters.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(o.ServiceInfo.ServiceInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(o.ServiceInfo.ServiceInfoCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(o.OperationInfo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(o.OperationInfo.OperationInfoCode, filterData.MakeLikePattern()))
                )

            .Select(item => new GetsOperationInfoServiceByProjectIdModel()
            {
                Id = item.Id,
                ServiceInfoName = item.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = item.ServiceInfo.ServiceInfoCode,
                MeasurementId = item.ServiceInfo.UnitOfMeasurementId,
                OperationInfoId = item.OperationInfo.Id,
                OperationInfoCode = item.OperationInfo.OperationInfoCode,
                OperationInfoName = item.OperationInfo.OperationInfoName,
                OperationInfoMeasurementId = item.OperationInfo.UnitOfMeasurementId
            });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

}