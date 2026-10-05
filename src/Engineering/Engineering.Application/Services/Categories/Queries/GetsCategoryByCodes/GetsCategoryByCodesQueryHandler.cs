using Engineering.Application.Abstractions.Data.Categories;
using Category = Engineering.Domain.Entities.Categories.Category;

namespace Engineering.Application.Services.Categories.Queries.GetsCategoryByCodes;

public class GetsCategoryByCodesQueryHandler : IQueryHandler<GetsCategoryByCodesQuery, DataResult<List<Category>>>
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger<GetsCategoryByCodesQueryHandler> _logger;

    public GetsCategoryByCodesQueryHandler(
        ILogger<GetsCategoryByCodesQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Category>>?>> Handle(GetsCategoryByCodesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCategoryByCodes(
                request.Codes,
                request.CompanyId, ct);

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