using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.HaveCategoryChild;

public record HaveCategoryChildQuery(
    long Id)
    : IQuery<Category>;