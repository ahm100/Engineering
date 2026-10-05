using Engineering.Application.Abstractions.Data.Categories;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.HaveCategoryChild;

public class HaveCategoryChildQueryHandler : IQueryHandler<HaveCategoryChildQuery, Category>
{
    private readonly ILogger<HaveCategoryChildQueryHandler> _logger;
    private readonly ICategoryRepository _repository;

    public HaveCategoryChildQueryHandler(
        ILogger<HaveCategoryChildQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Category?>> Handle(HaveCategoryChildQuery request, CT ct)
    {
        try
        {
            var result = await _repository.HaveCategoryChild(request.Id, ct);
            return result ?? Result.Failure<Category>(CategoryErrors.CategoryChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Category>(SharedErrors.UnknownError);
        }
    }
}