using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Persistence.Repositories.OperationInfos;

public class OperationInfoGroupRepository : BaseRepository<EngineeringDBContext, OperationInfoGroup>, IOperationInfoGroupRepository
{

    public OperationInfoGroupRepository(EngineeringDBContext context) : base(context)
    {

    }


    public async Task<OperationInfoGroup?> GetById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfoGroupRelations)
                .ThenInclude(x => x.OperationInfo)
            .Where(oo => oo.Id == id);

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<OperationInfoGroup?> FindByName(string name, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.OperationInfoGroupTitle == name &&
            (companyId == null || oo.CompanyId == companyId)
            );

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public Task<bool> FindOperationInfoGroupByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct)
    {
        var query = DbSet
            .AnyAsync(oo =>
            (companyId == null || oo.CompanyId == companyId) &&
            names.Contains(oo.OperationInfoGroupTitle) ||
            codes.Contains(oo.OperationInfoGroupCode),
            ct);

        return query;
    }

    public async Task<OperationInfoGroup?> FindByCode(string Code, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.OperationInfoGroupCode == Code &&
            (companyId == null || oo.CompanyId == companyId)
            );

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<OperationInfoGroup?> FindForDelete(long id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Id == id)
            .Include(oo => oo.OperationInfoGroupRelations);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<OperationInfoGroup> Data, int RowCount)> GetsOperationInfoGroup(List<long>? ids, string? filterData, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        var query = DbSet
            .Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoGroupTitle, filterData.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoGroupCode, filterData.MakeLikePattern()));
#pragma warning restore CS8604 // Possible null reference argument.

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<OperationInfoGroup> Data, int RowCount)> GetActiveOperationInfoGroups(string? filterData, string? code, string? name, long? companyId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Where(oo => oo.IsActive &&
            (companyId == null || oo.CompanyId == companyId) &&
            (code == null || oo.OperationInfoGroupCode.Contains(code)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoGroupCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.OperationInfoGroupTitle, filterData.MakeLikePattern())) &&
            (name == null || oo.OperationInfoGroupTitle.Contains(name)));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<string> CodeCreator(long? companyId, CT ct)
    {
        var query = await DbSet
            .Where(x => (companyId == null || x.CompanyId == companyId) &&
            EF.Functions.IsNumeric(x.OperationInfoGroupCode)).Select(x => Convert.ToInt64(x.OperationInfoGroupCode)).ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<(List<OperationInfoGroup> Data, int RowCount)> GetByOperationInfoGroupIds(List<long> groupIds, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.OperationInfoGroupRelations)
                .ThenInclude(x => x.OperationInfo)

            .Where(oo => groupIds.Contains(oo.Id));

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);
        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
}