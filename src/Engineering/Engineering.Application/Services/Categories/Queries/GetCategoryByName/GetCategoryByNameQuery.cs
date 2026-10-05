using Engineering.Application.Services.Categories.Models.GetCategoryByName;

namespace Engineering.Application.Services.Categories.Queries.GetCategoryByName;

public record GetCategoryByNameQuery(
    string CategoryName)
    : IQuery<GetCategoryByNameResponse?>;