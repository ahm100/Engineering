using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;
using Engineering.Application.Services.TelegramChats.TelegramServices;
using Engineering.Application.Services.TelegramChats.TelegramServices.Models;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFileStream;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadMultipleFileStream;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Queries.GetsUserById;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails;

public partial class RequestGoodsSupplyDetailLogic : IRequestGoodsSupplyDetailLogic
{
    private async Task<List<long>?> GetProductIdsForFilter(string? filterProduct, List<long>? productIds, CT ct)
    {
        List<long>? requestProductIds = [];

        requestProductIds.AddRange(productIds ?? []);
        List<long>? filteredProducts = [];
        if (!string.IsNullOrEmpty(filterProduct))
        {
            List<long>? filteredProductIds = [];
            filteredProducts = await WebServicesLogic.FilteredProductsDataReceiver(filterProduct, 0, 0, _mediator, ct);
            if (filteredProducts is not null && filteredProducts.Count > 0)
                filteredProductIds = filteredProducts.Where(x => x > 0).ToList();

            if (filteredProductIds is not null && filteredProductIds.Count > 0)
                requestProductIds.AddRange(filteredProductIds ?? []);
        }

        return requestProductIds.Distinct().ToList();
    }

    private async Task<Result> ValidateRequestGoodsSupplyDetails(List<CreateRequestGoodsSupplyDetailModel> details, CT ct)
    {
        var groupIds = details.Select(x => x.ProductGroupId).Distinct().ToList();
        if (groupIds.Any())
        {
            var groups = await _groupRepo.GetByIds(groupIds, ct);
            if (groupIds.Count != groups?.Count)
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.GroupsNotValid);
        }

        var productIds = details.Select(x => x.ProductId).Distinct().ToList();
        if (productIds.Any())
        {
            var productsData = await _productRepo.GetProductsByIds(productIds, null, null, 0, 0, ct);
            var products = productsData.Data;
            if (productIds.Count != productsData.RowCount)
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.ProductsNotValid);
            if (products is not null && products.Any(x => !x.IsActive))
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.ProductIsInActive);
        }

        return Result.Success();
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

    private CalculatProjectOperationDetailSupplyCounterModel TotalDataReceiver(ConsumableVolumeProduct consumableVolume)
    {
        var commerceT = GoodsSupplyManagementType.Commerce;
        var confirme = GoodsSupplyManagementStatus.CompleteSupply;
        var reject = GoodsSupplyManagementStatus.Return;
        var pmRejected = GoodsSupplyDetailStatus.ProjectManagerRejected;
        var pmReturned = GoodsSupplyDetailStatus.ProjectManagerReturned;
        var suRejected = GoodsSupplyDetailStatus.SupplyUnitRejected;
        var suReturned = GoodsSupplyDetailStatus.SupplyUnitReturned;
        var mRejected = GoodsSupplyDetailStatus.ManagementRejected;
        var mReturned = GoodsSupplyDetailStatus.ManagementReturned;
        var returned = GoodsSupplyDetailStatus.NotCompleteSupply;
        var closed = GoodsSupplyDetailStatus.Closed;

        var details = consumableVolume.RequestGoodsSupplyDetails.Where(x => x.Status != pmRejected && x.Status != pmReturned && x.Status != mRejected && x.Status != mReturned &&
                    x.Status != suRejected && x.Status != suReturned && x.Status != closed && x.Status != returned).ToList();
        var managments = details.Where(d => d.RequestGoodsSupplyProduct is not null && d.RequestGoodsSupplyProduct.RequestGoodsSupplyManagements.Any()).Select(d => d.RequestGoodsSupplyProduct)
            .SelectMany(p => p!.RequestGoodsSupplyManagements.Where(m => m.Status != reject)).ToList();

        decimal commerceDifferenceCount = 0;
        var commerceSupplyCount = managments.Where(x => x.Type == commerceT && x.Status == confirme).Sum(x => x.ConfirmedRequestCount is null ? 0 : x.ConfirmedRequestCount);
        var commerceRequestCount = managments.Where(x => x.Type == commerceT).Sum(x => x.RequestedCount);
        var commercePendingCount = managments.Where(x => x.Type == commerceT && x.Status != confirme).Sum(x => x.RequestedCount);
        if (commerceSupplyCount > 0)
            commerceDifferenceCount = commerceRequestCount - commerceSupplyCount ?? 0;

        var warehouseSupplyCount = managments.Where(x => x.Type != commerceT && x.Status == confirme).Sum(x => x.RequestedCount);
        var warehousePendingCount = managments.Where(x => x.Type != commerceT && x.Status == GoodsSupplyManagementStatus.PendingForConfirme).Sum(x => x.RequestedCount);

        var total = new CalculatProjectOperationDetailSupplyCounterModel
        {
            ConsumableVolumeId = consumableVolume.Id,
            ProjectOperationDetailId = consumableVolume.ProjectOperationDetail.Id,
            ProductGroupId = consumableVolume.ProductGroupId,
            TotalEstimatedCount = Calculator.RoundingDecimalDTFV(consumableVolume.FinalValue),
            TolerancePercentage = Calculator.RoundingDecimalDTFV(consumableVolume.UnusedPercentage),
            ToleranceCount = Calculator.RoundingDecimalDTFV((consumableVolume.FinalValue / 100) * consumableVolume.UnusedPercentage),
            TotalRequestedCount = Calculator.RoundingDecimalDTFV(details.Sum(x => x.RequestedCount)),
            TotalSupplyCount = commerceSupplyCount + warehouseSupplyCount,
            TotalDifferenceCount = commerceDifferenceCount,
        };

        var remainedCount = total.TotalEstimatedCount - (total.TotalRequestedCount - total.TotalDifferenceCount);
        if (remainedCount >= 0)
            total.TotalRemainedCount = remainedCount;
        else
        {
            var toleranceRemained = (total.TotalEstimatedCount + total.ToleranceCount) - (total.TotalRequestedCount - total.TotalDifferenceCount);
            if (toleranceRemained >= 0)
                total.TotalRemainedCount = toleranceRemained;
            else
                total.TotalRemainedCount = 0;
        }

        return total;
    }

    private decimal RemaindedCount(ConsumableVolumeProduct consumableVolume)
    {
        var pmRejected = GoodsSupplyDetailStatus.ProjectManagerRejected;
        var pmReturned = GoodsSupplyDetailStatus.ProjectManagerReturned;
        var suRejected = GoodsSupplyDetailStatus.SupplyUnitRejected;
        var suReturned = GoodsSupplyDetailStatus.SupplyUnitReturned;
        var mRejected = GoodsSupplyDetailStatus.ManagementRejected;
        var mReturned = GoodsSupplyDetailStatus.ManagementReturned;
        var closed = GoodsSupplyDetailStatus.Closed;
        var notComplete = GoodsSupplyDetailStatus.NotCompleteSupply;

        var totalRequest = consumableVolume.RequestGoodsSupplyDetails.Where(x => !x.IsDeleted && x.Status != pmRejected && x.Status != pmReturned &&
            x.Status != suRejected && x.Status != suReturned && x.Status != mRejected && x.Status != mReturned && x.Status != closed && x.Status != notComplete).Sum(x => x.RequestedCount);

        return totalRequest;
    }

    private decimal RemaindedCount(ProjectProduct projectProduct)
    {
        var pmRejected = GoodsSupplyDetailStatus.ProjectManagerRejected;
        var pmReturned = GoodsSupplyDetailStatus.ProjectManagerReturned;
        var suRejected = GoodsSupplyDetailStatus.SupplyUnitRejected;
        var suReturned = GoodsSupplyDetailStatus.SupplyUnitReturned;
        var mRejected = GoodsSupplyDetailStatus.ManagementRejected;
        var mReturned = GoodsSupplyDetailStatus.ManagementReturned;
        var closed = GoodsSupplyDetailStatus.Closed;
        var notComplete = GoodsSupplyDetailStatus.NotCompleteSupply;

        var totalRequest = projectProduct.RequestGoodsSupplyDetails.Where(x => !x.IsDeleted && x.Status != pmRejected && x.Status != pmReturned &&
            x.Status != suRejected && x.Status != suReturned && x.Status != mRejected && x.Status != mReturned && x.Status != closed && x.Status != notComplete).Sum(x => x.RequestedCount);

        return totalRequest;
    }

    private async Task<string> DescriptionMacker(string? requestDescription, GoodsSupplyDetailStatus status, CT ct)
    {
        var getUsers = await _mediator.Send(new GetsUserByIdQuery([_currentUserId]), ct);
        var user = getUsers.Value?.Data?.FirstOrDefault();
        var subSystem = "تامین";
        return $"{subSystem} - {user?.FullName} - {status.GetEnumDescription()} - {requestDescription}";
    }

    private static bool FileDeleter(List<DownloadMultipleFileStreamsModel> files)
    {
        foreach (var file in files)
            if (file is not null && file.Content is not null)
                File.Delete(file.FileName);

        return true;
    }

    private async Task<List<DownloadMultipleFileStreamsModel>?> DownLoadFiles(List<Guid>? urls, CT ct)
    {
        List<DownloadMultipleFileStreamsModel>? files = [];
        var downloadFilesStream = await _mediator.Send(new DownloadMultipleFileStreamQuery(urls!.Where(x => x != Guid.Empty).ToList(), false), ct);
        var receiveFiles = downloadFilesStream.Value?.Files;
        if (receiveFiles != null && receiveFiles.Count > 0)
            files.AddRange(receiveFiles ?? []);

        return files;
    }

    private static string GoodsSupplyStatusChangerMessageModel(GoodsSupplyDetailStatus? status,
        string? requestSerialNumber,
        string? costCenterName,
        string? projectName,
        List<ProductModel>? productModel,
        string? description,
        string? createDate,
        string? createTime,
        string? creator,
        string? creatorTelegramId,
        string? creatorRequest)
    {
        string message = string.Empty;

        message = $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                      $"{TelegramValues.SendBackIcon}<b>تغییر وضعیت درخواست</b>{Environment.NewLine}{Environment.NewLine}" +
                      $"<b>درخواست دهنده:</b> {creatorRequest} {Environment.NewLine}" +
                      $"وضعیت درخواست شما با شماره <b>{requestSerialNumber}</b> به <b>{status?.GetEnumDescription()}</b> تغییر کرد" +
                      $"{Environment.NewLine}<b>مرکز هزینه:</b> {costCenterName} {Environment.NewLine}" +
                      $"<b>پروژه:</b> {projectName} {Environment.NewLine}{Environment.NewLine}" +
                      $"{TelegramValues.Icon13}<b>لیست کالاها:</b>{Environment.NewLine}";

        if (productModel != null && productModel.Any())
        {
            foreach (var product in productModel)
            {
                message += $"<b>نام کالا:</b> {product.ProductName} {Environment.NewLine}" +
                           $"<b>کد:</b> {product.ProductCode} {Environment.NewLine}" +
                           $"<b>تعداد:</b> {product.RequestedCount} {Environment.NewLine}{Environment.NewLine}";
            }
        }

        message += $"<b>علت {status?.GetEnumDescription()}:</b> {description} {Environment.NewLine}" +
                   $"{TelegramValues.CalendarIcon}<b>تاریخ :</b> {createTime} {createDate} {Environment.NewLine}" +
                   $"{TelegramValues.UserIcon}<b>کاربر بررسی کننده درخواست :</b> {creator} {Environment.NewLine}" +
                   $"{creatorTelegramId}";

        return message;
    }

    private async Task<List<long>> GetGoodsManagerThirdPartyIds(
    long productId, CT ct)
    {
        var assignments = await _goodsManagerAssignmentRepo
            .GetActiveByProductId(productId, ct);

        //HTODO
        return assignments.Listed(x => x.OrganizationId);
    }
}