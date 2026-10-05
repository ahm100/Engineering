using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Extensions.StringExtensions;
using Engineering.Application.RequestGoodsSupplyManagements.Commands.CreateRequestGoodsSupplyManagement;
using Engineering.Application.RequestGoodsSupplyManagements.Models.GetFilteredAlternativeProducts;
using Engineering.Application.RequestGoodsSupplyManagements.Models.GetFilteredManagementRequestGoodsSupplies;
using Engineering.Application.RequestGoodsSupplyManagements.Models.GetsDestinationWarehouse;
using Engineering.Application.RequestGoodsSupplyManagements.Models.GetsManagementGoodsSupplyForWarehouse;
using Engineering.Application.RequestGoodsSupplyManagements.Models.GetsSourceWarehouse;
using Engineering.Application.RequestGoodsSupplyManagements.Models.UpdateRequestGoodsSupplyManagement;
using Engineering.Application.RequestGoodsSupplyManagements.Models.UpdateRequestGoodsSupplyManagements;
using Engineering.Application.RequestGoodsSupplyManagements.Queries.GetFilteredRequestGoodsSupplies;
using Engineering.Application.RequestGoodsSupplyManagements.Queries.GetsManagementGoodsSupplyForWarehouse;
using Engineering.Application.Services.CostCenterWarehouses.Queries.GetsByCostCenterId;
using Engineering.Application.Services.CostCenterWarehouses.Queries.GetsCostCenterWarehouseForSupplyManagement;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetFilteredAlternativeProducts;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyDetailById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyProductById;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetRequestGoodsManagementHistoryById;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetRequestGoodsSupplyProducts;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsGoodsSupplyManagmentBySupplyProductId;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsProjectManagerRequestGoodsSupplie;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsRequestGoodsSupplyManagementStatus;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsRequestGoodsSupplyManagementType;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsSupplyManagementExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsSupplyManagementExcelExporter;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedProjectGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetRequestGoodsManagementHistoryById;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetRequestGoodsSupplyManagementProducts;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetsGoodsSupplyManagmentBySupplyProductId;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetsProjectManagerRequestGoodsSupplie;
using Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredWarehousesByGroupId;
using Engineering.Application.WebServices.WarehouseServices.Inventories.Models.GetProductInventoryByFilter;
using Engineering.Application.WebServices.WarehouseServices.Inventories.Queries.GetProductInventoryByFilter;
using Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForConsumes;
using Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForRelocation;
using Engineering.Application.WebServices.WarehouseServices.Warehouse.Models.GetWarehouseByIds;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Polly.Registry;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.RequestGoodsSupplyDetailManagements;

public partial class RequestGoodsSupplyManagementLogic : IRequestGoodsSupplyManagementLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<RequestGoodsSupplyManagementLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileService _userProfileService;
    private readonly IViewProductRepository _productRepo;
    private readonly IUserInfoService _userInfoService;
    private readonly ResiliencePipelineProvider<string> _resilience;
    private readonly long _currenctUserId;
    List<long> _commerceIds = [];
    List<long> _inviceIds = [];

    public RequestGoodsSupplyManagementLogic(
        IMediator mediator,
        ILogger<RequestGoodsSupplyManagementLogic> logger,
        IUnitOfWork unitOfWork,
        IUserProfileService userProfileService,
        IViewProductRepository productRepo,
        IUserInfoService userInfoService,
        ResiliencePipelineProvider<string> resilience)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userProfileService = userProfileService;
        _productRepo = productRepo;
        _userInfoService = userInfoService;
        _resilience = resilience;
        _currenctUserId = _userProfileService.GetProfileInfo().UserId;
    }

    public async Task<Result<SetConfirmedGoodsSupplyProductResponse?>> SetConfirmedGoodsSupplyProduct(SetConfirmedGoodsSupplyProductModelRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<SetConfirmedGoodsSupplyProductValidator, SetConfirmedGoodsSupplyProductModelRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(isValidRequest.Error!);

        RequestGoodsSupplyProduct? value = null;
        if (request.ProductEntity is null)
        {
            var goodsSupplyProduct = await _mediator.Send(new GetRequestGoodsSupplyProductByIdQuery(request.Id), ct);
            if (goodsSupplyProduct.IsFailure)
                return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(goodsSupplyProduct.Error!);
            value = goodsSupplyProduct.Value!;
        }
        else
            value = request.ProductEntity!;

        if (value.RequestGoodsSupply.Type != GoodsSupplyType.Project && value.RequestGoodsSupply.Type != GoodsSupplyType.Contractor)
        {
            if (value.Status != GoodsSupplyDetailStatus.SupplyUnitPending && value.Status != GoodsSupplyDetailStatus.ManagementConfirmed)
                return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(RequestGoodsSupplyManagementErrors.InValidRequestGoodsSupplyType);
            else
            {
                var lastDescription = await DescriptionMacker(request.Description, GoodsSupplyDetailStatus.SupplyUnitPending, ct);
                value.SetStatus(GoodsSupplyDetailStatus.SupplyUnitPending, lastDescription);
                value.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                {
                    if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                        oo.UpdateStatus(GoodsSupplyDetailStatus.SupplyUnitPending, lastDescription);
                });
            }
        }

        if (value.RequestGoodsSupply.Type == GoodsSupplyType.Project || value.RequestGoodsSupply.Type == GoodsSupplyType.Contractor)
            if (value.Status != GoodsSupplyDetailStatus.ManagementConfirmed)
                return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(RequestGoodsSupplyManagementErrors.InValidRequestGoodsSupplyType);

        var detail = request.Detail;
        try
        {
            if (request.Detail is null)
            {
                var managment = await RollBackManagment(_inviceIds, _commerceIds, ct);
                return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(RequestGoodsSupplyErrors.InValidDetails);
            }

            var currentUser = _userProfileService.GetProfileInfo();
            var managements = new List<CreateRequestGoodsSupplyManagementModel>();
            long costCenterId;
            long projectId;
            long? projectOperationId = null;
            if (value.RequestGoodsSupply.IsProjectSupply)
            {
                costCenterId = value.RequestGoodsSupply.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId;
                projectId = value.RequestGoodsSupply.Project.Id;
            }
            else
            {
                costCenterId = value.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenterId;
                projectId = value.RequestGoodsSupply.ProjectOperation.Project.Id;
                projectOperationId = value.RequestGoodsSupply.ProjectOperation.Id;
            }

            var costCenterWarehousesRes = await _mediator.Send(new GetsCostCenterWarehouseByCostCenterIdQuery(costCenterId, 1, 100), ct);
            if (costCenterWarehousesRes.IsFailure || costCenterWarehousesRes.Value is null || costCenterWarehousesRes.Value.Data is null)
            {
                var managment = await RollBackManagment(_inviceIds, _commerceIds, ct);
                return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(RequestGoodsSupplyDetailErrors.InValidWarehouse);
            }
            var costCenterWarehouses = costCenterWarehousesRes.Value.Data;

            var productsResponse = await _productRepo.GetProductByIds([value.ProductId], ct);
            if (productsResponse is null || productsResponse.Count < 1)
            {
                var managment = await RollBackManagment(_inviceIds, _commerceIds, ct);
                return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(RequestGoodsSupplyErrors.ProdouctNotFound);
            }

            var productModels = new List<SetConfirmedProjectGoodsSupplyModel>();
            var exitForConsumes = new List<CreateExitForConsumesModel>();
            var exitForRelocations = new List<CreateExitForRelocationModel>();

            var sumCount = detail.Commerce?.RequestedCount + detail.InStocks?.Sum(x => x.RequestedCount) + detail.BetweenStocks?.Sum(x => x.RequestedCount);
            if (sumCount > value.RequestedCount)
            {
                var managment = await RollBackManagment(_inviceIds, _commerceIds, ct);
                return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(RequestGoodsSupplyManagementErrors.InValidRequestCount);
            }

            long productId = value.ProductId;
            long groupId = value.ProductGroupId;
            var productValue = productsResponse.FirstOrDefault()!;
            if (detail.AlternateId != null && detail.AlternateId.HasValue && detail.AlternateId > 0)
            {
                double rate = 1;
                var queryAlternatives = await _mediator.Send(new GetAlternativeByIdQuery(detail.AlternateId!.Value), ct);
                if (queryAlternatives.IsFailure)
                {
                    var managment = await RollBackManagment(_inviceIds, _commerceIds, ct);
                    return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(queryAlternatives.Error!);
                }
                var alternate = queryAlternatives.Value!;

                rate = alternate.Rate;
                productId = alternate.ProductId;
                var queryProducts = await _productRepo.GetProductByIds([value.ProductId], ct);
                if (queryProducts is null || queryProducts.Count < 1)
                {
                    var managment = await RollBackManagment(_inviceIds, _commerceIds, ct);
                    return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(RequestGoodsSupplyErrors.ProdouctNotFound);
                }
                productValue = queryProducts.FirstOrDefault()!;

                var sumRequestCount = (detail.InStocks?.Sum(oo => oo.RequestedCount / (decimal)rate)) +
                    (detail.BetweenStocks?.Sum(oo => oo.RequestedCount / (decimal)rate)) + (detail.Commerce != null ? detail.Commerce.RequestedCount : 0);
                if (value.RequestedCount < sumRequestCount)
                {
                    var managment = await RollBackManagment(_inviceIds, _commerceIds, ct);
                    return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(RequestGoodsSupplyManagementErrors.InValidRequestedCount);
                }
            }

            long measureUnitId = productValue.Group.MeasureId;
            productModels.Add(new SetConfirmedProjectGoodsSupplyModel(value, productId, detail.AlternateId, measureUnitId));

            if (detail.Commerce is not null)
            {
                if (string.IsNullOrEmpty(detail.Commerce.CommerceDescription))
                    detail.Commerce.CommerceDescription = request.Description;
                var commerce = await CreateCommerceGoodsSupplyProduct(value, costCenterWarehouses, currentUser.UserId, detail.Commerce,
                    costCenterId, projectId, projectOperationId, ct);
                if (commerce.IsFailure || commerce is null || commerce!.Value is null)
                {
                    var managment = await RollBackManagment(_inviceIds, _commerceIds, ct);
                    return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(commerce?.Error!);
                }

                _commerceIds.Add((long)commerce.Value!.InvoiceId!);
                managements.Add(commerce.Value!);
            }

            if (detail.InStocks is not null && detail.InStocks.Count > 0)
            {
                var stocksManagement = await CreateInStocksManagementProduct(value, detail.InStocks, costCenterWarehouses, currentUser.UserId, _inviceIds, _commerceIds, ct);
                if (stocksManagement.IsFailure || stocksManagement is null || stocksManagement!.Value is null)
                    return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(stocksManagement?.Error!);

                if (stocksManagement.Value.Any())
                {
                    var inviceIds = stocksManagement.Value.Where(x => x.InvoiceId.HasValue).Select(x => x.InvoiceId!.Value).ToList();
                    if (inviceIds.Any())
                        _inviceIds = _inviceIds.Concat(inviceIds).Distinct().ToList();
                }

                managements.AddRange(stocksManagement.Value);
            }

            if (detail.BetweenStocks is not null && detail.BetweenStocks.Count > 0)
            {
                var stocksManagement = await CreateBetweenStocksManagementProduct(value, detail.BetweenStocks, costCenterWarehouses, currentUser.UserId, _inviceIds, _commerceIds, ct);
                if (stocksManagement.IsFailure || stocksManagement is null || stocksManagement!.Value is null)
                    return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(stocksManagement?.Error!);

                if (stocksManagement.Value.Any())
                {
                    var inviceIds = stocksManagement.Value.Where(x => x.InvoiceId.HasValue).Select(x => x.InvoiceId!.Value).ToList();
                    if (inviceIds.Any())
                        _inviceIds = _inviceIds.Concat(inviceIds).Distinct().ToList();
                }
                managements.AddRange(stocksManagement.Value);
            }

            foreach (var item in managements)
            {
                var lastDescManagement = await DescriptionMacker(request.Description, item.Type, GoodsSupplyManagementStatus.Pending, ct);

                var createResponse = await _mediator.Send(new CreateRequestGoodsSupplyManagementCommand(item.Detail, item.ProductId, item.WarehouseId, item.DestinationWarehouseId,
                    item.InvoiceId, item.RequestedCount, item.AlternateId, item.Type, item.Description, lastDescManagement), ct);
                if (createResponse.IsFailure)
                {
                    var managment = await RollBackManagment(_inviceIds, _commerceIds, ct);
                    return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(createResponse.Error!);
                }
            }

            var lastDescription = await DescriptionMacker(request.Description, GoodsSupplyDetailStatus.PendingForSupply, ct);
            foreach (var item in value.RequestGoodsSupplyDetails)
                item.UpdateStatus(GoodsSupplyDetailStatus.PendingForSupply, lastDescription);

            value.UpdateStatus(value, currentUser.UserId, lastDescription);

            await _unitOfWork.CommitAsync(ct);
            return new SetConfirmedGoodsSupplyProductResponse(true);
        }
        catch (Exception ex)
        {
            var managment = await RollBackManagment(_inviceIds, _commerceIds, ct);

            _logger.LogError(ex, ex.Message);
            return Result.Failure<SetConfirmedGoodsSupplyProductResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<UpdateRequestGoodsSupplyManagementResponse?>> UpdateRequestGoodsSupplyManagement(UpdateRequestGoodsSupplyManagementRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateRequestGoodsSupplyManagementValidator, UpdateRequestGoodsSupplyManagementRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateRequestGoodsSupplyManagementResponse>(isValidRequest.Error!);

        var responseUpdate = await UpdateSupplyManagement(request, ct);
        if (responseUpdate.IsFailure)
            return Result.Failure<UpdateRequestGoodsSupplyManagementResponse>(responseUpdate.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateRequestGoodsSupplyManagementResponse(true);
    }

    public async Task<Result<UpdateRequestGoodsSupplyManagementsResponse?>> UpdateRequestGoodsSupplyManagements(UpdateRequestGoodsSupplyManagementsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateRequestGoodsSupplyManagementsValidator, UpdateRequestGoodsSupplyManagementsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateRequestGoodsSupplyManagementsResponse>(isValidRequest.Error!);

        foreach (var item in request.RequestGoodsSupplyManagements)
        {
            var responseUpdate = await UpdateSupplyManagement(item, ct);
            if (responseUpdate.IsFailure)
                return Result.Failure<UpdateRequestGoodsSupplyManagementsResponse>(responseUpdate.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateRequestGoodsSupplyManagementsResponse(true);
    }

    public async Task<Result<GetFilteredAlternativeProductsResponse?>> GetFilteredAlternativeProductsRequestAsync(GetFilteredAlternativeProductsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredAlternativeProductsValidator, GetFilteredAlternativeProductsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredAlternativeProductsResponse>(isValidRequest.Error!);

        var getManagementDetailQuery = await _mediator.Send(new GetFilteredAlternativeProductsQuery(null, null, [request.RequestGoodsSupplyDetailId]), ct);
        if (getManagementDetailQuery.IsFailure)
            return Result.Failure<GetFilteredAlternativeProductsResponse>(getManagementDetailQuery.Error!);
        var requestGoodsSupplyDetail = getManagementDetailQuery.Value!.Data!.FirstOrDefault()!;

        var queryAlternatives = await _mediator.Send(new GetByProductIdsQuery([requestGoodsSupplyDetail.ProductId], request.PageIndex, request.PageSize), ct);
        if (queryAlternatives.IsFailure)
            return Result.Failure<GetFilteredAlternativeProductsResponse>(queryAlternatives.Error!);
        var alternative = queryAlternatives.Value!.Data!;

        var productIds = alternative.Where(x => x.ProductId > 0).Select(x => x.ProductId).Distinct().ToList();
        var queryProducts = await _mediator.Send(new GetsProductByIdQuery(1, productIds.Count, null, productIds), ct);
        if (queryProducts.IsFailure)
            return Result.Failure<GetFilteredAlternativeProductsResponse>(queryProducts.Error!);
        var products = queryProducts.Value!.Data!;

        var groupIds = alternative.Select(c => c.GroupId!).Distinct().ToList();
        var queryGroups = await _mediator.Send(new GetGroupsByIdsQuery(groupIds, 1, groupIds.Count), ct);
        if (queryGroups.IsFailure)
            return Result.Failure<GetFilteredAlternativeProductsResponse>(queryGroups.Error!);
        var groups = queryGroups.Value!.Data!;

        var result = (from alternatives in alternative

                      join p in products on alternatives.ProductId equals p.Id into pJoin
                      from pResult in pJoin.DefaultIfEmpty()

                      join g in groups on alternatives.GroupId equals g.Id into gJoin
                      from gResult in gJoin.DefaultIfEmpty()

                      where pResult.Code.Equals(request.FilterData!) ||
                            pResult.Name.Equals(request.FilterData!) ||
                            request.FilterData == null

                      select new GetFilteredAlternativeProductsModel
                      {
                          Id = alternatives.Id,
                          ProductId = alternatives.ProductId,
                          ProductCode = pResult.Code.ToString(),
                          ProductName = pResult.Name,
                          Brand = pResult.Brand,
                          BrandModel = pResult.BrandModel,
                          GroupId = gResult.Id,
                          Measure = pResult.Group.Measure,
                          RequestedCount = requestGoodsSupplyDetail.RequestedCount * (decimal)alternatives.Rate
                      }).ToList();

        return new GetFilteredAlternativeProductsResponse(result, queryAlternatives.Value!.RowCount!);
    }

    public async Task<Result<GetFilteredManagementRequestGoodsSuppliesResponse?>> GetFilteredRequestGoodsSupplyManagements(GetFilteredManagementRequestGoodsSuppliesRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredManagementRequestGoodsSuppliesValidator, GetFilteredManagementRequestGoodsSuppliesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredManagementRequestGoodsSuppliesResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 ? null : _userInfoService.UserCompanyId;
        var queryRequestGoodsSupplies = await _mediator.Send(new GetFilteredRequestGoodsSuppliesQuery(
            null, request.CostCenterIds, request.ProjectIds, request.ProjectManagerId,
            request.CreatorIds, request.ProductIds, request.FromDate, request.ToDate,
            request.Statuses, request.RemoveStatuses, request.Types, request.RemoveTypes, request.FilterData,
            companyId, request.CustomerInvoiceNumber, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (queryRequestGoodsSupplies.IsFailure)
            return Result.Failure<GetFilteredManagementRequestGoodsSuppliesResponse>(queryRequestGoodsSupplies.Error!);
        var requestGoodsSupplies = queryRequestGoodsSupplies.Value!.Data!;

        List<Company>? companies = [];
        var companyIds = requestGoodsSupplies.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => x.CompanyId).ToList();
        companies = await CompanyDataReceiver(companyIds, ct);

        var creatorIds = requestGoodsSupplies.Select(x => x.CreatorId).ToList();
        var creatorsInfo = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);
        var measurementIds = requestGoodsSupplies.Select(x => x.ProjectOperation).Where(x => x.UnitOfMeasurementId != 0).Select(c => c.UnitOfMeasurementId).Distinct().ToList();
        var measureunits = await WebServicesLogic.MeasurementDataReceiver(measurementIds, _mediator, ct);

        var dataList = new List<GetFilteredManagementRequestGoodsSuppliesModel>();
        foreach (var item in requestGoodsSupplies)
        {
            var measureunit = measureunits?.Where(x => x.Id == item.ProjectOperation.UnitOfMeasurementId).FirstOrDefault();
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            var reviewerModel = RequestReviewer(item);
            dataList.Add(new GetFilteredManagementRequestGoodsSuppliesModel()
            {
                Id = item.Id,
                Created = TimeCalculator.DatePiker(item.Created),
                MeasurementName = measureunit?.Name,
                Workload = item.ProjectOperation.Workload,
                RequestNumber = item.RequestSerialNumber,
                CostCenterId = item.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                CostCenterName = item.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                ProjectId = item.ProjectOperation.Project.Id,
                ProjectName = item.ProjectOperation.Project.ProjectName,
                OperationInfoName = item.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoCode = item.ProjectOperation.OperationInfo.OperationInfoCode,
                FinalAmount = item.ProjectOperationDetail?.FinalAmount,
                PrivateName = item.ProjectOperationDetail?.OperationLocation.PrivateName,
                PrivateCode = item.ProjectOperationDetail?.OperationLocation.PrivateCode,
                PublicName = item.ProjectOperationDetail?.OperationLocation.PublicName,
                PublicCode = item.ProjectOperationDetail?.OperationLocation.PublicCode,
                ProjectOperationDetailDescription = item.ProjectOperationDetail?.Description,
                Status = item.Status,
                Type = item.Type,
                Importance = item.RequestGoodsSupplyDetails?.FirstOrDefault()?.Importance,
                CreatorId = item.CreatorId,
                Creator = creatorsInfo?.Where(oo => oo!.UserId.Equals(item.CreatorId))?.FirstOrDefault()?.FullName,
                Percent = reviewerModel.Percent,
                RejectedNumber = reviewerModel.RejectedNumber,
                AllInStock = reviewerModel.AllInStock,
                InStockNumber = reviewerModel.InStockNumber,
                AllBetweenStock = reviewerModel.AllBetweenStock,
                BetweenStockNumber = reviewerModel.BetweenStockNumber,
                AllCommerce = reviewerModel.AllCommerce,
                CommerceNuber = reviewerModel.CommerceNuber,
                CompanyId = item.CompanyId,
                CompanyNameFa = company?.NameFa,
            });
        }

        return new GetFilteredManagementRequestGoodsSuppliesResponse(dataList, queryRequestGoodsSupplies.Value.RowCount);
    }

    public async Task<Result<GetsProjectManagerRequestGoodsSupplieResponse?>> GetsProjectManagerRequestGoodsSupplie(GetsProjectManagerRequestGoodsSupplieRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsProjectManagerRequestGoodsSupplieValidator, GetsProjectManagerRequestGoodsSupplieRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectManagerRequestGoodsSupplieResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 ? null : _userInfoService.UserCompanyId;
        var queryRequestGoodsSupplies = await _mediator.Send(new GetsProjectManagerRequestGoodsSupplieQuery(
            null, request.CostCenterIds, request.ProjectIds, request.ProjectManagerId,
            request.CreatorIds, request.ProductIds, request.FromDate, request.ToDate,
            request.Statuses, request.RemoveStatuses, request.Types, request.FilterData,
            companyId, request.CustomerInvoiceNumber, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (queryRequestGoodsSupplies.IsFailure)
            return Result.Failure<GetsProjectManagerRequestGoodsSupplieResponse>(queryRequestGoodsSupplies.Error!);
        var requestGoodsSupplies = queryRequestGoodsSupplies.Value!.Data!;

        List<Company>? companies = [];
        var companyIds = requestGoodsSupplies.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => x.CompanyId).ToList();
        companies = await CompanyDataReceiver(companyIds, ct);

        var creatorIds = requestGoodsSupplies.Select(x => x.CreatorId).ToList();
        var creatorsInfo = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);
        var measurementIds = requestGoodsSupplies.Select(x => x.ProjectOperation).Where(x => x.UnitOfMeasurementId != 0).Select(c => c.UnitOfMeasurementId).Distinct().ToList();
        var measureunits = await WebServicesLogic.MeasurementDataReceiver(measurementIds, _mediator, ct);

        var dataList = new List<GetsProjectManagerRequestGoodsSupplieModel>();
        foreach (var item in requestGoodsSupplies)
        {
            var measureunit = measureunits?.Where(x => x.Id == item.ProjectOperation.UnitOfMeasurementId).FirstOrDefault();
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            var reviewerModel = RequestReviewer(item);
            dataList.Add(new GetsProjectManagerRequestGoodsSupplieModel()
            {
                Id = item.Id,
                Created = TimeCalculator.DatePiker(item.Created),
                CreatedShamsi = TimeCalculator.ConvertToShamsi(item.Created),
                MeasurementName = measureunit?.Name,
                Workload = item.ProjectOperation.Workload,
                RequestNumber = item.RequestSerialNumber,
                CostCenterId = item.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.Id,
                CostCenterName = item.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                ProjectId = item.ProjectOperation.Project.Id,
                ProjectName = item.ProjectOperation.Project.ProjectName,
                OperationInfoName = item.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoCode = item.ProjectOperation.OperationInfo.OperationInfoCode,
                FinalAmount = item.ProjectOperationDetail?.FinalAmount,
                PrivateName = item.ProjectOperationDetail?.OperationLocation.PrivateName,
                PrivateCode = item.ProjectOperationDetail?.OperationLocation.PrivateCode,
                PublicName = item.ProjectOperationDetail?.OperationLocation.PublicName,
                PublicCode = item.ProjectOperationDetail?.OperationLocation.PublicCode,
                ProjectOperationDetailDescription = item.ProjectOperationDetail?.Description,
                Status = item.Status,
                Type = item.Type,
                Importance = item.RequestGoodsSupplyDetails?.FirstOrDefault()?.Importance,
                CreatorId = item.CreatorId,
                Creator = creatorsInfo?.Where(oo => oo!.UserId.Equals(item.CreatorId))?.FirstOrDefault()?.FullName,
                Percent = reviewerModel.Percent,
                RejectedNumber = reviewerModel.RejectedNumber,
                AllInStock = reviewerModel.AllInStock,
                InStockNumber = reviewerModel.InStockNumber,
                AllBetweenStock = reviewerModel.AllBetweenStock,
                BetweenStockNumber = reviewerModel.BetweenStockNumber,
                AllCommerce = reviewerModel.AllCommerce,
                CommerceNuber = reviewerModel.CommerceNuber,
                CompanyId = item.CompanyId,
                CompanyNameFa = company?.NameFa,
                RequestedDate = TimeCalculator.DatePiker(item.RequestedDate),
                RequestedDateShamsi = TimeCalculator.ConvertToShamsi(item.RequestedDate)
            });
        }

        return new GetsProjectManagerRequestGoodsSupplieResponse(dataList, queryRequestGoodsSupplies.Value.RowCount);
    }

    public async Task<Result<GetsManagementGoodsSupplyForWarehouseResponse?>> GetsManagementGoodsSupplyForWarehouse(GetsManagementGoodsSupplyForWarehouseRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsManagementGoodsSupplyForWarehouseValidator, GetsManagementGoodsSupplyForWarehouseRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsManagementGoodsSupplyForWarehouseResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetsManagementGoodsSupplyForWarehouseQuery(request.InvoiceId, request.RequestGoodsSupplyId, request.CommercialRequestNo, request.ProductId), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsManagementGoodsSupplyForWarehouseResponse>(responses.Error!);
        var values = responses.Value!;

        var managements = new List<GetsManagementGoodsSupplyForWarehouseModel>();
        foreach (var value in values)
        {
            managements.Add(new GetsManagementGoodsSupplyForWarehouseModel()
            {
                Id = value.Id,
                InvoiceId = value!.InvoiceId,
                CostCenterId = value.RequestGoodsSupplyProduct.RequestGoodsSupplyDetails.FirstOrDefault().ProjectProduct is null ?
                value!.RequestGoodsSupplyProduct!.RequestGoodsSupply.ProjectOperation!.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId :
                value!.RequestGoodsSupplyProduct!.RequestGoodsSupply.Project!.ProjectCostCenters.FirstOrDefault()!.CostCenterId,
                ProjectId = value.RequestGoodsSupplyProduct.RequestGoodsSupplyDetails.FirstOrDefault().ProjectProduct is null ?
                value!.RequestGoodsSupplyProduct!.RequestGoodsSupply.ProjectOperation!.Project.Id :
                value!.RequestGoodsSupplyProduct!.RequestGoodsSupply.Project!.Id,
                ProjectOperationId = value.RequestGoodsSupplyProduct.RequestGoodsSupplyDetails.FirstOrDefault().ProjectProduct is null ?
                value!.RequestGoodsSupplyProduct!.RequestGoodsSupply.ProjectOperation.Id :
                null,
                ProjectOperationDetailId = value.RequestGoodsSupplyProduct.RequestGoodsSupplyDetails.FirstOrDefault().ProjectProduct is null ?
                value!.RequestGoodsSupplyProduct.RequestGoodsSupplyDetails.FirstOrDefault()?.ConsumableVolumeProduct.ProjectOperationDetail is not null ?
                value!.RequestGoodsSupplyProduct.RequestGoodsSupplyDetails.FirstOrDefault()?.ConsumableVolumeProduct.ProjectOperationDetail.Id :
                null :
                null,
                RequestedCount = value.RequestedCount,
                Type = value.Type,
                Status = value.Status,
                Description = value.Description,
                LastDescription = value.LastDescription
            });
        }

        return new GetsManagementGoodsSupplyForWarehouseResponse(managements, values.Count);
    }

    public async Task<Result<GetsSourceWarehouseResponse?>> GetsSourceWarehouse(GetsSourceWarehouseRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsSourceWarehouseValidator, GetsSourceWarehouseRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsSourceWarehouseResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestGoodsSupplyProductByIdQuery(request.RequestGoodsSupplyProductId), ct);
        if (response.IsFailure)
            return Result.Failure<GetsSourceWarehouseResponse>(response.Error!);
        var value = response.Value!;
        long productId = value.ProductId;
        long groupId = value.ProductGroupId;
        var costCenterId = value.RequestGoodsSupply.ProjectOperation?.Project.ProjectCostCenters.FirstOrDefault().CostCenterId ?? value.RequestGoodsSupply.Project?.ProjectCostCenters.FirstOrDefault().CostCenterId;
        double rate = 1;

        if (request.AlternateId != null && request.AlternateId.HasValue)
        {
            var queryAlternatives = await _mediator.Send(new GetAlternativeByIdQuery(request.AlternateId!.Value), ct);
            if (queryAlternatives.IsFailure)
                return Result.Failure<GetsSourceWarehouseResponse>(RequestGoodsSupplyDetailErrors.InValidAlternate);
            var alternate = queryAlternatives.Value!;

            rate = alternate.Rate;
            productId = alternate.ProductId;
        }

        var responses = await _mediator.Send(new GetsCostCenterWarehouseForSupplyManagementQuery(), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsSourceWarehouseResponse>(RequestGoodsSupplyDetailErrors.WarehouseNotFound);
        var values = responses.Value!.Data!.OrderByDescending(x => x.CostCenter.Id == costCenterId).ThenByDescending(x => x.IsDefault).ToList();

        var warehouseIds = values.Select(oo => oo.WarehouseId).Distinct().ToList();
        var getsWarehouse = await _mediator.Send(new GetFilteredWarehousesByGroupIdQuery(warehouseIds, groupId, request.FilterData, 1, warehouseIds.Count), ct);
        if (getsWarehouse.IsFailure || getsWarehouse.Value is null || getsWarehouse.Value.Data is null)
            return Result.Failure<GetsSourceWarehouseResponse>(RequestGoodsSupplyDetailErrors.InValidWarehouse2);
        var warehouseAssetIds = getsWarehouse.Value.Data.Select(x => (long)x.Id!).ToList();
        var warhouses = getsWarehouse.Value!.Data!;

        List<FilteredInventory>? inventories = [];
        var getsProductWarehouse = await _mediator.Send(new GetProductInventoryByFilterQuery(productId, null, request.FilterData, 0, 0), ct);
        if (getsProductWarehouse.Value?.Data is null)
            return Result.Failure<GetsSourceWarehouseResponse>(RequestGoodsSupplyDetailErrors.InValidWarehouse2);

        var result = new List<GetsSourceWarehouseModel>();
        inventories = getsProductWarehouse.Value!.Data;
        foreach (var item in inventories)
        {
            var costCenterWarehouse = values.FirstOrDefault(x => x.WarehouseId == item.WareHouseId);
            var data = new GetsSourceWarehouseModel()
            {
                WarehouseId = item?.WareHouseId,
                WarehouseCode = item?.WareHouseCode,
                WarehouseName = item?.WareHouseName,
                CostCenterId = costCenterWarehouse?.CostCenter.Id,
                CostCenterName = costCenterWarehouse?.CostCenter.CostCenterName,
                RequestedCount = value.RequestedCount * (decimal)rate,
                InStockCount = item?.RealQuantity,
                RequestQuantity = item?.RequestQuantity,
                IsDefault = costCenterWarehouse is null ? false : costCenterWarehouse.IsDefault,
            };

            if (data.CostCenterId == costCenterId && data.IsDefault)
                data.WarehouseName = $"{data.WarehouseName}(اصلی و پیش فرض)";
            else if (data.CostCenterId == costCenterId && !data.IsDefault)
                data.WarehouseName = $"{data.WarehouseName}(اصلی)";

            result.Add(data);
        }
        result = result.OrderByDescending(x => x.CostCenterId == costCenterId).ToList();

        return new GetsSourceWarehouseResponse(result, result.Count);
    }

    public async Task<Result<GetsDestinationWarehouseResponse?>> GetsDestinationWarehouse(GetsDestinationWarehouseRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsDestinationWarehouseValidator, GetsDestinationWarehouseRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsDestinationWarehouseResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestGoodsSupplyDetailByIdQuery(request.RequestGoodsSupplyDetailId), ct);
        if (response.IsFailure)
            return Result.Failure<GetsDestinationWarehouseResponse>(response.Error!);
        var value = response.Value!;
        var costCenterId = value.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id;
        long productId = value.ProductId;
        double rate = 1;

        if (request.AlternateId != null && request.AlternateId.HasValue)
        {
            var queryAlternatives = await _mediator.Send(new GetAlternativeByIdQuery(request.AlternateId!.Value), ct);
            if (queryAlternatives.IsFailure)
                return Result.Failure<GetsDestinationWarehouseResponse>(RequestGoodsSupplyDetailErrors.InValidAlternate);
            var alternate = queryAlternatives.Value!;

            rate = alternate.Rate;
            productId = alternate.ProductId;
        }

        var responses = await _mediator.Send(new GetsCostCenterWarehouseByCostCenterIdQuery(costCenterId, 1, 100), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsDestinationWarehouseResponse>(RequestGoodsSupplyDetailErrors.InValidWarehouse2);
        var values = responses.Value!.Data!.OrderByDescending(x => x.IsDefault).ToList();

        var warehouseIds = values.Select(oo => oo.WarehouseId).Distinct().ToList();
        var getsWarehouse = await _mediator.Send(new GetProductInventoryByFilterQuery(productId, warehouseIds, request.FilterData, 0, 0), ct);
        if (getsWarehouse.IsFailure || getsWarehouse.Value is null || getsWarehouse.Value.Data is null)
            return Result.Failure<GetsDestinationWarehouseResponse>(RequestGoodsSupplyDetailErrors.InValidWarehouse2);
        var warhouses = getsWarehouse.Value!.Data!;

        var result = new List<GetsDestinationWarehouseModel>();
        foreach (var item in warhouses)
        {
            var costCenterWarehouse = values.FirstOrDefault(x => x.WarehouseId == item.WareHouseId);
            var data = new GetsDestinationWarehouseModel()
            {
                WarehouseId = item?.WareHouseId,
                WarehouseCode = item?.WareHouseCode,
                WarehouseName = item?.WareHouseName,
                CostCenterId = costCenterWarehouse?.CostCenter.Id,
                CostCenterName = costCenterWarehouse?.CostCenter.CostCenterName,
                RequestedCount = value.RequestedCount * (decimal)rate,
                InStockCount = item?.RealQuantity,
                RequestQuantity = item?.RequestQuantity,
                IsDefault = costCenterWarehouse is null ? false : costCenterWarehouse.IsDefault,
            };

            if (data.CostCenterId == costCenterId && data.IsDefault)
                data.WarehouseName = $"{data.WarehouseName}(اصلی و پیش فرض)";
            else if (data.CostCenterId == costCenterId && !data.IsDefault)
                data.WarehouseName = $"{data.WarehouseName}(اصلی)";

            result.Add(data);
        }

        return new GetsDestinationWarehouseResponse(result, result.Count);
    }

    public async Task<Result<GetRequestGoodsSupplyProductResponse?>> GetRequestGoodsSupplyProduct(GetRequestGoodsSupplyProductRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestGoodsSupplyProductValidator, GetRequestGoodsSupplyProductRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestGoodsSupplyProductResponse>(isValidRequest.Error!);

        var getRequestGoodsSupplyManagementProductsQuery = await _mediator.Send(new GetRequestGoodsSupplyManagementProductsQuery(request.Id, request.Type, request.PageIndex, request.PageSize), ct);
        if (getRequestGoodsSupplyManagementProductsQuery.IsFailure)
            return Result.Failure<GetRequestGoodsSupplyProductResponse>(getRequestGoodsSupplyManagementProductsQuery.Error!);
        var requestGoodsSupplyManagements = getRequestGoodsSupplyManagementProductsQuery.Value!.Data!;
        var rowCount = getRequestGoodsSupplyManagementProductsQuery.Value!.RowCount!;

        var requestGoodsSupply = requestGoodsSupplyManagements.Select(oo => oo.RequestGoodsSupplyProduct).Select(oo => oo!.RequestGoodsSupply).FirstOrDefault();

        var productIds = requestGoodsSupplyManagements.Where(x => x.ReferenceId is not null && x.ReferenceId > 0).Select(x => (long)x.ReferenceId!).Distinct().ToList();
        var productsQuery = await _mediator.Send(new GetsProductByIdQuery(1, productIds.Count, null, productIds), ct);
        var products = productsQuery.Value?.Data!;

        var productModel = new List<GetRequestGoodsSupplyProductModel>();
        foreach (var requestGoodsSupplyManagement in requestGoodsSupplyManagements)
        {
            var product = products.Where(oo => oo.Id == requestGoodsSupplyManagement.ReferenceId).FirstOrDefault();
            var locations = requestGoodsSupplyManagement.RequestGoodsSupplyProduct!.RequestGoodsSupplyDetails.Select(x => x.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation).ToList();
            productModel.Add(new GetRequestGoodsSupplyProductModel()
            {
                Id = requestGoodsSupplyManagement.Id,
                DelivaryDeadLine = requestGoodsSupplyManagement.RequestGoodsSupplyProduct!.DelivaryDeadLine,
                PrivateName = StringSeparator.JoinWithDash(locations, x => x.PrivateName ?? " "),
                PrivateCode = StringSeparator.JoinWithDash(locations, x => x.PrivateCode ?? " "),
                PublicName = StringSeparator.JoinWithDash(locations, x => x.PublicName ?? " "),
                PublicCode = StringSeparator.JoinWithDash(locations, x => x.PublicCode ?? " "),
                ProductId = requestGoodsSupplyManagement.ReferenceId,
                RequestedCount = requestGoodsSupplyManagement.RequestedCount,
                Status = requestGoodsSupplyManagement.Status,
                Type = requestGoodsSupplyManagement.Type,
                ProductBrand = product?.Brand,
                ProductBrandModel = product?.BrandModel,
                ProductCode = product?.Code,
                ProductMeasure = product?.Group.Measure,
                ProductName = product?.Name,
                WarehouseId = requestGoodsSupplyManagement?.WarehouseId,
                InvoiceId = requestGoodsSupplyManagement?.InvoiceId,
                LastDescription = requestGoodsSupplyManagement?.LastDescription,
            });
        }

        var detail = new GetRequestGoodsSupplyProductDetail()
        {
            Products = productModel,
            RowCount = rowCount
        };

        return new GetRequestGoodsSupplyProductResponse()
        {
            Id = requestGoodsSupply?.Id,
            RequestNumber = requestGoodsSupply?.Id,
            CostCenterName = requestGoodsSupply is not null && requestGoodsSupply.ProjectOperation is not null && requestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.Any() ? requestGoodsSupply?.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName : null,
            ProjectName = requestGoodsSupply?.ProjectOperation.Project.ProjectName,
            OperationInfoName = requestGoodsSupply?.ProjectOperation.OperationInfo.OperationInfoName,
            Type = requestGoodsSupply?.Type,
            Detail = detail
        };
    }

    public async Task<Result<GetsSupplyManagementExcelExporterResponse?>> GetsSupplyManagementExcelExporter(GetsSupplyManagementExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsSupplyManagementExcelExporter, ProjectIds:{ProjectIds},", request.ProjectIds);

        var isValidRequest = await request.IsValidAsync<GetsSupplyManagementExcelExporterValidator, GetsSupplyManagementExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsSupplyManagementExcelExporterResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 ? null : _userInfoService.UserCompanyId;
        var responses = await _mediator.Send(new GetFilteredRequestGoodsSuppliesQuery(
            request.Ids, request.CostCenterIds, request.ProjectIds, request.ProjectManagerId,
            request.CreatorIds, request.ProductIds, request.FromDate, request.ToDate,
            request.Statuses, request.RemoveStatuses, request.Types, request.RemoveTypes,
            request.FilterData, companyId, request.CustomerInvoiceNumber, request.OrderBy,
            request.PageIndex, request.PageSize), ct);
        if (responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<GetsSupplyManagementExcelExporterResponse>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithFilterNotFound);
        var values = responses!.Value!.Data!;

        List<Company>? companies = [];
        var companyIds = values.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => x.CompanyId).ToList();
        companies = await CompanyDataReceiver(companyIds, ct);

        var userIds = values.Select(x => x.CreatorId).Distinct().ToList();
        var userInfos = await WebServicesLogic.UserDataReceiver(userIds, null, _mediator, ct);
        var measurementIds = values.Select(x => x.ProjectOperation.UnitOfMeasurementId).Distinct().ToList();
        var measurementInfos = await WebServicesLogic.MeasurementDataReceiver(measurementIds, _mediator, ct);

        var data = new List<GetsSupplyManagementExcelExporterResponseModel>();
        foreach (var item in values)
        {
            var reviewerModel = RequestReviewer(item);
            data.Add(new GetsSupplyManagementExcelExporterResponseModel()
            {
                Id = item.Id,
                CreatedOn = TimeCalculator.ConvertToShamsi(item.Created),
                MeasurementName = measurementInfos?.Where(x => x.Id == item.ProjectOperation.UnitOfMeasurementId).FirstOrDefault()?.Name,
                Workload = item.ProjectOperation.Workload,
                RequestNumber = item.RequestSerialNumber,
                CostCenterName = item.ProjectOperation.Project.ProjectCostCenters.Any() ? item.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName : null,
                ProjectName = item.ProjectOperation.Project.ProjectName,
                OperationInfoName = item.ProjectOperation.OperationInfo.OperationInfoName,
                FinalAmount = item.ProjectOperationDetail?.FinalAmount,
                PrivateName = item.ProjectOperationDetail?.OperationLocation.PrivateName,
                PrivateCode = item.ProjectOperationDetail?.OperationLocation.PrivateCode,
                PublicName = item.ProjectOperationDetail?.OperationLocation.PublicName,
                PublicCode = item.ProjectOperationDetail?.OperationLocation.PublicCode,
                ProjectOperationDetailDescription = item.ProjectOperationDetail?.Description,
                StatusDescription = item.Status.GetEnumDescription(),
                TypeDescription = item.Type.GetEnumDescription(),
                MaxImportanceDescription = item.RequestGoodsSupplyDetails?.FirstOrDefault()?.Importance?.GetEnumDescription(),
                CreatorId = item.CreatorId,
                Creator = userInfos?.Where(oo => oo!.UserId.Equals(item.CreatorId))?.FirstOrDefault()?.FullName,
                Percent = reviewerModel.Percent,
                RejectedNumber = reviewerModel.RejectedNumber,
                AllInStock = reviewerModel.AllInStock,
                InStockNumber = reviewerModel.InStockNumber,
                AllBetweenStock = reviewerModel.AllBetweenStock,
                BetweenStockNumber = reviewerModel.BetweenStockNumber,
                AllCommerce = reviewerModel.AllCommerce,
                CommerceNuber = reviewerModel.CommerceNuber,
                CompanyId = item.CompanyId,
                CompanyNameFa = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault()?.NameFa,
                MeasurementId = item.ProjectOperation.UnitOfMeasurementId,
            });
        }

        var file = new FileContentResult(SupplyManagementExcels.SupplyManagementToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"SupplyManagements-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsSupplyManagementExcelExporterResponse(file);
    }

    public async Task<Result<GetsSupplyManagementExcelEnumsResponse?>> GetsSupplyManagementExcelEnums(GetsSupplyManagementExcelEnumsRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<SupplyManagementExcelEnum>());
        return new GetsSupplyManagementExcelEnumsResponse(response);
    }

    public async Task<Result<GetsRequestGoodsSupplyManagementTypeResponse?>> GetsRequestGoodsSupplyManagementType(GetsRequestGoodsSupplyManagementTypeRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<GoodsSupplyManagementType>());
        return new GetsRequestGoodsSupplyManagementTypeResponse(response);
    }

    public async Task<Result<GetsRequestGoodsSupplyManagementStatusResponse?>> GetsRequestGoodsSupplyManagementStatus(GetsRequestGoodsSupplyManagementStatusRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<GoodsSupplyManagementStatus>());
        return new GetsRequestGoodsSupplyManagementStatusResponse(response);
    }

    public async Task<Result<GetRequestGoodsManagementHistoryByIdResponse?>> GetRequestGoodsManagementHistoryById(GetRequestGoodsManagementHistoryByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestGoodsManagementHistoryByIdValidator, GetRequestGoodsManagementHistoryByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestGoodsManagementHistoryByIdResponse>(isValidRequest.Error!);

        var query = await _mediator.Send(new GetRequestGoodsManagementHistoryByIdQuery(request.Id, request.PageIndex, request.PageSize), ct);
        if (query.IsFailure)
            return Result.Failure<GetRequestGoodsManagementHistoryByIdResponse>(query.Error!);
        if (query.Value is null || query.Value.Data is null || query.Value?.Data.Count <= 0)
            Result.Failure<GetRequestGoodsManagementHistoryByIdResponse>(RequestGoodsSupplyHistoryErrors.RequestGoodsSupplyManagementHistoryWithIdNotFound);
        var histories = query.Value?.Data;

        var requestGoodsSupplyCreatorIds = histories!.Select(c => c.CreatorId).Distinct().ToList();
        requestGoodsSupplyCreatorIds.AddRange(histories!.Select(c => c.RequestGoodsSupplyManagement.CreatorId).Distinct().ToList());
        var requestCreators = await WebServicesLogic.UserDataReceiver(requestGoodsSupplyCreatorIds.Distinct().ToList(), null, _mediator, ct);

        var requestGoodsSupplyProductIds = histories!.Select(c => c.RequestGoodsSupplyManagement.ReferenceId).Distinct().ToList();
        var requestProducts = await WebServicesLogic.ProductsDataReceiver(requestGoodsSupplyProductIds.Adapt<List<long>>().Distinct().ToList(), null, _mediator, _productRepo, ct);

        var requestGoodsSupplyWarehouseIds = histories!.Select(c => c.WarehouseId).Distinct().ToList();
        var requestWarehouses = await WebServicesLogic.WarehousesDataReceiver(requestGoodsSupplyWarehouseIds.Adapt<List<long?>>().Distinct().ToList(), _mediator, ct);

        var responseDetail = histories.Adapt<List<GetRequestGoodsManagementHistoryByIdManagementModel>>() ?? new List<GetRequestGoodsManagementHistoryByIdManagementModel>(0);
        responseDetail.ForEach(oo =>
        {
            var history = histories!.FirstOrDefault(x => x.Id == oo.Id);
            oo.Creator = requestCreators?.Where(c => c?.UserId == oo.CreatorId).FirstOrDefault()?.FullName;
            oo.ProductId = history!.RequestGoodsSupplyManagement.ReferenceId;
            oo.ProductName = requestProducts?.FirstOrDefault(x => x.Id == history!.RequestGoodsSupplyManagement.ReferenceId)?.Name;
            oo.ProductCode = requestProducts?.FirstOrDefault(x => x.Id == history!.RequestGoodsSupplyManagement.ReferenceId)?.Code;
            oo.Warehouse = requestWarehouses?.FirstOrDefault(x => x.Id == oo.WarehouseId)?.Name;
            oo.Created = TimeCalculator.ConvertToShamsi(history.Created);
            oo.AssignmentDate = TimeCalculator.ConvertToShamsi(history.AssignmentDate);
        });

        var history = histories!.FirstOrDefault();
        return new GetRequestGoodsManagementHistoryByIdResponse
        {
            CreatorId = history!.RequestGoodsSupplyManagement.CreatorId,
            ProductId = history!.RequestGoodsSupplyManagement.ReferenceId,
            Creator = requestCreators?.Where(c => c?.UserId == history.RequestGoodsSupplyManagement.CreatorId).FirstOrDefault()?.FullName,
            ProductName = requestProducts?.FirstOrDefault(x => x.Id == history!.RequestGoodsSupplyManagement.ReferenceId)?.Name,
            ProductCode = requestProducts?.FirstOrDefault(x => x.Id == history!.RequestGoodsSupplyManagement.ReferenceId)?.Code,
            RequestGoodsSupplyManagementd = history.RequestGoodsSupplyManagement.Id,
            RequestNumber = history.RequestGoodsSupplyManagement.RequestGoodsSupplyProduct!.SerialNumber + "-" + history.RequestGoodsSupplyManagement.RequestGoodsSupplyProduct!.Id.ToString(),
            Data = responseDetail,
            RowCount = query.Value?.RowCount,
            RequestedCount = history!.RequestGoodsSupplyManagement.RequestedCount,
        };
    }

    public async Task<Result<GetsGoodsSupplyManagmentBySupplyProductIdResponse?>> GetsGoodsSupplyManagmentBySupplyProductId(GetsGoodsSupplyManagmentBySupplyProductIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsGoodsSupplyManagmentBySupplyProductIdValidator, GetsGoodsSupplyManagmentBySupplyProductIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsGoodsSupplyManagmentBySupplyProductIdResponse>(isValidRequest.Error!);

        var query = await _mediator.Send(new GetsGoodsSupplyManagmentBySupplyProductIdQuery(request.Id), ct);
        if (query.IsFailure)
            return Result.Failure<GetsGoodsSupplyManagmentBySupplyProductIdResponse>(query.Error!);
        if (query.Value is null || query.Value.Data is null || query.Value!.Data!.Count <= 0)
            Result.Failure<GetsGoodsSupplyManagmentBySupplyProductIdResponse>(RequestGoodsSupplyHistoryErrors.RequestGoodsSupplyManagementHistoryWithIdNotFound);
        var values = query.Value!.Data!;

        var productIds = values.Where(x => x.ProductId is not null && x.ProductId > 0).Select(x => x.ProductId!.Value).Distinct().ToList();
        var products = await WebServicesLogic.ProductsDataReceiver(productIds, _mediator, _productRepo, ct);

        var warehouseIds = values.NullListed(x => x.DestinationWarehouseId);
        warehouseIds.AddRange(values.NullListed(x => x.WarehouseId));
        var warehouseRes = await _mediator.Send(new GetWarehouseByIdsQuery(warehouseIds, false, 1, warehouseIds.Count));
        List<GetWarehouseByIdsModel> warehouses = [];
        if (!warehouseRes.IsBad() && warehouseRes.Value.Data != null)
            warehouses = warehouseRes.Value.Data;

        var metaIds = values.Where(x => x.OperatorAppointmentId is not null).Select(x => x.OperatorAppointmentId!.Value).Distinct().ToList();
        var metaInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(metaIds, null, null, _mediator, ct);

        foreach (var management in values)
        {
            var manageProduct = products?.Where(m => m.Id == management.ProductId).FirstOrDefault();
            var mWarehouse = warehouses?.Where(x => x.Id.Equals(management.WarehouseId)).FirstOrDefault();
            var mDestinationWarehouse = warehouses?.Where(x => x.Id.Equals(management.DestinationWarehouseId)).FirstOrDefault();
            var operatorAppointment = metaInfos?.Where(x => x is not null && x.Id.Equals(management.OperatorAppointmentId)).FirstOrDefault();

            management.WarehouseName = mWarehouse?.Name;
            management.DestinationWarehouseName = mDestinationWarehouse?.Name;
            management.OperatorAppointmentName = operatorAppointment?.FullName;
            management.ProductName = manageProduct?.Name;
            management.ProductCode = manageProduct?.Code;
            management.Brand = manageProduct?.Brand;
            management.BrandModel = manageProduct?.BrandModel;
        }

        return new GetsGoodsSupplyManagmentBySupplyProductIdResponse(values);
    }

}
