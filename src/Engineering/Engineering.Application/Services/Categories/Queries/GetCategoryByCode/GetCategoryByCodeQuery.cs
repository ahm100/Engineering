using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.GetCategoryByCode;

public record GetCategoryByCodeQuery(
    string CategoryCode)
    : IQuery<Category>;