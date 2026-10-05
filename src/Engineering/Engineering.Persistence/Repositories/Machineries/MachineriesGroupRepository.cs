using Engineering.Application.Abstractions.Data.Machineries;
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Persistence.Repositories.Machineries;

public class MachineriesGroupRepository : BaseRepository<EngineeringDBContext, MachineriesGroup>, IMachineriesGroupRepository
{
    public MachineriesGroupRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<MachineriesGroup?> FindByName(string GroupName, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.GroupName == GroupName);

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public Task<bool> FindMachineriesGroupByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct)
    {
        var query = DbSet
            .AnyAsync(oo => (companyId == null || oo.CompanyId == companyId) &&
            names.Contains(oo.GroupName) ||
            codes.Contains(oo.GroupCode),
            ct);

        return query;
    }

    public async Task<MachineriesGroup?> FindByCode(string GroupCode, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.GroupCode == GroupCode);

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<List<MachineriesGroup>> GetsMachineriesGroupByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<MachineriesGroup?> HaveMachineriesGroupChild(long MachineriesGroupId, CT ct)
    {
        var query = DbSet
             .Where(oo => oo.Id == MachineriesGroupId && oo.Machineries.Any());

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<MachineriesGroup?> GetMachineriesGroupForDelete(long MachineriesGroupId, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Machineries)
                .ThenInclude(oo => oo.ConsumptionStandardMachineries)
            .Include(oo => oo.Machineries)
                .ThenInclude(oo => oo.ConsumableVolumeMachineries)
                    .ThenInclude(oo => oo.ProjectOperationDetail)

             .Where(oo => oo.Id == MachineriesGroupId);

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<string> CodeCreator(long? companyId, CT ct)
    {
        var query = await DbSet
            .Where(x => (companyId == null || x.CompanyId == companyId) &&
            EF.Functions.IsNumeric(x.GroupCode)).Select(x => Convert.ToInt64(x.GroupCode)).ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<(List<MachineriesGroup> Data, int RowCount)> GetsMachineriesGroupByCodes(List<string> codes, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            codes.Contains(oo.GroupCode));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<MachineriesGroup> Data, int RowCount)> GetMachineriesGroups(List<long>? ids, string? filterData, string? code, string? name, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            (code == null || oo.GroupCode.Contains(code)) &&
                (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.GroupCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.GroupName, filterData.MakeLikePattern())) &&
            (name == null || oo.GroupName.Contains(name))
            );

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created)
            .Include(oo => oo.Machineries);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<MachineriesGroup> Data, int RowCount)> GetsMachineryGroupsForRequestMachinery(long? projectId, List<long>? projectOperationIds, List<long>? projectOperationDetailIds,
        string? filterData, bool? isActive, long? companyId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet

            .Where(a => !a.IsDeleted &&
                (companyId == null || a.CompanyId == companyId) &&
                (projectId == null || a.Machineries.Any(m => m.ConsumableVolumeMachineries.Any(x => x.ProjectOperationDetail.ProjectOperation.Project.Id.Equals(projectId)))) &&
                (projectOperationIds == null || a.Machineries.Any(m => m.ConsumableVolumeMachineries.Any(x => projectOperationIds.Contains(x.ProjectOperationDetail.ProjectOperation.Id)))) &&
                (projectOperationDetailIds == null || a.Machineries.Any(m => m.ConsumableVolumeMachineries.Any(x => projectOperationDetailIds.Contains(x.ProjectOperationDetail.Id)))) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(a.GroupCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(a.GroupName, filterData.MakeLikePattern())));

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<MachineriesGroup> Data, int RowCount)> GetActiveMachineriesGroups(string? filterData, string? code, string? name, long? companyId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Where(oo => oo.IsActive &&
            (companyId == null || oo.CompanyId == companyId) &&
            (code == null || oo.GroupCode.Contains(code)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.GroupCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.GroupName, filterData.MakeLikePattern())) &&
            (name == null || oo.GroupName.Contains(name))
            );

        query = query.OrderByDescending(oo => oo.Created)
            .Include(oo => oo.Machineries);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

}