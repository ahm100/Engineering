using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Services.FiduciaryProductDetails.Commands.CreateFiduciaryProductDetail;
using Engineering.Application.Services.FiduciaryProductDetails.Commands.UpdateFiduciaryProductDetail;
using Engineering.Application.Services.FiduciaryProducts.Commands.CreateFiduciaryProduct;
using Engineering.Application.Services.FiduciaryProducts.Commands.DeleteFiduciaryProduct;
using Engineering.Application.Services.FiduciaryProducts.Commands.DeleteFiduciaryProductDetail;
using Engineering.Application.Services.FiduciaryProducts.Commands.UpdateFiduciaryProduct;
using Engineering.Application.Services.FiduciaryProducts.Models.CreateFiduciaryProduct;
using Engineering.Application.Services.FiduciaryProducts.Models.DeleteFiduciaryProduct;
using Engineering.Application.Services.FiduciaryProducts.Models.FiduciaryProductGroupDelete;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductById;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductDetailStatus;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductStatus;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductDetailHistories;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductHistories;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProducts;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductsExcelEnums;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductsExcelExporter;
using Engineering.Application.Services.FiduciaryProducts.Models.UpdateFiduciaryProduct;
using Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductById;
using Engineering.Application.Services.FiduciaryProducts.Queries.GetFiduciaryProductDataById;
using Engineering.Application.Services.FiduciaryProducts.Queries.GetFilteredFiduciaryProductDetailHistories;
using Engineering.Application.Services.FiduciaryProducts.Queries.GetFilteredFiduciaryProductHistories;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdNoIncluding;
using Engineering.Application.Services.Projects.Queries.GetProjectById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Inventories.Queries.GetInventoryByProductId;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.FiduciaryProducts;

public partial class FiduciaryProductLogic : IFiduciaryProductLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<FiduciaryProductLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileService _userProfileService;
    private readonly IViewProductRepository _pRepo;
    private readonly IUserInfoService _userInfoService;
    public FiduciaryProductLogic(
        IMediator mediator,
        ILogger<FiduciaryProductLogic> logger,
        IUnitOfWork unitOfWork,
        IUserProfileService userProfileService,
        IUserInfoService userInfoService,
        IViewProductRepository pRepo)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userProfileService = userProfileService;
        _userInfoService = userInfoService;
        _pRepo = pRepo;
    }

    public async Task<Result<CreateFiduciaryProductResponse?>> CreateFiduciaryProduct(
        CreateFiduciaryProductRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateFiduciaryProductValidator, CreateFiduciaryProductRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateFiduciaryProductResponse>(isValidRequest.Error!);

        var getProject = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId), ct);
        if (getProject.IsFailure)
            return Result.Failure<CreateFiduciaryProductResponse>(getProject.Error!);
        var project = getProject.Value!;

        var getProjectOperation = await _mediator.Send(new GetProjectOperationByIdNoIncludingQuery(request.ProjectOperationId), ct);
        if (getProjectOperation.IsFailure)
            return Result.Failure<CreateFiduciaryProductResponse>(getProjectOperation.Error!);
        var projectOperation = getProjectOperation.Value!;

        var thirdPartyQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [request.ThirdPartyId], null, false, null), ct);
        if (thirdPartyQuery.IsFailure)
            return Result.Failure<CreateFiduciaryProductResponse>(thirdPartyQuery.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateFiduciaryProductResponse>(companyResponse.Error!);
        }

        var createResponse = await _mediator.Send(new CreateFiduciaryProductCommand(
                project,
                projectOperation,
                request.ThirdPartyId,
                request.Description,
                companyId),
            ct);
        if (createResponse.IsFailure)
            return Result.Failure<CreateFiduciaryProductResponse>(createResponse.Error!);
        var fiduciaryProduct = createResponse.Value!;

        if (request.Products is not null && request.Products.Count > 0)
            foreach (var product in request.Products)
            {
                var createDetailResponse = await _mediator.Send(new CreateFiduciaryProductDetailCommand(fiduciaryProduct,
                        product.ProductId,
                        product.LoanCount,
                        product.LoanDays,
                        product.MeasureUnitId,
                        product.CurrencyId,
                        product.DailyLateFine),
                    ct);
                if (createDetailResponse.IsFailure)
                    return Result.Failure<CreateFiduciaryProductResponse>(createDetailResponse.Error!);
            }

        await _unitOfWork.CommitAsync(ct);
        return new CreateFiduciaryProductResponse(fiduciaryProduct.Id);
    }

    public async Task<Result<UpdateFiduciaryProductResponse?>> UpdateFiduciaryProduct(
        UpdateFiduciaryProductRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateFiduciaryProductValidator, UpdateFiduciaryProductRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateFiduciaryProductResponse>(isValidRequest.Error!);

        var getFiduciaryProduct = await _mediator.Send(new GetFiduciaryProductByIdQuery(request.Id), ct);
        if (getFiduciaryProduct.IsFailure)
            return Result.Failure<UpdateFiduciaryProductResponse>(getFiduciaryProduct.Error!);
        var fiduciaryProduct = getFiduciaryProduct.Value!;

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateFiduciaryProductResponse>(companyResponse.Error!);
        }

        var getProject = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId), ct);
        if (getProject.IsFailure)
            return Result.Failure<UpdateFiduciaryProductResponse>(getProject.Error!);
        var project = getProject.Value!;

        var getProjectOperation = await _mediator.Send(new GetProjectOperationByIdNoIncludingQuery(request.ProjectOperationId), ct);
        if (getProjectOperation.IsFailure)
            return Result.Failure<UpdateFiduciaryProductResponse>(getProjectOperation.Error!);
        var projectOperation = getProjectOperation.Value!;

        var thirdPartyResponse = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, new List<long>() { request.ThirdPartyId }, null, false, null), ct);
        if (thirdPartyResponse.IsFailure)
            return Result.Failure<UpdateFiduciaryProductResponse>(thirdPartyResponse.Error!);
        var thirdParty = thirdPartyResponse.Value!;

        if (!(ValidateFiduciaryProductStatus.AllowForUpdate.Any(x => x == fiduciaryProduct.Status)))
            return Result.Failure<UpdateFiduciaryProductResponse>(FiduciaryProductErrors.InValidStatus);

        var updateResponse = await _mediator.Send(new UpdateFiduciaryProductCommand(
                fiduciaryProduct,
                project,
                projectOperation,
                request.ThirdPartyId,
                request.Description,
                companyId),
            ct);
        if (updateResponse.IsFailure)
            return Result.Failure<UpdateFiduciaryProductResponse>(updateResponse.Error!);

        var newDetails = request.Products.Where(c => c.Id == null).ToList();
        var updateDetails = request.Products.Where(c => c.Id != null && c.IsDeleted == false).ToList();
        var deleteDetailIds = request.Products.Where(c => c.Id != null && c.IsDeleted == true).Select(oo => oo.Id!.Value).ToList();

        if (newDetails is not null && newDetails.Count > 0)
            foreach (var product in newDetails)
            {
                var createDetailResponse = await _mediator.Send(new CreateFiduciaryProductDetailCommand(fiduciaryProduct,
                        product.ProductId,
                        product.LoanCount,
                        product.LoanDays,
                        product.MeasureUnitId,
                        product.CurrencyId,
                        product.DailyLateFine),
                    ct);
                if (createDetailResponse.IsFailure)
                    return Result.Failure<UpdateFiduciaryProductResponse>(createDetailResponse.Error!);
            }

        if (updateDetails is not null && updateDetails.Count > 0)
            foreach (var product in updateDetails)
            {
                var detail = fiduciaryProduct.Details.FirstOrDefault(c => c.Id.Equals(product.Id));
                if (detail is null)
                    return Result.Failure<UpdateFiduciaryProductResponse>(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);

                if (ValidateFiduciaryProductDetailStatus.AllowForUpdateOrDelete.Any(x => x == detail.Status))
                {
                    var updateDetailResponse = await _mediator.Send(new UpdateFiduciaryProductDetailCommand(detail!, product.ProductId, product.LoanCount, product.LoanDays, product.MeasureUnitId, product.CurrencyId, product.DailyLateFine, product.Description), ct);
                    if (updateDetailResponse.IsFailure)
                        return Result.Failure<UpdateFiduciaryProductResponse>(updateDetailResponse.Error!);
                }
                else
                    return Result.Failure<UpdateFiduciaryProductResponse>(FiduciaryProductDetailErrors.InValidStatus);
            }

        if (deleteDetailIds is not null && deleteDetailIds.Count > 0)
            foreach (var deleteId in deleteDetailIds)
            {
                var detail = fiduciaryProduct.Details.FirstOrDefault(c => c.Id.Equals(deleteId));
                if (detail is null)
                    return Result.Failure<UpdateFiduciaryProductResponse>(FiduciaryProductDetailErrors.InValidFiduciaryProductDetail);

                if (ValidateFiduciaryProductDetailStatus.AllowForUpdateOrDelete.Any(x => x == detail.Status))
                {
                    var deleteDetailResponse = await _mediator.Send(new DeleteFiduciaryProductDetailCommand(deleteId), ct);
                    if (deleteDetailResponse.IsFailure)
                        return Result.Failure<UpdateFiduciaryProductResponse>(deleteDetailResponse.Error!);
                }
                else
                    return Result.Failure<UpdateFiduciaryProductResponse>(FiduciaryProductDetailErrors.InValidStatus);
            }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateFiduciaryProductResponse(fiduciaryProduct.Id);
    }

    public async Task<Result<DeleteFiduciaryProductResponse?>> DeleteFiduciaryProductAsync(
        DeleteFiduciaryProductRequest request, CT ct)
    {
        _logger.LogInformation("Delete FiduciaryProduct  with {FiduciaryProductId},", request.Id);

        var isValidRequest = await request.IsValidAsync<DeleteFiduciaryProductValidator, DeleteFiduciaryProductRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteFiduciaryProductResponse>(isValidRequest.Error!);

        var getFiduciaryProduct = await _mediator.Send(new GetFiduciaryProductByIdQuery(request.Id), ct);
        if (getFiduciaryProduct.IsFailure)
            return Result.Failure<DeleteFiduciaryProductResponse>(getFiduciaryProduct.Error!);
        var fiduciaryProduct = getFiduciaryProduct.Value!;

        var currentUser = _userProfileService.GetProfileInfo();
        if (fiduciaryProduct.CreatorId != currentUser.UserId)
            return Result.Failure<DeleteFiduciaryProductResponse>(FiduciaryProductErrors.InValidCreator);

        var deleteResponse = await _mediator.Send(new DeleteFiduciaryProductCommand(request.Id), ct);
        if (deleteResponse.IsFailure)
            return Result.Failure<DeleteFiduciaryProductResponse>(deleteResponse.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteFiduciaryProductResponse(deleteResponse.Value!.Id);
    }

    public async Task<Result<FiduciaryProductGroupDeleteResponse?>> FiduciaryProductGroupDelete(
        FiduciaryProductGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for FiduciaryProductGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<FiduciaryProductGroupDeleteValidator, FiduciaryProductGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<FiduciaryProductGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DeleteFiduciaryProductCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<FiduciaryProductGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new FiduciaryProductGroupDeleteResponse(true);
    }

    public async Task<Result<GetFiduciaryProductByIdResponse?>> GetFiduciaryProductById(
        GetFiduciaryProductByIdRequest request, CT ct)
    {
        _logger.LogInformation("Get Fiduciary Product By Id with {FiduciaryProductId},", request.Id);

        var isValidRequest = await request.IsValidAsync<GetFiduciaryProductByIdValidator, GetFiduciaryProductByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFiduciaryProductByIdResponse>(isValidRequest.Error!);

        var getFiduciaryProduct = await _mediator.Send(new GetFiduciaryProductDataByIdQuery(request.Id), ct);
        if (getFiduciaryProduct.IsFailure)
            return Result.Failure<GetFiduciaryProductByIdResponse>(getFiduciaryProduct.Error!);
        var fiduciaryProduct = getFiduciaryProduct.Value!;

        Company? company = null;
        if (fiduciaryProduct.CompanyId is not null && fiduciaryProduct.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(fiduciaryProduct.CompanyId, _mediator, ct);

        UserModel? thirdParty = null;
        if (fiduciaryProduct.ThirdParty?.Id > 0)
        {
            var thirdparties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver([(long)fiduciaryProduct.ThirdParty?.Id!], null, null, _mediator, ct);
            thirdParty = thirdparties?.FirstOrDefault();
        }

        List<FilteredUserResponseModel>? users = [];
        var userIds = GetUserIds(fiduciaryProduct);
        if (userIds is not null && userIds.Count > 0)
            users = await WebServicesLogic.UserDataReceiver(userIds, null, _mediator, ct);

        fiduciaryProduct.ThirdParty!.FullName = thirdParty?.FullName;
        fiduciaryProduct.Creator = users?.FirstOrDefault(x => x.UserId == fiduciaryProduct.CreatorId)?.FullName;
        fiduciaryProduct.CompanyNameFa = company?.NameFa;

        if (fiduciaryProduct.Details?.Count > 0 && fiduciaryProduct.Details is not null)
        {
            var productIds = fiduciaryProduct.Details.Where(x => x.ProductId is not null && x.ProductId > 0).Select(c => (long)c.ProductId!).Distinct().ToList();
            var products = await WebServicesLogic.ProductsDataReceiver(productIds, _mediator, _pRepo, ct);
            var measureIds = fiduciaryProduct.Details.Where(x => x.MeasureunitId is not null && x.MeasureunitId > 0).Select(x => (long)x.MeasureunitId!).Distinct().ToList();
            var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);
            var currencyIds = GetCurrencyIds(fiduciaryProduct);
            var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);
            var warehouseIds = fiduciaryProduct.Details.Where(x => x.Managements is not null && x.Managements.Count > 0).SelectMany(x => x.Managements!)
                .Where(z => z.Warehouse is not null && z.Warehouse.Id > 0).Select(w => (long?)w.Warehouse?.Id).Distinct().ToList();
            var warehouses = await WebServicesLogic.WarehousesDataReceiver(warehouseIds, _mediator, ct);

            fiduciaryProduct.Details.ForEach(item =>
            {
                if (item.Managements is not null && item.Managements.Count > 0)
                    item.Managements.ForEach(async item1 =>
                    {
                        if (item1.Returns is not null && item1.Returns.Count > 0)
                            item1.Returns.ForEach(item2 =>
                            {
                                item2.CurrencyName = currencies?.FirstOrDefault(c => c.Id == item2.CurrencyId)?.Name;
                                item2.Creator = users?.FirstOrDefault(c => c.UserId == item2.CreatorId)?.FullName;
                            });

                        var getInventory = await _mediator.Send(new GetInventoryByProductIdQuery(item.ProductId!.Value, item1.Warehouse!.Id), ct);
                        var invantory = getInventory.Value;

                        item1.Warehouse.Code = warehouses?.FirstOrDefault(oo => oo.Id == item1.Warehouse?.Id)?.Code;
                        item1.Warehouse.Name = warehouses?.FirstOrDefault(oo => oo.Id == item1.Warehouse?.Id)?.Name;
                        item1.Warehouse.RealQuantity = invantory?.RealQuantity ?? 0;
                        item1.Creator = users?.FirstOrDefault(c => c.UserId == item1.CreatorId)?.FullName;
                    });

                item.ProductCode = products?.FirstOrDefault(c => c.Id == item.ProductId)?.Code;
                item.ProductName = products?.FirstOrDefault(c => c.Id == item.ProductId)?.Name;
                item.ProductBrand = products?.FirstOrDefault(c => c.Id == item.ProductId)?.Brand;
                item.ProductBrandModel = products?.FirstOrDefault(c => c.Id == item.ProductId)?.BrandModel;
                item.MeasureunitName = measures?.FirstOrDefault(c => c.Id == item.MeasureunitId)?.Name;
                item.CurrencyName = currencies?.FirstOrDefault(c => c.Id == item.CurrencyId)?.Name;
                item.Creator = users?.FirstOrDefault(c => c.UserId == item.CreatorId)?.FullName;
            });
        }

        return fiduciaryProduct;
    }

    public async Task<Result<GetFiduciaryProductDetailStatusResponse?>> GetFiduciaryProductDetailStatus(
        GetFiduciaryProductDetailStatusRequest request, CT ct)
    {
        var response = await Task.Run(() => { return EnumExt.GetEnumObjectList<FiduciaryProductDetailStatus>(); });
        return new GetFiduciaryProductDetailStatusResponse(response);
    }

    public async Task<Result<GetFiduciaryProductStatusResponse?>> GetFiduciaryProductStatus(
        GetFiduciaryProductStatusRequest request, CT ct)
    {
        var response = await Task.Run(() => { return EnumExt.GetEnumObjectList<FiduciaryProductStatus>(); });
        return new GetFiduciaryProductStatusResponse(response);
    }

    public async Task<Result<GetFilteredFiduciaryProductDetailHistoriesResponse?>> GetFilteredFiduciaryProductDetailHistories(
        GetFilteredFiduciaryProductDetailHistoriesRequest request, CT ct)
    {
        _logger.LogInformation("Get FiduciaryProductDetail History with {FiduciaryProductDetailId},", request.Id);

        var isValidRequest = await request.IsValidAsync<GetFilteredFiduciaryProductDetailHistoriesValidator, GetFilteredFiduciaryProductDetailHistoriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredFiduciaryProductDetailHistoriesResponse>(isValidRequest.Error!);

        var getHistories = await _mediator.Send(new GetFilteredFiduciaryProductDetailHistoriesQuery(request.Id,
                request.OrderBy,
                request.PageIndex,
                request.PageSize),
            ct);
        if (getHistories.IsFailure)
            return Result.Failure<GetFilteredFiduciaryProductDetailHistoriesResponse>(getHistories.Error!);
        var histories = getHistories.Value!.Data!;

        var historiesCreatorIds = histories.Where(c => c.CreatorId is not null && c.CreatorId > 0).Select(c => c.CreatorId!.Value).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(historiesCreatorIds, null, _mediator, ct);

        var historiesProductIds = histories.Where(c => c.ProductId is not null && c.ProductId > 0).Select(c => c.ProductId!.Value).Distinct().ToList();
        var products = await WebServicesLogic.ProductsDataReceiver(historiesProductIds, _mediator, _pRepo, ct);

        var measureIds = histories.Where(c => c.MeasureunitId is not null && c.MeasureunitId > 0).Select(x => x.MeasureunitId!.Value).Distinct().ToList();
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var currencyIds = histories.Where(c => c.CurrencyId is not null && c.CurrencyId > 0).Select(x => x.CurrencyId!.Value).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var warehouseIds = histories.Where(x => x.WarehouseIds is not null && x.WarehouseIds.Count > 0)
            .SelectMany(x => x.WarehouseIds!).Adapt<List<long?>>().Distinct().ToList();
        var warehouses = await WebServicesLogic.WarehousesDataReceiver(warehouseIds, _mediator, ct);

        var models = new List<GetFilteredFiduciaryProductDetailHistoriesModel>();
        histories.ForEach(item =>
        {
            item.Creator = creators?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            item.CurrencyName = currencies?.FirstOrDefault(x => x.Id == item.CurrencyId)?.Name;
            item.ProductName = products?.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            item.ProductCode = products?.FirstOrDefault(x => x.Id == item.ProductId)?.Code;
            item.ProductBrand = products?.FirstOrDefault(x => x.Id == item.ProductId)?.Brand;
            item.ProductBrandModel = products?.FirstOrDefault(x => x.Id == item.ProductId)?.BrandModel;
            item.MeasureunitName = measures?.FirstOrDefault(x => x.Id == item.MeasureunitId)?.Name;
            item.Warehouse = string.Join(" - ", warehouses?.Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => x.Name).Distinct().ToList());
        });

        return new GetFilteredFiduciaryProductDetailHistoriesResponse(histories, getHistories.Value!.RowCount!);
    }

    public async Task<Result<GetFilteredFiduciaryProductHistoriesResponse?>> GetFilteredFiduciaryProductHistories(
        GetFilteredFiduciaryProductHistoriesRequest request, CT ct)
    {
        _logger.LogInformation("Get FiduciaryProduct History with {FiduciaryProductId},", request.Id);
        var isValidRequest = await request.IsValidAsync<GetFilteredFiduciaryProductHistoriesValidator, GetFilteredFiduciaryProductHistoriesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredFiduciaryProductHistoriesResponse>(isValidRequest.Error!);

        var getHistories = await _mediator.Send(new GetFilteredFiduciaryProductHistoriesQuery(request.Id,
                request.OrderBy,
                request.PageIndex,
                request.PageSize),
            ct);
        if (getHistories.IsFailure)
            return Result.Failure<GetFilteredFiduciaryProductHistoriesResponse>(getHistories.Error!);
        var histories = getHistories.Value!.Data!;

        List<long>? historiesThirdPartyIds = histories.Where(c => c.ThirdPartyId is not null && c.ThirdPartyId > 0)
            .Select(c => c.ThirdPartyId!.Value).Distinct().ToList();
        var thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(historiesThirdPartyIds, null, null, _mediator, ct);

        List<long>? historiesCreatorIds = histories.Where(c => c.CreatorId is not null && c.CreatorId > 0)
            .Select(c => c.CreatorId!.Value).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(historiesCreatorIds, null, _mediator, ct);

        var models = new List<GetFilteredFiduciaryProductHistoriesModel>();
        histories.ForEach(item =>
        {
            item.Creator = creators?.FirstOrDefault(c => c.UserId == item.CreatorId)?.FullName;
            item.ThirdParty = thirdParties?.FirstOrDefault(c => c is not null && c.Id == item.ThirdPartyId)?.FullName;
        });

        return new GetFilteredFiduciaryProductHistoriesResponse(histories, histories.Count);
    }

    public async Task<Result<GetFilteredFiduciaryProductsResponse?>> GetFilteredFiduciaryProducts(
        GetFilteredFiduciaryProductsRequest request, CT ct)
    {
        _logger.LogInformation("Get Filtered FiduciaryProducts with {CostCenterId}, {ProjectId},{Status}, {ProjectOperationIds}, {ThirdPartyId}, {FromDate}, {ToDate}, {FilterData}, {PageIndex}, {PageSize}",
            request.CostCenterId, request.ProjectId, request.Status, request.ProjectOperationIds, request.ThirdPartyId, request.FromDate, request.ToDate, request.FilterData, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetFilteredFiduciaryProductsValidator, GetFilteredFiduciaryProductsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredFiduciaryProductsResponse>(isValidRequest.Error!);

        var fiduciaryProducts = await GetFilteredFiduciaryProductsData(
            null,
            request.CostCenterId,
            request.ProjectId,
            request.Status,
            request.ProjectOperationIds,
            request.ThirdPartyId,
            request.FromDate,
            request.ToDate,
            request.FilterData,
            request.OrderBy,
            request.PageIndex,
            request.PageSize,
            _mediator,
            ct);
        if (fiduciaryProducts.IsFailure)
            return Result.Failure<GetFilteredFiduciaryProductsResponse>(fiduciaryProducts.Error!);

        return new GetFilteredFiduciaryProductsResponse(fiduciaryProducts.Value.Data, fiduciaryProducts.Value.RowCount);
    }

    public async Task<Result<GetFilteredFiduciaryProductsExcelEnumsResponse?>> GetFilteredFiduciaryProductsExcelEnums(
        GetFilteredFiduciaryProductsExcelEnumsRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<FiduciaryProductsExcelEnum>());
        return new GetFilteredFiduciaryProductsExcelEnumsResponse(response);
    }

    public async Task<Result<GetFilteredFiduciaryProductsExcelExporterResponse?>> GetFilteredFiduciaryProductsExcelExporter(
        GetFilteredFiduciaryProductsExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Get Filtered FiduciaryProducts with {CostCenterId}, {ProjectId},{Status}, {ProjectOperationIds}, {ThirdPartyId}, {FromDate}, {ToDate}, {FilterData}, {PageIndex}, {PageSize}",
            request.CostCenterId, request.ProjectId, request.Status, request.ProjectOperationIds, request.ThirdPartyId, request.FromDate, request.ToDate, request.FilterData, request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetFilteredFiduciaryProductsExcelExporterValidator, GetFilteredFiduciaryProductsExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredFiduciaryProductsExcelExporterResponse>(isValidRequest.Error!);

        var fiduciaryProducts = await GetFilteredFiduciaryProductsData(
            null,
            request.CostCenterId,
            request.ProjectId,
            request.Status,
            request.ProjectOperationIds,
            request.ThirdPartyId,
            request.FromDate,
            request.ToDate,
            request.FilterData,
            request.OrderBy,
            0,
            0,
            _mediator,
            ct);
        if (fiduciaryProducts.IsFailure)
            return Result.Failure<GetFilteredFiduciaryProductsExcelExporterResponse>(fiduciaryProducts.Error!);

        var data = fiduciaryProducts.Value.Data.Adapt<List<GetFilteredFiduciaryProductsExcelExporterResponseModel>>();
        var details = fiduciaryProducts.Value.Data.Where(x => x.Details is not null && x.Details.Count > 0)
            .SelectMany(x => x.Details!).Adapt<List<GetFilteredFiduciaryProductDetailsExcelExporterModel>>();

        var file = new FileContentResult(FiduciaryProductExcels.FiduciaryProductToExcel(data, details, request.ExcelFilters, []), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"FiduciaryProducts-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetFilteredFiduciaryProductsExcelExporterResponse(file);
    }
}