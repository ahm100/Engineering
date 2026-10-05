using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.UpdateRequestGoodsSupply;

public class UpdateRequestGoodsSupplyCommandHandler : ICommandHandler<UpdateRequestGoodsSupplyCommand, RequestGoodsSupply>
{
    private readonly ILogger<UpdateRequestGoodsSupplyCommandHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public UpdateRequestGoodsSupplyCommandHandler(
        ILogger<UpdateRequestGoodsSupplyCommandHandler> logger,
        IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupply?>> Handle(UpdateRequestGoodsSupplyCommand request, CT ct)
    {
        try
        {
            var entity = request.GoodsSupply;

            if (entity.Type == GoodsSupplyType.Project && request.SupplyerId is not null)
                entity.SetSupplyerId(request.SupplyerId);

            if (entity.Status == GoodsSupplyStatus.Created && request.IsDraft)
            {
                entity.SetStatusToDraft();
                if (entity.RequestGoodsSupplyProducts.Any(x => x.Status != GoodsSupplyDetailStatus.New && x.Status != GoodsSupplyDetailStatus.Draft))
                    return Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyDetailErrors.InValidStatusForDraft);
                else
                    foreach (var product in entity.RequestGoodsSupplyProducts)
                        if (GSDSRules.AllowStatusForUpdate.Any(x => x.Equals(product.Status)))
                        {
                            product.SetStatusToDraft();
                            foreach (var detail in entity.RequestGoodsSupplyDetails)
                                detail.SetStatusToDraft();
                        }
            }

            if (entity.Status == GoodsSupplyStatus.Draft && !request.IsDraft)
            {
                entity.SetStatusToCreated();
                foreach (var product in entity.RequestGoodsSupplyProducts)
                    if (GSDSRules.AllowStatusForUpdate.Any(x => x.Equals(product.Status)))
                    {
                        product.SetStatusToNew();
                        foreach (var detail in entity.RequestGoodsSupplyDetails)
                            product.SetStatusToNew();
                    }
            }

            entity.SetOperationInfoSeason(request.OperationInfoSeason);
            entity.SetBuyerId(request.BuyerId);
            entity.SetCurrencyId(request.CurrencyId);
            entity.SetTransferPrice(request.TransferPrice);
            entity.SetOtherPrice(request.OtherPrice);
            entity.SetDiscountOnInvoicePercentage(request.DiscountOnInvoicePercentage);
            entity.SetDiscountOnInvoiceNumber(request.DiscountOnInvoiceNumber);
            entity.SetDiscountedPriceOnInvoice(request.DiscountedPriceOnInvoice);
            entity.SetTaxOnInvoicePercentage(request.TaxOnInvoicePercentage);
            entity.SetTaxOnInvoiceNumber(request.TaxOnInvoiceNumber);
            entity.SetFinalInvoiceAmount(request.FinalInvoiceAmount);
            entity.SetRequestedDate(request.RequestedDate);
            entity.SetIsPettyCash(request.IsPettyCash);
            entity.SetDescription(request.Description);
            entity.SetPurchaseLocation(request.PurchaseLocation);
            entity.SetPurchaseReason(request.PurchaseReason);
            entity.SetConsumptionAddress(request.ConsumptionAddress);
            entity.SetConsumptionRateAndInventoryUrl(request.ConsumptionRateAndInventoryUrl);

            entity.AddHistory();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupply>(SharedErrors.UnknownError);
        }
    }
}
