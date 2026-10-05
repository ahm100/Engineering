using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.IsDuplicateCategoryByName;

public record IsDuplicateCategoryByNameQuery(
    string CategoryName,
    long? CompanyId)
    : IQuery<Category?>;