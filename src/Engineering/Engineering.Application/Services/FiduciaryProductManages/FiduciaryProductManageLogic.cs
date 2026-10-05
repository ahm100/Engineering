using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Services.CostCenterWarehouses.Queries.GetsByCostCenterId;
using Engineering.Application.Services.FiduciaryProductDetails.Commands.UpdateConfirmedFiduciaryProductDetail;
using Engineering.Application.Services.FiduciaryProductManages.Commands.CreateFiduciaryProductDetailManagement;
using Engineering.Application.Services.FiduciaryProductManages.Commands.CreateFiduciaryProductDetailReturn;
using Engineering.Application.Services.FiduciaryProductManages.Commands.CreateFiduciaryProductDetailReturnDocument;
using Engineering.Application.Services.FiduciaryProductManages.Models.CreateFiduciaryProductDetailReturn;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailManagementStatus;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnByDetailId;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnDocumentById;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnType;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFilteredFiduciaryProductManageWarehouses;
using Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductConfirmed;
using Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductPending;
using Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductRejected;
using Engineering.Application.Services.FiduciaryProductManages.Queries.GetFiduciaryProductDetailReturnDocuments;
using Engineering.Application.Services.FiduciaryProductManages.Queries.GetFiduciaryProductManagementById;
using Engineering.Application.Services.FiduciaryProducts.Commands.SetFiduciaryProductDetailDelivary;
using Engineering.Application.Services.FiduciaryProducts.Commands.UpdateFiduciaryProductDetailStatus;
using Engineering.Application.Services.FiduciaryProducts.Commands.UpdateFiduciaryProductStatus;
using Engineering.Application.Services.FiduciaryProducts.Models.SetFiduciaryProductDetailDelivary;
using Engineering.Application.Services.FiduciaryProducts.Models.SetFiduciaryProductDetailNoDelivary;
using Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductById;
using Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductDetailById;
using Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductDetailReturnByDetailId;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Application.WebServices.WarehouseServices.InvoiceProducts.Queries;
using Engineering.Application.WebServices.WarehouseServices.Invoices.Commands.CreateEntryThroughBorrow;
using Engineering.Application.WebServices.WarehouseServices.Invoices.Commands.CreateExitForBorrows;
using Engineering.Application.WebServices.WarehouseServices.Invoices.Models;
using Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForConsumes;
using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Inventories.Queries.GetInventoryByProductId;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Products.Queries.GetById;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Queries.GetsById;
using WarehouseEntity = Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Models.Warehouse;

namespace Engineering.Application.Services.FiduciaryProductManages;

public partial class FiduciaryProductManageLogic : IFiduciaryProductManageLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<FiduciaryProductManageLogic> _logger;
    private readonly IUserProfileService _userProfileServiced;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IViewProductRepository _pRepo;
    private readonly long _currenctUserId;
    public FiduciaryProductManageLogic(
        IMediator mediator,
        ILogger<FiduciaryProductManageLogic> logger,
        IUnitOfWork unitOfWork,
        IUserProfileService userProfileServiced,
        IViewProductRepository pRepo)
    {
        _mediator = mediator;
        _logger = logger;
        _userProfileServiced = userProfileServiced;
        _unitOfWork = unitOfWork;
        _pRepo = pRepo;
        _currenctUserId = _userProfileServiced.GetProfileInfo().UserId;
    }

    public async Task<Result<CreateFiduciaryProductDetailReturnResponse?>> CreateFiduciaryProductDetailReturn(
        CreateFiduciaryProductDetailReturnRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateFiduciaryProductDetailReturnValidator, CreateFiduciaryProductDetailReturnRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(isValidRequest.Error!);

        var requestIds = request.Data.Select(oo => oo.Id).ToList();
        var getManagement = await _mediator.Send(new GetFiduciaryProductManagementByIdsQuery(requestIds), ct);
        if (getManagement.IsFailure || getManagement.Value is null || getManagement.Value.Count <= 0)
            return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(getManagement.Error!);
        var managements = getManagement.Value!;

        var fiduciaryProduct = managements.FirstOrDefault()!.FiduciaryProductDetail.FiduciaryProduct;
        var details = managements.Select(oo => oo.FiduciaryProductDetail).ToList();

        var thirdPartyResponse = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, new List<long>() { fiduciaryProduct.ThirdPartyId }, null, false, null), ct);
        if (thirdPartyResponse.IsFailure)
            return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(thirdPartyResponse.Error!);
        var thirdParty = thirdPartyResponse.Value!.Data!.FirstOrDefault()!;

        foreach (var detail in details)
        {
            var managementIds = detail.Managements.Select(oo => oo.Id).ToList();
            var requestReturnCount = request.Data.Where(oo => managementIds.Contains(oo.Id)).Sum(oo => oo.ReturnCount);
            var sumConfirmedLoanCount = detail.Managements.Sum(oo => oo.ConfirmedLoanCount);
            var sumReturnCount = detail.Managements.SelectMany(oo => oo.Returns).Sum(oo => oo.ReturnCount);

            if (sumConfirmedLoanCount < (sumReturnCount + requestReturnCount))
                return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(FiduciaryProductDetailReturnErrors.InValidCount);

            if (sumConfirmedLoanCount >= (sumReturnCount + requestReturnCount))
            {
                var description = await DetailDescriptionMacker("عودت کالا", FiduciaryProductDetailStatus.Returned, ct);
                var updateDetailStatusResponse = await _mediator.Send(new UpdateFiduciaryProductDetailStatusCommand(
                        detail.Id,
                        FiduciaryProductDetailStatus.Returned,
                        null,
                        description),
                    ct);
                if (updateDetailStatusResponse.IsFailure)
                    return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(updateDetailStatusResponse.Error!);
            }
        }

        var warehouseIds = managements.Select(oo => (long?)oo.WarehouseId).Distinct().ToList();
        var getsWarehouse = await _mediator.Send(new GetsWarehouseByIdQuery(1, warehouseIds.Count, warehouseIds), ct);
        if (getsWarehouse.IsFailure)
            return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(getsWarehouse.Error!);
        var warehouses = getsWarehouse.Value!.Data!;

        var responses = new List<FiduciaryProductDetailReturn>();
        if (warehouseIds is not null && warehouseIds.Count > 0)
            foreach (var warehouseId in warehouseIds)
            {
                var warehouse = warehouses.SingleOrDefault(oo => oo.Id == warehouseId);
                if (warehouse is null)
                    return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(FiduciaryProductDetailManagementErrors.InValidWarehouseId);

                var manageDetails = managements.Where(oo => oo.WarehouseId == warehouseId).ToList();
                List<InvoiceProductDto> Products = new();

                foreach (var item in manageDetails)
                {
                    var requestReturn = request.Data.FirstOrDefault(oo => oo.Id == item.Id);
                    if (requestReturn is null)
                        return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);

                    var fiduciaryProductManagement = managements.FirstOrDefault(oo => oo.Id == item.Id);
                    if (fiduciaryProductManagement is null)
                        return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(FiduciaryProductDetailErrors.FiduciaryProductDetailWithIdNotFound);

                    var getProduct = await _mediator.Send(new GetProductByIdQuery(item.FiduciaryProductDetail.ProductId), ct);
                    if (getProduct.IsFailure)
                        return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(getProduct.Error!);
                    var product = getProduct.Value!;

                    var invoiceProductResponse = await _mediator.Send(new GetByInvoiceIdQuery(fiduciaryProductManagement.InvoiceId, null, null, null, null, null, 1, 10), ct);
                    if (invoiceProductResponse.IsFailure)
                        return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(invoiceProductResponse.Error!);
                    var invoiceProduct = invoiceProductResponse.Value!.Data!.FirstOrDefault();

                    Products.Add(new InvoiceProductDto(product.Id, invoiceProduct!.PackageId, requestReturn.ReturnCount, null, null, null));
                }

                var createEntryBorrowResponse = await _mediator.Send(new CreateEntryThroughBorrowCommand((long)warehouseId!, null, fiduciaryProduct.Id.ToString(), fiduciaryProduct.Id.ToString(), Products, ImportanceDegree.Medium, fiduciaryProduct.ThirdPartyId, thirdParty.FullName, null, null, true), ct);
                if (createEntryBorrowResponse.IsFailure)
                    return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(createEntryBorrowResponse.Error!);
                var entryBorrow = createEntryBorrowResponse.Value!;

                foreach (var detail in manageDetails)
                {
                    var fiduciaryProductDetail = details.FirstOrDefault(oo => oo.Id == detail.FiduciaryProductDetail.Id);
                    var requestData = request.Data.FirstOrDefault(oo => oo.Id == detail.Id);

                    var createReturnResponse = await _mediator.Send(new CreateFiduciaryProductDetailReturnCommand(entryBorrow.Id, requestData.Description, requestData.ReturnCount, requestData.ReturnDate, requestData.LateDay, requestData.LateFine, requestData.CurrencyId, requestData.Type, detail), ct);
                    if (createReturnResponse.IsFailure)
                        return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(createReturnResponse.Error!);
                    var detailReturn = createReturnResponse.Value!;

                    var createManagementResponse = await _mediator.Send(new CreateFiduciaryProductDetailManagementCommand(entryBorrow.Id, (long)warehouseId!, detail.DestinationWarehouseId, requestData.ReturnCount, null, fiduciaryProductDetail!), ct);
                    if (createManagementResponse.IsFailure)
                        return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(createManagementResponse.Error!);

                    if (requestData.Documents != null && requestData.Documents.Count > 0)
                        foreach (var document in requestData.Documents)
                        {
                            var createReturnDocumentResponse = await _mediator.Send(new CreateFiduciaryProductDetailReturnDocumentCommand(detailReturn, document), ct);
                            if (createReturnDocumentResponse.IsFailure)
                                return Result.Failure<CreateFiduciaryProductDetailReturnResponse>(createReturnDocumentResponse.Error!);
                        }

                    responses.Add(detailReturn);
                }
            }

        await _unitOfWork.CommitAsync(ct);
        var ids = responses.Select(x => x.Id).ToList();
        return new CreateFiduciaryProductDetailReturnResponse(ids, true);
    }

    public async Task<Result<SetFiduciaryProductConfirmedResponse?>> SetFiduciaryProductConfirmed(
        SetFiduciaryProductConfirmedRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetFiduciaryProductConfirmedValidator, SetFiduciaryProductConfirmedRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetFiduciaryProductConfirmedResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetFiduciaryProductByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<SetFiduciaryProductConfirmedResponse>(response.Error!);
        var fiduciaryProduct = response.Value!;
        var details = fiduciaryProduct.Details;

        if (!(ValidateFiduciaryProductStatus.AllowForConfirm.Any(x => x == fiduciaryProduct.Status)))
            return Result.Failure<SetFiduciaryProductConfirmedResponse>(FiduciaryProductErrors.InValidStatus);

        if (!(fiduciaryProduct.Details.Any(z => ValidateFiduciaryProductDetailStatus.AllowForConfirm.Any(x => x == z.Status))))
            return Result.Failure<SetFiduciaryProductConfirmedResponse>(FiduciaryProductDetailErrors.InValidStatus);

        var thirdPartyQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [fiduciaryProduct.ThirdPartyId], null, false, null), ct);
        if (thirdPartyQuery.IsFailure)
            return Result.Failure<SetFiduciaryProductConfirmedResponse>(thirdPartyQuery.Error!);
        var thirdParty = thirdPartyQuery.Value!.Data!.FirstOrDefault()!;

        var requestWarehouseIds = request.Details.Where(x => !x.IsRejected && x.Warehouses != null && x.Warehouses.Count > 0).SelectMany(oo => oo.Warehouses!).Select(oo => oo.WarehouseId).Distinct().ToList();
        List<long?> warehouseIds = requestWarehouseIds.Adapt<List<long?>>();

        List<WarehouseEntity>? requestWarehouses = [];
        if (request.Details.Any(x => !x.IsRejected) && warehouseIds is not null && warehouseIds.Count > 0)
        {
            var getsWarehouse = await _mediator.Send(new GetsWarehouseByIdQuery(1, warehouseIds.Count, warehouseIds), ct);
            if (getsWarehouse.IsFailure)
                return Result.Failure<SetFiduciaryProductConfirmedResponse>(getsWarehouse.Error!);
            requestWarehouses = getsWarehouse.Value!.Data!;
        }

        var detailIds = request.Details.Select(oo => oo.Id).ToList();
        var productIds = details.Where(oo => detailIds.Contains(oo.Id)).Select(oo => oo.ProductId).Distinct().ToList();
        var getProducts = await _mediator.Send(new GetsProductByIdQuery(1, productIds.Count, null, productIds), ct);
        if (getProducts.IsFailure)
            return Result.Failure<SetFiduciaryProductConfirmedResponse>(getProducts.Error!);
        var products = getProducts.Value!.Data!;

        if (request.Details.All(x => x.IsRejected))
        {
            var description = await DescriptionMacker(request.ProductDescription, FiduciaryProductStatus.Rejected, ct);
            var updateStatusResponse = await _mediator.Send(new UpdateFiduciaryProductStatusCommand(request.Id, FiduciaryProductStatus.Rejected, request.ProductDescription, description), ct);
            if (updateStatusResponse.IsFailure)
                return Result.Failure<SetFiduciaryProductConfirmedResponse>(updateStatusResponse.Error!);
        }
        else if (request.Details.All(x => !x.IsRejected))
        {
            var description = await DescriptionMacker(request.ProductDescription, FiduciaryProductStatus.Confirmed, ct);
            var updateStatusResponse = await _mediator.Send(new UpdateFiduciaryProductStatusCommand(request.Id, FiduciaryProductStatus.Confirmed, request.ProductDescription, description), ct);
            if (updateStatusResponse.IsFailure)
                return Result.Failure<SetFiduciaryProductConfirmedResponse>(updateStatusResponse.Error!);
        }
        else
        {
            var description = await DescriptionMacker(request.ProductDescription, FiduciaryProductStatus.IncompleteConfirmed, ct);
            var updateStatusResponse = await _mediator.Send(new UpdateFiduciaryProductStatusCommand(request.Id, FiduciaryProductStatus.IncompleteConfirmed, request.ProductDescription, description), ct);
            if (updateStatusResponse.IsFailure)
                return Result.Failure<SetFiduciaryProductConfirmedResponse>(updateStatusResponse.Error!);
        }

        if (request.Details is not null && request.Details.Count > 0)
            foreach (var detail in request.Details)
            {
                var fiduciaryProductDetail = details.FirstOrDefault(oo => oo.Id == detail.Id);
                if (fiduciaryProductDetail is null)
                    return Result.Failure<SetFiduciaryProductConfirmedResponse>(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);

                if (detail.IsRejected)
                {
                    var descriptionDetail = await DetailDescriptionMacker(detail.RejectedDescription, FiduciaryProductDetailStatus.Rejected, ct);
                    var setRejectedResponse = await _mediator.Send(new UpdateFiduciaryProductDetailStatusCommand(detail.Id, FiduciaryProductDetailStatus.Rejected, detail.RejectedDescription, descriptionDetail), ct);
                    if (setRejectedResponse.IsFailure)
                        return Result.Failure<SetFiduciaryProductConfirmedResponse>(setRejectedResponse.Error!);
                }
                else
                {
                    if (detail.ConfirmedLoanDays.HasValue || detail.ConfirmedDailyLateFine.HasValue)
                    {
                        var updateDetailResponse = await _mediator.Send(new UpdateConfirmedFiduciaryProductDetailCommand(detail.Id, detail.ConfirmedDailyLateFine, detail.ConfirmedLoanDays), ct);
                        if (updateDetailResponse.IsFailure)
                            return Result.Failure<SetFiduciaryProductConfirmedResponse>(updateDetailResponse.Error!);
                    }

                    if (detail.Warehouses is not null && detail.Warehouses.Count > 0)
                    {
                        var confirmedLoanCount = detail.Warehouses!.Sum(oo => oo.ConfirmedLoanCount);
                        if (fiduciaryProductDetail.LoanCount < confirmedLoanCount)
                            return Result.Failure<SetFiduciaryProductConfirmedResponse>(FiduciaryProductDetailManagementErrors.InValidConfirmedLoanCount);

                        var status = confirmedLoanCount == fiduciaryProductDetail.LoanCount ? FiduciaryProductDetailStatus.ExitForConsume : FiduciaryProductDetailStatus.IncompleteDelivered;
                        var descriptionDetail = await DetailDescriptionMacker(request.ProductDescription, status, ct);
                        var updateDetailStatusResponse = await _mediator.Send(new UpdateFiduciaryProductDetailStatusCommand(detail.Id, status, request.ProductDescription, descriptionDetail), ct);
                        if (updateDetailStatusResponse.IsFailure)
                            return Result.Failure<SetFiduciaryProductConfirmedResponse>(updateDetailStatusResponse.Error!);
                    }
                }
            }

        if (requestWarehouseIds is not null && requestWarehouseIds.Count > 0)
            foreach (var warehouseId in requestWarehouseIds)
            {
                var warehouse = requestWarehouses.FirstOrDefault(oo => oo.Id == warehouseId);
                if (warehouse is null)
                    return Result.Failure<SetFiduciaryProductConfirmedResponse>(FiduciaryProductDetailManagementErrors.InValidWarehouseId);

                var detailModels = request.Details.Where(oo => !oo.IsRejected && oo.Warehouses!.Any(oo => oo.WarehouseId == warehouseId)).ToList();
                List<InvoiceExitProductDto> Products = new();

                foreach (var item in detailModels)
                {
                    var fiduciaryProductDetail = details.FirstOrDefault(oo => oo.Id == item.Id);
                    if (fiduciaryProductDetail is null)
                        return Result.Failure<SetFiduciaryProductConfirmedResponse>(FiduciaryProductDetailErrors.FiduciaryProductDetailWithIdNotFound);

                    var product = products.FirstOrDefault(oo => oo.Id == fiduciaryProductDetail.ProductId);
                    if (product is null)
                        return Result.Failure<SetFiduciaryProductConfirmedResponse>(FiduciaryProductDetailErrors.InValidProductId);

                    var confirmedLoanCount = item.Warehouses!.FirstOrDefault(oo => oo.WarehouseId == warehouseId)!.ConfirmedLoanCount;
                    Products.Add(new InvoiceExitProductDto()
                    {
                        ProductId = product.Id,
                        MeasureId = product.Group.MeasureId,
                        Quantity = confirmedLoanCount,
                        Items = null,
                        ServiceReferenceId = null,
                        RetrunDate = DateTime.UtcNow,
                    });
                }

                var destWarehouseId = detailModels.SelectMany(x => x.Warehouses).FirstOrDefault(x => x.WarehouseId == warehouseId).DestWarehouseId;
                var exitBorrowsResponse = await _mediator.Send(new CreateExitForBorrowsCommand(warehouseId!, destWarehouseId, null, 1, fiduciaryProduct.Id.ToString(), fiduciaryProduct.Id.ToString(), Products, fiduciaryProduct.ThirdPartyId, thirdParty.FullName, null, null, true), ct);
                if (exitBorrowsResponse.IsFailure)
                    return Result.Failure<SetFiduciaryProductConfirmedResponse>(exitBorrowsResponse.Error!);
                var exitForBorrows = exitBorrowsResponse.Value!;

                foreach (var detail in detailModels)
                {
                    var fiduciaryProductDetail = details.FirstOrDefault(oo => oo.Id == detail.Id);
                    var confirmedLoanCount = detail.Warehouses!.FirstOrDefault(oo => oo.WarehouseId == warehouseId).ConfirmedLoanCount;
                    var DestWarehouesId = detail.Warehouses!.FirstOrDefault(oo => oo.WarehouseId == warehouseId).DestWarehouseId;

                    var createManagementResponse = await _mediator.Send(new CreateFiduciaryProductDetailManagementCommand(exitForBorrows.Id, warehouseId!, DestWarehouesId, confirmedLoanCount, null, fiduciaryProductDetail!), ct);
                    if (createManagementResponse.IsFailure)
                        return Result.Failure<SetFiduciaryProductConfirmedResponse>(createManagementResponse.Error!);
                }
            }

        await _unitOfWork.CommitAsync(ct);

        return new SetFiduciaryProductConfirmedResponse(fiduciaryProduct.Id);
    }

    public async Task<Result<SetFiduciaryProductPendingResponse?>> SetFiduciaryProductPending(
        SetFiduciaryProductPendingRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetFiduciaryProductPendingValidator, SetFiduciaryProductPendingRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetFiduciaryProductPendingResponse>(isValidRequest.Error!);

        var getFiduciaryProduct = await _mediator.Send(new GetFiduciaryProductByIdQuery(request.FiduciaryProductId), ct);
        if (getFiduciaryProduct.IsFailure)
            return Result.Failure<SetFiduciaryProductPendingResponse>(getFiduciaryProduct.Error!);
        var fiduciaryProduct = getFiduciaryProduct.Value!;

        if (!(ValidateFiduciaryProductStatus.AllowForPending.Any(x => x == fiduciaryProduct.Status)))
            return Result.Failure<SetFiduciaryProductPendingResponse>(FiduciaryProductErrors.InValidStatus);

        if (!(fiduciaryProduct.Details.Any(z => ValidateFiduciaryProductDetailStatus.AllowForPending.Any(x => x == z.Status))))
            return Result.Failure<SetFiduciaryProductPendingResponse>(FiduciaryProductDetailErrors.InValidStatus);

        var description = await DescriptionMacker("بررسی درخواست", FiduciaryProductStatus.Pending, ct);
        var updateStatusResponse = await _mediator.Send(new UpdateFiduciaryProductStatusCommand(request.FiduciaryProductId, FiduciaryProductStatus.Pending, null, description), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<SetFiduciaryProductPendingResponse>(updateStatusResponse.Error!);

        if (fiduciaryProduct.Details is not null && fiduciaryProduct.Details.Count > 0)
            foreach (var detail in fiduciaryProduct.Details)
            {
                var descriptionDetail = await DetailDescriptionMacker("بررسی کالای درخواست", FiduciaryProductDetailStatus.Returned, ct);
                var updateDetailStatusResponse = await _mediator.Send(new UpdateFiduciaryProductDetailStatusCommand(detail.Id, FiduciaryProductDetailStatus.Pending, null, descriptionDetail), ct);
                if (updateDetailStatusResponse.IsFailure)
                    return Result.Failure<SetFiduciaryProductPendingResponse>(updateDetailStatusResponse.Error!);
            }

        await _unitOfWork.CommitAsync(ct);
        return new SetFiduciaryProductPendingResponse(fiduciaryProduct.Id);
    }

    public async Task<Result<SetFiduciaryProductRejectedResponse?>> SetFiduciaryProductRejected(
        SetFiduciaryProductRejectedRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetFiduciaryProductRejectedValidator, SetFiduciaryProductRejectedRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetFiduciaryProductRejectedResponse>(isValidRequest.Error!);

        var getFiduciaryProduct = await _mediator.Send(new GetFiduciaryProductByIdQuery(request.FiduciaryProductId), ct);
        if (getFiduciaryProduct.IsFailure)
            return Result.Failure<SetFiduciaryProductRejectedResponse>(getFiduciaryProduct.Error!);
        var fiduciaryProduct = getFiduciaryProduct.Value!;

        if (!(ValidateFiduciaryProductStatus.AllowForRejected.Any(x => x == fiduciaryProduct.Status)))
            return Result.Failure<SetFiduciaryProductRejectedResponse>(FiduciaryProductErrors.InValidStatus);

        if (!(fiduciaryProduct.Details.Any(z => ValidateFiduciaryProductDetailStatus.AllowForRejected.Any(x => x == z.Status))))
            return Result.Failure<SetFiduciaryProductRejectedResponse>(FiduciaryProductDetailErrors.InValidStatus);

        var description = await DescriptionMacker(request.ProductDescription, FiduciaryProductStatus.Rejected, ct);
        var updateStatusResponse = await _mediator.Send(new UpdateFiduciaryProductStatusCommand(request.FiduciaryProductId, FiduciaryProductStatus.Rejected, request.ProductDescription, description), ct);
        if (updateStatusResponse.IsFailure)
            return Result.Failure<SetFiduciaryProductRejectedResponse>(updateStatusResponse.Error!);

        foreach (var detail in fiduciaryProduct.Details)
        {
            var descriptionDetail = await DetailDescriptionMacker(request.ProductDescription, FiduciaryProductDetailStatus.Rejected, ct);
            var updateDetailStatusResponse = await _mediator.Send(new UpdateFiduciaryProductDetailStatusCommand(detail.Id, FiduciaryProductDetailStatus.Rejected, request.ProductDescription, descriptionDetail), ct);
            if (updateDetailStatusResponse.IsFailure)
                return Result.Failure<SetFiduciaryProductRejectedResponse>(updateDetailStatusResponse.Error!);
        }

        await _unitOfWork.CommitAsync(ct);

        return new SetFiduciaryProductRejectedResponse(fiduciaryProduct.Id);
    }

    public async Task<Result<SetFiduciaryProductDetailDelivaryResponse?>> SetFiduciaryProductDetailDelivary(
        SetFiduciaryProductDetailDelivaryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetFiduciaryProductDetailDelivaryValidator, SetFiduciaryProductDetailDelivaryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetFiduciaryProductDetailDelivaryResponse>(isValidRequest.Error!);

        var getDetail = await _mediator.Send(new GetFiduciaryProductDetailByIdQuery(request.Id), ct);
        if (getDetail.IsFailure)
            return Result.Failure<SetFiduciaryProductDetailDelivaryResponse>(getDetail.Error!);
        var fiduciaryProductDetail = getDetail.Value!;

        if (!(ValidateFiduciaryProductDetailStatus.AllowForDeliverAndNoDeliver.Any(x => x == fiduciaryProductDetail.Status)))
            return Result.Failure<SetFiduciaryProductDetailDelivaryResponse>(FiduciaryProductDetailErrors.InValidStatus);

        var description = await DetailDescriptionMacker(request.ProductDescription, FiduciaryProductDetailStatus.Delivary, ct);
        var setDetailDelivary = await _mediator.Send(new SetFiduciaryProductDetailDelivaryCommand(
            request.Id,
            request.ProductDescription,
            description), ct);
        if (setDetailDelivary.IsFailure)
            return Result.Failure<SetFiduciaryProductDetailDelivaryResponse>(setDetailDelivary.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetFiduciaryProductDetailDelivaryResponse(fiduciaryProductDetail.Id);
    }

    public async Task<Result<SetFiduciaryProductDetailNoDelivaryResponse?>> SetFiduciaryProductDetailNoDelivary(
        SetFiduciaryProductDetailNoDelivaryRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetFiduciaryProductDetailNoDelivaryValidator, SetFiduciaryProductDetailNoDelivaryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetFiduciaryProductDetailNoDelivaryResponse>(isValidRequest.Error!);

        var getDetail = await _mediator.Send(new GetFiduciaryProductDetailByIdQuery(request.Id), ct);
        if (getDetail.IsFailure)
            return Result.Failure<SetFiduciaryProductDetailNoDelivaryResponse>(getDetail.Error!);

        var fiduciaryProductDetail = getDetail.Value!;

        if (!(ValidateFiduciaryProductDetailStatus.AllowForDeliverAndNoDeliver.Any(x => x == fiduciaryProductDetail.Status)))
            return Result.Failure<SetFiduciaryProductDetailNoDelivaryResponse>(FiduciaryProductDetailErrors.InValidStatus);

        var description = await DetailDescriptionMacker(request.ProductDescription, FiduciaryProductDetailStatus.NoDelivary, ct);
        var setDetailNoDelivery = await _mediator.Send(new UpdateFiduciaryProductDetailStatusCommand(
            request.Id,
            FiduciaryProductDetailStatus.NoDelivary,
            request.ProductDescription,
            description), ct);
        if (setDetailNoDelivery.IsFailure)
            return Result.Failure<SetFiduciaryProductDetailNoDelivaryResponse>(setDetailNoDelivery.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new SetFiduciaryProductDetailNoDelivaryResponse(fiduciaryProductDetail.Id);
    }

    public async Task<Result<GetFiduciaryProductManagementByIdResponse?>> GetFiduciaryProductManagementById(
        GetFiduciaryProductManagementByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFiduciaryProductManagementByIdValidator, GetFiduciaryProductManagementByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFiduciaryProductManagementByIdResponse>(isValidRequest.Error!);

        var getFiduciaryProduct = await _mediator.Send(new GetFiduciaryProductByIdQuery(request.FiduciaryProductId), ct);
        if (getFiduciaryProduct.IsFailure)
            return Result.Failure<GetFiduciaryProductManagementByIdResponse>(getFiduciaryProduct.Error!);
        var fiduciaryProduct = getFiduciaryProduct.Value!;

        var managements = fiduciaryProduct.Details.Where(c => c.Status == FiduciaryProductDetailStatus.Delivary ||
                                                              c.Status == FiduciaryProductDetailStatus.IncompleteDelivered ||
                                                              c.Status == FiduciaryProductDetailStatus.NoDelivary ||
                                                              c.Status == FiduciaryProductDetailStatus.Returned ||
                                                              c.Status == FiduciaryProductDetailStatus.IncompleteReturned)
                                                             .SelectMany(oo => oo.Managements).ToList();

        var productIds = fiduciaryProduct.Details.Where(x => x.ProductId > 0).Select(c => c.ProductId).Distinct().ToList();
        var products = await WebServicesLogic.ProductsDataReceiver(productIds, _mediator, _pRepo, ct);

        var measureIds = managements.Where(x => x.FiduciaryProductDetail is not null).Select(m => m.FiduciaryProductDetail.MeasureUnitId).Distinct().ToList();
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var currencyIds = managements.Where(x => x.FiduciaryProductDetail is not null).Select(m => m.FiduciaryProductDetail.CurrencyId).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var warehouseIds = managements.Where(x => x.WarehouseId > 0).Select(m => m.WarehouseId).Distinct().ToList();
        var warehouses = await WebServicesLogic.WarehousesDataReceiver(warehouseIds.Adapt<List<long?>>(), _mediator, ct);

        var getCreator = await WebServicesLogic.UserDataReceiver(new List<long>() { fiduciaryProduct.CreatorId }, null, _mediator, ct);
        var creator = getCreator?.FirstOrDefault();

        var thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(new List<long>() { fiduciaryProduct.ThirdPartyId }, null, null, _mediator, ct); ;
        var thirdParty = thirdParties?.FirstOrDefault();

        List<GetFiduciaryProductManagementByIdModel>? details = [];
        foreach (var man in managements)
        {
            if (details.Any(x => x.FiduciaryProductDetailId == man.FiduciaryProductDetail.Id))
                continue;

            details.Add(man.Adapt<GetFiduciaryProductManagementByIdModel>());
        }

        foreach (var detail in details)
        {
            detail.ProductCode = products?.FirstOrDefault(c => c.Id == detail.ProductId)?.Code;
            detail.ProductName = products?.FirstOrDefault(c => c.Id == detail.ProductId)?.Name;
            detail.BrandName = products?.FirstOrDefault(c => c.Id == detail.ProductId)?.Brand;
            detail.BrandModel = products?.FirstOrDefault(c => c.Id == detail.ProductId)?.BrandModel;
            detail.MeasureunitName = measures?.FirstOrDefault(c => c.Id == detail.MeasureunitId)?.Name;
            detail.CurrencyName = currencies?.FirstOrDefault(c => c.Id == detail.CurrencyId)?.Name;
            detail.WarehouseCode = warehouses?.FirstOrDefault(c => c.Id == detail.WarehouseId)?.Code;
            detail.WarehouseName = warehouses?.FirstOrDefault(c => c.Id == detail.WarehouseId)?.Name;
        }

        var response = fiduciaryProduct.Adapt<GetFiduciaryProductManagementByIdResponse>();
        response.ThirdParty = thirdParty?.FullName;
        response.Creator = creator?.FullName;
        response.Details = details;

        return response;
    }

    public async Task<Result<GetFiduciaryProductDetailReturnByDetailIdResponse?>> GetFiduciaryProductDetailReturnByDetailId(
        GetFiduciaryProductDetailReturnByDetailIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFiduciaryProductDetailReturnByDetailIdValidator, GetFiduciaryProductDetailReturnByDetailIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFiduciaryProductDetailReturnByDetailIdResponse>(isValidRequest.Error!);

        var getDetail = await _mediator.Send(new GetFiduciaryProductDetailReturnByDetailIdQuery(request.FiduciaryProductDetailId), ct);
        if (getDetail.IsFailure)
            return Result.Failure<GetFiduciaryProductDetailReturnByDetailIdResponse>(getDetail.Error!);
        var fiduciaryProductDetail = getDetail.Value!;

        List<long>? creatorIds = fiduciaryProductDetail.Returns?.Where(x => x.CreatorId is not null && x.CreatorId > 0).Select(x => x.CreatorId!.Value).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);
        var product = await WebServicesLogic.ProductDataReceiver(fiduciaryProductDetail.ProductId, _mediator, _pRepo, ct);
        var measureUnit = await WebServicesLogic.MeasureUnitDataReceiver(fiduciaryProductDetail.MeasureUnitId, _mediator, ct);

        fiduciaryProductDetail.ProductCode = product?.Code;
        fiduciaryProductDetail.ProductName = product?.Name;
        fiduciaryProductDetail.MeasureUnitName = measureUnit?.Name;
        fiduciaryProductDetail.ProductCode = product?.Code;

        fiduciaryProductDetail.Returns?.ForEach(item =>
        {
            item.Creator = creators?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
        });

        return fiduciaryProductDetail;
    }

    public async Task<Result<GetFiduciaryProductDetailReturnDocumentByIdResponse?>> GetFiduciaryProductDetailReturnDocumentById(
        GetFiduciaryProductDetailReturnDocumentByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFiduciaryProductDetailReturnDocumentByIdValidator, GetFiduciaryProductDetailReturnDocumentByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFiduciaryProductDetailReturnDocumentByIdResponse>(isValidRequest.Error!);

        var getReturnDocument = await _mediator.Send(new GetFiduciaryProductDetailReturnDocumentQuery(request.FiduciaryProductDetailReturnId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (getReturnDocument.IsFailure)
            return Result.Failure<GetFiduciaryProductDetailReturnDocumentByIdResponse>(getReturnDocument.Error!);
        var documents = getReturnDocument.Value!.Data!.Select(c => c.Url).ToList();

        return new GetFiduciaryProductDetailReturnDocumentByIdResponse(documents, getReturnDocument.Value!.RowCount!);
    }

    public async Task<Result<GetFilteredFiduciaryProductManageWarehousesResponse?>> GetFilteredFiduciaryProductManageWarehouses(
        GetFilteredFiduciaryProductManageWarehousesRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredFiduciaryProductManageWarehousesValidator, GetFilteredFiduciaryProductManageWarehousesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredFiduciaryProductManageWarehousesResponse>(isValidRequest.Error!);

        var getDetail = await _mediator.Send(new GetFiduciaryProductDetailByIdQuery(request.FiduciaryProductDetailId), ct);
        if (getDetail.IsFailure)
            return Result.Failure<GetFilteredFiduciaryProductManageWarehousesResponse>(getDetail.Error!);
        var fiduciaryProductDetail = getDetail.Value!;

        var getsCostCenterWarehouse = await _mediator.Send(new GetsCostCenterWarehouseByCostCenterIdQuery(fiduciaryProductDetail.FiduciaryProduct.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId, request.PageIndex, request.PageSize), ct);
        if (getsCostCenterWarehouse.IsFailure)
            return Result.Failure<GetFilteredFiduciaryProductManageWarehousesResponse>(getsCostCenterWarehouse.Error!);
        var costCenterWarehouses = getsCostCenterWarehouse.Value!.Data!;

        var costCenterWarehouseIds = costCenterWarehouses.Select(oo => (long?)oo.WarehouseId).ToList();
        var warhouses = await WebServicesLogic.WarehousesDataReceiver(costCenterWarehouseIds, _mediator, ct);

        var result = new List<GetFilteredFiduciaryProductManageWarehousesModel>();
        foreach (var costCenterWarehouse in costCenterWarehouses)
        {
            var getInventory = await _mediator.Send(new GetInventoryByProductIdQuery(fiduciaryProductDetail.ProductId, costCenterWarehouse.WarehouseId), ct);
            var invantory = getInventory.Value;

            var warehouse = warhouses?.FirstOrDefault(oo => oo.Id == costCenterWarehouse.WarehouseId);
            if (warehouse is null)
                return Result.Failure<GetFilteredFiduciaryProductManageWarehousesResponse>(FiduciaryProductErrors.InValidWarehouse);

            result.Add(new GetFilteredFiduciaryProductManageWarehousesModel()
            {
                Id = costCenterWarehouse.WarehouseId,
                Name = warehouse?.Name,
                Code = warehouse?.Code,
                IsDefault = costCenterWarehouse.IsDefault,
                RealQuantity = invantory?.RealQuantity ?? 0
            });
        }

        return new GetFilteredFiduciaryProductManageWarehousesResponse(result, result.Count);
    }

    public async Task<Result<GetFiduciaryProductDetailReturnTypeResponse?>> GetFiduciaryProductDetailReturnType(
        GetFiduciaryProductDetailReturnTypeRequest request, CT ct)
    {
        var response = await Task.Run(() => { return EnumExt.GetEnumObjectList<FiduciaryProductDetailReturnType>(); });
        return new GetFiduciaryProductDetailReturnTypeResponse(response);
    }

    public async Task<Result<GetFiduciaryProductDetailManagementStatusResponse?>> GetFiduciaryProductDetailManagementStatus(
        GetFiduciaryProductDetailManagementStatusRequest request, CT ct)
    {
        var response = await Task.Run(() => { return EnumExt.GetEnumObjectList<FiduciaryProductDetailManagementStatus>(); });
        return new GetFiduciaryProductDetailManagementStatusResponse(response);
    }
}
