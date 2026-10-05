using Engineering.Application.Abstractions.Data.Categories;
using Engineering.Application.Services.Categories.Models.GetsCategories;

namespace Engineering.Application.Services.CabinTypes.Queries.GetsCategoriesForResponse;

public class GetsCategoriesForResponseQueryHandler : IQueryHandler<GetsCategoriesForResponseQuery, DataResult<List<GetsCategoriesResponseModel>>>
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger<GetsCategoriesForResponseQueryHandler> _logger;

    public GetsCategoriesForResponseQueryHandler(
        ILogger<GetsCategoriesForResponseQueryHandler> logger,
        ICategoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsCategoriesResponseModel>>?>> Handle(GetsCategoriesForResponseQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCategoriesForResponse(
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
                ? new DataResult<List<GetsCategoriesResponseModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                }
                : Result.Failure<DataResult<List<GetsCategoriesResponseModel>>>(CategoryErrors.CategoryChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting categories for response, CompanyId:{CompanyId}", request.CompanyId);
            return Result.Failure<DataResult<List<GetsCategoriesResponseModel>>>(SharedErrors.UnknownError);
        }
    }
}