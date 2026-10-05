using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.CreateRequestGoodsSupplyDetail;

public class CreateRequestGoodsSupplyDetailCommandHandler : ICommandHandler<CreateRequestGoodsSupplyDetailCommand, RequestGoodsSupplyDetail>
{
    private readonly ILogger<CreateRequestGoodsSupplyDetailCommandHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public CreateRequestGoodsSupplyDetailCommandHandler(ILogger<CreateRequestGoodsSupplyDetailCommandHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyDetail?>> Handle(CreateRequestGoodsSupplyDetailCommand request, CT ct)
    {
        try
        {
            var detail = request.Detail;

            decimal? packageUnitPrice = null;
            if (detail.UnitPrice != null && detail.PackageId is not null && detail.PackageUnitPrice is null)
                packageUnitPrice = detail.UnitPrice;

            var entity = RequestGoodsSupplyDetail.Create(new CreateRGSDetailParameters
            {
                RequestGoodsSupply = request.RequestGoodsSupply,
                ConsumableVolumeProduct = request.ConsumableVolumeProduct,
                ProjectProduct = request.ProjectProduct,
                RequestGoodsSupplyProduct = request.RequestGoodsSupplyProduct,
                ProductId = detail.ProductId,
                Importance = detail.Importance,
                RequestedCount = Math.Round(detail.RequestedCount, 5),
                UnitPrice = detail.UnitPrice,
                TotalPrice = request.TotalPrice,
                DiscountByNumber = detail.DiscountByNumber,
                DiscountByPercentage = detail.DiscountByPercentage,
                DiscountedPrice = request.DiscountedPrice,
                TaxNumber = detail.TaxNumber,
                TaxPercentage = detail.TaxPercentage,
                PackingPrice = detail.PackingPrice,
                FinalPrice = request.FinalPrice,
                DelivaryDeadLine = detail.DelivaryDeadLine,
                DocumentUrls = detail.DocumentUrls,
                Description = detail.Description,
                ManagementDescription = detail.ManagementDescription,
                CheckGroup = detail.CheckGroup,
                ContractorId = detail.ContractorId,
                PackageId = detail.PackageId,
                PackageCount = detail.PackageCount,
                PackageUnitPrice = packageUnitPrice,
                DestinationWarehouseId = detail.DestinationWarehouseId,
                CustomerInvoiceNumber = detail.CustomerInvoiceNumber,

                LastDescription = null
            });

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupplyDetail>(SharedErrors.UnknownError);
        }
    }
}
