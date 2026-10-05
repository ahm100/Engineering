using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.CreateRequestGoodsSupplyProduct;

public class CreateRequestGoodsSupplyProductCommandHandler : ICommandHandler<CreateRequestGoodsSupplyProductCommand, RequestGoodsSupplyProduct>
{
    private readonly ILogger<CreateRequestGoodsSupplyProductCommandHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public CreateRequestGoodsSupplyProductCommandHandler(ILogger<CreateRequestGoodsSupplyProductCommandHandler> logger, IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyProduct?>> Handle(CreateRequestGoodsSupplyProductCommand request, CT ct)
    {
        try
        {
            var prices = CalculatePrices(request.PackageId, request.PackageCount, request.PackageUnitPrice, request.UnitPrice,
                request.RequestedCount, request.DiscountByNumber, request.TaxNumber, request.PackingPrice, request.RequestGoodsSupply.Type);

            var entity = RequestGoodsSupplyProduct.Create(
                request.RequestGoodsSupply,
                request.Importance,
                request.DelivaryDeadLine,
                request.ProductId,
                request.ProductGroupId,
                request.PackageId,
                request.RequestedCount,
                request.UnitPrice,
                prices.TotalPrice,
                request.TaxPercentage,
                request.TaxNumber,
                request.DiscountByPercentage,
                request.DiscountByNumber,
                request.PackingPrice,
                prices.DiscountedPrice,
                null,
                prices.FinalPrice,
                request.PackageCount,
                request.PackageUnitPrice,
                request.CheckGroup,
                request.ContractorId,
                request.DestinationWarehouseId,
                request.CustomerInvoiceNumber,
                request.Description,
                request.ManagementDescription,
                true
                );

            var result = await _repository.Create(entity, ct);
            return result;
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
