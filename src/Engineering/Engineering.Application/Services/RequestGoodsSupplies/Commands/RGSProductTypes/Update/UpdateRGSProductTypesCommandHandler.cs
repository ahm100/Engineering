using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSDetails.Create;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSDetails.Update;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProductTypes.Update;

public class UpdateRGSProductTypesCommandHandler : ICommandHandler<UpdateRGSProductTypesCommand, List<RequestGoodsSupplyType>?>
{
    private readonly IRequestGoodsSupplyTypeRepository _repository;
    private readonly IMediator _mediator;

    public UpdateRGSProductTypesCommandHandler(
        IRequestGoodsSupplyTypeRepository repository,
        IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Result<List<RequestGoodsSupplyType>?>> Handle(UpdateRGSProductTypesCommand request, CT ct)
    {
        var rgs = request.Entity;

        var models = request.ProductModels;

        var types = rgs.RequestGoodsSupplyTypes.ToList();

        foreach (var item in models)
        {
            var type = types.FirstOrDefault(x => x.Id == item.RequestGoodsSupplyTypeId);

            decimal? packageUnitPrice = null;

            if (item.UnitPrice != null &&
                item.PackageId != null &&
                item.PackageUnitPrice == null)
                packageUnitPrice = item.UnitPrice;

            var prices = CalculatePrices(
                item.PackageId,
                item.PackageCount,
                item.PackageUnitPrice,
                item.UnitPrice,
                item.RequestedCount,
                item.PackingPrice,
                request.Entity.Type);

            type.Update(new UpdateRGSTypeParameters
            {
                Importance = item.Importance,
                DelivaryDeadLine = item.DelivaryDeadLine,
                ReferenceId = item.ReferenceId,
                PackageId = item.PackageId,
                RequestedCount = item.RequestedCount,
                TotalPrice = item.TotalPrice,
                Urls = item.DocumentUrls,
                PackingPrice = item.PackingPrice,
                FinalPrice = prices.FinalPrice,
                PackageCount = item.PackageCount,
                PackageUnitPrice = item.PackageUnitPrice,
                CheckGroup = item.CheckGroup,
                ContractorId = item.ContractorId,
                Description = item.Description,
                ManagementDescription = item.ManagementDescription
            });

            await _repository.Update(type);
        }

        if (models.Any(x => x.UpdateDetails.HasAny()))
        {
            var updateDetailModels = models
                .SelectMany(x => x.UpdateDetails ?? Enumerable.Empty<UpdateRGSTypeDetailProductModel>())
                .Adapt<List<UpdateRGSTypeDetailModel>>();

            foreach (var item in updateDetailModels)
                item.Type = SupplyType.Product;

            var update = await _mediator.Send(new UpdateRGSDetailsCommand(rgs, updateDetailModels), ct);
            if (update.IsBad())
                return update.Failure<List<RequestGoodsSupplyType>?>();
        }

        if (models.Any(x => x.CreateDetails.HasAny()))
        {
            var createDetailModels = models
                .SelectMany(x => x.CreateDetails ?? Enumerable.Empty<CreateRGSTypeDetailProductModel>())
                .Adapt<List<CreateRGSTypeDetailModel>>();
            foreach (var item in createDetailModels)
                item.Type = SupplyType.Product;

            var create = await _mediator.Send(new CreateRGSDetailsCommand(rgs, rgs.RequestGoodsSupplyTypes.ToList(), createDetailModels), ct);
            if (create.IsBad())
                return create.Failure<List<RequestGoodsSupplyType>?>();
        }

        return types;
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