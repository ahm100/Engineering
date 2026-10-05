using Engineering.Application.Abstractions.Data.Categories;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.GetCategoryByCode;

public class GetCategoryByCodeQueryHandler : IQueryHandler<GetCategoryByCodeQuery, Category>
{
    private readonly ILogger<GetCategoryByCodeQueryHandler> _logger;
    private readonly ICategoryRepository _repository;

    public GetCategoryByCodeQueryHandler(
        ILogger<GetCategoryByCodeQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Category?>> Handle(GetCategoryByCodeQuery request, CT ct)
    {
        try
        {
            var categoryResponse = await _repository.GetCategoryByCode(request.CategoryCode, ct);
            return categoryResponse ?? Result.Failure<Category>(CategoryErrors.CategoryWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Category>(SharedErrors.UnknownError);
        }
    }
}