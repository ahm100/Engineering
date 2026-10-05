using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyProducts;

public class GetRequestGoodsSupplyProductsQueryHandler : IQueryHandler<GetRequestGoodsSupplyProductsQuery, DataResult<List<long>>>
{
    private readonly IRequestGoodsSupplyRepository _repository;
    private readonly ILogger<GetRequestGoodsSupplyProductsQueryHandler> _logger;

    public GetRequestGoodsSupplyProductsQueryHandler(ILogger<GetRequestGoodsSupplyProductsQueryHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetRequestGoodsSupplyProductsQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredProducts(
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.RequestGoodsSupplyIds,
                request.ProductGroupIds, ct);

            return items.Data.Any() ?
               new DataResult<List<long>>
               {
                   Data = items.Data,
                   RowCount = items.RowCount
               } : Result.Failure<DataResult<List<long>>>(RequestGoodsSupplyErrors.RequestGoodsProductNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
        }
    }
}
