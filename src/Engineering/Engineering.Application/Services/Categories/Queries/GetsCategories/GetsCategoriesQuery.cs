using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.GetsCategories;

public record GetsCategoriesQuery(
    List<long>? Ids,
    string? FilterData,
    string? code,
    string? name,
    bool? isActive,
    string[]? OrderBy,
    long? CompanyId,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<Category>>>;