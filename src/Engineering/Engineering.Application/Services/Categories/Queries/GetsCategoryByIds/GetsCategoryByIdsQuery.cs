using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.GetsCategoryByIds;

public record GetsCategoryByIdsQuery(
    List<long> Items)
    : IQuery<List<Category>>;