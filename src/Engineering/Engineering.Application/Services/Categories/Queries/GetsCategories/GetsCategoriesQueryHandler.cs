using Engineering.Application.Abstractions.Data.Categories;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.GetsCategories;

public class GetsCategoriesQueryHandler : IQueryHandler<GetsCategoriesQuery, DataResult<List<Category>>>
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger<GetsCategoriesQueryHandler> _logger;

    public GetsCategoriesQueryHandler(
        ILogger<GetsCategoriesQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Category>>?>> Handle(GetsCategoriesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCategories(
                request.Ids,
                request.FilterData,
                request.code,
                request.name,
                request.isActive,
                request.OrderBy,
                request.CompanyId,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any()
                ? new DataResult<List<Category>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                }
                : Result.Failure<DataResult<List<Category>>>(CategoryErrors.FilteredCategoryNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Category>>>(SharedErrors.UnknownError);
        }
    }
}