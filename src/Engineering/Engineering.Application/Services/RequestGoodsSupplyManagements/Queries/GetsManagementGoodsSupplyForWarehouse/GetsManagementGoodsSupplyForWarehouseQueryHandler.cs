using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.RequestGoodsSupplyManagements.Queries.GetsManagementGoodsSupplyForWarehouse;

public class GetsManagementGoodsSupplyForWarehouseQueryHandler : IQueryHandler<GetsManagementGoodsSupplyForWarehouseQuery, List<RequestGoodsSupplyManagement>>
{
    private readonly IRequestGoodsSupplyManagementRepository _repository;
    private readonly ILogger<GetsManagementGoodsSupplyForWarehouseQueryHandler> _logger;

    public GetsManagementGoodsSupplyForWarehouseQueryHandler(IRequestGoodsSupplyManagementRepository repository, ILogger<GetsManagementGoodsSupplyForWarehouseQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<RequestGoodsSupplyManagement>?>> Handle(GetsManagementGoodsSupplyForWarehouseQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsManagementGoodsSupplyForWarehouse(request.InvoiceId, request.RequestGoodsSupplyId, request.CommercialRequestNo, request.ProductId, ct);

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<RequestGoodsSupplyManagement>>(SharedErrors.UnknownError);
        }
    }
}
