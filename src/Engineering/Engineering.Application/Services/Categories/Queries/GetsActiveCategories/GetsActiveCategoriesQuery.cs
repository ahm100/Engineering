using Engineering.Application.Services.Categories.Models.GetsActiveCategories;

namespace Engineering.Application.Services.Categories.Queries.GetsActiveCategories;

public record GetsActiveCategoriesQuery(
    string? FilterData,
    string? Code,
    string? Name,
    long? CompanyId,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsActiveCategoriesResponseModel>>>;