using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSDetails.Create;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProjectTypes.Create;

public class CreateRGSProjectTypesCommandHandler : ICommandHandler<CreateRGSProjectTypesCommand, List<RequestGoodsSupplyType>?>
{
    private readonly IRequestGoodsSupplyTypeRepository _rgsTypeRepository;
    private readonly IMediator _mediator;

    public CreateRGSProjectTypesCommandHandler(IMediator mediator,
        IRequestGoodsSupplyTypeRepository rgsTypeRepository)
    {
        _mediator = mediator;
        _rgsTypeRepository = rgsTypeRepository;
    }

    public async Task<Result<List<RequestGoodsSupplyType>?>> Handle(CreateRGSProjectTypesCommand request, CT ct)
    {
        var rgsTypes = new List<RequestGoodsSupplyType>();

        var req = request.TypeModels;

        foreach (var detail in req)
        {
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
                detail.RequestedCount,
                detail.PackingPrice,
                request.Entity.Type);

            var createProject = new RequestGoodsSupplyType(new CreateRGSTypeParameters
            {
                RequestGoodsSupply = request.Entity,
                Importance = detail.Importance,
                DelivaryDeadLine = detail.DelivaryDeadLine,
                Urls = detail.DocumentUrls,
                ProjectName = detail.ProjectName,
                ProjectEnName = detail.ProjectEnName,
                ProjectCode = detail.ProjectCode,
                Type = SupplyType.Project,
                PackageId = detail.PackageId,
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

            await _rgsTypeRepository.Create(createProject, ct);

            rgsTypes.Add(createProject);
        }

        var createDetailModels = req
            .SelectMany(x => x.Details ?? Enumerable.Empty<CreateRGSTypeDetailProjectModel>())
            .Adapt<List<CreateRGSTypeDetailModel>>();
        foreach (var item in createDetailModels)
            item.Type = SupplyType.Project;

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