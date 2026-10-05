using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.GetsCategoryByCodes;

public record GetsCategoryByCodesQuery(
    List<string> Codes,
    long? CompanyId)
    : IQuery<DataResult<List<Category>>>;