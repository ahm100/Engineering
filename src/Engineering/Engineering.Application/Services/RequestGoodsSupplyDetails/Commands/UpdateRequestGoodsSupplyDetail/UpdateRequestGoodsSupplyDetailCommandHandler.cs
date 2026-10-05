using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.UpdateRequestGoodsSupplyDetail;

public class UpdateRequestGoodsSupplyDetailCommandHandler : ICommandHandler<UpdateRequestGoodsSupplyDetailCommand, RequestGoodsSupplyDetail>
{
    private readonly ILogger<UpdateRequestGoodsSupplyDetailCommandHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public UpdateRequestGoodsSupplyDetailCommandHandler(
        ILogger<UpdateRequestGoodsSupplyDetailCommandHandler> logger,
        IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyDetail?>> Handle(UpdateRequestGoodsSupplyDetailCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            var detail = request.Detail;

            decimal? packageUnitPrice = null;
            if (detail.UnitPrice != null && detail.PackageId is not null && detail.PackageUnitPrice is null)
                packageUnitPrice = detail.UnitPrice;


            var pmRejected = GoodsSupplyDetailStatus.ProjectManagerRejected;
            var pmReturned = GoodsSupplyDetailStatus.ProjectManagerReturned;
            var suRejected = GoodsSupplyDetailStatus.SupplyUnitRejected;
            var suReturned = GoodsSupplyDetailStatus.SupplyUnitReturned;
            var mRejected = GoodsSupplyDetailStatus.ManagementRejected;
            var mReturned = GoodsSupplyDetailStatus.ManagementReturned;
            var closed = GoodsSupplyDetailStatus.Closed;
            var notComplete = GoodsSupplyDetailStatus.NotCompleteSupply;

            var totalRequest = request.VolumeProduct.RequestGoodsSupplyDetails.Where(x => x.Id != entity.Id && x.Status != pmRejected && x.Status != pmReturned &&
                x.Status != suRejected && x.Status != suReturned && x.Status != mRejected && x.Status != mReturned && x.Status != closed && x.Status != notComplete).Sum(x => x.RequestedCount);

            var tolerancePercentage = request.VolumeProduct.UnusedPercentage;
            var estimatedCount = ((request.VolumeProduct.FinalValue / 100) * tolerancePercentage) + request.VolumeProduct.FinalValue;
            var requestCount = entity.RequestedCount != detail.RequestedCount ? detail.RequestedCount : entity.RequestedCount;
            if ((totalRequest + requestCount) > Math.Round(estimatedCount!.Value, 2))
                return Result.Failure<RequestGoodsSupplyDetail>(RequestGoodsSupplyDetailErrors.RequestedCount);

            entity.SetRequestedCount(Math.Round(requestCount, 3));
            entity.SetUnitPrice(detail.UnitPrice);
            entity.SetTotalPrice(request.TotalPrice);
            entity.SetDiscountByNumber(detail.DiscountByNumber);
            entity.SetDiscountByPercentage(detail.DiscountByPercentage);
            entity.SetDiscountedPrice(request.DiscountedPrice);
            entity.SetTaxNumber(detail.TaxNumber);
            entity.SetTaxPercentage(detail.TaxPercentage);
            entity.SetFinalPrice(request.FinalPrice);
            entity.SetDescription(detail.Description);
            entity.SetManagementDescription(detail.ManagementDescription);
            entity.UpdateCheckGroup(detail.CheckGroup);
            entity.SetContractorId(detail.ContractorId);
            entity.SetDestinationWarehouseId(detail.DestinationWarehouseId);
            entity.SetPackageId(detail.PackageId);
            entity.SetPackageCount(detail.PackageCount);
            entity.SetPackageUnitPrice(detail.PackageUnitPrice);
            entity.SetDelivaryDeadLine(detail.DelivaryDeadLine);
            entity.SetImportance(detail.Importance);
            entity.SetPackingPrice(detail.PackingPrice);
            entity.AddDocuments(detail.DocumentUrls);
            entity.SetCustomerInvoiceNumber(detail.CustomerInvoiceNumber);

            entity.SetProduct(request.Product);

            entity.AddHistory();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupplyDetail>(SharedErrors.UnknownError);
        }
    }
}
