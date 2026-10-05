using Engineering.Application.Abstractions.Data.Categories;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.GetsCategoryByIds;

public class GetsCategoryByIdsQueryHandler : IQueryHandler<GetsCategoryByIdsQuery, List<Category>>
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger<GetsCategoryByIdsQuery> _logger;

    public GetsCategoryByIdsQueryHandler(
        ILogger<GetsCategoryByIdsQuery> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<Category>?>> Handle(GetsCategoryByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCategoryByIds(request.Items, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<Category>>(SharedErrors.UnknownError);
        }
    }
}