using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyProduct;

public class GetsRequestGoodsSupplyProductQueryHandler : IQueryHandler<GetsRequestGoodsSupplyProductQuery, DataResult<List<GetsRequestGoodsSupplyProductModel>>>
{
    private readonly ILogger<GetsRequestGoodsSupplyProductQueryHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public GetsRequestGoodsSupplyProductQueryHandler(ILogger<GetsRequestGoodsSupplyProductQueryHandler> logger, IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsRequestGoodsSupplyProductModel>>?>> Handle(GetsRequestGoodsSupplyProductQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsRequestGoodsSupplyProduct(
                request.Ids,
                request.requestGoodsSupplyIds,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.ProductIds,
                request.CreatorIds,
                request.WarehouseIds,
                request.CityId,
                request.ProjectManagerId,
                request.ThirdPartyId,
                request.ChechThirdParty,
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
                request.OrderBy,
                request.IsExcel,
                request.ContainDraft,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ? new DataResult<List<GetsRequestGoodsSupplyProductModel>>
            {
                Data = result.Data,
                RowCount = result.RowCount
            } : Result.Failure<DataResult<List<GetsRequestGoodsSupplyProductModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<GetsRequestGoodsSupplyProductModel>>>(SharedErrors.UnknownError);
        }
    }
}
