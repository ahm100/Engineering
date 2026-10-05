using Engineering.Application.Services.Categories.Models.GetsCategories;

namespace Engineering.Application.Services.CabinTypes.Queries.GetsCategoriesForResponse;

public record GetsCategoriesForResponseQuery(
    List<long>? Ids,
    string? FilterData,
    string? code,
    string? name,
    bool? isActive,
    string[]? OrderBy,
    long? CompanyId,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsCategoriesResponseModel>>>;
