using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Currencies;
using Engineering.Application.Services.Contracts.Contracts.ChangeContractAdjustmentIndexState;
using Engineering.Application.Services.Contracts.Contracts.ChangeContractAdjustmentReferenceState;
using Engineering.Application.Services.Contracts.Contracts.ChangeContractGuaranteeStatus;
using Engineering.Application.Services.Contracts.Contracts.ChangeContractStatus;
using Engineering.Application.Services.Contracts.Contracts.CreateContract;
using Engineering.Application.Services.Contracts.Contracts.CreateContractAdjustmentConfiguration;
using Engineering.Application.Services.Contracts.Contracts.CreateContractAdjustmentIndex;
using Engineering.Application.Services.Contracts.Contracts.CreateContractAdjustmentReference;
using Engineering.Application.Services.Contracts.Contracts.CreateContractChange;
using Engineering.Application.Services.Contracts.Contracts.CreateContractFinancialInformation;
using Engineering.Application.Services.Contracts.Contracts.CreateContractGuarantee;
using Engineering.Application.Services.Contracts.Contracts.CreateContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.CreateContractSummaryChange;
using Engineering.Application.Services.Contracts.Contracts.CreateContractType;
using Engineering.Application.Services.Contracts.Contracts.CreateContractTypeDetail;
using Engineering.Application.Services.Contracts.Contracts.DeleteContract;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractAdjustmentConfiguration;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractChange;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractGuarantee;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractType;
using Engineering.Application.Services.Contracts.Contracts.DeleteContractTypeDetail;
using Engineering.Application.Services.Contracts.Contracts.FinalizeContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.GetAvailableContractTypeDetailSources;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentConfiguration;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexById;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexes;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferenceById;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferences;
using Engineering.Application.Services.Contracts.Contracts.GetContractById;
using Engineering.Application.Services.Contracts.Contracts.GetContractChangeAvailableItems;
using Engineering.Application.Services.Contracts.Contracts.GetContractChangeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractChanges;
using Engineering.Application.Services.Contracts.Contracts.GetContractFinancialInformation;
using Engineering.Application.Services.Contracts.Contracts.GetContractForProcesVerbal;
using Engineering.Application.Services.Contracts.Contracts.GetContractGuaranteeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractGuarantees;
using Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationById;
using Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationGrid;
using Engineering.Application.Services.Contracts.Contracts.GetContractsByStatus;
using Engineering.Application.Services.Contracts.Contracts.GetContractStructure;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetailById;
using Engineering.Application.Services.Contracts.Contracts.GetContractTypeDetails;
using Engineering.Application.Services.Contracts.Contracts.GetFilteredContracts;
using Engineering.Application.Services.Contracts.Contracts.UpdateContract;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractAdjustmentConfiguration;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractAdjustmentIndex;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractAdjustmentReference;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractChange;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractFinancialInformation;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractGuarantee;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractStructure;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractSummaryChange;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractType;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractTypeDetail;
using Engineering.Application.Services.Contracts.Queries.GetContractsByProjectIdForProcesVerbal;

namespace Engineering.Application.Services.Contracts;

public partial class ContractLogic : IContractLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ContractLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IContractRepository _contractRepository;
    private readonly IContractTypeRepository _contractTypeRepository;
    private readonly IContractTypeDetailRepository _contractTypeDetailRepository;
    private readonly IContractFinancialInformationRepository _contractFinancialInformationRepository;
    private readonly IContractAdjustmentConfigurationRepository _contractAdjustmentConfigurationRepository;
    private readonly IContractChangeRepository _contractChangeRepository;
    private readonly IContractAdjustmentReferenceRepository _contractAdjustmentReferenceRepository;
    private readonly IContractAdjustmentIndexRepository _contractAdjustmentIndexRepository;
    private readonly IContractGuaranteeRepository _contractGuaranteeRepository;
    private readonly IViewCurrencyRepository _currencyRepository;

    #region Constructor

    public ContractLogic(
        IMediator mediator,
        ILogger<ContractLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        IContractRepository contractRepository,
        IContractTypeRepository contractTypeRepository,
        IContractTypeDetailRepository contractTypeDetailRepository,
        IContractFinancialInformationRepository contractFinancialInformationRepository,
        IContractAdjustmentConfigurationRepository contractAdjustmentConfigurationRepository,
        IContractChangeRepository contractChangeRepository,
        IContractAdjustmentReferenceRepository contractAdjustmentReferenceRepository,
        IContractAdjustmentIndexRepository contractAdjustmentIndexRepository,
        IContractGuaranteeRepository contractGuaranteeRepository,
        IViewCurrencyRepository currencyRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _contractRepository = contractRepository;
        _contractTypeRepository = contractTypeRepository;
        _contractTypeDetailRepository = contractTypeDetailRepository;
        _contractFinancialInformationRepository = contractFinancialInformationRepository;
        _contractAdjustmentConfigurationRepository = contractAdjustmentConfigurationRepository;
        _contractChangeRepository = contractChangeRepository;
        _contractAdjustmentReferenceRepository = contractAdjustmentReferenceRepository;
        _contractAdjustmentIndexRepository = contractAdjustmentIndexRepository;
        _contractGuaranteeRepository = contractGuaranteeRepository;
        _currencyRepository = currencyRepository;
    }

    #endregion

    #region Contract Commands

    public async Task<Result<CreateContractResponse?>> CreateContract(
        CreateContractRequest request, CT ct)
    {
        _logger.LogInformation("CreateContract");

        var validation = await request.IsValidAsync<CreateContractValidator, CreateContractRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<CreateContractResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<CreateContractResponse>()!;

        var result = await CreateContractExecute(request, companyResult.Value, ct);
        if (result.IsBad())
            return result.Failure<CreateContractResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new CreateContractResponse(result.Value!.Id, result.Value.ContractNumber);
    }

    public async Task<Result<CreateContractRegistrationResponse?>> CreateContractRegistration(
        CreateContractRegistrationRequest request, CT ct)
    {
        _logger.LogInformation("CreateContractRegistration");

        var validation = await request.IsValidAsync<CreateContractRegistrationValidator, CreateContractRegistrationRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<CreateContractRegistrationResponse>()!;

        var company = await ResolveCompanyId(ct);
        if (company.IsBad())
            return company.Failure<CreateContractRegistrationResponse>()!;

        var result = await CreateContractRegistrationExecute(request, company.Value, ct);
        if (result.IsBad())
            return result.Failure<CreateContractRegistrationResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        var contract = result.Value!;
        return new CreateContractRegistrationResponse(contract.Id, contract.ContractNumber!.Value,
            contract.Status, contract.EndDate,
            contract.CalculateInitialAmount(), contract.CalculateFinalContractAmount(),
            contract.IsRegistrationPending, true);
    }

    public async Task<Result<UpdateContractRegistrationResponse?>> UpdateContractRegistration(
        UpdateContractRegistrationRequest request, CT ct)
    {
        _logger.LogInformation("UpdateContractRegistration");

        var validation = await request.IsValidAsync<UpdateContractRegistrationValidator, UpdateContractRegistrationRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<UpdateContractRegistrationResponse>()!;

        var company = await ResolveCompanyId(ct);
        if (company.IsBad())
            return company.Failure<UpdateContractRegistrationResponse>()!;

        var result = await UpdateContractRegistrationExecute(request, company.Value, ct);
        if (result.IsBad())
            return result.Failure<UpdateContractRegistrationResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new UpdateContractRegistrationResponse(true);
    }

    public async Task<Result<FinalizeContractRegistrationResponse?>> FinalizeContractRegistration(
        FinalizeContractRegistrationRequest request, CT ct)
    {
        _logger.LogInformation("FinalizeContractRegistration");

        var validation = await request.IsValidAsync<FinalizeContractRegistrationValidator,
            FinalizeContractRegistrationRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<FinalizeContractRegistrationResponse>()!;

        var company = await ResolveCompanyId(ct);
        if (company.IsBad())
            return company.Failure<FinalizeContractRegistrationResponse>()!;

        var result = await FinalizeContractRegistrationExecute(request, company.Value, ct);
        if (result.IsBad())
            return result.Failure<FinalizeContractRegistrationResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return result;
    }

    public async Task<Result<UpdateContractResponse?>> UpdateContract(
        UpdateContractRequest request, CT ct)
    {
        _logger.LogInformation("UpdateContract");

        var validation = await request.IsValidAsync<UpdateContractValidator, UpdateContractRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<UpdateContractResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<UpdateContractResponse>()!;

        var result = await UpdateContractExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<UpdateContractResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new UpdateContractResponse(true);
    }

    public async Task<Result<DeleteContractResponse?>> DeleteContract(DeleteContractRequest request, CT ct)
    {
        _logger.LogInformation("DeleteContract");

        var validation = await request.IsValidAsync<DeleteContractValidator, DeleteContractRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<DeleteContractResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<DeleteContractResponse>()!;

        var result = await DeleteContractExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<DeleteContractResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new DeleteContractResponse(true);
    }

    public async Task<Result<ChangeContractStatusResponse?>> ChangeContractStatus(
        ChangeContractStatusRequest request,
        CT ct)
    {
        _logger.LogInformation("ChangeContractStatus");

        var validation = await request.IsValidAsync<ChangeContractStatusValidator, ChangeContractStatusRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<ChangeContractStatusResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<ChangeContractStatusResponse>()!;

        var result = await ChangeContractStatusExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<ChangeContractStatusResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new ChangeContractStatusResponse(true);
    }

    public async Task<Result<UpdateContractStructureResponse?>> UpdateContractStructure(
        UpdateContractStructureRequest request,
        CT ct)
    {
        _logger.LogInformation("UpdateContractStructure");

        var validation = await request.IsValidAsync<UpdateContractStructureValidator, UpdateContractStructureRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<UpdateContractStructureResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<UpdateContractStructureResponse>()!;

        var result = await UpdateContractStructureExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<UpdateContractStructureResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new UpdateContractStructureResponse(true);
    }

    #endregion

    #region ContractFinancialInformation Commands

    public async Task<Result<CreateContractFinancialInformationResponse?>> CreateContractFinancialInformation(
        CreateContractFinancialInformationRequest request,
        CT ct)
    {
        _logger.LogInformation("CreateContractFinancialInformation");

        var validation = await request.IsValidAsync<
            CreateContractFinancialInformationValidator,
            CreateContractFinancialInformationRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<CreateContractFinancialInformationResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<CreateContractFinancialInformationResponse>()!;

        var result = await CreateContractFinancialInformationExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<CreateContractFinancialInformationResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new CreateContractFinancialInformationResponse(result.Value!.Id, true);
    }

    public async Task<Result<UpdateContractFinancialInformationResponse?>> UpdateContractFinancialInformation(
        UpdateContractFinancialInformationRequest request,
        CT ct)
    {
        _logger.LogInformation("UpdateContractFinancialInformation");

        var validation = await request.IsValidAsync<
            UpdateContractFinancialInformationValidator,
            UpdateContractFinancialInformationRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<UpdateContractFinancialInformationResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<UpdateContractFinancialInformationResponse>()!;

        var result = await UpdateContractFinancialInformationExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<UpdateContractFinancialInformationResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new UpdateContractFinancialInformationResponse(true);
    }

    #endregion

    #region ContractChange Commands

    public async Task<Result<CreateContractChangeResponse?>> CreateContractChange(
        CreateContractChangeRequest request,
        CT ct)
    {
        _logger.LogInformation("CreateContractChange");

        var validation = await request.IsValidAsync<
            CreateContractChangeValidator,
            CreateContractChangeRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<CreateContractChangeResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<CreateContractChangeResponse>()!;

        var result = await CreateContractChangeExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<CreateContractChangeResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new CreateContractChangeResponse(result.Value!.Id, true);
    }

    public async Task<Result<CreateContractSummaryChangeResponse?>> CreateContractSummaryChange(
        CreateContractSummaryChangeRequest request, CT ct)
    {
        _logger.LogInformation("CreateContractSummaryChange");

        var validation = await request.IsValidAsync<CreateContractSummaryChangeValidator, CreateContractSummaryChangeRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<CreateContractSummaryChangeResponse>()!;

        var company = await ResolveCompanyId(ct);
        if (company.IsBad())
            return company.Failure<CreateContractSummaryChangeResponse>()!;

        var result = await CreateContractSummaryChangeExecute(request, company.Value, ct);
        if (result.IsBad())
            return result.Failure<CreateContractSummaryChangeResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        var execution = result.Value!;
        return new CreateContractSummaryChangeResponse(
            execution.Change.Id,
            execution.FinalContractAmount,
            execution.NewEndDate,
            true);
    }

    public async Task<Result<UpdateContractChangeResponse?>> UpdateContractChange(
        UpdateContractChangeRequest request,
        CT ct)
    {
        _logger.LogInformation("UpdateContractChange");

        var validation = await request.IsValidAsync<
            UpdateContractChangeValidator,
            UpdateContractChangeRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<UpdateContractChangeResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<UpdateContractChangeResponse>()!;

        var result = await UpdateContractChangeExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<UpdateContractChangeResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new UpdateContractChangeResponse(true);
    }

    public async Task<Result<UpdateContractSummaryChangeResponse?>> UpdateContractSummaryChange(
        UpdateContractSummaryChangeRequest request, CT ct)
    {
        _logger.LogInformation("UpdateContractSummaryChange");

        var validation = await request.IsValidAsync<UpdateContractSummaryChangeValidator, UpdateContractSummaryChangeRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<UpdateContractSummaryChangeResponse>()!;

        var company = await ResolveCompanyId(ct);
        if (company.IsBad())
            return company.Failure<UpdateContractSummaryChangeResponse>()!;

        var result = await UpdateContractSummaryChangeExecute(request, company.Value, ct);
        if (result.IsBad())
            return result;

        await _unitOfWork.CommitAsync(ct);

        return result;
    }

    public async Task<Result<DeleteContractChangeResponse?>> DeleteContractChange(
        DeleteContractChangeRequest request,
        CT ct)
    {
        _logger.LogInformation("DeleteContractChange");

        var validation = await request.IsValidAsync<
            DeleteContractChangeValidator,
            DeleteContractChangeRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<DeleteContractChangeResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<DeleteContractChangeResponse>()!;

        var result = await DeleteContractChangeExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<DeleteContractChangeResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new DeleteContractChangeResponse(true);
    }

    #endregion

    #region Contract Queries

    public async Task<Result<GetContractByIdResponse?>> GetContractById(GetContractByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetContractById");

        var validation = await request.IsValidAsync<GetContractByIdValidator, GetContractByIdRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<GetContractByIdResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractByIdResponse>()!;

        return await GetContractByIdExecute(request, companyResult.Value, ct);
    }

    public async Task<Result<GetContractRegistrationByIdResponse?>> GetContractRegistrationById(
        GetContractRegistrationByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetContractRegistrationById");

        var validation = await request.IsValidAsync<GetContractRegistrationByIdValidator, GetContractRegistrationByIdRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<GetContractRegistrationByIdResponse>()!;

        var company = await ResolveCompanyId(ct);
        if (company.IsBad())
            return company.Failure<GetContractRegistrationByIdResponse>()!;

        return await GetContractRegistrationByIdExecute(request, company.Value, ct);
    }

    public async Task<Result<GetContractRegistrationGridResponse?>> GetContractRegistrationGrid(
        GetContractRegistrationGridRequest request, CT ct)
    {
        _logger.LogInformation("GetContractRegistrationGrid");

        var validation = await request.IsValidAsync<GetContractRegistrationGridValidator, GetContractRegistrationGridRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<GetContractRegistrationGridResponse>()!;

        var company = await ResolveCompanyId(ct);
        if (company.IsBad())
            return company.Failure<GetContractRegistrationGridResponse>()!;

        return await GetContractRegistrationGridExecute(request, company.Value, ct);
    }

    public async Task<Result<GetFilteredContractsResponse?>> GetFilteredContracts(
        GetFilteredContractsRequest request,
        CT ct)
    {
        _logger.LogInformation("GetFilteredContracts");

        var validation = await request.IsValidAsync<GetFilteredContractsValidator, GetFilteredContractsRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<GetFilteredContractsResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetFilteredContractsResponse>()!;

        return await GetFilteredContractsExecute(request, companyResult.Value, ct);
    }

    public async Task<Result<GetContractsByStatusResponse?>> GetContractsByStatus(
        GetContractsByStatusRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractsByStatus");

        var validation = await request.IsValidAsync<GetContractsByStatusValidator, GetContractsByStatusRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<GetContractsByStatusResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractsByStatusResponse>()!;

        return await GetContractsByStatusExecute(request, companyResult.Value, ct);
    }

    public async Task<Result<GetContractStructureResponse?>> GetContractStructure(
        GetContractStructureRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractStructure");

        var validation = await request.IsValidAsync<GetContractStructureValidator, GetContractStructureRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<GetContractStructureResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractStructureResponse>()!;

        return await GetContractStructureExecute(request, companyResult.Value, ct);
    }

    public async Task<Result<GetContractFinancialInformationResponse?>> GetContractFinancialInformation(
        GetContractFinancialInformationRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractFinancialInformation");

        var validation = await request.IsValidAsync<
            GetContractFinancialInformationValidator,
            GetContractFinancialInformationRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<GetContractFinancialInformationResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractFinancialInformationResponse>()!;

        return await GetContractFinancialInformationExecute(request, companyResult.Value, ct);
    }

    #endregion

    #region ContractType and ContractTypeDetail Commands

    public async Task<Result<CreateContractTypeResponse?>> CreateContractType(CreateContractTypeRequest request, CT ct)
    {
        _logger.LogInformation("CreateContractType");

        var validation = await request.IsValidAsync<CreateContractTypeValidator, CreateContractTypeRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<CreateContractTypeResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<CreateContractTypeResponse>()!;

        var result = await CreateContractTypeExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<CreateContractTypeResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new CreateContractTypeResponse(result.Value!.Id, true);
    }

    public async Task<Result<CreateContractTypeDetailResponse?>> CreateContractTypeDetail(
        CreateContractTypeDetailRequest request,
        CT ct)
        {
            _logger.LogInformation("CreateContractTypeDetail");

            var validation = await request.IsValidAsync<
                CreateContractTypeDetailValidator,
                CreateContractTypeDetailRequest>(ct);

            if (validation.IsBad())
                return validation.Failure<CreateContractTypeDetailResponse>()!;

            var companyResult = await ResolveCompanyId(ct);
            if (companyResult.IsBad())
                return companyResult.Failure<CreateContractTypeDetailResponse>()!;

            var result = await CreateContractTypeDetailExecute(request, companyResult.Value, ct);

            if (result.IsBad())
                return result.Failure<CreateContractTypeDetailResponse>()!;

            await _unitOfWork.CommitAsync(ct);

            return new CreateContractTypeDetailResponse(result.Value!.Id, true);
        }

    public async Task<Result<UpdateContractTypeResponse?>> UpdateContractType(UpdateContractTypeRequest request, CT ct)
    {
        _logger.LogInformation("UpdateContractType");

        var validation = await request.IsValidAsync<UpdateContractTypeValidator, UpdateContractTypeRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<UpdateContractTypeResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<UpdateContractTypeResponse>()!;

        var result = await UpdateContractTypeExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<UpdateContractTypeResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new UpdateContractTypeResponse(true);
    }

    public async Task<Result<DeleteContractTypeResponse?>> DeleteContractType(DeleteContractTypeRequest request, CT ct)
    {
        _logger.LogInformation("DeleteContractType");

        var validation = await request.IsValidAsync<DeleteContractTypeValidator, DeleteContractTypeRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<DeleteContractTypeResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<DeleteContractTypeResponse>()!;

        var result = await DeleteContractTypeExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<DeleteContractTypeResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new DeleteContractTypeResponse(true);
    }

    #endregion

    #region ContractType and ContractTypeDetail Queries

    public async Task<Result<GetContractTypeByIdResponse?>> GetContractTypeById(
        GetContractTypeByIdRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractTypeById");

        var validation = await request.IsValidAsync<GetContractTypeByIdValidator, GetContractTypeByIdRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<GetContractTypeByIdResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractTypeByIdResponse>()!;

        return await GetContractTypeByIdExecute(request, companyResult.Value, ct);
    }

    public async Task<Result<UpdateContractTypeDetailResponse?>> UpdateContractTypeDetail(
        UpdateContractTypeDetailRequest request,
        CT ct)
    {
        _logger.LogInformation("UpdateContractTypeDetail");

        var validation = await request.IsValidAsync<
            UpdateContractTypeDetailValidator,
            UpdateContractTypeDetailRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<UpdateContractTypeDetailResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<UpdateContractTypeDetailResponse>()!;

        var result = await UpdateContractTypeDetailExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<UpdateContractTypeDetailResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new UpdateContractTypeDetailResponse(true);
    }

    public async Task<Result<DeleteContractTypeDetailResponse?>> DeleteContractTypeDetail(
        DeleteContractTypeDetailRequest request,
        CT ct)
    {
        _logger.LogInformation("DeleteContractTypeDetail");

        var validation = await request.IsValidAsync<
            DeleteContractTypeDetailValidator,
            DeleteContractTypeDetailRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<DeleteContractTypeDetailResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<DeleteContractTypeDetailResponse>()!;

        var result = await DeleteContractTypeDetailExecute(request, companyResult.Value, ct);

        if (result.IsBad())
            return result.Failure<DeleteContractTypeDetailResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new DeleteContractTypeDetailResponse(true);
    }

    public async Task<Result<GetContractTypeDetailByIdResponse?>> GetContractTypeDetailById(
        GetContractTypeDetailByIdRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractTypeDetailById");

        var validation = await request.IsValidAsync<
            GetContractTypeDetailByIdValidator,
            GetContractTypeDetailByIdRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<GetContractTypeDetailByIdResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractTypeDetailByIdResponse>()!;

        return await GetContractTypeDetailByIdExecute(request, companyResult.Value, ct);
    }

    public async Task<Result<GetContractTypeDetailsResponse?>> GetContractTypeDetails(
        GetContractTypeDetailsRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractTypeDetails");

        var validation = await request.IsValidAsync<
            GetContractTypeDetailsValidator,
            GetContractTypeDetailsRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<GetContractTypeDetailsResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractTypeDetailsResponse>()!;

        return await GetContractTypeDetailsExecute(request, companyResult.Value, ct);
    }

    public async Task<Result<GetAvailableContractTypeDetailSourcesResponse?>> GetAvailableContractTypeDetailSources(
        GetAvailableContractTypeDetailSourcesRequest request,
        CT ct)
    {
        _logger.LogInformation("GetAvailableContractTypeDetailSources");

        var validation = await request.IsValidAsync<
            GetAvailableContractTypeDetailSourcesValidator,
            GetAvailableContractTypeDetailSourcesRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<GetAvailableContractTypeDetailSourcesResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetAvailableContractTypeDetailSourcesResponse>()!;

        return await GetAvailableContractTypeDetailSourcesExecute(request, companyResult.Value, ct);
    }

    #endregion

    #region ContractChange Queries

    public async Task<Result<GetContractChangeByIdResponse?>> GetContractChangeById(
        GetContractChangeByIdRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractChangeById");

        var validation = await request.IsValidAsync<
            GetContractChangeByIdValidator,
            GetContractChangeByIdRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<GetContractChangeByIdResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractChangeByIdResponse>()!;

        return await GetContractChangeByIdExecute(request, companyResult.Value, ct);
    }

    public async Task<Result<GetContractChangesResponse?>> GetContractChanges(
        GetContractChangesRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractChanges");

        var validation = await request.IsValidAsync<
            GetContractChangesValidator,
            GetContractChangesRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<GetContractChangesResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractChangesResponse>()!;

        return await GetContractChangesExecute(request, companyResult.Value, ct);
    }

    public async Task<Result<GetContractChangeAvailableItemsResponse?>>
        GetContractChangeAvailableItems(
            GetContractChangeAvailableItemsRequest request,
            CT ct)
    {
        _logger.LogInformation("GetContractChangeAvailableItems");

        var validation = await request.IsValidAsync<
            GetContractChangeAvailableItemsValidator,
            GetContractChangeAvailableItemsRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<GetContractChangeAvailableItemsResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractChangeAvailableItemsResponse>()!;

        return await GetContractChangeAvailableItemsExecute(request, companyResult.Value, ct);
    }

    #endregion

    #region ContractAdjustmentReference Commands

    public async Task<Result<CreateContractAdjustmentReferenceResponse?>> CreateContractAdjustmentReference(
        CreateContractAdjustmentReferenceRequest request,
        CT ct)
    {
        _logger.LogInformation("CreateContractAdjustmentReference");

        var validation = await request.IsValidAsync<
            CreateContractAdjustmentReferenceValidator,
            CreateContractAdjustmentReferenceRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<CreateContractAdjustmentReferenceResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<CreateContractAdjustmentReferenceResponse>()!;

        var result = await CreateContractAdjustmentReferenceExecute(request, ct);

        if (result.IsBad())
            return result.Failure<CreateContractAdjustmentReferenceResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new CreateContractAdjustmentReferenceResponse(result.Value!.Id, true);
    }

    public async Task<Result<UpdateContractAdjustmentReferenceResponse?>> UpdateContractAdjustmentReference(
        UpdateContractAdjustmentReferenceRequest request,
        CT ct)
    {
        _logger.LogInformation("UpdateContractAdjustmentReference");

        var validation = await request.IsValidAsync<
            UpdateContractAdjustmentReferenceValidator,
            UpdateContractAdjustmentReferenceRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<UpdateContractAdjustmentReferenceResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<UpdateContractAdjustmentReferenceResponse>()!;

        var result = await UpdateContractAdjustmentReferenceExecute(request, ct);

        if (result.IsBad())
            return result.Failure<UpdateContractAdjustmentReferenceResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new UpdateContractAdjustmentReferenceResponse(true);
    }

    public async Task<Result<ChangeContractAdjustmentReferenceStateResponse?>> ChangeContractAdjustmentReferenceState(
        ChangeContractAdjustmentReferenceStateRequest request,
        CT ct)
    {
        _logger.LogInformation("ChangeContractAdjustmentReferenceState");

        var validation = await request.IsValidAsync<
            ChangeContractAdjustmentReferenceStateValidator,
            ChangeContractAdjustmentReferenceStateRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<ChangeContractAdjustmentReferenceStateResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<ChangeContractAdjustmentReferenceStateResponse>()!;

        var result = await ChangeContractAdjustmentReferenceStateExecute(request, ct);

        if (result.IsBad())
            return result.Failure<ChangeContractAdjustmentReferenceStateResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new ChangeContractAdjustmentReferenceStateResponse(true);
    }

    public async Task<Result<CreateContractAdjustmentIndexResponse?>> CreateContractAdjustmentIndex(
        CreateContractAdjustmentIndexRequest request,
        CT ct)
    {
        _logger.LogInformation("CreateContractAdjustmentIndex");

        var validation = await request.IsValidAsync<
            CreateContractAdjustmentIndexValidator,
            CreateContractAdjustmentIndexRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<CreateContractAdjustmentIndexResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<CreateContractAdjustmentIndexResponse>()!;

        var result = await CreateContractAdjustmentIndexExecute(request, ct);

        if (result.IsBad())
            return result.Failure<CreateContractAdjustmentIndexResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new CreateContractAdjustmentIndexResponse(result.Value!.Id, true);
    }

    public async Task<Result<UpdateContractAdjustmentIndexResponse?>> UpdateContractAdjustmentIndex(
        UpdateContractAdjustmentIndexRequest request,
        CT ct)
    {
        _logger.LogInformation("UpdateContractAdjustmentIndex");

        var validation = await request.IsValidAsync<
            UpdateContractAdjustmentIndexValidator,
            UpdateContractAdjustmentIndexRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<UpdateContractAdjustmentIndexResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<UpdateContractAdjustmentIndexResponse>()!;

        var result = await UpdateContractAdjustmentIndexExecute(request, ct);

        if (result.IsBad())
            return result.Failure<UpdateContractAdjustmentIndexResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new UpdateContractAdjustmentIndexResponse(true);
    }

    public async Task<Result<ChangeContractAdjustmentIndexStateResponse?>> ChangeContractAdjustmentIndexState(
        ChangeContractAdjustmentIndexStateRequest request,
        CT ct)
    {
        _logger.LogInformation("ChangeContractAdjustmentIndexState");

        var validation = await request.IsValidAsync<
            ChangeContractAdjustmentIndexStateValidator,
            ChangeContractAdjustmentIndexStateRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<ChangeContractAdjustmentIndexStateResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<ChangeContractAdjustmentIndexStateResponse>()!;

        var result = await ChangeContractAdjustmentIndexStateExecute(request, ct);

        if (result.IsBad())
            return result.Failure<ChangeContractAdjustmentIndexStateResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new ChangeContractAdjustmentIndexStateResponse(true);
    }

    #endregion

    #region ContractAdjustmentReference Queries

    public async Task<Result<GetContractAdjustmentReferenceByIdResponse?>> GetContractAdjustmentReferenceById(
        GetContractAdjustmentReferenceByIdRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractAdjustmentReferenceById");

        var validation = await request.IsValidAsync<
            GetContractAdjustmentReferenceByIdValidator,
            GetContractAdjustmentReferenceByIdRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<GetContractAdjustmentReferenceByIdResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractAdjustmentReferenceByIdResponse>()!;

        return await GetContractAdjustmentReferenceByIdExecute(request, ct);
    }

    public async Task<Result<GetContractAdjustmentReferencesResponse?>> GetContractAdjustmentReferences(
        GetContractAdjustmentReferencesRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractAdjustmentReferences");

        var validation = await request.IsValidAsync<
            GetContractAdjustmentReferencesValidator,
            GetContractAdjustmentReferencesRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<GetContractAdjustmentReferencesResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractAdjustmentReferencesResponse>()!;

        return await GetContractAdjustmentReferencesExecute(request, ct);
    }

    public async Task<Result<GetContractAdjustmentIndexByIdResponse?>> GetContractAdjustmentIndexById(
        GetContractAdjustmentIndexByIdRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractAdjustmentIndexById");

        var validation = await request.IsValidAsync<
            GetContractAdjustmentIndexByIdValidator,
            GetContractAdjustmentIndexByIdRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<GetContractAdjustmentIndexByIdResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractAdjustmentIndexByIdResponse>()!;

        return await GetContractAdjustmentIndexByIdExecute(request, ct);
    }

    public async Task<Result<GetContractAdjustmentIndexesResponse?>> GetContractAdjustmentIndexes(
        GetContractAdjustmentIndexesRequest request,
        CT ct)
    {
        _logger.LogInformation("GetContractAdjustmentIndexes");

        var validation = await request.IsValidAsync<
            GetContractAdjustmentIndexesValidator,
            GetContractAdjustmentIndexesRequest>(ct);

        if (validation.IsBad())
            return validation.Failure<GetContractAdjustmentIndexesResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractAdjustmentIndexesResponse>()!;

        return await GetContractAdjustmentIndexesExecute(request, ct);
    }

    #endregion

    #region ContractAdjustmentConfiguration Commands

    public async Task<Result<CreateContractAdjustmentConfigurationResponse?>> CreateContractAdjustmentConfiguration(
        CreateContractAdjustmentConfigurationRequest request, CT ct)
    {
        _logger.LogInformation("CreateContractAdjustmentConfiguration");

        var validation = await request.IsValidAsync<CreateContractAdjustmentConfigurationValidator, CreateContractAdjustmentConfigurationRequest>(ct);
        if (validation.IsFailure)
            return validation.Failure<CreateContractAdjustmentConfigurationResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<CreateContractAdjustmentConfigurationResponse>()!;

        var result = await CreateContractAdjustmentConfigurationExecute(request, companyResult.Value, ct);
        if (result.IsBad())
            return result.Failure<CreateContractAdjustmentConfigurationResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new CreateContractAdjustmentConfigurationResponse(result.Value!.Id, true);
    }

    public async Task<Result<UpdateContractAdjustmentConfigurationResponse?>> UpdateContractAdjustmentConfiguration(
        UpdateContractAdjustmentConfigurationRequest request, CT ct)
    {
        _logger.LogInformation("UpdateContractAdjustmentConfiguration");

        var validation = await request.IsValidAsync<UpdateContractAdjustmentConfigurationValidator, UpdateContractAdjustmentConfigurationRequest>(ct);
        if (validation.IsFailure)
            return validation.Failure<UpdateContractAdjustmentConfigurationResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<UpdateContractAdjustmentConfigurationResponse>()!;

        var result = await UpdateContractAdjustmentConfigurationExecute(request, companyResult.Value, ct);
        if (result.IsFailure)
            return result.Failure<UpdateContractAdjustmentConfigurationResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new UpdateContractAdjustmentConfigurationResponse(true);
    }

    public async Task<Result<DeleteContractAdjustmentConfigurationResponse?>> DeleteContractAdjustmentConfiguration(
        DeleteContractAdjustmentConfigurationRequest request, CT ct)
    {
        _logger.LogInformation("DeleteContractAdjustmentConfiguration");

        var validation = await request.IsValidAsync<DeleteContractAdjustmentConfigurationValidator, DeleteContractAdjustmentConfigurationRequest>(ct);
        if (validation.IsFailure)
            return validation.Failure<DeleteContractAdjustmentConfigurationResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<DeleteContractAdjustmentConfigurationResponse>()!;

        var result = await DeleteContractAdjustmentConfigurationExecute(request, companyResult.Value, ct);
        if (result.IsFailure)
            return result.Failure<DeleteContractAdjustmentConfigurationResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new DeleteContractAdjustmentConfigurationResponse(true);
    }

    #endregion

    #region ContractAdjustmentConfiguration Queries

    public async Task<Result<GetContractAdjustmentConfigurationResponse?>> GetContractAdjustmentConfiguration(
        GetContractAdjustmentConfigurationRequest request, CT ct)
    {
        _logger.LogInformation("GetContractAdjustmentConfiguration");

        var validation = await request.IsValidAsync<GetContractAdjustmentConfigurationValidator, GetContractAdjustmentConfigurationRequest>(ct);
        if (validation.IsFailure)
            return validation.Failure<GetContractAdjustmentConfigurationResponse>()!;

        var companyResult = await ResolveCompanyId(ct);
        if (companyResult.IsBad())
            return companyResult.Failure<GetContractAdjustmentConfigurationResponse>()!;

        return await GetContractAdjustmentConfigurationExecute(request, companyResult.Value, ct);
    }

    #endregion

    #region ContractGuarantee Commands

    public async Task<Result<CreateContractGuaranteeResponse?>> CreateContractGuarantee(
        CreateContractGuaranteeRequest request, CT ct)
    {
        _logger.LogInformation("CreateContractGuarantee");

        var validation = await request.IsValidAsync<CreateContractGuaranteeValidator, CreateContractGuaranteeRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<CreateContractGuaranteeResponse>()!;

        var company = await ResolveCompanyId(ct);
        if (company.IsBad())
            return company.Failure<CreateContractGuaranteeResponse>()!;

        var result = await CreateContractGuaranteeExecute(request, company.Value, ct);
        if (result.IsBad())
            return result.Failure<CreateContractGuaranteeResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return new CreateContractGuaranteeResponse(result.Value!.Id, true);
    }

    public async Task<Result<UpdateContractGuaranteeResponse?>> UpdateContractGuarantee(
        UpdateContractGuaranteeRequest request, CT ct)
    {
        _logger.LogInformation("UpdateContractGuarantee");

        var validation = await request.IsValidAsync<UpdateContractGuaranteeValidator, UpdateContractGuaranteeRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<UpdateContractGuaranteeResponse>()!;

        var company = await ResolveCompanyId(ct);
        if (company.IsBad())
            return company.Failure<UpdateContractGuaranteeResponse>()!;

        var result = await UpdateContractGuaranteeExecute(request, company.Value, ct);
        if (result.IsBad())
            return result.Failure<UpdateContractGuaranteeResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return result;
    }

    public async Task<Result<ChangeContractGuaranteeStatusResponse?>> ChangeContractGuaranteeStatus(
        ChangeContractGuaranteeStatusRequest request, CT ct)
    {
        _logger.LogInformation("ChangeContractGuaranteeStatus");

        var validation = await request.IsValidAsync<ChangeContractGuaranteeStatusValidator, ChangeContractGuaranteeStatusRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<ChangeContractGuaranteeStatusResponse>()!;

        var company = await ResolveCompanyId(ct);
        if (company.IsBad())
            return company.Failure<ChangeContractGuaranteeStatusResponse>()!;

        var result = await ChangeContractGuaranteeStatusExecute(request, company.Value, ct);
        if (result.IsBad())
            return result.Failure<ChangeContractGuaranteeStatusResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return result;
    }

    public async Task<Result<DeleteContractGuaranteeResponse?>> DeleteContractGuarantee(
        DeleteContractGuaranteeRequest request, CT ct)
    {
        _logger.LogInformation("DeleteContractGuarantee");

        var validation = await request.IsValidAsync<DeleteContractGuaranteeValidator, DeleteContractGuaranteeRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<DeleteContractGuaranteeResponse>()!;

        var company = await ResolveCompanyId(ct);
        if (company.IsBad())
            return company.Failure<DeleteContractGuaranteeResponse>()!;

        var result = await DeleteContractGuaranteeExecute(request, company.Value, ct);
        if (result.IsBad())
            return result.Failure<DeleteContractGuaranteeResponse>()!;

        await _unitOfWork.CommitAsync(ct);

        return result;
    }

    #endregion

    #region ContractGuarantee Queries

    public async Task<Result<GetContractGuaranteeByIdResponse?>> GetContractGuaranteeById(
        GetContractGuaranteeByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetContractGuaranteeById");

        var validation = await request.IsValidAsync<GetContractGuaranteeByIdValidator, GetContractGuaranteeByIdRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<GetContractGuaranteeByIdResponse>()!;

        var company = await ResolveCompanyId(ct);
        if (company.IsBad())
            return company.Failure<GetContractGuaranteeByIdResponse>()!;

        var response = await _contractGuaranteeRepository.GetContractGuaranteeById(request.ContractId, request.Id, company.Value, ct);
        return response is null
            ? Result.Failure<GetContractGuaranteeByIdResponse>(ContractErrors.ContractGuaranteeNotFound)
            : response;
    }

    public async Task<Result<GetContractGuaranteesResponse?>> GetContractGuarantees(
        GetContractGuaranteesRequest request, CT ct)
    {
        _logger.LogInformation("GetContractGuarantees");

        var validation = await request.IsValidAsync<GetContractGuaranteesValidator, GetContractGuaranteesRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<GetContractGuaranteesResponse>()!;

        var company = await ResolveCompanyId(ct);
        if (company.IsBad())
            return company.Failure<GetContractGuaranteesResponse>()!;

        var contract = await _contractRepository.GetContract(request.ContractId, company.Value, ct);
        if (contract is null)
            return Result.Failure<GetContractGuaranteesResponse>(ContractErrors.ContractNotFound);

        var items = await _contractGuaranteeRepository.GetContractGuarantees(request.ContractId, company.Value, ct);
        return new GetContractGuaranteesResponse(request.ContractId, items);
    }

    #endregion

    #region ProcesVerbal Queries

    public async Task<Result<List<GetContractForProcesVerbalResponse>>> GetContractForProcesVerbal(
        GetContractForProcesVerbalRequest request, CT ct)
    {
        _logger.LogInformation("GetContractsByProjectIdForProcesVerbal");

        var validation = await request.IsValidAsync<GetContractForProcesVerbalValidator, GetContractForProcesVerbalRequest>(ct);
        if (validation.IsBad())
            return validation.Failure<List<GetContractForProcesVerbalResponse>>()!;

        var response = await _mediator.Send(new GetContractForProcesVerbalCommand(
            request.ProjectId), ct);
        return response!;
    }

    #endregion
}
