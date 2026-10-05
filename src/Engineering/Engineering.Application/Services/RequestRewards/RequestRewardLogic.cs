using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithoutInclude;
using Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdNoIncluding;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdNoIncluding;
using Engineering.Application.Services.Projects.Queries.GetProjectByIdNoIncluding;
using Engineering.Application.Services.RequestRewards.Commands.CloseRequestReward;
using Engineering.Application.Services.RequestRewards.Commands.ConfirmRequestReward;
using Engineering.Application.Services.RequestRewards.Commands.CreateRequestReward;
using Engineering.Application.Services.RequestRewards.Commands.CreateRequestRewardDocument;
using Engineering.Application.Services.RequestRewards.Commands.CreateRequestRewardProduct;
using Engineering.Application.Services.RequestRewards.Commands.CreateRequestRewardThirdParty;
using Engineering.Application.Services.RequestRewards.Commands.DeleteRequestReward;
using Engineering.Application.Services.RequestRewards.Commands.DeleteRequestRewardByIds;
using Engineering.Application.Services.RequestRewards.Commands.PendingRequestReward;
using Engineering.Application.Services.RequestRewards.Commands.RejectRequestReward;
using Engineering.Application.Services.RequestRewards.Commands.UpdateRequestReward;
using Engineering.Application.Services.RequestRewards.Contracts.CloseRequestReward;
using Engineering.Application.Services.RequestRewards.Contracts.ConfirmRequestReward;
using Engineering.Application.Services.RequestRewards.Contracts.CreateRequestReward;
using Engineering.Application.Services.RequestRewards.Contracts.DeleteRequestRewardByIds;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredFiduciaryProductsExcelEnums;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewardsExcelEnums;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewardsExcelExporter;
using Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;
using Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardStatus;
using Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardType;
using Engineering.Application.Services.RequestRewards.Contracts.PendingRequestReward;
using Engineering.Application.Services.RequestRewards.Contracts.RejectRequestReward;
using Engineering.Application.Services.RequestRewards.Queries.GetRequestRewardById;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestRewards;
using Engineering.Domain.Entities.RequestRewards.Enums;
using Gita.Backend.Shared.Domain.Shared.Contracts;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.RequestRewards;

partial class RequestRewardLogic : IRequestRewardLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<RequestRewardLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IUserProfileService _userProfileService;
    private readonly IRequestRewardRepository _requestRewardRepository;
    private readonly IViewProductRepository _pRepo;

    public RequestRewardLogic(
        IMediator mediator,
        ILogger<RequestRewardLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        IUserProfileService userProfileService,
        IRequestRewardRepository requestRewardRepository,
        IViewProductRepository pRepo
        )
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _userProfileService = userProfileService;
        _requestRewardRepository = requestRewardRepository;
        _pRepo = pRepo;
    }

    public async Task<Result<CreateRequestRewardResponse?>> CreateRequestReward(CreateRequestRewardRequest request, CT ct)
    {
        _logger.LogInformation("Get Filtered Request Rewards {@request}", request);

        var isValidRequest = await request.IsValidAsync<CreateRequestRewardValidator, CreateRequestRewardRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateRequestRewardResponse>(isValidRequest.Error!);

        RequestReward? requestReward = null;
        var responses = new List<RequestReward>();
        foreach (var data in request.Data)
        {
            if (data.IsDeleted)
            {
                if (data.Id is null || data.Id == 0)
                    return Result.Failure<CreateRequestRewardResponse>(RequestRewardErrors.IdIsEmptyForDelete);

                var deleteResponse = await _mediator.Send(new DeleteRequestRewardCommand((long)data.Id), ct);
                if (deleteResponse.IsFailure)
                    return Result.Failure<CreateRequestRewardResponse>(deleteResponse.Error!);

                requestReward = deleteResponse.Value!;
                continue;
            }

            if (data.Products is not null && data.Products.Count > 0)
                if (data.OfferedPrice < data!.Products.Sum(oo => oo.Price))
                    return Result.Failure<CreateRequestRewardResponse>(RequestRewardErrors.InValidProductPrices);

            CostCenter? costCenter = null;
            Project? project = null;
            ProjectOperation? projectOperation = null;
            ProjectOperationDetail? projectOperationDetail = null;
            if (data.ProjectOperationDetailId is not null && data.ProjectOperationDetailId > 0)
            {
                var getProjectOperationDetail = await _mediator.Send(new GetProjectOperationDetailByIdNoIncludingQuery(data.ProjectOperationDetailId!.Value), ct);
                if (getProjectOperationDetail.IsFailure)
                    return Result.Failure<CreateRequestRewardResponse>(getProjectOperationDetail.Error!);
                projectOperationDetail = getProjectOperationDetail.Value!;

                if (projectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId != data.CostCenterId)
                    return Result.Failure<CreateRequestRewardResponse>(RequestRewardErrors.CostCenterIdIsInValide);
                costCenter = projectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter!;
                if (data.ProjectId is not null && data.ProjectId > 0)
                    if (projectOperationDetail.ProjectOperation.Project.Id != data.ProjectId)
                        return Result.Failure<CreateRequestRewardResponse>(RequestRewardErrors.ProjectIdIsInValide);
                project = projectOperationDetail.ProjectOperation.Project!;
                if (data.ProjectOperationId is not null && data.ProjectOperationId > 0)
                    if (projectOperationDetail.ProjectOperation.Id != data.ProjectOperationId)
                        return Result.Failure<CreateRequestRewardResponse>(RequestRewardErrors.ProjectOperationIdIsInValide);
                projectOperation = projectOperationDetail.ProjectOperation!;
            }

            if (projectOperationDetail is null)
                if (data.ProjectOperationId is not null && data.ProjectOperationId > 0)
                {
                    var getProjectOperation = await _mediator.Send(new GetProjectOperationByIdNoIncludingQuery(data.ProjectOperationId!.Value), ct);
                    if (getProjectOperation.IsFailure)
                        return Result.Failure<CreateRequestRewardResponse>(getProjectOperation.Error!);
                    projectOperation = getProjectOperation.Value!;
                    if (!projectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == data.CostCenterId))
                        return Result.Failure<CreateRequestRewardResponse>(RequestRewardErrors.CostCenterIdIsInValide);
                    costCenter = projectOperation.Project.ProjectCostCenters.FirstOrDefault(x => x.CostCenterId == data.CostCenterId).CostCenter!;
                    if (data.ProjectId is not null && data.ProjectId > 0)
                        if (projectOperation.Project.Id != data.ProjectId)
                            return Result.Failure<CreateRequestRewardResponse>(RequestRewardErrors.ProjectIdIsInValide);
                    project = projectOperation.Project!;
                }

            if (projectOperation is null)
                if (data.ProjectId is not null && data.ProjectId > 0)
                {
                    var getProject = await _mediator.Send(new GetProjectByIdNoIncludingQuery(data.ProjectId!.Value), ct);
                    if (getProject.IsFailure)
                        return Result.Failure<CreateRequestRewardResponse>(getProject.Error!);
                    project = getProject.Value!;
                    if (!project.ProjectCostCenters.Any(x => x.CostCenterId == data.CostCenterId))
                        return Result.Failure<CreateRequestRewardResponse>(RequestRewardErrors.CostCenterIdIsInValide);
                    costCenter = project.ProjectCostCenters.FirstOrDefault(x => x.CostCenterId == data.CostCenterId)!.CostCenter!;
                }

            if (project is null)
            {
                var getCostCenter = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(data.CostCenterId), ct);
                if (getCostCenter.IsFailure)
                    return Result.Failure<CreateRequestRewardResponse>(getCostCenter.Error!);
                costCenter = getCostCenter.Value!;
            }

            if (data.CurrencyId is not null)
            {
                var getCurrency = await _mediator.Send(new GetCurrencyByIdQuery(data.CurrencyId.Value), ct);
                if (getCurrency.IsFailure)
                    return Result.Failure<CreateRequestRewardResponse>(getCurrency.Error!);
            }

            long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
            if (companyId >= 1)
            {
                var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
                if (companyResponse.IsFailure)
                    return Result.Failure<CreateRequestRewardResponse>(companyResponse.Error!);
            }

            if (data.Id is not null && data.Id != 0 && !data.IsDeleted)
            {
                var updateResponse = await _mediator.Send(new UpdateRequestRewardCommand((long)data.Id, data.OfferedPrice, data.Description, data.RegistrationDate, data.Type,
                        data.CurrencyId, costCenter, project, projectOperation, projectOperationDetail, null, companyId), ct);
                if (updateResponse.IsFailure)
                    return Result.Failure<CreateRequestRewardResponse>(updateResponse.Error!);
                requestReward = updateResponse.Value!;
            }
            else
            {
                if (data.Type == RequestRewardType.Discount && project == null)
                    return Result.Failure<CreateRequestRewardResponse>(RequestRewardErrors.ProjectIdIsInValide)!;
                if (data.Type == RequestRewardType.Discount && !project.IsActive)
                    return Result.Failure<CreateRequestRewardResponse>(RequestRewardErrors.ProjectIsInActive)!;
                var response = await _mediator.Send(new CreateRequestRewardCommand(data.OfferedPrice, data.Description, data.RegistrationDate, data.Type,
                                data.CurrencyId, costCenter, project, projectOperation, projectOperationDetail, null, companyId), ct);
                if (response.IsFailure)
                    return Result.Failure<CreateRequestRewardResponse>(response.Error!);
                requestReward = response.Value!;
            }

            if (data.Type == RequestRewardType.Fine || data.Type == RequestRewardType.FiduciaryProductFine)
            {
                if (data.Products is not null && data.Products.Count > 0)
                    foreach (var product in data.Products)
                    {
                        var createProductResponse = await _mediator.Send(new CreateRequestRewardProductCommand(product.Id, product.ProductId,
                            product.CurrencyId, product.Count, product.Price, requestReward, product.IsDeleted), ct);
                        if (createProductResponse.IsFailure)
                            return Result.Failure<CreateRequestRewardResponse>(createProductResponse.Error!);
                    }
            }

            if (data.ThirdPartyIds is not null && data.ThirdPartyIds.Count > 0)
            {
                var thirdPartyIds = data.ThirdPartyIds.Select(c => c).Select(oo => oo.ThirdPartyId).ToList();
                var getsThirdParty = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, thirdPartyIds.Count, thirdPartyIds, null, false, null), ct);
                if (getsThirdParty.IsFailure)
                    return Result.Failure<CreateRequestRewardResponse>(getsThirdParty.Error!);

                var usersInfos = getsThirdParty.Value!.Data!;
                if (usersInfos.Count != thirdPartyIds.Count)
                    return Result.Failure<CreateRequestRewardResponse>(RequestRewardErrors.InValidThirdPartyIds);

                foreach (var thirdPartyId in data.ThirdPartyIds)
                {
                    var createThirdPartyResponse = await _mediator.Send(new CreateRequestRewardThirdPartyCommand(thirdPartyId.Id, thirdPartyId.ThirdPartyId,
                        requestReward, thirdPartyId.IsDeleted), ct);
                    if (createThirdPartyResponse.IsFailure)
                        return Result.Failure<CreateRequestRewardResponse>(createThirdPartyResponse.Error!);
                }
            }

            if (data.Documents is not null && data.Documents.Count > 0)
            {
                foreach (var document in data.Documents)
                {
                    var createDocumentResponse = await _mediator.Send(new CreateRequestRewardDocumentCommand(document.Id, document.Url, requestReward, document.IsDeleted), ct);
                    if (createDocumentResponse.IsFailure)
                        return Result.Failure<CreateRequestRewardResponse>(createDocumentResponse.Error!);
                }
            }
            responses.Add(requestReward);
        }

        await _unitOfWork.CommitAsync(ct);
        var ids = responses.Select(x => x.Id).ToList();
        return new CreateRequestRewardResponse(ids, true);
    }

    public async Task<Result<ConfirmRequestRewardResponse?>> ConfirmRequestReward(ConfirmRequestRewardRequest request, CT ct)
    {
        _logger.LogInformation("Get Filtered Request Rewards {@request}", request);

        var isValidRequest = await request.IsValidAsync<ConfirmRequestRewardValidator, ConfirmRequestRewardRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ConfirmRequestRewardResponse>(isValidRequest.Error!);

        var requestReward = await _mediator.Send(new ConfirmRequestRewardCommand(request.Id, request.CurrencyId, request.ConfirmedPrice, request.ManagerDescription), ct);
        if (requestReward.IsFailure)
            return Result.Failure<ConfirmRequestRewardResponse>(requestReward.Error!);

        await _unitOfWork.CommitAsync(ct);

        var response = requestReward.Value!.Adapt<ConfirmRequestRewardResponse>();
        return response;
    }

    public async Task<Result<RejectRequestRewardResponse?>> RejectRequestReward(RejectRequestRewardRequest request, CT ct)
    {
        _logger.LogInformation("Get Filtered Request Rewards {@request}", request);

        var isValidRequest = await request.IsValidAsync<RejectRequestRewardValidator, RejectRequestRewardRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<RejectRequestRewardResponse>(isValidRequest.Error!);

        var requestReward = await _mediator.Send(new RejectRequestRewardCommand(request.Id, request.ManagerDescription), ct);
        if (requestReward.IsFailure)
            return Result.Failure<RejectRequestRewardResponse>(requestReward.Error!);

        await _unitOfWork.CommitAsync(ct);

        var response = requestReward.Value!.Adapt<RejectRequestRewardResponse>();
        return response;
    }

    public async Task<Result<GetFilteredRequestRewardsResponse?>> GetFilteredRequestRewards(GetFilteredRequestRewardsRequest request, CT ct)
    {
        _logger.LogInformation("Get Filtered Request Rewards {@request}", request);

        var isValidRequest = await request.IsValidAsync<GetFilteredRequestRewardsValidator, GetFilteredRequestRewardsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredRequestRewardsResponse>(isValidRequest.Error!);

        var userProfile = _userProfileService.GetProfileInfo();
        var userExtra = await _userProfileService.GetExtraInfo(ct);

        long? registerUserId = userProfile.UserId;
        long? managerId = null;

        var projectManager = await _userProfileService.HasUserAccessToAction(744, ct);
        if (projectManager.Status == ResponseStatusType.Ok)
        {
            managerId = userExtra.ThirdPartyId;
            registerUserId = null;
        }
        var systemManager = await _userProfileService.HasUserAccessToAction(745, ct);
        if (systemManager.Status == ResponseStatusType.Ok)
        {
            managerId = null;
            registerUserId = null;
        }

        var getResponse = await GetFilteredRequestRewardsHandler(request, registerUserId, managerId, ct);

        if (getResponse.IsFailure)
            return Result.Failure<GetFilteredRequestRewardsResponse>(getResponse.Error!);
        var requestRewards = getResponse.Value!.Data!;

        var thirdPartyIds = requestRewards.SelectMany(x => x.ThirdPartiesModel).Select(a => a.ThirdPartyId).Distinct().ToList();
        var metaData = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        var currencyIds = requestRewards.Where(x => x.CurrencyId is not null && x.CurrencyId > 0).Select(x => x.CurrencyId!.Value).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var companyIds = requestRewards?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var result = new List<GetFilteredRequestRewardsModel>();
        if (requestRewards is not null && requestRewards.Count > 0)
        {
            foreach (var item in requestRewards)
            {
                var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
                var currencyName = currencies?.FirstOrDefault(x => x.Id == item.CurrencyId)?.Name;
                item.Currency = currencyName;
                if (item.ThirdPartiesModel is not null && item.ThirdPartiesModel.Count > 0)
                {
                    foreach (var thirdparty in item.ThirdPartiesModel)
                    {
                        var user = metaData?.Where(x => x?.Id == thirdparty.ThirdPartyId).FirstOrDefault();
                        if (user is not null) // به دلیل خراب بودن دیتاهای داخل ریلیز
                        {
                            thirdparty.ThirdParty = user.FullName;
                        }
                    }
                }

            }
        }

        var total = new TotalPricesRequestRewardModel()
        {
            TotalOfferedPriceReward = requestRewards.Where(c => c.Type == RequestRewardType.Reward).Sum(oo => oo.OfferedPrice),
            TotalConfirmedPriceReward = requestRewards.Where(c => c.Type == RequestRewardType.Reward).Sum(oo => oo.ConfirmedPrice),
            TotalOfferedPriceFine = requestRewards.Where(c => c.Type == RequestRewardType.Fine).Sum(oo => oo.OfferedPrice),
            TotalConfirmedPriceFine = requestRewards.Where(c => c.Type == RequestRewardType.Fine).Sum(oo => oo.ConfirmedPrice),
        };

        return new GetFilteredRequestRewardsResponse
        {
            OtherData = total,
            Data = requestRewards,
            RowCount = getResponse.Value!.RowCount
        };
    }

    public async Task<Result<GetRequestRewardByIdResponse?>> GetRequestRewardById(GetRequestRewardByIdRequest request, CT ct)
    {
        _logger.LogInformation("Get Filtered Request Rewards {@request}", request);

        var isValidRequest = await request.IsValidAsync<GetRequestRewardByIdValidator, GetRequestRewardByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestRewardByIdResponse>(isValidRequest.Error!);

        var getRequestReward = await _mediator.Send(new GetRequestRewardByIdQuery(request.Id), ct);
        if (getRequestReward.IsFailure)
            return Result.Failure<GetRequestRewardByIdResponse>(getRequestReward.Error!);
        var requestReward = getRequestReward.Value!;

        Company? company = null;
        if (requestReward.CompanyId is not null && requestReward.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(requestReward.CompanyId, _mediator, ct);

        var thirdPartyIds = requestReward.RequestRewardThirdParties.Select(c => c.ThirdPartyId).ToList();
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        var thirdPartyModels = new List<GetRequestRewardByIdThirdPartyModel>();
        if (metaDataInfos is not null && metaDataInfos.Count > 0)
            foreach (var thirdParty in metaDataInfos)
            {
                thirdPartyModels.Add(new GetRequestRewardByIdThirdPartyModel()
                {
                    Id = requestReward.RequestRewardThirdParties.Where(x => x.ThirdPartyId == thirdParty!.Id).Select(x => x.Id).FirstOrDefault(),
                    ThirdPartyId = thirdParty!.Id,
                    FullName = thirdParty!.FullName
                });
            }

        var documentIds = requestReward.RequestRewardDocuments.Select(c => c.Id).ToList();
        var documents = new List<GetRequestRewardByIdDocumentModel>();
        if (requestReward.RequestRewardDocuments is not null && requestReward.RequestRewardDocuments.Count > 0)
            foreach (var doc in requestReward.RequestRewardDocuments)
            {
                documents.Add(new GetRequestRewardByIdDocumentModel()
                {
                    Id = doc!.Id,
                    Url = doc!.Url
                });
            }

        var getCurrency = await WebServicesLogic.CurrencyDataReceiver(requestReward.CurrencyId, _mediator, ct);
        var currency = getCurrency;

        var getCreator = await WebServicesLogic.UserDataReceiver(new List<long>() { requestReward.CreatorId }, null, _mediator, ct);
        var creator = getCreator?.FirstOrDefault();

        List<GetRequestRewardByIdProductModel>? products = new List<GetRequestRewardByIdProductModel>();
        if (requestReward.RequestRewardProducts is not null && requestReward.RequestRewardProducts.Count > 0)
        {
            var productIds = requestReward.RequestRewardProducts.Where(x => x.ProductId > 0).Select(x => x.ProductId).ToList();
            var getProducts = await WebServicesLogic.ProductsDataReceiver(productIds, _mediator, _pRepo, ct);

            var currencyIds = requestReward.RequestRewardProducts.Where(x => x.CurrencyId > 0).Select(x => x.CurrencyId).ToList();
            var getProductCurrencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

            foreach (var requestRewardProduct in requestReward.RequestRewardProducts)
            {
                var product = getProducts?.Where(x => x.Id == requestRewardProduct.ProductId).FirstOrDefault();
                var productCurrency = getProductCurrencies?.Where(x => x.Id == requestRewardProduct.CurrencyId).FirstOrDefault();

                products.Add(new GetRequestRewardByIdProductModel()
                {
                    Id = requestRewardProduct.Id,
                    Product = new GetRequestRewardByIdProduct()
                    {
                        Id = requestRewardProduct.Id,
                        ProductId = requestRewardProduct?.ProductId,
                        ProductName = product?.Name,
                        ProductCode = product?.Code,
                        CurrencyId = productCurrency?.Id,
                        CurrencyName = productCurrency?.Name,
                        Price = requestRewardProduct?.Price,
                        Count = requestRewardProduct?.Count
                    },
                    Currency = new GetRequestRewardByIdProductCurrencyModel()
                    {
                        Id = productCurrency?.Id,
                        Name = productCurrency?.Name
                    }
                });
            }
        }

        var response = new GetRequestRewardByIdResponse()
        {
            Id = requestReward.Id,
            RequestNumber = requestReward.Id,
            ConfirmedPrice = requestReward.ConfirmedPrice,
            CostCenter = new GetRequestRewardByIdCostCenterModel()
            {
                Id = requestReward.CostCenter!.Id,
                CostCenterName = requestReward.CostCenter!.CostCenterName,
            },
            Project = requestReward.Project is not null ? new GetRequestRewardByIdProjectModel()
            {
                Id = requestReward.Project!.Id,
                ProjectName = requestReward.Project!.ProjectName,
            } : null,
            ProjectOperation = requestReward.ProjectOperation is not null ? new GetRequestRewardByIdProjectOperationModel()
            {
                Id = requestReward.ProjectOperation!.Id,
                OperationInfoName = requestReward.ProjectOperation!.OperationInfo.OperationInfoName,
            } : null,
            ProjectOperationDetail = requestReward.ProjectOperationDetail is not null ? new GetRequestRewardByIdProjectOperationDetailModel()
            {
                Id = requestReward.ProjectOperationDetail!.Id,
                PublicName = requestReward.ProjectOperationDetail!.OperationLocation.PublicName,
            } : null,
            Currency = new GetRequestRewardByIdCurrencyModel()
            {
                Id = requestReward.CurrencyId,
                Currency = currency?.Name
            },
            Status = requestReward.Status,
            Type = requestReward.Type,
            Description = requestReward.Description,
            OfferedPrice = requestReward.OfferedPrice,
            RegistrationDate = requestReward.RegistrationDate,
            ThirdParties = thirdPartyModels,
            Documents = documents,
            Creator = creator?.FullName,
            Products = products,
            ManagerDescription = requestReward.ManagerDescription,
            CompanyId = requestReward.CompanyId,
            CompanyNameFa = company?.NameFa
        };

        return response;
    }

    public async Task<Result<CloseRequestRewardResponse?>> CloseRequestReward(CloseRequestRewardRequest request, CT ct)
    {
        _logger.LogInformation("Get Filtered Request Rewards {@request}", request);

        var isValidRequest = await request.IsValidAsync<CloseRequestRewardValidator, CloseRequestRewardRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CloseRequestRewardResponse>(isValidRequest.Error!);

        var requestReward = await _mediator.Send(new CloseRequestRewardCommand(request.Id), ct);
        if (requestReward.IsFailure)
            return Result.Failure<CloseRequestRewardResponse>(requestReward.Error!);

        await _unitOfWork.CommitAsync(ct);

        var response = requestReward.Value!.Adapt<CloseRequestRewardResponse>();
        return response;
    }

    public async Task<Result<PendingRequestRewardResponse?>> PendingRequestReward(PendingRequestRewardRequest request, CT ct)
    {
        _logger.LogInformation("Get Filtered Request Rewards {@request}", request);

        var isValidRequest = await request.IsValidAsync<PendingRequestRewardValidator, PendingRequestRewardRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<PendingRequestRewardResponse>(isValidRequest.Error!);

        var requestReward = await _mediator.Send(new PendingRequestRewardCommand(request.Id), ct);
        if (requestReward.IsFailure)
            return Result.Failure<PendingRequestRewardResponse>(requestReward.Error!);

        await _unitOfWork.CommitAsync(ct);

        var response = requestReward.Value!.Adapt<PendingRequestRewardResponse>();
        return response;
    }

    public async Task<Result<GetRequestRewardTypeResponse?>> GetRequestRewardType(GetRequestRewardTypeRequest request, CT ct)
    {
        var response = EnumExt.GetEnumObjectList<RequestRewardType>().Where(oo => oo.Code != (int)RequestRewardType.FiduciaryProductFine).ToList();
        return await Task.FromResult(new GetRequestRewardTypeResponse(response));
    }

    public async Task<Result<GetRequestRewardStatusResponse?>> GetRequestRewardStatus(GetRequestRewardStatusRequest request, CT ct)
    {
        return await Task.FromResult(new GetRequestRewardStatusResponse(EnumExt.GetEnumObjectList<RequestRewardStatus>()));
    }

    public async Task<Result<GetFilteredRequestRewardsExcelExporterResponse?>> GetFilteredRequestRewardsExcelExporter(GetFilteredRequestRewardsExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Get Filtered Request Rewards {@request}", request);
        var isValidRequest = await request.IsValidAsync<GetFilteredRequestRewardsExcelExporterValidator, GetFilteredRequestRewardsExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredRequestRewardsExcelExporterResponse>(isValidRequest.Error!);

        var userProfile = _userProfileService.GetProfileInfo();
        var userExtra = await _userProfileService.GetExtraInfo(ct);

        long? registerUserId = userProfile.UserId;
        long? managerId = null;

        var projectManager = await _userProfileService.HasUserAccessToAction(744, ct);
        if (projectManager.Status == ResponseStatusType.Ok)
        {
            managerId = userExtra.ThirdPartyId;
            registerUserId = null;
        }
        var systemManager = await _userProfileService.HasUserAccessToAction(745, ct);
        if (systemManager.Status == ResponseStatusType.Ok)
        {
            managerId = null;
            registerUserId = null;
        }

        var getResponse = await GetFilteredRequestRewardsHandler(
            request.Adapt<GetFilteredRequestRewardsRequest>(),
            registerUserId,
            managerId,
            ct);
        if (getResponse.IsFailure)
            return Result.Failure<GetFilteredRequestRewardsExcelExporterResponse>(getResponse.Error!);
        var requestRewards = getResponse.Value!.Data!;

        var thirdPartyIds = requestRewards.SelectMany(x => x.ThirdPartiesModel).Select(a => a.ThirdPartyId).Distinct().ToList();
        var metaData = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        var currencyIds = requestRewards.Where(x => x.CurrencyId is not null && x.CurrencyId > 0).Select(x => x.CurrencyId!.Value).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var companyIds = requestRewards?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = requestRewards.Adapt<List<GetFilteredRequestRewardsExcelExporterResponseModel>>();
        foreach (var item in data)
        {
            item.Currency = currencies?.Where(x => x.Id == item.CurrencyId).FirstOrDefault()?.Name;
            item.CompanyNameFa = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault()?.NameFa;
            item.ThirdPartiesModel = requestRewards?.Where(x => x.ThirdPartiesModel != null && x.ThirdPartiesModel.Count > 0 && x.Id == item.Id).FirstOrDefault()?.ThirdPartiesModel.ToList();
            if (item.ThirdPartiesModel is not null && item.ThirdPartiesModel.Count > 0)
                foreach (var thirdparty in item.ThirdPartiesModel)
                    item.ThirdParties += "," + metaData!.Where(x => x?.Id == thirdparty.ThirdPartyId).FirstOrDefault()?.FullName;
        }

        var file = new FileContentResult(RequestRewardExcels.RequestRewardToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"RequestRewards-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetFilteredRequestRewardsExcelExporterResponse(file);
    }

    public async Task<Result<GetFilteredRequestRewardsExcelEnumsResponse?>> GetFilteredRequestRewardsExcelEnums(GetFilteredRequestRewardsExcelEnumsRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<RequestRewardsExcelEnum>());
        return new GetFilteredRequestRewardsExcelEnumsResponse(response);
    }

    public async Task<Result<DeleteRequestRewardByIdsResponse?>> DeleteRequestRewardByIds(DeleteRequestRewardByIdsRequest request, CT ct)
    {
        var response = await _mediator.Send(new DeleteRequestRewardByIdsCommand(request.Ids), ct);
        if (response.IsBad())
            return response.Failure<DeleteRequestRewardByIdsResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteRequestRewardByIdsResponse(true);
    }
}
