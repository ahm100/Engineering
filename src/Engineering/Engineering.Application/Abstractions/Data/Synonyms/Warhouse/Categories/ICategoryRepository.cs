using Engineering.Domain.Entities.Synonyms.Warehouse.Categories;
namespace Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Categories;

public interface IViewCategoryRepository : IBaseRepository<ViewCategory>
{
    Task<ViewCategory?> GetById(
        long id, CT ct);

    Task<List<ViewCategory>> GetByIds(
        List<long> ids, CT ct);

    Task<List<ViewCategory>> GetFilteredGroupsByIds(
        List<long> ids,
        string? groupFilterData,
        string? productFilterData,
        int pageIndex,
        int pageSize,
        CT ct);
}