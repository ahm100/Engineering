using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.IsDuplicateCategoryByCode;

public record IsDuplicateCategoryByCodeQuery(
    string CategoryCode,
    long? CompanyId)
    : IQuery<Category>;