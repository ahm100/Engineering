using Engineering.Application.Abstractions.Data.Categories;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, Category?>
{
    private readonly ILogger<GetCategoryByIdQueryHandler> _logger;
    private readonly ICategoryRepository _repository;

    public GetCategoryByIdQueryHandler(
        ILogger<GetCategoryByIdQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Category?>> Handle(GetCategoryByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCategoryById(request.Id, ct);
            return result ?? Result.Failure<Category?>(CategoryErrors.CategoryWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Category?>(SharedErrors.UnknownError);
        }
    }
}