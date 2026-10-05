using Engineering.Application.RequestGoodsSupplyManagements.Models.GetFilteredManagementRequestGoodsSupplies;
using Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetsConsumableVolumeProductsForSupply;
using Engineering.Application.Services.OperationInfoSeasons.Queries.GetOperationInfoSeasonById;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByContractor;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdLessInclude;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationForRequestGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Contracts.UpdatePRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRequestGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyById;
using Engineering.Application.Services.RequestGoodsSupplies.Models.PRGSupplyImport;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RGSupplyImport;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyDetailByIds;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using Engineering.Domain.Entities.Synonyms.Warehouse.Products;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Queries.GetsUserById;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.InventoryPackages.Models.GetsPackagesByIds;
using Pipelines.Sockets.Unofficial.Arenas;

namespace Engineering.Application.Services.RequestGoodsSupplies;

public partial class RequestGoodsSupplyLogic : IRequestGoodsSupplyLogic
{

    private async Task<Result<CreateRequestGoodsSupplyValidatorsResponse?>> CreateRequestGoodsSupplyValidators(CreateRequestGoodsSupplyValidatorsRequest request, CT ct)
    {
        var requestValue = request.GoodsSupplyRequest;
        if (requestValue.Type == GoodsSupplyType.GoodsSupply && requestValue.Details.Sum(oo => oo.TotalPrice) > 0)
            return Result.Failure<CreateRequestGoodsSupplyValidatorsResponse>(RequestGoodsSupplyErrors.InValidPriceAndType);
        if (requestValue.Details.Count <= 0)
            return Result.Failure<CreateRequestGoodsSupplyValidatorsResponse>(RequestGoodsSupplyErrors.DetailsIsNull);

        var projectOperationQuery = await _mediator.Send(new GetProjectOperationForRequestGoodsSupplyQuery(requestValue.ProjectOperationId), ct);
        if (projectOperationQuery.IsFailure)
            return Result.Failure<CreateRequestGoodsSupplyValidatorsResponse>(projectOperationQuery.Error!);
        var projectOperation = projectOperationQuery.Value!;

        if (projectOperation.Project.Status == ProjectStatus.DefiniteDelivery || projectOperation.Project.Status == ProjectStatus.TemporaryDelivery ||
            projectOperation.Project.Status == ProjectStatus.Stopped || projectOperation.Project.Status == ProjectStatus.Canceled)
            return Result.Failure<CreateRequestGoodsSupplyValidatorsResponse>(RequestGoodsSupplyErrors.InValidProjectStatus);

        if (requestValue.Type == GoodsSupplyType.Contractor)
        {
            var ids = requestValue.Details.Select(x => x.ContractorId).Distinct().ToList();
            if (requestValue.Details.Any(x => x.ContractorId is null || x.ContractorId == 0))
                return Result.Failure<CreateRequestGoodsSupplyValidatorsResponse>(RequestGoodsSupplyErrors.ContractorTypeContractorIsEmpty);
            else
                foreach (var id in ids)
                {
                    var validateContractor = await _mediator.Send(new GetProjectOperationDetailByContractorQuery(projectOperation.Project.Id, (long)id!), ct);
                    if (validateContractor.IsFailure)
                        return Result.Failure<CreateRequestGoodsSupplyValidatorsResponse>(RequestGoodsSupplyErrors.InValidContractor);
                }
        }

        ProjectOperationDetail? projectOperationDetail = null;
        if (requestValue.ProjectOperationDetailId.HasValue)
        {
            var projectOperationDetailQuery = await _mediator.Send(new GetProjectOperationDetailByIdLessIncludeQuery(requestValue.ProjectOperationDetailId!.Value), ct);
            if (projectOperationDetailQuery.IsFailure)
                return Result.Failure<CreateRequestGoodsSupplyValidatorsResponse>(projectOperationDetailQuery.Error!);
            projectOperationDetail = projectOperationDetailQuery.Value!;
            if (!projectOperation.ProjectOperationDetails.Any(x => x.Id == projectOperationDetail.Id))
                return Result.Failure<CreateRequestGoodsSupplyValidatorsResponse>(RequestGoodsSupplyErrors.ProjectOperationDetailNotInProjectOperation);
        }

        OperationInfoSeason? operationInfoSeason = null;
        if (requestValue.OperationInfoSeasonId > 0)
        {
            var operationInfoSeasonQuery = await _mediator.Send(new GetOperationInfoSeasonByIdQuery(requestValue.OperationInfoSeasonId), ct);
            if (operationInfoSeasonQuery.IsFailure || operationInfoSeasonQuery.Value is null)
                return Result.Failure<CreateRequestGoodsSupplyValidatorsResponse>(operationInfoSeasonQuery.Error!);
            operationInfoSeason = operationInfoSeasonQuery.Value!;
            if (operationInfoSeason.Season.Id.Equals(1))
                return Result.Failure<CreateRequestGoodsSupplyValidatorsResponse>(RequestGoodsSupplyErrors.InValidSeason);
        }

        if (await IsCompanyValid(_companyId, ct) == false)
            return Result.Failure<CreateRequestGoodsSupplyValidatorsResponse>(BillOfLadingErrors.InvalidCompany);

        var response = new CreateRequestGoodsSupplyValidatorsResponse()
        {
            ProjectOperation = projectOperation,
            ProjectOperationDetail = projectOperationDetail,
            OperationInfoSeason = operationInfoSeason
        };
        return response;
    }


    private async Task<Result> ValidateRequestGoodsSupplyDetails(List<UpdateRequestGoodsSupplyDetailModel> details, CT ct)
    {
        var groupIds = details.Select(x => x.ProductGroupId).Distinct().ToList();
        if (groupIds.Any())
        {
            var groups = await WebServicesLogic.GroupsDataReceiver(groupIds, _mediator, ct);
            if (groupIds.Count != groups?.Count)
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.GroupsNotValid);
        }

        var productIds = details.Select(x => x.ProductId).Distinct().ToList();
        if (productIds.Any())
        {
            var products = await WebServicesLogic.ProductsDataReceiver(productIds, _mediator, _viewProductRepository, ct);
            if (productIds.Count != products?.Count)
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.ProductsNotValid);
            if (products is not null && products.Any(x => !x.IsActive))
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.ProductIsInActive);
        }

        return Result.Success();
    }

    private async Task<Result> ValidateProjectRequestGoodsSupplyDetails(List<UpdateProjectRequestGoodsSupplyDetailModel> details, CT ct)
    {
        var groupIds = details.Select(x => x.ProductGroupId).Distinct().ToList();
        var requestGoodsDetail = await _mediator.Send(new GetsRequestGoodsSupplyDetailByIdsQuery(details.Where(x => x.RequestGoodsSupplyDetailId.HasValue).Select(x => x.RequestGoodsSupplyDetailId.Value!).ToList()));
        if (requestGoodsDetail.IsBad() || requestGoodsDetail.Value.Data == null)
            return requestGoodsDetail.Failure<Result>();

        var projectProducts = requestGoodsDetail.Value.Data.Select(x => x.ProjectProduct).ToList();
        if (groupIds.Any() && projectProducts.FirstOrDefault().ProjectProductType == ProjectProductType.ProductGroup)
        {
            var groups = await WebServicesLogic.GroupsDataReceiver(groupIds, _mediator, ct);
            if (groupIds.Count != groups?.Count)
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.GroupsNotValid);
        }
        else if (groupIds.Any() && projectProducts.FirstOrDefault().ProjectProductType == ProjectProductType.Category)
        {
            var categories = await _viewCategoryRepository.GetByIds(groupIds, ct);
            if (categories == null || categories.Count == 0)
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.CategoriesNotValid);
        }

        var productIds = details.Select(x => x.ProductId).Distinct().ToList();
        if (productIds.Any())
        {
            var products = await _viewProductRepository.GetProductByIds(productIds, ct);
            if (products == null)
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.ProductsNotValid);
            if (productIds.Count != products?.Count)
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.ProductsNotValid);
            if (products is not null && products.Any(x => !x.IsActive))
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.ProductIsInActive);
        }

        return Result.Success();
    }

    private async Task<List<GetsRequestGoodsSupplyDetailModel>> DetailsModeling(RequestGoodsSupply value, List<ViewThirdParty?>? contractorsInfo, CT ct)
    {
        var requestGoodsSupplyDetails = value!.RequestGoodsSupplyDetails.ToList();
        var groupIds = requestGoodsSupplyDetails.Where(x => x.ConsumableVolumeProduct is not null && x.ConsumableVolumeProduct.ProductGroupId > 0).Select(x => x.ConsumableVolumeProduct.ProductGroupId).Distinct().ToList();
        var groups = await WebServicesLogic.GroupsDataReceiver(groupIds, _mediator, ct);
        var productIds = requestGoodsSupplyDetails.Where(x => x.ProductId > 0).Select(x => x.ProductId).Distinct().ToList();
        var products = await _viewProductRepository.GetProductByIds(productIds, ct);
        var packageIds = requestGoodsSupplyDetails.Where(x => x.PackageId is not null && x.PackageId > 0).Select(x => (long)x.PackageId!).ToList();
        var packagesInfo = await WebServicesLogic.PackagesDataReceiver(packageIds, _mediator, ct);
        var creatorIds = requestGoodsSupplyDetails.Select(c => c.CreatorId).ToList();
        List<FilteredUserResponseModel>? creators = [];
        var creatorResponse = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);
        if (creatorResponse is not null)
            creators.AddRange(creatorResponse);

        List<GetsRequestGoodsSupplyDetailModel> details = [];
        foreach (var item in requestGoodsSupplyDetails)
        {
            var group = groups?.Where(m => m.Id == item.ConsumableVolumeProduct.ProductGroupId).FirstOrDefault();
            var product = products?.Where(m => m.Id == item.ProductId).FirstOrDefault();
            var creator = creators?.Where(m => m.UserId == item.CreatorId).FirstOrDefault();
            var package = packagesInfo?.Where(x => x.Id.Equals(item.PackageId)).FirstOrDefault();


            var productGroupsQuery = await _mediator.Send(new GetsConsumableVolumeProductsForSupplyQuery(null, value.ProjectOperation.Id, item.ConsumableVolumeProduct.ProjectOperationDetail.Id,
                item.ConsumableVolumeProduct.ProductGroupId, null, null, 1, 10), ct);
            var modelingData = DetailModeling(item, productGroupsQuery.Value!.Data!.FirstOrDefault()!, group, product, contractorsInfo, package, creator);

            var managements = item.RequestGoodsSupplyManagements;
            if (managements != null && managements.Count > 0)
            {
                var managementProductIds = managements.Where(x => x.ReferenceId is not null && x.ReferenceId > 0).Select(x => (long)x.ReferenceId!).Distinct().ToList();
                List<GetProductModel>? managementProducts = new();
                if (managementProductIds != null && managementProductIds.Count > 0)
                    managementProducts = await WebServicesLogic.ProductsDataReceiver(managementProductIds, _mediator, _viewProductRepository, ct);

                var warehouseIds = managements.Where(x => x.WarehouseId is not null && x.WarehouseId > 0).Select(oo => oo.WarehouseId).ToList();
                warehouseIds.AddRange(managements.Where(x => x.DestinationWarehouseId is not null && x.DestinationWarehouseId > 0).Select(oo => oo.DestinationWarehouseId).ToList());
                var warehouses = await WebServicesLogic.WarehousesDataReceiver(warehouseIds, _mediator, ct);

                var managementDetails = new List<GetsRequestGoodsSupplyDetailManagementModel>();
                foreach (var management in managements)
                {
                    var warehouse = warehouses?.Where(x => x.Id == management.WarehouseId).FirstOrDefault();
                    var destinationWarehouse = warehouses?.Where(x => x.Id == management.DestinationWarehouseId).FirstOrDefault();
                    var managementProduct = managementProducts?.Where(x => x.Id == item.ProductId).FirstOrDefault();
                    var managementDetail = new GetsRequestGoodsSupplyDetailManagementModel()
                    {
                        Id = management.Id,
                        InvoiceId = management.InvoiceId,
                        WarehouseId = warehouse?.Id,
                        WarehouseName = warehouse?.Name,
                        DestinationWarehouseId = destinationWarehouse?.Id,
                        DestinationWarehouseName = destinationWarehouse?.Name,
                        AlternateId = management.AlternateId,
                        AssignmentDate = management.AssignmentDate,
                        RequestedCount = management.RequestedCount,
                        ConfirmedRequestCount = management.ConfirmedRequestCount,
                        Status = management.Status,
                        Type = management.Type,
                        Description = management.Description,
                        LastDescription = management.LastDescription,
                        OperatorAppointmentId = management.OperatorAppointmentId,
                        OperatorAppointmentName = contractorsInfo?.Where(x => x is not null).FirstOrDefault(x => x!.Id.Equals(management.OperatorAppointmentId))?.FullName,
                        Product = management.ReferenceId is not null ? new GetRequestGoodsSupplyManagementProductByIdModel()
                        {
                            Id = managementProduct?.Id,
                            Code = managementProduct?.Code,
                            Brand = managementProduct?.Brand,
                            BrandModel = managementProduct?.BrandModel,
                            Name = managementProduct?.Name
                        } : null
                    };
                    managementDetails.Add(managementDetail);
                }
                modelingData.Managements = managementDetails;
            }
            details.Add(modelingData.Adapt<GetsRequestGoodsSupplyDetailModel>());
        }

        return details;
    }

    private async Task<List<GetsRequestGoodsSupplyDetailModel>> ProjectDetailsModeling(RequestGoodsSupply value, List<ViewThirdParty?>? contractorsInfo, CT ct)
    {
        var requestGoodsSupplyDetails = value!.RequestGoodsSupplyDetails.ToList();
        var categoryIds = requestGoodsSupplyDetails
            .Where(x => x.ProjectProduct != null
             && x.ProjectProduct.ProductCategoryId.HasValue
             && x.ProjectProduct.ProductCategoryId.Value > 0)
            .Select(x => x.ProjectProduct.ProductCategoryId!.Value)
            .Distinct()
            .ToList();
        List<long> categoryGroupIds = [];

        if (categoryIds.HasAny() && categoryIds.Count > 0)
        {
            var categoryGroups = await _viewGroupRepository.GetFilteredGroupsByCategoryIds(categoryIds, null, null, 0, 0, ct);
            if (categoryGroups != null && categoryGroups.HasAny())
            {
                categoryGroupIds = categoryGroups
                .Listed(x => x.Id);
            }
        }

        var groupIds = requestGoodsSupplyDetails
            .Where(x => x.ProjectProduct != null
                && x.ProjectProduct.ProductGroupId.HasValue
                && x.ProjectProduct.ProductGroupId.Value > 0)
            .Select(x => x.ProjectProduct.ProductGroupId!.Value)
            .Distinct()
            .Union(categoryGroupIds)
            .ToList();

        var groups = await _viewGroupRepository.GetGroupsByIds(groupIds, ct);

        var productIds = requestGoodsSupplyDetails.Where(x => x.ProductId > 0).Select(x => x.ProductId).Distinct().ToList();
        var products = await _viewProductRepository.GetProductByIds(productIds, ct);
        var packageIds = requestGoodsSupplyDetails.Where(x => x.PackageId is not null && x.PackageId > 0).Select(x => (long)x.PackageId!).ToList();
        var packagesInfo = await WebServicesLogic.PackagesDataReceiver(packageIds, _mediator, ct);
        var creatorIds = requestGoodsSupplyDetails.Select(c => c.CreatorId).ToList();
        List<FilteredUserResponseModel>? creators = [];
        var creatorResponse = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);
        if (creatorResponse is not null)
            creators.AddRange(creatorResponse);

        List<GetsRequestGoodsSupplyDetailModel> details = [];
        foreach (var item in requestGoodsSupplyDetails)
        {
            var group = groups?.Where(m => m.Id == item.ProjectProduct.ProductGroupId).FirstOrDefault();
            var product = products?.Where(m => m.Id == item.ProductId).FirstOrDefault();
            var creator = creators?.Where(m => m.UserId == item.CreatorId).FirstOrDefault();
            var package = packagesInfo?.Where(x => x.Id.Equals(item.PackageId)).FirstOrDefault();

            var productGroupsQuery = await _projectProductRepositoy.GetProductByProjectIdAndGroupId(value.ProjectId, item.ProjectProduct?.ProductGroupId, item.ProjectProduct?.ProductCategoryId, ct);
            var modelingData = ProjectDetailModeling(item, productGroupsQuery!.FirstOrDefault()!, group, product, contractorsInfo, package, creator);

            var managements = item.RequestGoodsSupplyManagements;
            if (managements != null && managements.Count > 0)
            {
                var managementProductIds = managements.Where(x => x.ReferenceId is not null && x.ReferenceId > 0).Select(x => (long)x.ReferenceId!).Distinct().ToList();
                List<GetProductModel>? managementProducts = new();
                if (managementProductIds != null && managementProductIds.Count > 0)
                    managementProducts = await _viewProductRepository.GetProductByIds(managementProductIds, ct);

                var warehouseIds = managements.Where(x => x.WarehouseId is not null && x.WarehouseId > 0).Select(oo => oo.WarehouseId).ToList();
                warehouseIds.AddRange(managements.Where(x => x.DestinationWarehouseId is not null && x.DestinationWarehouseId > 0).Select(oo => oo.DestinationWarehouseId).ToList());
                var warehouses = await WebServicesLogic.WarehousesDataReceiver(warehouseIds, _mediator, ct);

                var managementDetails = new List<GetsRequestGoodsSupplyDetailManagementModel>();
                foreach (var management in managements)
                {
                    var warehouse = warehouses?.Where(x => x.Id == management.WarehouseId).FirstOrDefault();
                    var destinationWarehouse = warehouses?.Where(x => x.Id == management.DestinationWarehouseId).FirstOrDefault();
                    var managementProduct = managementProducts?.Where(x => x.Id == item.ProductId).FirstOrDefault();
                    var managementDetail = new GetsRequestGoodsSupplyDetailManagementModel()
                    {
                        Id = management.Id,
                        InvoiceId = management.InvoiceId,
                        WarehouseId = warehouse?.Id,
                        WarehouseName = warehouse?.Name,
                        DestinationWarehouseId = destinationWarehouse?.Id,
                        DestinationWarehouseName = destinationWarehouse?.Name,
                        AlternateId = management.AlternateId,
                        AssignmentDate = management.AssignmentDate,
                        RequestedCount = management.RequestedCount,
                        ConfirmedRequestCount = management.ConfirmedRequestCount,
                        Status = management.Status,
                        Type = management.Type,
                        Description = management.Description,
                        LastDescription = management.LastDescription,
                        OperatorAppointmentId = management.OperatorAppointmentId,
                        OperatorAppointmentName = contractorsInfo?.Where(x => x is not null).FirstOrDefault(x => x!.Id.Equals(management.OperatorAppointmentId))?.FullName,
                        Product = management.ReferenceId is not null ? new GetRequestGoodsSupplyManagementProductByIdModel()
                        {
                            Id = managementProduct?.Id,
                            Code = managementProduct?.Code,
                            ProductDescription = managementProduct?.ProductDescription,
                            Brand = managementProduct?.Brand,
                            BrandModel = managementProduct?.BrandModel,
                            Name = managementProduct?.Name
                        } : null
                    };
                    managementDetails.Add(managementDetail);
                }
                modelingData.Managements = managementDetails;
            }
            details.Add(modelingData.Adapt<GetsRequestGoodsSupplyDetailModel>());
        }

        return details;
    }

    private GetsRequestGoodsSupplyDetailModel DetailModeling(RequestGoodsSupplyDetail item, ConsumableVolumeProduct consumableVolume, Group? group, GetProductModel? product,
        List<ViewThirdParty?>? contractorsInfos, GetsPackagesByIdsModel? package, FilteredUserResponseModel? creator)
    {
        var goodsSupply = item.RequestGoodsSupply;
        var operationDetail = consumableVolume.ProjectOperationDetail;
        var location = consumableVolume.ProjectOperationDetail.OperationLocation;
        var total = TotalDataReceiver(consumableVolume);

        var supplyManagments = item.RequestGoodsSupplyManagements.Where(x => x.ReferenceId == item.ProductId && x.Status == GoodsSupplyManagementStatus.CompleteSupply).ToList();
        var commerceManagments = supplyManagments.Where(x => x.Type == GoodsSupplyManagementType.Commerce).ToList();
        var notCommerceManagments = supplyManagments.Where(x => x.Type != GoodsSupplyManagementType.Commerce).ToList();

        var detail = new GetsRequestGoodsSupplyDetailModel()
        {
            Id = item.Id,
            ProjectId = item?.ProjectProduct != null ? item.RequestGoodsSupply.ProjectId :
            item.RequestGoodsSupply.ProjectOperation.ProjectId,
            ConsumableVolumeProductIds = [consumableVolume.Id],
            Product = product,
            ProductId = item.ProductId,
            ProductGroupId = item?.ProjectProduct != null ? item.ProjectProduct.ProductGroupId :
            item.ConsumableVolumeProduct.ProductGroupId,
            ProductCategoryId = item?.ProjectProduct != null ? item.ProjectProduct.ProductCategoryId :
            item.ConsumableVolumeProduct.ProductGroupId,
            ProjectOperationDetail = new(operationDetail.Id, location.Id, location?.PrivateName, location?.PrivateCode, location?.PublicName, location?.PublicCode, operationDetail.FinalAmount, operationDetail.Description),
            CanUpdate = GSDSRules.AllowStatusForUpdate.Any(x => x.Equals(item.Status)),

            Group = new(consumableVolume.ProductGroupId, consumableVolume.ProductGroupId,
            consumableVolume.VolumeProductType, group?.Name, group?.Code, group?.MeasureUnitName),

            ProjectOperationDetails = location?.PrivateName,
            TolerancePercentage = total.TolerancePercentage,
            ToleranceCount = total.ToleranceCount,
            TotalEstimatedCount = total.TotalEstimatedCount,
            TotalRequestedCount = total.TotalRequestedCount,
            TotalSupplyCount = total.TotalSupplyCount,
            TotalRemainedCount = total.TotalRemainedCount,
            RequestedCount = item.RequestedCount,
            SupplyCount = commerceManagments.Sum(x => x.ConfirmedRequestCount is null ? 0 : x.ConfirmedRequestCount) + notCommerceManagments.Sum(x => x.RequestedCount),
            DelivaryDeadLine = item.DelivaryDeadLine,
            UnitPrice = item.UnitPrice,
            TotalPrice = item.TotalPrice,
            DiscountByNumber = item.DiscountByNumber,
            DiscountByPercentage = item.DiscountByPercentage,
            DiscountedPrice = item.DiscountedPrice,
            TaxPercentage = item.TaxPercentage,
            TaxNumber = item.TaxNumber,
            PackingPrice = item.PackingPrice,
            FinalPrice = item.FinalPrice,
            Importance = item.Importance,
            Status = item.Status,
            Package = new(package?.Id, package?.Quantity, package?.IsDefault, package?.IsActive, package?.Title, item.PackageCount),
            Contractor = new(item?.ContractorId, contractorsInfos?.Where(x => x?.Id == item?.ContractorId).FirstOrDefault()?.FullName),
            CheckGroup = item?.CheckGroup,
            Creator = creator?.FirstName + " " + creator?.LastName,
            Description = item?.Description,
            DestinationWarehouseId = item?.DestinationWarehouseId,
            ManagementDescription = item?.ManagementDescription,
            Documents = item?.RequestGoodsSupplyDetailDocuments.Select(oo => oo.Url).ToList(),
            CustomerInvoiceNumber = item?.CustomerInvoiceNumber,
            LastDescription = item?.LastDescription,
        };
        return detail;
    }

    private GetsRequestGoodsSupplyDetailModel ProjectDetailModeling(RequestGoodsSupplyDetail item, ProjectProduct projectProduct, Group? group, GetProductModel? product,
        List<ViewThirdParty?>? contractorsInfos, GetsPackagesByIdsModel? package, FilteredUserResponseModel? creator)
    {
        var goodsSupply = item.RequestGoodsSupply;
        var total = TotalProjectDataReceiver(projectProduct);

        var supplyManagments = item.RequestGoodsSupplyManagements.Where(x => x.ReferenceId == item.ProductId && x.Status == GoodsSupplyManagementStatus.CompleteSupply).ToList();
        var commerceManagments = supplyManagments.Where(x => x.Type == GoodsSupplyManagementType.Commerce).ToList();
        var notCommerceManagments = supplyManagments.Where(x => x.Type != GoodsSupplyManagementType.Commerce).ToList();

        var detail = new GetsRequestGoodsSupplyDetailModel()
        {
            Id = item.Id,
            ProjectId = item?.ProjectProduct != null ? item.RequestGoodsSupply.ProjectId :
            item.RequestGoodsSupply.ProjectOperation.ProjectId,
            ConsumableVolumeProductIds = [projectProduct.Id],
            ProjectProductIds = [projectProduct.Id],
            Product = product,
            ProductId = item.ProductId,
            ProductGroupId = item?.ProjectProduct != null ? item.ProjectProduct.ProductGroupId :
            item.ConsumableVolumeProduct.ProductGroupId,
            ProductCategoryId = item?.ProjectProduct != null ? item.ProjectProduct.ProductCategoryId :
            item.ConsumableVolumeProduct.ProductGroupId,
            CanUpdate = GSDSRules.AllowStatusForUpdate.Any(x => x.Equals(item.Status)),
            Group = new(projectProduct.ProductGroupId ?? projectProduct.ProductCategoryId, projectProduct.ProductGroupId ?? projectProduct.ProductCategoryId, item?.ProjectProduct != null ?
            (VolumeProductType)item.ProjectProduct.ProjectProductType :
            item?.ConsumableVolumeProduct?.VolumeProductType, group?.Name, group?.Code, group?.MeasureUnitName),
            TolerancePercentage = total.TolerancePercentage,
            ToleranceCount = projectProduct.TolerancePercentage * projectProduct.RequestQuantity,
            TotalEstimatedCount = total.TotalEstimatedCount,
            TotalRequestedCount = total.TotalRequestedCount,
            TotalSupplyCount = total.TotalSupplyCount,
            TotalRemainedCount = total.TotalRemainedCount,
            RequestedCount = item.RequestedCount,
            SupplyCount = commerceManagments.Sum(x => x.ConfirmedRequestCount is null ? 0 : x.ConfirmedRequestCount) + notCommerceManagments.Sum(x => x.RequestedCount),
            DelivaryDeadLine = item.DelivaryDeadLine,
            UnitPrice = item.UnitPrice,
            TotalPrice = item.TotalPrice,
            DiscountByNumber = item.DiscountByNumber,
            DiscountByPercentage = item.DiscountByPercentage,
            DiscountedPrice = item.DiscountedPrice,
            TaxPercentage = item.TaxPercentage,
            TaxNumber = item.TaxNumber,
            PackingPrice = item.PackingPrice,
            FinalPrice = item.FinalPrice,
            Importance = item.Importance,
            Status = item.Status,
            Package = new(package?.Id, package?.Quantity, package?.IsDefault, package?.IsActive, package?.Title, item.PackageCount),
            Contractor = new(item?.ContractorId, contractorsInfos?.Where(x => x?.Id == item?.ContractorId).FirstOrDefault()?.FullName),
            CheckGroup = item?.CheckGroup,
            Creator = creator?.FirstName + " " + creator?.LastName,
            Description = item?.Description,
            DestinationWarehouseId = item?.DestinationWarehouseId,
            ManagementDescription = item?.ManagementDescription,
            Documents = item?.RequestGoodsSupplyDetailDocuments.Select(oo => oo.Url).ToList(),
            CustomerInvoiceNumber = item?.CustomerInvoiceNumber,
            LastDescription = item?.LastDescription,
        };
        return detail;
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
        var closed = GoodsSupplyDetailStatus.Closed;
        var notComplete = GoodsSupplyDetailStatus.NotCompleteSupply;

        var totalRequest = consumableVolume.RequestGoodsSupplyDetails.Where(x => x.Status != pmRejected && x.Status != pmReturned &&
            x.Status != suRejected && x.Status != suReturned && x.Status != mRejected && x.Status != mReturned && x.Status != closed && x.Status != notComplete).Sum(x => x.RequestedCount);

        var details = consumableVolume.RequestGoodsSupplyDetails.Where(x => x.RequestGoodsSupplyProduct is not null).Select(x => x.RequestGoodsSupplyProduct).ToList()
            .Where(x => x?.Status != pmRejected && x?.Status != pmReturned && x?.Status != mRejected && x?.Status != mReturned && x?.Status != suRejected && x?.Status != suReturned && x?.Status != closed).ToList();

        var managments = details.SelectMany(x => x!.RequestGoodsSupplyManagements.Where(m => m.Status != reject)).ToList();

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
            TotalRequestedCount = Calculator.RoundingDecimalDTFV(details.Sum(x => x?.RequestedCount)),
            TotalSupplyCount = commerceSupplyCount + warehouseSupplyCount,
            TotalDifferenceCount = commerceDifferenceCount,
        };

        var remainedCount = total.TotalEstimatedCount - (total.TotalRequestedCount - total.TotalDifferenceCount) ?? 0;
        if (remainedCount >= 0)
            total.TotalRemainedCount = Math.Round(remainedCount, 2);
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

    private CalculatProjectOperationDetailSupplyCounterModel TotalProjectDataReceiver(ProjectProduct projectProduct)
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
        var closed = GoodsSupplyDetailStatus.Closed;
        var notComplete = GoodsSupplyDetailStatus.NotCompleteSupply;

        var totalRequest = projectProduct.RequestGoodsSupplyDetails.Where(x => x.Status != pmRejected && x.Status != pmReturned &&
            x.Status != suRejected && x.Status != suReturned && x.Status != mRejected && x.Status != mReturned && x.Status != closed && x.Status != notComplete).Sum(x => x.RequestedCount);

        var details = projectProduct.RequestGoodsSupplyDetails.Where(x => x.RequestGoodsSupplyProduct is not null).Select(x => x.RequestGoodsSupplyProduct).ToList()
            .Where(x => x?.Status != pmRejected && x?.Status != pmReturned && x?.Status != mRejected && x?.Status != mReturned && x?.Status != suRejected && x?.Status != suReturned && x?.Status != closed).ToList();

        var managments = details.SelectMany(x => x!.RequestGoodsSupplyManagements.Where(m => m.Status != reject)).ToList();

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
            ConsumableVolumeId = projectProduct.Id,
            ProductGroupId = projectProduct.ProductGroupId is not null ? projectProduct.ProductGroupId.Value : projectProduct.ProductCategoryId.Value!,
            TotalEstimatedCount = Calculator.RoundingDecimalDTFV(projectProduct.RequestQuantity),
            TolerancePercentage = projectProduct.TolerancePercentage,
            ToleranceCount = Calculator.RoundingDecimalDTFV((projectProduct.RequestQuantity / 100) * projectProduct.RemainingQuantity + 1),
            TotalRequestedCount = Calculator.RoundingDecimalDTFV(details.Sum(x => x?.RequestedCount)),
            TotalSupplyCount = commerceSupplyCount + warehouseSupplyCount,
            TotalDifferenceCount = commerceDifferenceCount,
        };

        var remainedCount = total.TotalEstimatedCount - (total.TotalRequestedCount - total.TotalDifferenceCount) ?? 0;
        if (remainedCount >= 0)
            total.TotalRemainedCount = Math.Round(remainedCount, 2);
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

    private ProductRequestReviewerModel RequestReviewer(RequestGoodsSupply item)
    {
        ProductRequestReviewerModel reviewerModel = new();
        if (item.RequestGoodsSupplyDetails is not null && item.RequestGoodsSupplyDetails.Count > 0)
        {
            var completeSupply = GoodsSupplyManagementStatus.CompleteSupply;
            // var inCompleteSupply = GoodsSupplyManagementStatus.InCompleteSupply;
            var reject = GoodsSupplyManagementStatus.Return;

            var inStockT = GoodsSupplyManagementType.InStock;
            var betweenStockT = GoodsSupplyManagementType.BetweenStock;
            var commerceT = GoodsSupplyManagementType.Commerce;

            var managments = item.RequestGoodsSupplyDetails.Where(x => !x.IsDeleted).SelectMany(x => x.RequestGoodsSupplyManagements).Where(x => !x.IsDeleted).ToList();
            if (managments is not null && managments.Count > 0)
            {
                reviewerModel.RejectedNumber = managments.Count(x => x.Status == reject);

                reviewerModel.AllInStock = managments.Count(x => (!x.Status.Equals(reject)) && x.Type.Equals(inStockT));
                reviewerModel.InStockNumber = managments.Count(x => x.Status.Equals(completeSupply) && x.Type.Equals(inStockT));

                reviewerModel.AllBetweenStock = managments.Count(x => (!x.Status.Equals(reject)) && x.Type.Equals(betweenStockT));
                reviewerModel.BetweenStockNumber = managments.Count(x => x.Status.Equals(completeSupply) && x.Type.Equals(betweenStockT));

                reviewerModel.AllCommerce = managments.Count(x => (!x.Status.Equals(reject)) && x.Type.Equals(commerceT));
                reviewerModel.CommerceNuber = managments.Count(x => x.Status.Equals(completeSupply) && x.Type.Equals(commerceT));

                var allRequest = managments.Count(x => !x.Status.Equals(reject));
                var allConfirmRequest = managments.Count(x => x.Status.Equals(completeSupply));
                reviewerModel.Percent = allRequest == 0 ? 0 : (100 / allRequest) * allConfirmRequest;
            }
        }

        return reviewerModel;
    }

    private async Task<string> DescriptionMacker(string? requestDescription, GoodsSupplyStatus status, CT ct)
    {
        var getUsers = await _mediator.Send(new GetsUserByIdQuery([_currenctUserId]), ct);
        var user = getUsers.Value?.Data?.FirstOrDefault();
        var subSystem = "مهندسی";
        return $"{subSystem} - {user?.FullName} - {status.GetEnumDescription()} - {requestDescription}";
    }

    private long? GetCompanyId()
    {
        var userCompanyId = _userInfoService.UserCompanyId;
        return userCompanyId <= 0 || userCompanyId == null ? null : userCompanyId;
    }

    private async Task<bool> IsCompanyValid(long? companyId, CT ct)
    {
        if (companyId is not null && companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            return companyResponse.IsSuccess;
        }
        return true;
    }

    private (List<long>? deletedIds, bool checkData) CheckDeletedData(List<UpdateRequestGoodsSupplyDetailModel> details)
    {
        var deletedDetailIds = details.Where(c => c.RequestGoodsSupplyDetailId != null && c.IsDeleted).Select(s => s.RequestGoodsSupplyDetailId!.Value).ToList();
        bool checkAnotherData = true;
        if (deletedDetailIds.Count == details.Count)
            checkAnotherData = true;
        else
            checkAnotherData = false;

        return (deletedDetailIds, checkAnotherData);
    }

    private (List<long>? deletedIds, bool checkData) CheckProjectDeletedData(List<UpdateProjectRequestGoodsSupplyDetailModel> details)
    {
        var deletedDetailIds = details.Where(c => c.RequestGoodsSupplyDetailId != null && c.IsDeleted).Select(s => s.RequestGoodsSupplyDetailId!.Value).ToList();
        bool checkAnotherData = true;
        if (deletedDetailIds.Count == details.Count)
            checkAnotherData = true;
        else
            checkAnotherData = false;

        return (deletedDetailIds, checkAnotherData);
    }


    private async Task<Result<(RGSupplyImportResponse? result, List<ViewProduct>? products)>> ValidateRGSupply(List<RGSupplyImportModel> request, ProjectOperation pO, CT ct)
    {
        var productCodes = request.Select(x => x.ProductCode).ToList();

        var products = await _viewProductRepository.GetProductByCodes(productCodes, ct);
        if (products is null)
            return Result.Failure<(RGSupplyImportResponse, List<ViewProduct>)>(RequestGoodsSupplyErrors.ProductsNotValid)!;

        var productSet = new HashSet<string>(products.Select(x => x.Code));
        var cStandards = await _consumptionStandardProductRepository.GetProductByOprationInfoId(pO.OperationInfoId, ct);
        if (cStandards is null)
            return Result.Failure<(RGSupplyImportResponse, List<ViewProduct>)>(ProjectErrors.OIDoesNotHaveProduct)!;
        List<string> missingAssignedCodes = [];

        var unitIds = cStandards.Listed(x => x.ProductUnitId);
        missingAssignedCodes = products
                .Where(p => !unitIds.Contains(p.Group.CategoryId) && !unitIds.Contains(p.GroupId))
                .Listed(p => p.Code);
        var missingProductCodes = productCodes.Where(x => !productSet.Contains(x)).ToList();

        if ((missingProductCodes is not null && missingProductCodes.Count() > 0) ||
            missingAssignedCodes is not null && missingAssignedCodes.Count() > 0)
        {
            var errorModel = request.Adapt<List<RGSupplyImportErrorResponseModel>>();

            var errors = errorModel.Where(x => missingProductCodes is not null && missingProductCodes.Contains(x.ProductCode));
            var assignedErrors = errorModel.Where(x => missingAssignedCodes is not null && missingAssignedCodes.Contains(x.ProductCode));

            if (missingProductCodes is not null && missingProductCodes.Count > 0)
                foreach (var item in errors)
                {
                    var messages = new List<string>();

                    if (string.IsNullOrWhiteSpace(item.ProductCode))
                        messages.Add($"کد کالا وارد نشده است.");
                    else if (!string.IsNullOrWhiteSpace(item.ProductCode) &&
                        missingProductCodes.Contains(item.ProductCode))
                        messages.Add($"کالای '{item.ProductCode}' فعال و یا موجود نیست.");

                    item.Message = messages.JoinList();
                }

            if (missingAssignedCodes is not null && missingAssignedCodes.Count > 0)
                foreach (var item in assignedErrors)
                {
                    var messages = new List<string>();

                    if (string.IsNullOrWhiteSpace(item.ProductCode))
                        messages.Add($"کد کالا وارد نشده است.");
                    else if (!string.IsNullOrWhiteSpace(item.ProductCode) &&
                        missingAssignedCodes.Contains(item.ProductCode))
                        messages.Add($"کالای'{item.ProductCode}' به شرح عملیات تخصیص داده نشده است.");

                    item.Message = messages.JoinList();
                }

            errorModel = errorModel.OrderByDescending(x => x.Message != null).ToList();
            var result = new FileContentResult(
                GenericExporter.ExportToExcel<
                RGSupplyImportErrorResponseModel,
                RGSupplyImportErrorResponseEnum>(
                errorModel!,
                Enum.GetValues<RGSupplyImportErrorResponseEnum>().ToList(),
                "اکسل ارورها"),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"ProductInventories-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
                LastModified = DateTime.UtcNow
            };
            return (new RGSupplyImportResponse(false, result), null);
        }

        return (new RGSupplyImportResponse(true, null), products);
    }

    private async Task<Result<(PRGSupplyImportResponse? result, List<ViewProduct>? products)>> ValidatePRGSupply(List<PRGSupplyImportModel> request, long projectId, CT ct)
    {
        var productCodes = request.Select(x => x.ProductCode).ToList();

        var products = await _viewProductRepository.GetProductByCodes(productCodes, ct);
        if (products is null)
            return Result.Failure<(PRGSupplyImportResponse, List<ViewProduct>)>(RequestGoodsSupplyErrors.ProductsNotValid)!;

        var productSet = new HashSet<string>(products.Select(x => x.Code));
        var pProducts = await _pProductRepo.GetProductByProjectId(projectId, ct);
        if (pProducts is null)
            return Result.Failure<(PRGSupplyImportResponse, List<ViewProduct>)>(ProjectErrors.ProjectDoesNotHaveProduct)!;

        List<string> missingAssignedCodes = [];
        if (pProducts.Any(x => x.ProjectProductType == ProjectProductType.ProductGroup))
        {
            var groupIds = pProducts.Listed(x => x.ProductGroupId);
            missingAssignedCodes = products.Where(x => !groupIds.Contains(x.GroupId)).Listed(x => x.Code);
        }
        else if (pProducts.Any(x => x.ProjectProductType == ProjectProductType.Category))
        {
            var catIds = pProducts.Listed(x => x.ProductCategoryId);
            missingAssignedCodes = products
                .Where(p => !catIds.Contains(p.Group.CategoryId))
                .Listed(p => p.Code);
        }

        var missingProductCodes = productCodes.Where(x => !productSet.Contains(x)).ToList();

        if ((missingProductCodes is not null && missingProductCodes.Count() > 0) ||
            missingAssignedCodes is not null && missingAssignedCodes.Count() > 0)
        {
            var errorModel = request.Adapt<List<PRGSupplyImportErrorResponseModel>>();

            var errors = errorModel.Where(x => missingProductCodes is not null && missingProductCodes.Contains(x.ProductCode));
            var assignedErrors = errorModel.Where(x => missingAssignedCodes is not null && missingAssignedCodes.Contains(x.ProductCode));

            if (missingProductCodes is not null && missingProductCodes.Count > 0)
                foreach (var item in errors)
                {
                    var messages = new List<string>();

                    if (string.IsNullOrWhiteSpace(item.ProductCode))
                        messages.Add($"کد کالا وارد نشده است.");
                    else if (!string.IsNullOrWhiteSpace(item.ProductCode) &&
                        missingProductCodes.Contains(item.ProductCode))
                        messages.Add($"کالای '{item.ProductCode}' فعال و یا موجود نیست.");

                    item.Message = messages.JoinList();
                }

            if (missingAssignedCodes is not null && missingAssignedCodes.Count > 0)
                foreach (var item in assignedErrors)
                {
                    var messages = new List<string>();

                    if (string.IsNullOrWhiteSpace(item.ProductCode))
                        messages.Add($"کد کالا وارد نشده است.");
                    else if (!string.IsNullOrWhiteSpace(item.ProductCode) &&
                        missingAssignedCodes.Contains(item.ProductCode))
                        messages.Add($"کالای'{item.ProductCode}' به پروژه تخصیص داده نشده است.");

                    item.Message = messages.JoinList();
                }

            errorModel = errorModel.OrderByDescending(x => x.Message != null).ToList();
            var result = new FileContentResult(
                GenericExporter.ExportToExcel<
                PRGSupplyImportErrorResponseModel,
                PRGSupplyImportErrorResponseEnum>(
                errorModel!,
                Enum.GetValues<PRGSupplyImportErrorResponseEnum>().ToList(),
                "اکسل ارورها"),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"ProductInventories-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
                LastModified = DateTime.UtcNow
            };
            return (new PRGSupplyImportResponse(false, result), null);
        }

        return (new PRGSupplyImportResponse(true, null), products);
    }

    private async Task<long?> GetCurrentUserOrganizationId(CT ct)
    {
        var thirdParties = await _thirdPartyRepo.GetByUserIds(new List<long> { _currenctUserId }, ct);
        return thirdParties?.FirstOrDefault()?.OrganizationId;
    }
}
