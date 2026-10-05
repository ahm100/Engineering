using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfosByCodes;
using Engineering.Application.Services.ProjectOperations.Models.GetsFilteredForReports;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Persistence.Repositories.OperationInfos;

public partial class OperationInfoRepository : BaseRepository<EngineeringDBContext, OperationInfo>, IOperationInfoRepository
{
    public OperationInfoRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<OperationInfo?> GetOperationInfoByIdWithChild(long id, CT ct)
    {
        var query = DbSet
            .Include(i => i.ConsumptionStandardExperts)
            .Include(i => i.ConsumptionStandardProduct)
            .Include(i => i.ConsumptionStandardMachineries)
                .ThenInclude(x => x.Machinery)
            .Include(x => x.OperationInfoDependencies)
            .Include(x => x.OperationInfoServices)
                .ThenInclude(x => x.ServiceInfo)
            .Include(x => x.OperationInfoSeasons)
                .ThenInclude(x => x.Season)
                .ThenInclude(x => x.Branch)
                .ThenInclude(x => x.Category)
            .Include(x => x.OperationInfoGroupRelations)
                .ThenInclude(x => x.OperationInfoGroup)
            .Include(x => x.OperationInfoActions)
                .ThenInclude(x => x.Action)
            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> GetOperationInfoByIdIncludeLess(long id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Id == id)
            ;

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> GetOperationInfoByIdByConsumptionStandards(long id, CT ct)
    {
        var query = DbSet
            .Include(i => i.ConsumptionStandardExperts)
            .Include(i => i.ConsumptionStandardProduct)
            .Include(i => i.ConsumptionStandardMachineries)
                .ThenInclude(x => x.Machinery)

            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> GetOperationInfoByIdByExperts(long id, CT ct)
    {
        var query = DbSet
            .Include(i => i.ConsumptionStandardExperts)

            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> GetOperationInfoByIdByMachineries(long id, CT ct)
    {
        var query = DbSet
            .Include(i => i.ConsumptionStandardMachineries)
                .ThenInclude(x => x.Machinery)

            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> GetOperationInfoByIdByProducts(long id, CT ct)
    {
        var query = DbSet
            .Include(i => i.ConsumptionStandardProduct)

            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> GetOperationInfoByIdByGroupRelations(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfoGroupRelations)
                .ThenInclude(x => x.OperationInfoGroup)

            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> GetOperationInfoByIdByProjectOperations(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperations)
                .ThenInclude(x => x.Project)

            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<bool> GetOperationInfoProjectOperationsValidator(
        long projdctId,
        long operationInfoId,
        long? employerContractId,
        long unitOfMeasurementId, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet

            .Where(oo => oo.ProjectOperations.Any(x =>
                x.Project.Id.Equals(projdctId) &&
                x.OperationInfo.Id.Equals(operationInfoId) &&
                ///TODO Employers
                //(employerContractId == null || x.EmployerContract.Id == employerContractId) &&
                x.UnitOfMeasurementId.Equals(unitOfMeasurementId)));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var result = await query.AnyAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> GetByIdWithChild(long id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Id == id)
            .Include(i => i.ConsumptionStandardExperts)
            .Include(i => i.ConsumptionStandardProduct)
            .Include(i => i.ConsumptionStandardMachineries)
                .ThenInclude(x => x.Machinery)
            .Include(x => x.OperationInfoDependencies)
            .Include(x => x.OperationInfoServices)
                .ThenInclude(x => x.ServiceInfo)
            .Include(x => x.OperationInfoServices)
                .ThenInclude(x => x.ProjectOperationDetailContractorServices)
            .Include(x => x.OperationInfoSeasons)
                .ThenInclude(x => x.Season.Branch.Category)
            .Include(x => x.OperationInfoGroupRelations)
                .ThenInclude(x => x.OperationInfoGroup)
            .Include(x => x.ProjectOperations)
                .ThenInclude(x => x.Project);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> GetByIdWithDependencies(long id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Id == id)
            .Include(x => x.OperationInfoDependencies);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> GetOperationInfo(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfoServices)
                .ThenInclude(x => x.ServiceInfo)
            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> FindForDelete(long id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Id == id)
            .Include(x => x.OperationInfoDependencies)
            .Include(x => x.ProjectOperations)
                .ThenInclude(x => x.ProjectOperationDetails)
            //.Include(x => x.ConsiderationDependencies)
            .Include(x => x.OperationInfoServices)
                .ThenInclude(x => x.ServiceInfo);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> HaveOperationInfoChild(long id, CT ct)
    {
        var query = DbSet
            .Include(i => i.ConsumptionStandardExperts)
            .Include(i => i.ConsumptionStandardProduct)
            .Include(i => i.ConsumptionStandardMachineries)
                .ThenInclude(oo => oo.Machinery)
             .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> GetOperationInfoWithProjectOperationId(long id, CT ct)
    {
        var query = DbSet
            .Include(i => i.ConsumptionStandardExperts)
            .Include(i => i.ConsumptionStandardProduct)
            .Include(i => i.ConsumptionStandardMachineries)
                .ThenInclude(oo => oo.Machinery)
            .Include(i => i.OperationInfoServices)
                .ThenInclude(oo => oo.ServiceInfo)

             .Where(oo => oo.ProjectOperations.Any(x => x.Id.Equals(id)));

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> FindByName(string name, long? unitOfMeasurementId, long? companyId, CT ct)
    {
        var query = DbSet
            .Include(i => i.ConsumptionStandardExperts)
            .Include(i => i.ConsumptionStandardProduct)
            .Include(i => i.ConsumptionStandardMachineries)
                .ThenInclude(x => x.Machinery)
            .Include(x => x.OperationInfoSeasons)
                .ThenInclude(x => x.Season.Branch.Category)
            .Include(x => x.OperationInfoGroupRelations)
                .ThenInclude(x => x.OperationInfoGroup)

            .Where(oo =>
                (companyId == null || oo.CompanyId == companyId) &&
                (unitOfMeasurementId == null || oo.UnitOfMeasurementId == unitOfMeasurementId) &&
                oo.OperationInfoName == name);

        var result = await query
            .FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationInfo?> FindByCode(string code, long? companyId, CT ct)
    {
        var query = DbSet
            .Include(i => i.ConsumptionStandardExperts)
            .Include(i => i.ConsumptionStandardProduct)
            .Include(i => i.ConsumptionStandardMachineries)
                .ThenInclude(x => x.Machinery)
            .Include(x => x.OperationInfoSeasons)
                .ThenInclude(x => x.Season.Branch.Category)
            .Include(x => x.OperationInfoGroupRelations)
                .ThenInclude(x => x.OperationInfoGroup)

            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.OperationInfoCode == code);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<List<OperationInfo>?> GetByCodes(
        List<string> codes, CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperations)
                .ThenInclude(x => x.Project)
            .Include(x => x.ConsumptionStandardProduct)
            .Include(x => x.ConsumptionStandardExperts)
            .Include(x => x.ConsumptionStandardMachineries)
                .ThenInclude(x => x.Machinery)
            .Where(oo => codes.Contains(oo.OperationInfoCode));

        var result = await query.ToListAsync(ct);
        return result;
    }

    public async Task<string> CodeCreator(long? companyId, CT ct)
    {
        var query = await DbSet
            .Where(x => (companyId == null || x.CompanyId == companyId) &&
            EF.Functions.IsNumeric(x.OperationInfoCode)).Select(x => Convert.ToInt64(x.OperationInfoCode)).ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<(List<GetOperationInfosModel> Data, int RowCount)> GetFilteredOperationInfo(
        List<long>? ids,
        string? filterData,
        long? categoryId,
        long? branchId,
        long? seasonId,
        bool? isActive,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                    (categoryId == null || oo.OperationInfoSeasons.Any(x => x.Season.Branch.Category.Id == categoryId)) &&
                    (branchId == null || oo.OperationInfoSeasons.Any(x => x.Season.Branch.Id == branchId)) &&
                    (seasonId == null || oo.OperationInfoSeasons.Any(x => x.Season.Id == seasonId)) &&
                    (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                    (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoCode, filterData.MakeLikePattern()) ||
                     string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoName, filterData.MakeLikePattern()) ||
                     string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationLatinName!, filterData.MakeLikePattern())) &&
                    (isActive == null || oo.IsActive == isActive));

        query = query.OrderBy(a => !a.IsActive)
            .ThenByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenBy(a => a.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = query
            .Select(x => new GetOperationInfosModel
            {
                Id = x.Id,
                OperationInfoName = x.OperationInfoName,
                OperationInfoCode = x.OperationInfoCode,
                OperationLatinName = x.OperationLatinName,
                Priority = x.Priority,
                BasePrice = x.OperationInfoActions.Any(x => x.Price != 0)
                    ? x.OperationInfoActions.Sum(x => x.Price) ?? 0
                    : x.BasePrice,
                IsActive = x.IsActive,
                UnitOfMeasurementId = x.UnitOfMeasurementId,

                IsPriceList = x.IsPriceList,
                CompanyId = x.CompanyId,
                HaveStandard = x.HaveStandard,
                HaveExpertStandard = x.ConsumptionStandardExperts.Any(),
                HaveMachineryStandard = x.ConsumptionStandardMachineries.Any(),
                HaveProductStandard = x.ConsumptionStandardProduct.Any(),
                HaveServices = x.OperationInfoServices.Any(),
                HasChanged = x.HasChanged,
                Created = x.Created,
                Updated = x.Updated,
                CreatorId = x.CreatorId,
                UpdaterId = x.UpdaterId,

                GroupItems = x.OperationInfoGroupRelations.Select(o => o.OperationInfoGroup.OperationInfoGroupTitle).ToList(),
            });

        var items = await newQuery.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<OperationInfo> Data, int RowCount)> GetsByMultiFilter(string filterData, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfoSeasons)
                .ThenInclude(x => x.Season.Branch.Category)
            .Include(x => x.OperationInfoGroupRelations)
                .ThenInclude(x => x.OperationInfoGroup)
            .Include(x => x.ConsumptionStandardExperts)
            .Include(x => x.ConsumptionStandardProduct)
            .Include(x => x.ConsumptionStandardMachineries)
                .ThenInclude(x => x.Machinery)
            .Include(x => x.OperationInfoDependencies)
            .Include(x => x.OperationInfoServices)
                .ThenInclude(x => x.ServiceInfo)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoName, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationLatinName!, filterData.MakeLikePattern()))
            );

        query = query.OrderBy(a => !a.IsActive)
            .ThenByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<long> Data, int RowCount)> GetOperationInfoContractors(CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfoServices)
                .ThenInclude(x => x.ServiceInfo)
            .Include(x => x.OperationInfoServices)
                .ThenInclude(x => x.ProjectOperationDetailContractorServices)
            .Where(x => x.OperationInfoServices != null && x.OperationInfoServices.Count > 0)
            .SelectMany(x => x.OperationInfoServices)
            .Where(o => o.ProjectOperationDetailContractorServices != null && o.ProjectOperationDetailContractorServices.Count > 0)
            .SelectMany(o => o.ProjectOperationDetailContractorServices)
            .Where(c => c.ContractorId != null && c.ContractorId > 0)
            .Select(c => (long)c.ContractorId!);

        var count = await query.CountAsync(ct);
        var items = await query.Distinct().ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfo> Data, int RowCount)> GetsBySeasonId(long seasonId, string? filterData, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfoSeasons)
                .ThenInclude(x => x.Season.Branch.Category)

            .Where(oo => oo.OperationInfoSeasons.Any(x => x.Season.Id == seasonId) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoName, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationLatinName!, filterData.MakeLikePattern()))
                );

        query = query
            .OrderBy(a => !a.IsActive)
            .ThenByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfo> Data, int RowCount)> GetsOperationInfoByContractorIds(List<long>? contractorIds, string? filterData, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfoServices)
                .ThenInclude(x => x.ProjectOperationDetailContractorServices)
            .Include(x => x.OperationInfoServices)
                .ThenInclude(x => x.ServiceInfo)

            .Where(oo => (contractorIds == null ||
                         oo.OperationInfoServices.Where(x => x.ProjectOperationDetailContractorServices != null && x.ProjectOperationDetailContractorServices.Count > 0)
                                                .SelectMany(c => c.ProjectOperationDetailContractorServices)
                                                .Any(c => c.ContractorId.HasValue && contractorIds.Contains(c.ContractorId.Value))) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoName, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationLatinName!, filterData.MakeLikePattern()))
                );

        query = query.OrderBy(a => !a.IsActive)
                     .ThenByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfo> Data, int RowCount)> GetsByIds(List<long> ids, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.OperationInfoDependencies)
            .Include(oo => oo.OperationInfoServices)
            .ThenInclude(oo => oo.ServiceInfo)

            .Where(oo => ids.Contains(oo.Id));

        query = query.OrderBy(a => !a.IsActive)
            .ThenByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<List<OperationInfo>> GetOperationInfos(
        List<long> ids, CT ct)
    {
        return await DbSet
            .Where(oo => ids.Contains(oo.Id)).ToListAsync(ct);
    }

    public async Task<(List<OperationInfo> Data, int RowCount)> GetsOperationInfoByIdsForSeason(List<long> ids, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.OperationInfoSeasons)
            .ThenInclude(oo => oo.Season)

            .Where(oo => ids.Contains(oo.Id));

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfo> Data, int RowCount)> GetsOperationInfoByIdsForServiceInfo(List<long> ids, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.OperationInfoServices)
            .ThenInclude(oo => oo.ServiceInfo)

            .Where(oo => ids.Contains(oo.Id));

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<GetOperationInfosModel> Data, int RowCount)> GetOperationInfosModelByIds(List<long> ids,
    string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = BuildQueryGetOperationInfosModelByIds(ids);

        query = query.OrderBy(a => !a.IsActive)
            .ThenByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfo> Data, int RowCount)> GetsOperationInfoByIdsIncludeLess(List<long> ids, CT ct)
    {
        var query = DbSet

            .Include(x => x.ProjectOperations)
            .Include(x => x.ConsumptionStandardExperts)
            .Include(x => x.ConsumptionStandardProduct)
            .Include(x => x.ConsumptionStandardMachineries)
                .ThenInclude(x => x.Machinery)

            .Where(oo => ids.Contains(oo.Id));

        var count = await query.CountAsync(ct);
        var items = await query
            .ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfo> Data, int RowCount)> GetsConsiderationOperationInfos(List<long> ids, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Any(id => id == oo.Id));

        query = query.OrderBy(a => !a.IsActive)
            .ThenByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfo> Data, int RowCount)> GetsPrioritizeOperationInfo(string? filterData, long? id, int priority, long? companyId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Priority < priority &&
                (id == null || oo.Id != id) &&
                (companyId == null || oo.CompanyId == companyId) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationLatinName!, filterData.MakeLikePattern()))
            );

        query = query
            .OrderBy(a => !a.IsActive)
            .ThenByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfo> Data, int RowCount)> GetActiveOperationInfos(string? filterData, long? categoryId, long? branchId, long? seasonId, int? priority,
        long? companyId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Where(oo =>
            (companyId == null || oo.CompanyId == companyId) &&
            (categoryId == null || oo.OperationInfoSeasons.Any(x => x.Season.Branch.Category.Id == categoryId)) &&
            (branchId == null || oo.OperationInfoSeasons.Any(x => x.Season.Branch.Id == branchId)) &&
            (seasonId == null || oo.OperationInfoSeasons.Any(x => x.Season.Id == seasonId)) &&
        (priority == null || oo.Priority < priority && oo.Priority != null) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoName, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationLatinName!, filterData.MakeLikePattern())) &&

        oo.IsActive);

        query = query
            .OrderBy(a => !a.IsActive)
            .ThenByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationInfo> Data, int RowCount)> GetByCostCenterAsync(long costCenterId, long projectId, string? filterData, List<long>? operationLocationIds, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
                         .Where(oo => oo.ProjectOperations.Any(oo => oo.ProjectOperationDetails.Any(oo => (oo.Status == ProjectOperationDetailStatus.NotStarted || oo.Status == ProjectOperationDetailStatus.Doing))) &&
                                     oo.ProjectOperations.Any(c => c.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                                     oo.ProjectOperations.Any(c => c.Project.Id.Equals(projectId)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoName, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationLatinName!, filterData.MakeLikePattern())) &&
                                     (operationLocationIds == null ||
                                     oo.ProjectOperations.Any(c => c.ProjectOperationDetails.Any(y => operationLocationIds.Contains(y.OperationLocation.Id)))));

        query = query
            .Distinct()
            .OrderBy(a => !a.IsActive)
            .ThenByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);

        var count = await query.Distinct().CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);

        return (entities, count);
    }

    public async Task<(List<GetsFilteredForReportsModel> Data, int RowCount)> GetsFilteredForReports(
    List<long>? costCenterIds,
    List<long>? projectIds,
    string? filterData,
    int pageIndex,
    int pageSize,
    CT ct)
    {
        var query = DbSet
            .Where(oo =>
                (projectIds == null || oo.ProjectOperations.Any(x => projectIds.Contains(x.Project.Id))) &&
                (costCenterIds == null || oo.ProjectOperations.Any(x => x.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId)))) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoCode, filterData.MakeLikePattern())) &&
                oo.IsDeleted != true)
            .Select(x => new GetsFilteredForReportsModel()
            {
                Id = x.Id,
                OperationInfoName = x.OperationInfoName,
                OperationInfoCode = x.OperationInfoCode,
                OperationLatinName = x.OperationLatinName,
                UnitOfMeasurementId = x.UnitOfMeasurementId,
                Created = x.Created
            }).Distinct();

        query = query.OrderByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<List<OperationInfo>> GetActiveOperationInfoBySeasonIds(
        List<long>? categoryIds,
        List<long>? branchIds,
        List<long>? seasonIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(oo =>
            (seasonIds == null || oo.OperationInfoSeasons.Any(x => seasonIds.Contains(x.Season.Id))) &&
            (branchIds == null || oo.OperationInfoSeasons.Any(x => branchIds.Contains(x.Season.Branch.Id))) &&
            (categoryIds == null || oo.OperationInfoSeasons.Any(x => categoryIds.Contains(x.Season.Branch.Category.Id))) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoName, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationLatinName!, filterData.MakeLikePattern())) &&

        oo.IsActive);

        query = query
            .OrderBy(a => !a.IsActive)
            .ThenByDescending(a => a.Priority != null && a.Priority != 0)
            .ThenBy(a => a.Priority)
            .ThenByDescending(a => a.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items);
    }

    public async Task<List<OperationInfoBulkDto>> FindByCodes(List<string> codes, long? companyId, CT ct)
    {
        if (codes == null || !codes.Any())
            return new List<OperationInfoBulkDto>();

        return await DbSet
            .Where(o => codes.Contains(o.OperationInfoCode)
                     && !o.IsDeleted
                     && (companyId == null || o.CompanyId == companyId))
            .Select(o => new OperationInfoBulkDto(
                o.Id,
                o.OperationInfoCode,
                o.UnitOfMeasurementId))
            .ToListAsync(ct);
    }

    public async Task AddRangeAsync(IEnumerable<OperationInfo> operationInfos, CT ct)
    {
        await DbSet.AddRangeAsync(operationInfos, ct);
    }

}