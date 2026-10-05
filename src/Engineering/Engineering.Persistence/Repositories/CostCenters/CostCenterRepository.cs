using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.CostCenters.Models.CostCenterModels;
using Engineering.Application.Services.CostCenters.Models.GetCompaniesWork;
using Engineering.Application.Services.CostCenters.Models.GetsActiveMainWarehouseCostCenter;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Persistence.Repositories.CostCenters;

public partial class CostCenterRepository : BaseRepository<EngineeringDBContext, CostCenter>, ICostCenterRepository
{
    public CostCenterRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<CostCenter?> GetCostCenter(
        long id,
        CT ct)
    {
        var query = DbSet

            .Include(x => x.Projects)

            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<CostCenter?> FindByIdAndChild(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.Projects)
            .Include(i => i.CostCenterType)
            .Include(i => i.CostCenterWarehouses)
            .Include(i => i.InformedUsers)
            .Include(i => i.CostCenterAuthorizedRoles)
            .Include(i => i.CostCenterAuthorizedUsers)
            .Include(i => i.CostCenterHistories)
            .Where(oo => oo.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<CostCenter?> GetCostCenterWithoutInclude(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.CostCenterAuthorizedRoles)
            .Include(i => i.CostCenterAuthorizedUsers)
            .Where(oo => oo.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<CostCenter?> GetCostCenterWithWarehousesInclude(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.CostCenterWarehouses)
            .Where(oo => oo.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<CostCenter?> GetCostCenterWithRolesInclude(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.CostCenterAuthorizedRoles)
            .Where(oo => oo.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<CostCenter?> GetCostCenterWithUsersInclude(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.CostCenterAuthorizedUsers)
            .Where(oo => oo.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<CostCenter?> GetCostCenterWithRolesAndUsersInclude(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.CostCenterAuthorizedRoles)
            .Include(i => i.CostCenterAuthorizedUsers)

            .Where(oo => oo.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<CostCenter?> GetCostCenterWithInformedUsersInclude(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.InformedUsers)

            .Where(oo => oo.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<CostCenter?> GetForDelete(
        long id,
        CT ct)
    {
        var query = DbSet

            .Include(x => x.Projects)

            .Where(oo => oo.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<CostCenter?> FindByName(
        string costCenterName,
        long? companyId,
        CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.CostCenterName == costCenterName)
            .Include(i => i.CostCenterType)
            .Include(i => i.CostCenterWarehouses)
            .Include(i => i.InformedUsers)
            .Include(i => i.CostCenterAuthorizedRoles)
            .Include(i => i.CostCenterAuthorizedUsers);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<CostCenter?> FindByCode(
        string costCenterCode,
        long? companyId,
        CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.CostCenterCode == costCenterCode)
            .Include(i => i.CostCenterType)
            .Include(i => i.CostCenterWarehouses)
            .Include(i => i.InformedUsers)
            .Include(i => i.CostCenterAuthorizedRoles)
            .Include(i => i.CostCenterAuthorizedUsers);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<string> CodeCreator(
        long? companyId,
        CT ct)
    {
        var query = await DbSet
            .Where(x => (companyId == null || x.CompanyId == companyId) &&
            EF.Functions.IsNumeric(x.CostCenterCode)).Select(x => Convert.ToInt64(x.CostCenterCode)).ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetActiveCostCenters(
        string? filterData,
        string? name,
        string? code,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            (code == null || oo.CostCenterCode.Contains(code)) &&
            (name == null || oo.CostCenterName.Contains(name)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterName, filterData.MakeLikePattern())) &&
            oo.IsActive);

        query = query.OrderByDescending(x => x.IsDefault).OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<GetsActiveMainWarehouseCostCenterModel> Data, int RowCount)> GetsActiveMainWarehouseCostCenter(
        string? filterData,
        List<long>? warehouseIds,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(oo =>
            oo.IsActive &&
            (warehouseIds == null || oo.CostCenterWarehouses.Any(x => warehouseIds.Contains(x.WarehouseId))) &&
            (companyId == null || oo.CompanyId == companyId) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterName, filterData.MakeLikePattern())))

            .Select(item => new GetsActiveMainWarehouseCostCenterModel()
            {
                Id = item.Id,
                CostCenterName = item.CostCenterName,
                CostCenterCode = item.CostCenterCode,
                Ids = item.CostCenterWarehouses.Select(x => x.WarehouseId).ToList(),
            });

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetContractorCostCenters(
        long contractorId,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(c =>
                        (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.CostCenterCode, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.CostCenterName, filterData.MakeLikePattern())) &&
                         c.Projects.Any(p => p.ProjectOperations.Any(po => po.ProjectOperationDetails.Any(pod =>
                         pod.ProjectOperationDetailContractorServices.Any(x => x.ContractorId != null && x.ContractorId > 0 && x.ContractorId.Equals(contractorId)))))
                  );

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetsActiveAuthorizedCostCenter(
        string? filterData,
        long userId,
        long? employerId,
        long? costCenterTypeId,
        long? cityId,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.Projects)
            .Include(i => i.CostCenterWarehouses)
            .Include(i => i.CostCenterAuthorizedUsers)

            .Where(oo => oo.IsActive &&
                (companyId == null || oo.CompanyId == companyId) &&
                (code == null || oo.CostCenterCode.Contains(code)) &&
                (name == null || oo.CostCenterName.Contains(name)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterName, filterData.MakeLikePattern())) &&
                (oo.CostCenterAuthorizedUsers.Any(u => u.AuthorizedUserId == userId) || !oo.CostCenterAuthorizedUsers.Any()) &&
                (cityId == null || oo.CityId == cityId) &&
                (employerId == null || oo.Projects.Any(u => u.EmployerId == employerId)) &&
                (costCenterTypeId == null || oo.CostCenterType.Id == costCenterTypeId));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetsAuthorizedCostCenter(
        string? filterData,
        long userId,
        long? employerId,
        long? costCenterTypeId,
        long? cityId,
        string? code,
        string? name,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.Projects)
            .Include(i => i.CostCenterWarehouses)
            .Include(i => i.CostCenterAuthorizedUsers)

            .Where(oo =>
                (companyId == null || oo.CompanyId == companyId) &&
                (code == null || oo.CostCenterCode.Contains(code)) &&
                (name == null || oo.CostCenterName.Contains(name)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterName, filterData.MakeLikePattern())) &&
                (oo.CostCenterAuthorizedUsers.Any(u => u.AuthorizedUserId == userId) || !oo.CostCenterAuthorizedUsers.Any()) &&
                (cityId == null || oo.CityId == cityId) &&
                (employerId == null || oo.Projects.Any(u => u.EmployerId == employerId)) &&
                (costCenterTypeId == null || oo.CostCenterType.Id == costCenterTypeId));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<GetCostCentersModel> Data, int RowCount)> GetCostCenters(
        List<long>? ids,
        string? filterData,
        string? costCenterName,
        string? costCenterCode,
        long? costCenterTypeId,
        long? informedUserId,
        long? authorizedRoleId,
        long? authorizedUserId,
        long? warehouseId,
        long? cityId,
        bool? isActive,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = BuildQueryGetCostCenters(
            ids,
            filterData,
            costCenterName,
            costCenterCode,
            costCenterTypeId,
            informedUserId,
            authorizedRoleId,
            authorizedUserId,
            warehouseId,
            cityId,
            isActive,
            companyId);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetsByNameOrCode(
        string filterData,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.CostCenterType)
            .Include(i => i.CostCenterWarehouses)
            .Include(i => i.InformedUsers)
            .Include(i => i.CostCenterAuthorizedRoles)
            .Include(i => i.CostCenterAuthorizedUsers)

            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterName, filterData.MakeLikePattern()))
            );

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetsCostCenterByWarehouse(
        long warehouseId,
        string? filterData,
        long? companyId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(i => i.Projects)

            .Where(oo =>
                oo.CostCenterWarehouses.Any(x => x.WarehouseId.Equals(warehouseId)) &&
                (companyId == null || oo.CompanyId == companyId) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterName, filterData.MakeLikePattern()))
                );

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);
        return (costCenters, count);
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetsByAuthorizedUserId(
        string? filterData,
        long userId,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.CostCenterAuthorizedUsers)

            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.CostCenterAuthorizedUsers.Any(u => u.AuthorizedUserId == userId) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterName, filterData.MakeLikePattern()))
            );

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetsByAuthorizedRoleId(
        string? filterData,
        long roleId,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.CostCenterAuthorizedRoles)

            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.CostCenterAuthorizedRoles.Any(u => u.AuthorizedRoleId == roleId) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterName, filterData.MakeLikePattern()))
            );

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetsByEmployerId(
        string? filterData,
        long employerId,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(i => i.Projects)

            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.Projects.Any(u => u.EmployerId == employerId) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterName, filterData.MakeLikePattern())));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetsCostCenterByContractorId(
        string? filterData,
        long contractorId,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
        .Include(i => i.Projects)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.Projects.Any(u =>
            (u.ProjectOperations.Any(p => p.ContractorContractDetails.Any(c => c.ContractorContract.ContractorContractHeader.ContractorId == contractorId && !c.IsDeleted) && !p.IsDeleted) ||
             u.ProjectOperations.Any(p => p.ProjectOperationDetails.Any(x => x.ProjectOperationDetailContractorServices.Any(i => i.ContractorContractDetailServices.Any(c => c.ContractorContractDetail.ContractorContract.ContractorContractHeader.ContractorId == contractorId && !c.IsDeleted))) && !p.IsDeleted)) &&
            !u.IsDeleted) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterName, filterData.MakeLikePattern())));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetsCostCenterByProjectManagerId(
        string? filterData,
        long projectManagerId,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
        .Include(i => i.Projects)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.Projects.Any(u => u.ProjectManager == projectManagerId) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterName, filterData.MakeLikePattern())));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetsByCityId(
        long cityId,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(oo => (companyId == null || oo.CompanyId == companyId) && oo.CityId == cityId);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetsByTypeId(
        long typeId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(oo => oo.CostCenterType.Id == typeId);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<(List<CostCenter> Data, int RowCount)> GetsCostCenterByIds(
        List<long>? ids,
        List<Guid>? preferentialReferenceCodes,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(oo =>
            (ids == null || ids.Contains(oo.Id)) &&
            (preferentialReferenceCodes == null || preferentialReferenceCodes.Contains(oo.PreferentialReferenceCode)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterName, filterData.MakeLikePattern()))
            );

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var costCenters = await query.ToListAsync(ct);

        return (costCenters, count);
    }

    public async Task<List<long>?> GetsCostCenterWarehouseByProjectOperation(
        long projectOperationId,
        CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Projects.Any(x => x.ProjectOperations.Any(p => p.Id.Equals(projectOperationId))))
            .SelectMany(x => x.CostCenterWarehouses.OrderByDescending(x => x.IsDefault).Select(x => x.WarehouseId));

        var items = await query.ToListAsync(ct);

        return items;
    }

    public async Task<List<long>?> GetFilteredCostCenterCities(
        List<long>? costCenterIds,
        CT ct)
    {
        var query = DbSet
            .Where(oo => (costCenterIds == null || costCenterIds.Contains(oo.Id)) &&
                    oo.CityId > 0)
            .Select(x => x.CityId);

        var items = await query.Distinct().ToListAsync(ct);

        return items;
    }

    public async Task<List<CostCenter>?> GetByCodes(
        List<string> costCenterCodes,
        long? companyId,
        CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            costCenterCodes.Contains(oo.CostCenterCode))
            ;
        var result = await query.ToListAsync(ct);
        return result;
    }

    public async Task<List<CostCenter>?> GetByIdsIncludeType(
        List<long> ids,
        CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id))
            .Include(oo => oo.CostCenterType)
            .Include(oo => oo.CostCenterHistories);
        var result = await query.ToListAsync(ct);
        return result;
    }

    public async Task<CostCenter?> GetIsDefaultCostCenterByCompanyId(
        long companyId,
        CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.CompanyId == companyId && x.IsDefault == true, ct);
    }


    public async Task<(List<GetCompaniesCostCentersModel> Data, int RowCount)> GetCompaniesWork(
            long companyId, CT ct)
    {
        var query = DbSet
            .Where(x => x.CompanyId == companyId)
            .Select(x => new GetCompaniesCostCentersModel
            {
                Id = x.Id,
                CostCenterName = x.CostCenterName,
                CostCenterCode = x.CostCenterCode,
                Projects = x.Projects.Select(x => new GetCompaniesProjectsModel
                {
                    Id = x.Id,
                    ProjectName = x.ProjectName,
                    ProjectCode = x.ProjectCode
                }).ToList()
            });

        var count = await query.CountAsync(ct);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }
}