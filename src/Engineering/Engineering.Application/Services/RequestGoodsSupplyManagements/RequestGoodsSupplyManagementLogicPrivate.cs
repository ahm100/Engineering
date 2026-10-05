using Engineering.Application.RequestGoodsSupplyManagements.Commands.CreateRequestGoodsSupplyManagement;
using Engineering.Application.RequestGoodsSupplyManagements.Commands.UpdateRequestGoodsSupplyManagement;
using Engineering.Application.RequestGoodsSupplyManagements.Models.GetFilteredManagementRequestGoodsSupplies;
using Engineering.Application.RequestGoodsSupplyManagements.Models.UpdateRequestGoodsSupplyManagement;
using Engineering.Application.Services.CostCenterWarehouses.Queries.GetsByCostCenterId;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetRequestGoodsSupplyManagementById;
using Engineering.Application.WebServices.Commercial.Commerces.Commands.CreateCommerce;
using Engineering.Application.WebServices.Commercial.Commerces.Commands.RemoveCommerce;
using Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCommerce;
using Engineering.Application.WebServices.MetaDataServices.Companies.Queries.GetFilteredCompaniesByIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredWarehousesByGroupId;
using Engineering.Application.WebServices.WarehouseServices.Invoices.Commands.RemoveInvoice;
using Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForConsumes;
using Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForRelocation;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Queries.GetsUserById;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.RequestGoodsSupplyDetailManagements;

public partial class RequestGoodsSupplyManagementLogic : IRequestGoodsSupplyManagementLogic
{
    private async Task<Result<bool>> UpdateSupplyManagement(
        UpdateRequestGoodsSupplyManagementRequest request,
        CT ct)
    {
        var managementRespose = await _mediator.Send(new GetRequestGoodsSupplyManagementByIdQuery(request.RequestGoodsSupplyManagementId), ct);
        if (managementRespose.IsFailure)
            return Result.Failure<bool>(managementRespose.Error!);
        var supplyManagement = managementRespose.Value!;
        var requestGoodsSupply = supplyManagement.RequestGoodsSupplyProduct!.RequestGoodsSupply;
        var costCenterId = requestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id;
        var projectId = requestGoodsSupply.ProjectOperation.Project.Id;
        var projectOperationId = requestGoodsSupply.ProjectOperation.Id;
        var supplyProduct = supplyManagement.RequestGoodsSupplyProduct;
        var projectOperationDetailId = supplyProduct.RequestGoodsSupplyDetails.FirstOrDefault()!.ConsumableVolumeProduct.ProjectOperationDetail.Id;
        var groupId = supplyProduct.ProductGroupId;
        var productId = supplyManagement.ReferenceId!.Value;

        var sourceWarehouseId = request.WarehouseId;
        var destinationWarehouseId = request.DestinationWarehouseId;

        if (supplyManagement.Status != GoodsSupplyManagementStatus.Return && supplyManagement.Status != GoodsSupplyManagementStatus.ReturnToSupply)
            return Result.Failure<bool>(RequestGoodsSupplyManagementErrors.InValidRequestGoodsSupplyManagementStatus);

        var currentUser = _userProfileService.GetProfileInfo();
        var inviceIds = new List<long>();
        var commerceIds = new List<long>();

        var sumRequestedCount = supplyProduct.RequestGoodsSupplyManagements
            .Where(oo => oo.Status != GoodsSupplyManagementStatus.Return && oo.Status != GoodsSupplyManagementStatus.ReturnToSupply).Sum(oo => oo.RequestedCount);

        if (supplyProduct.RequestedCount < (sumRequestedCount + request.RequestedCount))
            return Result.Failure<bool>(RequestGoodsSupplyManagementErrors.InValidRequestedCount);

        var queryProducts = await _mediator.Send(new GetsProductByIdQuery(1, 1, null, [productId]), ct);
        if (queryProducts.IsFailure)
        {
            var managment = await RollBackManagment(inviceIds, commerceIds, ct);
            return Result.Failure<bool>(queryProducts.Error!);
        }
        var product = queryProducts.Value!.Data!.FirstOrDefault()!;

        long? invoiceId = null;
        if (supplyProduct.RequestedCount > 0 && supplyProduct.PackageCount <= 0)
            supplyProduct.SetPackageCount(supplyProduct.RequestedCount);
        if (request.Type == GoodsSupplyManagementType.Commerce)
        {
            if (request.DestinationWarehouseId is not null)
            {
                var getWarehouse = await _mediator.Send(new GetFilteredWarehousesByGroupIdQuery([request.DestinationWarehouseId.Value], groupId, null, 1, 10), ct);
                if (getWarehouse.IsFailure || getWarehouse.Value is null || getWarehouse.Value.Data is null)
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<bool>(RequestGoodsSupplyDetailErrors.InValidWarehouse);
                }
            }
            decimal requestedCount = supplyProduct.PackageId != null && supplyProduct.PackageId > 0 ? (decimal)supplyProduct.PackageCount! : supplyProduct.RequestedCount;
            List<CreateCommerceRequestRequestDocument> documents = [];
            var docs = supplyProduct.RequestGoodsSupplyDetails.SelectMany(x => x.RequestGoodsSupplyDetailDocuments).ToList();
            foreach (var item in docs)
                documents.Add(new(null, item.Url, false));

            invoiceId = supplyManagement.Type == GoodsSupplyManagementType.Commerce ? supplyManagement.InvoiceId : null;
            var createCommerce = await _mediator.Send(new CreateCommerceCommand(invoiceId, requestGoodsSupply, supplyProduct, costCenterId, projectId, projectOperationId,
                [projectOperationDetailId], requestedCount, destinationWarehouseId, documents, false, currentUser.UserId, request.Description), ct);
            if (createCommerce.IsFailure)
            {
                var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                return Result.Failure<bool>(createCommerce.Error!);
            }
            invoiceId = createCommerce.Value!.Id;
            commerceIds.Add(createCommerce.Value!.Id);
        }
        else
        {
            long measureUnitId = product.Group.MeasureId;
            List<InvoiceRequestItemDto>? items = null;

            var invoiceExitProduct = new InvoiceExitProductDto()
            {
                ProductId = supplyManagement.ReferenceId!.Value,
                MeasureId = measureUnitId,
                Quantity = (double)request.RequestedCount,
                Items = items,
                ServiceReferenceId = null,
                RetrunDate = DateTime.UtcNow,
            };
            List<InvoiceExitProductDto> invoiceExitProducts = [invoiceExitProduct];

            var documents = new List<WebServices.WarehouseServices.Invoices.Models.DocumentInvoiceModel>();
            foreach (var item in requestGoodsSupply.RequestGoodsSupplyDetails.SelectMany(oo => oo.RequestGoodsSupplyDetailDocuments))
                documents.Add(new WebServices.WarehouseServices.Invoices.Models.DocumentInvoiceModel(item.Url));

            var costCenterWarehousesData = await _mediator.Send(new GetsCostCenterWarehouseByCostCenterIdQuery(costCenterId, 1, 100), ct);
            if (costCenterWarehousesData.IsFailure || costCenterWarehousesData.Value is null || costCenterWarehousesData.Value.Data is null)
            {
                var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                return Result.Failure<bool>(RequestGoodsSupplyDetailErrors.InValidWarehouse);
            }
            var costCenterWarehouses = costCenterWarehousesData.Value.Data;

            if (request.Type == GoodsSupplyManagementType.InStock)
            {
                if (!costCenterWarehouses.Any(x => x.WarehouseId == destinationWarehouseId))
                    return Result.Failure<bool>(RequestGoodsSupplyManagementErrors.InValidCostCenterInvoice);

                var getsWarehouse = await _mediator.Send(new GetFilteredWarehousesByGroupIdQuery([(long)destinationWarehouseId!], groupId, null, 1, 10), ct);
                if (getsWarehouse.IsFailure || getsWarehouse.Value is null || getsWarehouse.Value.Data is null || getsWarehouse.Value.Data.Count <= 0)
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<bool>(RequestGoodsSupplyDetailErrors.WarehouseCanNotSuportProduct);
                }

                var products = invoiceExitProducts.Adapt<List<InvoiceExitProductDtoNoManagement>>();
                var createExitForConsumes = await _mediator.Send(new CreateExitForConsumesCommand(destinationWarehouseId!.Value, request.Description, requestGoodsSupply.CreatorId,
                    (ImportanceDegree)supplyProduct.Importance!, supplyProduct.Id.ToString(), supplyProduct.RequestSerialNumber, products, documents, false, supplyProduct.ContractorId), ct);
                if (createExitForConsumes.IsFailure)
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<bool>(createExitForConsumes.Error!);
                }
                invoiceId = createExitForConsumes.Value!.Id;
            }

            if (request.Type == GoodsSupplyManagementType.BetweenStock)
            {
                if (sourceWarehouseId is null)
                    sourceWarehouseId = destinationWarehouseId;

                if (sourceWarehouseId is null || sourceWarehouseId <= 0)
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<bool>(RequestGoodsSupplyManagementErrors.DestinationWarehouseIdIsNull);
                }

                if (!costCenterWarehouses.Any(x => x.WarehouseId == supplyManagement.DestinationWarehouseId))
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<bool>(RequestGoodsSupplyManagementErrors.DestinationWarehouseNotInCostCenter);
                }

                if (sourceWarehouseId is not null && sourceWarehouseId > 0)
                {
                    var getsWarehouse = await _mediator.Send(new GetFilteredWarehousesByGroupIdQuery([(long)sourceWarehouseId!], groupId, null, 1, 10), ct);
                    if (getsWarehouse.IsFailure || getsWarehouse.Value is null || getsWarehouse.Value.Data is null || getsWarehouse.Value.Data.Count <= 0)
                    {
                        var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                        return Result.Failure<bool>(RequestGoodsSupplyDetailErrors.WarehouseCanNotSuportProduct);
                    }
                }

                if (destinationWarehouseId == sourceWarehouseId)
                {
                    var chosenWarehouseIds = costCenterWarehouses.Where(x => x.WarehouseId != sourceWarehouseId).Select(x => x.WarehouseId).ToList();
                    var getsWarehouse = await _mediator.Send(new GetFilteredWarehousesByGroupIdQuery(chosenWarehouseIds, groupId, null, 1, 10), ct);
                    if (getsWarehouse.IsFailure || getsWarehouse.Value is null || getsWarehouse.Value.Data is null || getsWarehouse.Value.Data.Count <= 0)
                    {
                        var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                        return Result.Failure<bool>(RequestGoodsSupplyDetailErrors.CostCenteWarehousesCanNotSuportProduct);
                    }

                    var inventories = getsWarehouse.Value.Data;
                    var defaultId = costCenterWarehouses.Where(x => x.IsDefault).Select(x => x.WarehouseId).FirstOrDefault();
                    if (inventories.Any(x => x.Id == defaultId))
                        destinationWarehouseId = defaultId;
                    else
                        destinationWarehouseId = inventories.FirstOrDefault()!.Id;
                }

                var products = invoiceExitProducts.Adapt<List<InvoiceExitProductDtoNoManagement>>();
                var createExitForRelocation = await _mediator.Send(new CreateExitForRelocationCommand(sourceWarehouseId!.Value, destinationWarehouseId!.Value, request.Description,
                    (ImportanceDegree)supplyProduct.Importance!, supplyProduct.Id.ToString(), supplyProduct.RequestSerialNumber, products, documents,
                    requestGoodsSupply.CreatorId, false, supplyProduct.ContractorId), ct);
                if (createExitForRelocation.IsFailure)
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<bool>(createExitForRelocation.Error!);
                }
                invoiceId = createExitForRelocation.Value!.Id;
                request.WarehouseId = request.DestinationWarehouseId;
            }
        }

        var lastDescription = await DescriptionMacker(request.Description, supplyManagement.Type, GoodsSupplyManagementStatus.PendingForConfirme, ct);
        var response = await _mediator.Send(new UpdateRequestGoodsSupplyManagementCommand(request.RequestGoodsSupplyManagementId, sourceWarehouseId,
            destinationWarehouseId, invoiceId, request.RequestedCount, request.Type, request.Description, lastDescription), ct);
        if (response.IsFailure)
        {
            var managment = await RollBackManagment(inviceIds, commerceIds, ct);
            return Result.Failure<bool>(response.Error!);
        }

        return true;
    }

    private async Task<Result<CreateRequestGoodsSupplyManagementModel?>> CreateCommerceGoodsSupplyProduct(
        RequestGoodsSupplyProduct value,
        List<CostCenterWarehouse> costCenterWarehouses,
        long? userId,
        GoodsSupplyProductModelCommerce commerce,
        long costCenterId,
        long projectId,
        long? projectOperationId,
        CT ct)
    {
        try
        {
            long productId = value.ProductId;
            List<long>? projectOperationDetailIds = [];
            if (!value.RequestGoodsSupply.IsProjectSupply)
                projectOperationDetailIds = value.RequestGoodsSupplyDetails.Select(x => x.ConsumableVolumeProduct.ProjectOperationDetail.Id).ToList();

            var groupId = value.ProductGroupId;
            var commerceType = GoodsSupplyManagementType.Commerce;

            if (!costCenterWarehouses.Any(x => x.WarehouseId == commerce.DestinationWarehouseId))
                return Result.Failure<CreateRequestGoodsSupplyManagementModel>(RequestGoodsSupplyDetailErrors.WarehouseIsUnvaild);

            var getsWarehouse = await _mediator.Send(new GetFilteredWarehousesByGroupIdQuery([commerce.DestinationWarehouseId], groupId, null, 1, 100), ct);
            if (getsWarehouse.IsFailure || getsWarehouse.Value is null || getsWarehouse.Value.Data is null)
                return Result.Failure<CreateRequestGoodsSupplyManagementModel>(RequestGoodsSupplyDetailErrors.CostCenterWarehouseCanNotAsset);

            var createInvice = value.RequestGoodsSupply.Type == GoodsSupplyType.GoodsSupply ? false : true;

            decimal requestedCount = commerce.RequestedCount;
            if (value.PackageId != null && value.PackageId > 0)
            {
                var packagesInfo = await WebServicesLogic.PackagesDataReceiver([value.PackageId!.Value], _mediator, ct);
                var packageInfo = packagesInfo!.FirstOrDefault();
                requestedCount = commerce.RequestedCount / packageInfo!.Quantity;
            }

            List<CreateCommerceRequestRequestDocument> documents = [];
            var docs = value.RequestGoodsSupplyDetails.SelectMany(x => x.RequestGoodsSupplyDetailDocuments).Distinct().ToList();
            foreach (var item in docs)
                documents.Add(new(null, item.Url, false));

            var createCommerce = await _mediator.Send(new CreateCommerceCommand(null, value.RequestGoodsSupply, value, costCenterId, projectId, projectOperationId, projectOperationDetailIds,
                requestedCount, commerce.DestinationWarehouseId, documents, createInvice, userId, commerce.CommerceDescription), ct);
            if (createCommerce.IsFailure || createCommerce!.Value is null)
                return Result.Failure<CreateRequestGoodsSupplyManagementModel>(createCommerce.Error!);

            var management = new CreateRequestGoodsSupplyManagementModel()
            {
                Detail = value,
                ProductId = productId,
                DestinationWarehouseId = commerce.DestinationWarehouseId,
                InvoiceId = createCommerce.Value?.Id,
                RequestedCount = commerce.RequestedCount,
                AlternateId = null,
                Type = commerceType,
                Description = commerce.CommerceDescription
            };
            return management;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CreateRequestGoodsSupplyManagementModel>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<CreateRequestGoodsSupplyManagementModel>?>> CreateInStocksManagementProduct(
        RequestGoodsSupplyProduct value,
        List<GoodsSupplyProductModelInStock> inStock,
        List<CostCenterWarehouse> costCenterWarehouses,
        long ownerId,
        List<long> inviceIds,
        List<long> commerceIds,
        CT ct)
    {
        try
        {
            var managements = new List<CreateRequestGoodsSupplyManagementModel>();
            foreach (var item in inStock)
            {
                var warehouseId = item.WarehouseId;
                if (!costCenterWarehouses.Any(x => x.WarehouseId == warehouseId))
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<List<CreateRequestGoodsSupplyManagementModel>>(RequestGoodsSupplyDetailErrors.WarehouseIsUnvaild);
                }

                var getsWarehouse = await _mediator.Send(new GetFilteredWarehousesByGroupIdQuery([warehouseId], value.ProductGroupId, null, 1, 10), ct);
                if (getsWarehouse.IsFailure || getsWarehouse.Value is null || getsWarehouse.Value.Data is null)
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<List<CreateRequestGoodsSupplyManagementModel>>(RequestGoodsSupplyDetailErrors.InValidWarehouse);
                }

                var documents = new List<WebServices.WarehouseServices.Invoices.Models.DocumentInvoiceModel>();
                var docs = value.RequestGoodsSupplyDetails.SelectMany(x => x.RequestGoodsSupplyDetailDocuments).ToList();
                foreach (var doc in docs)
                    documents.Add(new WebServices.WarehouseServices.Invoices.Models.DocumentInvoiceModel(doc.Url));

                var productQuery = await _productRepo.GetProductByIds([value.ProductId], ct);
                if (productQuery is null || productQuery.Count < 1)
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<List<CreateRequestGoodsSupplyManagementModel>>(RequestGoodsSupplyErrors.ProdouctNotFound);
                }
                var measureId = productQuery.FirstOrDefault()!.Group.MeasureId;

                var management = new CreateRequestGoodsSupplyManagementModel()
                {
                    Detail = value,
                    ProductId = value.ProductId,
                    DestinationWarehouseId = warehouseId,
                    InvoiceId = null,
                    RequestedCount = item.RequestedCount,
                    AlternateId = null,
                    Type = GoodsSupplyManagementType.InStock,
                    Description = item.Description
                };

                var products = new InvoiceExitProductDtoNoManagement()
                {
                    ProductId = value.ProductId,
                    MeasureId = measureId,
                    Quantity = (double)item.RequestedCount,
                    Items = null,
                    ServiceReferenceId = null,
                    RetrunDate = DateTime.UtcNow,
                };

                var createExit = await _mediator.Send(new CreateExitForConsumesCommand(
                        item.WarehouseId,
                        item.Description,
                        value.CreatorId,
                        (ImportanceDegree)value.Importance!,
                        value.Id.ToString(),
                        value.RequestSerialNumber,
                        [products],
                        documents,
                        false,
                        ownerId), ct);
                if (createExit.IsFailure || createExit.Value is null)
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<List<CreateRequestGoodsSupplyManagementModel>>(createExit.Error!);
                }

                var invoice = createExit.Value;
                inviceIds.Add(invoice.Id);
                management.InvoiceId = invoice.Id;
                managements.Add(management);

            }
            return managements;
        }
        catch (Exception ex)
        {
            var managment = await RollBackManagment(inviceIds, commerceIds, ct);

            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<CreateRequestGoodsSupplyManagementModel>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<CreateRequestGoodsSupplyManagementModel>?>> CreateBetweenStocksManagementProduct(
        RequestGoodsSupplyProduct value,
        List<GoodsSupplyProductModelBetweenStock> betweenStock,
        List<CostCenterWarehouse> costCenterWarehouses,
        long ownerId,
        List<long> inviceIds,
        List<long> commerceIds,
        CT ct)
    {
        try
        {
            var managements = new List<CreateRequestGoodsSupplyManagementModel>();
            foreach (var item in betweenStock)
            {
                var warehouseId = item.WarehouseId;
                var destinationWarehouseId = item.DestinationWarehouseId;

                if (!costCenterWarehouses.Any(x => x.WarehouseId == destinationWarehouseId))
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<List<CreateRequestGoodsSupplyManagementModel>>(RequestGoodsSupplyDetailErrors.WarehouseIsUnvaild);
                }

                var getsWarehouse = await _mediator.Send(new GetFilteredWarehousesByGroupIdQuery([destinationWarehouseId], value.ProductGroupId, null, 1, 10), ct);
                if (getsWarehouse.IsFailure || getsWarehouse.Value is null || getsWarehouse.Value.Data is null)
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<List<CreateRequestGoodsSupplyManagementModel>>(RequestGoodsSupplyDetailErrors.InValidWarehouse);
                }

                var documents = new List<WebServices.WarehouseServices.Invoices.Models.DocumentInvoiceModel>();
                var docs = value.RequestGoodsSupplyDetails.SelectMany(x => x.RequestGoodsSupplyDetailDocuments).ToList();
                foreach (var doc in docs)
                    documents.Add(new WebServices.WarehouseServices.Invoices.Models.DocumentInvoiceModel(doc.Url));

                var productQuery = await _mediator.Send(new GetsProductByIdQuery(1, 1, null, [value.ProductId]), ct);
                if (productQuery.IsFailure)
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<List<CreateRequestGoodsSupplyManagementModel>>(RequestGoodsSupplyErrors.ProdouctNotFound);
                }
                var measureId = productQuery.Value!.Data!.FirstOrDefault()!.Group.MeasureId;
                var management = new CreateRequestGoodsSupplyManagementModel()
                {
                    Detail = value,
                    ProductId = value.ProductId,
                    WarehouseId = warehouseId,
                    DestinationWarehouseId = destinationWarehouseId,
                    InvoiceId = null,
                    RequestedCount = item.RequestedCount,
                    AlternateId = null,
                    Type = GoodsSupplyManagementType.BetweenStock,
                    Description = item.Description
                };

                var product = new InvoiceExitProductDtoNoManagement()
                {
                    ProductId = value.ProductId,
                    MeasureId = measureId,
                    Quantity = (double)item.RequestedCount,
                    Items = null,
                    ServiceReferenceId = null,
                    RetrunDate = DateTime.UtcNow,
                };

                var createRelocation = await _mediator.Send(new CreateExitForRelocationCommand(
                    warehouseId,
                    destinationWarehouseId,
                    item.Description,
                    (ImportanceDegree)value.Importance!,
                    value.Id.ToString(),
                    value.RequestSerialNumber,
                    [product],
                    documents,
                    value.CreatorId,
                    false,
                    ownerId), ct);
                if (createRelocation.IsFailure || createRelocation.Value is null)
                {
                    var managment = await RollBackManagment(inviceIds, commerceIds, ct);
                    return Result.Failure<List<CreateRequestGoodsSupplyManagementModel>>(createRelocation.Error!);
                }

                var invoice = createRelocation.Value!;
                inviceIds.Add(invoice.Id);
                management.InvoiceId = invoice.Id;
                managements.Add(management);
            }
            return managements;
        }
        catch (Exception ex)
        {
            var managment = await RollBackManagment(inviceIds, commerceIds, ct);

            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<CreateRequestGoodsSupplyManagementModel>>(SharedErrors.UnknownError);
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private ProductRequestReviewerModel RequestReviewer(RequestGoodsSupply item)
    {
        ProductRequestReviewerModel reviewerModel = new();
        if (item.RequestGoodsSupplyDetails is not null && item.RequestGoodsSupplyDetails.Count > 0)
        {
            var completeSupply = GoodsSupplyManagementStatus.CompleteSupply;
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            var inCompleteSupply = GoodsSupplyManagementStatus.InCompleteSupply;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
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

    private async Task<bool> RollBackManagment(List<long>? inviceIds, List<long>? commerceIds, CT ct)
    {
        if (inviceIds is not null && inviceIds.Count > 0)
            foreach (var item in inviceIds)
            {
                var pipeline = _resilience.GetPipeline<Result>("RetryPipeline");
                var deleteResponse = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(new RemoveInvoiceCommand(item), ct), ct);
                if (deleteResponse.IsFailure)
                    return false;
            }

        if (commerceIds is not null && commerceIds.Count > 0)
            foreach (var item in commerceIds)
            {
                var pipeline = _resilience.GetPipeline<Result>("RetryPipeline");
                var deleteResponse = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(new RemoveCommerceCommand(item), ct), ct);
                if (deleteResponse.IsFailure)
                    return false;
            }

        return true;
    }

    private async Task<List<Company>?> CompanyDataReceiver(List<long?>? companyIds, CT ct)
    {
        List<Company>? companies = [];
        List<long> ids = companyIds.Adapt<List<long>>().Distinct().ToList();
        if (ids is not null && ids.Count > 0)
        {
            var companyResponse = await _mediator.Send(new GetFilteredCompaniesByIdsQuery(ids, null, 1, ids.Count), ct);
            companies = companyResponse.Value!.Data;
            return companies;
        }
        else
            return null;
    }

    private async Task<string> DescriptionMacker(string? requestDescription, GoodsSupplyManagementType type, GoodsSupplyManagementStatus status, CT ct)
    {
        var getUsers = await _mediator.Send(new GetsUserByIdQuery([_currenctUserId]), ct);
        var user = getUsers.Value?.Data?.FirstOrDefault();
        var subSystem = "مهندسی";
        return $"{subSystem} - {user?.FullName} - {status.GetEnumDescription()} {type.GetEnumDescription()} - {requestDescription}";
    }

    private async Task<string> DescriptionMacker(string? requestDescription, GoodsSupplyDetailStatus status, CT ct)
    {
        var getUsers = await _mediator.Send(new GetsUserByIdQuery([_currenctUserId]), ct);
        var user = getUsers.Value?.Data?.FirstOrDefault();
        var subSystem = "مهندسی";
        return $"{subSystem} - {user?.FullName} - {status.GetEnumDescription()} - {requestDescription}";
    }

}
