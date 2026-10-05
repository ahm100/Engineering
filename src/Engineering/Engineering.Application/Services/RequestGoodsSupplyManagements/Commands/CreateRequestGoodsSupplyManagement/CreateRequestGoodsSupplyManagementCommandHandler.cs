using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.RequestGoodsSupplyManagements.Commands.CreateRequestGoodsSupplyManagement;

public class CreateRequestGoodsSupplyManagementCommandHandler : ICommandHandler<CreateRequestGoodsSupplyManagementCommand, RequestGoodsSupplyManagement>
{
    private readonly IRequestGoodsSupplyManagementRepository _repository;
    private readonly ILogger<CreateRequestGoodsSupplyManagementCommandHandler> _logger;

    public CreateRequestGoodsSupplyManagementCommandHandler(IRequestGoodsSupplyManagementRepository repository,
                                                            ILogger<CreateRequestGoodsSupplyManagementCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<RequestGoodsSupplyManagement?>> Handle(CreateRequestGoodsSupplyManagementCommand request, CT ct)
    {
        try
        {
            var rGSupply = request.RequestGoodsSupplyProduct.RequestGoodsSupplyId;
            var update = await _repository.DoesManagementExist(request.RequestGoodsSupplyProduct.Id, request.InvoiceId, request.Type, ct);
            if (update is not null)
            {
                update.Update(request.WarehouseId, request.InvoiceId, request.RequestedCount, request.Type);
                return update;
            }
            else
            {
                var entity = RequestGoodsSupplyManagement.Create(request.RequestGoodsSupplyProduct, request.ProductId, request.WarehouseId, request.DestinationWarehouseId, request.InvoiceId,
                    request.RequestedCount, request.AlternateId, request.Type, request.Description, request.LastDescription);

                await _repository.Create(entity, ct);
                return entity;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupplyManagement>(SharedErrors.UnknownError);
        }
    }
}
