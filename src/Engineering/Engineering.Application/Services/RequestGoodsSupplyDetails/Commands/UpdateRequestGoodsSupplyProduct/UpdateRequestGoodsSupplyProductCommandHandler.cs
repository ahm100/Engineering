using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.UpdateRequestGoodsSupplyProduct;

public class UpdateRequestGoodsSupplyProductCommandHandler : ICommandHandler<UpdateRequestGoodsSupplyProductCommand, RequestGoodsSupplyProduct>
{
    private readonly ILogger<UpdateRequestGoodsSupplyProductCommandHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public UpdateRequestGoodsSupplyProductCommandHandler(
        ILogger<UpdateRequestGoodsSupplyProductCommandHandler> logger,
        IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyProduct?>> Handle(UpdateRequestGoodsSupplyProductCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            var details = request.Entity.RequestGoodsSupplyDetails.ToList();

            entity.SetContractorId(details.FirstOrDefault()?.ContractorId);

            entity.SetDescription(details.FirstOrDefault()?.Description);

            entity.SetManagementDescription(details.FirstOrDefault()?.ManagementDescription);

            entity.SetRequestedCount(details.Sum(x => x.RequestedCount));

            entity.SetPackageCount(details.Sum(x => x.PackageCount));

            var unit = details.Sum(x => x.UnitPrice) / details.Count();
            entity.SetUnitPrice(unit);

            var taxPercentage = details.Sum(x => x.TaxPercentage) / details.Count();
            entity.SetTaxPercentage(taxPercentage);

            var taxNumber = details.Sum(x => x.TaxNumber) / details.Count();
            entity.SetTaxNumber(taxNumber);

            var discountByPercentage = details.Sum(x => x.DiscountByPercentage) / details.Count();
            entity.SetDiscountByPercentage(discountByPercentage);

            var discountByNumber = details.Sum(x => x.DiscountByNumber) / details.Count();
            entity.SetDiscountByNumber(discountByNumber);

            var packageUnitPrice = details.Sum(x => x.PackageUnitPrice) / details.Count();
            entity.SetPackageUnitPrice(packageUnitPrice);

            var packingPrice = details.Sum(x => x.PackingPrice) / details.Count();
            entity.SetPackingPrice(packingPrice);

            var prices = CalculatePrices(entity.PackageId, entity.PackageCount, entity.PackageUnitPrice, entity.UnitPrice,
                entity.RequestedCount, entity.DiscountByNumber, entity.TaxNumber, entity.PackingPrice, entity.RequestGoodsSupply.Type);

            entity.SetTotalPrice(prices.TotalPrice);
            entity.SetDiscountedPrice(prices.DiscountedPrice);
            entity.SetFinalPrice(prices.FinalPrice);
            entity.SetCustomerInvoiceNumber(request.CustomerInvoiceNumber);

            if (GSDSRules.AllowStatusForUpdate.Any(x => x.Equals(entity.Status)))
            {
                if (entity.Status == GoodsSupplyDetailStatus.New)
                {
                    entity.SetStatus(GoodsSupplyDetailStatus.New, entity.LastDescription);
                    entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                    {
                        if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                            oo.UpdateStatus(GoodsSupplyDetailStatus.New, entity.LastDescription);
                    });
                }

                if (entity.Status == GoodsSupplyDetailStatus.Draft)
                {
                    entity.SetStatus(GoodsSupplyDetailStatus.Draft, entity.LastDescription);
                    entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                    {
                        if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                            oo.UpdateStatus(GoodsSupplyDetailStatus.Draft, entity.LastDescription);
                    });
                }

                if (entity.Status != GoodsSupplyDetailStatus.New && entity.Status != GoodsSupplyDetailStatus.Draft)
                {
                    entity.SetStatus(GoodsSupplyDetailStatus.ProjectManagerResend, entity.LastDescription);
                    entity.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                    {
                        if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                            oo.UpdateStatus(GoodsSupplyDetailStatus.ProjectManagerResend, entity.LastDescription);
                    });
                }
            }

            if (!entity.IsHistoryAdded)
            {
                entity.AddHistory();
                entity.IsHistoryAdded = true;
            }

            if (entity.Id > 0)
                await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupplyProduct>(SharedErrors.UnknownError);
        }
    }

    private (decimal? DiscountedPrice, decimal? FinalPrice, decimal? TotalPrice) CalculatePrices(
        long? packageId,
        decimal? packageCount,
        decimal? packageUnitPrice,
        decimal? unitPrice,
        decimal requestedCount,
        decimal? discountByNumber,
        decimal? taxNumber,
        decimal? packingPrice,
        GoodsSupplyType type)
    {
        decimal? discountedPrice = null;
        decimal? finalPrice = null;
        decimal? totalPrice = 0;

        if (packageId is not null)
            totalPrice = packageCount * packageUnitPrice;
        else
            totalPrice = unitPrice * requestedCount;

        if (type == GoodsSupplyType.Project || type == GoodsSupplyType.Contractor)
        {
            discountedPrice = totalPrice - (discountByNumber ?? 0);
            finalPrice = (discountedPrice ?? 0) + (taxNumber ?? 0) + (packingPrice ?? 0);
        }

        return (discountedPrice, finalPrice, totalPrice);
    }
}
