using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(
    long Id)
    : IQuery<Category?>;