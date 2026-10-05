using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Categories;
using Engineering.Domain.Entities.Synonyms.Warehouse.Categories;
namespace Engineering.Persistence.Repositories.Synonyms.Warehouse.Categories;

public class ViewCategoryRepository : BaseRepository<EngineeringDBContext, ViewCategory>, IViewCategoryRepository
{
    public ViewCategoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ViewCategory?> GetById(
        long id, CT ct)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<List<ViewCategory>> GetByIds(
        List<long> ids, CT ct)
    {
        var query = DbSet
            .Where(oo => ids.Contains(oo.Id) && !oo.IsDeleted);

        return await query.ToListAsync(ct);
    }

    public async Task<List<ViewCategory>> GetFilteredGroupsByIds(
        List<long> ids,
        string? groupFilterData,
        string? productFilterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Where(x => ids.Contains(x.Id) &&
        (string.IsNullOrWhiteSpace(groupFilterData) || x.Groups.Any(z => EF.Functions.Like(z.Name, groupFilterData.MakeLikePattern())) ||
            string.IsNullOrWhiteSpace(groupFilterData) || x.Groups.Any(z => EF.Functions.Like(z.Code, groupFilterData.MakeLikePattern()))) &&

        (string.IsNullOrWhiteSpace(productFilterData) || x.Groups.SelectMany(x => x.Products).Any(z => EF.Functions.Like(z.Name, productFilterData.MakeLikePattern())) ||
            string.IsNullOrWhiteSpace(productFilterData) || x.Groups.SelectMany(x => x.Products).Any(z => EF.Functions.Like(z.Code, productFilterData.MakeLikePattern()))));

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        return await query.ToListAsync(ct);
    }
}