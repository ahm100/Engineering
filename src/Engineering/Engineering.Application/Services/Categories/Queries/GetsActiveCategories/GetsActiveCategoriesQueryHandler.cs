using Engineering.Application.Abstractions.Data.Categories;
using Engineering.Application.Services.Categories.Models.GetsActiveCategories;

namespace Engineering.Application.Services.Categories.Queries.GetsActiveCategories;

public class GetsActiveCategoriesQueryHandler : IQueryHandler<GetsActiveCategoriesQuery, DataResult<List<GetsActiveCategoriesResponseModel>>>
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger<GetsActiveCategoriesQueryHandler> _logger;

    public GetsActiveCategoriesQueryHandler(
        ILogger<GetsActiveCategoriesQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsActiveCategoriesResponseModel>>?>> Handle(GetsActiveCategoriesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsActiveCategoriesForResponse(
                request.FilterData,
                request.Code,
                request.Name,
                request.CompanyId,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any()
                ? new DataResult<List<GetsActiveCategoriesResponseModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                }
                : Result.Failure<DataResult<List<GetsActiveCategoriesResponseModel>>>(CategoryErrors.CategoryChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting active categories for CompanyId:{CompanyId}", request.CompanyId);
            return Result.Failure<DataResult<List<GetsActiveCategoriesResponseModel>>>(SharedErrors.UnknownError);
        }
    }
}