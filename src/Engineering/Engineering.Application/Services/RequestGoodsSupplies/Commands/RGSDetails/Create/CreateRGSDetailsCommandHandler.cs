using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Packages;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSDetails.Create;

public class CreateRGSDetailsCommandHandler : ICommandHandler<CreateRGSDetailsCommand, List<RequestGoodsSupplyTypeDetail>?>
{
    private readonly IRequestGoodsSupplyTypeDetailRepository _repository;
    private readonly IMediator _mediator;
    private readonly IProjectProductRepository _projectProductRepo;
    private readonly IViewProductRepository _viewProductRepo;
    private readonly ICostCenterRepository _ccRepo;
    private readonly IViewPackageRepository _packageRepo;

    public CreateRGSDetailsCommandHandler(IMediator mediator,
        IProjectProductRepository projectProductRepo,
        IViewProductRepository viewProductRepo,
        IRequestGoodsSupplyTypeDetailRepository repository,
        ICostCenterRepository ccRepo,
        IViewPackageRepository packageRepo)
    {
        _mediator = mediator;
        _projectProductRepo = projectProductRepo;
        _viewProductRepo = viewProductRepo;
        _repository = repository;
        _ccRepo = ccRepo;
        _packageRepo = packageRepo;
    }

    public async Task<Result<List<RequestGoodsSupplyTypeDetail>?>> Handle(
        CreateRGSDetailsCommand request, CT ct)
    {
        if (request.DetailModels.FirstOrDefault()!.Type == SupplyType.Product)
        {
            var create = await CreateProductDetail(request.Entity, request.Types, request.DetailModels, ct);
            if (create.IsBad())
                return create.Failure<List<RequestGoodsSupplyTypeDetail>?>();

            return create.Value;
        }

        else if (request.DetailModels.FirstOrDefault()!.Type == SupplyType.Service)
        {
            var create = await CreateServiceDetail(request.Entity, request.Types, request.DetailModels, ct);
            if (create.IsBad())
                return create.Failure<List<RequestGoodsSupplyTypeDetail>?>();

            return create.Value;
        }

        else if (request.DetailModels.FirstOrDefault()!.Type == SupplyType.Ads)
        {
            var create = await CreateAdDetail(request.Entity, request.Types, request.DetailModels, ct);
            if (create.IsBad())
                return create.Failure<List<RequestGoodsSupplyTypeDetail>?>();

            return create.Value;
        }

        else if (request.DetailModels.FirstOrDefault()!.Type == SupplyType.Project)
        {
            var create = await CreateProjectDetail(request.Entity, request.Types, request.DetailModels, ct);
            if (create.IsBad())
                return create.Failure<List<RequestGoodsSupplyTypeDetail>?>();

            return create.Value;
        }

        return Result.Failure<List<RequestGoodsSupplyTypeDetail>?>(SharedErrors.UnknownError);
    }

    public async Task<Result<List<RequestGoodsSupplyTypeDetail>?>> CreateProductDetail(
        RequestGoodsSupply entity,
        List<RequestGoodsSupplyType> types,
        List<CreateRGSTypeDetailModel> detailModels, CT ct)
    {
        List<RequestGoodsSupplyTypeDetail>? details = [];
        var projectProducts = await _projectProductRepo.GetProductByProjectId(entity.ProjectId.Value, ct);
        var costCenters = await _ccRepo.GetByIdsIncludeType(detailModels.NullListed(x => x.CostCenterId), ct);
        var products = await _viewProductRepo.GetProductByIds(detailModels.NullListed(x => x.ReferenceId), ct);

        foreach (var item in detailModels)
        {
            var product = products?.FirstOrDefault(x => x.Id == item.ReferenceId);
            var projectProduct = projectProducts?.FirstOrDefault(x => x.Id == item.ReferenceId);

            var packagesQuery = await _packageRepo.GetByGroupId(product!.Group.Id, ct);
            if (packagesQuery is null)
                return Result.Failure<List<RequestGoodsSupplyTypeDetail>?>(RequestGoodsSupplyDetailErrors.ProductNoHavePackage)!;

            if (item.PackageId is not null)
            {
                var packageValidate = packagesQuery
                    .FirstOrDefault(x => x.Id == item.PackageId);

                if (packageValidate is null)
                    return Result.Failure<List<RequestGoodsSupplyTypeDetail>?>(RequestGoodsSupplyDetailErrors.PackageIdNotValidate)!;
            }
            else
            {
                var defaultPackage = packagesQuery
                    .FirstOrDefault(x => x.IsDefault);

                item.PackageId = defaultPackage?.Id
                    ?? packagesQuery.FirstOrDefault()?.Id;

                if (item.PackageId is null)
                    return Result.Failure<List<RequestGoodsSupplyTypeDetail>?>(RequestGoodsSupplyDetailErrors.PackageIdNotFound)!;
            }

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
                entity.Type);

            var type = types.FirstOrDefault(x => x.ReferenceId == item.ReferenceId && x.Type == SupplyType.Product);
            var cc = costCenters?.FirstOrDefault(x => x.Id == item.CostCenterId);

            var detail = RequestGoodsSupplyTypeDetail.Create(new CreateRGSTypeDetailParameters
            {
                RequestGoodsSupply = entity,
                ProjectProduct = projectProduct,
                RequestGoodsSupplyType = type,
                CostCenter = cc,
                ReferenceId = item.ReferenceId.Value,
                Type = SupplyType.Product,
                Importance = item.Importance,
                RequestedCount = item.RequestedCount,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice,
                PackageCount = item.PackageCount,
                PackageUnitPrice = packageUnitPrice,
                PackingPrice = item.PackingPrice,
                DelivaryDeadLine = item.DelivaryDeadLine,
                DocumentUrls = item.DocumentUrls,
                Description = item.Description,
                DescriptionEn = item.DescriptionEn,
                ManagementDescription = item.ManagementDescription,
                CheckGroup = item.CheckGroup,
                ContractorId = item.ContractorId,
                PackageId = item.PackageId,
                FinalPrice = prices.FinalPrice,
            });

            if (projectProduct is not null)
            {
                var sum = projectProduct.RequestGoodsSupplyTypeDetails.Where(x => !x.IsDeleted).Sum(x => x.RequestedCount);
                if (projectProduct.RequestQuantity < sum)
                    return Result.Failure<List<RequestGoodsSupplyTypeDetail>?>(RequestGoodsSupplyErrors.RequestCountMoreThanAssigned);
            }


            await _repository.Create(detail, ct);
            details.Add(detail);
        }

        return details;
    }

    public async Task<Result<List<RequestGoodsSupplyTypeDetail>?>> CreateServiceDetail(RequestGoodsSupply entity,
    List<RequestGoodsSupplyType> types,
    List<CreateRGSTypeDetailModel> detailModels, CT ct)
    {
        List<RequestGoodsSupplyTypeDetail>? details = [];
        var costCenters = await _ccRepo.GetByIdsIncludeType(detailModels.NullListed(x => x.CostCenterId), ct);
        foreach (var item in detailModels)
        {
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
                entity.Type);

            var type = types.FirstOrDefault(x => x.ReferenceId == item.ReferenceId && x.Type == SupplyType.Service);
            var cc = costCenters?.FirstOrDefault(x => x.Id == item.CostCenterId);
            var detail = RequestGoodsSupplyTypeDetail.Create(new CreateRGSTypeDetailParameters
            {
                RequestGoodsSupply = entity,
                RequestGoodsSupplyType = type,
                CostCenter = cc,
                ReferenceId = item.ReferenceId.Value,
                Type = SupplyType.Service,
                Importance = item.Importance,
                RequestedCount = item.RequestedCount,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice,
                PackageCount = item.PackageCount,
                PackageUnitPrice = packageUnitPrice,
                PackingPrice = item.PackingPrice,
                DelivaryDeadLine = item.DelivaryDeadLine,
                DocumentUrls = item.DocumentUrls,
                Description = item.Description,
                DescriptionEn = item.DescriptionEn,
                ManagementDescription = item.ManagementDescription,
                CheckGroup = item.CheckGroup,
                ContractorId = item.ContractorId,
                PackageId = item.PackageId,
                FinalPrice = prices.FinalPrice,
            });

            await _repository.Create(detail, ct);

            type.SetRequestedCount(type.RequestGoodsSupplyTypeDetails.Where(x => !x.IsDeleted).Sum(x => x.RequestedCount));
            details.Add(detail);
        }

        return details;
    }

    public async Task<Result<List<RequestGoodsSupplyTypeDetail>?>> CreateAdDetail(RequestGoodsSupply entity,
    List<RequestGoodsSupplyType> types,
    List<CreateRGSTypeDetailModel> detailModels, CT ct)
    {
        List<RequestGoodsSupplyTypeDetail>? details = [];
        var costCenters = await _ccRepo.GetByIdsIncludeType(detailModels.NullListed(x => x.CostCenterId), ct);
        foreach (var item in detailModels)
        {
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
                entity.Type);

            var type = types.FirstOrDefault(x => x.ReferenceId == item.ReferenceId && x.Type == SupplyType.Ads);
            var cc = costCenters?.FirstOrDefault(x => x.Id == item.CostCenterId);
            var detail = RequestGoodsSupplyTypeDetail.Create(new CreateRGSTypeDetailParameters
            {
                RequestGoodsSupply = entity,
                RequestGoodsSupplyType = type,
                CostCenter = cc,
                ReferenceId = item.ReferenceId.Value,
                Type = SupplyType.Ads,
                Importance = item.Importance,
                RequestedCount = item.RequestedCount,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice,
                PackageCount = item.PackageCount,
                PackageUnitPrice = packageUnitPrice,
                PackingPrice = item.PackingPrice,
                DelivaryDeadLine = item.DelivaryDeadLine,
                DocumentUrls = item.DocumentUrls,
                Description = item.Description,
                DescriptionEn = item.DescriptionEn,
                ManagementDescription = item.ManagementDescription,
                CheckGroup = item.CheckGroup,
                ContractorId = item.ContractorId,
                PackageId = item.PackageId,
                FinalPrice = prices.FinalPrice,
            });

            await _repository.Create(detail, ct);

            type.SetRequestedCount(type.RequestGoodsSupplyTypeDetails.Where(x => !x.IsDeleted).Sum(x => x.RequestedCount));
            details.Add(detail);
        }

        return details;
    }

    public async Task<Result<List<RequestGoodsSupplyTypeDetail>?>> CreateProjectDetail(RequestGoodsSupply entity,
    List<RequestGoodsSupplyType> types,
    List<CreateRGSTypeDetailModel> detailModels, CT ct)
    {
        List<RequestGoodsSupplyTypeDetail>? details = [];
        var costCenters = await _ccRepo.GetByIdsIncludeType(detailModels.NullListed(x => x.CostCenterId), ct);
        foreach (var item in detailModels)
        {
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
                entity.Type);

            var type = types.FirstOrDefault(x => x.Type == SupplyType.Project);
            var cc = costCenters?.FirstOrDefault(x => x.Id == item.CostCenterId);
            var detail = RequestGoodsSupplyTypeDetail.Create(new CreateRGSTypeDetailParameters
            {
                RequestGoodsSupply = entity,
                RequestGoodsSupplyType = type,
                CostCenter = cc,
                Type = SupplyType.Project,
                Importance = item.Importance,
                RequestedCount = item.RequestedCount,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice,
                PackageCount = item.PackageCount,
                PackageUnitPrice = packageUnitPrice,
                PackingPrice = item.PackingPrice,
                DelivaryDeadLine = item.DelivaryDeadLine,
                DocumentUrls = item.DocumentUrls,
                Description = item.Description,
                DescriptionEn = item.DescriptionEn,
                ManagementDescription = item.ManagementDescription,
                CheckGroup = item.CheckGroup,
                ContractorId = item.ContractorId,
                PackageId = item.PackageId,
                FinalPrice = prices.FinalPrice,
            });

            await _repository.Create(detail, ct);

            type.SetRequestedCount(type.RequestGoodsSupplyTypeDetails.Where(x => !x.IsDeleted).Sum(x => x.RequestedCount));
            details.Add(detail);
        }

        return details;
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