using Engineering.Application.Abstractions.Data.Categories;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.IsDuplicateCategoryByName;

public class IsDuplicateCategoryByNameQueryHandler : IQueryHandler<IsDuplicateCategoryByNameQuery, Category?>
{
    private readonly ILogger<IsDuplicateCategoryByNameQueryHandler> _logger;
    private readonly ICategoryRepository _repository;

    public IsDuplicateCategoryByNameQueryHandler(
        ILogger<IsDuplicateCategoryByNameQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Category?>> Handle(IsDuplicateCategoryByNameQuery request, CT ct)
    {
        try
        {
            var categoryResponse = await _repository.IsDuplicateCategoryByName(
                request.CategoryName,
                request.CompanyId,
                ct);
            return categoryResponse ?? Result.Failure<Category>(CategoryErrors.CategoryWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Category>(SharedErrors.UnknownError);
        }
    }
}