using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProducts;

namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFilteredFiduciaryProducts;

public class GetFilteredFiduciaryProductsQueryHandler : IQueryHandler<GetFilteredFiduciaryProductsQuery, DataResult<List<GetFilteredFiduciaryProductsModel>>>
{
    private readonly IFiduciaryProductRepository _repository;
    private readonly ILogger<GetFilteredFiduciaryProductsQueryHandler> _logger;

    public GetFilteredFiduciaryProductsQueryHandler(IFiduciaryProductRepository repository, ILogger<GetFilteredFiduciaryProductsQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<GetFilteredFiduciaryProductsModel>>?>> Handle(GetFilteredFiduciaryProductsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredAsync(request.Ids,
                request.CostCenterId,
                request.ProjectId,
                request.Status,
                request.ProjectOperationIds,
                request.ThirdPartyId,
                request.FromDate,
                request.ToDate,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);
            return result.Data.Any() ?
                      new DataResult<List<GetFilteredFiduciaryProductsModel>>
                      {
                          Data = result.Data,
                          RowCount = result.RowCount
                      } : Result.Failure<DataResult<List<GetFilteredFiduciaryProductsModel>>>(FiduciaryProductHistoryErrors.FiduciaryProductHistoryNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetFilteredFiduciaryProductsModel>>>(SharedErrors.UnknownError);
        }
    }
}
