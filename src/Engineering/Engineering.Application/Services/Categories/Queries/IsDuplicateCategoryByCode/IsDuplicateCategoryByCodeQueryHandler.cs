using Engineering.Application.Abstractions.Data.Categories;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.IsDuplicateCategoryByCode;

public class IsDuplicateCategoryByCodeQueryHandler : IQueryHandler<IsDuplicateCategoryByCodeQuery, Category>
{
    private readonly ILogger<IsDuplicateCategoryByCodeQueryHandler> _logger;
    private readonly ICategoryRepository _repository;

    public IsDuplicateCategoryByCodeQueryHandler(
        ILogger<IsDuplicateCategoryByCodeQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Category?>> Handle(IsDuplicateCategoryByCodeQuery request, CT ct)
    {
        try
        {
            var categoryResponse = await _repository.IsDuplicateCategoryByCode(
                request.CategoryCode,
                request.CompanyId,
                ct);
            return categoryResponse ?? Result.Failure<Category>(CategoryErrors.CategoryWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Category>(SharedErrors.UnknownError);
        }
    }
}