using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.GetCategoryWithoutInclude;

public record GetCategoryWithoutIncludeQuery(
    long Id)
    : IQuery<Category?>;