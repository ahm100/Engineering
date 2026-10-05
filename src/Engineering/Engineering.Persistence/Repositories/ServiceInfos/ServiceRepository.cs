using Engineering.Application.Abstractions.Data.ServiceInfos;
using Engineering.Application.Services.ProjectServices.Models.GetsServiceInfoByProjectId;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Persistence.Repositories.ServiceInfos;

public class ServiceInfoRepository : BaseRepository<EngineeringDBContext, ServiceInfo>, IServiceInfoRepository
{
    public ServiceInfoRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ServiceInfo?> FindByName(string name, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.ServiceInfoName == name &&
            (companyId == null || oo.CompanyId == companyId)
            );

        return await query.FirstOrDefaultAsync(ct);
    }

    public Task<bool> FindServiceInfoByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct)
    {
        var query = DbSet
            .AnyAsync(oo =>
            (companyId == null || oo.CompanyId == companyId) &&
            (names.Contains(oo.ServiceInfoName) ||
            codes.Contains(oo.ServiceInfoCode)),
            ct);

        return query;
    }

    public async Task<ServiceInfo?> FindByCode(string code, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.ServiceInfoCode == code &&
            (companyId == null || oo.CompanyId == companyId)
            );

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ServiceInfo?> ValidateServiceByName(string name, long measurementId, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.ServiceInfoName == name &&
            oo.UnitOfMeasurementId == measurementId &&
             (companyId == null || oo.CompanyId == companyId)
             );

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ServiceInfo?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfoServices)
            .Include(x => x.ServiceInfoDocuments)

            .Where(oo => oo.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<string> CodeCreator(long? companyId, CT ct)
    {
        var query = await DbSet
            .Where(x => (companyId == null || x.CompanyId == companyId) &&
            EF.Functions.IsNumeric(x.ServiceInfoCode)).Select(x => Convert.ToInt64(x.ServiceInfoCode)).ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<(List<ServiceInfo> Data, int RowCount)> GetServiceInfos(List<long>? ids, long? operationInfoId, long? categoryId, long? branchId, long? seasonId, string? filterData, string? code, string? name, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet

            .Where(oo =>
                (companyId == null || oo.CompanyId == companyId) &&
                (code == null || EF.Functions.Like(oo.ServiceInfoCode, code.MakeLikePattern())) &&
                (name == null || EF.Functions.Like(oo.ServiceInfoName, name.MakeLikePattern())) &&
                (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfoCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfoName, filterData.MakeLikePattern())) &&
                (operationInfoId == null || oo.OperationInfoServices.Any(x => x.OperationInfo.Id == operationInfoId)) &&
                (categoryId == null || oo.OperationInfoServices.Any(x => x.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Branch.Category.Id == categoryId))) &&
                (branchId == null || oo.OperationInfoServices.Any(x => x.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Branch.Id == branchId))) &&
                (seasonId == null || oo.OperationInfoServices.Any(x => x.OperationInfo.OperationInfoSeasons.Any(x => x.Season.Id == seasonId))) &&
                (isActive == null || oo.IsActive == isActive));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ServiceInfo> Data, int RowCount)> GetsServiceInfoByOperationInfo(List<long>? operationInfoIds, string? filterData, bool? isActive, long? companyId, int pageIndex, int pageSize, CT ct)
    {
        if (operationInfoIds != null)
        {
            var query = DbSet
                .Include(oo => oo.OperationInfoServices)
                    .ThenInclude(oo => oo.OperationInfo)

                .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                    oo.OperationInfoServices.Any(x => operationInfoIds.Contains(x.OperationInfo.Id)) &&
                    (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfoCode, filterData.MakeLikePattern()) ||
                    string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfoName, filterData.MakeLikePattern())) &&
                    (isActive == null || oo.IsActive == isActive));

            query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
            var count = await query.CountAsync(ct);
            if (pageIndex > 0 || pageSize > 0)
                query = query.Page(pageIndex, pageSize);

            var items = await query.ToListAsync(ct);
            return (items, count);
        }
        else
        {
            var query = DbSet
                .Include(oo => oo.OperationInfoServices)
                    .ThenInclude(oo => oo.OperationInfo)

                .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                    oo.OperationInfoServices.Any() &&
                    (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfoCode, filterData.MakeLikePattern()) ||
                    string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfoName, filterData.MakeLikePattern())) &&
                    (isActive == null || oo.IsActive == isActive));

            query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
            var count = await query.CountAsync(ct);
            if (pageIndex > 0 || pageSize > 0)
                query = query.Page(pageIndex, pageSize);

            var items = await query.ToListAsync(ct);
            return (items, count);
        }
    }

    public async Task<(List<ServiceInfo> Data, int RowCount)> GetsServiceInfoByProjectOperationIds(List<long>? projectOperationIds, string? filterData, bool? isActive, long? companyId, int pageIndex, int pageSize, CT ct)
    {
        if (projectOperationIds != null)
        {
            var query = DbSet
                .Include(oo => oo.OperationInfoServices)
                    .ThenInclude(oo => oo.OperationInfo)
                        .ThenInclude(oo => oo.ProjectOperations)

                .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                    oo.OperationInfoServices.Any(x => x.OperationInfo.ProjectOperations.Any(p => projectOperationIds.Contains(p.Id))) &&
                    (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfoCode, filterData.MakeLikePattern()) ||
                    string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfoName, filterData.MakeLikePattern())) &&
                    (isActive == null || oo.IsActive == isActive));

            query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
            var count = await query.CountAsync(ct);
            if (pageIndex > 0 || pageSize > 0)
                query = query.Page(pageIndex, pageSize);

            var items = await query.ToListAsync(ct);
            return (items, count);
        }
        else
        {
            var query = DbSet
                .Include(oo => oo.OperationInfoServices)
                    .ThenInclude(oo => oo.OperationInfo)

                .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                    oo.OperationInfoServices.Any(x => x.OperationInfo.ProjectOperations.Any()) &&
                    (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfoCode, filterData.MakeLikePattern()) ||
                    string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfoName, filterData.MakeLikePattern())) &&
                    (isActive == null || oo.IsActive == isActive));

            query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
            var count = await query.CountAsync(ct);
            if (pageIndex > 0 || pageSize > 0)
                query = query.Page(pageIndex, pageSize);

            var items = await query.ToListAsync(ct);
            return (items, count);
        }
    }

    public async Task<(List<ServiceInfo> Data, int RowCount)> GetsServiceInfoByIds(List<long> ids, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<ServiceInfo> Data, int RowCount)> GetActiveServiceInfos(string? filterData, string? code, string? name, long? projectId, long? companyId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            (code == null || oo.ServiceInfoCode.Contains(code)) &&
            (name == null || oo.ServiceInfoName.Contains(name)) &&
            (projectId == null || oo.OperationInfoServices.Any(x => x.OperationInfo.ProjectOperations.Any(p => p.Project.Id == projectId))) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfoCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ServiceInfoName, filterData.MakeLikePattern())) &&

            oo.IsActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<GetsServiceInfoByProjectIdModel> Data, int RowCount)> GetsServiceInfoByProjectId(
        long projectId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet

            .Where(oo =>
               oo.OperationInfoServices.Any(x => x.OperationInfo.ProjectOperations.Any(z => z.Project.Id.Equals(projectId))) &&
               (string.IsNullOrWhiteSpace(filterData) ||
                EF.Functions.Like(oo.ServiceInfoName, filterData.MakeLikePattern()) ||
                EF.Functions.Like(oo.ServiceInfoCode, filterData.MakeLikePattern())))

            .Select(y => new GetsServiceInfoByProjectIdModel()
            {
                ServiceInfoCode = y.ServiceInfoCode,
                ServiceInfoId = y.Id,
                ServiceInfoMeasurId = y.UnitOfMeasurementId,
                ServiceInfoTitle = y.ServiceInfoName,
                IsActive = y.IsActive,
                Created = y.Created,
            });

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items.Distinct().ToList(), count);
    }

}