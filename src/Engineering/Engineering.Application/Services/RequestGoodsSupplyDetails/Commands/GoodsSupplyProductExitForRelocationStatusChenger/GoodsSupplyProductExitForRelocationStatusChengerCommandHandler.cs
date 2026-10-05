using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Gita.Backend.Shared.Domain.Enums.Invoice;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.GoodsSupplyProductExitForRelocationStatusChenger;

public class GoodsSupplyProductExitForRelocationStatusChengerCommandHandler : ICommandHandler<GoodsSupplyProductExitForRelocationStatusChengerCommand, RequestGoodsSupplyProduct>
{
    private readonly ILogger<GoodsSupplyProductExitForRelocationStatusChengerCommandHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public GoodsSupplyProductExitForRelocationStatusChengerCommandHandler(ILogger<GoodsSupplyProductExitForRelocationStatusChengerCommandHandler> logger, IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyProduct?>> Handle(GoodsSupplyProductExitForRelocationStatusChengerCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetRequestGoodsSupplyProductForChangeStatus(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);

            var detailManagements = entity.RequestGoodsSupplyManagements.Where(x => x.InvoiceId == request.InvoiceId).ToList();
            if (detailManagements is null)
                return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);

            var status = GoodsSupplyManagementStatus.PendingForConfirme;
            switch (request.Status)
            {
                case WarehouseInvoiceStatus.Approved:
                    status = GoodsSupplyManagementStatus.CompleteSupply;
                    break;
                case WarehouseInvoiceStatus.Rejected:
                    status = GoodsSupplyManagementStatus.Return;
                    break;
                case WarehouseInvoiceStatus.Returned:
                    status = GoodsSupplyManagementStatus.Return;
                    break;
                case WarehouseInvoiceStatus.IncompleteDelivered:
                    status = GoodsSupplyManagementStatus.InCompleteSupply;
                    break;
            }

            entity.RequestGoodsSupplyManagements.ToList().ForEach(oo =>
            {
                if (oo.InvoiceId == request.InvoiceId)
                    oo.StatusChanger(status, request.LastDescription, request.UserId, null, null);
            });

            var statusDetail = GoodsSupplyDetailStatus.PendingForSupply;
            switch (request.Status)
            {
                case WarehouseInvoiceStatus.Approved:
                    statusDetail = GoodsSupplyDetailStatus.CompleteSupply;
                    break;
                case WarehouseInvoiceStatus.Rejected:
                    statusDetail = GoodsSupplyDetailStatus.NotCompleteSupply;
                    break;
                case WarehouseInvoiceStatus.Returned:
                    statusDetail = GoodsSupplyDetailStatus.NotCompleteSupply;
                    break;
                case WarehouseInvoiceStatus.IncompleteDelivered:
                    statusDetail = GoodsSupplyDetailStatus.InCompleteSupply;
                    break;
            }

            entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
            {
                oo.UpdateStatus(statusDetail, request.UserId, request.LastDescription);
            });

            entity.ChangeStatus(entity, request.LastDescription, request.UserId);
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupplyProduct>(SharedErrors.UnknownError);
        }
    }

}
