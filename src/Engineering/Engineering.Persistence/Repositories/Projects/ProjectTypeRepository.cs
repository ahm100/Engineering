using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Repositories.Projects;

public class ProjectTypeRepository : BaseRepository<EngineeringDBContext, ProjectType>, IProjectTypeRepository
{
    public ProjectTypeRepository(EngineeringDBContext context) : base(context)
    {

    }

    public async Task<ProjectType?> FindByName(string name, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.ProjectTypeTitle == name &&
            (companyId == null || oo.CompanyId == companyId)
            );

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public Task<bool> FindProjectTypeByNamesOrCodes(List<string> names, List<string> codes, long? companyId, CT ct)
    {
        var query = DbSet
            .AnyAsync(oo =>
            (companyId == null || oo.CompanyId == companyId) &&
            names.Contains(oo.ProjectTypeTitle) ||
            codes.Contains(oo.ProjectTypeCode),
            ct);

        return query;
    }

    public async Task<ProjectType?> FindByCode(string Code, long? companyId, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.ProjectTypeCode == Code &&
            (companyId == null || oo.CompanyId == companyId)
            );

        var result = await query
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<ProjectType?> FindForDelete(long id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Id == id)
            .Include(oo => oo.Projects);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<ProjectType> Data, int RowCount)> GetsProjectType(List<long>? ids, string? filterData, bool? isActive, long? companyId, string[]? orderBy, int pageIndex, int pageSize, CT ct)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        var query = DbSet.AsQueryable();

        if (companyId != null)
            query = query.Where(x => x.CompanyId == companyId);

        if (ids?.Count > 0)
            query = query.Where(x => ids.Contains(x.Id));

        if (!string.IsNullOrWhiteSpace(filterData))
        {
            var pattern = filterData.MakeLikePattern();

            query = query.Where(x =>
                EF.Functions.Like(x.ProjectTypeTitle, pattern) ||
                EF.Functions.Like(x.ProjectTypeCode, pattern));
        }
#pragma warning restore CS8604 // Possible null reference argument.

        if (isActive != null)
            query = query.Where(oo => oo.IsActive == isActive);

        query = query.OrderBy(oo => !oo.IsActive).ThenByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<List<ProjectType>> GetsProjectTypeByIds(List<long> ids, CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id));

        var items = await query.ToListAsync(ct);
        return items;
    }

    public async Task<(List<ProjectType> Data, int RowCount)> GetActiveProjectTypes(string? filterData, string? code, string? name, long? companyId, int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Where(oo => oo.IsActive &&
            (companyId == null || oo.CompanyId == companyId) &&
            (code == null || oo.ProjectTypeCode.Contains(code)) &&
            (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ProjectTypeCode, filterData.MakeLikePattern()) ||
            string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.ProjectTypeTitle, filterData.MakeLikePattern())) &&
            (name == null || oo.ProjectTypeTitle.Contains(name)));

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
            EF.Functions.IsNumeric(x.ProjectTypeCode)).Select(x => Convert.ToInt64(x.ProjectTypeCode)).ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

}