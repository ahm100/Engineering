using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Currencies;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Organizations;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Categories;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Groups;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Packages;
using Engineering.Application.Configs;
using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetsConsumableVolumeProductsForSupply;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Application.Services.OperationInfoSeasons;
using Engineering.Application.Services.OperationInfoSeasons.Queries.GetOperationInfoSeasonById;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationById;
using Engineering.Application.Services.Projects.Queries.GetProjectById;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.CreateRequestGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.CreateRGSType;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.DeleteRequestGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.DeleteRGS;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.DivisionRequestGoodsSupplyTransferPrice;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.SetGoodsSupplyToCreated;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.SetOperationInfoSeasonToGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.UpdateRequestGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.UpdateRGSType;
using Engineering.Application.Services.RequestGoodsSupplies.Contracts.CreatePRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Contracts.UpdatePRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRequestGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRequestGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRGS;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSTypeId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFilteredRequestGoodsSuppliesReports;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrProducts;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrRGS;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceTypeHistory;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsHistoryById;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyById;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyCreators;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyProductGroups;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyProducts;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyStatus;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyType;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSById;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSTypeByRGSId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSupplyForManagement;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodSupplyRequester;
using Engineering.Application.Services.RequestGoodsSupplies.Models.PRGSupplyImport;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RequestGoodsSuppliesGroupDelete;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RGSStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RGSupplyDetailsStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RGSupplyImport;
using Engineering.Application.Services.RequestGoodsSupplies.Models.SetGoodsSupplyToCreated;
using Engineering.Application.Services.RequestGoodsSupplies.Models.SetOperationInfoSeasonToGoodsSupply;
using Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetDetailByRGSId;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetDetailByRGSTypeId;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetFilteredRequestGoodsSuppliesReports;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetFltrProducts;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetFltrRGS;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetReferenceTypeHistory;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsHistoryById;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyById;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyCreators;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyProductGroups;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyProducts;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRGSById;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRGSTypeByRGSId;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetsRequestGoodSupplyRequester;
using Engineering.Application.Services.RequestGoodsSupplyDetails;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.CreateRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.CreateRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.DeleteRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateProjectRequestGoodsSupplyDetail;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.UpdateRequestGoodsSupplyDetail;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Queries.GetMeasureunitById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredUsers;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.Synonyms.MetaData.Currencies;
using Engineering.Domain.Entities.Synonyms.MetaData.Organizations;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using MessageSender.ClientSdk.Messaging;
using MessageSender.ClientSdk.Messaging.Targets;
using MessageSender.ClientSdk.Services;
using Microsoft.Extensions.Options;

namespace Engineering.Application.Services.RequestGoodsSupplies;

public partial class RequestGoodsSupplyLogic : IRequestGoodsSupplyLogic
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RequestGoodsSupplyLogic> _logger;
    private readonly IUserInfoService _userInfoService;
    private readonly IUserProfileService _userProfileService;
    private readonly IRequestGoodsSupplyDetailLogic _requestGoodsSupplyDetailLogic;
    private readonly long _currenctUserId;
    private readonly IOperationInfoSeasonLogic _operationInfoSeasonLogic;
    private readonly long? _companyId;
    private readonly IRequestGoodsSupplyRepository _requestGoodsSupplyRepository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly IRequestGoodsSupplyProductRepository _requestGoodsSupplyProductRepository;
    private readonly IViewProductRepository _viewProductRepository;
    private readonly IProjectProductRepository _projectProductRepositoy;
    private readonly IProjectProductRepository _pProductRepo;
    private readonly IOperationInfoSeasonRepository _oISeasonRepositoy;
    private readonly IViewGroupRepository _viewGroupRepository;
    private readonly IViewCategoryRepository _viewCategoryRepository;
    private readonly IConsumptionStandardProductRepository _consumptionStandardProductRepository;
    private readonly IMessageRelay _relay;
    private readonly IViewOrganizationRepository _orgRepo;
    private readonly IViewPackageRepository _packageRepo;
    private readonly IViewCurrencyRepository _currRepo;
    public RequestGoodsSupplyLogic(
        IMediator mediator,
        IUnitOfWork unitOfWork,
        ILogger<RequestGoodsSupplyLogic> logger,
        IUserInfoService userInfoService,
        IUserProfileService userProfileService,
        IRequestGoodsSupplyDetailLogic requestGoodsSupplyDetailLogic,
        IViewCurrencyRepository currRepo,
        IOperationInfoSeasonLogic operationInfoSeasonLogic,
        IOptionsSnapshot<MessageSenderConfig> options, IHttpContextAccessor httpContextAccessor,
        IRequestGoodsSupplyRepository requestGoodsSupplyRepository,
        IRequestGoodsSupplyDetailRepository requestGoodsSupplyDetailRepository,
        IRequestGoodsSupplyProductRepository requestGoodsSupplyProductRepository,
        IViewProductRepository viewProductRepository,
        IProjectProductRepository pProductRepo,
        IOperationInfoSeasonRepository oISeasonRepositoy,
        IConsumptionStandardProductRepository consumptionStandardProductRepository,
        IViewGroupRepository viewGroupRepository,
        IProjectProductRepository projectProductRepository,
        IViewThirdPartyRepository thirdPartyRepo,
        IMessageRelay relay,
        IViewOrganizationRepository orgRepo,
        IViewPackageRepository packageRepo,
        IViewCategoryRepository viewCategoryRepository
        )
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _userProfileService = userProfileService;
        _requestGoodsSupplyDetailLogic = requestGoodsSupplyDetailLogic;
        _currenctUserId = _userProfileService.GetProfileInfo().UserId;
        _operationInfoSeasonLogic = operationInfoSeasonLogic;
        _currRepo = currRepo;
        _companyId = GetCompanyId();
        _requestGoodsSupplyRepository = requestGoodsSupplyRepository;
        _viewProductRepository = viewProductRepository;
        _projectProductRepositoy = projectProductRepository;
        _pProductRepo = pProductRepo;
        _viewGroupRepository = viewGroupRepository;
        _consumptionStandardProductRepository = consumptionStandardProductRepository;
        _oISeasonRepositoy = oISeasonRepositoy;
        _requestGoodsSupplyProductRepository = requestGoodsSupplyProductRepository;
        _thirdPartyRepo = thirdPartyRepo;
        _relay = relay;
        _orgRepo = orgRepo;
        _packageRepo = packageRepo;
        _viewCategoryRepository = viewCategoryRepository;
    }

    public async Task<Result<CreateRGSTypeResponse?>> CreateRGSType(
        CreateRGSTypeRequest request, CT ct)
    {
        _logger.LogInformation("CreateRGSType");
        var result = await _mediator.Send(new CreateRGSTypeCommand(request), ct);
        if (result.IsBad()) return result.Failure<CreateRGSTypeResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new CreateRGSTypeResponse(result.Value!.Id, true);
    }

    public async Task<Result<UpdateRGSTypeResponse?>> UpdateRGSType(
        UpdateRGSTypeRequest request, CT ct)
    {
        _logger.LogInformation("UpdateRGSType");
        var result = await _mediator.Send(new UpdateRgsTypeCommand(request), ct);
        if (result.IsBad()) return result.Failure<UpdateRGSTypeResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return result;
    }

    public async Task<Result<DeleteRGSResponse?>> DeleteRGS(
        DeleteRGSRequest request, CT ct)
    {
        _logger.LogInformation("DeleteRGS");
        var result = await _mediator.Send(new DeleteRGSCommand(request.Id), ct);
        if (result.IsBad()) return result.Failure<DeleteRGSResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return result;
    }

    public async Task<Result<RGSStatusChangerResponse?>> RGSStatusChanger(
        RGSStatusChangerRequest request, CT ct)
    {
        _logger.LogInformation("RGSStatusChanger");
        var result = await _mediator.Send(new RGSStatusChangerCommand(request.RequestGoodsSupplyId,
            request.Status,
            request.Description), ct);
        if (result.IsBad()) return result.Failure<RGSStatusChangerResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return result;
    }

    public async Task<Result<CreateRequestGoodsSupplyResponse?>> CreateRequestGoodsSupply(
        CreateRequestGoodsSupplyRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateRequestGoodsSupply, CreateRequestGoodsSupply:{GoodsSupplyType},", request.Type);
        var isValidRequest = await request.IsValidAsync<CreateRequestGoodsSupplyValidator, CreateRequestGoodsSupplyRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateRequestGoodsSupplyResponse>(isValidRequest.Error!);

        var validate = await CreateRequestGoodsSupplyValidators(new(request), ct);
        if (validate.IsFailure)
            return Result.Failure<CreateRequestGoodsSupplyResponse>(validate.Error!);
        var validateValue = validate.Value!;

        if (request.Type == GoodsSupplyType.PurchaseForContractor && request.Details.Any(x => x.ContractorId == null))
            return Result.Failure<CreateRequestGoodsSupplyResponse>(RequestGoodsSupplyErrors.ContractorIdCantBeNull);

        var requestingOrganizationId = await GetCurrentUserOrganizationId(ct);

        using (var transaction = new CreateTransaction())
        {
            var response = await _mediator.Send(new CreateRequestGoodsSupplyCommand(
                validateValue.ProjectOperation,
                validateValue.ProjectOperationDetail,
                validateValue.OperationInfoSeason!,
                request.Type,
                request.IsDraft,
                request.SupplyerId,
                request.BuyerId,
                request.CurrencyId,
                requestingOrganizationId,
                request.TransferPrice,
                request.OtherPrice,
                request.DiscountOnInvoicePercentage,
                request.DiscountOnInvoiceNumber,
                null,
                request.TaxOnInvoicePercentage,
                request.TaxOnInvoiceNumber,
                null,
                request.RequestedDate,
                request.IsPettyCash,
                request.Description,
                _companyId,
                request.ConsumptionRateAndInventoryUrl,
                request.ConsumptionAddress,
                request.PurchaseLocation,
                request.PurchaseReason), ct);
            if (response.IsFailure)
                return Result.Failure<CreateRequestGoodsSupplyResponse>(response.Error!);
            var value = response!.Value!;

            var responseDetails = await _requestGoodsSupplyDetailLogic.CreateRequestGoodsDetailSupply(new(null, value, request.Details), ct);
            if (responseDetails.IsFailure)
                return Result.Failure<CreateRequestGoodsSupplyResponse>(responseDetails.Error!);

            if (request.TransferPrice is not null && request.TransferPrice > 0)
            {
                var responseDivision = await _mediator.Send(new DivisionRequestGoodsSupplyTransferPriceCommand(value), ct);
                if (responseDivision.IsFailure)
                    return Result.Failure<CreateRequestGoodsSupplyResponse>(responseDivision.Error!);
            }

            await _unitOfWork.CommitAsync(ct);

            var projectManagerId = await _requestGoodsSupplyRepository.GetProjectManagerId(value.Id, ct);

            var thirdParty = await _thirdPartyRepo.GetById(projectManagerId.Value, ct);
            try
            {
                await _relay.Send(new MessageEnvelope(
                    MessageSender.ClientSdk.Enums.MessageChannels.Inbox,

                    new TemplatedMessage(Guid.NewGuid().ToString(), "engineering-set-operator", new Dictionary<string, object?>
                    {
                    { "FullName", thirdParty?.FirstName + " " + thirdParty?.LastName },
                    { "RequestNumber", value.RequestSerialNumber },
                    })

                    , new UserTarget(thirdParty.UserId.Value)), ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to send notification for request {RequestNumber}",
                    value.RequestSerialNumber);
            }
            transaction.Complete();
            return response.Value!.Adapt<CreateRequestGoodsSupplyResponse>();
        }

    }

    public async Task<Result<RGSupplyImportResponse?>> RGSupplyImport(
        RGSupplyImportRequest request, CT ct)
    {
        _logger.LogInformation("RGSupplyImport");
        var isValidRequest = await request.IsValidAsync<RGSupplyImportValidator, RGSupplyImportRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<RGSupplyImportResponse>(isValidRequest.Error!);

        using (var stream = request.DocumentFile.OpenReadStream())
        {
            var missingHeaders = ExcelHelpers.GetMissingHeaders<RGSupplyImportModel>(stream);
            if (missingHeaders.Any())
                return Result.Failure<RGSupplyImportResponse>(
                    RequestGoodsSupplyErrors.InvalidColumnNames(missingHeaders.JoinList()));
        }

        var projectOperationData = await _mediator.Send(new GetProjectOperationByIdQuery(request.ProjectOperationId), ct);
        if (projectOperationData.IsBad())
            return projectOperationData.Failure<RGSupplyImportResponse>()!;
        var projectOperation = projectOperationData.Value;

        if (!projectOperation.ProjectOperationDetails.Any() ||
            (request.ProjectOperationDetailId != null &&
            projectOperation.ProjectOperationDetails.Any(x => x.Id != request.ProjectOperationDetailId)))
            return Result.Failure<RGSupplyImportResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithFilterNotFound);

        var oInfoSeason = await _oISeasonRepositoy.GetOperationInfoSeasonById(request.OperationInfoSeasonId, ct);
        if (oInfoSeason is null)
            return Result.Failure<RGSupplyImportResponse>(OperationInfoSeasonErrors.OINotAssignedToSeason);

        var rGS = ExcelImporter.Import<RGSupplyImportModel>(request.DocumentFile);
        if (rGS is null)
            return Result.Failure<RGSupplyImportResponse>(GlobalErrors.ErrorOnReadFile)!;

        var validate = await ValidateRGSupply(rGS, projectOperation, ct);

        if (validate.IsBad() || validate.Value.result!.IsDone == false)
            return validate.Value.result!;

        var requestingOrganizationId = await GetCurrentUserOrganizationId(ct);


        using (var transaction = new CreateTransaction())
        {
            var values = validate.Value;

            foreach (var item in rGS)
            {
                var product = values.products!.FirstOrDefault(x => x.Code == item.ProductCode);
                var response = await _mediator.Send(new CreateRequestGoodsSupplyCommand(
                    projectOperation,
                    null,
                    oInfoSeason,
                    GoodsSupplyType.GoodsSupply,
                    false,
                    null,
                    null,
                    null,
                    requestingOrganizationId,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    item.InputDate,
                    null,
                    item.Description,
                    _companyId,
                    null,
                    null,
                    null,
                    null), ct);
                if (response.IsFailure)
                    return Result.Failure<RGSupplyImportResponse>(response.Error!);
                var value = response!.Value!;

                var detail = new CreateRequestGoodsSupplyDetailModel
                {
                    ProjectOperationDetailId = request.ProjectOperationDetailId ?? projectOperation.ProjectOperationDetails.FirstOrDefault()!.Id,
                    ProductId = product.Id,
                    ProductGroupId = product.GroupId,
                    RequestedCount = item.Count,
                    Importance = GoodsSupplyDetailImportance.Medium,
                    CheckGroup = false
                };
                var responseDetails = await _requestGoodsSupplyDetailLogic.CreateRequestGoodsDetailSupply(new(null, value, [detail]), ct);
                if (responseDetails.IsFailure)
                    return Result.Failure<RGSupplyImportResponse>(responseDetails.Error!);
            }
            transaction.Complete();

            await _unitOfWork.CommitAsync(ct);

            return new RGSupplyImportResponse(true, null);
        }

    }

    public async Task<Result<CreateProjectRequestGoodsSuppliesResponse?>> CreateProjectRequestGoodsSupply(
        CreateProjectRequestGoodsSuppliesRequest request, CT ct)
    {
        using (var transaction = new CreateTransaction())
        {
            var companyId = CompanyValidator.GetCompanyId(_userInfoService);
            if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
                return Result.Failure<CreateProjectRequestGoodsSuppliesResponse>(GlobalErrors.InvalidCompany);
            var project = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId));
            if (project.IsBad())
                return project.Failure<CreateProjectRequestGoodsSuppliesResponse>()!;
            var requestingOrganizationId = await GetCurrentUserOrganizationId(ct);
            var response = await CreatePRequestGoodsSuppliesHandler(request, project.Value, companyId, requestingOrganizationId, ct);
            if (response.IsBad())
                return response.Failure<CreateProjectRequestGoodsSuppliesResponse>()!;
            var value = response!.Value!;

            var pRequest = new CreateProjectRequestGoodsSupplyDetailModelRequest(
                null,
                value,
                request.Details
            );
            var responseDetails = await _requestGoodsSupplyDetailLogic.CreateProjectRequestGoodsDetailSupply(pRequest, request.ProjectId, ct);
            if (responseDetails.IsFailure)
                return Result.Failure<CreateProjectRequestGoodsSuppliesResponse>(responseDetails.Error!);

            if (request.TransferPrice is not null && request.TransferPrice > 0)
            {
                var responseDivision = await _mediator.Send(new DivisionRequestGoodsSupplyTransferPriceCommand(value), ct);
                if (responseDivision.IsFailure)
                    return responseDivision.Failure<CreateProjectRequestGoodsSuppliesResponse>()!;
            }

            await _unitOfWork.CommitAsync(ct);

            var projectManagerId = await _requestGoodsSupplyRepository.GetProjectManagerId(value.Id, ct);

            var thirdParty = await _thirdPartyRepo.GetById(projectManagerId.Value, ct);

            try
            {
                await _relay.Send(new MessageEnvelope(
                    MessageSender.ClientSdk.Enums.MessageChannels.Inbox,

                    new TemplatedMessage(Guid.NewGuid().ToString(), "engineering-set-operator", new Dictionary<string, object?>
                    {
                    { "FullName", thirdParty?.FirstName + " " + thirdParty?.LastName },
                    { "RequestNumber", value.RequestSerialNumber },
                    })

                    , new UserTarget(thirdParty.UserId.Value)), ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to send notification for request {RequestNumber}",
                    value.RequestSerialNumber);
            }
            transaction.Complete();
            return response.Value!.Adapt<CreateProjectRequestGoodsSuppliesResponse>();
        }
    }

    public async Task<Result<PRGSupplyImportResponse?>> PRGSupplyImport(
        PRGSupplyImportRequest request, CT ct)
    {
        using (var transaction = new CreateTransaction())
        {
            var companyId = CompanyValidator.GetCompanyId(_userInfoService);
            if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
                return Result.Failure<PRGSupplyImportResponse>(GlobalErrors.InvalidCompany);

            using (var stream = request.DocumentFile.OpenReadStream())
            {
                var missingHeaders = ExcelHelpers.GetMissingHeaders<PRGSupplyImportModel>(stream);
                if (missingHeaders.Any())
                    return Result.Failure<PRGSupplyImportResponse>(
                        RequestGoodsSupplyErrors.InvalidColumnNames(missingHeaders.JoinList()));
            }

            var projectData = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId), ct);
            if (projectData.IsBad())
                return projectData.Failure<PRGSupplyImportResponse>()!;
            var project = projectData.Value;

            if (!project!.HasProduct)
                return Result.Failure<PRGSupplyImportResponse>(ProjectErrors.HasProductIsFalse)!;

            var pRGS = ExcelImporter.Import<PRGSupplyImportModel>(request.DocumentFile);
            if (pRGS is null)
                return Result.Failure<PRGSupplyImportResponse>(GlobalErrors.ErrorOnReadFile)!;

            var validate = await ValidatePRGSupply(pRGS, project.Id, ct);
            if (validate.IsBad() || validate.Value.result!.IsDone == false)
                return validate.Value.result!;

            var cmnd = new CreateProjectRequestGoodsSuppliesRequest
            {
                ProjectId = project.Id,
                Type = GoodsSupplyType.GoodsSupply,
                IsDraft = request.IsDraft,
                RequestedDate = pRGS.FirstOrDefault()?.InputDate,
                Description = pRGS.FirstOrDefault()?.Description,
                Details = null,
            };

            var requestingOrganizationId = await GetCurrentUserOrganizationId(ct);

            var response = await CreatePRequestGoodsSuppliesHandler(cmnd, project, companyId, requestingOrganizationId, ct);
            if (response.IsBad())
                return response.Failure<PRGSupplyImportResponse>()!;
            var value = response!.Value!;

            var products = validate.Value.products;
            foreach (var item in pRGS)
            {
                var product = products!.FirstOrDefault(x => item.ProductCode == x.Code);

                var detailsCmnd = new CreateProjectRequestGoodsSupplyDetailModel
                {
                    ProjectId = project.Id,
                    ProductId = product.Id,
                    ProductGroupId = product.GroupId,
                    RequestedCount = item.Count,
                    Importance = GoodsSupplyDetailImportance.Medium,
                    CheckGroup = false,
                };

                var pRequest = new CreateProjectRequestGoodsSupplyDetailModelRequest(
                    null,
                    value,
                    [detailsCmnd]
                );

                var responseDetails = await _requestGoodsSupplyDetailLogic.CreateProjectRequestGoodsDetailSupply(pRequest, project.Id, ct);
                if (responseDetails.IsFailure)
                    return Result.Failure<PRGSupplyImportResponse>(responseDetails.Error!);
            }

            await _unitOfWork.CommitAsync(ct);

            transaction.Complete();
            return new PRGSupplyImportResponse(true, null);
        }
    }

    public async Task<Result<UpdateRequestGoodsSupplyResponse?>> UpdateRequestGoodsSupply(
        UpdateRequestGoodsSupplyRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateRequestGoodsSupply, UpdateRequestGoodsSupply:{Id},", request.RequestGoodsSupplyId);
        using (var transaction = new CreateTransaction())
        {
            var isValidRequest = await request.IsValidAsync<UpdateRequestGoodsSupplyValidator, UpdateRequestGoodsSupplyRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<UpdateRequestGoodsSupplyResponse>(isValidRequest.Error!);

            var goodsSupplyResponse = await _mediator.Send(new GetRequestGoodsSupplyByIdQuery(request.RequestGoodsSupplyId), ct);
            if (goodsSupplyResponse.IsFailure)
                return Result.Failure<UpdateRequestGoodsSupplyResponse>(goodsSupplyResponse.Error!);
            var value = goodsSupplyResponse.Value!;

            var lastDescription = await DescriptionMacker(request.Description, GoodsSupplyStatus.Created, ct);
            if (request.Details.Count <= 0)
                return Result.Failure<UpdateRequestGoodsSupplyResponse>(RequestGoodsSupplyErrors.DetailsIsNull);
            else if (request.Details.Where(x => !x.IsDeleted).Count() <= 0)
                return Result.Failure<UpdateRequestGoodsSupplyResponse>(RequestGoodsSupplyErrors.CanNotDeleteDetails);

            var deletedModel = CheckDeletedData(request.Details);
            if (deletedModel.deletedIds is not null && deletedModel.deletedIds.Count > 0)
                foreach (var deletedId in deletedModel.deletedIds)
                {
                    var entityDetail = value.RequestGoodsSupplyDetails.Where(x => x.Id == deletedId).FirstOrDefault();
                    if (entityDetail is null)
                        return Result.Failure<UpdateRequestGoodsSupplyResponse>(RequestGoodsSupplyDetailErrors.RequestGoodsSupplyDetailWithIdNotFound);

                    var haveAnyForAdd = request.Details.Any(x => x.ProductId == entityDetail.ProductId && x.IsDeleted == false);
                    var responseDetails = await _requestGoodsSupplyDetailLogic.DeleteRequestGoodsSupplyDetail(new DeleteRequestGoodsSupplyDetailModelRequest(null, entityDetail, request.Details.Count, deletedModel.checkData, haveAnyForAdd), ct);
                    if (responseDetails.IsFailure)
                        return Result.Failure<UpdateRequestGoodsSupplyResponse>(responseDetails.Error!);
                }

            var requestDetails = request.Details.Where(x => !x.IsDeleted).ToList();
            var validationResult = await ValidateRequestGoodsSupplyDetails(requestDetails, ct);
            if (validationResult.IsFailure)
                return Result.Failure<UpdateRequestGoodsSupplyResponse>(validationResult.Error!);

            List<RequestGoodsSupplyProduct>? products = value.RequestGoodsSupplyProducts.ToList();

            var updateList = requestDetails.Where(c => c.RequestGoodsSupplyDetailId.HasValue && !c.IsDeleted).ToList();
            foreach (var update in updateList)
            {
                var updateRequest = update!.Adapt<UpdateRequestGoodsSupplyDetailRequest>();
                var responseDetails = await _requestGoodsSupplyDetailLogic.UpdateRequestGoodsDetailSupply(updateRequest, ct);
                if (responseDetails.IsFailure)
                    return Result.Failure<UpdateRequestGoodsSupplyResponse>(responseDetails.Error!);
            }

            var newList = requestDetails.Where(c => c.RequestGoodsSupplyDetailId == null).ToList();
            foreach (var item in newList)
            {
                var packagesQuery = await _packageRepo.GetByGroupId(item!.ProductGroupId, ct);
                if (packagesQuery is null)
                    return Result.Failure<UpdateRequestGoodsSupplyResponse>(RequestGoodsSupplyDetailErrors.ProductNoHavePackage);
                if (item.PackageId is not null)
                {
                    var pckageValidate = packagesQuery!.Where(x => x.Id == item.PackageId)?.FirstOrDefault();
                    if (pckageValidate is null)
                        return Result.Failure<UpdateRequestGoodsSupplyResponse>(RequestGoodsSupplyDetailErrors.PackageIdNotValidate);
                }
                else
                {
                    var isDefaultPckage = packagesQuery!.Where(x => x.IsDefault)?.FirstOrDefault();
                    item.PackageId = isDefaultPckage?.Id;
                    if (isDefaultPckage is null)
                        item.PackageId = packagesQuery!.FirstOrDefault()?.Id;
                    if (item.PackageId is null)
                        return Result.Failure<UpdateRequestGoodsSupplyResponse>(RequestGoodsSupplyDetailErrors.PackageIdNotFound);
                }

                RequestGoodsSupplyProduct? product = null;
                if (!products.Any(x => x.ProductId.Equals(item.ProductId)))
                {
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
                        item.PackageUnitPrice,
                        item.CheckGroup,
                        item.ContractorId,
                        item.DestinationWarehouseId,
                        item.CustomerInvoiceNumber,
                        item.Description,
                        item.ManagementDescription), ct);
                    if (productResponse.IsFailure)
                        return Result.Failure<UpdateRequestGoodsSupplyResponse?>(productResponse.Error!);
                    product = productResponse.Value!;
                    products.Add(product);
                }
                else
                {
                    product = products.FirstOrDefault(x => x.ProductId.Equals(item.ProductId))!;
                }
                var newRequest = item!.Adapt<CreateRequestGoodsSupplyDetailModel>();
                var responseDetails = await _requestGoodsSupplyDetailLogic.ProcessCreateRequestDetail(newRequest, value, product, ct);
                if (responseDetails.IsFailure)
                    return Result.Failure<UpdateRequestGoodsSupplyResponse>(responseDetails.Error!);
            }

            OperationInfoSeason? operationInfoSeason = null;
            if (request.OperationInfoSeasonId > 0)
            {
                var operationInfoSeasonQuery = await _mediator.Send(new GetOperationInfoSeasonByIdQuery(request.OperationInfoSeasonId), ct);
                if (operationInfoSeasonQuery.IsFailure)
                    return Result.Failure<UpdateRequestGoodsSupplyResponse>(operationInfoSeasonQuery.Error!);
                operationInfoSeason = operationInfoSeasonQuery.Value!;
            }

            var updated = await _mediator.Send(new UpdateRequestGoodsSupplyCommand(
                value,
                operationInfoSeason!,
                request.SupplyerId,
                request.BuyerId,
                request.CurrencyId,
                request.TransferPrice,
                request.OtherPrice,
                request.DiscountOnInvoicePercentage,
                request.DiscountOnInvoiceNumber,
                null,
                request.TaxOnInvoicePercentage,
                request.TaxOnInvoiceNumber,
                null,
                request.IsPettyCash,
                request.Description,
                request.RequestedDate,
                request.ConsumptionRateAndInventoryUrl,
                request.ConsumptionAddress,
                request.PurchaseLocation,
                request.PurchaseReason,
                request.IsDraft
                ), ct);
            if (updated.IsFailure)
                return Result.Failure<UpdateRequestGoodsSupplyResponse>(updated.Error!);

            if ((request.TransferPrice is not null && request.TransferPrice > 0) || (value.RequestGoodsSupplyProducts.Any(x => x.TransferPrice is not null || x.TransferPrice > 0)))
            {
                var responseDivision = await _mediator.Send(new DivisionRequestGoodsSupplyTransferPriceCommand(value), ct);
                if (responseDivision.IsFailure)
                    return Result.Failure<UpdateRequestGoodsSupplyResponse>(responseDivision.Error!);
            }

            await _unitOfWork.CommitAsync(ct);
            transaction.Complete();
            return new UpdateRequestGoodsSupplyResponse(true);
        }
    }

    public async Task<Result<UpdateProjectRequestGoodsSuppliesResponse?>> UpdateProjectRequestGoodsSupply(
        UpdateProjectRequestGoodsSuppliesRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateRequestGoodsSupply, UpdateRequestGoodsSupply:{Id},", request.RequestGoodsSupplyId);
        using (var transaction = new CreateTransaction())
        {
            var goodsSupplyResponse = await _mediator.Send(new GetRequestGoodsSupplyByIdQuery(request.RequestGoodsSupplyId), ct);
            if (goodsSupplyResponse.IsFailure)
                return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse>(goodsSupplyResponse.Error!);
            var value = goodsSupplyResponse.Value!;

            var lastDescription = await DescriptionMacker(request.Description, GoodsSupplyStatus.Created, ct);
            if (request.Details.Count <= 0)
                return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse>(RequestGoodsSupplyErrors.DetailsIsNull);
            else if (request.Details.Where(x => !x.IsDeleted).Count() <= 0)
                return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse>(RequestGoodsSupplyErrors.CanNotDeleteDetails);

            var deletedModel = CheckProjectDeletedData(request.Details);
            if (deletedModel.deletedIds is not null && deletedModel.deletedIds.Count > 0)
                foreach (var deletedId in deletedModel.deletedIds)
                {
                    var entityDetail = value.RequestGoodsSupplyDetails.Where(x => x.Id == deletedId).FirstOrDefault();
                    if (entityDetail is null)
                        return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse>(RequestGoodsSupplyDetailErrors.RequestGoodsSupplyDetailWithIdNotFound);

                    var haveAnyForAdd = request.Details.Any(x => x.ProductId == entityDetail.ProductId && x.IsDeleted == false);
                    var responseDetails = await _requestGoodsSupplyDetailLogic.DeleteRequestGoodsSupplyDetail(new DeleteRequestGoodsSupplyDetailModelRequest(null, entityDetail, request.Details.Count, deletedModel.checkData, haveAnyForAdd), ct);
                    if (responseDetails.IsFailure)
                        return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse>(responseDetails.Error!);
                }

            var requestDetails = request.Details.Where(x => !x.IsDeleted).ToList();
            var projectId = value.ProjectId;
            var validationResult = await ValidateProjectRequestGoodsSupplyDetails(requestDetails, ct);
            if (validationResult.IsFailure)
                return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse>(validationResult.Error!);

            List<RequestGoodsSupplyProduct>? products = value.RequestGoodsSupplyProducts.ToList();

            var project = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId), ct);
            var updateList = requestDetails.Where(c => c.RequestGoodsSupplyDetailId.HasValue && !c.IsDeleted).ToList();
            foreach (var update in updateList)
            {
                var updateRequest = update!.Adapt<UpdateProjectRequestGoodsSupplyDetailRequest>();
                var responseDetails = await _requestGoodsSupplyDetailLogic.UpdateProjectRequestGoodsDetailSupply(updateRequest, project.Value!, ct);
                if (responseDetails.IsFailure)
                    return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse>(responseDetails.Error!);
            }

            var newList = requestDetails.Where(c => c.RequestGoodsSupplyDetailId == null).ToList();
            foreach (var item in newList)
            {
                var packagesQuery = await _packageRepo.GetByGroupId(item!.ProductGroupId, ct);
                if (packagesQuery is null)
                    return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse>(RequestGoodsSupplyDetailErrors.ProductNoHavePackage);
                if (item.PackageId is not null)
                {
                    var pckageValidate = packagesQuery!.Where(x => x.Id == item.PackageId)?.FirstOrDefault();
                    if (pckageValidate is null)
                        return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse>(RequestGoodsSupplyDetailErrors.PackageIdNotValidate);
                }
                else
                {
                    var isDefaultPckage = packagesQuery!.Where(x => x.IsDefault)?.FirstOrDefault();
                    item.PackageId = isDefaultPckage?.Id;
                    if (isDefaultPckage is null)
                        item.PackageId = packagesQuery!.FirstOrDefault()?.Id;
                    if (item.PackageId is null)
                        return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse>(RequestGoodsSupplyDetailErrors.PackageIdNotFound);
                }

                RequestGoodsSupplyProduct? product = null;
                if (!products.Any(x => x.ProductId.Equals(item.ProductId)))
                {
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
                        item.PackageUnitPrice,
                        item.CheckGroup,
                        item.ContractorId,
                        item.DestinationWarehouseId,
                        item.CustomerInvoiceNumber,
                        item.Description,
                        item.ManagementDescription), ct);
                    if (productResponse.IsFailure)
                        return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse?>(productResponse.Error!);
                    product = productResponse.Value!;
                    products.Add(product);
                }
                else
                {
                    product = products.FirstOrDefault(x => x.ProductId.Equals(item.ProductId))!;
                }

                var responseDetails = await _requestGoodsSupplyDetailLogic.ProcessCreateProjectRequestDetail(item.Adapt<CreateProjectRequestGoodsSupplyDetailModel>(), value, product, projectId!.Value, ct);
                if (responseDetails.IsFailure)
                    return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse>(responseDetails.Error!);
            }

            var updated = await _mediator.Send(new UpdateRequestGoodsSupplyCommand(
                value,
                null,
                request.SupplyerId,
                request.BuyerId,
                request.CurrencyId,
                request.TransferPrice,
                request.OtherPrice,
                request.DiscountOnInvoicePercentage,
                request.DiscountOnInvoiceNumber,
                null,
                request.TaxOnInvoicePercentage,
                request.TaxOnInvoiceNumber,
                null,
                request.IsPettyCash,
                request.Description,
                request.RequestedDate,
                request.ConsumptionRateAndInventoryUrl,
                request.ConsumptionAddress,
                request.PurchaseLocation,
                request.PurchaseReason,
                request.IsDraft), ct);
            if (updated.IsFailure)
                return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse>(updated.Error!);

            if ((request.TransferPrice is not null && request.TransferPrice > 0) || (value.RequestGoodsSupplyProducts.Any(x => x.TransferPrice is not null || x.TransferPrice > 0)))
            {
                var responseDivision = await _mediator.Send(new DivisionRequestGoodsSupplyTransferPriceCommand(value), ct);
                if (responseDivision.IsFailure)
                    return Result.Failure<UpdateProjectRequestGoodsSuppliesResponse>(responseDivision.Error!);
            }

            await _unitOfWork.CommitAsync(ct);
            transaction.Complete();
            return new UpdateProjectRequestGoodsSuppliesResponse(true);
        }
    }

    public async Task<Result<SetOperationInfoSeasonToGoodsSupplyResponse?>> SetOperationInfoSeasonToGoodsSupply(
        SetOperationInfoSeasonToGoodsSupplyRequest request, CT ct)
    {
        _logger.LogInformation("Request for SetOperationInfoSeasonToGoodsSupply, SetOperationInfoSeasonToGoodsSupply:{Id},", request.Id);
        using (var transaction = new CreateTransaction())
        {
            var isValidRequest = await request.IsValidAsync<SetOperationInfoSeasonToGoodsSupplyValidator, SetOperationInfoSeasonToGoodsSupplyRequest>(ct);
            if (isValidRequest.IsFailure)
                return Result.Failure<SetOperationInfoSeasonToGoodsSupplyResponse>(isValidRequest.Error!);

            var goodsSupplyResponse = await _mediator.Send(new GetRequestGoodsSupplyByIdQuery(request.Id), ct);
            if (goodsSupplyResponse.IsFailure)
                return Result.Failure<SetOperationInfoSeasonToGoodsSupplyResponse>(goodsSupplyResponse.Error!);
            var value = goodsSupplyResponse.Value!;

            var createOperationInfoSeason = await _operationInfoSeasonLogic
                .CreateOperationInfoSeasonFormGoodsSupply(new(value.ProjectOperation.OperationInfo, request.SeasonId), ct);
            if (createOperationInfoSeason.IsFailure)
                return Result.Failure<SetOperationInfoSeasonToGoodsSupplyResponse>(createOperationInfoSeason.Error!);

            var requestCommand = await _mediator.Send(new SetOperationInfoSeasonToGoodsSupplyCommand(value, createOperationInfoSeason.Value!.OperationInfoSeason), ct);
            if (requestCommand.IsFailure)
                return Result.Failure<SetOperationInfoSeasonToGoodsSupplyResponse>(requestCommand.Error!);

            await _unitOfWork.CommitAsync(ct);
            transaction.Complete();
            return new SetOperationInfoSeasonToGoodsSupplyResponse(true);
        }
    }

    public async Task<Result<RequestGoodsSuppliesGroupDeleteResponse?>> RequestGoodsSuppliesGroupDelete(
        RequestGoodsSuppliesGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for RequestGoodsSuppliesGroupDelete, RequestGoodsSuppliesGroupDelete");
        using (var transaction = new CreateTransaction())
        {
            foreach (var item in request.Ids)
            {
                var newRequest = new DeleteRequestGoodsSupplyRequest(item);
                var response = await DeleteRequestGoodsSupply(newRequest, ct);
                if (response.IsFailure)
                    return Result.Failure<RequestGoodsSuppliesGroupDeleteResponse>(response.Error!);
            }

            await _unitOfWork.CommitAsync(ct);
            transaction.Complete();
            return new RequestGoodsSuppliesGroupDeleteResponse(true);
        }
    }

    public async Task<Result<SetGoodsSupplyToCreatedResponse?>> SetGoodsSupplyToCreated(
        SetGoodsSupplyToCreatedRequest request, CT ct)
    {
        _logger.LogInformation("Request for SetGoodsSupplyToCreated, SetGoodsSupplyToCreated:{Id},", request.Id);
        var isValidRequest = await request.IsValidAsync<SetGoodsSupplyToCreatedValidator, SetGoodsSupplyToCreatedRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetGoodsSupplyToCreatedResponse>(isValidRequest.Error!);

        var goodsSupplyResponse = await _mediator.Send(new GetRequestGoodsSupplyByIdQuery(request.Id), ct);
        if (goodsSupplyResponse.IsFailure)
            return Result.Failure<SetGoodsSupplyToCreatedResponse>(goodsSupplyResponse.Error!);
        var value = goodsSupplyResponse.Value!;

        var requestCommand = await _mediator.Send(new SetGoodsSupplyToCreatedCommand(value), ct);
        if (requestCommand.IsFailure)
            return Result.Failure<SetGoodsSupplyToCreatedResponse>(requestCommand.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new SetGoodsSupplyToCreatedResponse(true);
    }

    public async Task<Result<DeleteRequestGoodsSupplyResponse?>> DeleteRequestGoodsSupply(
        DeleteRequestGoodsSupplyRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteRequestGoodsSupply, DeleteRequestGoodsSupply:{Id},", request.Id);
        var isValidRequest = await request.IsValidAsync<DeleteRequestGoodsSupplyValidator, DeleteRequestGoodsSupplyRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteRequestGoodsSupplyResponse>(isValidRequest.Error!);

        var requestCommand = await _mediator.Send(new DeleteRequestGoodsSupplyCommand(request.Id), ct);
        if (requestCommand.IsFailure)
            return Result.Failure<DeleteRequestGoodsSupplyResponse>(requestCommand.Error!);

        await _unitOfWork.CommitAsync(ct);
        return requestCommand.Value!.Adapt<DeleteRequestGoodsSupplyResponse>();
    }

    public async Task<Result<GetRequestGoodsSupplyByIdResponse?>> GetRequestGoodsSupplyById(
        GetRequestGoodsSupplyByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetRequestGoodsSupplyById, GetRequestGoodsSupplyById:{Id},", request.Id);
        var isValidRequest = await request.IsValidAsync<GetRequestGoodsSupplyByIdValidator, GetRequestGoodsSupplyByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestGoodsSupplyByIdResponse>(isValidRequest.Error!);

        var query = await _mediator.Send(new GetRequestGoodsSupplyByIdQuery(request.Id), ct);
        if (query.IsFailure)
            return Result.Failure<GetRequestGoodsSupplyByIdResponse>(query.Error!);
        var value = query.Value!;

        List<ViewThirdParty?>? contractorsInfo = [];
        var metaIds = new List<long>();
        metaIds.AddRange(new[] { value.SupplyerId, value.BuyerId }.Where(id => id.HasValue).Select(id => id!.Value));
        if (value.RequestGoodsSupplyDetails is not null && value.RequestGoodsSupplyDetails.Any())
            metaIds.AddRange(value.RequestGoodsSupplyDetails.Where(x => x.ContractorId is not null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).ToList());

        ViewOrganization? org = null;
        if (value.RequestingOrganizationId is not null)
            org = await _orgRepo.GetById(value.RequestingOrganizationId.Value, ct);
        if (metaIds is not null && metaIds.Count > 0)
        {
            var contractors = await _thirdPartyRepo.GetByIds(metaIds, ct);
            contractorsInfo.AddRange(contractors ?? []);
        }

        ViewCurrency? currency = null;
        if(value.CurrencyId is not null)
            currency = await _currRepo.GetById(value.CurrencyId.Value, ct);

        List<GetsRequestGoodsSupplyDetailModel> details = [];
        if (value.IsProjectSupply)
        {
            details = await ProjectDetailsModeling(value, contractorsInfo, ct);
            return new GetRequestGoodsSupplyByIdResponse()
            {
                Id = value.Id,
                SupplyerId = value.SupplyerId,
                SupplyerName = contractorsInfo.FirstOrDefault(x => x is not null && x.Id.Equals(value.SupplyerId))?.FullName,
                BuyerId = value.BuyerId,
                BuyerFullName = contractorsInfo.FirstOrDefault(x => x is not null && x.Id.Equals(value.BuyerId))?.FullName,
                RequestNumber = value.RequestSerialNumber,
                Currency = new GetsRequestGoodsSupplyCurrencyModel(value.CurrencyId, currency?.Name),
                OtherPrice = value.OtherPrice,
                TransferPrice = value.TransferPrice,
                ConsumptionAddress = value.ConsumptionAddress,
                Status = value.Status,
                Type = value.Type,
                CanUpdate = details.All(x => x.CanUpdate),
                CostCenterId = value.Project?.ProjectCostCenters.FirstOrDefault()?.CostCenter.Id,
                CostCenterName = value.Project?.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                ProjectId = value.ProjectId,
                ProjectName = value.Project?.ProjectName,
                ProjectEnName = value.Project?.ProjectEnName,
                CreatedOn = value.Created,
                IsPettyCash = value.IsPettyCash,
                IsProjectSupply = value.IsProjectSupply,
                PurchaseLocation = value.PurchaseLocation,
                PurchaseReason = value.PurchaseReason,
                RequestingOrganizationId = value.RequestingOrganizationId,
                RequestingOrganization = org?.NameFa,
                RequestingOrganizationEn = org?.NameEn,
                RequestedDate = value.RequestedDate,
                CreatorId = value.CreatorId,
                Creator = details?.Where(m => m.Creator is not null).FirstOrDefault()?.Creator,
                Details = details,
            };
        }

        else
        {
            details = await DetailsModeling(value, contractorsInfo, ct);
            var unitQuery = await _mediator.Send(new GetMeasureunitByIdQuery(value.ProjectOperation.OperationInfo.UnitOfMeasurementId));
            return new GetRequestGoodsSupplyByIdResponse()
            {
                Id = value.Id,
                SupplyerId = value.SupplyerId,
                SupplyerName = contractorsInfo.FirstOrDefault(x => x is not null && x.Id.Equals(value.SupplyerId))?.FullName,
                BuyerId = value.BuyerId,
                BuyerFullName = contractorsInfo.FirstOrDefault(x => x is not null && x.Id.Equals(value.BuyerId))?.FullName,
                RequestNumber = value.RequestSerialNumber,
                Currency = new GetsRequestGoodsSupplyCurrencyModel(value.CurrencyId, currency?.Name),
                OtherPrice = value.OtherPrice,
                ConsumptionAddress = value.ConsumptionAddress,
                TransferPrice = value.TransferPrice,
                Status = value.Status,
                Type = value.Type,
                CanUpdate = details.All(x => x.CanUpdate),
                ProjectOperationId = value.ProjectOperation.Id,
                OperationInfoCode = value.ProjectOperation.OperationInfo.OperationInfoCode,
                OperationInfoName = value.ProjectOperation.OperationInfo.OperationInfoName,
                CostCenterId = value.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                CostCenterName = value.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                ProjectId = value.ProjectOperation.Project.Id,
                ProjectName = value.ProjectOperation.Project.ProjectName,
                ProjectEnName = value.ProjectOperation.Project.ProjectEnName,
                Workload = value.ProjectOperation.Workload,
                CategoryId = value.OperationInfoSeason?.Season.Branch.Category.Id,
                CategoryName = value.OperationInfoSeason?.Season.Branch.Category.CategoryName,
                BranchId = value.OperationInfoSeason?.Season.Branch.Id,
                BranchName = value.OperationInfoSeason?.Season.Branch.BranchName,
                SeasonId = value.OperationInfoSeason?.Season.Id,
                SeasonName = value.OperationInfoSeason?.Season.SeasonName,
                ProjectOperationDetailId = value.ProjectOperationDetail?.Id,
                PrivateName = value.ProjectOperationDetail?.OperationLocation.PrivateName,
                PrivateCode = value.ProjectOperationDetail?.OperationLocation.PrivateCode,
                PublicName = value.ProjectOperationDetail?.OperationLocation.PublicName,
                PublicCode = value.ProjectOperationDetail?.OperationLocation.PublicCode,
                MeasureId = value.ProjectOperation.OperationInfo.UnitOfMeasurementId,
                MeasureName = unitQuery.Value?.Name,
                RequestingOrganizationId = value.RequestingOrganizationId,
                RequestingOrganization = org?.NameFa,
                RequestingOrganizationEn = org?.NameEn,
                CreatedOn = value.Created,
                IsPettyCash = value.IsPettyCash,
                IsProjectSupply = value.IsProjectSupply,
                PurchaseLocation = value.PurchaseLocation,
                PurchaseReason = value.PurchaseReason,
                RequestedDate = value.RequestedDate,
                CreatorId = value.CreatorId,
                Creator = details?.Where(m => m.Creator is not null).FirstOrDefault()?.Creator,
                Details = details,
            };
        }
    }

    public async Task<Result<GetFilteredRequestGoodsSuppliesReportsResponse?>> GetFilteredRequestGoodsSuppliesReports(
        GetFilteredRequestGoodsSuppliesReportsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFilteredRequestGoodsSuppliesReports, GetFilteredRequestGoodsSuppliesReports");
        var isValidRequest = await request.IsValidAsync<GetFilteredRequestGoodsSuppliesReportsValidator, GetFilteredRequestGoodsSuppliesReportsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredRequestGoodsSuppliesReportsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetFilteredRequestGoodsSuppliesReportsQuery(
            null, null, request.CostCenterId, request.ProjectId, request.ProjectOperationId, request.ProjectOperationDetailId, request.Statuses,
            request.ProductIds, request.Type, request.ProductType, request.ProductGroupId, request.FromDate, request.ToDate, request.CreatorId,
            _companyId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null || response.Value.Data.Count <= 0)
            return Result.Failure<GetFilteredRequestGoodsSuppliesReportsResponse>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithFilterNotFound);
        var requestGoodsSupplies = response!.Value!.Data!;

        var requestGoodsSupplyCreatorIds = requestGoodsSupplies.Select(c => c.CreatorId).Distinct().ToList();
        var requestGoodsSupplyCreators = await WebServicesLogic.UserDataReceiver(requestGoodsSupplyCreatorIds, null, _mediator, ct);
        var currencyIds = requestGoodsSupplies.Where(x => x.CurrencyId is not null && x.CurrencyId > 0).Select(x => (long)x.CurrencyId!).ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);
        var measurementIds = requestGoodsSupplies.Where(x => x.ProjectOperation.UnitOfMeasurementId != 0).Select(x => x.ProjectOperation.UnitOfMeasurementId).ToList();
        var measureunits = await WebServicesLogic.MeasurementDataReceiver(measurementIds, _mediator, ct);

        var results = new List<GetFilteredRequestGoodsSuppliesReportsModel>();
        foreach (var requestGoodsSupply in requestGoodsSupplies)
        {
            var requestGoodsSupplyCreator = requestGoodsSupplyCreators?.Where(oo => oo!.UserId.Equals(requestGoodsSupply.CreatorId)).FirstOrDefault();
            var details = new List<GetFilteredRequestGoodsSupplyDetailsReportsModel>();
            var requestGoodsSupplyDetails = requestGoodsSupply!.RequestGoodsSupplyDetails.ToList();
            var groupIds = requestGoodsSupplyDetails.Select(c => c.ConsumableVolumeProduct.ProductGroupId).ToList();
            var groups = await WebServicesLogic.GroupsDataReceiver(groupIds, _mediator, ct);
            var productIds = requestGoodsSupplyDetails.Where(x => x.ProductId > 0).Select(oo => oo.ProductId).ToList();
            var products = await WebServicesLogic.ProductsDataReceiver(productIds, _mediator, _viewProductRepository, ct);

            var reviewerModel = RequestReviewer(requestGoodsSupply);
            var packageIds = requestGoodsSupplyDetails.Where(x => x.PackageId is not null && x.PackageId > 0).Select(x => (long)x.PackageId!).ToList();
            var packagesInfo = await WebServicesLogic.PackagesDataReceiver(packageIds, _mediator, ct);

            List<UserModel?>? contractorsInfo = [];
            var ids = requestGoodsSupply.RequestGoodsSupplyDetails.Where(x => x.ContractorId is not null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList();
            if (ids is not null && ids.Count > 0)
            {
                var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(ids, null, null, _mediator, ct);
                contractorsInfo.AddRange(contractors ?? []);
            }

            foreach (var item in requestGoodsSupplyDetails)
            {
                var product = products?.Where(x => x.Id == item.ProductId).FirstOrDefault();
                var group = groups?.Where(x => x.Id == item.ConsumableVolumeProduct.ProductGroupId).FirstOrDefault();
                var operationId = requestGoodsSupply.ProjectOperation.Id;
                var operationDetailId = item.ConsumableVolumeProduct.ProjectOperationDetail.Id;
                var groupId = item.ConsumableVolumeProduct.ProductGroupId;
                var package = packagesInfo?.Where(x => x.Id.Equals(item.PackageId)).FirstOrDefault();

                var productGroupsQuery = await _mediator.Send(new GetsConsumableVolumeProductsForSupplyQuery(null, operationId, operationDetailId, groupId, null, null, 1, 10), ct);
                var total = TotalDataReceiver(productGroupsQuery.Value!.Data!.FirstOrDefault()!);

                var detail = item.Adapt<GetFilteredRequestGoodsSupplyDetailsReportsModel>();
                detail.GroupName = group?.Name;
                detail.GroupCode = group?.Code;
                detail.GroupMeasure = group?.MeasureUnitName;
                detail.Product = product;
                detail.ProductCode = product?.Code;
                detail.ProductName = product?.Name;
                detail.Brand = product?.Brand;
                detail.BrandModel = product?.BrandModel;
                detail.TolerancePercentage = total.TolerancePercentage;
                detail.ToleranceCount = total.ToleranceCount;
                detail.TotalEstimatedCount = total.TotalEstimatedCount;
                detail.TotalRequestedCount = total.TotalRequestedCount;
                detail.TotalSupplyCount = total.TotalSupplyCount;
                detail.TotalRemainedCount = total.TotalRemainedCount;
                detail.Creator = requestGoodsSupplyCreator?.FullName;
                detail.Package = new(package?.Id, package?.Quantity, package?.IsDefault, package?.IsActive, package?.Title, item.PackageCount);
                detail.Documents = item.RequestGoodsSupplyDetailDocuments.Select(oo => oo.Url).ToList();
                detail.FullName = contractorsInfo.Where(x => x?.Id == item.ContractorId).FirstOrDefault()?.FullName;
                detail.ManagementType = item.RequestGoodsSupplyManagements.Any() ? item.RequestGoodsSupplyManagements.Last().Type : null;
                detail.RequestedCount = item.RequestedCount;
                detail.SupplyCount = item.RequestGoodsSupplyManagements.Where(x => x.Status == GoodsSupplyManagementStatus.CompleteSupply).Sum(x => x.RequestedCount);
                detail.LastDescription = item.LastDescription;
                details.Add(detail);
            }

            var measureunit = measureunits?.Where(x => x.Id == requestGoodsSupply.ProjectOperation.UnitOfMeasurementId).FirstOrDefault();
            GetFilteredRequestGoodsSuppliesReportsModel result = new();

            result = requestGoodsSupply.Adapt<GetFilteredRequestGoodsSuppliesReportsModel>();
            result.MeasurementName = measureunit?.Name;
            result.RejectedNumber = reviewerModel.RejectedNumber;
            result.AllInStock = reviewerModel.AllInStock;
            result.InStockNumber = reviewerModel.InStockNumber;
            result.AllBetweenStock = reviewerModel.AllBetweenStock;
            result.BetweenStockNumber = reviewerModel.BetweenStockNumber;
            result.AllCommerce = reviewerModel.AllCommerce;
            result.CommerceNumber = reviewerModel.CommerceNuber;
            result.Creator = requestGoodsSupplyCreator?.FullName;
            result.Details = details;

            results.Add(result);
        }

        return new GetFilteredRequestGoodsSuppliesReportsResponse(results ?? new List<GetFilteredRequestGoodsSuppliesReportsModel>(0), response!.Value!.RowCount!);
    }

    public async Task<Result<GetRequestGoodsHistoryByIdResponse?>> GetRequestGoodsHistoryById(
        GetRequestGoodsHistoryByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetRequestGoodsHistoryById, GetRequestGoodsHistoryById:{Id},", request.Id);
        var isValidRequest = await request.IsValidAsync<GetRequestGoodsHistoryByIdValidator, GetRequestGoodsHistoryByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestGoodsHistoryByIdResponse>(isValidRequest.Error!);

        var query = await _mediator.Send(new GetRequestGoodsHistoryByIdQuery(request.Id, request.PageIndex, request.PageSize), ct);
        if (query.IsFailure)
            return Result.Failure<GetRequestGoodsHistoryByIdResponse>(query.Error!);
        if (query.Value is null || query.Value.Data is null || query.Value?.Data.Count <= 0)
            Result.Failure<GetRequestGoodsHistoryByIdResponse>(RequestGoodsSupplyHistoryErrors.RequestGoodsSupplyHistoryWithIdNotFound);
        var histories = query.Value?.Data;

        var requestGoodsSupplyCreatorIds = histories!.Select(c => c.CreatorId).Distinct().ToList();
        var requestCreators = await WebServicesLogic.UserDataReceiver(requestGoodsSupplyCreatorIds, null, _mediator, ct);
        var responseDetail = histories.Adapt<List<GetRequestGoodsHistoryByIdDetailModel>>() ?? new List<GetRequestGoodsHistoryByIdDetailModel>(0);
        responseDetail.ForEach(oo =>
        {
            oo.Creator = requestCreators?.Where(c => c!.UserId.Equals(oo.CreatorId)).FirstOrDefault()?.FullName;
        });

        return new GetRequestGoodsHistoryByIdResponse(responseDetail, query.Value?.Data?.Count);
    }

    public async Task<Result<GetFltrRGSupplyWithProductsResponse?>> GetFltrRGSupplyWithProducts(
        GetFltrRGSupplyWithProductsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFltrRGSupplyWithProducts");

        bool checkThirdParty = false;
        var engConfig = await _mediator.Send(new GetActiveConfigQuery());
        if (request.ThirdPartyId is not null)
            if (!engConfig.IsBad() && engConfig.Value!.ProjectThirdParties)
                checkThirdParty = true;

        var rGSupply = await _requestGoodsSupplyRepository.GetFltrRGSWithProducts(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProductGroupIds,
            request.ProductIds,
            request.CreatorIds,
            request.ManagementIds,
            request.Types,
            request.Statuses,
            request.RemoveStatuses,
            request.StartDate,
            request.EndDate,
            request.ProjectManagerId,
            request.ThirdPartyId,
            checkThirdParty,
            request.CityId,
            request.FilterData,
            request.OrderBy,
            request.PageIndex,
            request.PageSize,
            _companyId,
            ct
        );
        if (rGSupply == null || rGSupply.Count == 0)
            return Result.Failure<GetFltrRGSupplyWithProductsResponse>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithFilterNotFound);

        var creatorIds = rGSupply.NullListed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);
        if (creators != null && creators.Count > 0)
            foreach (var item in rGSupply)
            {
                var creator = creators!.FirstOrDefault(x => x.UserId == item.CreatorId);
                item.Creator = creator?.FullName;
            }

        return new GetFltrRGSupplyWithProductsResponse(rGSupply, rGSupply.Count());
    }


    public async Task<Result<GetFltrProductsResponse?>> GetFltrProducts(
        GetFltrProductsRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrProducts");
        var result = await _mediator.Send(new GetFltrProductsQuery(request.ProjectId, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (result.IsBad()) return result.Failure<GetFltrProductsResponse>()!;

        return result;
    }

    public async Task<Result<RGSupplyDetailsStatusChangerResponse?>> RGSupplyDetailsStatusChanger(
        RGSupplyDetailsStatusChangerRequest request, CT ct)
    {
        _logger.LogInformation("Request for RGSupplyDetailsStatusChanger");

        var response = await RGSupplyDetailsStatusChangerCommand(request, ct);
        if (response.IsBad())
            return response.Failure<RGSupplyDetailsStatusChangerResponse?>();

        await _unitOfWork.CommitAsync(ct);
        return new RGSupplyDetailsStatusChangerResponse(true);
    }

    public async Task<Result<GetRequestGoodsSupplyStatusResponse?>> GetRequestGoodsSupplyStatus(
        GetRequestGoodsSupplyStatusRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetRequestGoodsSupplyStatus, GetRequestGoodsSupplyStatus");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<GoodsSupplyStatus>());

        if (request.Statuses?.Any() == true)
        {
            var codes = request.Statuses.Select(x => (int)x);
            response = response.Where(x => !codes.Contains(x.Code)).ToList();
        }
        return new GetRequestGoodsSupplyStatusResponse(response);
    }

    public async Task<Result<GetRequestGoodsSupplyTypeResponse?>> GetRequestGoodsSupplyType(
        GetRequestGoodsSupplyTypeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetRequestGoodsSupplyType, GetRequestGoodsSupplyType");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<GoodsSupplyType>());
        return new GetRequestGoodsSupplyTypeResponse(response);
    }

    public async Task<Result<GetsRequestGoodSupplyRequesterResponse?>> GetsRequestGoodSupplyRequester(
        GetsRequestGoodSupplyRequesterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsRequestGoodSupplyRequester, GetsRequestGoodSupplyRequester");
        var isValidRequest = await request.IsValidAsync<GetsRequestGoodSupplyRequesterValidator, GetsRequestGoodSupplyRequesterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsRequestGoodSupplyRequesterResponse>(isValidRequest.Error!);

        var reponse = await _mediator.Send(new GetsRequestGoodSupplyRequesterQuery(), ct);
        if (reponse.IsFailure)
            return Result.Failure<GetsRequestGoodSupplyRequesterResponse>(reponse.Error!);
        var value = reponse.Value!;

        List<FilteredUserResponseModel?> requesters = new();
        var data = new List<GetsRequestGoodSupplyRequesterModel>();
        if (value.Data?.Count > 0)
        {
            var responseValue = await _mediator.Send(new GetFilteredUsersQuery(value.Data!, request.FilterData, null, null, null, 1, value.Data!.Count), ct);
            if (responseValue.Value?.Data is not null)
                requesters.AddRange(responseValue.Value.Data!);

            if (!string.IsNullOrEmpty(request.FilterData))
            {
                foreach (var id in value.Data!)
                {
                    if (!requesters.Any(x => x?.UserId == id))
                        continue;

                    var requester = requesters.Where(x => x?.UserId == id).FirstOrDefault();
                    data.Add(new GetsRequestGoodSupplyRequesterModel()
                    {
                        Id = id,
                        FirstName = requester?.FirstName,
                        LastName = requester?.LastName,
                        Nickname = requester?.Nickname,
                        DefaultPhoneNo = requester?.DefaultPhoneNo,
                        OrganizationCode = requester?.OrganizationCode,
                        IdentityNo = requester?.IdentityNo
                    });
                }
            }
            else
            {
                foreach (var id in value.Data!)
                {
                    var requester = requesters.Where(x => x?.UserId == id).FirstOrDefault();
                    data.Add(new GetsRequestGoodSupplyRequesterModel()
                    {
                        Id = id,
                        FirstName = requester?.FirstName,
                        LastName = requester?.LastName,
                        Nickname = requester?.Nickname,
                        DefaultPhoneNo = requester?.DefaultPhoneNo,
                        OrganizationCode = requester?.OrganizationCode,
                        IdentityNo = requester?.IdentityNo
                    });
                }
            }
        }
        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsRequestGoodSupplyRequesterResponse(responseData ?? new List<GetsRequestGoodSupplyRequesterModel>(0), requesters?.Count ?? 0);
    }

    public async Task<Result<GetRequestGoodsSupplyCreatorsResponse?>> GetRequestGoodsSupplyCreators(
        GetRequestGoodsSupplyCreatorsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetRequestGoodsSupplyCreators, GetRequestGoodsSupplyCreators");
        var isValidRequest = await request.IsValidAsync<GetRequestGoodsSupplyCreatorsRequestValidator, GetRequestGoodsSupplyCreatorsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestGoodsSupplyCreatorsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestGoodsSupplyCreatorsQuery(request.CostCenterIds, request.ProjectIds, request.ProjectOperationIds, request.ProjectOperationDetailIds, request.RequestGoodsSupplyIds), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetRequestGoodsSupplyCreatorsResponse>(response.Error!);
        var userIds = response.Value!.Data;

        List<FilteredUserResponseModel>? creators = [];
        var data = new List<GetRequestGoodsSupplyCreatorsResponseModel>();
        if (userIds is not null && userIds?.Count > 0)
        {
            var responseValue = await WebServicesLogic.UserDataReceiver(userIds!, request.FilterData, _mediator, ct);
            if (responseValue is not null && responseValue.Count > 0)
                foreach (var item in responseValue)
                    if (item != null)
                        creators.Add(item);

            if (!string.IsNullOrEmpty(request.FilterData))
            {
                foreach (var id in userIds)
                {
                    if (!creators.Any(x => x?.UserId == id))
                        continue;

                    var creator = creators.Where(x => x?.UserId == id).FirstOrDefault();
                    data.Add(new GetRequestGoodsSupplyCreatorsResponseModel()
                    {
                        Id = creator?.UserId,
                        FirstName = creator?.FirstName,
                        LastName = creator?.LastName,
                        Nickname = creator?.Nickname
                    });
                }
            }
            else
            {
                foreach (var id in userIds)
                {
                    if (!creators.Any(x => x?.UserId == id))
                        continue;

                    var creator = creators.Where(x => x?.UserId == id).FirstOrDefault();
                    data.Add(new GetRequestGoodsSupplyCreatorsResponseModel()
                    {
                        Id = creator?.UserId,
                        FirstName = creator?.FirstName,
                        LastName = creator?.LastName,
                        Nickname = creator?.Nickname
                    });
                }
            }
        }
        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetRequestGoodsSupplyCreatorsResponse(responseData ?? new List<GetRequestGoodsSupplyCreatorsResponseModel>(0), creators?.Count ?? 0);
    }

    public async Task<Result<GetRequestGoodsSupplyProductGroupsResponse?>> GetRequestGoodsSupplyProductGroups(
        GetRequestGoodsSupplyProductGroupsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetRequestGoodsSupplyProductGroups, GetRequestGoodsSupplyProductGroups");
        var isValidRequest = await request.IsValidAsync<GetRequestGoodsSupplyProductGroupsRequestValidator, GetRequestGoodsSupplyProductGroupsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestGoodsSupplyProductGroupsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestGoodsSupplyProductGroupsQuery(request.CostCenterId, request.ProjectId, request.ProjectOperationId, request.ProjectOperationDetailId, request.ProductType), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetRequestGoodsSupplyProductGroupsResponse>(response.Error!);
        var responses = response.Value!.Data;

        int rowCount = 0;
        List<FilteredGroup>? groups = [];
        List<WarehouseCategory>? categories = [];
        var data = new List<GetRequestGoodsSupplyProductGroupsResponseModel>();
        if (responses is not null && responses?.Count > 0)
        {
            var categoryIds = responses.Where(x => x.RequestGoodsSupplyDetails is not null && x.RequestGoodsSupplyDetails.Count > 0).SelectMany(x => x.RequestGoodsSupplyDetails)
                 .Where(x => x.ConsumableVolumeProduct.VolumeProductType == VolumeProductType.Category).Select(x => x.ConsumableVolumeProduct.ProductGroupId)
                 .Where(x => x > 0).Distinct().ToList();
            var categoryValues = await WebServicesLogic.CategoriesDataReceiver(categoryIds, request.FilterData, _mediator, ct);
            if (categoryValues is not null && categoryValues.Count > 0)
                foreach (var item in categoryValues)
                    categories.Add(item);

            var groupIds = responses.Where(x => x.RequestGoodsSupplyDetails is not null && x.RequestGoodsSupplyDetails.Count > 0).SelectMany(x => x.RequestGoodsSupplyDetails)
              .Where(x => x.ConsumableVolumeProduct.VolumeProductType == VolumeProductType.ProductGroup).Select(x => x.ConsumableVolumeProduct.ProductGroupId)
              .Where(x => x > 0).Distinct().ToList();
            var productGroupValues = await WebServicesLogic.GetFilteredGroupsDataReceiver(groupIds, request.FilterData, _mediator, ct);
            if (productGroupValues is not null && productGroupValues.Count > 0)
                foreach (var item in productGroupValues)
                    groups.Add(item);

            if (request.ProductType == VolumeProductType.ProductGroup || request.ProductType == null)
            {
                rowCount += groups.Count;
                if (!string.IsNullOrEmpty(request.FilterData))
                {
                    foreach (var id in groupIds)
                    {
                        if (!groups.Any(x => x?.Id == id))
                            continue;

                        var ProductGroup = groups.Where(x => x?.Id == id).FirstOrDefault();
                        data.Add(new GetRequestGoodsSupplyProductGroupsResponseModel()
                        {
                            ProductGroupId = ProductGroup?.Id,
                            ProductGroupName = ProductGroup?.Name,
                            ProductGroupCode = ProductGroup?.Code,
                            ProductType = VolumeProductType.ProductGroup
                        });
                    }
                }
                else
                {
                    foreach (var id in groupIds)
                    {
                        if (!groups.Any(x => x?.Id == id))
                            continue;

                        var ProductGroup = groups.Where(x => x?.Id == id).FirstOrDefault();
                        data.Add(new GetRequestGoodsSupplyProductGroupsResponseModel()
                        {
                            ProductGroupId = ProductGroup?.Id,
                            ProductGroupName = ProductGroup?.Name,
                            ProductGroupCode = ProductGroup?.Code,
                            ProductType = VolumeProductType.ProductGroup
                        });
                    }
                }
            }

            if (request.ProductType == VolumeProductType.Category || request.ProductType == null)
            {
                rowCount += categories.Count;
                if (categoryIds is not null && categoryIds.Count > 0)
                {
                    if (!string.IsNullOrEmpty(request.FilterData))
                    {
                        foreach (var id in categoryIds)
                        {
                            if (!categories.Any(x => x?.Id == id))
                                continue;

                            var category = categories.Where(x => x?.Id == id).FirstOrDefault();
                            data.Add(new GetRequestGoodsSupplyProductGroupsResponseModel()
                            {
                                ProductGroupId = category?.Id,
                                ProductGroupName = category?.Title,
                                ProductGroupCode = category?.Code,
                                ProductType = VolumeProductType.Category
                            });
                        }
                    }
                    else
                    {
                        foreach (var id in categoryIds)
                        {
                            if (!categories.Any(x => x?.Id == id))
                                continue;

                            var category = categories.Where(x => x?.Id == id).FirstOrDefault();
                            data.Add(new GetRequestGoodsSupplyProductGroupsResponseModel()
                            {
                                ProductGroupId = category?.Id,
                                ProductGroupName = category?.Title,
                                ProductGroupCode = category?.Code,
                                ProductType = VolumeProductType.Category
                            });
                        }
                    }
                }

            }
        }
        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetRequestGoodsSupplyProductGroupsResponse(responseData ?? new List<GetRequestGoodsSupplyProductGroupsResponseModel>(0), rowCount);
    }

    public async Task<Result<GetRequestGoodsSupplyProductsResponse?>> GetRequestGoodsSupplyProducts(
        GetRequestGoodsSupplyProductsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetRequestGoodsSupplyProducts, GetRequestGoodsSupplyProducts");
        var isValidRequest = await request.IsValidAsync<GetRequestGoodsSupplyProductsRequestValidator, GetRequestGoodsSupplyProductsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestGoodsSupplyProductsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetRequestGoodsSupplyProductsQuery(request.CostCenterIds, request.ProjectIds, request.ProjectOperationIds, request.ProjectOperationDetailIds, request.RequestGoodsSupplyIds, request.ProductGroupIds), ct);
        if (response.IsFailure || response.Value is null)
            return Result.Failure<GetRequestGoodsSupplyProductsResponse>(response.Error!);
        var ids = response.Value!.Data;

        List<Product>? products = [];
        var data = new List<GetRequestGoodsSupplyProductsResponseModel>();
        if (ids is not null && ids?.Count > 0)
        {
            var responseValue = await WebServicesLogic.ProductsDataReceiver(ids, request.FilterData, _mediator, _viewProductRepository, ct);
            if (responseValue is not null && responseValue.Count > 0)
                foreach (var item in responseValue)
                    if (item != null)
                        products.Add(item);

            if (!string.IsNullOrEmpty(request.FilterData))
            {
                foreach (var id in ids)
                {
                    if (!products.Any(x => x?.Id == id))
                        continue;

                    var product = products.Where(x => x?.Id == id).FirstOrDefault();
                    data.Add(new GetRequestGoodsSupplyProductsResponseModel()
                    {
                        Id = product?.Id,
                        Name = product?.Name,
                        Code = product?.Code,
                        MeasureunitId = product?.Group.MeasureId,
                        Measureunit = product?.Group.Measure,
                    });
                }
            }
            else
            {
                foreach (var id in ids)
                {
                    if (!products.Any(x => x?.Id == id))
                        continue;

                    var product = products.Where(x => x?.Id == id).FirstOrDefault();
                    data.Add(new GetRequestGoodsSupplyProductsResponseModel()
                    {
                        Id = product?.Id,
                        Name = product?.Name,
                        Code = product?.Code,
                        MeasureunitId = product?.Group.MeasureId,
                        Measureunit = product?.Group.Measure,
                    });
                }
            }
        }
        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetRequestGoodsSupplyProductsResponse(responseData ?? new List<GetRequestGoodsSupplyProductsResponseModel>(0), products?.Count ?? 0);
    }

    public async Task<Result<GetRGSByIdResponse?>> GetRGSById(
        GetRGSByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetRGSById");
        var result = await _mediator.Send(new GetRGSByIdQuery(request.Id), ct);
        if (result.IsBad()) return result.Failure<GetRGSByIdResponse>()!;

        return result;
    }

    public async Task<Result<GetDetailByRGSTypeIdResponse?>> GetDetailByRGSTypeId(
        GetDetailByRGSTypeIdRequest request, CT ct)
    {
        _logger.LogInformation("GetDetailByRGSTypeId");
        var result = await _mediator.Send(new GetDetailByRGSTypeIdQuery(
            request.Id,
            request.Statuses,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad()) return result.Failure<GetDetailByRGSTypeIdResponse>()!;

        return result;
    }

    public async Task<Result<GetReferenceTypeHistoryResponse?>> GetReferenceTypeHistory(
        GetReferenceTypeHistoryRequest request, CT ct)
    {
        _logger.LogInformation("GetReferenceTypeHistory");
        var result = await _mediator.Send(new GetReferenceTypeHistoryQuery(request.ReferenceId,
            request.Statuses,
            request.SupplyType,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad()) return result.Failure<GetReferenceTypeHistoryResponse>()!;

        return result;
    }

    public async Task<Result<GetFltrRGSResponse?>> GetFltrRGS(
        GetFltrRGSRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrRGS");
        var result = await _mediator.Send(new GetFltrRGSQuery(
            request.ProjectIds,
            request.CityId,
            request.ProjectManagerId,
            request.Statuses,
            request.Types,
            request.CreatorIds,
            request.FromDate,
            request.ToDate,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad()) return result.Failure<GetFltrRGSResponse>()!;

        return result;
    }

    public async Task<Result<GetRGSTypeByRGSIdResponse?>> GetRGSTypeByRGSId(
        GetRGSTypeByRGSIdRequest request, CT ct)
    {
        _logger.LogInformation("GetRGSTypeByRGSId");
        var result = await _mediator.Send(new GetRGSTypeByRGSIdQuery(
            request.Id,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad()) return result.Failure<GetRGSTypeByRGSIdResponse>()!;

        return result;
    }

    public async Task<Result<GetDetailByRGSIdResponse?>> GetDetailByRGSId(
        GetDetailByRGSIdRequest request, CT ct)
    {
        _logger.LogInformation("GetDetailByRGSId");
        var result = await _mediator.Send(new GetDetailByRGSIdQuery(
            request.Id,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad()) return result.Failure<GetDetailByRGSIdResponse>()!;

        return result;
    }
}
