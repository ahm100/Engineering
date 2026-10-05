using Engineering.Application.Abstractions.Data.OperationLocations;
using Engineering.Application.Services.OperationLocations.Queries.GetsLocationByProjectOperationDetailIds;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Persistence.Repositories.OperationLocations;

public class OperationLocationRepository : BaseRepository<EngineeringDBContext, OperationLocation>, IOperationLocationRepository
{
    public OperationLocationRepository(EngineeringDBContext context) : base(context)
    {
    }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public async Task<OperationLocation?> FindByName(
        string name,
        long? companyId,
        CT ct)
    {
        var query = DbSet

            .Include(x => x.CostCenter)
            .Include(oo => oo.Project)

            .Where(oo => oo.PrivateName == name &&
            (companyId == null || oo.CompanyId == companyId) &&
                !oo.CostCenter.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationLocation?> GetOperationLocationById(
        long id,
        CT ct)
    {
        var query = DbSet

            .Include(x => x.CostCenter)
            .Include(oo => oo.Project)
            .Include(x => x.Children.Where(x => !x.IsDeleted))
            .Include(x => x.ProjectOperationDetails)

            .Where(oo => oo.Id == id &&

                !oo.CostCenter.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationLocation?> GetOperationLocationNameByCostCenter(
        string name,
        long? costCenterId,
        long? projectId,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(oo => oo.Project)
            .Where(x => !x.IsDeleted && object.Equals(x.Parent, null))

            .Where(oo =>

                 (costCenterId == null || oo.CostCenterId == costCenterId) &&
                 (projectId == null || oo.ProjectId == projectId) &&
                 oo.PublicName == name);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationLocation?> GetOperationLocationNameByParent(
        string name,
        long parentId,
        long? companyId,
        CT ct)
    {
        var query = DbSet

            .Include(x => x.CostCenter)
            .Include(oo => oo.Project)

            .Where(oo => oo.PrivateName == name &&
                oo.ParentId == parentId &&
                (companyId == null || oo.CompanyId == companyId) &&
                !oo.CostCenter.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationLocation?> FindByCode(
        string code,
        long? companyId,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(oo => oo.Project)

            .Where(oo => oo.PrivateCode == code &&
            (companyId == null || oo.CompanyId == companyId) &&
                !oo.CostCenter.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<List<OperationLocation>?> GetByCodes(
        List<string> codes,
        CT ct)
    {
        var query = DbSet

            .Where(oo => codes.Contains(oo.PrivateCode) &&
                !oo.CostCenter.IsDeleted);

        var result = await query.ToListAsync(ct);
        return result;
    }

    public async Task<OperationLocation?> GetOperationLocationCodeByCostCenter(
        string code,
        long? costCenterId,
        long? projectId,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(oo => oo.Project)
            .Where(x => !x.IsDeleted && object.Equals(x.Parent, null))

            .Where(oo =>
                 (costCenterId == null || oo.CostCenterId == costCenterId) &&
                 (projectId == null || oo.ProjectId == projectId) &&
                oo.PrivateCode == code);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationLocation?> GetOperationLocationCodeByParent(
        string code,
        long parentId,
        long? companyId,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(oo => oo.Project)

            .Where(oo => oo.PrivateCode == code &&
                oo.ParentId == parentId &&
                (companyId == null || oo.CompanyId == companyId) &&
                !oo.CostCenter.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationLocation?> FindByIdWithCostCenter(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(x => x.Project)
            .Include(x => x.ProjectOperationDetails)
            .Where(oo => oo.Id == id && !oo.CostCenter.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<OperationLocation?> HaveOperationLocationChild(
        long operationLocationId,
        CT ct)
    {
        var query = DbSet
             .Where(oo => oo.Id == operationLocationId && oo.Children.Any());

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<string> CodeCreator(
        long? costCenterId,
        long? projectId,
        long? parentId,
        long? companyId,
        CT ct)
    {
        if (parentId is null)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var query = await DbSet
            .Include(oo => oo.CostCenter)
            .Include(oo => oo.Project)
            .Where(oo =>
                (companyId == null || oo.CompanyId == companyId) &&
                (costCenterId == null || oo.CostCenterId == costCenterId) &&
                (projectId == null || oo.ProjectId == projectId) &&
                object.Equals(oo.Parent, null) &&
                EF.Functions.IsNumeric(oo.PrivateCode)).Select(x => Convert.ToInt64(x.PrivateCode)).ToListAsync(ct);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            long suggestedCode = 1;
            if (query is not null && query.Any())
                suggestedCode = query.Max() + 1;

            return suggestedCode.ToString();
        }
        else
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var query = await DbSet
            .Include(oo => oo.CostCenter)
            .Include(oo => oo.Project)
            .Where(oo =>
                (companyId == null || oo.CompanyId == companyId) &&
                (costCenterId == null || oo.CostCenterId == costCenterId) &&
                (projectId == null || oo.ProjectId == projectId) &&
                (parentId == null || oo.ParentId == parentId) &&
                EF.Functions.IsNumeric(oo.PrivateCode)).Select(x => Convert.ToInt64(x.PrivateCode)).ToListAsync(ct);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            long suggestedCode = 1;
            if (query is not null && query.Any())
                suggestedCode = query.Max() + 1;

            return suggestedCode.ToString();
        }
    }

    public async Task<(List<OperationLocation> Data, int RowCount)> GetOperationLocations(
        List<long>? ids,
        long? costCenterId,
        long? projectId,
        long? parentId,
        string? filterData,
        bool? isActive,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(oo => oo.Project)
            .Include(oo => oo.Parent)
            .Include(oo => oo.Children.Where(x => !x.IsDeleted))

            .Where(oo =>
                (costCenterId == null || oo.CostCenterId == costCenterId) &&
                (projectId == null || oo.ProjectId == projectId) &&
                (parentId == null || oo.ParentId == parentId) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.PublicName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.PublicCode, filterData.MakeLikePattern())) &&
                (isActive == null || oo.IsActive == isActive) &&
                (ids == null || ids.Count == 0 || ids.Contains(oo.Id)));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<OperationLocation> Data, int RowCount)> GetsOperationLocation(
        string? filterData,
        string? publicName,
        string? publicCode,
        List<long>? ids,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(oo => oo.Project)
            .Include(oo => oo.Parent)
            .Include(oo => oo.Children.Where(x => !x.IsDeleted))

            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                (ids == null || ids.Contains(oo.Id)) &&
                (string.IsNullOrWhiteSpace(publicCode) || EF.Functions.Like(oo.PublicCode, publicCode.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(publicName) || EF.Functions.Like(oo.PublicName, publicName.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.PublicCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.PublicName!, filterData.MakeLikePattern())));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<OperationLocation> Data, int RowCount)> GetsOperationLocationChild(
        long parentId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(oo => oo.Project)
            .Include(oo => oo.Parent)
                .ThenInclude(oo => oo.Parent)
            .Include(oo => oo.Children.Where(x => !x.IsDeleted))

            .Where(oo => oo.ParentId == parentId && !oo.CostCenter.IsDeleted);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<OperationLocation> Data, int RowCount)> GetActiveOperationLocations(
        string? filterData,
        long? costCenterId,
        long? projectId,
        string? name,
        string? code,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(oo => oo.Project)
            .Include(oo => oo.Parent)
            .Include(oo => oo.Children.Where(x => !x.IsDeleted))
            .Where(oo =>
                (string.IsNullOrWhiteSpace(code) || EF.Functions.Like(oo.PublicCode, code.MakeLikePattern())) &&
                (string.IsNullOrWhiteSpace(name) || EF.Functions.Like(oo.PublicName, name.MakeLikePattern())) &&
                (costCenterId == null || oo.CostCenterId == costCenterId) &&
                (projectId == null || oo.ProjectId == projectId) &&
                (companyId == null || oo.CompanyId == companyId) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.PublicCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.PublicName!, filterData.MakeLikePattern())) &&
                oo.IsActive &&
                oo.ParentId == null &&

                !oo.CostCenter.IsDeleted &&
                oo.CostCenter.IsActive
            );

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<OperationLocation> Data, int RowCount)> GetByCostCenterId(
        long? costCenterId,
        long? projectId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(oo => oo.CostCenter)
            .Include(oo => oo.Project)
            .Include(oo => oo.Parent)
            .Include(oo => oo.Children.Where(x => !x.IsDeleted))

             .Where(oo =>
                (costCenterId == null || oo.CostCenterId == costCenterId) &&
                (projectId == null || oo.ProjectId == projectId));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<OperationLocation> Data, int RowCount)> GetsWithoutParentOperationLocation(
        long? costCenterId,
        long? projectId,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var query = DbSet
            .Include(oo => oo.CostCenter)
            .Include(oo => oo.Project)
            .Include(oo => oo.Parent)
            .Include(oo => oo.Children.Where(x => !x.IsDeleted))

            .Where(oo => oo.Parent == null &&
                (costCenterId == null || oo.CostCenterId == costCenterId) &&
                (projectId == null || oo.ProjectId == projectId) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.PublicName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.PublicCode, filterData.MakeLikePattern())) &&
                (isActive == null || oo.IsActive == isActive)
                );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<OperationLocation> Data, int RowCount)> GetsByIds(
        List<long> Ids,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(oo => oo.Project)

            .Where(b => Ids.Contains(b.Id));

        var count = await query.CountAsync(ct);
        var projectOperations = await query.ToListAsync(ct);

        return (projectOperations, count);
    }

    public async Task<(List<GetsLocationByProjectOperationDetailIdsModel> Data, int RowCount)> GetsLocationByProjectOperationDetailIds(
        List<long> projectOperationDetailIds,
        CT ct)
    {
        var query = DbSet
            .SelectMany(item => item.ProjectOperationDetails
                .Where(detail => projectOperationDetailIds.Contains(detail.Id))

            .Select(detail => new GetsLocationByProjectOperationDetailIdsModel()
            {
                Id = item.Id,
                ProjectOperationDetailId = detail.Id,
                Description = detail.Description,
                PrivateCode = item.PrivateCode,
                PrivateName = item.PrivateName,
                PublicCode = item.PublicCode,
                PublicName = item.PublicName,
            }));

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<OperationLocation> Data, int RowCount)> GetByCostCenterAsync(
        long? costCenterId,
        long? projectId,
        string? filterData,
        List<long>? operationInfoIds,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(oo =>
                oo.ProjectOperationDetails.Any(oo =>
                    oo.Status == ProjectOperationDetailStatus.NotStarted || oo.Status == ProjectOperationDetailStatus.Doing) &&
                (costCenterId == null || oo.ProjectOperationDetails.Any(oo => oo.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId))) &&
                (projectId == null || oo.ProjectOperationDetails.Any(oo => oo.ProjectOperation.ProjectId.Equals(projectId))) &&

                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.PublicName, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.PublicCode!, filterData.MakeLikePattern())) &&
                (operationInfoIds == null || oo.ProjectOperationDetails.Any(c => operationInfoIds.Contains(c.ProjectOperation.OperationInfo.Id))));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
}