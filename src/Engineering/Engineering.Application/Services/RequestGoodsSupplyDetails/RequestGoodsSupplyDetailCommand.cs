using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.CreateRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateProjectRequestGoodsSupplyDetail;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails;

public partial class RequestGoodsSupplyDetailLogic
{
    public async Task<Result<RequestGoodsSupplyDetail?>> CreateProjectRequestGoodsSupplyDetailCommand(CreateProjectRequestGoodsSupplyDetailCommand request, CT ct)
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

            var result = await _requestGoodsSupplyDetailRepository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupplyDetail>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<RequestGoodsSupplyDetail?>> UpdateProjectRequestGoodsSupplyDetailCommand(
        RequestGoodsSupplyDetail supply,
        ProjectProduct product,
        RequestGoodsSupplyProduct supplyProduct,
        UpdateProjectRequestGoodsSupplyDetailRequest requestdetail,
        decimal? totalPrice,
        decimal? discountedPrice,
        decimal? finalPrice, CT ct)
    {
        try
        {
            var entity = supply;
            var detail = requestdetail;

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

            var totalRequest = product.RequestGoodsSupplyDetails.Where(x => x.Id != entity.Id && x.Status != pmRejected && x.Status != pmReturned &&
                x.Status != suRejected && x.Status != suReturned && x.Status != mRejected && x.Status != mReturned && x.Status != closed && x.Status != notComplete).Sum(x => x.RequestedCount);

            var tolerancePercentage = product.TolerancePercentage;
            var estimatedCount = ((product.RequestQuantity / 100) * tolerancePercentage) + product.RequestQuantity;
            var requestCount = entity.RequestedCount != detail.RequestedCount ? detail.RequestedCount : entity.RequestedCount;
            if ((totalRequest + requestCount) > Math.Round(estimatedCount!, 2))
                return Result.Failure<RequestGoodsSupplyDetail>(RequestGoodsSupplyDetailErrors.RequestedCount);

            entity.UpdateDetail(new UpdateRGSDetailParameters
            {
                RequestedCount = requestCount,

                UnitPrice = detail.UnitPrice,
                TotalPrice = totalPrice,
                DiscountByNumber = detail.DiscountByNumber,
                DiscountByPercentage = detail.DiscountByPercentage,
                DiscountedPrice = discountedPrice,
                TaxNumber = detail.TaxNumber,
                TaxPercentage = detail.TaxPercentage,
                PackingPrice = detail.PackingPrice,
                FinalPrice = finalPrice,
                DelivaryDeadLine = detail.DelivaryDeadLine,
                DocumentUrls = detail.DocumentUrls,
                Description = detail.Description,
                ManagementDescription = detail.ManagementDescription,
                CheckGroup = detail.CheckGroup,
                ContractorId = detail.ContractorId,
                DestinationWarehouseId = detail.DestinationWarehouseId,
                PackageId = detail.PackageId,
                PackageCount = detail.PackageCount,
                PackageUnitPrice = detail.PackageUnitPrice,
                Importance = detail.Importance,
                CustomerInvoiceNumber = detail.CustomerInvoiceNumber,
                RequestGoodsSupplyProduct = supplyProduct,
                SupplyProduct = supplyProduct,
            });

            await _requestGoodsSupplyDetailRepository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupplyDetail>(SharedErrors.UnknownError);
        }
    }
}
