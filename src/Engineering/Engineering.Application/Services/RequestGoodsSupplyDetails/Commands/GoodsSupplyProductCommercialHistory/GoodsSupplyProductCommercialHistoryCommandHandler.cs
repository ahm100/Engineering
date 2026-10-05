using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.GoodsSupplyProductCommercialHistory;

public class GoodsSupplyProductCommercialHistoryCommandHandler : ICommandHandler<GoodsSupplyProductCommercialHistoryCommand, RequestGoodsSupplyProduct>
{
    private readonly ILogger<GoodsSupplyProductCommercialHistoryCommandHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public GoodsSupplyProductCommercialHistoryCommandHandler(ILogger<GoodsSupplyProductCommercialHistoryCommandHandler> logger, IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyProduct?>> Handle(GoodsSupplyProductCommercialHistoryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetRequestGoodsSupplyProductForChangeStatus(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);

            var requestGoodsSupplyManagements = entity.RequestGoodsSupplyManagements.Where(x => x.InvoiceId == request.InvoiceId).ToList();
            if (requestGoodsSupplyManagements is null)
                return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);

            var newDesc = $"{request.Status.GetEnumDescription()} - {request.LastDescription}";
            foreach (var item in entity.RequestGoodsSupplyManagements.Where(x => x.InvoiceId == request.InvoiceId))
                if (item.InvoiceId.Equals(request.InvoiceId))
                {
                    item.SetOperatorAppointmentId(request.OperatorAppointmentId);

                    item.SetLastDescription(newDesc);

                    item.AddHistory(request.UserId, newDesc);

                    if (!entity.IsHistoryAdded)
                    {
                        entity.SetLastDescription(newDesc);
                        entity.AddHistory();
                        entity.IsHistoryAdded = true;
                    }

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
