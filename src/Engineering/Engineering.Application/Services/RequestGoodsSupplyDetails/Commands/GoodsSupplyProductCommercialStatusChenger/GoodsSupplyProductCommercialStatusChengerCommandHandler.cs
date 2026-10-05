using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Gita.Backend.Shared.Domain.Enums.Commerces;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.GoodsSupplyProductCommercialStatusChenger;

public class GoodsSupplyProductCommercialStatusChengerCommandHandler : ICommandHandler<GoodsSupplyProductCommercialStatusChengerCommand, RequestGoodsSupplyProduct>
{
    private readonly ILogger<GoodsSupplyProductCommercialStatusChengerCommandHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public GoodsSupplyProductCommercialStatusChengerCommandHandler(ILogger<GoodsSupplyProductCommercialStatusChengerCommandHandler> logger, IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyProduct?>> Handle(GoodsSupplyProductCommercialStatusChengerCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetRequestGoodsSupplyProductForChangeStatus(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);

            var goodsSupplyDetail = entity.RequestGoodsSupplyManagements.Where(x => x.InvoiceId == request.InvoiceId).FirstOrDefault();
            if (goodsSupplyDetail is null)
                return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);

            foreach (var item in entity.RequestGoodsSupplyManagements)
                if (item.InvoiceId.Equals(request.InvoiceId))
                {
                    var status = GoodsSupplyManagementStatus.PendingForConfirme;
                    switch (request.Status)
                    {
                        case CommerceRequestStatus.Done:
                            status = GoodsSupplyManagementStatus.CompleteSupply;
                            break;
                        case CommerceRequestStatus.PreInvoiceConfirm:
                            status = GoodsSupplyManagementStatus.CommercialInvoiceConfirmation;
                            break;
                        case CommerceRequestStatus.PreInvoiceUnpaid:
                            status = GoodsSupplyManagementStatus.CommercialInvoiceConfirmation;
                            break;
                        case CommerceRequestStatus.Rejected:
                            status = GoodsSupplyManagementStatus.Return;
                            break;
                        case CommerceRequestStatus.Return:
                            status = GoodsSupplyManagementStatus.Return;
                            break;
                        case CommerceRequestStatus.ReturnToSupply:
                            status = GoodsSupplyManagementStatus.ReturnToSupply;
                            break;
                    }

                    item.StatusChanger(status, request.LastDescription, request.UserId, request.OperatorAppointmentId, request.ConfirmedRequestCount);

                    var statusDetail = GoodsSupplyDetailStatus.PendingForSupply;
                    switch (request.Status)
                    {
                        case CommerceRequestStatus.Done:
                            statusDetail = GoodsSupplyDetailStatus.CompleteSupply;
                            break;
                        case CommerceRequestStatus.Rejected:
                            statusDetail = GoodsSupplyDetailStatus.NotCompleteSupply;
                            break;
                        case CommerceRequestStatus.PreInvoiceConfirm:
                            statusDetail = GoodsSupplyDetailStatus.CommercialInvoiceConfirmation;
                            break;
                        case CommerceRequestStatus.Return:
                            statusDetail = GoodsSupplyDetailStatus.NotCompleteSupply;
                            break;
                        case CommerceRequestStatus.ReturnToSupply:
                            statusDetail = GoodsSupplyDetailStatus.ReturnToSupply;
                            break;
                        case CommerceRequestStatus.PreInvoiceUnpaid:
                            statusDetail = GoodsSupplyDetailStatus.CommercialInvoiceConfirmation;
                            break;
                    }

                    entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                    {
                        oo.UpdateStatus(statusDetail, request.UserId, request.LastDescription);
                    });

                    entity.ChangeStatus(entity, request.LastDescription, request.UserId);
                    await _repository.Update(entity);
                }

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupplyProduct>(SharedErrors.UnknownError);
        }
    }

}
