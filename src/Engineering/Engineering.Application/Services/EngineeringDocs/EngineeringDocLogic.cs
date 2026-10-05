using Engineering.Application.Abstractions.Data.EngineeringDocs;
using Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDoc;
using Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDocHistory;
using Engineering.Application.Services.EngineeringDocs.Contracts.DeleteProjectDoc;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDiscipline;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDoc;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDocType;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocById;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocs;
using Engineering.Application.Services.EngineeringDocs.Contracts.ProjectDocCodeGenerator;
using Engineering.Application.Services.EngineeringDocs.Contracts.SeedeDocTypes;
using Engineering.Application.Services.EngineeringDocs.Contracts.UpdateProjectDoc;

namespace Engineering.Application.Services.EngineeringDocs;

public partial class EngineeringDocLogic : IEngineeringDocLogic
{
    private readonly ILogger<EngineeringDocLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IDisciplineDocRepository _disciplineDocRepository;
    private readonly IDisciplineDocTypeRepository _disciplineDocTypeRepository;
    private readonly IDisciplineRepository _disciplineRepository;
    private readonly IProjectDocRepository _projectDocRepository;
    private readonly IProjectDocHistoryRepository _projectDocHistoryRepository;
    private readonly IMediator _mediator;

    public EngineeringDocLogic(
        ILogger<EngineeringDocLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        IDisciplineDocRepository disciplineDocRepository,
        IDisciplineDocTypeRepository disciplineDocTypeRepository,
        IDisciplineRepository disciplineRepository,
        IProjectDocRepository projectDocRepository,
        IProjectDocHistoryRepository projectDocHistoryRepository,
     IMediator mediator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _disciplineDocRepository = disciplineDocRepository;
        _disciplineDocTypeRepository = disciplineDocTypeRepository;
        _disciplineRepository = disciplineRepository;
        _projectDocRepository = projectDocRepository;
        _mediator = mediator;
        _projectDocHistoryRepository = projectDocHistoryRepository;
    }

    public async Task<Result<SeedDocTypeResponse?>> DocTypeSeeder(
        CT ct)
    {
        var result = await DocTypeSeederCommand(ct);

        if (result.IsFailure)
            return result.Failure<SeedDocTypeResponse>()!;

        return new SeedDocTypeResponse(null, true);
    }

    public async Task<Result<GetDisciplineResponse?>> GetDiscipline(
        GetDisciplineRequest request, CT ct)
    {
        _logger.LogInformation("GetDiscipline");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetDisciplineResponse>(GlobalErrors.InvalidCompany);

        var result = await GetDisciplineCommand(request, ct);
        if (result.IsBad()) return result.Failure<GetDisciplineResponse>()!;

        await result!.Value!.Data.SetFullName(_mediator, ct);

        return result;
    }

    public async Task<Result<GetDisciplineDocResponse?>> GetDisciplineDoc(
        GetDisciplineDocRequest request, CT ct)
    {
        _logger.LogInformation("GetDisciplineDoc");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetDisciplineDocResponse>(GlobalErrors.InvalidCompany);

        var result = await GetDisciplineDocCommand(request, ct);
        if (result.IsBad()) return result.Failure<GetDisciplineDocResponse>()!;
        await result!.Value!.Data.SetFullName(_mediator, ct);


        return result;
    }

    public async Task<Result<GetDisciplineDocTypeResponse?>> GetDisciplineDocType(
        GetDisciplineDocTypeRequest request, CT ct)
    {
        _logger.LogInformation("GetDisciplineDocType");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetDisciplineDocTypeResponse>(GlobalErrors.InvalidCompany);

        var result = await GetDisciplineDocTypeCommand(request, ct);
        if (result.IsBad()) return result.Failure<GetDisciplineDocTypeResponse>()!;

        await result!.Value!.Data.SetFullName(_mediator, ct);

        return result;
    }

    public async Task<Result<CreateProjectDocResponse?>> CreateProjectDoc(
        CreateProjectDocRequest request, CT ct)
    {
        _logger.LogInformation("CreateProjectDoc");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateProjectDocResponse>(GlobalErrors.InvalidCompany);

        var IsDisciplineValid = await _disciplineRepository.IsDisciplineValid(request.DisciplineId, ct);
        var IsDisciplineDocValid = await _disciplineDocRepository.IsDisciplineDocValid(request.DisciplineDocId, ct);

        var isAllowedDocForDiscipline = await _disciplineDocTypeRepository
            .IsDisciplineDocTypeValid(
                request.DisciplineId,
                request.DisciplineDocId,
                ct);
        if (!isAllowedDocForDiscipline)
            return Result.Failure<CreateProjectDocResponse>(GlobalErrors.InValidRequest);

        var result = await CreateProjectDocCommand(request, ct);
        if (result.IsBad())
            return result.Failure<CreateProjectDocResponse>()!;

        return new CreateProjectDocResponse(result.Value!.Id, true);
    }

    public async Task<Result<UpdateProjectDocResponse?>> UpdateProjectDoc(
        UpdateProjectDocRequest request, CT ct)
    {
        _logger.LogInformation("UpdateProjectDoc");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateProjectDocResponse>(GlobalErrors.InvalidCompany);

        var IsDisciplineValid = await _disciplineRepository.IsDisciplineValid(request.DisciplineId, ct);
        var IsDisciplineDocValid = await _disciplineDocRepository.IsDisciplineDocValid(request.DisciplineDocId, ct);

        var isAllowedDocForDiscipline = await _disciplineDocTypeRepository.IsDisciplineDocTypeValid(
            request.DisciplineId, request.DisciplineDocId, ct);
        if (!isAllowedDocForDiscipline)
            return Result.Failure<UpdateProjectDocResponse>(
                GlobalErrors.InValidRequest);

        var result = await UpdateProjectDocCommand(request, ct);
        if (result.IsBad())
            return result.Failure<UpdateProjectDocResponse>()!;

        return result;
    }

    public async Task<Result<ProjectDocCodeGeneratorResponse?>> ProjectDocCodeGenerator(
        ProjectDocCodeGeneratorRequest request, CT ct)
    {
        _logger.LogInformation("ProjectDocCodeGenerator");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<ProjectDocCodeGeneratorResponse>(GlobalErrors.InvalidCompany);

        var isAllowedDocForDiscipline = await _disciplineDocTypeRepository.IsDisciplineDocTypeValid(
            request.DisciplineId, request.DisciplineDocId, ct);
        if (!isAllowedDocForDiscipline)
            return Result.Failure<ProjectDocCodeGeneratorResponse>(
                GlobalErrors.InValidRequest);

        var result = await ProjectDocCodeGeneratorCommand(request, ct);
        if (result.IsBad())
            return result.Failure<ProjectDocCodeGeneratorResponse>()!;

        return result;
    }

    public async Task<Result<DeleteProjectDocResponse?>> DeleteProjectDoc(
        DeleteProjectDocRequest request, CT ct)
    {
        _logger.LogInformation("DeleteProjectDoc");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<DeleteProjectDocResponse>(
                GlobalErrors.InvalidCompany);

        var result = await DeleteProjectDocCommand(request, ct);
        if (result.IsBad())
            return result.Failure<DeleteProjectDocResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return result;
    }

    public async Task<Result<GetProjectDocByIdResponse?>> GetProjectDocById(
        GetProjectDocByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectDocById");

        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetProjectDocByIdResponse>(
                GlobalErrors.InvalidCompany);

        var result = await GetProjectDocByIdCommand(request, ct);
        if (result.IsBad())
            return result.Failure<GetProjectDocByIdResponse>()!;

        return result;
    }

    public async Task<Result<GetProjectDocsResponse?>> GetProjectDocs(
        GetProjectDocsRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectDocs");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetProjectDocsResponse>(
                GlobalErrors.InvalidCompany);

        var result = await GetProjectDocsCommand(request, ct);
        if (result.IsBad())
            return result.Failure<GetProjectDocsResponse>()!;

        return result!;
    }

    public async Task<Result<GetProjectDocHistoriesResponse>> GetProjectDocHistories(
    GetProjectDocHistoriesRequest request,
    CT ct)
    {
        _logger.LogInformation("GetProjectDocHistories");

        var companyId = CompanyValidator.GetCompanyId(_userInfoService)!.Value;

        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetProjectDocHistoriesResponse>(
                GlobalErrors.InvalidCompany)!;

        var result = await GetProjectDocHistoriesCommand(request, ct);

        if (result.IsBad())
            return result.Failure<GetProjectDocHistoriesResponse>()!;

        return result;
    }

}


