using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProduct;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyProduct;

public class GetsGoodsSupplyProductQueryHandler : IQueryHandler<GetsGoodsSupplyProductQuery, DataResult<List<GetsGoodsSupplyProductModel>>>
{
    private readonly ILogger<GetsGoodsSupplyProductQueryHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public GetsGoodsSupplyProductQueryHandler(ILogger<GetsGoodsSupplyProductQueryHandler> logger, IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsGoodsSupplyProductModel>>?>> Handle(GetsGoodsSupplyProductQuery request, CT ct)
    {
        try
        {
            var requestItem = request.Request;
            var result = await _repository.GetsGoodsSupplyProduct(
            request.Ids,
            requestItem.RequestGoodsSupplyIds,
            requestItem.CostCenterIds,
            requestItem.ProjectIds,
            requestItem.ProjectOperationIds,
            requestItem.ProjectOperationDetailIds,
            request.ProductIds,
            requestItem.CreatorIds,
            requestItem.WarehouseIds,
            requestItem.ContractorIds,
            requestItem.BuyerIds,
            requestItem.SupplyerIds,
            request.ManagerSelectedProductIds,
            requestItem.ThirdPartyId,
            requestItem.CityId,
            requestItem.RequestGoodsSupplyId,
            requestItem.ProjectManagerId,
            requestItem.Importances,
            requestItem.Types,
            requestItem.Statuses,
            requestItem.RemoveStatuses,
            requestItem.StartDate,
            requestItem.EndDate,
            requestItem.RequestNumber,
            requestItem.FilterDescription,
            requestItem.FilterPublicName,
            requestItem.FilterOperationInfoName,
            requestItem.FilterManagerDescription,
            requestItem.FilterData,
            requestItem.CustomerInvoiceNumber,
            requestItem.IsDeraft,
            requestItem.OrderBy,
            request.IsExcel,
            request.CompanyId,
            request.CheckThirdParty,
            requestItem.PageIndex,
            requestItem.PageSize,
            ct);

            return result.Data.Any() ? new DataResult<List<GetsGoodsSupplyProductModel>>
            {
                Data = result.Data,
                RowCount = result.RowCount
            } : Result.Failure<DataResult<List<GetsGoodsSupplyProductModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<GetsGoodsSupplyProductModel>>>(SharedErrors.UnknownError);
        }
    }
}
