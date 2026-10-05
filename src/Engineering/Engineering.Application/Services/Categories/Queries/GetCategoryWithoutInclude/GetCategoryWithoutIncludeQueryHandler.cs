using Engineering.Application.Abstractions.Data.Categories;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.GetCategoryWithoutInclude;

public class GetCategoryWithoutIncludeQueryHandler : IQueryHandler<GetCategoryWithoutIncludeQuery, Category?>
{
    private readonly ILogger<GetCategoryWithoutIncludeQueryHandler> _logger;
    private readonly ICategoryRepository _repository;

    public GetCategoryWithoutIncludeQueryHandler(
        ILogger<GetCategoryWithoutIncludeQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Category?>> Handle(GetCategoryWithoutIncludeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCategoryWithoutIncludeById(request.Id, ct);
            return result ?? Result.Failure<Category?>(CategoryErrors.CategoryWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Category?>(SharedErrors.UnknownError);
        }
    }
}