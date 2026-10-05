using Engineering.Application.Services.Categories.Models.GetCategoryById;

namespace Engineering.Application.Services.Categories.Queries.GetCategoryByIdForResponse;

public record GetCategoryByIdForResponseQuery(long Id) : IQuery<GetCategoryByIdResponse>;