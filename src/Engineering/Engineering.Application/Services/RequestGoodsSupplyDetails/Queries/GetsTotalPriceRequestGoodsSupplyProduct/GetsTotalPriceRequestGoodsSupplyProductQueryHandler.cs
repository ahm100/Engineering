using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsTotalPriceRequestGoodsSupplyProduct;

public class GetsTotalPriceRequestGoodsSupplyProductQueryHandler : IQueryHandler<GetsTotalPriceRequestGoodsSupplyProductQuery, decimal>
{
    private readonly ILogger<GetsTotalPriceRequestGoodsSupplyProductQueryHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public GetsTotalPriceRequestGoodsSupplyProductQueryHandler(ILogger<GetsTotalPriceRequestGoodsSupplyProductQueryHandler> logger, IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<decimal>> Handle(GetsTotalPriceRequestGoodsSupplyProductQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsTotalPriceRequestGoodsSupplyProduct(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.ProductIds,
                request.CreatorIds,
                request.WarehouseIds,
                request.CityId,
                request.ProjectManagerId,
                request.Importances,
                request.Types,
                request.Statuses,
                request.RemoveStatuses,
                request.StartDate,
                request.EndDate,
                request.RequestNumber,
                request.FilterDescription,
                request.FilterPublicName,
                request.FilterOperationInfoName,
                request.FilterManagerDescription,
                request.FilterData,
                request.CustomerInvoiceNumber,
                ct);

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<decimal>(SharedErrors.UnknownError);
        }
    }
}
