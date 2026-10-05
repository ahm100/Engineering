using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSDetails.Create;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Application.Services.ServiceInfos.Queries.GetsByIds;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSServiceTypes.Create;

public class CreateRGSServiceTypesCommandHandler : ICommandHandler<CreateRGSServiceTypesCommand, List<RequestGoodsSupplyType>?>
{
    private readonly IRequestGoodsSupplyTypeRepository _rgsTypeRepository;
    private readonly IMediator _mediator;

    public CreateRGSServiceTypesCommandHandler(IMediator mediator,
        IRequestGoodsSupplyTypeRepository rgsTypeRepository)
    {
        _mediator = mediator;
        _rgsTypeRepository = rgsTypeRepository;
    }

    public async Task<Result<List<RequestGoodsSupplyType>?>> Handle(CreateRGSServiceTypesCommand request, CT ct)
    {
        var rgsTypes = new List<RequestGoodsSupplyType>();
        var serviceIds = request.TypeModels.Listed(x => x.ReferenceId);

        var req = request.TypeModels;

        var services = await _mediator.Send(new GetsServiceInfoByIdsQuery(1, serviceIds.Count, serviceIds), ct);
        if (services.IsBad())
            return services.Failure<List<RequestGoodsSupplyType>>()!;

        foreach (var item in serviceIds)
        {
            var detail = req.FirstOrDefault(x => x.ReferenceId == item);
            if (detail == null)
                return Result.Failure<List<RequestGoodsSupplyType>>(RequestGoodsSupplyErrors.ProdouctNotFound)!;

            var requestCount = req
                .Where(x => x.ReferenceId == item)
                .Sum(x => x.RequestedCount);

            var service = services.Value.Data!.FirstOrDefault(x => x.Id == item);
            if (service is null)
                return Result.Failure<List<RequestGoodsSupplyType>>(RequestGoodsSupplyErrors.ProdouctNotFound)!;

            //var packagesQuery = await _mediator.Send(new GetPackageByProductIdQuery(detail.ReferenceId, null, 1, 10),
            //    ct);

            //if (packagesQuery?.Value?.Data == null)
            //    return Result.Failure<List<RequestGoodsSupplyType>>(RequestGoodsSupplyDetailErrors.ProductNoHavePackage)!;

            //if (detail.PackageId is not null)
            //{
            //    var packageValidate = packagesQuery.Value.Data
            //        .FirstOrDefault(x => x.Id == detail.PackageId);

            //    if (packageValidate is null)
            //        return Result.Failure<List<RequestGoodsSupplyType>>(RequestGoodsSupplyDetailErrors.PackageIdNotValidate)!;
            //}
            //else
            //{
            //    var defaultPackage = packagesQuery.Value.Data
            //        .FirstOrDefault(x => x.IsDefault);

            //    detail.PackageId = defaultPackage?.Id
            //        ?? packagesQuery.Value.Data.FirstOrDefault()?.Id;

            //    if (detail.PackageId is null)
            //        return Result.Failure<List<RequestGoodsSupplyType>>(RequestGoodsSupplyDetailErrors.PackageIdNotFound)!;
            //}

            decimal? packageUnitPrice = null;

            if (detail.UnitPrice != null &&
                detail.PackageId != null &&
                detail.PackageUnitPrice == null)
                packageUnitPrice = detail.UnitPrice;

            var prices = CalculatePrices(
                detail.PackageId,
                detail.PackageCount,
                detail.PackageUnitPrice,
                detail.UnitPrice,
                requestCount,
                detail.PackingPrice,
                request.Entity.Type);

            var createService = new RequestGoodsSupplyType(new CreateRGSTypeParameters
            {
                RequestGoodsSupply = request.Entity,
                Importance = detail.Importance,
                DelivaryDeadLine = detail.DelivaryDeadLine,
                ReferenceId = detail.ReferenceId,
                Urls = detail.DocumentUrls,
                Type = SupplyType.Service,
                PackageId = detail.PackageId,
                RequestedCount = requestCount,
                UnitPrice = detail.UnitPrice,
                TotalPrice = detail.TotalPrice,
                DiscountedPrice = prices.DiscountedPrice,
                TransferPrice = null,
                PackingPrice = detail.PackingPrice,
                FinalPrice = prices.FinalPrice,
                PackageCount = detail.PackageCount,
                PackageUnitPrice = detail.PackageUnitPrice,
                CheckGroup = detail.CheckGroup,
                ContractorId = detail.ContractorId,
                Description = detail.Description,
                ManagementDescription = detail.ManagementDescription,
                IsHistoryAdded = true
            });

            await _rgsTypeRepository.Create(createService, ct);

            rgsTypes.Add(createService);
        }

        var createDetailModels = req
            .SelectMany(x => x.Details ?? Enumerable.Empty<CreateRGSTypeDetailServiceModel>())
            .Adapt<List<CreateRGSTypeDetailModel>>();
        foreach (var item in createDetailModels)
            item.Type = SupplyType.Service;

        var createDetails = await _mediator.Send(new CreateRGSDetailsCommand(request.Entity, rgsTypes, createDetailModels));
        if (createDetails.IsBad())
            return createDetails.Failure<List<RequestGoodsSupplyType>?>();

        return rgsTypes;
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