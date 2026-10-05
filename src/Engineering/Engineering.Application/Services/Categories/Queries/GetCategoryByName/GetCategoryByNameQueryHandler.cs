using Engineering.Application.Abstractions.Data.Categories;
using Engineering.Application.Services.Categories.Models.GetCategoryByName;

namespace Engineering.Application.Services.Categories.Queries.GetCategoryByName;

public class GetCategoryByNameQueryHandler : IQueryHandler<GetCategoryByNameQuery, GetCategoryByNameResponse?>
{
    private readonly ILogger<GetCategoryByNameQueryHandler> _logger;
    private readonly ICategoryRepository _repository;

    public GetCategoryByNameQueryHandler(
        ILogger<GetCategoryByNameQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCategoryByNameResponse?>> Handle(GetCategoryByNameQuery request, CT ct)
    {
        try
        {
            var categoryResponse = await _repository.GetCategoryByName(request.CategoryName, ct);

            return categoryResponse is null ? Result.Failure<GetCategoryByNameResponse>(CategoryErrors.CategoryWithNameNotFound) :
            new GetCategoryByNameResponse
            {
                Id = categoryResponse.Id,
                CategoryName = categoryResponse.CategoryName,
                CategoryCode = categoryResponse.CategoryCode,
                AlternativeId = $"Category{categoryResponse.Id}",
                IsActive = categoryResponse.IsActive
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCategoryByNameResponse>(SharedErrors.UnknownError);
        }
    }
}