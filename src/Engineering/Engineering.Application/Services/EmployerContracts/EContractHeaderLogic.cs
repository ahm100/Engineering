using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Abstractions.Data.ServiceInfos;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Currencies;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Groups;
using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenter;
using Engineering.Application.Services.EmployerContracts.Contracts.ChangeEContractsStatus;
using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.CreateEContractHeader;
using Engineering.Application.Services.EmployerContracts.Contracts.DeleteEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.DeleteEContractHeader;
using Engineering.Application.Services.EmployerContracts.Contracts.EContractFinancialCover;
using Engineering.Application.Services.EmployerContracts.Contracts.GetConsiderationTypes;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractById;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHeaderById;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractOperationHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractStatus;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractTypes;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEDocumentTypes;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEOProductHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEOServiceHistory;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads.Enum;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads.Exporter;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts.Enum;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts.Exporter;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEmployers;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContract;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractFinancial;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractHeader;
using Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractVolume;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetECThirdParties;
using Engineering.Application.Services.EmployerEmployees.Queries.GetECThirdParties;
using Engineering.Application.Services.Projects.Queries.GetProjectByIds;
using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;
using Engineering.Domain.Entities.Synonyms.Warehouse.Products;

namespace Engineering.Application.Services.EmployerContracts;

public partial class EContractHeaderLogic : IEContractHeaderLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<EContractHeaderLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IConfiguration _configuration;
    private readonly IEmployerContractHeadRepository _headRepo;
    private readonly IEmployerContractRepository _contractRepo;
    private readonly IEmployerContractHistoryRepository _contractHistoryRepo;
    private readonly IEmployerOperationHistoryRepository _contractOperationHistoryRepo;
    private readonly IEmployerDocRepository _contractDocRepo;
    private readonly IEmployerCostOverRepository _contractCostRepo;
    private readonly IEmployerOperationRepository _contractOperationRepo;
    private readonly IEmployerConsiderationRepository _contractConsiderationRepo;
    private readonly IProjectOperationRepository _projectOperationRepo;
    private readonly IEmployerOperationProductRepository _employerOperationProductRepo;
    private readonly IEmployerOperationServiceRepository _employerOperationServiceRepo;
    private readonly IEmployerOperationServiceHistoryRepository _employerOperationServiceHistoryRepo;
    private readonly IEmployerOperationProductHistoryRepository _employerOperationProductHistoryRepo;
    private readonly IServiceInfoRepository _serviceInfoRepository;
    private readonly IOperationInfoServiceRepository _operationInfoServiceRepository;
    private readonly IViewGroupRepository _productGroupRepo;
    private readonly IViewCurrencyRepository _curRepo;
    private readonly IViewProductRepository _productRepo;

    public EContractHeaderLogic(
        IMediator mediator,
        ILogger<EContractHeaderLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        IConfiguration configuration,
        IEmployerContractHeadRepository headRepository,
        IEmployerContractRepository contractRepository,
        IEmployerDocRepository contractDocRepository,
        IEmployerCostOverRepository employerCostOverRepository,
        IEmployerOperationRepository contractOperationRepo,
        IEmployerConsiderationRepository contractConsiderationRepo,
        IEmployerContractHistoryRepository contractHistoryRepo,
        IEmployerOperationHistoryRepository contractOperationHistoryRepo,
        IProjectOperationRepository projectOperationRepo,
        IEmployerOperationServiceHistoryRepository employerOperationServiceHistoryRepo,
        IEmployerOperationProductHistoryRepository employerOperationProductHistoryRepo,
        IEmployerOperationProductRepository employerOperationProductRepo,
        IServiceInfoRepository serviceInfoRepository,
        IViewCurrencyRepository curRepo,
        IOperationInfoServiceRepository operationInfoServiceRepository,
        IViewGroupRepository productGroupRepo,
        IViewProductRepository productRepo,
        IEmployerOperationServiceRepository employerOperationServiceRepo)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _configuration = configuration;
        _headRepo = headRepository;
        _contractRepo = contractRepository;
        _contractDocRepo = contractDocRepository;
        _contractCostRepo = employerCostOverRepository;
        _contractOperationRepo = contractOperationRepo;
        _contractConsiderationRepo = contractConsiderationRepo;
        _contractHistoryRepo = contractHistoryRepo;
        _contractOperationHistoryRepo = contractOperationHistoryRepo;
        _curRepo = curRepo;
        _projectOperationRepo = projectOperationRepo;
        _employerOperationProductRepo = employerOperationProductRepo;
        _employerOperationServiceRepo = employerOperationServiceRepo;
        _serviceInfoRepository = serviceInfoRepository;
        _operationInfoServiceRepository = operationInfoServiceRepository;
        _employerOperationServiceHistoryRepo = employerOperationServiceHistoryRepo;
        _employerOperationProductHistoryRepo = employerOperationProductHistoryRepo;
        _productGroupRepo = productGroupRepo;
        _productRepo = productRepo;
    }

    public async Task<Result<CreateEContractHeaderResponse?>> CreateEContractHeader(
        CreateEContractHeaderRequest request, CT ct)
    {
        _logger.LogInformation("CreateEContractHeader");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateEContractHeaderResponse>(GlobalErrors.InvalidCompany);

        if (!string.IsNullOrEmpty(request.Code))
        {
            var verify = await VerifyCodeHandler(null, request.Code!, companyId!.Value, ct);
            if (verify.Value)
                return Result.Failure<CreateEContractHeaderResponse>(EContractErrors.IsDuplicate);
        }

        var costCenterRes = await _mediator.Send(new GetCostCenterQuery(request.CostCenterId), ct);
        if (costCenterRes.IsBad()) return costCenterRes.Failure<CreateEContractHeaderResponse>()!;

        var projectIds = request.CreateContracts.Listed(x => x.ProjectId);
        var projectRes = await _mediator.Send(new GetProjectByIdsQuery(projectIds, true, false), ct);
        if (projectRes.IsBad()) return projectRes.Failure<CreateEContractHeaderResponse>()!;
        var projects = projectRes.Value!;
        if (projects!.Any(x => x.Contractual))
            return Result.Failure<CreateEContractHeaderResponse>(EContractErrors.NoContractual!);

        var result = await CreateHeaderHandler(
            costCenterRes.Value!, request, companyId!.Value, ct);
        if (result.IsBad()) return result.Failure<CreateEContractHeaderResponse>()!;

        foreach (var item in request.CreateContracts)
        {
            var contract = await CreateContract(result.Value!, projects, item, ct);
            if (contract.IsBad()) return contract.Failure<CreateEContractHeaderResponse>()!;
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateEContractHeaderResponse(result.Value!.Id, true);
    }

    public async Task<Result<CreateEContractResponse?>> CreateEContract(
        CreateEContractRequest request, CT ct)
    {
        _logger.LogInformation("CreateEContract");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateEContractResponse>(GlobalErrors.InvalidCompany);

        foreach (var contractModel in request.CreateContracts)
        {
            if (!string.IsNullOrEmpty(contractModel.Code))
            {
                var verify = await VerifyEContractCodeHandler(
                    null,
                    contractModel.Code!,
                    request.EContractHeaderId,
                    ct);

                if (verify.Value)
                    return Result.Failure<CreateEContractResponse>(EContractErrors.IsDuplicate);
            }
            else
            {
                var newCode = CodeCreateHandler(request.EContractHeaderId, ct);
                if (newCode.Result.IsBad())
                    return Result.Failure<CreateEContractResponse>(GlobalErrors.InvalidCompany);
                contractModel.Code = newCode.Result.Value;
            }
        }

        var result = await GetEContractHeaderHandler(request.EContractHeaderId, ct);
        if (result.IsBad()) return result.Failure<CreateEContractResponse>()!;

        if (request.CreateContracts.Any(x => x.AssigneOperations != null)
            && request.CreateContracts.Any(x => x.CreateOperations != null))
        {
            bool assignValidate = request.CreateContracts
                .SelectMany(x => x.AssigneOperations!)
                .GroupBy(y => y.ProjectOperationId)
                .Any(g => g.Count() > 1);
            if (assignValidate)
                return result.Failure<CreateEContractResponse>()!;

            bool createValidate = request.CreateContracts
                .SelectMany(x => x.CreateOperations!)
                .GroupBy(y => y.OperationInfoId)
                .Any(g => g.Count() > 1);
            if (createValidate)
                return result.Failure<CreateEContractResponse>()!;
        }

        var projectIds = request.CreateContracts.Listed(x => x.ProjectId);
        var projectRes = await _mediator.Send(new GetProjectByIdsQuery(projectIds, true, false), ct);
        if (projectRes.IsBad()) return projectRes.Failure<CreateEContractResponse>()!;
        var projects = projectRes.Value!;
        if (projects!.Any(x => x.Contractual))
            return Result.Failure<CreateEContractResponse>(EContractErrors.NoContractual!);

        foreach (var item in request.CreateContracts)
        {
            var contract = await CreateContract(result.Value!, projects, item, ct);
            if (contract.IsBad()) return contract.Failure<CreateEContractResponse>()!;
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateEContractResponse(result.Value!.Id, true);
    }

    public async Task<Result<UpdateEContractResponse?>> UpdateEContract(
        UpdateEContractRequest request, CT ct)
    {
        _logger.LogInformation("UpdateEContract");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateEContractResponse>(GlobalErrors.InvalidCompany);

        var result = await GetEContractHandler(request.Id, ct);
        if (result.IsBad()) return result.Failure<UpdateEContractResponse>()!;

        if (!string.IsNullOrEmpty(request.Code) && request.Code != result.Value!.Code)
        {
            var verify = await VerifyEContractCodeHandler(
                request.Id,
                request.Code!,
                result.Value.EmployerContractHeadId,
                ct);

            if (verify.Value)
                return Result.Failure<UpdateEContractResponse>(EContractErrors.IsDuplicate);
        }

        var contract = await UpdateContract(result.Value!, request, ct);
        if (contract.IsBad()) return contract.Failure<UpdateEContractResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateEContractResponse(true);
    }

    public async Task<Result<UpdateEContractHeaderResponse?>> UpdateEContractHeader(
        UpdateEContractHeaderRequest request, CT ct)
    {
        _logger.LogInformation("UpdateEContractHeader");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateEContractHeaderResponse>(GlobalErrors.InvalidCompany);

        var result = await GetEContractHeaderFullHandler(request.Id, ct);
        if (result.IsBad()) return result.Failure<UpdateEContractHeaderResponse>()!;

        var response = await UpdateHeaderHandler(result.Value!, request, companyId!.Value, ct);
        if (response.IsBad()) return response.Failure<UpdateEContractHeaderResponse>()!;

        if (request.DeleteContracts.HasAny())
            foreach (var item in request.DeleteContracts!)
            {
                var entity = result.Value!.EmployerContracts.FirstOrDefault(x => x.Id == item);
                if (entity is not null)
                {
                    var contract = await DeleteEContractHandler(entity!, ct);
                    if (contract.IsBad()) return contract.Failure<UpdateEContractHeaderResponse>()!;
                }
            }

        if (request.UpdateContracts.HasAny())
            foreach (var item in request.UpdateContracts!)
            {
                var entity = result.Value!.EmployerContracts.FirstOrDefault(x => x.Id == item.Id);
                if (entity is not null)
                {
                    var contract = await UpdateContract(entity!, item, ct);
                    if (contract.IsBad()) return contract.Failure<UpdateEContractHeaderResponse>()!;
                }
            }

        if (request.CreateContracts.HasAny())
        {
            var projectIds = request.CreateContracts!.Listed(x => x.ProjectId);
            var projectRes = await _mediator.Send(new GetProjectByIdsQuery(projectIds, true, false), ct);
            if (projectRes.IsBad()) return projectRes.Failure<UpdateEContractHeaderResponse>()!;
            var projects = projectRes.Value!;
            if (projects!.Any(x => x.Contractual))
                return Result.Failure<UpdateEContractHeaderResponse>(EContractErrors.NoContractual!);

            foreach (var item in request.CreateContracts!)
            {
                var contract = await CreateContract(result.Value!, projects, item, ct);
                if (contract.IsBad()) return contract.Failure<UpdateEContractHeaderResponse>()!;
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateEContractHeaderResponse(true);
    }

    public async Task<Result<DeleteEContractHeaderResponse?>> DeleteEContractHeader(
        DeleteEContractHeaderRequest request, CT ct)
    {
        _logger.LogInformation("DeleteEContractHeader");
        var result = await DeleteEContractHeaderHandler(request.Id, ct);
        if (result.IsBad()) return result.Failure<DeleteEContractHeaderResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteEContractHeaderResponse(result.Value!.IsDeleted);
    }

    public async Task<Result<DeleteEContractResponse?>> DeleteEContract(
        DeleteEContractRequest request, CT ct)
    {
        _logger.LogInformation("DeleteEContract");
        var entity = await _contractRepo.DeleteEContract(request.Id, ct);
        if (entity is null)
            return Result.Failure<DeleteEContractResponse>(EContractErrors.NotFound);

        var result = await DeleteEContractHandler(entity!, ct);
        if (result.IsBad()) return result.Failure<DeleteEContractResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteEContractResponse(result.Value!.IsDeleted);
    }

    public async Task<Result<UpdateEContractVolumeResponse?>> UpdateEContractVolumeService(
        UpdateEContractVolumeRequest request, CT ct)
    {
        _logger.LogInformation("UpdateEContractVolumeService");
        var head = await GetEContractHeaderHandler(request.Id, ct);
        if (head.IsBad()) return head.Failure<UpdateEContractVolumeResponse>()!;
        var value = head.Value!;

        var response = await UpdateEContractHeadVolumeHandler(request, value, ct);
        if (request.EContractVolumeModel.HasAny())
        {
            foreach (var item in request.EContractVolumeModel!)
            {
                var contract = await GetEContractHandler(item.Id, ct);
                if (contract.IsBad()) return contract.Failure<UpdateEContractVolumeResponse>()!;

                var create = await UpdateProjectOperationVolumeHandler(item, contract.Value!, ct);
                if (create.IsBad()) return create.Failure<UpdateEContractVolumeResponse>()!;
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateEContractVolumeResponse(true);
    }

    public async Task<Result<EContractFinancialCoverResponse?>> EContractFinancialCover(
        EContractFinancialCoverRequest request, CT ct)
    {
        _logger.LogInformation("EContractFinancialCover");
        var head = await GetEContractHeaderHandler(request.Id, ct);
        if (head.IsBad()) return head.Failure<EContractFinancialCoverResponse>()!;
        var value = head.Value!;

        var result = await CreateEContractHeadFinancialHandler(request, value!, ct);
        if (result.IsBad()) return result.Failure<EContractFinancialCoverResponse>()!;

        if (request.EContracts.HasAny())
            foreach (var eContract in request.EContracts!)
            {
                var contract = await GetEContractHandler(eContract.Id, ct);
                if (contract.IsBad()) return contract.Failure<EContractFinancialCoverResponse>()!;

                var create = await EContractFinancialCover(value, eContract, ct);
                if (create.IsBad()) return create.Failure<EContractFinancialCoverResponse>()!;
            }

        await _unitOfWork.CommitAsync(ct);
        return new EContractFinancialCoverResponse(value.Id, true);
    }

    public async Task<Result<UpdateEContractFinancialResponse?>> UpdateEContractFinancial(
        UpdateEContractFinancialRequest request, CT ct)
    {
        _logger.LogInformation("EContractFinancialCover");
        var head = await GetEContractHeaderHandler(request.Id, ct);
        if (head.IsBad()) return head.Failure<UpdateEContractFinancialResponse>()!;
        var value = head.Value!;

        var response = await UpdateEContractHeadFinancialHandler(request, value!, ct);
        if (response.IsBad()) return response.Failure<UpdateEContractFinancialResponse>()!;

        if (request.UpdateEContractFinancials.HasAny())
            foreach (var eContract in request.UpdateEContractFinancials!)
            {
                var contract = await GetEContractHandler(eContract.Id, ct);
                if (contract is null)
                    return Result.Failure<UpdateEContractFinancialResponse>(EContractErrors.FilteredEmployerContractNotFound)!;
                var res = await UpdateEContractFinancial(value, eContract, ct);
                if (res.IsBad()) return res.Failure<UpdateEContractFinancialResponse>()!;
            }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateEContractFinancialResponse(value.Id, true);
    }

    public async Task<Result<SetECStatusResponse?>> EContractStatusChanger(
        SetECStatusRequest request, CT ct)
    {
        _logger.LogInformation("EContractStatusChanger");
        var res = await StatusChanger(request, ct);
        if (res.IsBad()) return res.Failure<SetECStatusResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new SetECStatusResponse(true);
    }

    public async Task<Result<GetEContractHeaderByIdResponse?>> GetEContractHeaderById(
        GetEContractHeaderByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetEContractHeaderById");
        var result = await GetEContractHeaderByIdHandler(request.Id, ct);
        if (result.IsBad()) return result.Failure<GetEContractHeaderByIdResponse>()!;
        var value = result.Value;
        var empIds = value.EmployerId.ToDataList();
        var employers = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(empIds, null, null, _mediator, ct);
        result.Value!.Employer = employers.FirstOrDefault(x => x.Id == value.EmployerId)!.FullName!;
        var currency = await WebServicesLogic.CurrencyDataReceiver(value.CurrencyId, _mediator, ct);
        result.Value.Currency = currency?.Name;
        foreach (var x in result.Value.EContracts)
        {
            x.Currency = currency.Name;
            var productGroupIds = x.EmployerOperations?.Where(x => x.Products != null && x.Products.HasAny()).SelectMany(x => x.Products!).Listed(x => x.ProductGroupId);
            var productIds = x.EmployerOperations?.Where(x => x.Products != null && x.Products.HasAny()).SelectMany(x => x.Products!).NullListed(x => x.ProductId);
            List<ViewGroup> productGroups = [];
            List<ViewProduct> products = [];
            if (productGroupIds is not null && productGroupIds.Count() > 0)
                productGroups = await _productGroupRepo.GetByIds(productGroupIds, ct);

            if (productIds is not null && productIds.Count() > 0)
            {
                var productResults = await _productRepo.GetProductsByIds(productIds, false, null, 1, productIds.Count(), ct);
                products = productResults.Data;
            }

            if (x.EmployerOperations != null)
            {
                foreach (var item in x.EmployerOperations.SelectMany(x => x.Products ?? Enumerable.Empty<GetEOperationProductModel>()))
                {
                    item.ProductGroupName = productGroups.FirstOrDefault(pg => pg.Id == item.ProductGroupId)?.Name;
                    item.ProductName = products.FirstOrDefault(p => p.Id == item.ProductId)?.Name;
                }
            }
        }
        return result!;
    }

    public async Task<Result<GetEContractByIdResponse?>> GetEContractById(
        GetEContractByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetEContractById");
        var result = await GetEContractByIdHandler(request.Id, ct);
        if (result.IsBad()) return result.Failure<GetEContractByIdResponse>()!;
        var currency = await WebServicesLogic.CurrencyDataReceiver(result.Value?.CurrencyId, _mediator, ct);
        var value = result.Value!;
        var productGroupIds = value.EmployerOperations?.Where(x => x.Products != null && x.Products.HasAny()).SelectMany(x => x.Products!).Listed(x => x.ProductGroupId);
        var productIds = value.EmployerOperations?.Where(x => x.Products != null && x.Products.HasAny()).SelectMany(x => x.Products!).NullListed(x => x.ProductId);
        List<ViewGroup> productGroups = [];
        List<ViewProduct> products = [];
        if (productGroupIds is not null && productGroupIds.Count() > 0)
            productGroups = await _productGroupRepo.GetByIds(productGroupIds, ct);

        if (productIds is not null && productIds.Count() > 0)
        {
            var productResults = await _productRepo.GetProductsByIds(productIds, false, null, 1, productIds.Count(), ct);
            products = productResults.Data;
        }

        if (value.EmployerOperations != null)
        {
            foreach (var item in value.EmployerOperations.SelectMany(x => x.Products ?? Enumerable.Empty<GetEOperationProductModel>()))
            {
                item.ProductGroupName = productGroups.FirstOrDefault(pg => pg.Id == item.ProductGroupId)?.Name;
                item.ProductName = products.FirstOrDefault(p => p.Id == item.ProductId)?.Name;
            }
        }
        result.Value.Currency = currency?.Name;
        return result!;
    }

    public async Task<Result<GetFltrEContractHeadsResponse?>> GetFltrEContractHeads(
        GetFltrEContractHeadsRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrEContractHeads");
        var result = await GetFltrEContractHeadsHandler(request, ct);
        if (result.IsBad()) return result.Failure<GetFltrEContractHeadsResponse>()!;
        var values = result.Value!.Data!;

        var currIds = values.Listed(x => x.CurrencyId);
        var currencies = await _curRepo.GetCurrenciesByIds(currIds, ct);

        var empIds = values.Listed(x => x.EmployerId);
        var employers = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(empIds, null, null, _mediator, ct);

        var creatorIds = values.Listed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        foreach (var item in values)
        {
            item.Employer = employers?.FirstOrDefault(x => x.Id == item.EmployerId)?.FullName;
            item.Currency = currencies?.FirstOrDefault(x => x.Id == item.CurrencyId)?.Name;
            item.Creator = creators?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
        }

        return new GetFltrEContractHeadsResponse(result!.Value!.Data!, result.Value.RowCount);
    }

    public async Task<Result<GetFltrEContractsResponse?>> GetFltrEContracts(
        GetFltrEContractsRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrEContracts");
        var result = await GetFltrEContractsHandler(request, ct);
        if (result.IsBad()) return result.Failure<GetFltrEContractsResponse>()!;
        var values = result.Value!.Data!;

        var currIds = values.Listed(x => x.CurrencyId);
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currIds, _mediator, ct);

        var empIds = values.Listed(x => x.EmployerId);
        var employers = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(empIds, null, null, _mediator, ct);

        var creatorIds = values.Listed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        foreach (var item in values)
        {
            item.Employer = employers?.FirstOrDefault(x => x.Id == item.EmployerId)?.FullName;
            item.Currency = currencies?.FirstOrDefault(x => x.Id == item.CurrencyId)?.Name;
            item.Creator = creators?.FirstOrDefault(x => x.Id == item.CreatorId)?.FullName;
        }
        return new GetFltrEContractsResponse(result!.Value!.Data!, result.Value.RowCount);
    }

    public async Task<Result<GetEContractTypesResponse?>> GetEContractTypes(
        GetEContractTypesRequest request, CT ct)
    {
        _logger.LogInformation("GetEContractTypes");
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<EContractType>());
        return new GetEContractTypesResponse(result);
    }

    public async Task<Result<GetEContractStatusResponse?>> GetEContractStatus(
        GetEContractStatusRequest request, CT ct)
    {
        _logger.LogInformation("GetEContractStatus");
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<EContractStatus>());
        return new GetEContractStatusResponse(result);
    }

    public async Task<Result<GetConsiderationTypesResponse?>> GetConsiderationTypes(
        GetConsiderationTypesRequest request, CT ct)
    {
        _logger.LogInformation("GetConsiderationTypes");
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<ConsiderationType>());
        return new GetConsiderationTypesResponse(result);
    }

    public async Task<Result<GetEDocumentTypesResponse?>> GetEDocumentTypes(
        GetEDocumentTypesRequest request, CT ct)
    {
        _logger.LogInformation("GetEDocumentTypes");
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<EDocumentType>());
        return new GetEDocumentTypesResponse(result);
    }

    public async Task<Result<GetFltrEContractHeadsEnumResponse?>> GetFltrEContractHeadsEnum(
        GetFltrEContractHeadsEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFltrEContractHeadsEnum");
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<FltrEContractHeadsExcelEnum>());
        return new GetFltrEContractHeadsEnumResponse(result);
    }

    public async Task<Result<GetFltrEContractHeadsExporterResponse?>> GetFltrEContractHeadsExporter(
        GetFltrEContractHeadsExporterRequest request, CT ct)
    {
        var result = await GetFltrEContractHeadsHandler(request.Adapt<GetFltrEContractHeadsRequest>(), ct);
        if (result.IsBad()) return result.Failure<GetFltrEContractHeadsExporterResponse>()!;
        var values = result.Value!.Data!;

        var currIds = values.Listed(x => x.CurrencyId);
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currIds, _mediator, ct);

        var empIds = values.Listed(x => x.EmployerId);
        var employers = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(empIds, null, null, _mediator, ct);

        var creatorIds = values.Listed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        foreach (var item in values)
        {
            item.Employer = employers?.FirstOrDefault(x => x.Id == item.EmployerId)?.FullName;
            item.Currency = currencies?.FirstOrDefault(x => x.Id == item.CurrencyId)?.Name;
            item.Creator = creators?.FirstOrDefault(x => x.Id == item.CreatorId)?.FullName;
        }

        var file = new FileContentResult(EmployerContractExcels.GetFltrEContractHeadsToExcel(
        values, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ContractHeads-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };
        return new GetFltrEContractHeadsExporterResponse(file);
    }

    public async Task<Result<GetFltrEContractsEnumResponse?>> GetFltrEContractsEnum(
        GetFltrEContractsEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFltrEContractEnum");
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<FltrEContractsEnum>());
        return new GetFltrEContractsEnumResponse(result);
    }

    public async Task<Result<GetFltrEContractsExporterResponse?>> GetFltrEContractsExporter(
        GetFltrEContractsExporterRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrEContractsExporter");
        var result = await GetFltrEContractsHandler(request.Adapt<GetFltrEContractsRequest>(), ct);
        if (result.IsBad()) return result.Failure<GetFltrEContractsExporterResponse>()!;
        var values = result.Value!.Data!;

        var currIds = values.Listed(x => x.CurrencyId);
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currIds, _mediator, ct);

        var empIds = values.Listed(x => x.EmployerId);
        var employers = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(empIds, null, null, _mediator, ct);

        var creatorIds = values.Listed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        values.ForEach(e =>
        {
            if (employers is not null)
                e.Employer = employers?.FirstOrDefault(x => x!.Id == e.EmployerId)!.FullName;

            e.Currency = currencies?.FirstOrDefault(x => x.Id == e.CurrencyId)!.Name;
            e.Creator = creators?.FirstOrDefault(x => x.Id == e.CreatorId)?.FullName;
        });

        var file = new FileContentResult(EmployerContractExcels.GetFltrEContractsToExcel(
        values, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"FltrEContractHeads-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetFltrEContractsExporterResponse(file);
    }

    public async Task<Result<GetFltrEmployersResponse?>> GetFltrEmployers(
        GetFltrEmployersRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrEContracts");
        var result = await GetFltrEmployersHandler(request, ct);
        if (result.IsBad()) return result.Failure<GetFltrEmployersResponse>()!;
        var values = result.Value!;

        var data = new List<GetFltrEmployersModel>();
        var employers = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(values, request.FilterData, null, _mediator, ct);
        if (employers is not null && employers.Count > 0)
        {
            foreach (var employer in employers)
                data.Add(new GetFltrEmployersModel(employer!.Id, employer!.FullName));

            data = data.SetPaging(request.PageIndex - 1, request.PageSize);
        }

        return new GetFltrEmployersResponse(data, values.Count);
    }

    public async Task<Result<GetEContractHistoryResponse>> GetEContractHistory(
        GetEContractHistoryRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrEContractsHistory");
        var result = await GetEContractHistoryHandler(request, ct);
        if (result.IsBad()) return result.Failure<GetEContractHistoryResponse>()!;
        return new GetEContractHistoryResponse(result.Value!, result.Value!.Count);
    }

    public async Task<Result<GetEContractOperationHistoryResponse>> GetEContractOperationHistory(
        GetEContractOperationHistoryRequest request, CT ct)
    {
        _logger.LogInformation("GetEContractOperationHistory");
        var result = await GetEContractOperationHistoryHandler(request, ct);
        if (result.IsBad()) return result.Failure<GetEContractOperationHistoryResponse>()!;
        return new GetEContractOperationHistoryResponse(result.Value!, result.Value!.Count);
    }

    public async Task<Result<GetEOProductHistoryResponse>> GetEOProductHistory(
        GetEOProductHistoryRequest request, CT ct)
    {
        _logger.LogInformation("GetEContractOperationHistory");
        var result = await GetEOProductHistoryHandler(request, ct);
        if (result.IsBad()) return result.Failure<GetEOProductHistoryResponse>()!;
        return new GetEOProductHistoryResponse(result.Value!, result.Value!.Count);
    }

    public async Task<Result<GetEOServiceHistoryResponse>> GetEOServiceHistory(
        GetEOServiceHistoryRequest request, CT ct)
    {
        _logger.LogInformation("GetEContractOperationHistory");
        var result = await GetEOServiceHistoryHandler(request, ct);
        if (result.IsBad()) return result.Failure<GetEOServiceHistoryResponse>()!;
        return new GetEOServiceHistoryResponse(result.Value!, result.Value!.Count);
    }

    public async Task<Result<GetECThirdPartiesResponse?>> GetECThirdParties(
        GetECThirdPartiesRequest request, CT ct)
    {
        _logger.LogInformation("GetECThirdParties");
        var result = await _mediator.Send(new GetECThirdPartiesQuery(request.ProjectId, request.EmployerIds, request.PageIndex, request.PageSize), ct);
        if (result.IsBad()) return result.Failure<GetECThirdPartiesResponse>()!;
        return result;
    }
}