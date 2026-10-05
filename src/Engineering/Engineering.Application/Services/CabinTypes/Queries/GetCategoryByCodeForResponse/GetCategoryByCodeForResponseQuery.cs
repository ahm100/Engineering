using Engineering.Application.Services.Categories.Models.GetCategoryByCode;

namespace Engineering.Application.Services.CabinTypes.Queries.GetCategoryByCodeForResponse;

public record GetCategoryByCodeForResponseQuery(
    string CategoryCode)
    : IQuery<GetCategoryByCodeResponse?>;