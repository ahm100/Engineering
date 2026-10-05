using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Extensions.StringExtensions;
using Engineering.Application.Services.BillOfLadings.Contracts.GetCCHVersionsById;
using Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.ContractorContractHeaderStatusChanger;
using Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.CreateContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.DeleteContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.UpdateContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.DeleteContractorContract;
using Engineering.Application.Services.ContractorContracts.Commands.SetProjectOperationDetailServicesStatus;
using Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderGroupStatusChanger;
using Engineering.Application.Services.ContractorContracts.Contracts.ContractorContractHeaderStatusChanger;
using Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.DeleteContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.DeleteContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Contracts.GeOperationtFilteredSuggestedPriceHistories;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCCByHeaderId;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCCThirdParties;
using Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderInfo;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractsDate;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorPriceHistory;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractsByProjectId;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractHistory;
using Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractsByContractor;
using Engineering.Application.Services.ContractorContracts.Contracts.GetFltrProjectContractors;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorByContractorContractType;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport;
using Engineering.Application.Services.ContractorContracts.Contracts.GetServiceFilteredSuggestedPriceHistories;
using Engineering.Application.Services.ContractorContracts.Contracts.GetServicePriceHistory;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractDetailReports;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractReports;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsIntegratedProjectOperationDetailService;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsPartialProjectOperationDetailService;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedOperationContract;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedServiceContract;
using Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice;
using Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract;
using Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContractDetailPrices;
using Engineering.Application.Services.ContractorContracts.Queries.GetCCThirdParties;
using Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractById;
using Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractHeaderByIdIncludeless;
using Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractHeaderByIdNew;
using Engineering.Application.Services.ContractorContracts.Queries.GetContractorContractsDate;
using Engineering.Application.Services.ContractorContracts.Queries.GetContractsByProjectId;
using Engineering.Application.Services.ContractorContracts.Queries.GetFilteredContractorContractsByContractor;
using Engineering.Application.Services.ContractorContracts.Queries.GetFltrProjectContractors;
using Engineering.Application.Services.ContractorContracts.Queries.GetsContractorByContractorContractType;
using Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractDetailByIds;
using Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractDetailPrice;
using Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractHeaderByIds;
using Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractHeaderHistory;
using Engineering.Application.Services.ContractorContracts.Queries.GetsFilteredContractorContractDetailOperationPrice;
using Engineering.Application.Services.ContractorContracts.Queries.GetsFilteredContractorContractDetailReports;
using Engineering.Application.Services.ContractorContracts.Queries.GetsFilteredContractorContractDetailServicePrice;
using Engineering.Application.Services.ContractorContracts.Queries.GetsFilteredContractorContractReports;
using Engineering.Application.Services.ContractorContracts.Queries.GetsIntegratedProjectOperationDetailService;
using Engineering.Application.Services.ContractorContracts.Queries.GetsPartialProjectOperationDetailService;
using Engineering.Application.Services.ContractorContracts.Queries.GetsRequestedOperationContract;
using Engineering.Application.Services.ContractorContracts.Queries.GetsRequestedServiceContract;
using Engineering.Application.Services.Projects.Queries.GetProjectByIdNoIncluding;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetsCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredByIds;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ContractorContracts;

public partial class ContractorContractLogic : IContractorContractLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ContractorContractLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileService _userProfileService;
    private readonly IUserInfoService _userInfoService;
    private readonly IContractorContractHeaderRepository _repository;
    private readonly IContractorContractRepository _cCRepository;
    private readonly IContractorContractDetailServiceRepository _cCDetailServiceRepository;
    private readonly IDailyProjectOperationServiceRepository _dailyServiceRepository;
    private readonly IContractorContractDetailPriceHistoryRepository _contractorContractPriceHistoryRepo;
    private readonly IContractorContractDetailPriceRepository _contractorContractPriceRepo;
    private readonly IContractorContractHeaderVersionRepository _contractorContractHeaderVersionRepo;

    public ContractorContractLogic(
        IMediator mediator,
        ILogger<ContractorContractLogic> logger,
        IUnitOfWork unitOfWork,
        IUserProfileService userProfileService,
        IUserInfoService userInfoService,
        IContractorContractHeaderRepository repository,
        IContractorContractDetailPriceHistoryRepository contractorContractPriceHistoryRepo,
        IContractorContractDetailPriceRepository contractorContractPriceRepo,
        IContractorContractHeaderVersionRepository contractorContractHeaderVersionRepo,
        IContractorContractRepository cCRepository,
        IDailyProjectOperationServiceRepository dailyServiceRepository,
        IContractorContractDetailServiceRepository cCDetailServiceRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userProfileService = userProfileService;
        _userInfoService = userInfoService;
        _repository = repository;
        _contractorContractPriceHistoryRepo = contractorContractPriceHistoryRepo;
        _contractorContractPriceRepo = contractorContractPriceRepo;
        _contractorContractHeaderVersionRepo = contractorContractHeaderVersionRepo;
        _cCRepository = cCRepository;
        _dailyServiceRepository = dailyServiceRepository;
        _cCDetailServiceRepository = cCDetailServiceRepository;
    }

    public async Task<Result<CreateContractorContractResponse?>>
    CreateContractorContract(
        CreateContractorContractRequest request,
        CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<CreateContractorContractResponse?>()!;
        if (request is null)
        {
            return Result.Failure<CreateContractorContractResponse>(
                GlobalErrors.ValueIsNull);
        }

        var isValidRequest = await request.IsValidAsync<
            CreateContractorContractValidator,
            CreateContractorContractRequest>(ct);

        if (isValidRequest.IsFailure)
        {
            return Result.Failure<CreateContractorContractResponse>(
                isValidRequest.Error!);
        }

        var companyId = companyResult.Value;

        var currencyRes = await _mediator.Send(
            new GetCurrencyByIdQuery(request.CurrencyId),
            ct);

        if (currencyRes.IsBad())
            return currencyRes
                .Failure<CreateContractorContractResponse>()!;

        var projectRes = await _mediator.Send(
            new GetProjectByIdNoIncludingQuery(request.ProjectId),
            ct);

        if (projectRes.IsBad())
            return projectRes
                .Failure<CreateContractorContractResponse>()!;

        var project = projectRes.Value!;

        if (project.CompanyId != companyId)
            return Result.Failure<CreateContractorContractResponse>(
                ContractorContractErrors.ProjectDoesNotBelongToCompany);

        var projectCostCenter = project.ProjectCostCenters.FirstOrDefault();

        if (projectCostCenter is null)
        {
            return Result.Failure<CreateContractorContractResponse>(
                ContractorContractErrors.ProjectNoHaveCostCenter);
        }

        var contractorRes = await _mediator.Send(
            new GetWithSkillOnlyByIdsQuery(
                1,
                1,
                [request.ContractorId],
                null,
                false,
                null),
            ct);

        if (contractorRes.IsFailure ||
            contractorRes.Value?.Data?.Any() != true)
        {
            return Result.Failure<CreateContractorContractResponse>(
                ContractorContractErrors.InValidContractorId);
        }

        var headerValue = await _mediator.Send(
            new CreateContractorContractHeaderCommand(
                projectCostCenter.CostCenter,
                request.ContractorId,
                request.CurrencyId,
                request.Description,
                request.Urls,
                companyId),
            ct);

        if (headerValue.IsFailure)
            return Result.Failure<CreateContractorContractResponse>(headerValue.Error!);
        var value = headerValue.Value!;

        if (request.FixContractors?.Any() == true)
        {
            var fixContractors = await CreateFixContractors(
                project, request.ContractorId, value, request.FixContractors, ct);
            if (fixContractors.IsFailure)
                return Result.Failure<CreateContractorContractResponse>(fixContractors.Error!);
        }

        if (request.ServiceContractors?.Any() == true)
        {
            var serviceContractors = await CreateServiceContracts(
                project, request.ContractorId, value, request.ServiceContractors, ct);
            if (serviceContractors.IsFailure)
                return Result.Failure<CreateContractorContractResponse>(serviceContractors.Error!);
        }

        if (request.ItemPriceListContracts?.Any() == true)
        {
            var operationContracts = await CreateOperationBasedContracts(
                project,
                request.ContractorId,
                value,
                request.ItemPriceListContracts,
                ct);

            if (operationContracts.IsFailure)
                return Result.Failure<CreateContractorContractResponse>(
                    operationContracts.Error!);
        }

        if (request.ProfessionalWorkdayContractors?.Any() == true)
        {
            var personContractors = await CreateProfessionalWorkdayContracts(
                project, request.ContractorId, value, request.ProfessionalWorkdayContractors, ct);
            if (personContractors.IsFailure)
                return Result.Failure<CreateContractorContractResponse>(personContractors.Error!);
        }

        value.AddHistory();

        await _unitOfWork.CommitAsync(ct);
        return new CreateContractorContractResponse(true);
    }

    public async Task<Result<UpdateContractorContractResponse?>>
        UpdateContractorContract(
        UpdateContractorContractRequest request,
        CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<UpdateContractorContractResponse?>()!;
        if (request is null)
            return Result.Failure<UpdateContractorContractResponse>(
                GlobalErrors.ValueIsNull);

        var validation = await request.IsValidAsync<
        UpdateContractorContractValidator,
        UpdateContractorContractRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<UpdateContractorContractResponse>(
                validation.Error!);

        var companyId = companyResult.Value;

        var projectRes = await _mediator.Send(new GetProjectByIdNoIncludingQuery(request.ProjectId), ct);
        if (projectRes.IsBad()) return projectRes.Failure<UpdateContractorContractResponse>()!;
        var project = projectRes.Value!;

        if (project.CompanyId != companyId)
            return Result.Failure<UpdateContractorContractResponse>(
                ContractorContractErrors.ProjectDoesNotBelongToCompany);

        var response = await _mediator.Send(new GetContractorContractHeaderByIdIncludelessQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateContractorContractResponse>(response.Error!);
        var value = response.Value!;

        if (value.CompanyId != companyId)
            return Result.Failure<UpdateContractorContractResponse>(GlobalErrors.InvalidCompany);

        if (value.CurrencyId != request.CurrencyId)
        {
            var currency = await _mediator.Send(new GetCurrencyByIdQuery(request.CurrencyId), ct);
            if (currency.IsFailure)
                return Result.Failure<UpdateContractorContractResponse>(currency.Error!);
        }

        var updateCommand = await _mediator.Send(new UpdateContractorContractHeaderCommand(
            value, request.CurrencyId, request.Description, request.Urls), ct);
        if (updateCommand.IsFailure)
            return Result.Failure<UpdateContractorContractResponse>(updateCommand.Error!);

        if (request.UpdateFixContractors?.Any() == true)
        {
            var fixContractors = await UpdateFixContractors(
                request.ProjectId, request.ContractorId, value, request.UpdateFixContractors, companyId, ct);
            if (fixContractors.IsFailure)
                return Result.Failure<UpdateContractorContractResponse>(fixContractors.Error!);
        }

        if (request.CreateFixContractors?.Any() == true)
        {
            var fixContractors = await CreateFixContractors(
                project, request.ContractorId, value, request.CreateFixContractors, ct);
            if (fixContractors.IsFailure)
                return Result.Failure<UpdateContractorContractResponse>(fixContractors.Error!);
        }

        if (request.UpdateServiceContractors?.Any() == true)
        {
            var serviceContractors = await UpdateServiceContracts(
                request.ProjectId, request.ContractorId, value, request.UpdateServiceContractors, companyId, ct);
            if (serviceContractors.IsBad())
                return serviceContractors.Failure<UpdateContractorContractResponse>()!;
        }

        if (request.CreateServiceContractors?.Any() == true)
        {
            var serviceContractors = await CreateServiceContracts(
                project, request.ContractorId, value, request.CreateServiceContractors, ct);
            if (serviceContractors.IsFailure)
                return Result.Failure<UpdateContractorContractResponse>(serviceContractors.Error!);
        }

        if (request.CreateItemPriceListContracts?.Any() == true)
        {
            var operationContracts = await CreateOperationBasedContracts(
                project,
                request.ContractorId,
                value,
                request.CreateItemPriceListContracts,
                ct);

            if (operationContracts.IsFailure)
                return Result.Failure<UpdateContractorContractResponse>(
                    operationContracts.Error!);
        }

        if (request.UpdateItemPriceListContracts?.Any() == true)
        {
            var operationContracts = await UpdateOperationBasedContracts(
                project,
                request.ContractorId,
                value,
                request.UpdateItemPriceListContracts,
                ct);

            if (operationContracts.IsBad())
                return operationContracts
                    .Failure<UpdateContractorContractResponse>()!;
        }

        if (value.IsDeleted == false && value.ContractorContracts.All(x => x.IsDeleted == true))
        {
            var deleteHeaderlCommand = await _mediator.Send(new DeleteContractorContractHeaderCommand(value.Id), ct);
            if (deleteHeaderlCommand.IsFailure)
                return Result.Failure<UpdateContractorContractResponse>(deleteHeaderlCommand.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateContractorContractResponse(true);
    }

    public async Task<Result<UpdateContractorContractDetailPricesResponse?>>
    UpdateContractorContractDetailPrices(
        UpdateContractorContractDetailPricesRequest request,
        CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<UpdateContractorContractDetailPricesResponse?>()!;
        if (request is null)
            return Result.Failure<
                UpdateContractorContractDetailPricesResponse>(
                GlobalErrors.ValueIsNull);

        if (request.Details is null || request.Details.Count == 0)
            return Result.Failure<
                UpdateContractorContractDetailPricesResponse>(
                GlobalErrors.ValueIsNull);

        var detailIds = request.Details
            .Select(x => x.ContractorContractDetailId)
            .Distinct()
            .ToList();

        var validation = await request.IsValidAsync<
        UpdateContractorContractDetailPricesValidator,
        UpdateContractorContractDetailPricesRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<
                UpdateContractorContractDetailPricesResponse>(
                validation.Error!);

        var responseGetDetails = await _mediator.Send(new GetsContractorContractDetailByIdsQuery(detailIds), ct);
        if (responseGetDetails.IsFailure || responseGetDetails.Value is null || responseGetDetails.Value.Data is null)
            return Result.Failure<UpdateContractorContractDetailPricesResponse>(responseGetDetails.Error!);
        var detailValues = responseGetDetails.Value!.Data!;

        foreach (var detail in request.Details)
        {
            var detailValue = detailValues.FirstOrDefault(x => x.Id == detail.ContractorContractDetailId);
            if (detailValue is null)
                return Result.Failure<UpdateContractorContractDetailPricesResponse>(ContractorContractDetailErrors.ContractorContractDetailWithIdNotFound);

            if (detail.DeleteContractorContractDetailPriceIds?.Any() == true)
            {
                var prices = await DeleteContractorContractDetailPrices(detail.DeleteContractorContractDetailPriceIds, ct);
                if (prices.IsFailure)
                    return Result.Failure<UpdateContractorContractDetailPricesResponse>(prices.Error!);
            }

            int activeCount = 0;

            activeCount += detailValue.ContractorContractDetailPrices.Count(p => p.IsActive);

            var dPrices = detailValue.ContractorContractDetailPrices.ToList();

            var create = detail.CreatePrices;
            var update = detail.UpdatePrices;

            if (create != null && create.Any())
            {
                activeCount += create
                    .Count(p => p.IsActive);
            }

            if (update != null && update.Any())
            {
                foreach (var item in update)
                {
                    var match = dPrices.FirstOrDefault(x =>
                        x.Id == item.ContractorContractDetailPriceId);
                    if (match != null && !item.IsActive && match.IsActive)
                    {
                        activeCount += update
                            .Count(p => p.IsActive);
                    }
                }
            }

            if (activeCount > 1)
                return Result.Failure<UpdateContractorContractDetailPricesResponse>(ContractorContractErrors.TwoPriceAreActive);

            if (detail.UpdatePrices?.Any() == true)
            {
                var prices = await UpdateContractorContractDetailPrices(detailValue.ContractorContract, detailValue, detail.UpdatePrices, ct);
                if (prices.IsFailure)
                    return Result.Failure<UpdateContractorContractDetailPricesResponse>(prices.Error!);
            }

            if (detail.CreatePrices?.Any() == true)
            {
                var prices = await CreateContractorContractDetailPrices(detailValue.ContractorContract, detailValue, detail.CreatePrices, ct);
                if (prices.IsFailure)
                    return Result.Failure<UpdateContractorContractDetailPricesResponse>(prices.Error!);
            }

            if (detailValue.ContractorContractDetailPrices.Any(x => !x.IsDeleted) == false)
                return Result.Failure<UpdateContractorContractDetailPricesResponse>(ContractorContractDetailErrors.ContractorContractDetailNoHavePrices);

            if (detailValue.ContractorContractDetailPrices.Any(x => x.IsActive) == false)
                detailValue.ContractorContractDetailPrices.FirstOrDefault()!.SetActive();

            detailValue.SetTotalAmountClc();
            detailValue.ContractorContract.SetTotalAmountClc();
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateContractorContractDetailPricesResponse(true);
    }

    public async Task<Result<ContractorContractHeaderStatusChangerResponse?>> ContractorContractHeaderStatusChanger(
        ContractorContractHeaderStatusChangerModelRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<ContractorContractHeaderStatusChangerResponse?>()!;
        ContractorContractHeader? value = null;
        if (request.ContractorContractHeader is not null)
            value = request.ContractorContractHeader;
        else if (request.Id is not null && request.Id > 0)
        {
            var contractorContractQuery = await _mediator.Send(new GetContractorContractHeaderByIdIncludelessQuery(request.Id!.Value), ct);
            if (contractorContractQuery.IsFailure)
                return Result.Failure<ContractorContractHeaderStatusChangerResponse>(contractorContractQuery.Error!);
            value = contractorContractQuery.Value!;
        }
        else
            return Result.Failure<ContractorContractHeaderStatusChangerResponse>(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyIds);

        if (value.CompanyId != companyResult.Value)
            return Result.Failure<ContractorContractHeaderStatusChangerResponse>(GlobalErrors.InvalidCompany);

        if (request.Status == ContractorContractStatus.ProjectManagerConfirmed &&
            (value.Status == ContractorContractStatus.ProjectManagerResend || value.Status == ContractorContractStatus.New))
            value.SetStatus(ContractorContractStatus.ProjectManagerPending);

        if (request.Status == ContractorContractStatus.ManagementConfirmed && value.Status == ContractorContractStatus.ProjectManagerConfirmed)
            value.SetStatus(ContractorContractStatus.ManagementPending);

        var updated = await _mediator.Send(new ContractorContractHeaderStatusChangerCommand(
            value, request.Status, request.Description, request.Urls), ct);
        if (updated.IsFailure)
            return Result.Failure<ContractorContractHeaderStatusChangerResponse>(updated.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ContractorContractHeaderStatusChangerResponse(updated.Value!.Id);
    }

    public async Task<Result<ContractorContractHeaderGroupStatusChangerResponse?>>
    ContractorContractHeaderGroupStatusChanger(
        ContractorContractHeaderGroupStatusChangerRequest request,
        CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<ContractorContractHeaderGroupStatusChangerResponse?>()!;
        if (request is null)
            return Result.Failure<
                ContractorContractHeaderGroupStatusChangerResponse>(
                GlobalErrors.ValueIsNull);

        var validation = await request.IsValidAsync<
            ContractorContractHeaderGroupStatusChangerValidator,
            ContractorContractHeaderGroupStatusChangerRequest>(ct);

        if (validation.IsFailure)
        {
            return Result.Failure<
                ContractorContractHeaderGroupStatusChangerResponse>(
                validation.Error!);
        }

        using var transaction = new CreateTransaction();

        var contractorContractQuery = await _mediator.Send(
            new GetsContractorContractHeaderByIdsQuery(request.Ids),
            ct);

        if (contractorContractQuery.IsFailure)
            return Result.Failure<
                ContractorContractHeaderGroupStatusChangerResponse>(
                contractorContractQuery.Error!);

        var values = contractorContractQuery.Value!.Data!;

        if (values.Any(oo => oo.CompanyId != companyResult.Value))
            return Result.Failure<ContractorContractHeaderGroupStatusChangerResponse>(GlobalErrors.InvalidCompany);

        if (values.Count != request.Ids!.Count)
            return Result.Failure<
                ContractorContractHeaderGroupStatusChangerResponse>(
                SharedErrors.ItemNotFound);

        foreach (var item in values)
        {
            var newRequest =
                new ContractorContractHeaderStatusChangerModelRequest(
                    item.Id,
                    null,
                    request.Status,
                    request.Description,
                    request.Urls);

            var response =
                await ContractorContractHeaderStatusChanger(
                    newRequest,
                    ct);

            if (response.IsFailure)
                return Result.Failure<
                    ContractorContractHeaderGroupStatusChangerResponse>(
                    response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);

        transaction.Complete();

        return new ContractorContractHeaderGroupStatusChangerResponse(true);
    }

    public async Task<Result<DeleteContractorContractHeaderResponse?>> DeleteContractorContractHeader(
        DeleteContractorContractHeaderRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<DeleteContractorContractHeaderResponse?>()!;
        var getValue = await _mediator.Send(new GetContractorContractHeaderByIdQuery(request.Id), ct);
        if (getValue.IsFailure)
            return Result.Failure<DeleteContractorContractHeaderResponse>(getValue.Error!);
        var value = getValue.Value!;

        if (value.CompanyId != companyResult.Value)
            return Result.Failure<DeleteContractorContractHeaderResponse>(GlobalErrors.InvalidCompany);

        var user = _userProfileService.GetProfileInfo();
        if (user.UserId != value.CreatorId)
            return Result.Failure<DeleteContractorContractHeaderResponse>(ContractorContractErrors.InValidUser);

        var deleted = await _mediator.Send(new DeleteContractorContractHeaderCommand(request.Id), ct);
        if (deleted.IsFailure)
            return Result.Failure<DeleteContractorContractHeaderResponse>(deleted.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteContractorContractHeaderResponse(value.Id);
    }

    public async Task<Result<DeleteContractorContractResponse?>> DeleteContractorContract(
        DeleteContractorContractRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<DeleteContractorContractResponse?>()!;
        var getValue = await _mediator.Send(new GetContractorContractByIdQuery(request.Id, companyResult.Value), ct);
        if (getValue.IsFailure)
            return Result.Failure<DeleteContractorContractResponse>(getValue.Error!);
        var value = getValue.Value!;

        var user = _userProfileService.GetProfileInfo();
        if (user.UserId != value.CreatorId)
            return Result.Failure<DeleteContractorContractResponse>(ContractorContractErrors.InValidUser);
        if (value.ContractorContractHeader.Status != ContractorContractStatus.New)
            return Result.Failure<DeleteContractorContractResponse>(ContractorContractErrors.InValidStatusForDelete);

        var command = await _mediator.Send(new DeleteContractorContractCommand(request.Id, companyResult.Value), ct);
        if (command.IsFailure)
            return Result.Failure<DeleteContractorContractResponse>(command.Error!);

        var details = value.Details.ToList();
        foreach (var detail in details)
            if (detail.ContractorContractDetailServices is not null)
            {
                var services = detail.ContractorContractDetailServices.Select(x => x.ProjectOperationDetailContractorService).ToList();
                foreach (var service in services)
                {
                    var newStatus = await _mediator.Send(
                        new SetProjectOperationDetailServicesStatusCommand(
                            service.Id,
                            ContractorServiceStatus.New),
                        ct);

                    if (newStatus.IsFailure)
                        return Result.Failure<DeleteContractorContractResponse>(
                            newStatus.Error!);
                }
            }

        await _unitOfWork.CommitAsync(ct);
        return new DeleteContractorContractResponse(command.Value!.Id);
    }


    public async Task<Result<GetsIntegratedProjectOperationDetailServiceResponse?>> GetsIntegratedProjectOperationDetailService(
        GetsIntegratedProjectOperationDetailServiceRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetsIntegratedProjectOperationDetailServiceResponse?>()!;
        var validation = await request.IsValidAsync<
            GetsIntegratedProjectOperationDetailServiceValidator,
            GetsIntegratedProjectOperationDetailServiceRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<GetsIntegratedProjectOperationDetailServiceResponse>(
                validation.Error!);
        var companyId = companyResult.Value;
        var servicesQuery = await _mediator.Send(new GetsIntegratedProjectOperationDetailServiceQuery(
            null, request.CostCenterId, request.ProjectId, request.ContractorId,
            request.ProjectOperationIds, request.ServiceInfoIds, request.FilterData, companyId, request.OrderBy, 0, 0), ct);
        if (servicesQuery.IsFailure)
            return Result.Failure<GetsIntegratedProjectOperationDetailServiceResponse>(servicesQuery.Error!);
        var notCollectiveServices = servicesQuery.Value!.Data!.Where(x => x.ProjectServiceDetail is null).Select(x => x.OperationInfoService.ServiceInfo).Distinct().ToList();
        var collectiveServices = servicesQuery.Value!.Data!.Where(x => x.ProjectServiceDetail is not null).Select(x => x.ProjectServiceDetail!.ProjectService).Distinct().ToList();
        var result = new List<GetsIntegratedProjectOperationDetailServiceModel>();

        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver([request.ContractorId!.Value], null, null, _mediator, ct);

        var opMeasurements = notCollectiveServices.SelectMany(c => c.OperationInfoServices.Select(x => x.OperationInfo.UnitOfMeasurementId)).ToList();
        var siMeasurements = notCollectiveServices.Select(c => c.UnitOfMeasurementId).ToList();
        var notCollectiveMeasureUnitIds = opMeasurements.Concat(siMeasurements).Distinct().ToList();
        var opCollectiveMeasurements = collectiveServices.SelectMany(c => c.ProjectServiceDetails.Select(s => s.OperationInfoService.OperationInfo.UnitOfMeasurementId)).ToList();
        var siCollectiveMeasurements = collectiveServices.Select(c => c.ServiceInfo.UnitOfMeasurementId).ToList();
        var collectiveMeasureUnitIds = opCollectiveMeasurements.Concat(siCollectiveMeasurements).Distinct().ToList();
        var measureUnitIds = notCollectiveMeasureUnitIds.Concat(collectiveMeasureUnitIds).Distinct().ToList();
        var measurments = await WebServicesLogic.MeasurementDataReceiver(measureUnitIds, _mediator, ct);

        if (notCollectiveServices is not null && notCollectiveServices.Count > 0)
            foreach (var notCollectiveService in notCollectiveServices)
            {
                var services = notCollectiveService.OperationInfoServices.SelectMany(x => x.ProjectOperationDetailContractorServices).ToList();
                var operationInfos = services.Select(x => x.OperationInfoService.OperationInfo).Distinct().ToList();
                var operationCodes = operationInfos.Select(x => x.OperationInfoCode).Distinct().ToList();
                var operationInfoCodes = StringSeparator.WithDash(operationCodes);

                List<string>? strings = [];
                foreach (var operationInfo in operationInfos)
                    strings.Add(operationInfo.OperationInfoName + "(" + measurments?.FirstOrDefault(x => x.Id == operationInfo.UnitOfMeasurementId)?.Name + ")");
                var operationInfoNames = StringSeparator.WithDash(strings);

                var operationLocations = services.Select(x => x.ProjectOperationDetail.OperationLocation.PrivateName).Distinct().ToList();
                var projectOperationDetails = services.Select(x => x.ProjectOperationDetail).ToList();
                result.Add(new GetsIntegratedProjectOperationDetailServiceModel()
                {
                    StartDate = TimeCalculator.DatePiker(projectOperationDetails.Min(p => p.StartDate)),
                    EndDate = TimeCalculator.DatePiker(projectOperationDetails.Max(p => p.EndDate)),
                    ServiceInfoId = notCollectiveService.Id,
                    ServiceInfoCode = notCollectiveService.ServiceInfoCode,
                    ServiceInfoName = notCollectiveService.ServiceInfoName,
                    FinalAmount = projectOperationDetails.Distinct().Sum(x => x.FinalAmount),
                    OperationInfoNames = operationInfoNames,
                    OperationInfoCodes = operationInfoCodes,
                    OperationLocationPrivateNames = StringSeparator.WithDash(operationLocations),
                    ServiceInfoVolume = services.Sum(x => x.Volume),
                    ServiceInfoUnitOfMeasurement = measurments?.FirstOrDefault(c => c.Id == notCollectiveService.UnitOfMeasurementId)?.Name,
                    ContractorId = request.ContractorId,
                    Contractor = contractors?.FirstOrDefault(c => c!.Id == request.ContractorId)?.FullName,
                    CollectiveService = false,
                    HasExperts = services.Any(x => x.ProjectOperationDetailContractorExperts.Any())
                });
            }

        if (collectiveServices is not null && collectiveServices.Count > 0)
            foreach (var collectiveService in collectiveServices)
            {
                var services = collectiveServices.SelectMany(c => c.ProjectServiceDetails.SelectMany(s => s.ProjectOperationDetailContractorServices)).ToList();
                var operationInfos = services.Select(x => x.OperationInfoService.OperationInfo).Distinct().ToList();
                var operationCodes = operationInfos.Select(x => x.OperationInfoCode).Distinct().ToList();
                var operationInfoCodes = StringSeparator.WithDash(operationCodes);

                List<string>? strings = [];
                foreach (var operationInfo in operationInfos)
                    strings.Add(operationInfo.OperationInfoName + "(" + measurments?.FirstOrDefault(x => x.Id == operationInfo.UnitOfMeasurementId)?.Name + ")");
                var operationInfoNames = StringSeparator.WithDash(strings);

                var operationLocations = services.Select(x => x.ProjectOperationDetail.OperationLocation.PrivateName).Distinct().ToList();
                var projectOperationDetails = services.Select(x => x.ProjectOperationDetail).ToList();
                result.Add(new GetsIntegratedProjectOperationDetailServiceModel()
                {
                    StartDate = TimeCalculator.DatePiker(projectOperationDetails.Min(p => p.StartDate)),
                    EndDate = TimeCalculator.DatePiker(projectOperationDetails.Max(p => p.EndDate)),
                    FinalAmount = projectOperationDetails.Sum(x => x.FinalAmount),
                    ServiceInfoId = collectiveService.ServiceInfo.Id,
                    ProjectServiceId = collectiveService.Id,
                    ServiceInfoCode = collectiveService.ServiceInfo.ServiceInfoCode,
                    ServiceInfoName = collectiveService.ServiceInfo.ServiceInfoName,
                    OperationInfoNames = operationInfoNames,
                    OperationInfoCodes = operationInfoCodes,
                    OperationLocationPrivateNames = StringSeparator.WithDash(operationLocations),
                    ServiceInfoVolume = collectiveService.Volume,
                    ServiceInfoUnitOfMeasurement = measurments?.FirstOrDefault(c => c.Id == collectiveService.ServiceInfo.UnitOfMeasurementId)?.Name,
                    ContractorId = request.ContractorId,
                    Contractor = contractors?.FirstOrDefault(c => c!.Id == request.ContractorId)?.FullName,
                    CollectiveService = true,
                    HasExperts = services.Any(x => x.ProjectOperationDetailContractorExperts.Any())
                });
            }

        var count = result.Count;
        var data = result.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsIntegratedProjectOperationDetailServiceResponse(data, count);
    }

    public async Task<Result<GetsPartialProjectOperationDetailServiceResponse?>>
    GetsPartialProjectOperationDetailService(
        GetsPartialProjectOperationDetailServiceRequest request,
        CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetsPartialProjectOperationDetailServiceResponse?>()!;
        if (request is null)
        {
            return Result.Failure<GetsPartialProjectOperationDetailServiceResponse>(
                GlobalErrors.ValueIsNull);
        }

        var isValidRequest = await request.IsValidAsync<
            GetsPartialProjectOperationDetailServiceValidator,
            GetsPartialProjectOperationDetailServiceRequest>(ct);

        if (isValidRequest.IsFailure)
        {
            return Result.Failure<GetsPartialProjectOperationDetailServiceResponse>(
                isValidRequest.Error!);
        }

        var companyId = companyResult.Value;

        var servicesQuery = await _mediator.Send(
            new GetsPartialProjectOperationDetailServiceQuery(
                request.CostCenterId,
                request.ProjectId,
                request.ContractorId,
                request.ServiceInfoId,
                request.ProjectOperationDetailServiceIds,
                request.FilterData,
                companyId),
            ct);

        if (servicesQuery.IsFailure)
        {
            return Result.Failure<GetsPartialProjectOperationDetailServiceResponse>(
                servicesQuery.Error!);
        }

        var data = servicesQuery.Value?.Data ??
                   new List<GetsPartialProjectOperationDetailServiceModel>();

        var measureUnitIds = data
            .Select(x => x.UnitOfMeasurementId)
            .Where(x => x > 0)
            .Distinct()
            .ToList();

        var measurements = measureUnitIds.Count > 0
            ? await WebServicesLogic.MeasurementDataReceiver(
                measureUnitIds,
                _mediator,
                ct)
            : null;

        data.ForEach(m =>
        {
            m.ServiceInfoUnitOfMeasurement = measurements?
                .FirstOrDefault(x => x.Id == m.UnitOfMeasurementId)
                ?.Name;
        });

        return new GetsPartialProjectOperationDetailServiceResponse(data);
    }

    public async Task<Result<GetsContractorContractDetailPriceResponse?>> GetsContractorContractDetailPrice(
        GetsContractorContractDetailPriceRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetsContractorContractDetailPriceResponse?>()!;
        if (request is null)
            return Result.Failure<
                GetsContractorContractDetailPriceResponse>(
                GlobalErrors.ValueIsNull);

        var validation = await request.IsValidAsync<
            GetsContractorContractDetailPriceValidator,
            GetsContractorContractDetailPriceRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<
                GetsContractorContractDetailPriceResponse>(
                validation.Error!);

        var servicesQuery = await _mediator.Send(new GetsContractorContractDetailPriceQuery(
            request.ContractorContractHedearId,
            request.StartDate,
            request.EndDate,
            request.FilterData,
            request.OrderBy,
            request.PageIndex,
            request.PageSize), ct);
        if (servicesQuery.IsFailure)
            return Result.Failure<GetsContractorContractDetailPriceResponse>(servicesQuery.Error!);
        var values = servicesQuery.Value!.Data!;
        var measureUnitIds = values!.SelectMany(x => new[] { x.ServiceInfoUnitOfMeasurementId, x.OperationInfoUnitOfMeasurementId }).Distinct().ToList();
        var measurments = await WebServicesLogic.MeasurementDataReceiver(measureUnitIds, _mediator, ct);

        values.ForEach(m =>
        {
            m.ServiceInfoUnitOfMeasurement = measurments?.Where(oo => oo.Id == m.ServiceInfoUnitOfMeasurementId).FirstOrDefault()?.Name;
            m.OperationInfoUnitOfMeasurement = measurments?.Where(oo => oo.Id == m.OperationInfoUnitOfMeasurementId).FirstOrDefault()?.Name;
        });

        return new GetsContractorContractDetailPriceResponse(values, servicesQuery.Value.RowCount);
    }

    public async Task<Result<GetOperationFilteredSuggestedPriceHistoriesResponse?>> GetsOperationFilteredSuggestedPriceHistory(
        GetOperationFilteredSuggestedPriceHistoriesRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetOperationFilteredSuggestedPriceHistoriesResponse?>()!;
        if (request is null)
            return Result.Failure<
                GetOperationFilteredSuggestedPriceHistoriesResponse>(
                GlobalErrors.ValueIsNull);

        var validation = await request.IsValidAsync<
            GetOperationFilteredSuggestedPriceHistoriesValidator,
            GetOperationFilteredSuggestedPriceHistoriesRequest>(ct);

        if (validation.IsFailure)

            return Result.Failure<
                GetOperationFilteredSuggestedPriceHistoriesResponse>(
                validation.Error!);

        var companyId = companyResult.Value;
        var response = await _mediator.Send(new GetsFilteredContractorContractDetailOperationPriceQuery(request.ProjectOperationId, request.OperationInfoId,
            request.StartDate, request.EndDate, request.ContractorId, request.CostCenterId, request.ProjectId, request.FilterData, companyId,
            request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetOperationFilteredSuggestedPriceHistoriesResponse>(response.Error!);

        var value = response.Value!.Data!;
        var header = value.Select(c => c.ContractorContractDetail).First();
        var measureId = header.ProjectOperation!.UnitOfMeasurementId;
        var queryMeasure = await WebServicesLogic.MeasurementDataReceiver([measureId], _mediator, ct);
        var measureUnit = queryMeasure?.FirstOrDefault();

        var details = new List<GetOperationFilteredSuggestedPriceHistoriesDetailModel>();
        foreach (var price in value)
        {
            var queryCurrency = await WebServicesLogic.CurrencyDataReceiver(price.CurrencyId, _mediator, ct);
            var queryThirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver([price.ContractorContractDetail.ContractorContract.ContractorContractHeader.ContractorId], null, null, _mediator, ct);
            var thirdParty = queryThirdParties?.FirstOrDefault();
            details.Add(new GetOperationFilteredSuggestedPriceHistoriesDetailModel
            {
                ContractorName = thirdParty?.FullName,
                CostCenterName = price.ContractorContractDetail.ProjectOperation?.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                StartDate = price.StartDate,
                EndDate = price.EndDate,
                RequestNumber = price.ContractorContractDetail.ContractorContract.Id.ToString(),
                ProjectName = price.ContractorContractDetail.ProjectOperation?.Project.ProjectName,
                WorkLoad = price.ContractorContractDetail.WorkLoad,
                Currency = queryCurrency?.Name,
                UnitPrice = (price.Price / price.ContractorContractDetail.WorkLoad)
            });
        }

        var detail = new GetOperationFilteredSuggestedPriceHistoriesModel()
        {
            Data = details,
            RowCount = response.Value!.RowCount
        };

        return new GetOperationFilteredSuggestedPriceHistoriesResponse
        {
            OperationInfoName = header.ProjectOperation.OperationInfo.OperationInfoName,
            OperationInfoCode = header.ProjectOperation.OperationInfo.OperationInfoCode,
            MeasureUnitId = header.ProjectOperation.UnitOfMeasurementId,
            MeasureUnit = measureUnit?.Name,
            ProjectOperationId = header.ProjectOperation.Id,
            WorkLoad = header.ProjectOperation.Workload,
            Detail = detail
        };
    }

    public async Task<Result<GetContractorContractsDateResponse?>> GetContractorContractsDate(
        GetContractorContractsDateRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractorContractsDateResponse?>()!;
        var response = await _mediator.Send(new GetContractorContractsDateQuery(request.ProjectId, request.ContractorId, companyResult.Value), ct);
        if (response.IsFailure)
            return Result.Failure<GetContractorContractsDateResponse>(response.Error!);
        return response.Value!;
    }

    public async Task<Result<GetServiceFilteredSuggestedPriceHistoriesResponse?>> GetsServiceFilteredSuggestedPriceHistory(
        GetServiceFilteredSuggestedPriceHistoriesRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetServiceFilteredSuggestedPriceHistoriesResponse?>()!;
        if (request is null)
            return Result.Failure<
                GetServiceFilteredSuggestedPriceHistoriesResponse>(
                GlobalErrors.ValueIsNull);

        var validation = await request.IsValidAsync<
            GetServiceFilteredSuggestedPriceHistoriesValidator,
            GetServiceFilteredSuggestedPriceHistoriesRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<
                GetServiceFilteredSuggestedPriceHistoriesResponse>(
                validation.Error!);

        var companyId = companyResult.Value;
        var response = await _mediator.Send(new GetsFilteredContractorContractDetailServicePriceQuery(request.ProjectOperationServiceId, request.ServiceInfoId,
            request.StartDate, request.EndDate, request.ContractorId, request.CostCenterId, request.ProjectId, request.FilterData, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetServiceFilteredSuggestedPriceHistoriesResponse>(response.Error!);
        var value = response.Value!.Data!;

        List<long>? ids = null;
        var services = value.SelectMany(c => c.ContractorContractDetail.ContractorContractDetailServices).ToList();
        var operationMeasures = services.Select(x => x.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId).ToList();
        ids?.AddRange(operationMeasures);
        var serviceMeasures = services.Select(x => x.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.UnitOfMeasurementId).ToList();
        ids?.AddRange(serviceMeasures);

        var measureUnit = await WebServicesLogic.MeasurementDataReceiver(ids, _mediator, ct);

        var details = new List<GetServiceFilteredSuggestedPriceHistoriesDetailModel>();
        foreach (var price in value)
        {
            var queryCurrency = await WebServicesLogic.CurrencyDataReceiver(price.CurrencyId, _mediator, ct);
            var currency = queryCurrency?.Name;
            var queryThirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver([price.ContractorContractDetail.ContractorContract.ContractorContractHeader.ContractorId], null, null, _mediator, ct);
            var thirdParty = queryThirdParties?.FirstOrDefault();

            details.Add(new GetServiceFilteredSuggestedPriceHistoriesDetailModel
            {
                ContractorName = thirdParty?.FullName,
                CostCenterName = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService?.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                StartDate = price.StartDate,
                EndDate = price.EndDate,
                RequestNumber = price.ContractorContractDetail.ContractorContract.Id.ToString(),
                ProjectName = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.ProjectName,
                WorkLoad = price.ContractorContractDetail.WorkLoad,
                Currency = currency,
                UnitPrice = (price.Price / price.ContractorContractDetail.WorkLoad),
                TotalPrice = price.Price

            });
        }

        var detail = new GetServiceFilteredSuggestedPriceHistoriesModel()
        {
            Data = details,
            RowCount = response.Value!.RowCount
        };

        return new GetServiceFilteredSuggestedPriceHistoriesResponse
        {
            OperationInfoName = services.FirstOrDefault()?.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
            OperationInfoCode = services.FirstOrDefault()?.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode,
            OperationWorkLoad = services.FirstOrDefault()?.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Workload,
            OperationMeasureUnitId = services.FirstOrDefault()?.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId,
            OperationMeasureUnit = measureUnit?.Where(c => c.Id.Equals(services.FirstOrDefault()?.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId)).FirstOrDefault()?.Name,
            ProjectOperationId = services.FirstOrDefault()?.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Id,
            ServiceWorkLoad = services.FirstOrDefault()?.ProjectOperationDetailContractorService.Volume,
            ServiceInfoCode = services.FirstOrDefault()?.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode,
            ServiceInfoName = services.FirstOrDefault()?.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
            ServiceMeasureUnitId = services.FirstOrDefault()?.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
            ServiceMeasureUnit = measureUnit?.Where(c => c.Id.Equals(services.FirstOrDefault()?.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId)).FirstOrDefault()?.Name,
            Detail = detail,
        };
    }

    public async Task<Result<GetServicePriceHistoryResponse?>> GetServicePriceHistory(
    GetServicePriceHistoryRequest request,
    CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetServicePriceHistoryResponse?>()!;
        if (request is null)
        {
            return Result.Failure<GetServicePriceHistoryResponse>(
                GlobalErrors.ValueIsNull);
        }

        var isValidRequest = await request.IsValidAsync<
            GetServicePriceHistoryValidator,
            GetServicePriceHistoryRequest>(ct);

        if (isValidRequest.IsFailure)
        {
            return Result.Failure<GetServicePriceHistoryResponse>(
                isValidRequest.Error!);
        }

        var companyId = companyResult.Value;

        var response = await _mediator.Send(
            new GetsFilteredContractorContractDetailServicePriceQuery(
                null,
                request.ServiceInfoId,
                null,
                null,
                null,
                null,
                null,
                null,
                companyId,
                null,
                request.PageIndex,
                request.PageSize),
            ct);

        if (response.IsFailure)
        {
            return Result.Failure<GetServicePriceHistoryResponse>(
                response.Error!);
        }

        var values = response.Value?.Data ??
                     new List<ContractorContractDetailPrice>();

        var details = new List<GetServicePriceHistoryModel>();

        foreach (var price in values)
        {
            var podcs = price.ContractorContractDetail
                .ContractorContractDetailServices
                .Select(x => x.ProjectOperationDetailContractorService)
                .FirstOrDefault(x =>
                    x is not null &&
                    x.Type == PODContractorServiceType.ServiceBased &&
                    x.OperationInfoService is not null);

            var project = podcs?
                .ProjectOperationDetail
                .ProjectOperation
                .Project;

            var costCenter = project?
                .ProjectCostCenters
                .FirstOrDefault()?
                .CostCenter;

            var currencyResult =
                await WebServicesLogic.CurrencyDataReceiver(
                    price.CurrencyId,
                    _mediator,
                    ct);

            string? contractorName = null;

            if (podcs?.ContractorId is > 0)
            {
                var contractors =
                    await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(
                        [podcs.ContractorId.Value],
                        null,
                        null,
                        _mediator,
                        ct);

                contractorName = contractors?
                    .FirstOrDefault()?
                    .FullName;
            }

            var workLoad = price.ContractorContractDetail.WorkLoad;

            details.Add(new GetServicePriceHistoryModel
            {
                Id = price.Id,
                ServiceInfoId = request.ServiceInfoId,

                ContractorName = contractorName,

                CostCenterName = costCenter?.CostCenterName,

                ProjectName = project?.ProjectName,

                StartDate = price.StartDate,
                EndDate = price.EndDate,

                RequestNumber = price.ContractorContractDetail
                    .ContractorContract
                    .Id
                    .ToString(),

                WorkLoad = workLoad,

                Currency = currencyResult?.Name,

                UnitPrice = workLoad > 0
                    ? price.Price / workLoad
                    : null,

                TotalPrice = price.Price
            });
        }

        return new GetServicePriceHistoryResponse(
            details,
            response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetFilteredContractorContractHeaderInfoResponse?>> GetsFilteredContractorContractHeaderInfo(
        GetFilteredContractorContractHeaderInfoRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetFilteredContractorContractHeaderInfoResponse?>()!;
        if (request is null)
            return Result.Failure<
                GetFilteredContractorContractHeaderInfoResponse>(
                GlobalErrors.ValueIsNull);

        var validation = await request.IsValidAsync<
            GetFilteredContractorContractHeaderInfoValidator,
            GetFilteredContractorContractHeaderInfoRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<
                GetFilteredContractorContractHeaderInfoResponse>(
                validation.Error!);

        var companyId = companyResult.Value;
        var queryServices = await _mediator.Send(new GetsIntegratedProjectOperationDetailServiceQuery(
            request.ProjectOperationDetailServiceIds, null, null, null, null, null, null, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (queryServices.IsFailure)
            return Result.Failure<GetFilteredContractorContractHeaderInfoResponse>(queryServices.Error!);
        var services = queryServices.Value!.Data!;

        var contractorId = GetContractorIds(services);
        if (contractorId.Distinct().Count() != 1)
            return Result.Failure<GetFilteredContractorContractHeaderInfoResponse>(ContractorContractErrors.InValidContractorIds);

        var queryContractor = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorId, null, null, _mediator, ct);
        var contractor = queryContractor?.SingleOrDefault();

        var filteredServices = services.Where(c => request.FilterData == null || c.OperationInfoService.ServiceInfo.ServiceInfoName.Contains(request.FilterData) || c.OperationInfoService.ServiceInfo.ServiceInfoCode.Contains(request.FilterData) ||
            c.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName.Contains(request.FilterData) || c.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode.Contains(request.FilterData)).ToList();

        var operationUnitOfMeasurements = filteredServices.Select(c => c.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId).Distinct().ToList();
        var serviceInfoUnitOfMeasurements = filteredServices.Select(c => c.OperationInfoService.ServiceInfo.UnitOfMeasurementId).Distinct().ToList();
        var measureUnitIds = operationUnitOfMeasurements.Concat(operationUnitOfMeasurements).Concat(serviceInfoUnitOfMeasurements).Distinct().ToList();
        var measurments = await WebServicesLogic.MeasurementDataReceiver(measureUnitIds, _mediator, ct);

        var response = new GetFilteredContractorContractHeaderInfoResponse
        {
            Contractor = contractor?.FullName,
            ContractorId = contractor?.Id,
            StartDate = services.Where(c => c.ProjectOperationDetail.StartDate != null).Select(c => c.ProjectOperationDetail.StartDate).Min(),
            EndDate = services.Where(c => c.ProjectOperationDetail.EndDate != null).Select(c => c.ProjectOperationDetail.EndDate).Max(),
            RowCount = filteredServices.Count
        };

        return response;
    }

    public async Task<Result<GetsRequestedOperationContractResponse?>>
    GetsRequestedOperationContract(
        GetsRequestedOperationContractRequest request,
        CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetsRequestedOperationContractResponse?>()!;
        if (request is null)
            return Result.Failure<
                GetsRequestedOperationContractResponse>(
                GlobalErrors.ValueIsNull);

        if (request.ProjectOperationDetailServiceIds is null ||
            request.ProjectOperationDetailServiceIds.Count == 0)
            return Result.Failure<
                GetsRequestedOperationContractResponse>(
                GlobalErrors.ValueIsNull);

        var requestedIds =
            request.ProjectOperationDetailServiceIds;

        if (requestedIds.Any(x => x <= 0) ||
            requestedIds.Count != requestedIds.Distinct().Count())
        {
            return Result.Failure<
                GetsRequestedOperationContractResponse>(
                ContractorContractErrors
                    .InValidProjectOperationServiceId);
        }

        var companyId = companyResult.Value;

        var queryServices =
            await _mediator.Send(
                new GetsRequestedOperationContractQuery(
                    requestedIds,
                    companyId),
                ct);

        if (queryServices.IsFailure)
            return Result.Failure<
                GetsRequestedOperationContractResponse>(
                queryServices.Error!);

        var services =
            queryServices.Value!.Data!;
        if (services.Count != requestedIds.Count ||
            services.Any(x =>
                x.Type !=
                PODContractorServiceType.OperationBased) ||
            services.Any(x =>
                !requestedIds.Contains(x.Id)))
            return Result.Failure<
                GetsRequestedOperationContractResponse>(
                ContractorContractErrors
                    .InValidProjectOperationServiceId);

        var contractorId =
            GetContractorIds(services);

        if (contractorId.Distinct().Count() != 1)
            return Result.Failure<
                GetsRequestedOperationContractResponse>(
                ContractorContractErrors
                    .InValidContractorIds);


        var queryContractor = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorId, null, null, _mediator, ct);
        var contractor = queryContractor?.SingleOrDefault();

        var measureUnitIds = services.Select(c => c.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId).Distinct().ToList();
        var measurments = await WebServicesLogic.MeasurementDataReceiver(measureUnitIds, _mediator, ct);

        var response = new GetsRequestedOperationContractResponse
        {
            Contractor = contractor?.FullName,
            ContractorId = contractor?.Id,
            StartDate = services.Where(c => c.ProjectOperationDetail.StartDate != null).Select(c => c.ProjectOperationDetail.StartDate).Min(),
            EndDate = services.Where(c => c.ProjectOperationDetail.EndDate != null).Select(c => c.ProjectOperationDetail.EndDate).Max(),
        };

        foreach (var item in services)
        {
            if (response.Details.Any(x => x.ProjectOperationId == item.ProjectOperationDetail.ProjectOperation.Id))
                continue;

            var resultItem = item.Adapt<GetsRequestedOperationContractModel>();
            resultItem.OperationUnitOfMeasurement = measurments?.Where(c => c.Id == resultItem.UnitOfMeasurementId).Select(c => c.Name).FirstOrDefault()!;
            response.Details.Add(resultItem);
        }
        return response;
    }

    public async Task<Result<GetsRequestedServiceContractResponse?>> GetsRequestedServiceContract(
        GetsRequestedServiceContractRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetsRequestedServiceContractResponse?>()!;
        if (request is null)
            return Result.Failure<GetsRequestedServiceContractResponse>(
                GlobalErrors.ValueIsNull);

        var companyId = companyResult.Value;
        var queryServices = await _mediator.Send(new GetsRequestedServiceContractQuery(
            request.ProjectId, request.ServiceIds, request.ContractorId, companyId), ct);
        if (queryServices.IsFailure)
            return Result.Failure<GetsRequestedServiceContractResponse>(queryServices.Error!);
        var services = queryServices.Value!.Data!;

        List<ProjectOperationDetailContractorService>? contractorServices = [];
        foreach (var item in services)
        {
            if (contractorServices.Any(x => x.OperationInfoService.ServiceInfo.Id == item.OperationInfoService.ServiceInfo.Id &&
            x.ContractorId == item.ContractorId && x.ProjectOperationDetail.ProjectOperation.Project.Id == item.ProjectOperationDetail.ProjectOperation.Project.Id))
                continue;
            else
                contractorServices.Add(item);
        }

        var contractorId = GetContractorIds(contractorServices);
        if (contractorId.Distinct().Count() != 1)
            return Result.Failure<GetsRequestedServiceContractResponse>(ContractorContractErrors.InValidContractorIds);
        var queryContractor = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorId, null, null, _mediator, ct);
        var contractor = queryContractor!.SingleOrDefault();

        var measurementIds = services.Select(c => c.OperationInfoService.ServiceInfo.UnitOfMeasurementId).Distinct().ToList();
        measurementIds.AddRange(services.Select(c => c.OperationInfoService.OperationInfo.UnitOfMeasurementId).Distinct().ToList());
        measurementIds.AddRange(services.Select(c => c.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId).Distinct().ToList());
        var measurments = await WebServicesLogic.MeasurementDataReceiver(measurementIds, _mediator, ct);

        var response = new GetsRequestedServiceContractResponse
        {
            Contractor = contractor?.FullName,
            ContractorId = contractor?.Id,
            StartDate = TimeCalculator.DatePiker(services.Where(c => c.ProjectOperationDetail.StartDate != null).Select(c => c.ProjectOperationDetail.StartDate).Min()),
            EndDate = TimeCalculator.DatePiker(services.Where(c => c.ProjectOperationDetail.EndDate != null).Select(c => c.ProjectOperationDetail.EndDate).Max()),
            RowCount = services.Count
        };

        var result = new List<GetsRequestedServiceContractModel>();
        foreach (var item in contractorServices)
        {
            var first = GetContractorServices(services, item).FirstOrDefault()?.ProjectOperationDetail;
            var last = GetContractorServices(services, item).LastOrDefault()?.ProjectOperationDetail;
            var serviceVolumes = GetContractorServices(services, item).Sum(x => x.Volume);
            var operationInfos = GetContractorServices(services, item).Select(x => x.ProjectOperationDetail.ProjectOperation.OperationInfo);
            var operationLocations = GetContractorServices(services, item).Select(x => x.ProjectOperationDetail.OperationLocation);
            var projectOperationIds = GetContractorServices(services, item).Select(x => x.ProjectOperationDetail.ProjectOperation.Id).Distinct().ToList();

            var operationInfoNames = "";
            var operationNames = operationInfos.Select(x => x.OperationInfoName).Distinct().ToList();
            List<string>? strings = [];
            foreach (var item1 in operationNames)
                strings.Add(item1 + "(" + measurments?.FirstOrDefault(x => x.Id == item.OperationInfoService.OperationInfo.UnitOfMeasurementId)?.Name + ")");

            operationInfoNames = StringSeparator.WithDash(strings);

            var operationInfoCodes = "";
            var operationCodes = operationInfos.Select(x => x.OperationInfoCode).Distinct().ToList();
            operationInfoCodes = StringSeparator.WithDash(operationCodes);

            var operationLocationPrivateNames = "";
            var privateNames = operationLocations.Select(x => x.PrivateName).Distinct().ToList();
            operationLocationPrivateNames = StringSeparator.WithDash(privateNames);

            result.Add(new GetsRequestedServiceContractModel()
            {
                EndDate = TimeCalculator.DatePiker(last?.EndDate),
                StartDate = TimeCalculator.DatePiker(first?.StartDate),
                ServiceInfoId = item.OperationInfoService.ServiceInfo.Id,
                OperationInfoNames = operationInfoNames,
                OperationInfoCodes = operationInfoCodes,
                OperationLocationPrivateNames = operationLocationPrivateNames,
                ServiceInfoVolume = serviceVolumes,
                ServiceInfoUnitOfMeasurement = measurments?.FirstOrDefault(c => c.Id == item.OperationInfoService.ServiceInfo.UnitOfMeasurementId)?.Name,
                ServiceInfoCode = item.OperationInfoService.ServiceInfo.ServiceInfoCode,
                ServiceInfoName = item.OperationInfoService.ServiceInfo.ServiceInfoName,
                UnitOfMeasurementId = item.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                ProjectOperationIds = projectOperationIds,
                //ProjectOperationDetailServiceId=item.Id
            });
        }

        response.Details.AddRange(result);
        return response;
    }

    public async Task<Result<GetContractorContractHistoryResponse?>> GetsContractorContractHistory(
        GetContractorContractHistoryRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractorContractHistoryResponse?>()!;
        var queryHistory = await _mediator.Send(new GetsContractorContractHeaderHistoryQuery(request.ContractorContractId, request.PageIndex, request.PageSize), ct);
        if (queryHistory.IsFailure)
            return Result.Failure<GetContractorContractHistoryResponse>(queryHistory.Error!);
        var histories = queryHistory.Value!.Data!;
        var rowCount = queryHistory.Value!.RowCount!;

        var thirdPartyIds = histories.Select(c => c.CreatorId).Distinct().ToList();
        var queryCreators = await WebServicesLogic.UserDataReceiver(thirdPartyIds, null, _mediator, ct);

        var result = queryHistory.Value!.Data!.Adapt<List<GetContractorContractHistoryModel>>();
        result.ForEach(c => c.Creator = queryCreators?.Where(p => p!.UserId == c.CreatorId).FirstOrDefault()?.FullName);
        return new GetContractorContractHistoryResponse(result, rowCount);
    }

    public async Task<Result<GetContractorContractHeaderByIdResponse?>> GetContractorContractHeaderById(
        GetContractorContractHeaderByIdRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractorContractHeaderByIdResponse?>()!;
        var result = await _mediator.Send(new GetContractorContractHeaderByIdNewQuery(request.Id, companyResult.Value), ct);
        if (result.IsFailure && result.Value is null)
            return Result.Failure<GetContractorContractHeaderByIdResponse>(result.Error!);
        return await PullAllData(result.Value!, ct);
    }

    public async Task<Result<GetCCHByIdResponse?>> GetCCHById(
        GetCCHByIdRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetCCHByIdResponse?>()!;
        var result = await GetCCHByIdQuery(request.Id, companyResult.Value, ct);
        if (result.IsFailure && result.Value is null)
            return result.Failure<GetCCHByIdResponse>()!;
        return await PullCCHByIdData(result.Value!, ct);
    }

    public async Task<Result<GetCCByHeaderIdResponse?>> GetCCByHeaderId(
        GetCCByHeaderIdRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetCCByHeaderIdResponse?>()!;
        var result = await GetCCByHeaderIdQuery(request, companyResult.Value, ct);
        if (result.IsFailure && result.Value is null)
            return result.Failure<GetCCByHeaderIdResponse>()!;
        return await PullCCByHeaderIdAllData(result.Value!, ct);
    }

    public async Task<Result<GetsFilteredContractorContractHeaderResponse?>> GetsFilteredContractorContractHeader(
        GetsFilteredContractorContractHeaderRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetsFilteredContractorContractHeaderResponse?>()!;
        var validation = await request.IsValidAsync<
            GetsFilteredContractorContractHeaderValidator,
            GetsFilteredContractorContractHeaderRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<
                GetsFilteredContractorContractHeaderResponse>(
                validation.Error!);

        var result = await GetContractorContractHeaderByFilterExecute(request, companyResult.Value, null, ct);
        if (result.IsBad()) return result.Failure<GetsFilteredContractorContractHeaderResponse>()!;
        var values = result.Value!.Data!;

        var curencyIds = values.NullListed(x => x.CurrencyId);
        var currenciesInfo = await WebServicesLogic.CurrenciesDataReceiver(
            curencyIds,
            _mediator,
            ct);

        var ids = values.NullListed(c => c.CreatorId);
        var creators = await UserDataReceiver(ids, ct);

        var contractorIds = values!.Select(oo => oo.ContractorId).Distinct().ToList();
        contractorIds.AddRange(values!.Where(x => x.ProjectManagerId is not null && x.ProjectManagerId > 0).Select(oo => oo.ProjectManagerId!.Value).Distinct().ToList());
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);
        values.ForEach(oo =>
        {
            var contract = values!.FirstOrDefault(x => x.Id.Equals(oo.Id));
            if (oo.CurrencyId is not null)
                oo.Currency = currenciesInfo?.Where(c => c.Id == oo.CurrencyId).FirstOrDefault()?.Name;

            if (oo.CreatorId is not null)
                oo.Creator = creators?.Where(c => c.UserId == oo.CreatorId).FirstOrDefault()?.FullName;

            oo.Contractor = contractors?.Where(c => c?.Id == oo.ContractorId).FirstOrDefault()?.FullName!;
            oo.ProjectManager = contractors?.Where(c => c?.Id == oo.ProjectManagerId).FirstOrDefault()?.FullName!;
        });

        return new GetsFilteredContractorContractHeaderResponse(values, result.Value!.RowCount!);
    }

    public async Task<Result<GetContractorPriceHistoryResponse?>> GetContractorPriceHistory(
        GetContractorPriceHistoryRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractorPriceHistoryResponse?>()!;
        if (request is null)
            return Result.Failure<GetContractorPriceHistoryResponse>(
                GlobalErrors.ValueIsNull);

        var validation = await request.IsValidAsync<
            GetContractorPriceHistoryValidator,
            GetContractorPriceHistoryRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<GetContractorPriceHistoryResponse>(
                validation.Error!);

        var result = await GetContractorPriceHistoryExecute(request, ct);
        if (result.IsBad()) return result.Failure<GetContractorPriceHistoryResponse>()!;
        var values = result.Value!.Data!;

        var curencyIds = values.Listed(x => x.CurrencyId);
        var currenciesData = await _mediator.Send(new GetsCurrencyByIdQuery(1, curencyIds.Count, curencyIds, true), ct);
        var currenciesInfo = currenciesData.Value!.Data;

        var ids = values.Listed(c => c.CreatorId);
        var creators = await UserDataReceiver(ids, ct);
        values.ForEach(oo =>
        {
            oo.Currency = currenciesInfo?.Where(c => c.Id == oo.CurrencyId).FirstOrDefault()?.Name;
            oo.Creator = creators?.Where(c => c.UserId == oo.CreatorId).FirstOrDefault()?.FullName;
        });

        return new GetContractorPriceHistoryResponse(values, result.Value!.RowCount!);
    }

    public async Task<Result<GetsFilteredContractorContractReportsResponse?>> GetsFilteredContractorContractReports(
        GetsFilteredContractorContractReportsRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetsFilteredContractorContractReportsResponse?>()!;
        if (request is null)
            return Result.Failure<GetsFilteredContractorContractReportsResponse>(
                GlobalErrors.ValueIsNull);

        var validation = await request.IsValidAsync<
            GetsFilteredContractorContractReportsValidator,
            GetsFilteredContractorContractReportsRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<GetsFilteredContractorContractReportsResponse>(
                validation.Error!);

        var companyId = companyResult.Value;
        var responses = await _mediator.Send(new GetsFilteredContractorContractReportsQuery(null, request.ContractorId, request.CostCenterId, request.ProjectIds, request.ContractorContractIds,
             request.FromDate, request.ToDate, companyId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsFilteredContractorContractReportsResponse>(responses.Error!);
        var values = responses.Value!.Data;

        var results = values.Adapt<List<GetsFilteredContractorContractReportsModel>>();

        var contractorIds = values!.Select(oo => oo.ContractorContractHeader.ContractorId).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        var currenciesInfo = await CurrencyDataReceiver(values!.Select(x => x.ContractorContractHeader).ToList(), ct);
        var ids = results.Where(x => x.CreatorId is not null && x.CreatorId.HasValue).Select(c => c.CreatorId!.Value).Distinct().ToList();
        var creators = await UserDataReceiver(ids, ct);
        results.ForEach(oo =>
        {
            oo.Contractor = contractors?.Where(c => c?.Id == oo.ContractorId).FirstOrDefault()?.FullName!;

            if (oo.CurrencyId is not null)
                oo.Currency = currenciesInfo?.Where(c => c.Id == oo.CurrencyId).FirstOrDefault()?.Name;

            if (oo.CreatorId is not null)
                oo.Creator = creators?.Where(c => c.UserId == oo.CreatorId).FirstOrDefault()?.FullName;
        });

        return new GetsFilteredContractorContractReportsResponse(results, responses.Value!.RowCount!);
    }

    public async Task<Result<GetsFilteredContractorContractDetailReportsResponse?>>
    GetsFilteredContractorContractDetailReports(
        GetsFilteredContractorContractDetailReportsRequest request,
        CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetsFilteredContractorContractDetailReportsResponse?>()!;
        if (request is null)
            return Result.Failure<
                GetsFilteredContractorContractDetailReportsResponse>(
                GlobalErrors.ValueIsNull);

        var companyId = companyResult.Value;

        var validation = await request.IsValidAsync<
        GetsFilteredContractorContractDetailReportsValidator,
        GetsFilteredContractorContractDetailReportsRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<
                GetsFilteredContractorContractDetailReportsResponse>(
                validation.Error!);

        var responses = await _mediator.Send(new GetsFilteredContractorContractDetailReportsQuery(null, request.ContractorContractId, request.ContractorId,
             request.FromDate, request.ToDate, companyId, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsFilteredContractorContractDetailReportsResponse>(responses.Error!);
        var values = responses.Value!.Data;

        var results = values.Adapt<List<GetsFilteredContractorContractDetailReportsModel>>();

        var resultById = results.ToDictionary(x => x.Id);

        foreach (var value in values!)
        {
            if (!resultById.TryGetValue(value.Id, out var model))
                continue;

            var projectOperation = value.ProjectOperation;

            // Fallback برای رکوردهای قدیمی
            if (projectOperation is null)
            {
                projectOperation = value.ContractorContractDetailServices
                    .FirstOrDefault(x => !x.IsDeleted)?
                    .ProjectOperationDetailContractorService?
                    .ProjectOperationDetail?
                    .ProjectOperation;
            }

            if (projectOperation is null)
                continue;

            var operationInfo = projectOperation.OperationInfo;
            var project = projectOperation.Project;

            // -------------------------
            // ProjectOperation
            // -------------------------
            model.ProjectOperationId = projectOperation.Id;

            // -------------------------
            // OperationInfo
            // -------------------------
            model.OperationInfoId = operationInfo?.Id;
            model.OperationInfoName =
                operationInfo?.OperationInfoName ?? string.Empty;
            model.OperationInfoCode =
                operationInfo?.OperationInfoCode ?? string.Empty;

            // -------------------------
            // Project
            // -------------------------
            model.ProjectId = project?.Id;
            model.ProjectName =
                project?.ProjectName ?? string.Empty;
            model.ProjectCode =
                project?.ProjectCode ?? string.Empty;

            // -------------------------
            // CostCenters
            // -------------------------
            var costCenters = project?
                .ProjectCostCenters
                .Where(x =>
                    !x.IsDeleted &&
                    x.CostCenter != null)
                .Select(x => new ContractorContractCostCenterModel
                {
                    Id = x.CostCenter.Id,
                    Name = x.CostCenter.CostCenterName,
                    Code = x.CostCenter.CostCenterCode
                })
                .DistinctBy(x => x.Id)
                .ToList()
                ?? [];

            model.CostCenters = costCenters;

            // backward compatibility برای فیلدهای قدیمی
            var firstCostCenter = costCenters.FirstOrDefault();

            model.CostCenterId = firstCostCenter?.Id;
            model.CostCenterName =
                firstCostCenter?.Name ?? string.Empty;
            model.CostCenterCode =
                firstCostCenter?.Code ?? string.Empty;
        }

        var measureUnitIds = values!
            .SelectMany(x => x.ContractorContractDetailServices)
            .Where(x => !x.IsDeleted)
            .Select(x => x.ProjectOperationDetailContractorService)
            .Where(x => x is not null)
            .Select(x =>
                x!.OperationInfoService != null
                    ? x.OperationInfoService.ServiceInfo.UnitOfMeasurementId
                    : x.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId)
            .Where(x => x > 0)
            .Distinct()
            .ToList();

        var measurunits =
            await WebServicesLogic.MeasurementDataReceiver(
                measureUnitIds,
                _mediator,
                ct);

        var ids = results.Where(x => x.CreatorId is not null && x.CreatorId.HasValue).Select(c => c.CreatorId!.Value).Distinct().ToList();
        var creators = await UserDataReceiver(ids, ct);

        results.ForEach(oo =>
        {
            oo.ServiceInfoUnitOfMeasurement = measurunits?.Where(c => c.Id == oo.ServiceInfoUnitOfMeasurementId).FirstOrDefault()?.Name;

            if (oo.CreatorId is not null)
                oo.Creator = creators?.Where(c => c.UserId == oo.CreatorId).FirstOrDefault()?.FullName;
        });

        return new GetsFilteredContractorContractDetailReportsResponse(results, responses.Value!.RowCount!);
    }

    public async Task<Result<GetFilteredContractorContractsByContractorResponse?>> GetsFilteredContractorContractByContractor(
        GetFilteredContractorContractsByContractorRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetFilteredContractorContractsByContractorResponse?>()!;
        if (request is null)
            return Result.Failure<GetFilteredContractorContractsByContractorResponse>(
                GlobalErrors.ValueIsNull);


        var validation = await request.IsValidAsync<
            GetFilteredContractorContractsByContractorValidator,
            GetFilteredContractorContractsByContractorRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<GetFilteredContractorContractsByContractorResponse>(
                validation.Error!);

        var companyId = companyResult.Value;
        var contractorContractQuery = await _mediator.Send(new GetFilteredContractorContractsByContractorQuery(request.ContractorId, request.CostCenterId, request.ProjectIds,
            request.ContractIds, request.FromDate, request.ToDate, request.FilterData, companyId, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (contractorContractQuery.IsFailure)
            return Result.Failure<GetFilteredContractorContractsByContractorResponse>(contractorContractQuery.Error!);
        var contractorContracts = contractorContractQuery.Value!.Data!;

        var result = new List<GetFilteredContractorContractsByContractorModel>();
        foreach (var item in contractorContracts)
        {
            var costCenterName = "";
            var projectName = "";
            if (item.Details.FirstOrDefault()?.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService is not null)
            {
                costCenterName = item.Details.FirstOrDefault()!.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName;
                projectName = item.Details.FirstOrDefault()!.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.ProjectName;
            }
            if (item.Details.FirstOrDefault()?.ProjectOperation is not null)
            {
                costCenterName = item.Details.FirstOrDefault()!.ProjectOperation!.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName;
                projectName = item.Details.FirstOrDefault()!.ProjectOperation!.Project.ProjectName;
            }

            result.Add(new GetFilteredContractorContractsByContractorModel()
            {
                ContractorNumber = item.Id,
                CostCenterName = costCenterName,
                ProjectName = projectName,
                Created = item.Created,
                Id = item.Id,
            });
        }

        return new GetFilteredContractorContractsByContractorResponse(result, contractorContractQuery.Value!.RowCount!);
    }

    public async Task<Result<GetsContractorByContractorContractTypeResponse?>> GetsContractorByContractorContractType(
        GetsContractorByContractorContractTypeRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetsContractorByContractorContractTypeResponse?>()!;
        var response = await _mediator.Send(new GetsContractorByContractorContractTypeQuery(request.ContractorContractTypeId, companyResult.Value), ct);
        if (response.IsFailure)
            return Result.Failure<GetsContractorByContractorContractTypeResponse>(response.Error!);
        if (response.Value!.Data is null)
            return Result.Failure<GetsContractorByContractorContractTypeResponse>(ContractorContractErrors.ContractorsNotFound!);

        ///TODO
        var contractorIds = response.Value!.Data!.Select(x => x.ContractorContractHeader.ContractorId).Distinct().ToList();

        List<FilteredUserModel?> contractors = new();
        var responseValue = await WebServicesLogic.GetFilteredByIdsDataReceiver(contractorIds, request.FilterData, _mediator, ct);
        if (responseValue is not null && responseValue.Count > 0)
            contractors.AddRange(responseValue);

        var result = new List<GetsContractorByContractorContractTypeModel>();
        if (!string.IsNullOrEmpty(request.FilterData))
        {
            foreach (var item in contractorIds)
            {
                if (!contractors.Any(x => x?.Id == item))
                    continue;

                var contractor = contractors.Where(x => x?.Id == item).FirstOrDefault();
                result.Add(new(item, contractor?.FirstName + " " + contractor?.LastName));
            }
        }
        else
        {
            foreach (var item in contractorIds)
            {
                var contractor = contractors.Where(x => x?.Id == item).FirstOrDefault();
                result.Add(new(item, contractor?.FirstName + " " + contractor?.LastName));
            }
        }

        var responseData = result.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetsContractorByContractorContractTypeResponse(responseData ?? new List<GetsContractorByContractorContractTypeModel>(0), contractors?.Count ?? 0);
    }

    public async Task<Result<GetsContractorContractServiceReportResponse?>> GetsContractorContractServiceReport(
        GetsContractorContractServiceReportRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetsContractorContractServiceReportResponse?>()!;
        if (request is null)
            return Result.Failure<GetsContractorContractServiceReportResponse>(
                GlobalErrors.ValueIsNull);

        var validation = await request.IsValidAsync<
            GetsContractorContractServiceReportValidator,
            GetsContractorContractServiceReportRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<GetsContractorContractServiceReportResponse>(
                validation.Error!);


        var result = await ProcessServiceReportValuesAsync(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            request.ContractorIds,
            request.ServiceInfoIds,
            request.MeasurUnitIds,
            request.ContractTypeId,
            request.StartDate,
            request.EndDate,
            request.FromDate,
            request.ToDate,
            request.FromCreated,
            request.ToCreated,
            request.Status,
            request.ContractStatus,
            request.FilterData,
            request.FilterDescription,
            request.FilterServiceInfo,
            request.OrderBy,
            request.PageIndex,
            request.PageSize,
            _mediator,
            ct);

        if (result.IsFailure)
            return Result.Failure<GetsContractorContractServiceReportResponse>(
                result.Error!);

        if (result.Value is null)
            return Result.Failure<GetsContractorContractServiceReportResponse>(
                SharedErrors.ItemNotFound);

        var response = result.Value;

        return new GetsContractorContractServiceReportResponse(
                    response.OtherData,
                    response.Data ?? new List<GetsContractorContractServiceReportModel>(),
                    response.RowCount);
    }

    public async Task<Result<GetSuggestedServicePriceResponse?>> GetSuggestedServicePrice(
        GetSuggestedServicePriceRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetSuggestedServicePriceResponse?>()!;
        if (request is null)
            return Result.Failure<GetSuggestedServicePriceResponse>(
                GlobalErrors.ValueIsNull);

        var validation = await request.IsValidAsync<
            GetSuggestedServicePriceValidator,
            GetSuggestedServicePriceRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<GetSuggestedServicePriceResponse>(
                validation.Error!);

        var result = await GetSuggestedServicePriceExecute(null, request, ct);
        if (result.IsBad()) return result.Failure<GetSuggestedServicePriceResponse>()!;
        var values = result.Value!;

        var contractorIds = values!.Data.Listed(oo => oo.ContractorId);
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        var curencyIds = values.Data.Listed(x => x.CurrencyId);
        var currenciesData = await _mediator.Send(new GetsCurrencyByIdQuery(1, curencyIds.Count, curencyIds, true), ct);
        var currencies = currenciesData.Value?.Data;

        var ids = values.Data.Listed(c => c.CreatorId);
        ids.AddRange(values.Data.NullListed(x => x.UpdaterId));
        var creators = await UserDataReceiver(ids, ct);

        values.Data.ForEach(oo =>
        {
            oo.Currency = currencies?.FirstOrDefault(c => c.Id == oo.CurrencyId)?.Name;
            oo.Creator = creators?.FirstOrDefault(c => c.UserId == oo.CreatorId)?.FullName;

            if (contractors.HasAny())
                oo.ContractorName = contractors?.FirstOrDefault(c => c!.Id == oo.ContractorId)?.FullName;

            if (oo.UpdaterId.HasValue)
                oo.Updater = creators?.FirstOrDefault(c => c.UserId == oo.UpdaterId)?.FullName;
        });

        var measurementIds = values.Data.NullListed(x => x.UnitOfMeasurementId);
        var measurements = await WebServicesLogic.MeasurementDataReceiver(measurementIds, _mediator, ct);
        if (measurements.HasAny())
            values.Data.ForEach(oo =>
            {
                oo.UnitOfMeasurement = measurements?.FirstOrDefault(c => c.Id == oo.UnitOfMeasurementId)?.Name;
            });

        return values;
    }

    public async Task<Result<GetCCHVersionByCCHIdResponse?>> GetCCHVersionByCCHId(
         GetCCHVersionByCCHIdRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetCCHVersionByCCHIdResponse?>()!;
        var result = await GetCCHVersionByCCHIdExcecute(request, ct);
        if (result.IsBad()) return result.Failure<GetCCHVersionByCCHIdResponse>()!;
        return new GetCCHVersionByCCHIdResponse(result.Value!);
    }

    public async Task<Result<GetDraftedFixCCsResponse>> GetDraftedFixCCs(
         GetDraftedFixCCsRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetDraftedFixCCsResponse>()!;
        var result = await GetDraftedFixCCsExecute(request, companyResult.Value, ct);
        if (result.IsBad()) return result.Failure<GetDraftedFixCCsResponse>()!;
        var values = result.Value!.Where(x => x.DailyServices!.Any()).SelectMany(x => x.DailyServices!);

        var measureIds = values.Where(x => x.ProjectOperationMeasureId.HasValue && x.ServiceInfoMeasureId.HasValue)
            .SelectMany(x => new[] { x.ProjectOperationMeasureId!.Value, x.ServiceInfoMeasureId!.Value }).Where(x => x > 0).Distinct().ToList();
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var creatorIds = values.NullListed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        foreach (var item in result.Value!)
            item.DailyServices!.ForEach(x =>
            {
                x.ServiceInfoMeasurement = measures?.FirstOrDefault(m => m.Id == x.ServiceInfoMeasureId)?.Name;
                x.ProjectOperationMeasurement = measures?.FirstOrDefault(m => m.Id == x.ProjectOperationMeasureId)?.Name;
                x.Creator = creators?.FirstOrDefault(m => m.UserId == x.CreatorId)?.FullName;
            });

        return new GetDraftedFixCCsResponse(result.Value!);
    }

    public async Task<Result<GetConfirmedCCDailyServicesResponse?>> GetConfirmedCCDailyServices(
         GetConfirmedCCDailyServicesRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetConfirmedCCDailyServicesResponse?>()!;
        if (request is null)
            return Result.Failure<GetConfirmedCCDailyServicesResponse>(
                GlobalErrors.ValueIsNull);

        var validation = await request.IsValidAsync<
            GetConfirmedCCDailyServicesValidator,
            GetConfirmedCCDailyServicesRequest>(ct);

        if (validation.IsFailure)
            return Result.Failure<GetConfirmedCCDailyServicesResponse>(
                validation.Error!);

        var result = await GetConfirmedCCDailyServicesExecute(request, ct);
        if (result.IsBad()) return result.Failure<GetConfirmedCCDailyServicesResponse>()!;
        var values = result.Value!.Data!;
        var otherData = new GetConfirmedCCDailyServicesTotalModel()
        {
            TotalVolume = values.Sum(x => x.ContractorContractDetailVolume ?? 0),
            DoneVolume = values.Sum(x => x.Volume ?? 0),
            DonePrice = values.Sum(x => x.TotalPrice ?? 0),
            ContractPrice = values.Sum(x => x.ContractorContractDetailPrice ?? 0),
        };

        var data = values.SetPaging(request.PageIndex - 1, request.PageSize);

        var measureIds = data.SelectMany(x => new[] { x.ServiceInfoMeasureId, x.ProjectOperationMeasureId })
            .Where(x => x > 0).Distinct().ToList();
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var creatorIds = data.NullListed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var contractorIds = values!.Listed(oo => oo.ContractorId).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        data!.ForEach(x =>
            {
                x.ServiceInfoMeasure = measures?.FirstOrDefault(m => m.Id == x.ServiceInfoMeasureId)?.Name;
                x.ProjectOperationMeasure = measures?.FirstOrDefault(m => m.Id == x.ProjectOperationMeasureId)?.Name;
                x.CreatorName = creators?.FirstOrDefault(m => m.UserId == x.CreatorId)?.FullName;

                if (contractors.HasAny())
                {
                    x.Contractor = contractors?.FirstOrDefault(m => m!.Id == x.ContractorId)?.FullName;
                    x.ContractorNickName = contractors?.FirstOrDefault(m => m!.Id == x.ContractorId)?.Nickname;
                }
            });

        return new GetConfirmedCCDailyServicesResponse(otherData, data!, result.Value.RowCount);
    }

    public async Task<Result<GetDraftedServiceCCsResponse>> GetDraftedServiceCCs(
         GetDraftedServiceCCsRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetDraftedServiceCCsResponse>()!;
        var result = await GetDraftedServiceCCsExecute(request, companyResult.Value, ct);
        if (result.IsBad()) return result.Failure<GetDraftedServiceCCsResponse>()!;
        var values = result.Value!.Where(x => x.DailyServices!.Any()).SelectMany(x => x.DailyServices!);

        var measureIds = values.Where(x => x.ProjectOperationMeasureId.HasValue && x.ServiceInfoMeasureId.HasValue)
            .SelectMany(x => new[] { x.ProjectOperationMeasureId!.Value, x.ServiceInfoMeasureId!.Value }).Where(x => x > 0).Distinct().ToList();
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var creatorIds = values.NullListed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        foreach (var item in result.Value!)
            item.DailyServices!.ForEach(x =>
            {
                x.ServiceInfoMeasurement = measures?.FirstOrDefault(m => m.Id == x.ServiceInfoMeasureId)?.Name;
                x.ProjectOperationMeasurement = measures?.FirstOrDefault(m => m.Id == x.ProjectOperationMeasureId)?.Name;
                x.Creator = creators?.FirstOrDefault(m => m.UserId == x.CreatorId)?.FullName;
            });

        var totalAmount = result.Value!.Sum(x => x.TotalAmount ?? 0);
        var totalWorkedAmount = result.Value!.Sum(x => x.TotalWorkedAmount ?? 0);
        return new GetDraftedServiceCCsResponse(totalAmount, totalWorkedAmount, result.Value!);
    }

    public async Task<Result<GetContractsByProjectIdResponse?>> GetContractsByProjectId(
        GetContractsByProjectIdRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractsByProjectIdResponse?>()!;
        _logger.LogInformation("GetContractsByProjectId");
        var result = await _mediator.Send(new GetContractsByProjectIdQuery(request.ProjectId, request.PageIndex, request.PageSize, companyResult.Value), ct);
        if (result.IsBad()) return result.Failure<GetContractsByProjectIdResponse>()!;
        return result;
    }

    public async Task<Result<GetCCThirdPartiesResponse?>> GetCCThirdParties(
        GetCCThirdPartiesRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetCCThirdPartiesResponse?>()!;
        _logger.LogInformation("GetCCThirdParties");
        var result = await _mediator.Send(new GetCCThirdPartiesQuery(request.ProjectId, request.ContractorIds, request.PageIndex, request.PageSize, companyResult.Value), ct);
        if (result.IsBad()) return result.Failure<GetCCThirdPartiesResponse>()!;
        return result;
    }

    public async Task<Result<GetFltrProjectContractorsResponse?>> GetFltrProjectContractors(
        GetFltrProjectContractorsRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetFltrProjectContractorsResponse?>()!;
        _logger.LogInformation("GetCCThirdParties");
        var result = await _mediator.Send(new GetFltrProjectContractorsQuery(
            request.CostCenterIds, request.ProjectIds, request.FilterData, request.PageIndex, request.PageSize, companyResult.Value), ct);
        if (result.IsBad()) return result.Failure<GetFltrProjectContractorsResponse>()!;
        return result;
    }

    public async Task<Result<GetDraftedFixCCsEnumResponse?>> GetDraftedFixCCsEnum(
        GetDraftedFixCCsEnumRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetDraftedFixCCsEnumResponse?>()!;
        _logger.LogInformation("Request for GetSuggestedServicePriceExcelEnum");
        var draft = await Task.Run(() => EnumExt.GetEnumObjectList<GetDraftedServiceCCsEnum>());
        var daily = await Task.Run(() => EnumExt.GetEnumObjectList<GetDraftedServiceDailiesEnum>());
        return new GetDraftedFixCCsEnumResponse(draft, daily);
    }

    public async Task<Result<GetDraftedServiceCCsEnumResponse?>> GetDraftedServiceCCsEnum(
        GetDraftedServiceCCsEnumRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetDraftedServiceCCsEnumResponse?>()!;
        _logger.LogInformation("Request for GetSuggestedServicePriceExcelEnum");
        var contract = await Task.Run(() => EnumExt.GetEnumObjectList<GetDraftedServiceCCsEnum>());
        var daily = await Task.Run(() => EnumExt.GetEnumObjectList<GetDraftedServiceDailiesEnum>());
        return new GetDraftedServiceCCsEnumResponse(contract, daily);
    }

    public async Task<Result<GetDraftedFixCCsExporterResponse?>> GetDraftedFixCCsExporter(
        GetDraftedFixCCsExporterRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetDraftedFixCCsExporterResponse?>()!;
        var result = await GetDraftedFixCCsExecute(
            request.Adapt<GetDraftedFixCCsRequest>(),
            companyResult.Value,
            ct);
        if (result.IsBad()) return result.Failure<GetDraftedFixCCsExporterResponse>()!;
        var values = result.Value!.Where(x => x.DailyServices!.Any()).SelectMany(x => x.DailyServices!);

        var measureIds = values.Where(x => x.ProjectOperationMeasureId.HasValue && x.ServiceInfoMeasureId.HasValue)
            .SelectMany(x => new[] { x.ProjectOperationMeasureId!.Value, x.ServiceInfoMeasureId!.Value }).Where(x => x > 0).Distinct().ToList();
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var creatorIds = values.NullListed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        foreach (var item in result.Value!)
            item.DailyServices!.ForEach(x =>
            {
                x.ServiceInfoMeasurement = measures?.FirstOrDefault(m => m.Id == x.ServiceInfoMeasureId)?.Name;
                x.ProjectOperationMeasurement = measures?.FirstOrDefault(m => m.Id == x.ProjectOperationMeasureId)?.Name;
                x.Creator = creators?.FirstOrDefault(m => m.UserId == x.CreatorId)?.FullName;
            });

        var file = new FileContentResult(ContractorContractExcels.GetDraftedFixCCsToExcel(
            result.Value, request.ServiceFilters!, request.DailyFilters!), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"DraftedFixCCs-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetDraftedFixCCsExporterResponse(file);
    }

    public async Task<Result<GetDraftedServiceCCsExporterResponse>> GetDraftedServiceCCsExporter(
         GetDraftedServiceCCsExporterRequest request, CT ct)
    {
        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetDraftedServiceCCsExporterResponse>()!;
        var result = await GetDraftedServiceCCsExecute(
            request.Adapt<GetDraftedServiceCCsRequest>(),
            companyResult.Value,
            ct);
        if (result.IsBad()) return result.Failure<GetDraftedServiceCCsExporterResponse>()!;
        var values = result.Value!.Where(x => x.DailyServices!.Any()).SelectMany(x => x.DailyServices!);

        var measureIds = values.Where(x => x.ProjectOperationMeasureId.HasValue && x.ServiceInfoMeasureId.HasValue)
            .SelectMany(x => new[] { x.ProjectOperationMeasureId!.Value, x.ServiceInfoMeasureId!.Value }).Where(x => x > 0).Distinct().ToList();
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var creatorIds = values.NullListed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        foreach (var item in result.Value!)
            item.DailyServices!.ForEach(x =>
            {
                x.ServiceInfoMeasurement = measures?.FirstOrDefault(m => m.Id == x.ServiceInfoMeasureId)?.Name;
                x.ProjectOperationMeasurement = measures?.FirstOrDefault(m => m.Id == x.ProjectOperationMeasureId)?.Name;
                x.Creator = creators?.FirstOrDefault(m => m.UserId == x.CreatorId)?.FullName;
            });

        var file = new FileContentResult(ContractorContractExcels.GetDraftedServiceCCsToExcel(
            result.Value, request.ServiceFilters!, request.DailyFilters!), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"DraftedServiceCCs-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetDraftedServiceCCsExporterResponse(file);
    }

}
