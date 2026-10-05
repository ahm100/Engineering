using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProductByIdWithScale;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailBySupplyProductIdWithScale;

public class GetsGoodsSupplyDetailBySupplyProductIdWithScaleQueryHandler : IQueryHandler<GetsGoodsSupplyDetailBySupplyProductIdWithScaleQuery, DataResult<List<GetsGoodsSupplyDetailBySupplyProductIdWithScaleModel>>>
{
    private readonly ILogger<GetsGoodsSupplyDetailBySupplyProductIdWithScaleQueryHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public GetsGoodsSupplyDetailBySupplyProductIdWithScaleQueryHandler(ILogger<GetsGoodsSupplyDetailBySupplyProductIdWithScaleQueryHandler> logger, IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsGoodsSupplyDetailBySupplyProductIdWithScaleModel>>?>> Handle(GetsGoodsSupplyDetailBySupplyProductIdWithScaleQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsGoodsSupplyDetailBySupplyProductIdWithScale(
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
                request.RequestNumber,
                request.FilterDescription,
                request.FilterPublicName,
                request.FilterOperationInfoName,
                request.FilterManagerDescription,
                request.FilterData,
                request.CustomerInvoiceNumber,
                request.FilterProduct,
                request.FromDate,
                request.ToDate,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ? new DataResult<List<GetsGoodsSupplyDetailBySupplyProductIdWithScaleModel>>
            {
                Data = result.Data,
                RowCount = result.RowCount
            } : Result.Failure<DataResult<List<GetsGoodsSupplyDetailBySupplyProductIdWithScaleModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<GetsGoodsSupplyDetailBySupplyProductIdWithScaleModel>>>(SharedErrors.UnknownError);
        }
    }
}
