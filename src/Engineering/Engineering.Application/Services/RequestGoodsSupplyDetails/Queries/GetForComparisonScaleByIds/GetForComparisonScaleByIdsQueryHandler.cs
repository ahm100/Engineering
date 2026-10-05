using Engineering.Application.Abstractions.Data.RequestGoodsSupplies.Documents;
using Engineering.Domain.Entities.RequestGoodsSupplies.Documents;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetForComparisonScaleByIds;

public class GetForComparisonScaleByIdsQueryHandler : IQueryHandler<GetForComparisonScaleByIdsQuery, List<RequestGoodsSupplyDetailDocument>?>
{
    private readonly ILogger<GetForComparisonScaleByIdsQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailDocumentRepository _repository;

    public GetForComparisonScaleByIdsQueryHandler(ILogger<GetForComparisonScaleByIdsQueryHandler> logger, IRequestGoodsSupplyDetailDocumentRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<RequestGoodsSupplyDetailDocument>?>> Handle(GetForComparisonScaleByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetRequestGoodsSupplyProductByIds(
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
                ct);

            if (result is not null && result.Count > 0)
            {
                return result;
            }
            else
                return result ?? Result.Failure<List<RequestGoodsSupplyDetailDocument>?>(RequestGoodsSupplyDetailErrors.RequestGoodsSupplyDetailProductsNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<RequestGoodsSupplyDetailDocument>?>(SharedErrors.UnknownError);
        }
    }
}
