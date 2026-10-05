using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductDetailHistories;

namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFilteredFiduciaryProductDetailHistories;

public class GetFilteredFiduciaryProductDetailHistoriesQueryHandler : IQueryHandler<GetFilteredFiduciaryProductDetailHistoriesQuery, DataResult<List<GetFilteredFiduciaryProductDetailHistoriesModel>>>
{
    private readonly IFiduciaryProductDetailHistoryRepository _repository;
    private readonly ILogger<GetFilteredFiduciaryProductDetailHistoriesQueryHandler> _logger;

    public GetFilteredFiduciaryProductDetailHistoriesQueryHandler(IFiduciaryProductDetailHistoryRepository repository, ILogger<GetFilteredFiduciaryProductDetailHistoriesQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<GetFilteredFiduciaryProductDetailHistoriesModel>>?>> Handle(GetFilteredFiduciaryProductDetailHistoriesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredFiduciaryProductDetailHistories(
                request.FiduciaryProductId,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);
            return result.Data.Any() ?
                        new DataResult<List<GetFilteredFiduciaryProductDetailHistoriesModel>>
                        {
                            Data = result.Data,
                            RowCount = result.RowCount
                        } : Result.Failure<DataResult<List<GetFilteredFiduciaryProductDetailHistoriesModel>>>(FiduciaryProductDetailHistoryErrors.FiduciaryProductDetailHistoryWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetFilteredFiduciaryProductDetailHistoriesModel>>>(SharedErrors.UnknownError);
        }
    }
}
