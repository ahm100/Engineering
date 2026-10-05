using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Packages;
using Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSDetails.Update;

public class UpdateRGSDetailsCommandHandler : ICommandHandler<UpdateRGSDetailsCommand, bool?>
{
    private readonly IRequestGoodsSupplyTypeDetailRepository _repository;
    private readonly IMediator _mediator;
    private readonly ICostCenterRepository _ccRepo;
    private readonly IProjectProductRepository _projectProductRepo;
    private readonly IViewPackageRepository _packageRepo;

    public UpdateRGSDetailsCommandHandler(IMediator mediator,
        IRequestGoodsSupplyTypeDetailRepository repository,
        ICostCenterRepository ccRepo,
        IProjectProductRepository projectProductRepo,
        IViewPackageRepository packageRepo)
    {
        _mediator = mediator;
        _repository = repository;
        _ccRepo = ccRepo;
        _projectProductRepo = projectProductRepo;
        _packageRepo = packageRepo;
    }

    public async Task<Result<bool?>> Handle(UpdateRGSDetailsCommand request, CT ct)
    {
        var details = request.Entity.RequestGoodsSupplyTypeDetails;
        var ccIds = request.Models.NullListed(x => x.CostCenterId);
        var ccs = await _ccRepo.GetByIdsIncludeType(ccIds, ct);

        foreach (var item in request.Models)
        {
            var detail = details.FirstOrDefault(x => x.Id == item.RGSTypeDetailId);
            var cc = ccs?.FirstOrDefault(x => x.Id == item.CostCenterId);
            var type = request.Entity.RequestGoodsSupplyTypes
                .FirstOrDefault(x => x.RequestGoodsSupplyTypeDetails.Any(x => x.Id == item.RGSTypeDetailId));

            if (detail is null || (detail.Type != SupplyType.Project && type is null))
                continue;

            Result<bool?> result = item.Type switch
            {
                SupplyType.Product => await UpdateProductDetail(
                    detail,
                    item,
                    type,
                    cc,
                    request.Entity.Type,
                    ct),

                SupplyType.Service => await UpdateServiceDetail(
                    detail,
                    item,
                    type,
                    cc,
                    request.Entity.Type,
                    ct),

                SupplyType.Ads => await UpdateAdsDetail(
                    detail,
                    item,
                    type,
                    cc,
                    request.Entity.Type,
                    ct),

                SupplyType.Project => await UpdateProjectDetail(
                    detail,
                    item,
                    type,
                    cc,
                    request.Entity.Type,
                    ct),

                _ => true
            };

            if (result.IsBad())
                return result;
        }

        return true;
    }

    private async Task<Result<bool?>> UpdateProductDetail(
    RequestGoodsSupplyTypeDetail detail,
    UpdateRGSTypeDetailModel item,
    RequestGoodsSupplyType type,
    CostCenter? cc,
    GoodsSupplyType supplyType,
    CT ct)
    {
        if (detail.ProjectProductId is not null)
        {
            var pp = await _projectProductRepo.GetById(detail.ProjectProductId.Value, ct);

            var sum = pp.RequestGoodsSupplyTypeDetails
                .Where(x => !x.IsDeleted)
                .Sum(x => x.RequestedCount);

            if (pp is not null && pp.RequestQuantity < sum)
                return Result.Failure<bool?>(RequestGoodsSupplyErrors.RequestCountMoreThanAssigned);
        }

        await _repository.Create(detail, ct);

        var prices = CalculatePrices(
            item.PackageId,
            item.PackageCount,
            item.PackageUnitPrice,
            item.UnitPrice,
            item.RequestedCount,
            item.PackingPrice,
            supplyType);

        detail.Update(new UpdateRGSTypeDetailParameters
        {
            Importance = item.Importance,
            UnitPrice = item.UnitPrice,
            TotalPrice = item.TotalPrice,
            PackingPrice = item.PackingPrice,
            RequestedCount = item.RequestedCount,
            FinalPrice = prices.FinalPrice,
            DelivaryDeadLine = item.DelivaryDeadLine,
            DocumentUrls = item.DocumentUrls,
            Description = item.Description,
            ManagementDescription = item.ManagementDescription,
            CheckGroup = item.CheckGroup,
            ContractorId = item.ContractorId,
            PackageId = item.PackageId,
            PackageCount = item.PackageCount,
            PackageUnitPrice = item.PackageUnitPrice,
            CostCenter = cc
        });

        type.SetRequestedCount(
            type.RequestGoodsSupplyTypeDetails
                .Where(x => !x.IsDeleted)
                .Sum(x => x.RequestedCount));

        await _repository.Update(detail);

        return true;
    }

    private async Task<Result<bool?>> UpdateServiceDetail(
        RequestGoodsSupplyTypeDetail detail,
        UpdateRGSTypeDetailModel item,
        RequestGoodsSupplyType type,
        CostCenter? cc,
        GoodsSupplyType supplyType,
        CT ct)
    {
        var prices = CalculatePrices(
            item.PackageId,
            item.PackageCount,
            item.PackageUnitPrice,
            item.UnitPrice,
            item.RequestedCount,
            item.PackingPrice,
            supplyType);

        detail.Update(new UpdateRGSTypeDetailParameters
        {
            Importance = item.Importance,
            UnitPrice = item.UnitPrice,
            TotalPrice = item.TotalPrice,
            PackingPrice = item.PackingPrice,
            RequestedCount = item.RequestedCount,
            FinalPrice = prices.FinalPrice,
            DelivaryDeadLine = item.DelivaryDeadLine,
            DocumentUrls = item.DocumentUrls,
            Description = item.Description,
            ManagementDescription = item.ManagementDescription,
            CheckGroup = item.CheckGroup,
            ContractorId = item.ContractorId,
            PackageId = item.PackageId,
            PackageCount = item.PackageCount,
            PackageUnitPrice = item.PackageUnitPrice,
            CostCenter = cc
        });

        type.SetRequestedCount(
            type.RequestGoodsSupplyTypeDetails
                .Where(x => !x.IsDeleted)
                .Sum(x => x.RequestedCount));

        await _repository.Update(detail);

        return true;
    }

    private async Task<Result<bool?>> UpdateAdsDetail(
        RequestGoodsSupplyTypeDetail detail,
        UpdateRGSTypeDetailModel item,
        RequestGoodsSupplyType type,
        CostCenter? cc,
        GoodsSupplyType supplyType,
        CT ct)
    {
        var prices = CalculatePrices(
            item.PackageId,
            item.PackageCount,
            item.PackageUnitPrice,
            item.UnitPrice,
            item.RequestedCount,
            item.PackingPrice,
            supplyType);

        detail.Update(new UpdateRGSTypeDetailParameters
        {
            Importance = item.Importance,
            UnitPrice = item.UnitPrice,
            TotalPrice = item.TotalPrice,
            PackingPrice = item.PackingPrice,
            RequestedCount = item.RequestedCount,
            FinalPrice = prices.FinalPrice,
            DelivaryDeadLine = item.DelivaryDeadLine,
            DocumentUrls = item.DocumentUrls,
            Description = item.Description,
            ManagementDescription = item.ManagementDescription,
            CheckGroup = item.CheckGroup,
            ContractorId = item.ContractorId,
            PackageId = item.PackageId,
            PackageCount = item.PackageCount,
            PackageUnitPrice = item.PackageUnitPrice,
            CostCenter = cc
        });

        type.SetRequestedCount(
            type.RequestGoodsSupplyTypeDetails
                .Where(x => !x.IsDeleted)
                .Sum(x => x.RequestedCount));

        await _repository.Update(detail);

        return true;
    }

    private async Task<Result<bool?>> UpdateProjectDetail(
        RequestGoodsSupplyTypeDetail detail,
        UpdateRGSTypeDetailModel item,
        RequestGoodsSupplyType type,
        CostCenter? cc,
        GoodsSupplyType supplyType,
        CT ct)
    {
        var prices = CalculatePrices(
            item.PackageId,
            item.PackageCount,
            item.PackageUnitPrice,
            item.UnitPrice,
            item.RequestedCount,
            item.PackingPrice,
            supplyType);

        detail.Update(new UpdateRGSTypeDetailParameters
        {
            Importance = item.Importance,
            UnitPrice = item.UnitPrice,
            TotalPrice = item.TotalPrice,
            PackingPrice = item.PackingPrice,
            RequestedCount = item.RequestedCount,
            FinalPrice = prices.FinalPrice,
            DelivaryDeadLine = item.DelivaryDeadLine,
            DocumentUrls = item.DocumentUrls,
            Description = item.Description,
            ManagementDescription = item.ManagementDescription,
            CheckGroup = item.CheckGroup,
            ContractorId = item.ContractorId,
            PackageId = item.PackageId,
            PackageCount = item.PackageCount,
            PackageUnitPrice = item.PackageUnitPrice,
            CostCenter = cc
        });

        type.SetRequestedCount(
            type.RequestGoodsSupplyTypeDetails
                .Where(x => !x.IsDeleted)
                .Sum(x => x.RequestedCount));

        await _repository.Update(detail);

        return true;
    }

    private (decimal? DiscountedPrice, decimal? FinalPrice, decimal? TotalPrice) CalculatePrices(
        long? packageId,
        decimal? packageCount,
        decimal? packageUnitPrice,
        decimal? unitPrice,
        decimal requestedCount,
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
            discountedPrice = totalPrice;
            finalPrice = (discountedPrice ?? 0) + (packingPrice ?? 0);
        }

        return (discountedPrice, finalPrice, totalPrice);
    }
}