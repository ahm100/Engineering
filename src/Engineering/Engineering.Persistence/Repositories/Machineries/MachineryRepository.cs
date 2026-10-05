using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Persistence.Repositories.Machineries;

public class MachineryRepository : BaseRepository<EngineeringDBContext, Machinery>, IMachineryRepository
{
    public MachineryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<Machinery?> FindByName(string name, long? companyId, CT ct)
    {
        var query = DbSet
            .Include(x => x.MachineriesGroup)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.MachineryName == name &&
            !oo.MachineriesGroup.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<Machinery?> FindByCode(string code, long? companyId, CT ct)
    {
        var query = DbSet
            .Include(x => x.MachineriesGroup)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            oo.MachineryCode == code &&
            !oo.MachineriesGroup.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<Machinery?> FindByIdWithMachineriesGroup(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.MachineriesGroup)
            .Where(oo => oo.Id == id && !oo.MachineriesGroup.IsDeleted);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<Machinery?> GetMachineryWithoutInclude(long id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Id == id && !oo.MachineriesGroup.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<Machinery?> GetMachineryForDelete(long MachineryId, CT ct)
    {
        var query = DbSet
            .Include(x => x.ConsumptionStandardMachineries)
            .Include(x => x.ConsumableVolumeMachineries)

            .Where(oo => oo.Id == MachineryId);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<Machinery?> HaveMachineryChild(long MachineryId, CT ct)
    {
        var query = DbSet
             .Where(oo => oo.Id == MachineryId);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public Task<bool> FindMachineryByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct)
    {
        var query = DbSet
            .AnyAsync(oo => (companyId == null || oo.CompanyId == companyId) &&
            names.Contains(oo.MachineryName) ||
            codes.Contains(oo.MachineryCode),
            ct);

        return query;
    }

    public async Task<string> CodeCreator(long? companyId, CT ct)
    {
        var query = await DbSet
            .Where(x => (companyId == null || x.CompanyId == companyId) &&
            EF.Functions.IsNumeric(x.MachineryCode)).Select(x => Convert.ToInt64(x.MachineryCode)).ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<(List<Machinery> Data, int RowCount)> GetsMachineryByIds(List<long> ids, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);

        return (entities, count);
    }

    public async Task<(List<Machinery> Data, int RowCount)> GetMachineries(List<long>? ids, string? filterData, long? groupId, string? code, string? name, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.MachineriesGroup)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
            (code == null || oo.MachineryCode.Contains(code)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.MachineryCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.MachineryName, filterData.MakeLikePattern())) &&
            (name == null || oo.MachineryName.Contains(name)) &&
            (groupId == null || oo.MachineriesGroup.Id == groupId) && !oo.MachineriesGroup.IsDeleted);

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var Machineries = await query.ToListAsync(ct);

        return (Machineries, count);
    }

    public async Task<(List<Machinery> Data, int RowCount)> GetsMachineryForRequestMachinery(long? projectId, long? projectOperationId, long? projectOperationDetailId, long? machineriesGroupId,
        string? filterData, bool? isActive, long? companyId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet

            .Where(oo => !oo.MachineriesGroup.IsDeleted &&
                (companyId == null || oo.CompanyId == companyId) &&
                (projectId == null || oo.ConsumableVolumeMachineries.Any(x => x.ProjectOperationDetail.ProjectOperation.Project.Id.Equals(projectId))) &&
                (projectOperationId == null || oo.ConsumableVolumeMachineries.Any(x => x.ProjectOperationDetail.ProjectOperation.Id.Equals(projectOperationId))) &&
                (projectOperationDetailId == null || oo.ConsumableVolumeMachineries.Any(x => x.ProjectOperationDetail.Id.Equals(projectOperationDetailId))) &&
                (machineriesGroupId == null || oo.MachineriesGroup.Id.Equals(machineriesGroupId)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.MachineryCode, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.MachineryName, filterData.MakeLikePattern())));

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);

        return (items, count);
    }

    public async Task<(List<Machinery> Data, int RowCount)> GetActiveMachineries(string? filterData, long? groupId, string? code, string? name, long? companyId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.MachineriesGroup)
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
            (code == null || oo.MachineryCode.Contains(code)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.MachineryCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.MachineryName, filterData.MakeLikePattern())) &&
            (name == null || oo.MachineryName.Contains(name)) &&
            (groupId == null || oo.MachineriesGroup.Id == groupId) &&
            oo.IsActive &&

            !oo.MachineriesGroup.IsDeleted &&
            oo.MachineriesGroup.IsActive);
        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var Machineries = await query.ToListAsync(ct);

        return (Machineries, count);
    }

    public async Task<(List<Machinery> Data, int RowCount)> GetByMachineriesGroupId(long groupId, int pageIndex, int pageSize, CT ct)
    {

        var query = DbSet
             .Where(oo => oo.MachineriesGroup.Id == groupId);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var Machineries = await query.ToListAsync(ct);

        return (Machineries, count);
    }
}