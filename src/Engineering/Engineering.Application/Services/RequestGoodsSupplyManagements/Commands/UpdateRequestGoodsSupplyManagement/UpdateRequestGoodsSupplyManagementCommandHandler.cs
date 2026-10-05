using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.RequestGoodsSupplyManagements.Commands.UpdateRequestGoodsSupplyManagement;

public class UpdateRequestGoodsSupplyManagementCommandHandler : ICommandHandler<UpdateRequestGoodsSupplyManagementCommand, RequestGoodsSupplyManagement>
{
    private readonly IRequestGoodsSupplyManagementRepository _repository;
    private readonly ILogger<UpdateRequestGoodsSupplyManagementCommandHandler> _logger;

    public UpdateRequestGoodsSupplyManagementCommandHandler(IRequestGoodsSupplyManagementRepository repository,
                                                            ILogger<UpdateRequestGoodsSupplyManagementCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<RequestGoodsSupplyManagement?>> Handle(UpdateRequestGoodsSupplyManagementCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.RequestGoodsSupplyManagementId, ct);
            if (entity is null)
                return Result.Failure<RequestGoodsSupplyManagement>(RequestGoodsSupplyManagementErrors.RequestGoodsSupplyManagementWithIdNotFound);
            if (entity.Status != GoodsSupplyManagementStatus.Return && entity.Status != GoodsSupplyManagementStatus.ReturnToSupply)
                return Result.Failure<RequestGoodsSupplyManagement>(RequestGoodsSupplyManagementErrors.InValidRequestGoodsSupplyManagementStatus);

            entity.SetWarehouseId(request.SourceWarehouseId);
            entity.SetDestinationWarehouseId(request.DestinationWarehouseId);
            entity.SetInvoiceId(request.InvoiceId);
            entity.SetRequestedCount(request.RequestedCount);
            entity.SetType(request.Type);
            entity.SetDescription(request.Description);
            entity.SetPendingForConfirme();
            entity.SetLastDescription(request.LastDescription);

            entity.RequestGoodsSupplyProduct!.UpdateStatus(entity.RequestGoodsSupplyProduct, entity.UpdaterId, entity.LastDescription);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupplyManagement>(SharedErrors.UnknownError);
        }
    }
}
