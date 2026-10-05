using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Messengers;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Organizations;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Categories;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Groups;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Packages;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.WarehouseAssets;
using Engineering.Application.Configs;
using Engineering.Application.Extensions.StringExtensions;
using Engineering.Application.RequestGoodsSupplyDetailManagements;
using Engineering.Application.RequestGoodsSupplyManagements.Queries.GetsSupplyManagementByConsumableVolumes;
using Engineering.Application.Services.Branchs.Queries.GetAllManagerGoods;
using Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetsConsumableVolumeProductsForSupply;
using Engineering.Application.Services.CostCenters.Queries.GetsCostCenterWarehouseByProjectOperation;
using Engineering.Application.Services.CostCenterWarehouses.Queries.GetsByCostCenterId;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Application.Services.OperationLocations.Queries.GetsLocationByProjectOperationDetailIds;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByContractor;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdLessInclude;
using Engineering.Application.Services.Projects;
using Engineering.Application.Services.Projects.Models.UpdateProjectProductQuantities;
using Engineering.Application.Services.Projects.Queries.GetProjectById;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.DeleteRequestGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Contracts.CreatePRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFilteredRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyById;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetProjectNameRequestGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.CreateRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.CreateRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.DeleteRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.DeleteRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.RequestGoodsSupplyProductStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.SetRequestGoodsSupplyProductStatusClose;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.UpdateRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.UpdateRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.DeleteRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.DeleteRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetAllGoodsSupplyProductDocument;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductDocuments;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductHistoryById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectDetailData;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectOperationDetailsByRequestId;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsDetailHistoryById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyDetailImportance;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyDetailStatus;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyProductById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyDetailBySupplyProductId;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyDetailStatus;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsProjectOperationDetailData;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProductExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProductExcelExporter;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsTotalPriceRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetTotalSupplyByProjectOperationDetailIds;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplyProductGroupStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplyProductStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateProjectRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetAllGoodsSupplyProductDocument;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetGoodsSupplyProductById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetGoodsSupplyProductDocuments;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetGoodsSupplyProductHistoryById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetProjectOperationDetailsByRequestId;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsDetailHistoryById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyDetailById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyProductById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyProductByIdModeled;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailByProductId;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailBySupplyProductId;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsProjectOperationDetailData;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyProductByIds;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsTotalPriceRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetTotalSupplyByProductIds;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetTotalSupplyByProjectOperationDetailIds;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedGoodsSupplyProduct;
using Engineering.Application.Services.TelegramChats.Queries.GetByRequestGoodsSupplyId;
using Engineering.Application.Services.TelegramChats.TelegramServices;
using Engineering.Application.Services.TelegramChats.TelegramServices.Models;
using Engineering.Application.Services.TelegramMessageHistorys;
using Engineering.Application.WebServices.IdentityServices.Users.Queries.GetUsersByActionId;
using Engineering.Application.WebServices.MessageSender.MessageSenders.Models;
using Engineering.Application.WebServices.MessageSender.MessageSenders.Models.CreateMessage;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByCategoryIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredWarehousesByGroupIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredWarehousesByGroupIds;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.GetProductByGroupIds;
using Engineering.Application.WebServices.WarehouseServices.Warehouse.Models.GetWarehouseByIds;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models;
using Engineering.Domain.Entities.Messengers.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.Synonyms.MetaData.Organizations;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Queries.GetUserById;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.InventoryPackages.Models.GetsPackagesByIds;
using MessageSender.ClientSdk.Messaging;
using MessageSender.ClientSdk.Messaging.Targets;
using MessageSender.ClientSdk.Services;
using Microsoft.Extensions.Options;
using Pipelines.Sockets.Unofficial.Arenas;
using Warehouse.ClientSdks.Services;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails;

public partial class RequestGoodsSupplyDetailLogic : IRequestGoodsSupplyDetailLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<RequestGoodsSupplyDetailLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IUserProfileService _userProfileService;
    private readonly IRequestGoodsSupplyManagementLogic _requestGoodsSupplyManagementLogic;
    private readonly ITelegramMessageHistoryLogic _telegramMessageHistoryLogic;
    private readonly long _currentUserId;
    private readonly string? _authorization;
    private readonly MessageSenderConfig _messageSenderConfig;
    private readonly IProjectProductRepository _projectProductRepositoy;
    private readonly IProjectRepository _projectRepository;
    private readonly IRequestGoodsSupplyDetailRepository _requestGoodsSupplyDetailRepository;
    private readonly IProjectOperationDetailRepository _projectOperationDetailRepository;
    private readonly IConsumableVolumeProductRepository _consumableVolumeProductRepository;
    private readonly IRequestGoodsSupplyManagementRepository _requestGoodsSupplyManagementRepository;
    private readonly IMessengerChannelRepository _messengerChannelRepo;
    private readonly IProjectLogic _projectLogic;
    private readonly IViewGroupRepository _groupRepo;
    private readonly IViewCategoryRepository _categoryRepo;
    private readonly IViewProductRepository _productRepo;
    private readonly IProjectThirdPartyRepository _projectThirdPartyRepo;
    private readonly IViewWarehouseAssetRepository _warehouseAssetRepo;
    private readonly IRequestGoodsSupplyProductRepository _rgsProductRepo;
    private readonly IViewOrganizationRepository _viewOrganizationRepository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly IMessageRelay _relay;
    private readonly IGoodsManagerAssignmentRepository _goodsManagerAssignmentRepo;
    private readonly IViewOrganizationRepository _organizationRepo;
    private readonly IViewPackageRepository _packageRepo;

    public RequestGoodsSupplyDetailLogic(
        IMediator mediator,
        ILogger<RequestGoodsSupplyDetailLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        IRequestGoodsSupplyManagementLogic requestGoodsSupplyManagementLogic,
        IProjectThirdPartyRepository projectThirdPartyRepo,
        IUserProfileService userProfileService,
        IHttpContextAccessor httpContextAccessor,
        IOptionsSnapshot<MessageSenderConfig> options,
        IProjectProductRepository projectProductRepository,
        IProjectRepository projectRepository,
        IRequestGoodsSupplyDetailRepository requestGoodsSupplyDetailRepository,
        IMessengerChannelRepository messengerChannelRepo,
        IProjectOperationDetailRepository projectOperationDetailRepository,
        IConsumableVolumeProductRepository consumableVolumeProductRepository,
        IRequestGoodsSupplyManagementRepository requestGoodsSupplyManagementRepository,
        IProjectLogic projectLogic,
        IViewGroupRepository groupRepo,
        IViewCategoryRepository categoryRepo,
        IViewProductRepository productRepo,
        IViewWarehouseAssetRepository warehouseAssetRepo,
        IProductService productService,
        IRequestGoodsSupplyProductRepository rgsProduct,
        IViewThirdPartyRepository thirdPartyRepo,
        IViewOrganizationRepository viewOrganizationRepository,
        IMessageRelay relay,
        IGoodsManagerAssignmentRepository goodsManagerAssignmentRepo,
        IViewOrganizationRepository organizationRepo,
        IViewPackageRepository packageRepo
        )
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _telegramMessageHistoryLogic = telegramMessageHistoryLogic;
        _requestGoodsSupplyManagementLogic = requestGoodsSupplyManagementLogic;
        _messengerChannelRepo = messengerChannelRepo;
        _userProfileService = userProfileService;
        _authorization = httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();
        _messageSenderConfig = options.Value;
        _currentUserId = _userProfileService.GetProfileInfo().UserId;
        _projectProductRepositoy = projectProductRepository;
        _projectThirdPartyRepo = projectThirdPartyRepo;
        _projectRepository = projectRepository;
        _requestGoodsSupplyDetailRepository = requestGoodsSupplyDetailRepository;
        _projectOperationDetailRepository = projectOperationDetailRepository;
        _consumableVolumeProductRepository = consumableVolumeProductRepository;
        _requestGoodsSupplyManagementRepository = requestGoodsSupplyManagementRepository;
        _projectLogic = projectLogic;
        _groupRepo = groupRepo;
        _categoryRepo = categoryRepo;
        _productRepo = productRepo;
        _warehouseAssetRepo = warehouseAssetRepo;
        _viewOrganizationRepository = viewOrganizationRepository;
        _rgsProductRepo = rgsProduct;
        _thirdPartyRepo = thirdPartyRepo;
        _relay = relay;
        _goodsManagerAssignmentRepo = goodsManagerAssignmentRepo;
        _organizationRepo = organizationRepo;
        _packageRepo = packageRepo;
    }

    public async Task<Result<CreateRequestGoodsSupplyDetailResponse?>> CreateRequestGoodsDetailSupply(
        CreateRequestGoodsSupplyDetailModelRequest request, CT ct)
    {
        using (var transaction = new CreateTransaction())
        {
            var isValidRequest = await request.IsValidAsync<CreateRequestGoodsSupplyDetailValidator, CreateRequestGoodsSupplyDetailModelRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(isValidRequest.Error!);

            RequestGoodsSupply? value = null;
            if (request.RequestGoodsSupply is not null)
                value = request.RequestGoodsSupply;
            else if (request.RequestGoodsSupplyId is not null && request.RequestGoodsSupplyId > 0)
            {
                var requestGoodsSupplyQuery = await _mediator.Send(new GetRequestGoodsSupplyByIdQuery(request.RequestGoodsSupplyId!.Value), ct);
                if (requestGoodsSupplyQuery.IsFailure)
                    return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(requestGoodsSupplyQuery.Error!);
                value = requestGoodsSupplyQuery.Value!;
            }
            else
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyIds);

            var validationResult = await ValidateRequestGoodsSupplyDetails(request.Details, ct);
            if (validationResult.IsFailure)
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(validationResult.Error!);

            List<RequestGoodsSupplyProduct>? products = new List<RequestGoodsSupplyProduct>();
            var response = new CreateRequestGoodsSupplyDetailResponse();
            foreach (var item in request.Details)
            {
                RequestGoodsSupplyProduct? product = null;
                if (!products.Any(x => x.ProductId.Equals(item.ProductId)))
                {
                    var packagesQuery = await _packageRepo.GetByGroupId(item!.ProductGroupId, ct);
                    if (packagesQuery is null)
                        return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.ProductNoHavePackage);
                    if (item.PackageId is not null)
                    {
                        var pckageValidate = packagesQuery!.Where(x => x.Id == item.PackageId)?.FirstOrDefault();
                        if (pckageValidate is null)
                            return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.PackageIdNotValidate);
                    }
                    else
                    {
                        var isDefaultPckage = packagesQuery!.Where(x => x.IsDefault)?.FirstOrDefault();
                        item.PackageId = isDefaultPckage?.Id;
                        if (isDefaultPckage is null)
                            item.PackageId = packagesQuery!.FirstOrDefault()?.Id;
                        if (item.PackageId is null)
                            return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.PackageIdNotFound);
                    }

                    decimal? packageUnitPrice = null;
                    if (item.UnitPrice != null && item.PackageId is not null && item.PackageUnitPrice is null)
                        packageUnitPrice = item.UnitPrice;

                    var productResponse = await _mediator.Send(new CreateRequestGoodsSupplyProductCommand(
                        value,
                        item.Importance,
                        item.DelivaryDeadLine,
                        item.ProductId,
                        item.ProductGroupId,
                        item.PackageId,
                        item.RequestedCount,
                        item.UnitPrice,
                        item.TaxPercentage,
                        item.TaxNumber,
                        item.DiscountByPercentage,
                        item.DiscountByNumber,
                        item.PackingPrice,
                        item.PackageCount,
                        packageUnitPrice,
                        item.CheckGroup,
                        item.ContractorId,
                        item.DestinationWarehouseId,
                        item.CustomerInvoiceNumber,
                        item.Description,
                        item.ManagementDescription), ct);
                    if (productResponse.IsFailure)
                        return Result.Failure<CreateRequestGoodsSupplyDetailResponse?>(productResponse.Error!);
                    product = productResponse.Value!;
                    products.Add(product);
                }
                else
                    product = products.FirstOrDefault(x => x.ProductId.Equals(item.ProductId))!;

                var createDetail = await ProcessCreateRequestDetail(item, value, product, ct);
                if (createDetail.IsFailure)
                    return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(createDetail.Error!);

                response.RequestGoodsSupplyDetailIds.Add(createDetail!.Value!.Id);
            }

            await _unitOfWork.CommitAsync(ct);

            transaction.Complete();
            return response;
        }
    }

    public async Task<Result<CreateRequestGoodsSupplyDetailResponse?>> CreateProjectRequestGoodsDetailSupply(
        CreateProjectRequestGoodsSupplyDetailModelRequest request, long projectId, CT ct)
    {
        using (var transaction = new CreateTransaction())
        {

            RequestGoodsSupply? value = null;
            if (request.RequestGoodsSupply is not null)
                value = request.RequestGoodsSupply;
            else if (request.RequestGoodsSupplyId is not null && request.RequestGoodsSupplyId > 0)
            {
                var requestGoodsSupplyQuery = await _mediator.Send(new GetRequestGoodsSupplyByIdQuery(request.RequestGoodsSupplyId!.Value), ct);
                if (requestGoodsSupplyQuery.IsBad())
                    return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(requestGoodsSupplyQuery.Error!);
                value = requestGoodsSupplyQuery.Value!;
            }
            else
                return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyIds);

            List<RequestGoodsSupplyProduct>? products = new List<RequestGoodsSupplyProduct>();
            var response = new CreateRequestGoodsSupplyDetailResponse();
            foreach (var item in request.Details)
            {
                RequestGoodsSupplyProduct? product = null;
                if (!products.Any(x => x.ProductId.Equals(item.ProductId)))
                {
                    var packagesQuery = await _packageRepo.GetByGroupId(item!.ProductGroupId, ct);
                    if (packagesQuery is null)
                        return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.ProductNoHavePackage);
                    if (item.PackageId is not null)
                    {
                        var pckageValidate = packagesQuery!.Where(x => x.Id == item.PackageId)?.FirstOrDefault();
                        if (pckageValidate is null)
                            return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.PackageIdNotValidate);
                    }
                    else
                    {
                        var isDefaultPckage = packagesQuery!.Where(x => x.IsDefault)?.FirstOrDefault();
                        item.PackageId = isDefaultPckage?.Id;
                        if (isDefaultPckage is null)
                            item.PackageId = packagesQuery!.FirstOrDefault()?.Id;
                        if (item.PackageId is null)
                            return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.PackageIdNotFound);
                    }

                    decimal? packageUnitPrice = null;
                    if (item.UnitPrice != null && item.PackageId is not null && item.PackageUnitPrice is null)
                        packageUnitPrice = item.UnitPrice;

                    var productResponse = await _mediator.Send(new CreateRequestGoodsSupplyProductCommand(
                        value,
                        item.Importance,
                        item.DelivaryDeadLine,
                        item.ProductId,
                        item.ProductGroupId,
                        item.PackageId,
                        item.RequestedCount,
                        item.UnitPrice,
                        item.TaxPercentage,
                        item.TaxNumber,
                        item.DiscountByPercentage,
                        item.DiscountByNumber,
                        item.PackingPrice,
                        item.PackageCount,
                        packageUnitPrice,
                        item.CheckGroup,
                        item.ContractorId,
                        item.DestinationWarehouseId,
                        item.CustomerInvoiceNumber,
                        item.Description,
                        item.ManagementDescription), ct);
                    if (productResponse.IsFailure)
                        return Result.Failure<CreateRequestGoodsSupplyDetailResponse?>(productResponse.Error!);
                    product = productResponse.Value!;
                    products.Add(product);
                }
                else
                    product = products.FirstOrDefault(x => x.ProductId.Equals(item.ProductId))!;

                var createDetail = await ProcessCreateProjectRequestDetail(item, value, product, projectId, ct);
                if (createDetail.IsFailure)
                    return Result.Failure<CreateRequestGoodsSupplyDetailResponse>(createDetail.Error!);

                response.RequestGoodsSupplyDetailIds.Add(createDetail!.Value!.Id);
            }

            await _unitOfWork.CommitAsync(ct);

            transaction.Complete();
            return response;
        }
    }

    public async Task<Result<CreateRequestGoodsSupplyDetailResponseModel?>> ProcessCreateRequestDetail(
        CreateRequestGoodsSupplyDetailModel request, RequestGoodsSupply value, RequestGoodsSupplyProduct? product, CT ct)
    {
        if (value.Type == GoodsSupplyType.Project && (request.ContractorId is null || request.ContractorId <= 0))
            return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(RequestGoodsSupplyErrors.ContractorIsImportant);

        var getProjectOperationDetail = await _mediator.Send(new GetProjectOperationDetailByIdLessIncludeQuery(request.ProjectOperationDetailId), ct);
        if (getProjectOperationDetail.IsFailure)
            return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(getProjectOperationDetail.Error!);

        if (request.ContractorId is not null && request.ContractorId > 0 && value.Type == GoodsSupplyType.Contractor)
        {
            var validateContractor = await _mediator.Send(new GetProjectOperationDetailByContractorQuery(value.ProjectOperation.Project.Id, (long)request.ContractorId!), ct);
            if (validateContractor.IsFailure)
                return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(RequestGoodsSupplyErrors.InValidContractor);
        }

        var detailProductQuery = await _projectOperationDetailRepository.GetProjectOperationDetailWithProductVolumes(request.ProjectOperationDetailId, ct)!;
        if (detailProductQuery is null)
            return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(ProjectErrors.ProjectProductWithIdsNotFound)!;

        var detailProduct = detailProductQuery.ConsumableVolumeProducts.FirstOrDefault(x => x.ProductGroupId == request.ProductGroupId);
        if (detailProduct == null)
        {
            var categories = detailProductQuery.ConsumableVolumeProducts
                .Where(x => x.VolumeProductType == VolumeProductType.Category)
                .Listed(x => x.ProductGroupId);
            var groupCategories = await WebServicesLogic.GetFilteredGroupsByCategoryIds(categories, [product.ProductGroupId], null, _mediator, ct);
            var category = groupCategories.FirstOrDefault(x => x.Id == request.ProductGroupId);
            detailProduct = detailProductQuery.ConsumableVolumeProducts.FirstOrDefault(x => x.ProductGroupId == category.CategoryId);
        }

        // حساب کتاب مقدار باقی مانده
        var totalRequest = RemaindedCount(detailProduct);
        var tolerancePercentage = detailProduct.UnusedPercentage;
        var estimatedCount = ((detailProduct.FinalValue / 100) * tolerancePercentage) + detailProduct.FinalValue;
        if ((totalRequest + request.RequestedCount) > estimatedCount)
            return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(RequestGoodsSupplyDetailErrors.RequestedCount);

        var prices = CalculatePrices(request.PackageId, request.PackageCount, request.PackageUnitPrice, request.UnitPrice,
            request.RequestedCount, request.DiscountByNumber, request.TaxNumber, request.PackingPrice, value.Type);

        var requestCommand = await _mediator.Send(new CreateRequestGoodsSupplyDetailCommand(
            value,
            detailProduct,
            null,
            product,
            request,
            prices.TotalPrice,
            prices.DiscountedPrice,
            prices.FinalPrice), ct);
        if (requestCommand.IsFailure)
            return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(requestCommand.Error!);
        var detail = requestCommand.Value;

        var updateProductResponse = await _mediator.Send(new UpdateRequestGoodsSupplyProductCommand(product!, detail?.CustomerInvoiceNumber), ct);
        if (updateProductResponse.IsFailure)
            return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(updateProductResponse.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateRequestGoodsSupplyDetailResponseModel(requestCommand!.Value!.Id);
    }

    public async Task<Result<CreateRequestGoodsSupplyDetailResponseModel?>> ProcessCreateProjectRequestDetail(
        CreateProjectRequestGoodsSupplyDetailModel request, RequestGoodsSupply value, RequestGoodsSupplyProduct? product, long projectId, CT ct)
    {
        if (value.Type == GoodsSupplyType.Project && (request.ContractorId is null || request.ContractorId <= 0))
            return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(RequestGoodsSupplyErrors.ContractorIsImportant);

        var getProject = await _projectRepository.GetProjectByIdIncludeLess(projectId, ct);
        if (getProject is null)
            return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(ProjectErrors.ProjectWithIdsNotFound);

        if (request.ContractorId is not null && request.ContractorId > 0 && value.Type == GoodsSupplyType.Contractor)
        {
            var validateContractor = await _mediator.Send(new GetProjectOperationDetailByContractorQuery(value.ProjectOperation.Project.Id, (long)request.ContractorId!), ct);
            if (validateContractor.IsFailure)
                return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(RequestGoodsSupplyErrors.InValidContractor);
        }

        var projectProductQuery = await _projectProductRepositoy.GetProductByProjectId(projectId, ct)!;
        if (projectProductQuery is null)
            return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(ProjectErrors.ProjectProductWithIdsNotFound)!;

        var projectProduct = projectProductQuery.FirstOrDefault(x => x.ProductGroupId == request.ProductGroupId);
        if (projectProduct == null)
        {
            var categories = projectProductQuery
                .Where(x => x.ProductCategoryId.HasValue)
                .Select(x => x.ProductCategoryId!.Value)
                .ToList();
            var productGroup = await _groupRepo.GetByIds([request.ProductGroupId], ct);
            projectProduct = projectProductQuery.FirstOrDefault(x => x.ProductCategoryId == productGroup.FirstOrDefault().CategoryId);
        }

        // حساب کتاب مقدار باقی مانده
        var totalRequest = RemaindedCount(projectProduct);
        var tolerancePercentage = projectProduct.TolerancePercentage;
        var estimatedCount = ((projectProduct.RequestQuantity / 100) * tolerancePercentage) + projectProduct.RequestQuantity;
        if ((totalRequest + request.RequestedCount) > estimatedCount)
            return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(RequestGoodsSupplyDetailErrors.ProductCountLow);

        var prices = CalculatePrices(request.PackageId, request.PackageCount, request.PackageUnitPrice, request.UnitPrice,
            request.RequestedCount, request.DiscountByNumber, request.TaxNumber, request.PackingPrice, value.Type);
        var command = new CreateProjectRequestGoodsSupplyDetailCommand(
            value,
            null,
            projectProduct,
            product,
            request,
            prices.TotalPrice,
            prices.DiscountedPrice,
            prices.FinalPrice);

        var inProgressQuantity = projectProduct.RequestGoodsSupplyDetails.Where(x => GSDSRules.InProgress.Contains(x.Status)).Sum(x => x.RequestedCount);
        var completeQuantity = projectProduct.RequestGoodsSupplyDetails.Where(x => GSDSRules.Completed.Contains(x.Status)).Sum(x => x.RequestedCount);
        var changeQuantity = new UpdateProjectProductQuantitiesRequest(projectProduct.Id, completeQuantity, inProgressQuantity);
        var projectProductQuantites = await _projectLogic.UpdateProjectProductQuantity(changeQuantity, ct);
        var requestCommand = await CreateProjectRequestGoodsSupplyDetailCommand(command, ct);
        if (requestCommand.IsFailure)
            return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(requestCommand.Error!);
        var detail = requestCommand.Value;

        var updateProductResponse = await _mediator.Send(new UpdateRequestGoodsSupplyProductCommand(product!, detail?.CustomerInvoiceNumber), ct);
        if (updateProductResponse.IsFailure)
            return Result.Failure<CreateRequestGoodsSupplyDetailResponseModel?>(updateProductResponse.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateRequestGoodsSupplyDetailResponseModel(requestCommand!.Value!.Id);
    }

    public async Task<Result<UpdateRequestGoodsSupplyProductResponse?>> UpdateRequestGoodsSupplyProduct(UpdateRequestGoodsSupplyProductRequest request, CT ct)
    {
        using (var transaction = new CreateTransaction())
        {
            var isValidRequest = await request.IsValidAsync<UpdateRequestGoodsSupplyProductValidator, UpdateRequestGoodsSupplyProductRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<UpdateRequestGoodsSupplyProductResponse>(isValidRequest.Error!);

            var requestGoodsSupplyQuery = await _mediator.Send(new GetRequestGoodsSupplyProductByIdQuery(request.Id), ct);
            if (requestGoodsSupplyQuery.IsFailure)
                return Result.Failure<UpdateRequestGoodsSupplyProductResponse>(requestGoodsSupplyQuery.Error!);
            var value = requestGoodsSupplyQuery.Value!;

            if (request.Updates is not null)
            {
                if (request.Updates.Any(x => x.ProductId != value.ProductId))
                    return Result.Failure<UpdateRequestGoodsSupplyProductResponse>(RequestGoodsSupplyErrors.UpdateListHaveAnotherProduct);

                foreach (var update in request.Updates)
                {
                    var responseDetails = await UpdateRequestGoodsDetailSupply(update, ct);
                    if (responseDetails.IsFailure)
                        return Result.Failure<UpdateRequestGoodsSupplyProductResponse>(responseDetails.Error!);
                }
            }

            if (request.Creates is not null)
            {
                if (request.Creates.Any(x => x.ProductId != value.ProductId))
                    return Result.Failure<UpdateRequestGoodsSupplyProductResponse>(RequestGoodsSupplyErrors.CreateListHaveAnotherProduct);

                foreach (var create in request.Creates)
                {
                    var responseDetails = await ProcessCreateRequestDetail(create, value.RequestGoodsSupply, value, ct);
                    if (responseDetails.IsFailure)
                        return Result.Failure<UpdateRequestGoodsSupplyProductResponse>(responseDetails.Error!);
                }
            }

            await _unitOfWork.CommitAsync(ct);

            transaction.Complete();
            return new UpdateRequestGoodsSupplyProductResponse(true);
        }
    }

    public async Task<Result<UpdateRequestGoodsSupplyDetailResponse?>> UpdateRequestGoodsDetailSupply(UpdateRequestGoodsSupplyDetailRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateRequestGoodsSupplyDetailValidator, UpdateRequestGoodsSupplyDetailRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateRequestGoodsSupplyDetailResponse>(isValidRequest.Error!);

        var requestGoodsSupplyQuery = await _mediator.Send(new GetRequestGoodsSupplyDetailByIdQuery(request.RequestGoodsSupplyDetailId), ct);
        if (requestGoodsSupplyQuery.IsFailure)
            return Result.Failure<UpdateRequestGoodsSupplyDetailResponse>(requestGoodsSupplyQuery.Error!);
        var value = requestGoodsSupplyQuery.Value!;

        if (value.RequestGoodsSupply.Type == GoodsSupplyType.Project && (request.ContractorId is null || request.ContractorId <= 0))
            return Result.Failure<UpdateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.ContractorIsImportant);

        if (!GSDSRules.AllowStatusForUpdate.Any(x => x.Equals(value.Status)))
            return Result.Failure<UpdateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.InValidRequestGoodsSupplyDetailStatus);

        var getProjectOperationDetail = await _mediator.Send(new GetProjectOperationDetailByIdLessIncludeQuery(request.ProjectOperationDetailId), ct);
        if (getProjectOperationDetail.IsFailure)
            return Result.Failure<UpdateRequestGoodsSupplyDetailResponse>(getProjectOperationDetail.Error!);

        if (!value.RequestGoodsSupply.ProjectOperation.ProjectOperationDetails.Any(x => x.Id == getProjectOperationDetail.Value!.Id))
            return Result.Failure<UpdateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.ProjectOperationDetailNotInProjectOperation);
        var detailProductQuery = await _projectOperationDetailRepository.GetProjectOperationDetailWithProductVolumes(request.ProjectOperationDetailId, ct)!;
        if (detailProductQuery is null)
            return Result.Failure<UpdateRequestGoodsSupplyDetailResponse?>(ProjectErrors.ProjectProductWithIdsNotFound)!;

        var detailProduct = detailProductQuery.ConsumableVolumeProducts.FirstOrDefault(x => x.ProductGroupId == request.ProductGroupId);
        if (detailProduct == null)
        {
            var categories = detailProductQuery.ConsumableVolumeProducts
                .Where(x => x.VolumeProductType == VolumeProductType.Category)
                .Listed(x => x.ProductGroupId);
            var groupCategories = await WebServicesLogic.GetFilteredGroupsByCategoryIds(categories, [request.ProductGroupId], null, _mediator, ct);
            var category = groupCategories.FirstOrDefault(x => x.Id == request.ProductGroupId);
            detailProduct = detailProductQuery.ConsumableVolumeProducts.FirstOrDefault(x => x.ProductGroupId == category.CategoryId);
        }

        var entityDetail = value.RequestGoodsSupply.RequestGoodsSupplyDetails.Where(x => x.Id == request.RequestGoodsSupplyDetailId).FirstOrDefault();
        if (entityDetail is null)
            return Result.Failure<UpdateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.RequestGoodsSupplyDetailWithIdNotFound);

        var packagesQuery = await _packageRepo.GetByGroupId(request!.ProductGroupId, ct);
        if (packagesQuery is null)
            return Result.Failure<UpdateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.ProductNoHavePackage);
        if (request.PackageId is not null)
        {
            var pckageValidate = packagesQuery!.Where(x => x.Id == request.PackageId)?.FirstOrDefault();
            if (pckageValidate is null)
                return Result.Failure<UpdateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.PackageIdNotValidate);
        }
        else
        {
            var isDefaultPckage = packagesQuery!.Where(x => x.IsDefault)?.FirstOrDefault();
            request.PackageId = isDefaultPckage?.Id;
            if (isDefaultPckage is null)
                request.PackageId = packagesQuery!.FirstOrDefault()?.Id;
            if (request.PackageId is null)
                return Result.Failure<UpdateRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.PackageIdNotFound);
        }

        if (request.PackageCount is null)
            request.PackageCount = request.RequestedCount;

        if (request.PackageUnitPrice is null)
            request.PackageUnitPrice = request.UnitPrice;

        List<RequestGoodsSupplyProduct>? products = value.RequestGoodsSupply.RequestGoodsSupplyProducts.ToList();
        RequestGoodsSupplyProduct? product = null;
        if (!products.Any(x => x.ProductId.Equals(request.ProductId)))
        {
            var productResponse = await _mediator.Send(new CreateRequestGoodsSupplyProductCommand(
                value.RequestGoodsSupply,
                request.Importance,
                request.DelivaryDeadLine,
                request.ProductId,
                request.ProductGroupId,
                request.PackageId,
                request.RequestedCount,
                request.UnitPrice,
                request.TaxPercentage,
                request.TaxNumber,
                request.DiscountByPercentage,
                request.DiscountByNumber,
                request.PackingPrice,
                request.PackageCount,
                request.PackageUnitPrice,
                request.CheckGroup,
                request.ContractorId,
                request.DestinationWarehouseId,
                request.CustomerInvoiceNumber,
                request.Description,
                request.ManagementDescription), ct);
            if (productResponse.IsFailure)
                return Result.Failure<UpdateRequestGoodsSupplyDetailResponse?>(productResponse.Error!);
            product = productResponse.Value!;
            products.Add(product);
        }
        else
            product = value.RequestGoodsSupplyProduct;

        var prices = CalculatePrices(request.PackageId, request.PackageCount, request.PackageUnitPrice, request.UnitPrice,
            request.RequestedCount, request.DiscountByNumber, request.TaxNumber, request.PackingPrice, value.RequestGoodsSupply.Type);
        decimal? discountedPrice = prices.DiscountedPrice;
        decimal? finalPrice = prices.FinalPrice;
        decimal? totalPrice = prices.TotalPrice;

        var updateCommand = await _mediator.Send(new UpdateRequestGoodsSupplyDetailCommand(
            value,
            detailProduct,
            product,
            request,
            totalPrice,
            discountedPrice,
            finalPrice), ct);
        if (updateCommand.IsFailure)
            return Result.Failure<UpdateRequestGoodsSupplyDetailResponse>(updateCommand.Error!);
        var detail = updateCommand.Value;

        var updateProductResponse = await _mediator.Send(new UpdateRequestGoodsSupplyProductCommand(product!, detail?.CustomerInvoiceNumber), ct);
        if (updateProductResponse.IsFailure)
            return Result.Failure<UpdateRequestGoodsSupplyDetailResponse?>(updateProductResponse.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateRequestGoodsSupplyDetailResponse(true);
    }

    public async Task<Result<UpdateProjectRequestGoodsSupplyDetailResponse?>> UpdateProjectRequestGoodsDetailSupply(UpdateProjectRequestGoodsSupplyDetailRequest request, Project project, CT ct)
    {
        var requestGoodsSupplyQuery = await _mediator.Send(new GetRequestGoodsSupplyDetailByIdQuery(request.RequestGoodsSupplyDetailId), ct);
        if (requestGoodsSupplyQuery.IsFailure)
            return Result.Failure<UpdateProjectRequestGoodsSupplyDetailResponse>(requestGoodsSupplyQuery.Error!);
        var value = requestGoodsSupplyQuery.Value!;

        if (value.RequestGoodsSupply.Type == GoodsSupplyType.Project && (request.ContractorId is null || request.ContractorId <= 0))
            return Result.Failure<UpdateProjectRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyErrors.ContractorIsImportant);

        if (!GSDSRules.AllowStatusForUpdate.Any(x => x.Equals(value.Status)))
            return Result.Failure<UpdateProjectRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.InValidRequestGoodsSupplyDetailStatus);

        var detailProduct = project.ProjectProducts.FirstOrDefault(x => x.ProductGroupId == request.ProductGroupId || x.ProductCategoryId == request.ProductGroupId);
        var entityDetail = value.RequestGoodsSupply.RequestGoodsSupplyDetails.Where(x => x.Id == request.RequestGoodsSupplyDetailId).FirstOrDefault();
        if (entityDetail is null)
            return Result.Failure<UpdateProjectRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.RequestGoodsSupplyDetailWithIdNotFound);

        var packagesQuery = await _packageRepo.GetByGroupId(value!.RequestGoodsSupplyProduct.ProductGroupId, ct);
        if (packagesQuery is null)
            return Result.Failure<UpdateProjectRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.ProductNoHavePackage);
        if (request.PackageId is not null)
        {
            var pckageValidate = packagesQuery!.Where(x => x.Id == request.PackageId)?.FirstOrDefault();
            if (pckageValidate is null)
                return Result.Failure<UpdateProjectRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.PackageIdNotValidate);
        }
        else
        {
            var isDefaultPckage = packagesQuery!.Where(x => x.IsDefault)?.FirstOrDefault();
            request.PackageId = isDefaultPckage?.Id;
            if (isDefaultPckage is null)
                request.PackageId = packagesQuery!.FirstOrDefault()?.Id;
            if (request.PackageId is null)
                return Result.Failure<UpdateProjectRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.PackageIdNotFound);
        }

        if (request.PackageCount is null)
            request.PackageCount = request.RequestedCount;

        if (request.PackageUnitPrice is null)
            request.PackageUnitPrice = request.UnitPrice;

        List<RequestGoodsSupplyProduct>? products = value.RequestGoodsSupply.RequestGoodsSupplyProducts.ToList();
        RequestGoodsSupplyProduct? product = null;
        if (!products.Any(x => x.ProductId.Equals(request.ProductId)))
        {
            var productResponse = await _mediator.Send(new CreateRequestGoodsSupplyProductCommand(
                value.RequestGoodsSupply,
                request.Importance,
                request.DelivaryDeadLine,
                request.ProductId,
                request.ProductGroupId,
                request.PackageId,
                request.RequestedCount,
                request.UnitPrice,
                request.TaxPercentage,
                request.TaxNumber,
                request.DiscountByPercentage,
                request.DiscountByNumber,
                request.PackingPrice,
                request.PackageCount,
                request.PackageUnitPrice,
                request.CheckGroup,
                request.ContractorId,
                request.DestinationWarehouseId,
                request.CustomerInvoiceNumber,
                request.Description,
                request.ManagementDescription), ct);
            if (productResponse.IsFailure)
                return Result.Failure<UpdateProjectRequestGoodsSupplyDetailResponse?>(productResponse.Error!);
            product = productResponse.Value!;
            products.Add(product);
        }
        else
            product = value.RequestGoodsSupplyProduct;

        var prices = CalculatePrices(request.PackageId, request.PackageCount, request.PackageUnitPrice, request.UnitPrice,
            request.RequestedCount, request.DiscountByNumber, request.TaxNumber, request.PackingPrice, value.RequestGoodsSupply.Type);
        decimal? discountedPrice = prices.DiscountedPrice;
        decimal? finalPrice = prices.FinalPrice;
        decimal? totalPrice = prices.TotalPrice;

        var updateCommand = await UpdateProjectRequestGoodsSupplyDetailCommand(
            value,
            detailProduct,
            product,
            request,
            totalPrice,
            discountedPrice,
            finalPrice, ct);
        if (updateCommand.IsFailure)
            return Result.Failure<UpdateProjectRequestGoodsSupplyDetailResponse>(updateCommand.Error!);
        var detail = updateCommand.Value;

        var updateProductResponse = await _mediator.Send(new UpdateRequestGoodsSupplyProductCommand(product!, detail?.CustomerInvoiceNumber), ct);
        if (updateProductResponse.IsFailure)
            return Result.Failure<UpdateProjectRequestGoodsSupplyDetailResponse?>(updateProductResponse.Error!);

        return new UpdateProjectRequestGoodsSupplyDetailResponse(true);
    }

    public async Task<Result<DeleteRequestGoodsSupplyDetailResponse?>> DeleteRequestGoodsSupplyDetail(DeleteRequestGoodsSupplyDetailModelRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<DeleteRequestGoodsSupplyDetailValidator, DeleteRequestGoodsSupplyDetailModelRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteRequestGoodsSupplyDetailResponse>(isValidRequest.Error!);

        RequestGoodsSupplyDetail? value = null;
        if (request.RequestGoodsSupplyDetail is not null)
            value = request.RequestGoodsSupplyDetail;
        else if (request.RequestGoodsSupplyDetailId is not null && request.RequestGoodsSupplyDetailId > 0)
        {
            var requestGoodsSupplyQuery = await _mediator.Send(new GetRequestGoodsSupplyDetailByIdQuery(request.RequestGoodsSupplyDetailId!.Value), ct);
            if (requestGoodsSupplyQuery.IsFailure)
                return Result.Failure<DeleteRequestGoodsSupplyDetailResponse>(requestGoodsSupplyQuery.Error!);
            value = requestGoodsSupplyQuery.Value!;
        }
        else
            return Result.Failure<DeleteRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.InValidRequestGoodsSupplyDetailId);

        if (!GSDSRules.AllowStatusForDelete.Any(x => x.Equals(value.Status)))
            return Result.Failure<DeleteRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.InValidRequestGoodsSupplyDetailStatusForDelete);

        if (request.AnotherDataCount is not null && request.AnotherDataCount == value.RequestGoodsSupply.RequestGoodsSupplyDetails.Count)
            if (value.RequestGoodsSupply.RequestGoodsSupplyDetails.Count(x => x.Id != value.Id) <= 0)
                return Result.Failure<DeleteRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.CanNotDelete);

        var updateCommand = await _mediator.Send(new DeleteRequestGoodsSupplyDetailCommand(value), ct);
        if (updateCommand.IsFailure)
            return Result.Failure<DeleteRequestGoodsSupplyDetailResponse>(updateCommand.Error!);

        if (request.CheckAnotherData)
            if (updateCommand.Value!.RequestGoodsSupply.RequestGoodsSupplyDetails.All(x => x.IsDeleted == true))
                return Result.Failure<DeleteRequestGoodsSupplyDetailResponse>(RequestGoodsSupplyDetailErrors.CanNotDelete);

        if (request.HaveAnyForAdd is not null)
            if (!request.HaveAnyForAdd.Value)
            {
                var updateCommandProduct = await _mediator.Send(new SetRequestGoodsSupplyProductStatusCloseCommand(value.RequestGoodsSupplyProduct!), ct);
                if (updateCommandProduct.IsFailure)
                    return Result.Failure<DeleteRequestGoodsSupplyDetailResponse>(updateCommand.Error!);
            }

        await _unitOfWork.CommitAsync(ct);
        return new DeleteRequestGoodsSupplyDetailResponse(true);
    }

    public async Task<Result<RequestGoodsSupplyProductStatusChangerResponse?>> RequestGoodsSupplyProductStatusChanger(RequestGoodsSupplyProductStatusChangerModelRequest request, bool groupChange, CT ct)
    {
        _logger.LogInformation("Request for RequestGoodsSupplyProductStatusChanger, RequestGoodsSupplyProductStatusChanger");
        using (var transaction = new CreateTransaction())
        {
            var isValidRequest = await request.IsValidAsync<RequestGoodsSupplyProductStatusChangerValidator, RequestGoodsSupplyProductStatusChangerModelRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<RequestGoodsSupplyProductStatusChangerResponse>(isValidRequest.Error!);

            RequestGoodsSupplyProduct? value = null;
            if (request.RequestGoodsSupplyProduct is not null)
                value = request.RequestGoodsSupplyProduct;
            else if (request.Id is not null && request.Id > 0)
            {
                var requestGoodsSupplyQuery = await _mediator.Send(new GetRequestGoodsSupplyProductByIdQuery(request.Id!.Value), ct);
                if (requestGoodsSupplyQuery.IsFailure)
                    return Result.Failure<RequestGoodsSupplyProductStatusChangerResponse>(requestGoodsSupplyQuery.Error!);
                value = requestGoodsSupplyQuery.Value!;
            }
            else
                return Result.Failure<RequestGoodsSupplyProductStatusChangerResponse>(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyIds);

            RequestGoodsSupplyProduct? supplyProduct = null;
            var lastDescription = await DescriptionMacker(request.Description, request.Status, ct);
            if (!value.Status.Equals(request.Status))
            {
                if ((value.RequestGoodsSupply.Type == GoodsSupplyType.Project || value.RequestGoodsSupply.Type == GoodsSupplyType.Contractor) && request.Status == GoodsSupplyDetailStatus.ManagementConfirmed)
                {
                    if (value.Status == GoodsSupplyDetailStatus.ProjectManagerConfirmed)
                    {
                        value.SetStatus(GoodsSupplyDetailStatus.ManagementPending, null);
                        value.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                        {
                            if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                oo.UpdateStatus(GoodsSupplyDetailStatus.ManagementPending, null);
                        });
                    }

                    if (value.Status == GoodsSupplyDetailStatus.ManagementPending)
                    {
                        value.SetStatus(GoodsSupplyDetailStatus.ManagementConfirmed, lastDescription);
                        value.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                        {
                            if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                oo.UpdateStatus(GoodsSupplyDetailStatus.ManagementConfirmed, lastDescription);
                        });
                    }

                    if (value.DestinationWarehouseId is null)
                    {
                        var destinationWarehouseId = value.RequestGoodsSupply.ProjectOperation is not null ? value.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterWarehouses.FirstOrDefault(x => x.IsDefault)?.WarehouseId :
                            value.RequestGoodsSupply.Project?.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterWarehouses.FirstOrDefault(x => x.IsDefault)?.WarehouseId;
                        if (destinationWarehouseId is null)
                            return Result.Failure<RequestGoodsSupplyProductStatusChangerResponse>(RequestGoodsSupplyErrors.CostCenterDontHaveWarehouse);
                        else
                            value.SetDestinationWarehouseId(destinationWarehouseId!.Value);
                    }

                    var detail = new SetConfirmedGoodsSupplyProductModel();
                    detail.Commerce = new GoodsSupplyProductModelCommerce()
                    {
                        DestinationWarehouseId = value.DestinationWarehouseId!.Value,
                        CommerceDescription = request.Description,
                        RequestedCount = value.RequestedCount
                    };

                    var newRequest = new SetConfirmedGoodsSupplyProductModelRequest()
                    {
                        Id = value.Id,
                        ProductEntity = value,
                        Description = request.Description,
                        Detail = detail
                    };
                    var response = await _requestGoodsSupplyManagementLogic.SetConfirmedGoodsSupplyProduct(newRequest, ct);
                    if (response.IsFailure)
                        return Result.Failure<RequestGoodsSupplyProductStatusChangerResponse>(response.Error!);
                }
                else
                {
                    if (GSDSRules.ProjectManagerChecker.Any(x => x.Equals(request.Status)))
                        if (value.Status == GoodsSupplyDetailStatus.New || value.Status == GoodsSupplyDetailStatus.ProjectManagerResend)
                        {
                            value.SetStatus(GoodsSupplyDetailStatus.ProjectManagerPending, null);
                            value.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                            {
                                if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                    oo.UpdateStatus(GoodsSupplyDetailStatus.ProjectManagerPending, null);
                            });
                        }

                    if (GSDSRules.ManagerChecker.Any(x => x.Equals(request.Status)))
                        if (value.Status == GoodsSupplyDetailStatus.ProjectManagerConfirmed)
                        {
                            value.SetStatus(GoodsSupplyDetailStatus.ManagementPending, null);
                            value.RequestGoodsSupplyDetails.ToList().ForEach(oo =>
                            {
                                if (oo.Status != GoodsSupplyDetailStatus.PendingForSupply)
                                    oo.UpdateStatus(GoodsSupplyDetailStatus.ManagementPending, null);
                            });
                        }

                    var hasGoodsManager = false;
                    if (value.ProductId > 0)
                    {
                        var assignments = await _goodsManagerAssignmentRepo.FindHaveCategory(value.ProductId, ct);
                        hasGoodsManager = assignments;
                    }

                    var response = await _mediator.Send(
                        new RequestGoodsSupplyProductStatusChangerCommand(
                            value,
                            request.Status,
                            request.SendToSupply,
                            null,
                            null,
                            lastDescription,
                            hasGoodsManager),
                        ct);


                    if (response.IsFailure)
                        return Result.Failure<RequestGoodsSupplyProductStatusChangerResponse>(response.Error!);
                    supplyProduct = response.Value;
                }
            }

            if (GSDSRules.AllowForPM.Contains(value.Status))
            {
                var projectManagerId = await _rgsProductRepo.GetProjectManagerId(value.Id, ct);
                var thirdParty = await _thirdPartyRepo.GetById(projectManagerId, ct);
                await _relay.Send(new MessageEnvelope(
                    MessageSender.ClientSdk.Enums.MessageChannels.Inbox,

                    new TemplatedMessage(Guid.NewGuid().ToString(), "engineering-set-operator", new Dictionary<string, object?>
                    {
                        { "FullName", thirdParty?.FirstName + " " + thirdParty?.LastName },
                        { "RequestNumber", value.RequestSerialNumber },
                    })

                    , new UserTarget(thirdParty.UserId.Value)), ct);
            }

            if (GSDSRules.AllowForManagement.Contains(value.Status))
            {
                var ids = await _mediator.Send(new GetUsersByActionIdQuery([10812]), ct);
                if (!ids.IsBad() && ids.Value.Value is not null && ids.Value.Value.Data is not null)
                {
                    var thirdParties = await _thirdPartyRepo.GetByUserIds(
                        ids.Value.Value.Data.Listed(x => x.UserId), ct);

                    if (thirdParties is not null && thirdParties.Count > 0)
                    {
                        foreach (var item in thirdParties)
                        {
                            await _relay.Send(new MessageEnvelope(
                                MessageSender.ClientSdk.Enums.MessageChannels.Inbox,

                                new TemplatedMessage(Guid.NewGuid().ToString(), "engineering-set-operator", new Dictionary<string, object?>
                                {
                        { "FullName", item?.FirstName + " " + item?.LastName },
                        { "RequestNumber", value.RequestSerialNumber },
                                })

                                , new UserTarget(item.UserId.Value)), ct);
                        }
                    }
                }
            }

            if (value.Status == GoodsSupplyDetailStatus.GoodsManagerPending)
            {
                var gmThirdPartyIds = await GetGoodsManagerThirdPartyIds(
                    value.ProductId, ct);

                if (gmThirdPartyIds.HasAny())
                {
                    var thirdParties = await _thirdPartyRepo.GetByIds(
                        gmThirdPartyIds, ct);

                    foreach (var item in thirdParties.Where(x => x.UserId.HasValue))
                    {
                        await _relay.Send(new MessageEnvelope(
                            MessageSender.ClientSdk.Enums.MessageChannels.Inbox,

                            new TemplatedMessage(
                                Guid.NewGuid().ToString(),
                                "engineering-set-operator",
                                new Dictionary<string, object?>
                                {
                                { "FullName", item.FirstName + " " + item.LastName },
                                { "RequestNumber", value.RequestSerialNumber },
                                }),

                            new UserTarget(item.UserId!.Value)), ct);
                    }
                }
            }

            await _unitOfWork.CommitAsync(ct);

            /*if (groupChange == false)
                if (RequestGoodsSupplyProduct.AllowStatusForSendNotification.Any(x => x.Equals(request.Status)))
                    await SendNotification(supplyProduct?.CreatorId.ToString(), supplyProduct?.LastDescription, ct);

            if (RequestGoodsSupplyProduct.AllowStatusForRejectedMessage.Any(x => x.Equals(request.Status)))
                await NotifyTelegramChats(value, request.Description, ct);
*/
            transaction.Complete();
            return new RequestGoodsSupplyProductStatusChangerResponse(true, lastDescription);
        }
    }

    public async Task<Result<RequestGoodsSupplyProductGroupStatusChangerResponse?>> RequestGoodsSupplyProductGroupStatusChanger(RequestGoodsSupplyProductGroupStatusChangerRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<RequestGoodsSupplyProductGroupStatusChangerValidator, RequestGoodsSupplyProductGroupStatusChangerRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<RequestGoodsSupplyProductGroupStatusChangerResponse>(isValidRequest.Error!);

        using (var transaction = new CreateTransaction())
        {
            var productQuery = await _mediator.Send(new GetsRequestGoodsSupplyProductByIdsQuery(request.Ids), ct);
            if (productQuery.IsFailure)
                return Result.Failure<RequestGoodsSupplyProductGroupStatusChangerResponse>(productQuery.Error!);
            var values = productQuery.Value!.Data!;

            List<(string? reciever, string? content)> notifModel = [];
            if (values is not null && values.Count > 0)
                foreach (var item in values)
                {
                    var response = await RequestGoodsSupplyProductStatusChanger(new RequestGoodsSupplyProductStatusChangerModelRequest(
                        null, item, request.Status, request.SendToSupply, request.Description), true, ct);
                    if (response.IsFailure || response.Value is null)
                        return Result.Failure<RequestGoodsSupplyProductGroupStatusChangerResponse>(response.Error!);
                    var supplyProduct = response.Value;

                    if (GSDSRules.AllowStatusForSendNotification.Any(x => x == request.Status))
                        notifModel.Add(new(item.CreatorId.ToString(), supplyProduct!.LastDecription));
                }

            if (notifModel is not null && notifModel.Count > 0)
                foreach (var item in notifModel)
                    await SendNotification(item.reciever, item.content, ct);

            await _unitOfWork.CommitAsync(ct);

            transaction.Complete();
            return new RequestGoodsSupplyProductGroupStatusChangerResponse(true);
        }
    }

    public async Task NotifyChats(RequestGoodsSupplyProduct? goodsSupplyProduct, string? description, CT ct)
    {
        if (goodsSupplyProduct is not null)
        {
            var costCenter = goodsSupplyProduct.RequestGoodsSupply.IsProjectSupply ? goodsSupplyProduct.RequestGoodsSupply.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter : goodsSupplyProduct.RequestGoodsSupply.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter;
            var project = goodsSupplyProduct.RequestGoodsSupply.IsProjectSupply ? goodsSupplyProduct.RequestGoodsSupply.Project : goodsSupplyProduct.RequestGoodsSupply.ProjectOperation.Project;
            var messengers = await _messengerChannelRepo.GetFltrChannel([costCenter.Id], [project.Id], MessengerMessageType.SupplyRequest, ct);

            if (messengers is not null && messengers.Count > 0)
            {
                var currentUserId = _userProfileService.GetProfileInfo().UserId;
                var creator = "";
                var getUser = await _mediator.Send(new GetUserByIdQuery(currentUserId), ct);
                if (getUser.IsSuccess && getUser.Value is not null)
                {
                    creator = getUser.Value.FullName;
                }

                var creatorRequestId = goodsSupplyProduct?.CreatorId;
                var creatorResponse = await WebServicesLogic.UserDataReceiver(
                    new List<long> { creatorRequestId!.Value },
                    null, _mediator, ct);
                var (creatorRequest, creatorTelegramId) = GetCreatorDetails(creatorResponse);

                var productIds = goodsSupplyProduct?.RequestGoodsSupplyDetails.Select(x => x.ProductId).ToList();
                var productModelList = await GetProductModels(productIds, goodsSupplyProduct!, ct);


                foreach (var item in messengers)
                {
                    var createDateShamsi = TimeCalculator.ConvertToShamsi(DateTime.Now);
                    var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                    var createTime = TimeZoneInfo.ConvertTime(DateTime.Now, tehranTimeZone).ToString("HH:mm:ss");
                    var projectName = " ";
                    if (goodsSupplyProduct is not null)
                    {
                        var projectNameQuery = await _mediator.Send(new GetProjectNameRequestGoodsSupplyQuery(goodsSupplyProduct.Id), ct);
                        projectName = projectNameQuery.Value;
                    }

                    foreach (var chat in messengers)
                    {
                        var message = GoodsSupplyStatusChangerMessageModel(goodsSupplyProduct!.Status,
                            goodsSupplyProduct!.RequestSerialNumber,
                            costCenter.CostCenterName,
                            project.ProjectName,
                            productModelList,
                            description,
                            createDateShamsi,
                            createTime,
                            creator,
                            creatorTelegramId,
                            creatorRequest);

                        await TelegramServicesLogic.SendMessage(chat,
                            message,
                            null,
                            _mediator,
                            _authorization,
                            _messageSenderConfig, ct);
                    }
                }
            }
        }
    }

    public async Task NotifyTelegramChats(RequestGoodsSupplyProduct? goodsSupplyProduct, string? description, CT ct)
    {
        if (goodsSupplyProduct is not null)
        {
            var telegramChatsResult = await _mediator.Send(new GetByRequestGoodsSupplyIdQuery(goodsSupplyProduct.Id, true), ct);

            if (telegramChatsResult.IsSuccess && telegramChatsResult.Value?.Data != null)
            {
                var currentUserId = _userProfileService.GetProfileInfo().UserId;
                var creator = "";
                var getUser = await _mediator.Send(new GetUserByIdQuery(currentUserId), ct);
                if (getUser.IsSuccess && getUser.Value is not null)
                {
                    creator = getUser.Value.FullName;
                }

                var creatorRequestId = goodsSupplyProduct?.CreatorId;
                var creatorResponse = await WebServicesLogic.UserDataReceiver(
                    new List<long> { creatorRequestId!.Value },
                    null, _mediator, ct);
                var (creatorRequest, creatorTelegramId) = GetCreatorDetails(creatorResponse);

                var productIds = goodsSupplyProduct?.RequestGoodsSupplyDetails.Select(x => x.ProductId).ToList();
                var productModelList = await GetProductModels(productIds, goodsSupplyProduct!, ct);


                foreach (var item in telegramChatsResult.Value.Data)
                {
                    var createDateShamsi = TimeCalculator.ConvertToShamsi(DateTime.Now);
                    var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                    var createTime = TimeZoneInfo.ConvertTime(DateTime.Now, tehranTimeZone).ToString("HH:mm:ss");
                    var projectName = " ";
                    if (goodsSupplyProduct is not null)
                    {
                        var projectNameQuery = await _mediator.Send(new GetProjectNameRequestGoodsSupplyQuery(goodsSupplyProduct.Id), ct);
                        projectName = projectNameQuery.Value;
                    }

                    var sendChats = item.TelegramChatTypes.Where(x =>
                            x.TelegramMessageType == Domain.Entities.TelegramChats.Enums.TelegramMessageType.SupplyRequestReturn ||
                            x.TelegramMessageType == Domain.Entities.TelegramChats.Enums.TelegramMessageType.SupplyRequestRejection ||
                            x.TelegramMessageType == Domain.Entities.TelegramChats.Enums.TelegramMessageType.OtherGroups)
                        .GroupBy(x => x.ChatId)
                        .Select(g => g.FirstOrDefault())
                        .ToList();

                    if (sendChats is not null && sendChats.Any())
                    {
                        foreach (var chat in sendChats)
                        {
                            await TelegramServicesLogic.GoodsSupplyStatusChangerMessage(
                                chat!.ChatId,
                                goodsSupplyProduct?.Status,
                                goodsSupplyProduct?.RequestSerialNumber,
                                item.CostCenter.CostCenterName,
                                projectName,
                                productModelList,
                                description,
                                createDateShamsi,
                                createTime,
                                creator,
                                creatorTelegramId,
                                creatorRequest,
                                _telegramMessageHistoryLogic,
                                item.Id,
                                chat.TelegramMessageType,
                                _authorization,
                                _messageSenderConfig,
                                ct);
                        }
                    }
                }
            }
        }
    }

    public async Task<Result<CreateMessageResponse?>> SendNotification(string? reciever, string? content, CT ct)
    {
        /*reciever = string.IsNullOrEmpty(reciever) ? "1" : reciever;
        content = string.IsNullOrEmpty(content) ? "درخواست تامین برگشت یا رد شده است." : content;
        var sendMessage = await _mediator.Send(new CreateMessageCommand(reciever, content, MessagePriority.Medium, MessageType.Notification), ct);
        if (sendMessage.IsFailure)
            return Result.Failure<CreateMessageResponse>(sendMessage.Error!);*/

        var message = "sendMessage.Value";
        return new CreateMessageResponse(new Guid(), "", "", "", MessagePriority.High, MessageType.Notification, MessageStatus.Sent);
    }

    public (string FullName, string TelegramId) GetCreatorDetails(IEnumerable<FilteredUserResponseModel>? creatorResponse)
    {
        var creator = creatorResponse?.FirstOrDefault();
        var creatorRequest = creator?.FullName ?? "";
        var creatorTelegramId = creator?.Contacts?.FirstOrDefault(x => x.ContactTypeId == 12)?.ContactPoint ?? "";
        return (creatorRequest, creatorTelegramId);
    }

    public async Task<List<ProductModel>> GetProductModels(List<long>? productIds, RequestGoodsSupplyProduct goodsSupply, CT ct)
    {
        var products = await WebServicesLogic.ProductsDataReceiver(productIds, _mediator, _productRepo, ct);
#pragma warning disable CS8604 // Possible null reference argument.
        return products.Select(product => new ProductModel
        {
            ProductName = product.Name,
            ProductCode = product.Code,
            RequestedCount = goodsSupply.RequestGoodsSupplyDetails.FirstOrDefault(x => x.ProductId == product.Id)?.RequestedCount
        }).ToList();
#pragma warning restore CS8604 // Possible null reference argument.
    }


    public async Task<Result<GetRequestGoodsSupplyProductByIdResponse?>> GetRequestGoodsSupplyProductById(GetRequestGoodsSupplyProductByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestGoodsSupplyProductByIdValidator, GetRequestGoodsSupplyProductByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestGoodsSupplyProductByIdResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetRequestGoodsSupplyProductByIdModeledQuery(request.Id), ct);
        if (responses.IsFailure)
            return Result.Failure<GetRequestGoodsSupplyProductByIdResponse>(responses.Error!);

        var value = responses.Value!;

        var measurments = await WebServicesLogic.MeasurementDataReceiver([value.ProjectOperationMeasureId], _mediator, ct);

        var creators = await WebServicesLogic.UserDataReceiver([value.CreatorId!.Value], null, _mediator, ct);
        var products = await WebServicesLogic.ProductsDataReceiver([value.ProductId!.Value], _mediator, _productRepo, ct);
        if (value.CurrencyId is not null)
        {
            var currencies = await WebServicesLogic.CurrenciesDataReceiver([value.CurrencyId.Value], _mediator, ct);
            var currency = currencies?.Where(m => m.Id == value.CurrencyId).FirstOrDefault();
            value.CurrencyName = currency?.Name;
        }

        var packagesInfo = new List<GetsPackagesByIdsModel>();
        if (value.PackageId is not null)
            packagesInfo = await WebServicesLogic.PackagesDataReceiver([value.PackageId!.Value], _mediator, ct);

        var warehouseIds = new List<long?>();
        if (value.DestinationWarehouseId is not null)
            warehouseIds.Add(value.DestinationWarehouseId!.Value);

        var metaIds = new List<long>();
        metaIds.AddRange(new[] { value.SupplyerId, value.BuyerId, value.ContractorId }.Where(id => id.HasValue).Select(id => id!.Value));

        if (value.Managements is not null && value.Managements.Any())
        {
            metaIds.AddRange(value.Managements.Where(x => x!.OperatorAppointmentId is not null && x.OperatorAppointmentId > 0).Select(x => x.OperatorAppointmentId!.Value).ToList());
            warehouseIds.AddRange(value.Managements.Where(x => x!.DestinationWarehouseId is not null && x.DestinationWarehouseId > 0).Select(x => x.DestinationWarehouseId).ToList());
            warehouseIds.AddRange(value.Managements.Where(x => x!.WarehouseId is not null && x.WarehouseId > 0).Select(x => x.WarehouseId).ToList());
        }

        var newMetaIds = metaIds.Distinct().ToList();
        var metaInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(newMetaIds, null, null, _mediator, ct);

        var newWarehouseIds = warehouseIds.Distinct().ToList();
        var warehouses = await WebServicesLogic.WarehousesDataReceiver(newWarehouseIds, _mediator, ct);

        var creator = creators?.Where(m => m.UserId == value.CreatorId).FirstOrDefault();
        var product = products?.Where(m => m.Id == value.ProductId).FirstOrDefault();
        var group = product?.Group;
        var package = packagesInfo?.Where(x => x.Id.Equals(value.PackageId)).FirstOrDefault();
        var supplyer = metaInfos?.Where(x => x is not null && x.Id.Equals(value.SupplyerId)).FirstOrDefault();
        var buyer = metaInfos?.Where(x => x is not null && x.Id.Equals(value.BuyerId)).FirstOrDefault();
        var contractor = metaInfos?.Where(x => x is not null && x.Id.Equals(value.ContractorId)).FirstOrDefault();
        var warehouse = warehouses?.Where(x => x.Id.Equals(value.DestinationWarehouseId)).FirstOrDefault();

        value.Creator = creator?.FullName;
        value.MeasurementName = measurments?.FirstOrDefault()?.Name;
        value.ProductName = product?.Name;
        value.ProductBrand = product?.Brand;
        value.ProductBrandModel = product?.BrandModel;
        value.ProductGroupId = product?.Group.Id;
        value.ProductGroupName = product?.Group.Name;
        value.ProductGroupCode = product?.Group.Code;
        value.PackageName = package?.Title;
        value.PackageQuantity = package?.Quantity;
        value.SupplyerFullName = supplyer?.FullName;
        value.BuyerFullName = buyer?.FullName;
        value.ContractorFullName = contractor?.FullName;
        value.DestinationWarehouseName = warehouse?.Name;

        var responseSupplyDetails = await _mediator.Send(new GetsGoodsSupplyDetailByProductIdQuery(request.Id, 0, 0), ct);

        var details = new List<GetFilteredRequestGoodsSupplyDetailsModel>();
        var requestGoodsSupplyCreator = metaInfos?.Where(oo => oo!.UserId.Equals(value.CreatorId)).FirstOrDefault();
        var supplyDetails = responseSupplyDetails.Value?.Data;
        if (supplyDetails is not null)
            foreach (var detail in supplyDetails)
            {
                var operationId = value.ProjectOperationId;
                var operationDetailId = detail.ConsumableVolumeProduct.ProjectOperationDetail.Id;

                CalculatProjectOperationDetailSupplyCounterModel total = new();
                var productGroupsQuery = await _mediator.Send(new GetsConsumableVolumeProductsForSupplyQuery(null, operationId, operationDetailId, value.ProductGroupId, null, null, 1, 10), ct);
                if (productGroupsQuery.Value?.Data is not null && productGroupsQuery.Value?.Data.Count > 0)
                    total = TotalDataReceiver(productGroupsQuery.Value!.Data!.FirstOrDefault()!);

                details.Add(new()
                {
                    RequestGoodsSupplyId = detail.RequestGoodsSupply.Id,
                    ConsumableVolumeProductId = detail.ConsumableVolumeProduct.Id,
                    Id = detail.Id,
                    GroupId = detail.ConsumableVolumeProduct.ProductGroupId,
                    GroupName = group?.Name,
                    GroupCode = group?.Code,
                    GroupMeasure = group?.Measure,
                    Product = product,
                    ProductId = detail.ProductId,
                    ProductCode = product?.Code,
                    ProductName = product?.Name,
                    ProductNumber = detail.ConsumableVolumeProduct.FinalValue,
                    Brand = product?.Brand,
                    BrandModel = product?.BrandModel,
                    Importance = detail.Importance,
                    Status = detail.Status,
                    PrivateName = detail.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PrivateName,
                    PrivateCode = detail.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PrivateCode,
                    PublicName = detail.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PublicName,
                    PublicCode = detail.ConsumableVolumeProduct.ProjectOperationDetail.OperationLocation.PublicCode,
                    ProjectOperationDetailDescription = detail.ConsumableVolumeProduct.ProjectOperationDetail.Description,
                    DelivaryDeadLine = detail.DelivaryDeadLine,
                    TolerancePercentage = total.TolerancePercentage,
                    ToleranceCount = total.ToleranceCount,
                    TotalEstimatedCount = total.TotalEstimatedCount,
                    TotalRequestedCount = total.TotalRequestedCount,
                    TotalSupplyCount = total.TotalSupplyCount,
                    TotalRemainedCount = total.TotalRemainedCount,
                    RequestedCount = detail.RequestedCount,
                    SupplyCount = detail.RequestGoodsSupplyManagements.Where(x => x.Status == GoodsSupplyManagementStatus.CompleteSupply).Sum(x => x.RequestedCount),
                    UnitPrice = detail.UnitPrice,
                    TotalPrice = detail.TotalPrice,
                    DiscountByNumber = detail.DiscountByNumber,
                    DiscountByPercentage = detail.DiscountByPercentage,
                    DiscountedPrice = detail.DiscountedPrice,
                    TaxNumber = detail.TaxNumber,
                    TaxPercentage = detail.TaxPercentage,
                    PackingPrice = detail.PackingPrice,
                    FinalPrice = detail.FinalPrice,
                    Description = detail.Description,
                    ManagementDescription = detail.ManagementDescription,
                    DestinationWarehouseId = detail.DestinationWarehouseId,
                    DestinationWarehouse = warehouse?.Name,
                    Creator = requestGoodsSupplyCreator?.FullName,
                    Package = new(package?.Id, package?.Quantity, package?.IsDefault, package?.IsActive, package?.Title, detail.PackageCount),
                    Documents = detail.RequestGoodsSupplyDetailDocuments.Select(oo => oo.Url).ToList(),
                    ContractorId = detail.ContractorId,
                    FullName = metaInfos?.Where(x => x?.Id == detail.ContractorId).FirstOrDefault()?.FullName,
                    CustomerInvoiceNumber = detail.CustomerInvoiceNumber,
                    LastDescription = detail.LastDescription
                });
            }

        value.Details = details;
        if (value.Managements is not null && value.Managements.Any())
        {
            foreach (var management in value.Managements!)
            {
                var mWarehouse = warehouses?.Where(x => x.Id.Equals(management.WarehouseId)).FirstOrDefault();
                var mDestinationWarehouse = warehouses?.Where(x => x.Id.Equals(management.DestinationWarehouseId)).FirstOrDefault();
                var operatorAppointment = metaInfos?.Where(x => x is not null && x.Id.Equals(management.OperatorAppointmentId)).FirstOrDefault();

                management.WarehouseName = mWarehouse?.Name;
                management.WarehouseName = mWarehouse?.Name;
                management.ProductName = product?.Name;
                management.ProductCode = product?.Code;
                management.Brand = product?.Brand;
                management.BrandModel = product?.BrandModel;
                management.DestinationWarehouseName = mDestinationWarehouse?.Name;
                management.OperatorAppointmentName = operatorAppointment?.FullName;
            }

            value.OperatorAppointmentName = string.Join(" - ", value.Managements.Select(x => x.OperatorAppointmentName).ToList());
        }

        return value;
    }

    public async Task<Result<GetsGoodsSupplyDetailBySupplyProductIdResponse?>> GetsGoodsSupplyDetailBySupplyProductId(GetsGoodsSupplyDetailBySupplyProductIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsGoodsSupplyDetailBySupplyProductIdValidator, GetsGoodsSupplyDetailBySupplyProductIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsGoodsSupplyDetailBySupplyProductIdResponse>(isValidRequest.Error!);

        List<GetsGoodsSupplyDetailBySupplyProductIdModel> values = [];
        var supplyProduct = await _requestGoodsSupplyDetailRepository.GetRequestGoodSupply(request.Id, ct);

        if (!supplyProduct.RequestGoodsSupply.IsProjectSupply)
        {
            var responses = await _mediator.Send(new GetsGoodsSupplyDetailBySupplyProductIdQuery(request.Id), ct);
            if (responses.IsFailure || responses.Value!.Data is null)
                return Result.Failure<GetsGoodsSupplyDetailBySupplyProductIdResponse>(responses.Error!);
            values = responses.Value!.Data!;

            var products = await _productRepo.GetProductByIds([values!.FirstOrDefault()!.ProductId!.Value], ct);
            var product = products?.FirstOrDefault();

            var packagesInfo = new List<GetsPackagesByIdsModel>();
            if (values.FirstOrDefault()?.PackageId is not null)
                packagesInfo = await WebServicesLogic.PackagesDataReceiver([values.FirstOrDefault()!.PackageId!.Value], _mediator, ct);

            var projectOperationDetailIds = values
                .Where(x => x.ProjectOperationDetailId.HasValue)
                .Select(x => x.ProjectOperationDetailId!.Value)
                .ToList();

            var locationResponses = await _mediator.Send(new GetsLocationByProjectOperationDetailIdsQuery(projectOperationDetailIds), ct);
            var locations = locationResponses.Value?.Data;

            foreach (var detail in values)
            {
                detail.ProductId = detail.ProductId;
                detail.ProductCode = product?.Code;
                detail.ProductName = product?.Name;
                detail.Brand = product?.Brand;
                detail.BrandModel = product?.BrandModel;

                if (locations is not null)
                {
                    var location = locations.FirstOrDefault(x => x.ProjectOperationDetailId.Equals(detail.ProjectOperationDetailId));
                    detail.PrivateName = location?.PrivateName;
                    detail.PrivateCode = location?.PrivateCode;
                    detail.PublicCode = location?.PublicCode;
                    detail.PublicName = location?.PublicName;
                    detail.ProjectOperationDetailDescription = location?.Description;
                }

                var package = packagesInfo?.FirstOrDefault();
                detail.Package = new(package?.Id, package?.Quantity, package?.IsDefault, package?.IsActive, package?.Title, detail.PackageCount);
            }
        }

        else if (supplyProduct.RequestGoodsSupply.IsProjectSupply)
        {
            var responses = await _requestGoodsSupplyDetailRepository.GetsProjectGoodsSupplyDetailBySupplyProductId(request.Id, ct);
            if (responses is null)
                return Result.Failure<GetsGoodsSupplyDetailBySupplyProductIdResponse>(RequestGoodsSupplyErrors.ProdouctNotFound!);
            values = responses;

            var products = await _productRepo.GetProductByIds([values!.FirstOrDefault()!.ProductId!.Value], ct);
            var product = products?.FirstOrDefault();

            var packagesInfo = new List<GetsPackagesByIdsModel>();
            if (values.FirstOrDefault()?.PackageId is not null)
                packagesInfo = await WebServicesLogic.PackagesDataReceiver([values.FirstOrDefault()!.PackageId!.Value], _mediator, ct);

            foreach (var detail in values)
            {
                detail.ProductId = detail.ProductId;
                detail.ProductCode = product?.Code;
                detail.ProductName = product?.Name;
                detail.Brand = product?.Brand;
                detail.BrandModel = product?.BrandModel;
                var package = packagesInfo?.FirstOrDefault();
                detail.Package = new(package?.Id, package?.Quantity, package?.IsDefault, package?.IsActive, package?.Title, detail.PackageCount);
            }
        }

        return new GetsGoodsSupplyDetailBySupplyProductIdResponse(values!);
    }

    public async Task<Result<GetGoodsSupplyProductByIdResponse?>> GetGoodsSupplyProductById(GetGoodsSupplyProductByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetGoodsSupplyProductByIdValidator, GetGoodsSupplyProductByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetGoodsSupplyProductByIdResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetGoodsSupplyProductByIdQuery(request.Id), ct);
        if (responses.IsFailure)
            return Result.Failure<GetGoodsSupplyProductByIdResponse>(responses.Error!);
        var value = responses.Value!;

        var locationResponses = await _mediator.Send(new GetsLocationByProjectOperationDetailIdsQuery(value.ProjectOperationDetailIds), ct);
        var locations = locationResponses.Value?.Data;
        if (locations is not null)
        {
            value.ProjectOperationDetailNames = string.Join(" - ", locations!.Select(x => x.PrivateName).ToList() ?? []);
            value.ProjectOperationDetailCodes = string.Join(" - ", locations!.Select(x => x.PrivateCode).ToList() ?? []);
            value.ProjectOperationDetailDescriptions = string.Join(" - ", locations!.Select(x => x.Description).ToList() ?? []);
        }

        var measurments = await WebServicesLogic.MeasurementDataReceiver([value.ProjectOperationMeasureId], _mediator, ct);

        var creators = await WebServicesLogic.UserDataReceiver([value.CreatorId!.Value], null, _mediator, ct);
        var products = await WebServicesLogic.ProductsDataReceiver([value.ProductId!.Value], _mediator, _productRepo, ct);
        if (value.CurrencyId is not null)
        {
            var currencies = await WebServicesLogic.CurrenciesDataReceiver([value.CurrencyId.Value], _mediator, ct);
            var currency = currencies?.Where(m => m.Id == value.CurrencyId).FirstOrDefault();
            value.CurrencyName = currency?.Name;
        }

        var packagesInfo = new List<GetsPackagesByIdsModel>();
        if (value.PackageId is not null)
            packagesInfo = await WebServicesLogic.PackagesDataReceiver([value.PackageId!.Value], _mediator, ct);

        var warehouseIds = new List<long?>();
        if (value.DestinationWarehouseId is not null)
            warehouseIds.Add(value.DestinationWarehouseId!.Value);

        var metaIds = new List<long>();
        metaIds.AddRange(new[] { value.SupplyerId, value.BuyerId, value.ContractorId }.Where(id => id.HasValue).Select(id => id!.Value));

        var newMetaIds = metaIds.Distinct().ToList();
        var metaInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(newMetaIds, null, null, _mediator, ct);

        var newWarehouseIds = warehouseIds.Distinct().ToList();
        var warehouses = await WebServicesLogic.WarehousesDataReceiver(newWarehouseIds, _mediator, ct);

        var creator = creators?.Where(m => m.UserId == value.CreatorId).FirstOrDefault();
        var product = products?.Where(m => m.Id == value.ProductId).FirstOrDefault();
        var group = product?.Group;
        var package = packagesInfo?.Where(x => x.Id.Equals(value.PackageId)).FirstOrDefault();
        var supplyer = metaInfos?.Where(x => x is not null && x.Id.Equals(value.SupplyerId)).FirstOrDefault();
        var buyer = metaInfos?.Where(x => x is not null && x.Id.Equals(value.BuyerId)).FirstOrDefault();
        var contractor = metaInfos?.Where(x => x is not null && x.Id.Equals(value.ContractorId)).FirstOrDefault();
        var warehouse = warehouses?.Where(x => x.Id.Equals(value.DestinationWarehouseId)).FirstOrDefault();

        value.Creator = creator?.FullName;
        value.MeasurementName = measurments?.FirstOrDefault()?.Name;
        value.ProductName = product?.Name;
        value.ProductBrand = product?.Brand;
        value.ProductBrandModel = product?.BrandModel;
        value.ProductGroupId = product?.Group.Id;
        value.ProductGroupName = product?.Group.Name;
        value.ProductGroupCode = product?.Group.Code;
        value.PackageName = package?.Title;
        value.PackageQuantity = package?.Quantity;
        value.SupplyerFullName = supplyer?.FullName;
        value.BuyerFullName = buyer?.FullName;
        value.ContractorFullName = contractor?.FullName;
        value.DestinationWarehouseName = warehouse?.Name;

        return value;
    }

    public async Task<Result<GetGoodsSupplyProductDocumentsResponse?>> GetGoodsSupplyProductDocuments(GetGoodsSupplyProductDocumentsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetGoodsSupplyProductDocumentsValidator, GetGoodsSupplyProductDocumentsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetGoodsSupplyProductDocumentsResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetGoodsSupplyProductDocumentsQuery(request.Id), ct);
        if (responses.IsFailure)
            return Result.Failure<GetGoodsSupplyProductDocumentsResponse>(responses.Error!);

        return new GetGoodsSupplyProductDocumentsResponse(responses.Value);
    }

    public async Task<Result<GetAllGoodsSupplyProductDocumentResponse?>> GetAllGoodsSupplyProductDocument(GetAllGoodsSupplyProductDocumentRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetAllGoodsSupplyProductDocumentValidator, GetAllGoodsSupplyProductDocumentRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetAllGoodsSupplyProductDocumentResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetAllGoodsSupplyProductDocumentQuery(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.ProductIds,
            request.CityId,
            request.Types,
            request.Statuses,
            request.StartDate,
            request.EndDate,
            request.PageIndex,
            request.PageSize
            ), ct);
        if (responses.IsFailure)
            return Result.Failure<GetAllGoodsSupplyProductDocumentResponse>(responses.Error!);

        return new GetAllGoodsSupplyProductDocumentResponse(responses.Value);
    }

    public async Task<Result<GetsGoodsSupplyProductResponse?>> GetsGoodsSupplyProduct(GetsGoodsSupplyProductRequest request, CT ct)
    {
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);

        bool checkThirdParty = false;
        var engConfig = await _mediator.Send(new GetActiveConfigQuery());
        if (request.ThirdPartyId is not null)
            if (!engConfig.IsBad() && engConfig.Value!.ProjectThirdParties)
                checkThirdParty = true;

        List<long>? managerSelectedProductIds = null;
        if (request.GoodsManagerId is not null && request.GoodsManagerId > 0)
        {
            var organizationsIds = await _organizationRepo.GetByManagerId(request.GoodsManagerId!.Value, ct);
            if (organizationsIds is not null && organizationsIds.Count > 0)
            {
                var selectedProduct = await _mediator.Send(new GetAllManagerGoodsQuery(organizationsIds), ct);
                if (selectedProduct.Value is not null && selectedProduct.Value.Count > 0)
                    managerSelectedProductIds = selectedProduct.Value;
                else
                    return new GetsGoodsSupplyProductResponse(new List<GetsGoodsSupplyProductModel>(0), 0);
            }
            else
                return new GetsGoodsSupplyProductResponse(new List<GetsGoodsSupplyProductModel>(0), 0);
        }

        List<long>? requestProductIds = await GetProductIdsForFilter(request.FilterProduct, request.ProductIds, ct);
        var responses = await _mediator.Send(new GetsGoodsSupplyProductQuery(
            request,
            request.Ids,
            requestProductIds,
            managerSelectedProductIds,
            companyId!.Value,
            checkThirdParty,
            false), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsGoodsSupplyProductResponse>(responses.Error!);

        var values = responses.Value!.Data!;

        var projectOperationDetailIds = values.Where(x => x.ProjectOperationDetailIds.Any()).SelectMany(x => x.ProjectOperationDetailIds).Distinct().ToList();
        if (projectOperationDetailIds.Count > 0)
        {
            var locationResponses = await _mediator.Send(new GetsLocationByProjectOperationDetailIdsQuery(projectOperationDetailIds), ct);
            var locations = locationResponses.Value?.Data;
            if (locations is not null)
                foreach (var value in values)
                {
                    var valueLocations = locations.Where(x => value.ProjectOperationDetailIds.Contains(x.ProjectOperationDetailId)).ToList();

                    value.ProjectOperationDetailNames = StringSeparator.JoinWithDash(valueLocations, x => x.PrivateName ?? " ");
                    value.ProjectOperationDetailCodes = StringSeparator.JoinWithDash(valueLocations, x => x.PrivateCode ?? " ");
                    value.ProjectOperationDetailDescriptions = StringSeparator.JoinWithDash(valueLocations, x => x.Description ?? " ");
                }
        }

        var measureUnitIds = values.Where(x => x.ProjectOperationMeasureId != null).Select(x => x.ProjectOperationMeasureId).Distinct().ToList();

        List<Measureunit> measurments = [];
        if (measureUnitIds.Any())
        {
            measurments = await WebServicesLogic.MeasurementDataReceiver(measureUnitIds.Select(x => x.Value).ToList(), _mediator, ct);
        }

        var creatorIds = values.Where(x => x.CreatorId is not null && x.CreatorId > 0).Select(c => c.CreatorId!.Value).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var currencyIds = values.Where(x => x.CurrencyId is not null && x.CurrencyId > 0).Select(c => c.CurrencyId!.Value).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var productIds = values.Where(x => x.ProductId is not null && x.ProductId > 0).Select(x => x.ProductId!.Value).Distinct().ToList();
        var products = await _productRepo.GetProductByIds(productIds, ct);

        var packageIds = values.Where(x => x.PackageId is not null && x.PackageId > 0).Select(x => x.PackageId!.Value).Distinct().ToList();
        var packagesInfo = await WebServicesLogic.PackagesDataReceiver(packageIds, _mediator, ct);

        var metaIds = values.SelectMany(x => new long?[] { x.SupplyerId, x.BuyerId, x.ProjectManagerId, x.ContractorId })
            .Where(id => id.HasValue && id > 0).Select(id => id!.Value).Distinct().ToList();
        var oaIds = values.Where(x => x.OperatorAppointmentIds is not null && x.OperatorAppointmentIds.Count > 0).SelectMany(x => x.OperatorAppointmentIds!).Distinct().ToList();
        metaIds.AddRange(oaIds);
        var metaInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(metaIds, null, null, _mediator, ct);

        var organizationIds = values.NullListed(x => x.RequestingOrganizationId);
        List<ViewOrganization>? organizations = [];
        if (organizationIds is not null && organizationIds.Count > 0)
            organizations = await _viewOrganizationRepository.GetByIds(organizationIds, ct);

        var newWarehouseIds = values.NullListed(x => x.DestinationWarehouseId);
        newWarehouseIds.AddRange(values.NullListed(x => x.DefaultWarehouseId));
        var warehouseRes = await _mediator.Send(new GetWarehouseByIdsQuery(newWarehouseIds, false, 1, newWarehouseIds.Count()));
        List<GetWarehouseByIdsModel> warehouses = [];
        if (!warehouseRes.IsBad() && warehouseRes.Value!.Data != null)
            warehouses = warehouseRes.Value.Data;

        foreach (var item in values)
        {
            var creator = creators?.FirstOrDefault(m => m.UserId == item.CreatorId);
            var measurment = measurments?.FirstOrDefault(m => m.Id == item.ProjectOperationMeasureId);
            var currency = currencies?.FirstOrDefault(m => m.Id == item.CurrencyId);
            var product = products?.FirstOrDefault(m => m.Id == item.ProductId);
            var package = packagesInfo?.FirstOrDefault(x => x.Id.Equals(item.PackageId));
            var supplyer = metaInfos?.FirstOrDefault(x => x is not null && x.Id.Equals(item.SupplyerId));
            var buyer = metaInfos?.FirstOrDefault(x => x is not null && x.Id.Equals(item.BuyerId));
            var contractor = metaInfos?.FirstOrDefault(x => x is not null && x.Id.Equals(item.ContractorId));
            var warehouse = warehouses?.FirstOrDefault(x => x.Id.Equals(item.DestinationWarehouseId));
            var defWarehouse = warehouses?.FirstOrDefault(x => x.Id.Equals(item.DefaultWarehouseId));
            var projectManager = metaInfos?.FirstOrDefault(x => x is not null && x.Id.Equals(item.ProjectManagerId));
            var org = organizations?.FirstOrDefault(x => x.Id == item.RequestingOrganizationId);

            item.MeasurementName = measurment?.Name;
            item.Creator = creator?.FullName;
            item.CurrencyName = currency?.Name;
            item.ProductName = product?.Name;
            item.ProductCode = product?.Code;
            item.ProductBrand = product?.Brand;
            item.ProductDescription = product?.ProductDescription;
            item.ProductBrandModel = product?.BrandModel;
            item.ProductIsActive = product?.IsActive;
            item.ProductGroupId = product?.Group.Id;
            item.ProductGroupName = product?.Group.Name;
            item.ProductGroupCode = product?.Group.Code;
            item.GroupIsActive = product?.Group?.IsActive;
            item.ProductGroupMeasurementName = product?.Group.Measure;
            item.PackageName = package?.Title;
            item.PackageQuantity = package?.Quantity;
            item.SupplyerFullName = supplyer?.FullName;
            item.BuyerFullName = buyer?.FullName;
            item.ContractorFullName = contractor?.FullName;
            item.DestinationWarehouseName = warehouse?.Name;
            item.ManagerFullName = warehouse?.ManagerFullName;
            item.OrganizationName = warehouse?.OrganizationName;
            item.OwnerFullName = warehouse?.OwnerFullName;
            item.ProjectManager = projectManager?.FullName;
            item.DefaultWarehouse = defWarehouse?.Name;
            item.RequestingOrganization = org?.NameFa;

            if (item.OperatorAppointmentIds is not null && item.OperatorAppointmentIds.Any())
            {
                var operatorAppointments = metaInfos?.Where(x => x is not null && item.OperatorAppointmentIds.Contains(x.Id)).Select(x => x?.FullName).ToList();
                if (operatorAppointments is not null)
                    item.OperatorAppointmentName = string.Join(" - ", operatorAppointments);
            }
        }

        return new GetsGoodsSupplyProductResponse(values ?? new List<GetsGoodsSupplyProductModel>(0), responses!.Value!.RowCount!);
    }

    public async Task<Result<GetsRequestGoodsSupplyProductResponse?>> GetsRequestGoodsSupplyProduct(GetsRequestGoodsSupplyProductRequest request, CT ct)
    {
        bool checkThirdParty = false;
        var engConfig = await _mediator.Send(new GetActiveConfigQuery());
        if (request.ThirdPartyId is not null)
            if (!engConfig.IsBad() && engConfig.Value!.ProjectThirdParties)
                checkThirdParty = true;

        var cmp = _userInfoService.UserCompanyId;

        List<long>? requestProductIds = await GetProductIdsForFilter(request.FilterProduct, request.ProductIds, ct);
        var responses = await _mediator.Send(new GetsRequestGoodsSupplyProductQuery(
            request.Ids,
            request.requestGoodsSupplyIds,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            requestProductIds,
            request.CreatorIds,
            request.WarehouseIds,
            request.CityId,
            request.ProjectManagerId,
            request.ThirdPartyId,
            request.Importances,
            request.Types,
            request.Statuses,
            request.RemoveStatuses,
            request.StartDate,
            request.EndDate,
            request.RequestNumber,
            request.FilterDescription,
            request.FilterPublicName,
            request.FilterOperationInfoName,
            request.FilterManagerDescription,
            request.FilterData,
            request.CustomerInvoiceNumber,
            request.OrderBy,
            request.IsExcel ?? false,
            request.ContainDraft,
            checkThirdParty,
            request.PageIndex,
            request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsRequestGoodsSupplyProductResponse>(responses.Error!);
        var values = responses.Value!.Data!;

        var measureUnitIds = values.NullListed(x => x.ProjectOperationMeasureId);
        var measurments = await WebServicesLogic.MeasurementDataReceiver(measureUnitIds, _mediator, ct);

        var creatorIds = values.Where(x => x.CreatorId is not null && x.CreatorId > 0).Select(c => c.CreatorId!.Value).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var currencyIds = values.Where(x => x.CurrencyId is not null && x.CurrencyId > 0).Select(c => c.CurrencyId!.Value).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var productIds = values.Where(x => x.ProductId is not null && x.ProductId > 0).Select(x => x.ProductId!.Value).Distinct().ToList();
        var productsData = await _productRepo.GetProductsByIds(productIds, false, null, 0, 0, ct);
        var products = productsData.Data;

        var packageIds = values.Where(x => x.PackageId is not null && x.PackageId > 0).Select(x => x.PackageId!.Value).Distinct().ToList();
        var packagesInfo = await WebServicesLogic.PackagesDataReceiver(packageIds, _mediator, ct);

        var metaIds = values.SelectMany(x => new long?[] { x.SupplyerId, x.BuyerId, x.ProjectManagerId, x.ContractorId }
            .Concat(x.Managements?.Where(m => m.OperatorAppointmentId is not null && m.OperatorAppointmentId > 0)
            .Select(m => m.OperatorAppointmentId) ?? Enumerable.Empty<long?>()))
            .Where(id => id.HasValue && id > 0).Select(id => id!.Value).Distinct().ToList();
        var metaInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(metaIds, null, null, _mediator, ct);

        var warehouseIds = values.Where(x => x.DestinationWarehouseId is not null && x.DestinationWarehouseId > 0).Select(x => x.DestinationWarehouseId).ToList();
        warehouseIds.AddRange(values.Where(x => x.Managements is not null && x.Managements.Any())
            .SelectMany(x => x.Managements!.Where(x => x.DestinationWarehouseId is not null && x.DestinationWarehouseId > 0)).Select(x => x.DestinationWarehouseId).ToList());
        warehouseIds.AddRange(values.Where(x => x.Managements is not null && x.Managements.Any())
            .SelectMany(x => x.Managements!.Where(x => x.WarehouseId is not null && x.WarehouseId > 0)).Select(x => x.WarehouseId).ToList());
        var newWarehouseIds = warehouseIds.Distinct().ToList();
        var warehouses = await WebServicesLogic.WarehousesDataReceiver(newWarehouseIds, _mediator, ct);

        var projectOperationDetailIds = values.Where(x => x.ProjectOperationDetailIds.HasAny()).SelectMany(x => x.ProjectOperationDetailIds).Distinct().ToList();
        if (projectOperationDetailIds.Count > 0)
        {
            var locationResponses = await _mediator.Send(new GetsLocationByProjectOperationDetailIdsQuery(projectOperationDetailIds), ct);
            var locations = locationResponses.Value?.Data;
            if (locations is not null)
                foreach (var value in values)
                {
                    var valueLocations = locations.Where(x => value.ProjectOperationDetailIds.Contains(x.ProjectOperationDetailId)).ToList();

                    value.ProjectOperationDetailNames = StringSeparator.JoinWithDash(valueLocations, x => x.PrivateName ?? " ");
                    value.ProjectOperationDetailCodes = StringSeparator.JoinWithDash(valueLocations, x => x.PrivateCode ?? " ");
                    value.ProjectOperationDetailDescriptions = StringSeparator.JoinWithDash(valueLocations, x => x.Description ?? " ");
                }
        }
        foreach (var item in values)
        {
            var creator = creators?.FirstOrDefault(m => m.UserId == item.CreatorId);
            var measurment = measurments?.FirstOrDefault(m => m.Id == item.ProjectOperationMeasureId);
            var currency = currencies?.FirstOrDefault(m => m.Id == item.CurrencyId);
            var product = products?.FirstOrDefault(m => m.Id == item.ProductId);
            var package = packagesInfo?.FirstOrDefault(x => x.Id.Equals(item.PackageId));
            var supplyer = metaInfos?.FirstOrDefault(x => x is not null && x.Id.Equals(item.SupplyerId));
            var buyer = metaInfos?.FirstOrDefault(x => x is not null && x.Id.Equals(item.BuyerId));
            var contractor = metaInfos?.FirstOrDefault(x => x is not null && x.Id.Equals(item.ContractorId));
            var warehouse = warehouses?.FirstOrDefault(x => x.Id.Equals(item.DestinationWarehouseId));
            var projectManager = metaInfos?.FirstOrDefault(x => x is not null && x.Id.Equals(item.ProjectManagerId));
            item.MeasurementName = measurment?.Name;
            item.Creator = creator?.FullName;
            item.CurrencyName = currency?.Name;
            item.ProductName = product?.Name;
            item.ProductCode = product?.Code;
            item.ProductBrand = product?.Brand?.Name;
            item.ProductBrandModel = product?.BrandModel?.Name;
            item.ProductTechnicalCode = product?.Description;
            item.ProductGroupId = product?.Group.Id;
            item.ProductGroupName = product?.Group.Name;
            item.ProductGroupCode = product?.Group.Code;
            item.ProductGroupMeasurementName = product?.Group.MeasureUnit.Name;
            item.PackageName = package?.Title;
            item.PackageQuantity = package?.Quantity;
            item.SupplyerFullName = supplyer?.FullName;
            item.BuyerFullName = buyer?.FullName;
            item.ContractorFullName = contractor?.FullName;
            item.DestinationWarehouseName = warehouse?.Name;
            item.ProjectManager = projectManager?.FullName;

            if (item.Managements is not null && item.Managements.Any())
            {
                foreach (var management in item.Managements!)
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
                    management.Brand = manageProduct?.Brand?.Name;
                    management.BrandModel = manageProduct?.BrandModel?.Name;
                }

                item.OperatorAppointmentName = string.Join(" - ", item.Managements.Select(x => x.OperatorAppointmentName).ToList());
            }
        }
        return new GetsRequestGoodsSupplyProductResponse(values ?? new List<GetsRequestGoodsSupplyProductModel>(0), responses!.Value!.RowCount!);
    }

    public async Task<Result<GetsRequestGoodsSupplyProductExcelExporterResponse?>> GetsRequestGoodsSupplyProductExcelExporter(GetsRequestGoodsSupplyProductExcelExporterRequest request, CT ct)
    {
        request.IsExcel = true;
        var result = await GetsRequestGoodsSupplyProduct(request.Adapt<GetsRequestGoodsSupplyProductRequest>(), ct);

        var values = result.Value!.Data;
        var excelProduct = values.Adapt<List<GetsRequestGoodsSupplyProductExcelExporterResponseModel>>();
        foreach (var item in excelProduct)
        {
            var value = values.FirstOrDefault(x => x.Id == item.Id);
            item.CreatedShamsi = TimeCalculator.ConvertToShamsi(value!.Created);
            item.DelivaryDeadLineShamsi = TimeCalculator.ConvertToShamsi(value!.DelivaryDeadLine);
            item.RequestedDateShamsi = TimeCalculator.ConvertToShamsi(value!.RequestedDate);
        }

        List<GetsRequestGoodsSupplyProductManagementExcelExporterModel>? excelManagement = [];
        foreach (var item in values)
            if (item.Managements is not null && item.Managements.Count > 0)
            {
                var managements = item.Managements.Adapt<List<GetsRequestGoodsSupplyProductManagementExcelExporterModel>>();
                foreach (var item1 in managements)
                {
                    var manage = item.Managements.FirstOrDefault(x => x.Id == item1.Id);
                    item1.RequestNumber = item.RequestNumber;
                    item1.AssignmentDateShamsi = TimeCalculator.ConvertToShamsi(manage!.AssignmentDate);
                }

                excelManagement.AddRange(managements);
            }

        var file = new FileContentResult(SupplyManagementExcels.RequestGoodsSupplyProductToExcel(excelProduct, excelManagement, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"RequestGoodsSupplyProducts-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsRequestGoodsSupplyProductExcelExporterResponse(file);
    }

    public async Task<Result<GetsRequestGoodsSupplyProductExcelEnumsResponse?>> GetsRequestGoodsSupplyProductExcelEnums(GetsRequestGoodsSupplyProductExcelEnumsRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<RequestGoodsSupplyProductExcelEnum>());
        return new GetsRequestGoodsSupplyProductExcelEnumsResponse(response);
    }

    public async Task<Result<DeleteRequestGoodsSupplyProductResponse?>> DeleteRequestGoodsSupplyProduct(DeleteRequestGoodsSupplyProductModelRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<DeleteRequestGoodsSupplyProductValidator, DeleteRequestGoodsSupplyProductModelRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteRequestGoodsSupplyProductResponse>(isValidRequest.Error!);

        RequestGoodsSupplyProduct? value = null;
        if (request.RequestGoodsSupplyProduct is not null)
            value = request.RequestGoodsSupplyProduct;
        else if (request.Id is not null && request.Id > 0)
        {
            var requestGoodsSupplyQuery = await _mediator.Send(new GetRequestGoodsSupplyProductByIdQuery(request.Id!.Value), ct);
            if (requestGoodsSupplyQuery.IsFailure)
                return Result.Failure<DeleteRequestGoodsSupplyProductResponse>(requestGoodsSupplyQuery.Error!);
            value = requestGoodsSupplyQuery.Value!;
        }
        else
            return Result.Failure<DeleteRequestGoodsSupplyProductResponse>(RequestGoodsSupplyDetailErrors.InValidRequestGoodsSupplyDetailId);

        if (!GSDSRules.AllowStatusForDelete.Any(x => x.Equals(value.Status)))
            return Result.Failure<DeleteRequestGoodsSupplyProductResponse>(RequestGoodsSupplyDetailErrors.InValidRequestGoodsSupplyDetailStatusForDelete);

        if (value.RequestGoodsSupply.RequestGoodsSupplyProducts.Count(x => x.Id != value.Id) <= 0)
        {
            var requestCommand = await _mediator.Send(new DeleteRequestGoodsSupplyCommand(value.RequestGoodsSupply.Id), ct);
            if (requestCommand.IsFailure)
                return Result.Failure<DeleteRequestGoodsSupplyProductResponse>(requestCommand.Error!);
        }
        else
        {
            var updateCommand = await _mediator.Send(new DeleteRequestGoodsSupplyProductCommand(value), ct);
            if (updateCommand.IsFailure)
                return Result.Failure<DeleteRequestGoodsSupplyProductResponse>(updateCommand.Error!);
        }

        //if (request.CheckAnotherData)
        //    if (updateCommand.Value!.RequestGoodsSupply.RequestGoodsSupplyProducts.All(x => x.IsDeleted == true))
        //        return Result.Failure<DeleteRequestGoodsSupplyProductResponse>(RequestGoodsSupplyDetailErrors.CanNotDelete);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteRequestGoodsSupplyProductResponse(true);
    }

    public async Task<Result<GetGoodsSupplyProductHistoryByIdResponse?>> GetGoodsSupplyProductHistoryById(GetGoodsSupplyProductHistoryByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetGoodsSupplyProductHistoryById, GetGoodsSupplyProductHistoryById:{Id},", request.Id);
        var isValidRequest = await request.IsValidAsync<GetGoodsSupplyProductHistoryByIdValidator, GetGoodsSupplyProductHistoryByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetGoodsSupplyProductHistoryByIdResponse>(isValidRequest.Error!);

        var query = await _mediator.Send(new GetGoodsSupplyProductHistoryByIdQuery(request.Id, request.PageIndex, request.PageSize), ct);
        if (query.IsFailure)
            return Result.Failure<GetGoodsSupplyProductHistoryByIdResponse>(query.Error!);
        if (query.Value is null || query.Value.Data is null || query.Value?.Data.Count <= 0)
            Result.Failure<GetGoodsSupplyProductHistoryByIdResponse>(RequestGoodsSupplyHistoryErrors.RequestGoodsSupplyHistoryWithIdNotFound);
        var histories = query.Value?.Data;

        var requestGoodsSupplyCreatorIds = histories!.Select(c => c.CreatorId).Distinct().ToList();
        var requestCreators = await WebServicesLogic.UserDataReceiver(requestGoodsSupplyCreatorIds, null, _mediator, ct);
        var responseDetail = histories.Adapt<List<GetGoodsSupplyProductHistoryByIdModel>>() ?? new List<GetGoodsSupplyProductHistoryByIdModel>(0);
        responseDetail.ForEach(oo =>
        {
            oo.Creator = requestCreators?.Where(c => c!.UserId.Equals(oo.CreatorId)).FirstOrDefault()?.FullName;
        });

        return new GetGoodsSupplyProductHistoryByIdResponse(responseDetail, query.Value?.RowCount);
    }

    public async Task<Result<GetsTotalPriceRequestGoodsSupplyProductResponse?>> GetsTotalPriceRequestGoodsSupplyProduct(GetsTotalPriceRequestGoodsSupplyProductRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsTotalPriceRequestGoodsSupplyProductValidator, GetsTotalPriceRequestGoodsSupplyProductRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsTotalPriceRequestGoodsSupplyProductResponse>(isValidRequest.Error!);

        List<long>? requestProductIds = await GetProductIdsForFilter(request.FilterProduct, request.ProductIds, ct);

        var responses = await _mediator.Send(new GetsTotalPriceRequestGoodsSupplyProductQuery(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            requestProductIds,
            request.CreatorIds,
            request.WarehouseIds,
            request.CityId,
            request.ProjectManagerId,
            request.Importances,
            request.Types,
            request.Statuses,
            request.RemoveStatuses,
            request.StartDate,
            request.EndDate,
            request.RequestNumber,
            request.FilterDescription,
            request.FilterPublicName,
            request.FilterOperationInfoName,
            request.FilterManagerDescription,
            request.FilterData,
            request.CustomerInvoiceNumber), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsTotalPriceRequestGoodsSupplyProductResponse>(responses.Error!);
        var values = responses.Value!;

        return new GetsTotalPriceRequestGoodsSupplyProductResponse(values);
    }


    public async Task<Result<GetGoodsSupplyDetailProductsResponse?>> GetGoodsSupplyProducts(GetGoodsSupplyDetailProductsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetGoodsSupplyDetailProductsValidator, GetGoodsSupplyDetailProductsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetGoodsSupplyDetailProductsResponse>(isValidRequest.Error!);

        var productGroupsQuery = await _mediator.Send(new GetsConsumableVolumeProductsForSupplyQuery(request.Type, request.ProjectOperationId, request.ProjectOperationDetailId,
        null, request.ContractorId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (productGroupsQuery.IsFailure || productGroupsQuery.Value is null || productGroupsQuery.Value.Data is null || productGroupsQuery.Value.Data.Count <= 0)
            return Result.Failure<GetGoodsSupplyDetailProductsResponse>(RequestGoodsSupplyErrors.RequestGoodsProductGroupsNotFound);

        var productGroups = productGroupsQuery.Value!.Data!.Where(x => !x.IsDeleted).ToList();
        var productGroupIds = productGroups.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup).Select(c => c.ProductGroupId).Distinct().ToList();
        var categoryIds = productGroups.Where(x => x.VolumeProductType == VolumeProductType.Category).Select(c => c.ProductGroupId).Distinct().ToList();

        List<long>? gIds = [];
        List<long>? cIds = [];
        List<FilteredGroup>? filteredGroups = [];
        if (productGroups.Any(x => x.VolumeProductType == VolumeProductType.ProductGroup))
        {
            var queryGroups = await _groupRepo.GetFilteredGroupsByIds(productGroupIds, request.GroupFilterData, request.ProdutFilterData, 1, productGroupIds.Count, ct);
            if (queryGroups is null || queryGroups.Count <= 0)
                return Result.Failure<GetGoodsSupplyDetailProductsResponse>(RequestGoodsSupplyErrors.RequestGoodsProductGroupsNotFound);
            filteredGroups = queryGroups.Adapt<List<FilteredGroup>>();
            gIds = filteredGroups.Select(x => x.Id).Distinct().ToList();
        }

        List<WarehouseCategory>? filteredCategories = [];
        List<FilteredGroupsModel>? groupsModels = [];
        if (productGroups.Any(x => x.VolumeProductType == VolumeProductType.Category))
        {
            var catIds = categoryIds.Adapt<List<long?>>();
            var queryCategories = await _categoryRepo.GetFilteredGroupsByIds(catIds.NullListed(x => x), request.GroupFilterData, request.ProdutFilterData, 1, catIds.Count(), ct);
            if (queryCategories is null || queryCategories.Count <= 0)
                return Result.Failure<GetGoodsSupplyDetailProductsResponse>(RequestGoodsSupplyErrors.RequestGoodsProductGroupsNotFound);
            filteredCategories = queryCategories.Adapt<List<WarehouseCategory>>();
            cIds = filteredCategories.Select(x => x.Id!.Value).Distinct().ToList();

            if (cIds is not null && cIds.Count > 0)
            {
                var groupsQuery = await _groupRepo.GetFilteredGroupByCatIds(cIds, request.GroupFilterData, request.ProdutFilterData, 0, 0, ct);
                if (groupsQuery is null || groupsQuery.Count <= 0)
                    return Result.Failure<GetGoodsSupplyDetailProductsResponse>(RequestGoodsSupplyErrors.RequestGoodsProductGroupsNotFound);
                groupsModels = groupsQuery;
            }
        }
        var cGroupIds = groupsModels.Select(x => x.Id).ToList();
        productGroups = productGroups.Where(x => cGroupIds.Contains(x.ProductGroupId) || gIds.Contains(x.ProductGroupId)).ToList();
        var totals = new List<CalculatProjectOperationDetailSupplyCounterModel>();
        foreach (var item in productGroups)
        {
            var total = TotalDataReceiver(item);
            if (totals.Any(x => x.ProductGroupId == total.ProductGroupId))
            {
                totals.Where(x => x.ProductGroupId == total.ProductGroupId).FirstOrDefault()!.ProjectOperationDetailId = null;
                totals.Where(x => x.ProductGroupId == total.ProductGroupId).FirstOrDefault()!.TotalEstimatedCount += total.TotalEstimatedCount;
                totals.Where(x => x.ProductGroupId == total.ProductGroupId).FirstOrDefault()!.ToleranceCount += total.ToleranceCount;
                totals.Where(x => x.ProductGroupId == total.ProductGroupId).FirstOrDefault()!.TotalRequestedCount += total.TotalRequestedCount;
                totals.Where(x => x.ProductGroupId == total.ProductGroupId).FirstOrDefault()!.TotalSupplyCount += total.TotalSupplyCount;
                totals.Where(x => x.ProductGroupId == total.ProductGroupId).FirstOrDefault()!.TotalDifferenceCount += total.TotalDifferenceCount;
                totals.Where(x => x.ProductGroupId == total.ProductGroupId).FirstOrDefault()!.TotalRemainedCount += total.TotalRemainedCount;
            }
            else
                totals.Add(total);
        }

        var costCenterWs = await _mediator.Send(new GetsCostCenterWarehouseByProjectOperationQuery(request.ProjectOperationId.Value), ct);
        var costCenterWarehouseIds = costCenterWs.Value;

        List<GetGoodsSupplyDetailProductsModel> groups = [];
        if (productGroups.Any(x => x.VolumeProductType == VolumeProductType.ProductGroup))
        {
            var selectedGroups = productGroups.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup && gIds.Contains(x.ProductGroupId)).ToList();
            long? warhouseId = null;
            Result<DataResult<List<GetFilteredWarehousesByGroupIdsModel>>?> getsWarehouse = null;
            if (costCenterWarehouseIds is not null && costCenterWarehouseIds.Any())
            {
                getsWarehouse = await _mediator.Send(new GetFilteredWarehousesByGroupIdsQuery(costCenterWarehouseIds, selectedGroups.Select(x => x.ProductGroupId).ToList(),
                    request.FilterData, 1, costCenterWarehouseIds.Count), ct);
                warhouseId = getsWarehouse.Value?.Data?.FirstOrDefault()?.Id;
            }

            foreach (var item in selectedGroups)
            {
                if (groups is not null && groups.Any(x => x.GroupId == item.ProductGroupId))
                {
                    groups.FirstOrDefault(x => x.GroupId == item.ProductGroupId)!.HaveMultiProjectOperationDetail = true;
                    continue;
                }

                var total = totals.Where(x => x.ProductGroupId == item.ProductGroupId)!.FirstOrDefault();
                var groupInfo = filteredGroups.Where(x => x.Id == item.ProductGroupId).FirstOrDefault();

                var group = new GetGoodsSupplyDetailProductsModel
                {
                    GroupId = item.ProductGroupId,
                    GroupName = groupInfo?.Name,
                    GroupCode = groupInfo?.Code,
                    Urls = groupInfo?.Urls,
                    MeasureUnitId = groupInfo?.MeasureUnitId,
                    GroupMeasure = groupInfo?.MeasureUnitName,
                    IsActive = groupInfo?.IsActive,
                    TotalEstimatedCount = Calculator.RoundingDecimalDTFII(total!.TotalEstimatedCount),
                    TolerancePercentage = Calculator.RoundingDecimalDTFII(total.TolerancePercentage),
                    ToleranceCount = Calculator.RoundingDecimalDTFII(total.ToleranceCount),
                    TotalRequestedCount = Calculator.RoundingDecimalDTFII(total.TotalRequestedCount),
                    TotalSupplyCount = total.TotalSupplyCount,
                    TotalDifferenceCount = total.TotalDifferenceCount,
                    TotalRemainedCount = total.TotalRemainedCount,
                    DestinationWarehouseId = warhouseId,
                    ProductType = VolumeProductType.ProductGroup,
                    AssetAccess = warhouseId is null ? false : true,
                };
                groups?.Add(group);
            }
        }

        if (productGroups.Any(x => x.VolumeProductType == VolumeProductType.Category))
        {
            var groupIds = groupsModels.Select(x => x.Id).Distinct().ToList();
            long? warhouseId = null;
            if (costCenterWarehouseIds is not null && costCenterWarehouseIds.Any())
            {
                var getsWarehouse = await _mediator.Send(new GetFilteredWarehousesByGroupIdsQuery(costCenterWarehouseIds, groupIds,
                    request.FilterData, 1, costCenterWarehouseIds.Count), ct);
                warhouseId = getsWarehouse.Value?.Data?.FirstOrDefault()?.Id;
            }

            foreach (var item in groupsModels)
            {
                if (groups is not null && groups.Any(x => x.GroupId == item.Id))
                {
                    groups.FirstOrDefault(x => x.GroupId == item.Id)!.HaveMultiProjectOperationDetail = true;
                    continue;
                }

                var total = totals.Where(x => x.ProductGroupId == item.CategoryId)!.FirstOrDefault();

                var group = new GetGoodsSupplyDetailProductsModel
                {
                    GroupId = item?.Id,
                    GroupName = item?.Name,
                    GroupCode = item?.Code,
                    Urls = item?.Urls,
                    MeasureUnitId = item?.MeasureUnitId,
                    GroupMeasure = item?.MeasureUnitName,
                    TotalEstimatedCount = Calculator.RoundingDecimalDTFII(total!.TotalEstimatedCount),
                    TolerancePercentage = Calculator.RoundingDecimalDTFII(total.TolerancePercentage),
                    ToleranceCount = Calculator.RoundingDecimalDTFII(total.ToleranceCount),
                    TotalRequestedCount = Calculator.RoundingDecimalDTFII(total.TotalRequestedCount),
                    TotalSupplyCount = total.TotalSupplyCount,
                    TotalDifferenceCount = total.TotalDifferenceCount,
                    TotalRemainedCount = total.TotalRemainedCount,
                    DestinationWarehouseId = warhouseId,
                    AssetAccess = warhouseId is null ? false : true,
                    CategoryId = item?.CategoryId,
                    ProductType = VolumeProductType.Category
                };
                groups?.Add(group);
            }
        }

        List<RequestGoodsSupplyManagement>? requestmanagments = [];
        if (productGroups.Any())
        {
            var productGroupsIds = productGroups.Select(x => x.Id).ToList();
            var managmentsQuery = await _mediator.Send(new GetsSupplyManagementByConsumableVolumesQuery(productGroupsIds), ct);
            if (managmentsQuery is not null && managmentsQuery.Value!.Count > 0)
                requestmanagments.AddRange(managmentsQuery.Value);
        }

        var queryProducts = await _productRepo.GetProductForDetailsModel(groups.NullListed(x => x.GroupId), request.ProdutFilterData, ct);
        foreach (var group in groups!)
        {
            var products = queryProducts.Where(x => x.GroupId == group.GroupId).ToList();
            if (products is null || products.Count <= 0)
                continue;

            var productIds = products.Select(c => c.Id).ToList();
            var totalsProduct = await _mediator.Send(new GetTotalSupplyByProductIdsQuery(productIds), ct);
            var totalSupply = totalsProduct!.Value!;
            var activeProducts = products.Where(x => x.IsActive is not null && x.IsActive.Value).ToList();

            var resultModelDetails = (from product in activeProducts
                                      join g in totalSupply on product.Id equals g.ProductId into gJoin
                                      from gResult in gJoin.DefaultIfEmpty()
                                      select new GetGoodsSupplyDetailProductsModelDetail
                                      {
                                          Id = product.Id,
                                          Name = product.Name,
                                          Code = product.Code,
                                          Urls = product.Urls,
                                          Description = product.Description,
                                          Measure = product.Measure,
                                          BrandId = product.BrandId,
                                          Brand = product.Brand,
                                          BrandModelId = product.BrandModelId,
                                          BrandModel = product.BrandModel,
                                          IsActive = product.IsActive,
                                          IsWastage = product.IsWastage,
                                          Features = product.Features,
                                          RequestedCount = requestmanagments.Count > 0 ? (float)requestmanagments.Where(x => x.ReferenceId == product.Id).Sum(x => x.RequestedCount) : 0,
                                          SupplyCount = requestmanagments.Count > 0 ? (float)requestmanagments.Where(x => x.ReferenceId == product.Id &&
                                            (x.Status == GoodsSupplyManagementStatus.CompleteSupply)).Sum(x => x.RequestedCount) : 0,
                                      }).ToList();

            resultModelDetails.ForEach(c =>
            {
                var supplyManagments = requestmanagments.Where(x => x.ReferenceId == c.Id && x.Status == GoodsSupplyManagementStatus.CompleteSupply).ToList();
                var commerceManagments = supplyManagments.Where(x => x.Type == GoodsSupplyManagementType.Commerce).ToList();
                var notCommerceManagments = supplyManagments.Where(x => x.Type != GoodsSupplyManagementType.Commerce).ToList();
                if (supplyManagments.Count > 0)
                    c.SupplyCount = commerceManagments.Sum(x => x.ConfirmedRequestCount is null ? 0 : (float)x.ConfirmedRequestCount) + notCommerceManagments.Sum(x => (float)x.RequestedCount);
            });

            group.Products = resultModelDetails;
        }
        return new GetGoodsSupplyDetailProductsResponse(groups ?? new List<GetGoodsSupplyDetailProductsModel>(0), groups == null ? 0 : groups.Count);
    }

    public async Task<Result<GetGoodsSupplyDetailProductsResponse?>> GetProjectGoodsSupplyProducts(
        GetGoodsSupplyDetailProductsRequest request, CT ct)
    {
        var projectId = request.ProjectId!.Value;

        var project = await _mediator.Send(
            new GetProjectByIdQuery(projectId), ct);

        if (project.IsBad())
            return project.Failure<GetGoodsSupplyDetailProductsResponse>()!;

        var projectProducts = await _projectProductRepositoy
            .GetProductGoodsSupplyByProjectId(projectId, ct);

        if (projectProducts is null || projectProducts.Count == 0)
            return Result.Failure<GetGoodsSupplyDetailProductsResponse>(
                RequestGoodsSupplyErrors.RequestGoodsProductGroupsNotFound);

        var categoryIds = projectProducts
            .Where(x =>
                !x.IsDeleted &&
                x.ProjectProductType == ProjectProductType.Category &&
                x.ProductCategoryId.HasValue)
            .Select(x => x.ProductCategoryId!.Value)
            .Distinct()
            .ToList();

        var costCenterId = project.Value!
            .ProjectCostCenters
            .FirstOrDefault()!
            .CostCenterId;

        var warehouses = await _mediator.Send(
            new GetsCostCenterWarehouseByCostCenterIdQuery(
                costCenterId,
                1,
                100),
            ct);

        if (warehouses.IsBad())
            return warehouses.Failure<GetGoodsSupplyDetailProductsResponse>()!;

        var warehouseIds = warehouses.Value?
            .Data?
            .Select(x => x.WarehouseId)
            .ToList();

        var categoryGroups = await _warehouseAssetRepo
            .GetFilteredGroupByCatAndWareIds(
                categoryIds,
                warehouseIds,
                request.GroupFilterData,
                request.FilterData,
                0,
                0,
                ct);

        var groupIds = projectProducts
            .Where(x =>
                !x.IsDeleted &&
                x.ProjectProductType == ProjectProductType.ProductGroup &&
                x.ProductGroupId.HasValue)
            .Select(x => x.ProductGroupId!.Value)
            .Distinct()
            .ToList();

        if (categoryGroups is not null)
        {
            var categoryGroupIds = categoryGroups
                .Select(x => x.Id);

            groupIds.AddRange(
                categoryGroupIds.Except(groupIds));
        }

        if (groupIds.Count == 0)
            return Result.Failure<GetGoodsSupplyDetailProductsResponse>(
                RequestGoodsSupplyErrors.RequestGoodsProductGroupsNotFound);

        var groupsQuery = await _groupRepo.GetFilteredGroupsByIds(
            groupIds,
            request.GroupFilterData,
            request.ProdutFilterData,
            1,
            groupIds.Count,
            ct);

        if (groupsQuery is null || groupsQuery.Count == 0)
            return Result.Failure<GetGoodsSupplyDetailProductsResponse>(
                RequestGoodsSupplyErrors.RequestGoodsProductGroupsNotFound);

        var validGroupIds = groupsQuery
            .Select(x => x.Id)
            .ToHashSet();

        groupIds = groupIds
            .Where(validGroupIds.Contains)
            .ToList();

        var groupsInfo = groupsQuery
            .ToDictionary(x => x.Id);

        var projectProductIds = projectProducts
            .Select(x => x.Id)
            .Distinct()
            .ToList();

        var supplyManagements =
            await _requestGoodsSupplyManagementRepository
                .GetsSupplyManagementByProjectProducts(projectProductIds, ct)
            ?? [];

        // برای جلوگیری از جستجوی چندباره روی محصولات پروژه
        var projectProductsByGroupId = projectProducts
            .Where(x =>
                x.ProjectProductType == ProjectProductType.ProductGroup &&
                x.ProductGroupId.HasValue)
            .GroupBy(x => x.ProductGroupId!.Value)
            .ToDictionary(
                x => x.Key,
                x => x.ToList());

        // برای جلوگیری از جستجوی چندباره روی تامین ها
        var supplyByReferenceId = supplyManagements
            .GroupBy(x => x.ReferenceId)
            .ToDictionary(
                x => x.Key,
                x => x.ToList());

        List<GetGoodsSupplyDetailProductsModel> groups = [];

        foreach (var groupId in groupIds)
        {
            groupsInfo.TryGetValue(
                groupId,
                out var groupInfo);

            projectProductsByGroupId.TryGetValue(
                groupId,
                out var projectGroupRows);

            projectGroupRows ??= [];

            var estimated = projectGroupRows
                .Sum(x => x.RequestQuantity);

            decimal requested = 0;
            decimal supplied = 0;

            foreach (var projectProduct in projectGroupRows)
            {
                if (!supplyByReferenceId.TryGetValue(
                        projectProduct.Id,
                        out var supplies))
                    continue;

                requested += supplies
                    .Sum(x => x.RequestedCount);

                supplied += supplies
                    .Where(x =>
                        x.Status == GoodsSupplyManagementStatus.CompleteSupply)
                    .Sum(x =>
                        x.ConfirmedRequestCount ??
                        x.RequestedCount);
            }

            groups.Add(new GetGoodsSupplyDetailProductsModel
            {
                GroupId = groupId,
                GroupName = groupInfo?.Name,
                GroupCode = groupInfo?.Code,
                Urls = groupInfo?.Urls,
                MeasureUnitId = groupInfo?.MeasureUnitId,
                GroupMeasure = groupInfo?.MeasureUnitName,

                TotalEstimatedCount =
                    Calculator.RoundingDecimalDTFII(estimated),

                TotalRequestedCount =
                    Calculator.RoundingDecimalDTFII(requested),

                TotalSupplyCount = supplied,
                TotalDifferenceCount = requested - supplied,
                TotalRemainedCount = estimated - supplied,

                AssetAccess = true,
                IsProjectSupply = true
            });
        }

        List<GetGoodsSupplyDetailProductsModelDetail> queryProducts = [];

        if (groupIds.Count > 0)
        {
            queryProducts =
                await _productRepo.GetProductForPOProductsDetailsModel(
                    groupIds,
                    request.ProdutFilterData,
                    ct);
        }

        // برای جلوگیری از Where روی کل محصولات به ازای هر گروه
        var productsByGroupId = queryProducts
            .Where(x => x.IsActive == true)
            .GroupBy(x => x.GroupId)
            .ToDictionary(
                x => x.Key,
                x => x.ToList());

        foreach (var group in groups)
        {
            productsByGroupId.TryGetValue(
                group.GroupId!.Value,
                out var products);

            products ??= [];

            projectProductsByGroupId.TryGetValue(
                group.GroupId!.Value,
                out var groupProjectProducts);

            var projectProduct = groupProjectProducts?
                .FirstOrDefault();

            group.ProductType = projectProduct is null
                ? VolumeProductType.Category
                : VolumeProductType.ProductGroup;

            foreach (var product in products)
            {
                if (!supplyByReferenceId.TryGetValue(
                        product.Id,
                        out var productSupplies))
                {
                    product.RequestedCount = 0;
                    product.SupplyCount = 0;
                    continue;
                }

                var requested = productSupplies
                    .Sum(x => x.RequestedCount);

                var supplied = productSupplies
                    .Where(x =>
                        x.Status == GoodsSupplyManagementStatus.CompleteSupply)
                    .Sum(x =>
                        x.ConfirmedRequestCount ??
                        x.RequestedCount);

                product.RequestedCount = (float)requested;
                product.SupplyCount = (float)supplied;
            }

            group.Products = products;
        }

        if (categoryGroups?.Any() == true)
        {
            var groupsById = groups
                .ToDictionary(x => x.GroupId);

            foreach (var item in categoryGroups)
            {
                if (groupsById.TryGetValue(
                        item.Id,
                        out var group))
                {
                    group.CategoryId = item.CategoryId;
                }
            }
        }

        // تعداد کل قبل از صفحه بندی
        var totalCount = groups.Count;

        var pageIndex = request.PageIndex <= 0
            ? 1
            : request.PageIndex;

        var pageSize = request.PageSize <= 0
            ? 20
            : request.PageSize;

        // صفحه بندی خروجی
        var pagedGroups = groups
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new GetGoodsSupplyDetailProductsResponse(
            pagedGroups,
            totalCount);
    }

    public async Task<Result<GetProjectOperationDetailsByRequestIdResponse?>> GetProjectOperationDetailsByRequestId(GetProjectOperationDetailsByRequestIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetProjectOperationDetailsByRequestIdValidator, GetProjectOperationDetailsByRequestIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectOperationDetailsByRequestIdResponse>(isValidRequest.Error!);

        var responses = await _mediator.Send(new GetProjectOperationDetailsByRequestIdQuery(request.ProjectOperationId, request.ProductGroupId, request.ProjectOperationDetailId, request.FilterData, request.ContractorId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetProjectOperationDetailsByRequestIdResponse>(responses.Error!);
        var values = responses.Value!.Data!.Where(x => (request.ProjectOperationDetailIds == null || request.ProjectOperationDetailIds.Contains(x.Id))).ToList();
        var ids = values.Select(c => c.Id).ToList();
        if (ids.Count == 1)
            return Result.Failure<GetProjectOperationDetailsByRequestIdResponse>(RequestGoodsSupplyErrors.ProjectOperationDetailJustOne);

        var getTotal = await _mediator.Send(new GetTotalSupplyByProjectOperationDetailIdsQuery(ids, request.ProductGroupId), ct);
        if (getTotal.IsFailure)
            return Result.Failure<GetProjectOperationDetailsByRequestIdResponse>(getTotal.Error!);
        var totalSupply = getTotal.Value!;

        var resultModel = values.Select(c => new GetProjectOperationDetailsByRequestIdModel
        {
            ProjectOperationDetailId = c.Id,
            PrivateName = c.OperationLocation.PrivateName,
            PrivateCode = c.OperationLocation.PrivateCode,
            PublicName = c.OperationLocation.PublicName,
            PublicCode = c.OperationLocation.PublicCode,
            EstimatedCount = CalculateEstimatedCount(c, request.ProductGroupId, totalSupply)
        }).ToList();

        var total = resultModel.Sum(c => c.EstimatedCount);
        var result = resultModel
            .GroupJoin(totalSupply, model => model.ProjectOperationDetailId, supply => supply.ProductOperationDetailId, (model, gJoin) => new { model, gJoin })
            .SelectMany(x => x.gJoin.DefaultIfEmpty(), (x, gResult) => new GetProjectOperationDetailsByRequestIdModel
            {
                ProjectOperationDetailId = x.model.ProjectOperationDetailId,
                PrivateName = x.model.PrivateName,
                PrivateCode = x.model.PrivateCode,
                PublicName = x.model.PublicName,
                PublicCode = x.model.PublicCode,
                EstimatedCount = x.model.EstimatedCount,
                ProvidedCount = gResult != null ? gResult.TotalSupplyCount : 0,
            }).ToList();

        // تنظیم مقدار RequestedCount بر اساس نوع تقسیم بندی
        result.ForEach(c => c.RequestedCount = CalculateRequestedCount(request.Type, request.RequestedCount, result.Count, c.EstimatedCount, total));

        return new GetProjectOperationDetailsByRequestIdResponse(result, responses.Value!.RowCount!);
    }

    public async Task<Result<GetsProjectOperationDetailDataResponse?>> GetsProjectOperationDetailData(GetsProjectOperationDetailDataRequest request, CT ct)
    {
        var values = new List<GetsProjectOperationDetailDataModel>();
        var result = await _mediator.Send(new GetsProjectOperationDetailDataQuery(
            request.ProjectOperationId, request.Type, request.ProductGroupId, request.ProjectOperationDetailId,
            request.FilterData, request.ContractorId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (result.IsBad()) return result.Failure<GetsProjectOperationDetailDataResponse>()!;
        var datas = result.Value!.Data!;

        if (request.Type == VolumeProductType.ProductGroup)
        {
            var ids = datas.Select(c => c.Id).ToList();
            var getTotal = await _mediator.Send(new GetTotalSupplyByProjectOperationDetailIdsQuery(ids, request.ProductGroupId), ct);
            if (getTotal.IsBad()) return getTotal.Failure<GetsProjectOperationDetailDataResponse>()!;

            foreach (var item in datas)
            {
                var supply = getTotal.Value!.Where(x => x.ProductOperationDetailId == item.Id).ToList();
                var consumableVolume = item.ConsumableVolumeProducts?.Where(x => x.ProductGroupId == request.ProductGroupId)?.FirstOrDefault();
                var data = new GetsProjectOperationDetailDataModel()
                {
                    ProjectOperationDetailId = item.Id,
                    PrivateName = item.OperationLocation.PrivateName,
                    PrivateCode = item.OperationLocation.PrivateCode,
                    PublicName = item.OperationLocation.PublicName,
                    PublicCode = item.OperationLocation.PublicCode,
                    TotalEstimatedCount = (float?)Math.Round(consumableVolume?.FinalValue ?? 0, 5),
                    TolerancePercentage = (float?)Math.Round(consumableVolume!.UnusedPercentage ?? 0, 5),
                    ToleranceCount = (float?)Math.Round((((float?)consumableVolume?.FinalValue / 100) * (float?)consumableVolume!.UnusedPercentage) ?? 0, 5),
                    TotalRequestedCount = (float?)Math.Round(supply.Sum(c => c.RequestedCount), 5),
                    TotalSupplyCount = (float?)Math.Round(supply.Sum(c => c.TotalSupplyCount), 5),
                    Description = item.Description,
                };
                var remainedCount = data.TotalEstimatedCount - data.TotalRequestedCount;
                if (remainedCount >= 0)
                    data.TotalRemainedCount = (float?)Math.Round(remainedCount ?? 0, 5);
                else
                {
                    var toleranceRemained = (data.TotalEstimatedCount + data.ToleranceCount) - data.TotalRequestedCount;
                    if (toleranceRemained >= 0)
                        data!.TotalRemainedCount = (float?)Math.Round(toleranceRemained ?? 0, 5);
                    else
                        data!.TotalRemainedCount = 0;
                }

                values.Add(data);
            }
        }
        else if (request.Type == VolumeProductType.Category)
        {
            var ids = datas.Select(c => c.Id).ToList();
            var getTotal = await _consumableVolumeProductRepository.GetTotalSupplyByProjectOperationDetailIdsAndCategoryIdAsync(ids, request.ProductGroupId, ct);
            if (getTotal is null)
                return Result.Failure<GetsProjectOperationDetailDataResponse>(ConsumableVolumeProductErrors.ProjectOperationDetailProductWithIdNotFound!);

            foreach (var item in datas)
            {
                var supply = getTotal.Where(x => x.ProductOperationDetailId == item.Id).ToList();
                var consumableVolume = item.ConsumableVolumeProducts?.Where(x => x.ProductGroupId == request.ProductGroupId)?.FirstOrDefault();
                var data = new GetsProjectOperationDetailDataModel()
                {
                    ProjectOperationDetailId = item.Id,
                    PrivateName = item.OperationLocation.PrivateName,
                    PrivateCode = item.OperationLocation.PrivateCode,
                    PublicName = item.OperationLocation.PublicName,
                    PublicCode = item.OperationLocation.PublicCode,
                    TotalEstimatedCount = (float?)Math.Round(consumableVolume?.FinalValue ?? 0, 5),
                    TolerancePercentage = (float?)Math.Round(consumableVolume!.UnusedPercentage ?? 0, 5),
                    ToleranceCount = (float?)Math.Round((((float?)consumableVolume?.FinalValue / 100) * (float?)consumableVolume!.UnusedPercentage) ?? 0, 5),
                    TotalRequestedCount = (float?)Math.Round(supply.Sum(c => c.RequestedCount), 5),
                    TotalSupplyCount = (float?)Math.Round(supply.Sum(c => c.TotalSupplyCount), 5),
                    Description = item.Description,
                };
                var remainedCount = data.TotalEstimatedCount - data.TotalRequestedCount;
                if (remainedCount >= 0)
                    data.TotalRemainedCount = (float?)Math.Round(remainedCount ?? 0, 5);
                else
                {
                    var toleranceRemained = (data.TotalEstimatedCount + data.ToleranceCount) - data.TotalRequestedCount;
                    if (toleranceRemained >= 0)
                        data!.TotalRemainedCount = (float?)Math.Round(toleranceRemained ?? 0, 5);
                    else
                        data!.TotalRemainedCount = 0;
                }

                values.Add(data);

            }
        }
        return new GetsProjectOperationDetailDataResponse(values, values.Count);
    }

    public async Task<Result<GetProjectDetailDataResponse?>> GetProjectDetailData(
    GetProjectDetailDataRequest request, CT ct)
    {
        var values = new List<GetProjectDetailDataModel>();

        var result = await _projectProductRepositoy.GetProductByProjectId(request.ProjectId, ct);
        if (result is null)
            return Result.Failure<GetProjectDetailDataResponse>(
                ProjectErrors.ProjectProductWithIdsNotFound);

        var datas = result;

        if (request.Type == ProjectProductType.ProductGroup)
        {
            var filtered = datas.Where(x => x.ProductGroupId == request.ProductGroupId).ToList();
            foreach (var item in filtered)
            {
                var data = new GetProjectDetailDataModel
                {
                    ProjectId = item.Project.Id,
                    ProjectProductId = item.Id,
                    Type = item.ProjectProductType,
                    ProductGroupId = item.ProductGroupId,
                    ProductCategoryId = item.ProductCategoryId,
                    ProjectName = item.Project.ProjectName,
                    ProjectCode = item.Project.ProjectCode,
                    TotalEstimatedCount = (float?)item.RequestQuantity,
                    TolerancePercentage = (float?)item.TolerancePercentage,
                    ToleranceCount = (float?)Math.Round(
                        ((decimal)item.RequestQuantity / 100) * item.TolerancePercentage, 5),
                    TotalRequestedCount = (float?)Math.Round(
                        item.RequestGoodsSupplyDetails
                            .Where(x => !GSDSRules.TotalSupply.Contains(x.Status))
                            .Sum(x => x.RequestedCount), 5),
                    TotalSupplyCount = (float?)Math.Round(
                        item.RequestGoodsSupplyDetails
                            .Where(x => GSDSRules.Completed.Contains(x.Status))
                            .Sum(x => x.RequestedCount), 5)
                };

                var remainedCount = data.TotalEstimatedCount - data.TotalRequestedCount;
                if (remainedCount >= 0)
                {
                    data.TotalRemainedCount =
                        (float?)Math.Round(remainedCount ?? 0, 5);
                }
                else
                {
                    var toleranceRemained =
                        (data.TotalEstimatedCount + data.ToleranceCount) - data.TotalRequestedCount;

                    data.TotalRemainedCount = toleranceRemained >= 0
                        ? (float?)Math.Round(toleranceRemained ?? 0, 5)
                        : 0;
                }

                values.Add(data);
            }
        }
        else if (request.Type == ProjectProductType.Category)
        {
            var filtered = datas.Where(x => x.ProductCategoryId == request.ProductGroupId).ToList();
            foreach (var item in filtered)
            {
                var data = new GetProjectDetailDataModel
                {
                    ProjectId = item.Project.Id,
                    ProjectProductId = item.Id,
                    Type = item.ProjectProductType,
                    ProductGroupId = item.ProductGroupId,
                    ProductCategoryId = item.ProductCategoryId,
                    ProjectName = item.Project.ProjectName,
                    ProjectCode = item.Project.ProjectCode,
                    TotalEstimatedCount = (float?)item.RequestQuantity,
                    TolerancePercentage = (float?)item.TolerancePercentage,
                    ToleranceCount = (float?)Math.Round(
                        ((decimal)item.RequestQuantity / 100) * item.TolerancePercentage, 5),
                    TotalRequestedCount = (float?)Math.Round(
                        item.RequestGoodsSupplyDetails
                            .Where(x => !GSDSRules.TotalSupply.Contains(x.Status))
                            .Sum(x => x.RequestedCount), 5),
                    TotalSupplyCount = (float?)Math.Round(
                        item.RequestGoodsSupplyDetails
                            .Where(x => GSDSRules.Completed.Contains(x.Status))
                            .Sum(x => x.RequestedCount), 5)
                };

                var remainedCount = data.TotalEstimatedCount - data.TotalRequestedCount;
                if (remainedCount >= 0)
                {
                    data.TotalRemainedCount =
                        (float?)Math.Round(remainedCount ?? 0, 5);
                }
                else
                {
                    var toleranceRemained =
                        (data.TotalEstimatedCount + data.ToleranceCount) - data.TotalRequestedCount;

                    data.TotalRemainedCount = toleranceRemained >= 0
                        ? (float?)Math.Round(toleranceRemained ?? 0, 5)
                        : 0;
                }

                values.Add(data);
            }
        }

        return new GetProjectDetailDataResponse(values, values.Count);
    }


    public async Task<Result<GetRequestGoodsSupplyDetailStatusResponse?>> GetRequestGoodsSupplyDetailStatus(GetRequestGoodsSupplyDetailStatusRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetRequestGoodsSupplyStatus, GetRequestGoodsSupplyStatus");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<GoodsSupplyDetailStatus>());

        if (request.Statuses?.Any() == true)
        {
            var codes = request.Statuses.Select(x => (int)x);
            response = response.Where(x => !codes.Contains(x.Code)).ToList();
        }
        return new GetRequestGoodsSupplyDetailStatusResponse(response);
    }

    public async Task<Result<GetsGoodsSupplyDetailStatusResponse?>> GetsGoodsSupplyDetailStatus(GetsGoodsSupplyDetailStatusRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<GoodsSupplyDetailStatus>());
        return new GetsGoodsSupplyDetailStatusResponse(response);
    }

    public async Task<Result<GetRequestGoodsSupplyDetailImportanceResponse?>> GetRequestGoodsSupplyDetailImportance(GetRequestGoodsSupplyDetailImportanceRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<GoodsSupplyDetailImportance>());
        return new GetRequestGoodsSupplyDetailImportanceResponse(response);
    }

    public async Task<Result<GetRequestGoodsDetailHistoryByIdResponse?>> GetRequestGoodsDetailHistoryById(GetRequestGoodsDetailHistoryByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestGoodsDetailHistoryByIdValidator, GetRequestGoodsDetailHistoryByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestGoodsDetailHistoryByIdResponse>(isValidRequest.Error!);

        var query = await _mediator.Send(new GetRequestGoodsDetailHistoryByIdQuery(request.Id, request.PageIndex, request.PageSize), ct);
        if (query.IsFailure)
            return Result.Failure<GetRequestGoodsDetailHistoryByIdResponse>(query.Error!);
        if (query.Value is null || query.Value.Data is null || query.Value?.Data.Count <= 0)
            Result.Failure<GetRequestGoodsDetailHistoryByIdResponse>(RequestGoodsSupplyHistoryErrors.RequestGoodsSupplyDetailHistoryWithIdNotFound);
        var histories = query.Value?.Data;

        var requestGoodsSupplyCreatorIds = histories!.Select(c => c.CreatorId).Distinct().ToList();
        requestGoodsSupplyCreatorIds.AddRange(histories!.Select(c => c.RequestGoodsSupplyDetail.CreatorId).Distinct().ToList());
        var requestCreators = await WebServicesLogic.UserDataReceiver(requestGoodsSupplyCreatorIds.Distinct().ToList(), null, _mediator, ct);

        var requestGoodsSupplyProductIds = histories!.Select(c => c.RequestGoodsSupplyDetail.ProductId).Distinct().ToList();
        var requestProducts = await WebServicesLogic.ProductsDataReceiver(requestGoodsSupplyProductIds.Distinct().ToList(), null, _mediator, _productRepo, ct);

        var responseDetail = histories.Adapt<List<GetRequestGoodsDetailHistoryByIdDetailModel>>() ?? new List<GetRequestGoodsDetailHistoryByIdDetailModel>(0);
        responseDetail.ForEach(oo =>
        {
            var history = histories!.FirstOrDefault(x => x.Id == oo.Id);
            oo.Creator = requestCreators?.Where(c => c?.UserId == oo.CreatorId).FirstOrDefault()?.FullName;
            oo.ProductId = history!.RequestGoodsSupplyDetail.ProductId;
            oo.ProductName = requestProducts?.FirstOrDefault(x => x.Id == history!.RequestGoodsSupplyDetail.ProductId)?.Name;
            oo.ProductCode = requestProducts?.FirstOrDefault(x => x.Id == history!.RequestGoodsSupplyDetail.ProductId)?.Code;
            oo.Created = TimeCalculator.ConvertToShamsi(history.Created);
            oo.DelivaryDeadLine = TimeCalculator.ConvertToShamsi(history.DelivaryDeadLine);
        });

        var history = histories!.FirstOrDefault();
        return new GetRequestGoodsDetailHistoryByIdResponse
        {
            CreatorId = history!.RequestGoodsSupplyDetail.CreatorId,
            ProductId = history!.RequestGoodsSupplyDetail.ProductId,
            Creator = requestCreators?.Where(c => c?.UserId == history.CreatorId).FirstOrDefault()?.FullName,
            ProductName = requestProducts?.FirstOrDefault(x => x.Id == history!.RequestGoodsSupplyDetail.ProductId)?.Name,
            ProductCode = requestProducts?.FirstOrDefault(x => x.Id == history!.RequestGoodsSupplyDetail.ProductId)?.Code,
            RequestGoodsSupplyDetaild = history.RequestGoodsSupplyDetail.Id,
            RequestNumber = history.RequestGoodsSupplyDetail.RequestGoodsSupply.SerialNumber + "-" + history.RequestGoodsSupplyDetail.RequestGoodsSupply.Id.ToString(),
            Data = responseDetail,
            RowCount = query.Value?.RowCount,
            RequestCount = history!.RequestGoodsSupplyDetail.RequestedCount,
        };
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////

    // متدهای کمکی برای محاسبه مقدارهای مورد نیاز
    private static decimal CalculateEstimatedCount(ProjectOperationDetail c, long productGroupId, List<GetTotalSupplyByProjectOperationDetailIdsModel>? totalSupply)
    {
        var consumableVolume = c.ConsumableVolumeProducts?.FirstOrDefault(x => x.ProductGroupId == productGroupId)?.FinalValue;
        var totalSupplyCount = totalSupply?.FirstOrDefault(x => x.ProductOperationDetailId.Equals(c.Id))?.RequestedCount;
        return (consumableVolume - totalSupplyCount) ?? 0;
    }

    private static decimal CalculateRequestedCount(DividerByProjectOperationDetailsType type, decimal requestedCount, int resultCount, decimal? estimatedCount, decimal? total)
    {
        return type switch
        {
            DividerByProjectOperationDetailsType.ByEqualDivision => Math.Round(requestedCount / resultCount, 5),
            DividerByProjectOperationDetailsType.ByOperationDetailNumber => Math.Round(((estimatedCount * requestedCount) / total) ?? 0, 5),
            DividerByProjectOperationDetailsType.ArbitraryDivision => Math.Round(requestedCount, 5),
            _ => throw new NotImplementedException(),
        };
    }

}
