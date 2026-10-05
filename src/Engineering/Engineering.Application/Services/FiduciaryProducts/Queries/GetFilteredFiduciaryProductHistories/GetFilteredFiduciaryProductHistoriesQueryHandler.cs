using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductHistories;

namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFilteredFiduciaryProductHistories;

public class GetFilteredFiduciaryProductHistoriesQueryHandler : IQueryHandler<GetFilteredFiduciaryProductHistoriesQuery, DataResult<List<GetFilteredFiduciaryProductHistoriesModel>>>
{
    private readonly IFiduciaryProductHistoryRepository _repository;
    private readonly ILogger<GetFilteredFiduciaryProductHistoriesQueryHandler> _logger;

    public GetFilteredFiduciaryProductHistoriesQueryHandler(IFiduciaryProductHistoryRepository repository, ILogger<GetFilteredFiduciaryProductHistoriesQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<GetFilteredFiduciaryProductHistoriesModel>>?>> Handle(GetFilteredFiduciaryProductHistoriesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredFiduciaryProductHistories(
                request.FiduciaryProductId,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);
            return result.Data.Any() ?
                      new DataResult<List<GetFilteredFiduciaryProductHistoriesModel>>
                      {
                          Data = result.Data,
                          RowCount = result.RowCount
                      } : Result.Failure<DataResult<List<GetFilteredFiduciaryProductHistoriesModel>>>(FiduciaryProductHistoryErrors.FiduciaryProductHistoryWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetFilteredFiduciaryProductHistoriesModel>>>(SharedErrors.UnknownError);
        }
    }
}
