using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Packages;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSDetails.Create;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProductTypes.Create;

public class CreateRGSProductTypesCommandHandler : ICommandHandler<CreateRGSProductTypesCommand, List<RequestGoodsSupplyType>?>
{
    private readonly IRequestGoodsSupplyTypeRepository _rgsTypeRepository;
    private readonly IMediator _mediator;
    private readonly IProjectProductRepository _projectProductRepo;
    private readonly IViewProductRepository _viewProductRepo;
    private readonly IViewPackageRepository _packageRepo;

    public CreateRGSProductTypesCommandHandler(
        IMediator mediator,
        IProjectProductRepository projectProductRepo,
        IViewProductRepository viewProductRepo,
        IRequestGoodsSupplyTypeRepository rgsTypeRepository,
        IViewPackageRepository packageRepo)
    {
        _mediator = mediator;
        _projectProductRepo = projectProductRepo;
        _viewProductRepo = viewProductRepo;
        _rgsTypeRepository = rgsTypeRepository;
        _packageRepo = packageRepo;
    }

    public async Task<Result<List<RequestGoodsSupplyType>?>> Handle(CreateRGSProductTypesCommand request, CT ct)
    {
        var rgsTypes = new List<RequestGoodsSupplyType>();
        var productIds = request.TypeModels.Listed(x => x.ReferenceId);

        var req = request.TypeModels;

        var products = await _viewProductRepo.GetProductByIds(productIds, ct);
        if (products == null || products.Count == 0)
            return Result.Failure<List<RequestGoodsSupplyType>>(RequestGoodsSupplyErrors.ProdouctNotFound)!;

        var projectProducts = await _projectProductRepo.GetProductByProjectId(request.ProjectId, ct);

        foreach (var item in productIds)
        {
            var detail = req.FirstOrDefault(x => x.ReferenceId == item);
            if (detail == null)
                return Result.Failure<List<RequestGoodsSupplyType>>(RequestGoodsSupplyErrors.ProdouctNotFound)!;

            var requestCount = req
                .Where(x => x.ReferenceId == item)
                .Sum(x => x.RequestedCount);

            var product = products.FirstOrDefault(x => x.Id == item);
            if (product == null)
                return Result.Failure<List<RequestGoodsSupplyType>>(RequestGoodsSupplyErrors.ProdouctNotFound)!;

            var projectProduct = projectProducts?
                .FirstOrDefault(x =>
                x.ProductGroupId == product.Group.Id ||
                x.ProductCategoryId == product.Group.Category.Id);

            if (projectProducts is not null && projectProducts.Count > 0 && projectProduct is null)
                return Result.Failure<List<RequestGoodsSupplyType>>(ProjectErrors.ProjectDoesNotHaveThisProduct)!;

            if (projectProduct is not null)
            {
                var totalRequest = RemaindedCount(projectProduct);
                var tolerancePercentage = projectProduct.TolerancePercentage;
                var estimatedCount =
                    ((projectProduct.RequestQuantity / 100) * tolerancePercentage) +
                    projectProduct.RequestQuantity;

                if ((totalRequest + requestCount) > estimatedCount)
                    return Result.Failure<List<RequestGoodsSupplyType>>(RequestGoodsSupplyErrors.RequestCountMoreThanAssigned)!;
            }

            var packagesQuery = await _packageRepo.GetByGroupId(product.Group.Id, ct);
            if (packagesQuery is null)
                return Result.Failure<List<RequestGoodsSupplyType>>(RequestGoodsSupplyDetailErrors.ProductNoHavePackage)!;

            if (detail.PackageId is not null)
            {
                var packageValidate = packagesQuery
                    .FirstOrDefault(x => x.Id == detail.PackageId);

                if (packageValidate is null)
                    return Result.Failure<List<RequestGoodsSupplyType>>(RequestGoodsSupplyDetailErrors.PackageIdNotValidate)!;
            }
            else
            {
                var defaultPackage = packagesQuery
                    .FirstOrDefault(x => x.IsDefault);

                detail.PackageId = defaultPackage?.Id
                    ?? packagesQuery.FirstOrDefault()?.Id;

                if (detail.PackageId is null)
                    return Result.Failure<List<RequestGoodsSupplyType>>(RequestGoodsSupplyDetailErrors.PackageIdNotFound)!;
            }

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

            var createProduct = new RequestGoodsSupplyType(new CreateRGSTypeParameters
            {
                RequestGoodsSupply = request.Entity,
                Importance = detail.Importance,
                DelivaryDeadLine = detail.DelivaryDeadLine,
                ReferenceId = detail.ReferenceId,
                Urls = detail.DocumentUrls,
                Type = SupplyType.Product,
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

            await _rgsTypeRepository.Create(createProduct, ct);

            rgsTypes.Add(createProduct);
        }

        var createDetailModels = req
            .SelectMany(x => x.Details ?? Enumerable.Empty<CreateRGSTypeDetailProductModel>())
            .Adapt<List<CreateRGSTypeDetailModel>>();
        foreach (var item in createDetailModels)
            item.Type = SupplyType.Product;

        var createDetails = await _mediator.Send(new CreateRGSDetailsCommand(request.Entity, rgsTypes, createDetailModels));
        if (createDetails.IsBad())
            return createDetails.Failure<List<RequestGoodsSupplyType>?>();

        return rgsTypes;
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