using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Organizations;
using Engineering.Application.Services.Categories.Queries.GetCategoryWithoutInclude;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithoutInclude;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Application.Services.ProjectOperations.Models.GetsStatus;
using Engineering.Application.Services.Projects.Command.AssignProjectsToCostCenter;
using Engineering.Application.Services.Projects.Commands.ActiveProject;
using Engineering.Application.Services.Projects.Commands.CreateProject;
using Engineering.Application.Services.Projects.Commands.CreateProjectCode;
using Engineering.Application.Services.Projects.Commands.Disable;
using Engineering.Application.Services.Projects.Commands.InactiveProject;
using Engineering.Application.Services.Projects.Commands.ProjectStatusChanger;
using Engineering.Application.Services.Projects.Commands.SetManagerToProjects;
using Engineering.Application.Services.Projects.Commands.StateChangerProjects;
using Engineering.Application.Services.Projects.Commands.UpdateProject;
using Engineering.Application.Services.Projects.Models.ActiveProject;
using Engineering.Application.Services.Projects.Models.AddAuthorizedThirdPartyToProject;
using Engineering.Application.Services.Projects.Models.AssignProjectsToCostCenter;
using Engineering.Application.Services.Projects.Models.CreateProject;
using Engineering.Application.Services.Projects.Models.CreateProjectCode;
using Engineering.Application.Services.Projects.Models.CreateProjectProduct;
using Engineering.Application.Services.Projects.Models.Delete;
using Engineering.Application.Services.Projects.Models.DeleteProjectProduct;
using Engineering.Application.Services.Projects.Models.DeleteProjectThirdParty;
using Engineering.Application.Services.Projects.Models.FindLastUnitOrg;
using Engineering.Application.Services.Projects.Models.GetActiveProjects;
using Engineering.Application.Services.Projects.Models.GetContractorProjects;
using Engineering.Application.Services.Projects.Models.GetProjectByCode;
using Engineering.Application.Services.Projects.Models.GetProjectById;
using Engineering.Application.Services.Projects.Models.GetProjectByName;
using Engineering.Application.Services.Projects.Models.GetProjectCategoryProductByProjectId;
using Engineering.Application.Services.Projects.Models.GetProjectForPdf;
using Engineering.Application.Services.Projects.Models.GetProjectHistory;
using Engineering.Application.Services.Projects.Models.GetProjectPOTimelines;
using Engineering.Application.Services.Projects.Models.GetProjectProductCategoryByCostCenterId;
using Engineering.Application.Services.Projects.Models.GetProjectProductCategoryByProjectId;
using Engineering.Application.Services.Projects.Models.GetProjectProductGroupByCostCenterId;
using Engineering.Application.Services.Projects.Models.GetProjectProductGroupByProjectId;
using Engineering.Application.Services.Projects.Models.GetProjectProgress;
using Engineering.Application.Services.Projects.Models.GetProjects;
using Engineering.Application.Services.Projects.Models.GetProjectsByCostCenter;
using Engineering.Application.Services.Projects.Models.GetProjectThirdParties;
using Engineering.Application.Services.Projects.Models.GetsActiveProjectByCostCenterIds;
using Engineering.Application.Services.Projects.Models.GetsByEmployerId;
using Engineering.Application.Services.Projects.Models.GetsByNameOrCode;
using Engineering.Application.Services.Projects.Models.GetsContractedProject;
using Engineering.Application.Services.Projects.Models.GetsProjectByIds;
using Engineering.Application.Services.Projects.Models.GetsProjectByProjectManagerId;
using Engineering.Application.Services.Projects.Models.GetsProjectExcelEnum;
using Engineering.Application.Services.Projects.Models.GetsProjectExcelExporter;
using Engineering.Application.Services.Projects.Models.GetsProjectSorting;
using Engineering.Application.Services.Projects.Models.GetSummarizedProjectById;
using Engineering.Application.Services.Projects.Models.GetUnAssignedProjects;
using Engineering.Application.Services.Projects.Models.GroupProjectStatusChanger;
using Engineering.Application.Services.Projects.Models.InactiveProject;
using Engineering.Application.Services.Projects.Models.ProjectGroupDelete;
using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Application.Services.Projects.Models.ProjectStatusChanger;
using Engineering.Application.Services.Projects.Models.ProjectWarehouseModels;
using Engineering.Application.Services.Projects.Models.SetManagerToProjects;
using Engineering.Application.Services.Projects.Models.StateChangerProjects;
using Engineering.Application.Services.Projects.Models.UpdateProject;
using Engineering.Application.Services.Projects.Models.UpdateProjectProduct;
using Engineering.Application.Services.Projects.Models.UpdateProjectProductQuantities;
using Engineering.Application.Services.Projects.Queries.FindLastUnitOrg;
using Engineering.Application.Services.Projects.Queries.GetActiveProjects;
using Engineering.Application.Services.Projects.Queries.GetContractorProjects;
using Engineering.Application.Services.Projects.Queries.GetLastProjectCode;
using Engineering.Application.Services.Projects.Queries.GetProjectByCode;
using Engineering.Application.Services.Projects.Queries.GetProjectById;
using Engineering.Application.Services.Projects.Queries.GetProjectByName;
using Engineering.Application.Services.Projects.Queries.GetProjectForPdf;
using Engineering.Application.Services.Projects.Queries.GetProjectPOTimelines;
using Engineering.Application.Services.Projects.Queries.GetProjectProgress;
using Engineering.Application.Services.Projects.Queries.GetProjects;
using Engineering.Application.Services.Projects.Queries.GetProjectsByCostCenter;
using Engineering.Application.Services.Projects.Queries.GetsActiveProjectByCostCenterIds;
using Engineering.Application.Services.Projects.Queries.GetsByEmployerId;
using Engineering.Application.Services.Projects.Queries.GetsByNameOrCode;
using Engineering.Application.Services.Projects.Queries.GetsContractedProject;
using Engineering.Application.Services.Projects.Queries.GetsProjectByIds;
using Engineering.Application.Services.Projects.Queries.GetsProjectByProjectManagerId;
using Engineering.Application.Services.Projects.Queries.GetsProjectSorting;
using Engineering.Application.Services.Projects.Queries.GetSummarizedProjectById;
using Engineering.Application.Services.Projects.Queries.GetUnAssignedProjects;
using Engineering.Application.Services.ProjectTypes.Queries.GetProjectTypeById;
using Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetProvinceById;
using Engineering.Application.WebServices.MetaDataServices.Employers.Models;
using Engineering.Application.WebServices.MetaDataServices.Employers.Queries.GetEmployerById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Application.WebServices.WarehouseServices.WarehouseAssetes.Queries.GetUnUsedWarehousesCategories;
using Engineering.Application.WebServices.WarehouseServices.WarehouseAssetes.Queries.GetUnUsedWarehousesGroups;
using Engineering.Domain.Entities.Categories;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Synonyms.MetaData.Organizations;
using Engineering.Domain.Errors.EngineeringConfigs;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.PreferentialTemporary.Commands.CreatePreferentialTemporary;
using Gita.Backend.Shared.Domain.Constants;
using Gita.Backend.Shared.Persistence.Extensions;
using Warehouse.ClientSdks.Services;
using Warehouse.ClientSdks.Services.WarehouseAssets;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.Projects;

partial class ProjectLogic : IProjectLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IProjectHistoryRepository _projectHistoryRepository;
    private readonly IProjectProductRepository _ppRepo;
    private readonly ICostCenterWarehouseRepository _ccWarehouseRepo;
    private readonly IProjectWarehouseRepository _projectWarehouseRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectThirdPartyRepository _projectThirdPartyRepository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly IRequestGoodsSupplyRepository _requestGoodsSupplyRepo;
    private readonly IViewCityRepository _viewCityRepository;
    private readonly IViewThirdPartyRepository _viewThirdPartyRepository;
    private readonly IViewOrganizationRepository _viewOrganizationRepository;

    public ProjectLogic(IMediator mediator,
        ILogger<ProjectLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        IProjectHistoryRepository projectHistoryRepository,
        IViewThirdPartyRepository thirdPartyRepo,
        IProjectThirdPartyRepository projectThirdPartyRepository,
        IProjectRepository projectRepository,
        IProjectProductRepository projectProductRepository,
        ICostCenterWarehouseRepository costCenterWarehouseRepository,
        IProjectWarehouseRepository projectWarehouseRepository,
        ICategoryService categoryService,
        IWarehouseAssetService warehouseAssetService,
        IRequestGoodsSupplyRepository requestGoodsSupplyRepo,
        IViewCityRepository viewCityRepository,
        IViewThirdPartyRepository viewThirdPartyRepository,
        IViewOrganizationRepository viewOrganizationRepository
        )
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _thirdPartyRepo = thirdPartyRepo;
        _projectThirdPartyRepository = projectThirdPartyRepository;
        _projectHistoryRepository = projectHistoryRepository;
        _ppRepo = projectProductRepository;
        _ccWarehouseRepo = costCenterWarehouseRepository;
        _projectWarehouseRepository = projectWarehouseRepository;
        _projectRepository = projectRepository;
        //_warehouseAssetService = warehouseAssetService;
        _requestGoodsSupplyRepo = requestGoodsSupplyRepo;
        _viewCityRepository = viewCityRepository;
        _viewThirdPartyRepository = viewThirdPartyRepository;
        _viewOrganizationRepository = viewOrganizationRepository;
    }

    public async Task<Result<CreateProjectResponse?>> CreateProject(
        CreateProjectRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProject, ProjectName:{ProjectName},", request.ProjectName);

        CostCenter? costCenter = null;

        if (request.CostCenterId is not null && request.CostCenterId > 0)
        {
            var getCostCenter = await _mediator.Send(
                new GetCostCenterWithoutIncludeQuery(request.CostCenterId.Value),
                ct);

            if (getCostCenter.IsFailure)
                return Result.Failure<CreateProjectResponse>(
                    CostCenterErrors.CostCenterWithIdNotFound);

            costCenter = getCostCenter.Value;
        }

        List<CostCenter>? costCentersList = [];

        if (request.CostCenterIds.HasAny())
            foreach (var costCenterId in request.CostCenterIds!)
                if (costCenterId > 0)
                {
                    var getCostCenter = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(
                        costCenterId), ct);
                    if (getCostCenter.IsFailure)
                        return Result.Failure<CreateProjectResponse>(CostCenterErrors.CostCenterWithIdNotFound);

                    costCentersList.Add(getCostCenter.Value);
                }

        var isValidRequest = await request.IsValidAsync<CreateProjectValidator, CreateProjectRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateProjectResponse>(isValidRequest.Error!);

        List<ProjectWarehouseRequest>? projectWarehouses = null;
        if (request.ProjectWarehouses is not null)
        {
            var warehouseSelection = await ValidateProjectWarehouseSelection(
                request.ProjectWarehouses,
                ct);
            if (warehouseSelection.IsBad())
                return warehouseSelection.Failure<CreateProjectResponse>()!;

            projectWarehouses = warehouseSelection.Value;
        }

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateProjectResponse>(companyResponse.Error!);
        }

        var nameIsDuplicate = await _mediator.Send(new GetProjectByNameQuery(request.ProjectName, request.CostCenterId, companyId), ct);
        if (nameIsDuplicate is { IsSuccess: true, Value: not null, })
            return Result.Failure<CreateProjectResponse>(ProjectErrors.NameIsDuplicate);

        List<Category>? categories = [];
        if (request.CategoryIds.HasAny())
            foreach (var item in request.CategoryIds!)
                if (item > 0)
                {
                    var getProjectCategory = await _mediator.Send(new GetCategoryWithoutIncludeQuery(item), ct);
                    if (getProjectCategory.IsBad()) return Result.Failure<CreateProjectResponse>(CategoryErrors.CategoryWithIdNotFound);
                    categories.Add(getProjectCategory.Value!);
                }

        ProjectType? type = null;
        if (request.IsOrganizationUnit == false)
        {
            if (request.ProjectTypeId is null)
                return Result.Failure<CreateProjectResponse>(ProjectErrors.ProjectTypeIdIsEmpty);
            var getProjectType = await _mediator.Send(new GetProjectTypeByIdQuery(request.ProjectTypeId.Value), ct);
            if (getProjectType.IsBad())
                return Result.Failure<CreateProjectResponse>(ProjectErrors.ProjectTypeWithIdNotFound);
            type = getProjectType.Value;
        }

        Employer? employer = null;
        if (request.EmployerId is not null)
        {
            var employerData = await _mediator.Send(new GetEmployerByIdQuery(request.EmployerId.Value), ct);
            if (employerData.IsFailure)
                return Result.Failure<CreateProjectResponse>(MetaDataErrors.EmployerWithIdNotFound);
            if (employerData.Value is null)
                return Result.Failure<CreateProjectResponse>(MetaDataErrors.EmployerValueIsNull);
            if (employerData.Value.UniqueCode is null)
                return Result.Failure<CreateProjectResponse>(MetaDataErrors.EmployerUniqueCodeIsNull);
            employer = employerData.Value;
        }

        string projectCode;

        if (!string.IsNullOrEmpty(request.ProjectCode))
        {
            // manual code: company-wide check, no cost center
            var codeIsDuplicate = await _mediator.Send(
                new GetProjectByCodeQuery(request.ProjectCode, null, companyId), ct);
            if (codeIsDuplicate is { IsSuccess: true, Value: not null })
                return Result.Failure<CreateProjectResponse>(ProjectErrors.CodeIsDuplicate);

            projectCode = request.ProjectCode;
        }
        else if (request.IsOrganizationUnit == true)
        {
            var lastProjectCode = await _mediator.Send(new FindLastUnitOrgQuery(request.OrganizationId!.Value), ct);
            projectCode = $"{request.OrganizationId}-{lastProjectCode.Value}";
        }
        else
        {
            if (employer is null)
                return Result.Failure<CreateProjectResponse>(MetaDataErrors.EmployerWithIdNotFound);

            var generated = await GenerateUniqueProjectCode(
                employer.UniqueCode!, costCenter?.CostCenterCode, companyId, ct);
            if (generated.IsFailure)
                return Result.Failure<CreateProjectResponse>(generated.Error!);

            projectCode = generated.Value!;
        }

        var contractual = request.Contractual is null ? true : request.Contractual.Value;
        var createProjectResponse = await _mediator.Send(new CreateProjectCommand(
            type,
            request.ProjectName,
            request.ProjectEnName,
            request.EmployerId,
            categories,
            costCenter,
            costCentersList,
            request.SupervisorEngineerId,
            request.AdvisorId,
            request.ProjectManagerId,
            request.PlanningAssistantId,
            request.ImplementationAssistants!,
            request.TechnicalAssistants!,
            request.Status,
            contractual,
            request.CollectiveService,
            request.IsActive,
            projectCode,
            request.Prefix,
            request.ApprovedBudget,
            request.CityId,
            request.Description,
            request.DescriptionEn,
            request.AddressDescription,
            companyId,
            request.HasProduct,
            request.OrganizationId,
            request.IsOrganizationUnit,
            projectWarehouses), ct);
        if (createProjectResponse.IsFailure)
            return Result.Failure<CreateProjectResponse>(createProjectResponse.Error!);

        await _unitOfWork.CommitAsync(ct);

        try
        {
            var createPreferentialTemporary = new CreatePreferentialTemporaryCommand(createProjectResponse.Value!.GetPreferentialName(),
                DepartmentNames.Engineering.Project, createProjectResponse.Value!.PreferentialReferenceCode, true, null, createProjectResponse.Value.ProjectCostCenters.FirstOrDefault()?.CostCenter.PreferentialReferenceCode);

            var createPreferentialTemporaryResponse = await _mediator.Send(createPreferentialTemporary, ct);
            if (createPreferentialTemporaryResponse.IsFailure)
                _logger.LogError("CreatePreferentialTemporary for Project with id of {ProjectId} failed with Code:{Code}, Msg:{Msg}", createProjectResponse.Value!.Id, createPreferentialTemporaryResponse.Error!.Code, createPreferentialTemporaryResponse.Error!.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return new CreateProjectResponse(createProjectResponse.Value!.Id, true);
    }

    public async Task<Result<CreateProjectThirdPartyResponse?>> CreateProjectThirdParty(
        CreateProjectThirdPartyRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectThirdParties, ProjectId:{Id},", request.ProjectId);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateProjectThirdPartyResponse>(companyResponse.Error!);
        }

        var engConfig = await _mediator.Send(new GetActiveConfigQuery());
        if (engConfig.IsBad() || !engConfig.Value!.ProjectThirdParties)
            return Result.Failure<CreateProjectThirdPartyResponse>(EngineeringConfigErrors.ProjectThirdPartyIsFalse);

        var project = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId), ct);
        if (project.IsBad())
            return project.Failure<CreateProjectThirdPartyResponse>()!;

        var projectHasThirdParty = await _projectThirdPartyRepository.HasThirdParty(request.ProjectId, ct);
        if (projectHasThirdParty)
        {
            var thirdParty = await _thirdPartyRepo.GetById(request.CreatorId, ct);
            if (thirdParty is null)
                return Result.Failure<CreateProjectThirdPartyResponse>(ProjectErrors.ThirdPartyNotFound);
            var pThirdParty = await _projectThirdPartyRepository.GetByProjectAndThirdPartyId(request.ProjectId, thirdParty.Id, ct);
            if (project.Value!.CreatorId != thirdParty!.UserId && pThirdParty is null)
                return Result.Failure<CreateProjectThirdPartyResponse>(ProjectErrors.OnlyCreatorCanAssignUsers);
        }

        var create = await CreateProjectThirdPartiesCommand(request, project.Value!, ct);
        if (create.IsBad())
            return create.Failure<CreateProjectThirdPartyResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new CreateProjectThirdPartyResponse(true);
    }

    public async Task<Result<CreateProjectCodeResponse?>> CreateProjectCode(
        CreateProjectCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectCode, CostCenterId:{CostCenterId},", request.CostCenterId);

        var isValidRequest = await request.IsValidAsync<CreateProjectCodeValidator, CreateProjectCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateProjectCodeResponse>(isValidRequest.Error!);

        CostCenter? costCenter = null;
        if (request.CostCenterId is not null && request.CostCenterId > 0)
        {
            var getCostCenter = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(request.CostCenterId!.Value), ct);
            if (getCostCenter.IsFailure)
                return Result.Failure<CreateProjectCodeResponse>(CostCenterErrors.CostCenterWithIdNotFound);
            costCenter = getCostCenter.Value;
        }

        //Validate Employer
        var employerData = await _mediator.Send(new GetEmployerByIdQuery(request.EmployerId), ct);
        if (employerData.IsFailure)
            return Result.Failure<CreateProjectCodeResponse>(MetaDataErrors.EmployerWithIdNotFound);
        if (employerData.Value is null)
            return Result.Failure<CreateProjectCodeResponse>(MetaDataErrors.EmployerValueIsNull);
        if (employerData.Value.UniqueCode is null)
            return Result.Failure<CreateProjectCodeResponse>(MetaDataErrors.EmployerUniqueCodeIsNull);

        var result = await GenerateUniqueProjectCode(
            employerData.Value.UniqueCode, costCenter?.CostCenterCode, _userInfoService.UserCompanyId, ct);
        if (result.IsFailure)
            return Result.Failure<CreateProjectCodeResponse>(result.Error!);

        return new CreateProjectCodeResponse(result.Value!);
    }

    public async Task<Result<FindLastUnitOrgResponse?>> FindLastUnitOrg(
        FindLastUnitOrgRequest request, CT ct)
    {
        var code = await _mediator.Send(new FindLastUnitOrgQuery(request.OrganizationId), ct);
        return new FindLastUnitOrgResponse((request.OrganizationId + "-" + code.Value).ToString());
    }

    public async Task<Result<UpdateProjectResponse?>> UpdateProject(
        UpdateProjectRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProject, id:{Id}, Project:{ProjectName} ,", request.Id, request.ProjectName);
        //Validate data
        var isValidRequest = await request.IsValidAsync<UpdateProjectValidator, UpdateProjectRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectResponse>(isValidRequest.Error!);

        // UPDATED: Check both legacy ID and new list for the City fallback rule
        if (request.CostCenterId is null && (request.CostCenterIds?.Count ?? 0) == 0 && request.CityId is null)
            return Result.Failure<UpdateProjectResponse?>(ProjectErrors.CityIsNull);

        var engConfig = await _mediator.Send(new GetActiveConfigQuery());
        var projectHasThirdParty = await _projectThirdPartyRepository.HasThirdParty(request.Id, ct);

        if (request.ThirdPartyId is not null)
            if (!engConfig.IsBad() && engConfig.Value!.ProjectThirdParties && projectHasThirdParty)
            {
                var pThirdParty = await _projectThirdPartyRepository.GetByProjectAndThirdPartyId(request.Id, request.ThirdPartyId.Value, ct);
                if (pThirdParty is null)
                    return Result.Failure<UpdateProjectResponse>(ProjectErrors.DontHaveAccessToProject);
            }

        var getProject = await _mediator.Send(new GetProjectByIdQuery(request.Id), ct);
        if (getProject.IsFailure)
            return Result.Failure<UpdateProjectResponse>(ProjectErrors.ProjectWithIdNotFound);
        var project = getProject.Value!;
        if (project.Status != ProjectStatus.NotStarted && request.Status == ProjectStatus.NotStarted)
            return Result.Failure<UpdateProjectResponse>(ProjectErrors.ProjectCanNotEdit);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<UpdateProjectResponse>(companyResponse.Error!);
        }

        // UPDATED: Validate Legacy CostCenter (with duplicate prevention)
        if (request.CostCenterId is not null && request.CostCenterId > 0)
        {
            if (!project.ProjectCostCenters.Any(x => x.CostCenterId == request.CostCenterId.Value))
            {
                var getCostCenter = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(request.CostCenterId.Value), ct);
                if (getCostCenter.IsFailure)
                    return Result.Failure<UpdateProjectResponse>(getCostCenter.Error!);
                project.SetCostCenter(getCostCenter.Value!);
            }
        }

        // ADDED: Validate New CostCenters List
        if (request.CostCenterIds.HasAny())
        {
            foreach (var costCenterId in request.CostCenterIds!)
            {
                if (costCenterId > 0 && !project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId))
                {
                    var getCostCenter = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(costCenterId), ct);
                    if (getCostCenter.IsFailure)
                        return Result.Failure<UpdateProjectResponse>(getCostCenter.Error!);
                    project.SetCostCenter(getCostCenter.Value!);
                }
            }
        }

        //Validate name
        if (request.ProjectName != project.ProjectName)
        {
            var nameIsDuplicate = await _mediator.Send(new GetProjectByNameQuery(request.ProjectName, request.CostCenterId, companyId), ct);
            if (nameIsDuplicate is { IsSuccess: true, Value: not null, } && nameIsDuplicate.Value.Id != request.Id)
                return Result.Failure<UpdateProjectResponse>(ProjectErrors.NameIsDuplicate);
        }

        //Validate Category
        if (request.DeleteCategoryIds != null && request.DeleteCategoryIds.Count > 0)
        {
            foreach (var item in request.DeleteCategoryIds)
            {
                var delete = project.ProjectCategories.FirstOrDefault(x => x.CategoryId == item);
                if (delete is not null)
                    delete.SoftDelete();
            }
        }

        if (request.CategoryIds != null && request.CategoryIds.Count > 0)
        {
            List<Category> cats = [];
            foreach (var item in request.CategoryIds)
            {
                if (!project.ProjectCategories.Any(x => x.CategoryId == item))
                {
                    if (item > 0)
                    {
                        var getCategory = await _mediator.Send(new GetCategoryWithoutIncludeQuery(item), ct);
                        if (getCategory.IsBad()) return getCategory.Failure<UpdateProjectResponse>()!;
                        cats.Add(getCategory.Value!);
                    }
                }
            }
            if (cats != null && cats.Count > 0)
                project.SetCategory(cats);
        }

        //Validate Type
        if (request.ProjectTypeId != null && request.ProjectTypeId != project.ProjectType?.Id)
        {
            var getProjectType = await _mediator.Send(new GetProjectTypeByIdQuery(request.ProjectTypeId.Value), ct);
            if (getProjectType.IsFailure)
                return Result.Failure<UpdateProjectResponse>(getProjectType.Error!);
            if (request.IsOrganizationUnit == true)
                return Result.Failure<UpdateProjectResponse>(ProjectErrors.UnitOrgCantHaveType);
            project.SetProjectType(getProjectType.Value);
        }

        //Validate Employer
        string newCode = "";
        if (!project.AddAutomated)
        {
            if (request.EmployerId is not null)
            {
                if (request.EmployerId != project.EmployerId)
                {
                    if (project.ContractorContracts is null || project.ContractorContracts?.Count <= 0 || project.EmployerId.Equals(1))
                    {
                        var employerData = await _mediator.Send(new GetEmployerByIdQuery(request.EmployerId.Value), ct);
                        if (employerData.IsFailure)
                            return Result.Failure<UpdateProjectResponse>(MetaDataErrors.EmployerWithIdNotFound);
                        if (employerData.Value is null)
                            return Result.Failure<UpdateProjectResponse>(MetaDataErrors.EmployerValueIsNull);
                        if (employerData.Value.UniqueCode is null)
                            return Result.Failure<UpdateProjectResponse>(MetaDataErrors.EmployerUniqueCodeIsNull);

                        if (string.IsNullOrEmpty(request.ProjectCode))
                        {
                            var generated = await GenerateUniqueProjectCode(
                                employerData.Value.UniqueCode,
                                project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterCode,
                                companyId,
                                ct);
                            if (generated.IsFailure)
                                return Result.Failure<UpdateProjectResponse>(generated.Error!);

                            newCode = generated.Value!;
                        }
                        else
                            newCode = request.ProjectCode;
                    }
                    else
                        return Result.Failure<UpdateProjectResponse>(ProjectErrors.ProjectHaveContracts);
                }
            }
        }

        if (!string.IsNullOrEmpty(request.ProjectCode) && request.ProjectCode != project.ProjectCode)
        {
            var codeIsDuplicate = await _mediator.Send(new GetProjectByCodeQuery(request.ProjectCode, null, companyId), ct);
            if (codeIsDuplicate is { IsSuccess: true, Value: not null, } && codeIsDuplicate.Value.Id != request.Id)
                return Result.Failure<UpdateProjectResponse>(ProjectErrors.CodeIsDuplicate);
            newCode = request.ProjectCode;
        }

        // Validate Cost Center Deletions 
        if (request.DeleteCostCenterIds.HasAny())
        {
            foreach (var item in request.DeleteCostCenterIds!)
            {
                var delete = project.ProjectCostCenters.FirstOrDefault(x => x.CostCenterId == item);
                if (delete is not null)
                {
                    // Dependency Check
                    var hasDependencies = await CheckCostCenterDependencies(project.Id, item, ct);
                    if (hasDependencies)
                    {
                        return Result.Failure<UpdateProjectResponse>(ProjectErrors.CostCenterHasDependencies);
                    }

                    // Soft Delete the relation
                    delete.SoftDelete();
                }
            }
        }

        var contractual = request.Contractual is null ? true : request.Contractual.Value;

        if (request.ProjectWarehouses is not null)
        {
            var synchronizeProjectWarehouses = await SynchronizeProjectWarehouses(
                project,
                request.ProjectWarehouses,
                ct);
            if (synchronizeProjectWarehouses.IsBad())
                return synchronizeProjectWarehouses.Failure<UpdateProjectResponse>()!;
        }

        var updateProjectResponse = await _mediator.Send(new UpdateProjectCommand(
            project,
            newCode,
            request.Prefix,
            request.ProjectName,
            request.ProjectEnName,
            request.EmployerId,
            request.AdvisorId,
            request.SupervisorEngineerId,
            request.ProjectManagerId,
            request.PlanningAssistantId,
            request.Status,
            contractual,
            request.CollectiveService,
            request.IsActive,
            request.ImplementationAssistants,
            request.TechnicalAssistants,
            request.ApprovedBudget,
            request.CityId,
            request.Description,
            request.DescriptionEn,
            request.AddressDescription,
            companyId,
            request.HasProduct,
            request.OrganizationId,
            request.IsOrganizationUnit), ct);

        if (updateProjectResponse.IsBad())
            return Result.Failure<UpdateProjectResponse>(updateProjectResponse.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectResponse(updateProjectResponse.Value!.Id, true);
    }

    public async Task<Result<UpdateProjectProductQuantitiesResponse?>> UpdateProjectProductQuantity(
       UpdateProjectProductQuantitiesRequest request, CT ct)
    {
        var result = await UpdateProjectProductQuantityCommand(request, ct);
        if (result.IsBad())
            return result.Failure<UpdateProjectProductQuantitiesResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectProductQuantitiesResponse(true);
    }

    public async Task<Result<CreateProjectProductResponse?>> CreateProjectProduct(
        CreateProjectProductRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectProduct");

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateProjectProductResponse>(GlobalErrors.InvalidCompany);

        var projectProduct = await CreateProjectProductHandler(request, companyId, ct);
        if (projectProduct.IsBad())
            return projectProduct.Failure<CreateProjectProductResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new CreateProjectProductResponse(true);
    }

    public async Task<Result<UpdateProjectProductResponse?>> UpdateProjectProduct(
        UpdateProjectProductRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectProduct");

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateProjectProductResponse>(GlobalErrors.InvalidCompany);

        var updateProjectProduct = await UpdateProjectProductHandler(request, companyId, ct);
        if (updateProjectProduct.IsBad())
            return updateProjectProduct.Failure<UpdateProjectProductResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectProductResponse(true);
    }

    public async Task<Result<DeleteProjectProductResponse?>> DeleteProjectProduct(
        DeleteProjectProductRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteProjectProduct");

        var projectProduct = await DeleteProjectProductCommand(request, ct);
        if (projectProduct.IsBad())
            return projectProduct.Failure<DeleteProjectProductResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectProductResponse(true);
    }

    public async Task<Result<DeleteProjectThirdPartyResponse?>> DeleteProjectThirdParty(
        DeleteProjectThirdPartyRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteProjectThirdParty");

        var engConfig = await _mediator.Send(new GetActiveConfigQuery());
        if (engConfig.IsBad() || !engConfig.Value!.ProjectThirdParties)
            return Result.Failure<DeleteProjectThirdPartyResponse>(EngineeringConfigErrors.ProjectThirdPartyIsFalse);

        var projectProduct = await DeleteProjectThirdPartyCommand(request, ct);
        if (projectProduct.IsBad())
            return projectProduct.Failure<DeleteProjectThirdPartyResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectThirdPartyResponse(true);
    }

    public async Task<Result<GetFltrProjectThirdPartyResponse?>> GetFltrProjectThirdParty(
        GetFltrProjectThirdPartyRequest request, CT ct)
    {
        _logger.LogInformation("GetFltrProjectThirdParty");
        var result = await GetFltrProjectThirdPartyQuery(request, ct);
        if (result.IsBad())
            return result.Failure<GetFltrProjectThirdPartyResponse>()!;

        return result;
    }

    public async Task<Result<GetProjectProductByProjectIdResponse?>> GetProjectProductByProjectId(
        GetProjectProductByProjectIdRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectProductByProjectId");
        var result = await _ppRepo.GetPPByProjectId(request.Id, ProjectProductType.ProductGroup, ct);
        if (result is null || result.Count == 0)
            return Result.Failure<GetProjectProductByProjectIdResponse>(ProjectErrors.GroupNotFound);

        var ids = result!.Listed(x => x.ProductGroupId);
        if (ids.Any(x => x > 0))
        {
            var responseValue = await WebServicesLogic.GetFilteredGroupsDataReceiver(ids, null, _mediator, ct);
            result.ForEach(item =>
            {
                var group = responseValue?.FirstOrDefault(x => x.Id == item.ProductGroupId);
                if (group is not null)
                {
                    item.ProductGroupName = group.Name;
                    item.ProductGroupCode = group.Code;
                }
            });
            return new GetProjectProductByProjectIdResponse(result.Adapt<List<GetProjectProductByProjectIdModel>>(), result.Count);
        }
        return new GetProjectProductByProjectIdResponse(new List<GetProjectProductByProjectIdModel>(), 0);
    }

    public async Task<Result<GetProjectCategoryProductByProjectIdResponse?>> GetProjectCategoryProductByProjectId(
        GetProjectCategoryProductByProjectIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectCategoryProductByProjectId");

        var result = await _ppRepo.GetPPByProjectId(
            request.Id,
            ProjectProductType.Category,
            ct);
        if (result is null || result.Count == 0)
            return Result.Failure<GetProjectCategoryProductByProjectIdResponse>(
                ProjectErrors.GroupNotFound);
        var ids = result.Select(x => (long)x.ProductGroupId).ToList();
        var responseValue = await WebServicesLogic.CategoriesDataReceiver(ids, null, _mediator, ct);
        var data = result.Select(item =>
        {
            var category = responseValue?.FirstOrDefault(x => x.Id == item.ProductGroupId);

            return new GetProjectCategoryProductByProjectIdModel
            {
                Id = item.Id,
                ProductCategoryId = item.ProductGroupId,
                CategoryTitle = category?.Title,
                CategoryCode = category?.Code,
                RequestQuantity = item.RequestQuantity ?? 0,
                RemainingQuantity = item.RemainingQuantity ?? 0,
                InProgressQuantity = item.InProgressQuantity ?? 0,
                CompletedQuantity = item.CompletedQuantity ?? 0,
                TolerancePercentage = item.TolerancePercentage ?? 0,
                IsActive = item.IsActive ?? false,
                DefaultManagerSet = item.DefaultManagerSet,
                ProductType = ProjectProductType.Category
            };
        }).ToList();
        return new GetProjectCategoryProductByProjectIdResponse(data, data.Count);
    }

    public async Task<Result<GetProjectProductGroupByCostCenterIdResponse?>> GetProjectProductGroupByCostCenterId(
        GetProjectProductGroupByCostCenterIdRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectProductGroupByCostCenterId");
        var responseWar = await _ccWarehouseRepo.GetWarehouseIds(request.CostCenterId!.Value, ct);
        if (responseWar == null || responseWar.Count == 0)
            return Result.Failure<GetProjectProductGroupByCostCenterIdResponse>(ProjectErrors.WarehouseNotFound);
        List<long> warehouseIds = responseWar.Listed(x => x);

        var usedGroupIds = await _ppRepo.GetUsedPPGroupByProjectId(request.ProjectId, ct);

        var result = await _mediator.Send(new GetUnUsedWarehousesGroupsQuery(
            warehouseIds, usedGroupIds, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (result.Value == null || result.Value.RowCount == 0)
            return Result.Failure<GetProjectProductGroupByCostCenterIdResponse>(ProjectErrors.CategoryNotFound);
        return new GetProjectProductGroupByCostCenterIdResponse(
            result.Value.Data.Adapt<List<GetProjectProductModel>>(), result?.Value?.Data?.Count ?? 0);
    }

    public async Task<Result<GetProjectProductCategoryByCostCenterIdResponse?>> GetProjectProductCategoryByCostCenterId(
        GetProjectProductCategoryByCostCenterIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectProductCategoryByCostCenterId");
        var warehouseIds = await _ccWarehouseRepo.GetWarehouseIds(request.CostCenterId!.Value, ct);
        if (warehouseIds == null || warehouseIds.Count == 0)
            return Result.Failure<GetProjectProductCategoryByCostCenterIdResponse>(ProjectErrors.WarehouseNotFound);

        var usedCategoryIds = await _ppRepo.GetUsedPPCategoryByProjectId(request.ProjectId, ct);
        var result = await _mediator.Send(new GetUnUsedWarehousesCategoriesQuery(
            warehouseIds, usedCategoryIds, request.FilterData, request.PageIndex, request.PageSize), ct);
        if (result.Value == null || result.Value.RowCount == 0)
            return Result.Failure<GetProjectProductCategoryByCostCenterIdResponse>(ProjectErrors.CategoryNotFound);

        return new GetProjectProductCategoryByCostCenterIdResponse(
            result.Value.Data.Adapt<List<GetProjectProductCategoryByCostCenterIdModel>>(), result.Value.RowCount);
    }

    public async Task<Result<GetProjectProductGroupByProjectIdResponse?>> GetProjectProductGroupByProjectId(
        GetProjectProductGroupByProjectIdRequest request,
        CT ct)
    {
        _logger.LogInformation("GetProjectProductGroupByProjectId");

        var validation = await request.IsValidAsync<GetProjectProductGroupByProjectIdValidator, GetProjectProductGroupByProjectIdRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<GetProjectProductGroupByProjectIdResponse>(validation.Error!);

        if (!await _projectRepository.ExistsProject(request.ProjectId, ct))
            return Result.Failure<GetProjectProductGroupByProjectIdResponse>(ProjectWarehouseErrors.ProjectNotFound);

        var warehouseIds = await _projectWarehouseRepository.GetWarehouseIds(request.ProjectId, ct);
        if (!warehouseIds.HasAny())
            return Result.Failure<GetProjectProductGroupByProjectIdResponse>(ProjectWarehouseErrors.DataNotFound);

        var usedGroupIds = await _ppRepo.GetUsedPPGroupByProjectId(request.ProjectId, ct);
        var result = await _mediator.Send(new GetUnUsedWarehousesGroupsQuery(
            warehouseIds,
            usedGroupIds,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad() || result.Value?.Data is null || result.Value.RowCount == 0)
            return Result.Failure<GetProjectProductGroupByProjectIdResponse>(ProjectErrors.GroupNotFound);

        return new GetProjectProductGroupByProjectIdResponse(
            result.Value.Data.Adapt<List<GetProjectProductModel>>(),
            result.Value.RowCount);
    }

    public async Task<Result<GetProjectProductCategoryByProjectIdResponse?>> GetProjectProductCategoryByProjectId(
        GetProjectProductCategoryByProjectIdRequest request,
        CT ct)
    {
        _logger.LogInformation("GetProjectProductCategoryByProjectId");

        var validation = await request.IsValidAsync<GetProjectProductCategoryByProjectIdValidator, GetProjectProductCategoryByProjectIdRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<GetProjectProductCategoryByProjectIdResponse>(validation.Error!);

        if (!await _projectRepository.ExistsProject(request.ProjectId, ct))
            return Result.Failure<GetProjectProductCategoryByProjectIdResponse>(ProjectWarehouseErrors.ProjectNotFound);

        var warehouseIds = await _projectWarehouseRepository.GetWarehouseIds(request.ProjectId, ct);
        if (!warehouseIds.HasAny())
            return Result.Failure<GetProjectProductCategoryByProjectIdResponse>(ProjectWarehouseErrors.DataNotFound);

        var usedCategoryIds = await _ppRepo.GetUsedPPCategoryByProjectId(request.ProjectId, ct);
        var result = await _mediator.Send(new GetUnUsedWarehousesCategoriesQuery(
            warehouseIds,
            usedCategoryIds,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad() || result.Value?.Data is null || result.Value.RowCount == 0)
            return Result.Failure<GetProjectProductCategoryByProjectIdResponse>(ProjectErrors.CategoryNotFound);

        return new GetProjectProductCategoryByProjectIdResponse(
            result.Value.Data.Adapt<List<GetProjectProductCategoryByCostCenterIdModel>>(),
            result.Value.RowCount);
    }

    public async Task<Result<InactiveProjectResponse?>> InactiveProject(
        InactiveProjectRequest request, CT ct)
    {
        _logger.LogInformation("Request for InactiveProject, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<InactiveProjectValidator, InactiveProjectRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InactiveProjectResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new InactiveProjectCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<InactiveProjectResponse>(response.Error!);

        var inactivedProject = response.Value!;
        await _unitOfWork.CommitAsync(ct);

        return new InactiveProjectResponse(inactivedProject.Id, inactivedProject.IsActive);
    }

    public async Task<Result<ActiveProjectResponse?>> ActiveProject(
        ActiveProjectRequest request, CT ct)
    {
        _logger.LogInformation("Request for  ActiveProject, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ActiveProjectValidator, ActiveProjectRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ActiveProjectResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ActiveProjectCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<ActiveProjectResponse>(response.Error!);

        var activedProject = response.Value!;
        await _unitOfWork.CommitAsync(ct);

        return new ActiveProjectResponse(activedProject.Id, activedProject.IsActive);
    }

    public async Task<Result<StateChangerProjectsResponse?>> StateChangerProjects(
        StateChangerProjectsRequest request, CT ct)
    {
        _logger.LogInformation("Request for  StateChangerProjects, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<StateChangerProjectsValidator, StateChangerProjectsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<StateChangerProjectsResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<StateChangerProjectsResponse>(GlobalErrors.IdsNotEqual);

        var responses = await _mediator.Send(new GetsProjectByIdsQuery(
            request.Ids,
            null,
            request.Statuses,
            false,
            false,
            1,
            request.Ids.Count), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null || responses.Value.Data.Count <= 0)
            return Result.Failure<StateChangerProjectsResponse>(responses.Error!);
        var values = responses.Value.Data;

        var response = await _mediator.Send(new StateChangerProjectsCommand(values, request.State), ct);
        if (response.IsFailure)
            return Result.Failure<StateChangerProjectsResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new StateChangerProjectsResponse(true);
    }

    public async Task<Result<GroupProjectStatusChangerResponse?>> GroupProjectStatusChanger(
        GroupProjectStatusChangerRequest request, CT ct)
    {
        _logger.LogInformation("Request for  GroupProjectStatusChanger, Id:{Id}", request.Ids);

        var isValidRequest = await request.IsValidAsync<GroupProjectStatusChangerValidator, GroupProjectStatusChangerRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GroupProjectStatusChangerResponse>(isValidRequest.Error!);

        if (request.Ids.Count != request.Ids.Distinct().Count())
            return Result.Failure<GroupProjectStatusChangerResponse>(GlobalErrors.IdsNotEqual);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new ProjectStatusChangerCommand(item, null, null, request.Status, null), ct);
            if (response.IsFailure)
                return Result.Failure<GroupProjectStatusChangerResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new GroupProjectStatusChangerResponse(true);
    }

    public async Task<Result<DeleteProjectResponse?>> DeleteProject(
        DeleteProjectRequest request, CT ct)
    {
        _logger.LogInformation("Request for  DeleteProject, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DeleteProjectValidator, DeleteProjectRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteProjectResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DisableProjectCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteProjectResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);

        return new DeleteProjectResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<ProjectGroupDeleteResponse?>> ProjectGroupDelete(
        ProjectGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<ProjectGroupDeleteValidator, ProjectGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DisableProjectCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<ProjectGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new ProjectGroupDeleteResponse(true);
    }

    public async Task<Result<ProjectStatusChangerResponse?>> ProjectStatusChanger(
        ProjectStatusChangerRequest request, CT ct)
    {
        _logger.LogInformation("Request for  ProjectStatusChanger, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<ProjectStatusChangerValidator, ProjectStatusChangerRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectStatusChangerResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new ProjectStatusChangerCommand(request.Id, null, null, request.Status, null), ct);
        if (response.IsFailure)
            return Result.Failure<ProjectStatusChangerResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ProjectStatusChangerResponse(response.Value!.Id);
    }

    public async Task<Result<SetManagerToProjectsResponse?>> SetManagerToProjects(
        SetManagerToProjectsRequest request, CT ct)
    {
        _logger.LogInformation("Request for AddManagerToProjects");
        //Validate data
        var isValidRequest = await request.IsValidAsync<SetManagerToProjectsValidator, SetManagerToProjectsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<SetManagerToProjectsResponse>(isValidRequest.Error!);

        List<Project> projects = new();
        if (request.ProjectIds is not null && request.ProjectIds.Count > 0)
        {
            if (request.ProjectIds.Count != request.ProjectIds.Distinct().Count())
                return Result.Failure<SetManagerToProjectsResponse>(GlobalErrors.IdsNotEqual);

            var getProjects = await _mediator.Send(new GetsProjectByIdsQuery(
                request.ProjectIds,
                null,
                request.Statuses,
                false,
                false,
                1,
                request.ProjectIds.Count), ct);
            if (getProjects.IsFailure)
                return Result.Failure<SetManagerToProjectsResponse>(ProjectErrors.ProjectWithIdsNotFound);
            projects = getProjects.Value!.Data!;

            if (projects.Count != request.ProjectIds.Count)
                return Result.Failure<SetManagerToProjectsResponse>(ProjectErrors.UnValidIdInIds);
        }

        var validateManager = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [request.ProjectManagerId], null, false, null), ct);
        if (validateManager.IsFailure)
            return Result.Failure<SetManagerToProjectsResponse>(ProjectErrors.UnValidManagerId);

        var response = await _mediator.Send(new SetManagerToProjectsCommand(projects, request.ProjectManagerId), ct);
        if (response.IsFailure)
            return Result.Failure<SetManagerToProjectsResponse>(ProjectErrors.UnValidManagerId);

        await _unitOfWork.CommitAsync(ct);
        return new SetManagerToProjectsResponse(response.Value);
    }

    public async Task<Result<GetProjectByIdResponse?>> GetProjectById(
        GetProjectByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectById, id:{Id}", request.Id);
        var isValidRequest = await request.IsValidAsync<GetProjectByIdValidator, GetProjectByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectByIdResponse>(isValidRequest.Error!);

        var projectResponse = await _mediator.Send(new GetProjectByIdQuery(request.Id), ct);
        if (projectResponse.IsFailure)
            return Result.Failure<GetProjectByIdResponse>(projectResponse.Error!);
        var project = projectResponse.Value!;

        Company? company = null;
        if (project.CompanyId is not null && project.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(project.CompanyId, _mediator, ct);

        var allIds = IdCollector(project);
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct); // بره سراغ متا دیتا
        var usersInfos = metaDataInfos.Adapt<List<GetUserInfo>?>(); // تبدیل به نوع مورد نیاز

        var response = ProjectDatasCollector(project, usersInfos, company);

        if (response.OrganizationId is not null)
        {
            var org = await _viewOrganizationRepository.GetById(response.OrganizationId.Value, ct);
            response.Organization = org?.NameFa;
        }

        MyViewCity? city = null;
        if (project.CityId.HasValue)
        {
            city = await _viewCityRepository.GetCityById(project.CityId.Value, ct);
            if (city is not null)
            {
                var provinceData = await _mediator.Send(new GetProvinceByIdQuery(city.ProvinceId.Value), ct);

                var province = !provinceData.IsBad() && provinceData.Value!.Value is not null ? provinceData.Value.Value : null;

                var cityModel = new ProjectCityInfo
                {
                    CityId = city.Id,
                    City = city.Name,
                    ProvinceId = province?.Id,
                    Province = province?.Name,
                    CountryId = province?.Country.Id,
                    Country = province?.Country.Name,
                    AddressDescription = project.AddressDescription
                };

                response.ProjectCityInfo = cityModel;
            }

        }

        var creatorData = await _thirdPartyRepo.GetByUserIds([response.CreatorId], ct);
        if (creatorData is not null)
        {
            var creator = creatorData.FirstOrDefault();
            response.Creator = creator?.FirstName + " " + creator?.LastName;
        }
        return response;
    }

    public async Task<Result<GetSummarizedProjectByIdResponse?>> GetSummarizedProjectById(
        GetSummarizedProjectByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetSummarizedProjectById, id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<GetSummarizedProjectByIdValidator, GetSummarizedProjectByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetSummarizedProjectByIdResponse>(isValidRequest.Error!);

        var projectResponse = await _mediator.Send(new GetSummarizedProjectByIdQuery(request.Id), ct);
        if (projectResponse.IsFailure)
            return Result.Failure<GetSummarizedProjectByIdResponse>(projectResponse.Error!);
        var project = projectResponse.Value!;

        Company? company = null;
        if (project.CompanyId is not null && project.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(project.CompanyId, _mediator, ct);

        var allIds = IdCollector(project);
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct); // بره سراغ متا دیتا

        var usersInfos = metaDataInfos.Adapt<List<GetUserInfo>?>(); // تبدیل به نوع مورد نیاز
        var response = SummarizedProjectDataCollector(project, usersInfos, company);
        return response.Adapt<GetSummarizedProjectByIdResponse>();
    }

    public async Task<Result<GetProjectByNameResponse?>> GetProjectByName(
        GetProjectByNameRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectByName ProjectName:{ProjectName}", request.ProjectName);

        var isValidRequest = await request.IsValidAsync<GetProjectByNameValidator, GetProjectByNameRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectByNameResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectByNameQuery(request.ProjectName, request.CostCenterId, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectByNameResponse>(response.Error!);
        var project = response.Value!;

        Company? company = null;
        if (project.CompanyId is not null && project.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(project.CompanyId, _mediator, ct);

        var allIds = IdCollector(project);
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct); // بره سراغ متا دیتا

        var usersInfos = metaDataInfos.Adapt<List<GetUserInfo>?>(); // تبدیل به نوع مورد نیاز
        return ProjectDataCollector(project, usersInfos, company).Adapt<GetProjectByNameResponse>();
    }

    public async Task<Result<GetProjectByCodeResponse?>> GetProjectByCode(
        GetProjectByCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectByCode, ProjectCode:{ProjectCode}", request.ProjectCode);

        var isValidRequest = await request.IsValidAsync<GetProjectByCodeValidator, GetProjectByCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectByCodeResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectByCodeQuery(request.ProjectCode, null, null), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectByCodeResponse>(response.Error!);
        var project = response.Value!;

        Company? company = null;
        if (project.CompanyId is not null && project.CompanyId > 0)
            company = await WebServicesLogic.CompanyDataReceiver(project.CompanyId, _mediator, ct);

        var allIds = IdCollector(project);
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct); // بره سراغ متا دیتا

        var usersInfos = metaDataInfos.Adapt<List<GetUserInfo>?>(); // تبدیل به نوع مورد نیاز
        return ProjectDataCollector(project, usersInfos, company).Adapt<GetProjectByCodeResponse>();
    }

    public async Task<Result<GetActiveProjectsResponse?>> GetActiveProjects(
        GetActiveProjectsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetActiveProjects, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetActiveProjectsValidator, GetActiveProjectsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetActiveProjectsResponse>(isValidRequest.Error!);

        bool checkThirdParty = false;
        var engConfig = await _mediator.Send(new GetActiveConfigQuery());
        if (request.ThirdPartyId is not null)
            if (!engConfig.IsBad() && engConfig.Value!.ProjectThirdParties)
                checkThirdParty = true;

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetActiveProjectsQuery(
            request.FilterData,
            request.EmployerId,
            request.CostCenterId,
            request.ProjectTypeId,
            request.CategoryId,
            request.ProjectManagerId,
            request.PlanningAssistantId,
            request.ThirdPartyId,
            request.SupervisorEngineerId,
            request.AdvisorId,
            request.ImplementationAssistantId,
            request.TechnicalAssistantId,
            companyId,
            request.Contractual,
            request.Statuses,
            checkThirdParty,
            request.HaveCostCenter!.Value,
            request.IsOrganizationUnit!.Value,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetActiveProjectsResponse>(response.Error!);
        var values = response.Value?.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsActiveProjectModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetActiveProjectsResponse(data ?? new List<GetsActiveProjectModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsActiveProjectByCostCenterIdsResponse?>> GetsActiveProjectByCostCenterIds(
        GetsActiveProjectByCostCenterIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsActiveProjectByCostCenterIds, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsActiveProjectByCostCenterIdsValidator, GetsActiveProjectByCostCenterIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsActiveProjectByCostCenterIdsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsActiveProjectByCostCenterIdsQuery(
            request.FilterData,
            request.EmployerId,
            request.CostCenterIds,
            request.ProjectTypeId,
            request.CategoryId,
            request.ProjectManagerId,
            request.PlanningAssistantId,
            request.SupervisorEngineerId,
            request.AdvisorId,
            request.ImplementationAssistantId,
            request.TechnicalAssistantId,
            companyId,
            request.Contractual,
            request.Statuses,
            request.HaveCostCenter!.Value,
            request.IsOrganizationUnit!.Value,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsActiveProjectByCostCenterIdsResponse>(response.Error!);
        var values = response.Value?.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsActiveProjectModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsActiveProjectByCostCenterIdsResponse(data ?? new List<GetsActiveProjectModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetProjectsResponse?>> GetProjects(
        GetProjectsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjects, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetProjectsValidator, GetProjectsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectsResponse>(isValidRequest.Error!);

        bool checkThirdParty = false;
        var engConfig = await _mediator.Send(new GetActiveConfigQuery());
        if (request.ThirdPartyId is not null)
            if (!engConfig.IsBad() && engConfig.Value!.ProjectThirdParties)
                checkThirdParty = true;

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetProjectsQuery(
            null,
            request.FilterData,
            request.Status,
            request.CategoryIds,
            request.CostCenterId,
            request.AdvisorId,
            request.ProjectManagerId,
            request.PlanningAssistantId,
            request.ThirdPartyId,
            request.SupervisorEngineerId,
            request.ImplementationAssistantId,
            request.TechnicalAssistantId,
            request.EmployerId,
            request.ProjectTypeId,
            request.IsActive,
            companyId,
            request.OrderBy,
            request.Statuses,
            checkThirdParty,
            request.HaveCostCenter!.Value,
            request.IsOrganizationUnit!.Value,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectsResponse>(response.Error!);
        var projects = response.Value!.Data;

        var companyIds = projects?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var allIds = IdCollectors(projects!); //متد برای جمع آوری همه شناسه ها
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct); // بره سراغ متا دیتا
        var usersInfos = metaDataInfos.Adapt<List<GetUserInfo?>>(); // تبدیل به نوع مورد نیاز

        var cityIds = projects!.Where(x => x.CityId is not null).Select(x => x.CityId.Value).ToList();
        var cities = await _viewCityRepository.GetByIds(cityIds, ct);

        var data = ProjectsDataCollector(projects!, usersInfos, companies, cities);        // متدی برای جمع آوردی دیتای خروجی

        var orgsIds = data.NullListed(x => x.OrganizationId);
        List<ViewOrganization>? organizations = null;
        if (orgsIds != null && orgsIds.Count > 0)
            organizations = await _viewOrganizationRepository.GetByIds(orgsIds, ct);

        var creatorData = await _thirdPartyRepo.GetByUserIds(data.Listed(x => x.CreatorId), ct);
        foreach (var item in data)
        {
            var org = organizations?.FirstOrDefault(x => x.Id == item.OrganizationId);
            item.Organization = org?.NameFa;
            var creator = creatorData.FirstOrDefault(x => x.UserId == item.CreatorId);
            item.Creator = creator?.FirstName + " " + creator?.LastName;
        }

        return new GetProjectsResponse(data.Adapt<List<GetProjectsModel>>() ?? new List<GetProjectsModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsByNameOrCodeResponse?>> GetsByNameOrCode(
        GetsByNameOrCodeRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByNameOrCode, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsByNameOrCodeValidator, GetsByNameOrCodeRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsByNameOrCodeResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsByNameOrCodeQuery(
            request.FilterData,
            companyId,
            request.Statuses,
            request.HaveCostCenter!.Value,
            request.IsOrganizationUnit!.Value,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsByNameOrCodeResponse>(response.Error!);
        var projects = response.Value!.Data;

        var companyIds = projects?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var allIds = IdCollectors(projects!); //متد برای جمع آوری همه شناسه ها
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct); // بره سراغ متا دیتا
        var usersInfos = metaDataInfos.Adapt<List<GetUserInfo?>>(); // تبدیل به نوع مورد نیاز

        var cityIds = projects!.Where(x => x.CityId is not null).Select(x => x.CityId.Value).ToList();
        var cities = await _viewCityRepository.GetByIds(cityIds, ct);

        var data = await ProjectsDataCollector(response.Value?.Data!, usersInfos, companies, cities);// متدی برای جمع آوردی دیتای خروجی

        var orgsId = data.NullListed(x => x.OrganizationId);
        List<ViewOrganization>? organizations = null;
        if (orgsId != null && orgsId.Count > 0)
        {
            organizations = await _viewOrganizationRepository.GetByIds(orgsId, ct);
        }
        var creatorData = await _thirdPartyRepo.GetByUserIds(data.Listed(x => x.CreatorId), ct);
        foreach (var item in data)
        {
            var org = organizations?.FirstOrDefault(x => x.Id == item.OrganizationId);
            item.Organization = org?.NameFa;
            var creator = creatorData.FirstOrDefault(x => x.UserId == item.CreatorId);
            item.Creator = creator?.FirstName + " " + creator?.LastName;
        }

        return new GetsByNameOrCodeResponse(data.Adapt<List<GetProjectsModel>>() ?? new List<GetProjectsModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetProjectsByCostCenterResponse?>> GetProjectsByCostCenterId(
        GetProjectsByCostCenterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectsByCostCenterId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetProjectsByCostCenterValidator, GetProjectsByCostCenterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectsByCostCenterResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetProjectsByCostCenterQuery(
            request.CostCenterId,
            request.FilterData,
            request.Statuses,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetProjectsByCostCenterResponse>(response.Error!);
        var values = response.Value?.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetProjectsByCostCenterModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetProjectsByCostCenterResponse(data ?? new List<GetProjectsByCostCenterModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectByEmployerIdResponse?>> GetsByEmployerId(
        GetsProjectByEmployerIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByEmployerId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectByEmployerIdValidator, GetsProjectByEmployerIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectByEmployerIdResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsByEmployerIdQuery(
            request.EmployerId,
            request.FilterData,
            companyId,
            request.Statuses,
            request.HaveCostCenter!.Value,
            request.IsOrganizationUnit!.Value,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsProjectByEmployerIdResponse>(ProjectErrors.FilteredProjectNotFound);
        var projects = response.Value!.Data!;

        var companyIds = projects?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var allIds = projects!.Where(x => x.SupervisorEngineer != 0 && x.SupervisorEngineer is not null).Select(x => (long)x.SupervisorEngineer!).Distinct().ToList();
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct); // بره سراغ متا دیتا

        var data = new List<GetsProjectByEmployerIdModel>();
        foreach (var item in projects!)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            var supervisorInfo = metaDataInfos?.Where(x => x?.Id == item.SupervisorEngineer).FirstOrDefault();
            var supervisor = new ProjectUserModel(item.SupervisorEngineer, supervisorInfo?.UserId, supervisorInfo?.FullName, supervisorInfo?.AvatarUrl, supervisorInfo?.OrganizationCode);
            data.Add(new(item.Id, item.ProjectCode, item.ProjectName, item.ProjectCostCenters.FirstOrDefault()?.CostCenterId, item.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName, supervisor, item.Contractual, item.IsActive, item.CompanyId, company?.NameFa));
        }

        return new GetsProjectByEmployerIdResponse(data ?? new List<GetsProjectByEmployerIdModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectByProjectManagerIdResponse?>> GetsProjectByProjectManagerId(
        GetsProjectByProjectManagerIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByProjectManagerId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectByProjectManagerIdValidator, GetsProjectByProjectManagerIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectByProjectManagerIdResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsProjectByProjectManagerIdQuery(
            request.CostCenterIds,
            request.ProjectManagerId,
            request.FilterData,
            companyId,
            request.Statuses,
            request.HaveCostCenter!.Value,
            request.IsOrganizationUnit!.Value,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsProjectByProjectManagerIdResponse>(ProjectErrors.FilteredProjectNotFound);
        var projects = response.Value!.Data!;

        var data = projects.Adapt<List<GetsProjectByProjectManagerIdModel>>();
        return new GetsProjectByProjectManagerIdResponse(data ?? new List<GetsProjectByProjectManagerIdModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsContractedProjectResponse?>> GetsContractedProject(
        GetsContractedProjectRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsContractedProject, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsContractedProjectValidator, GetsContractedProjectRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsContractedProjectResponse>(isValidRequest.Error!);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (companyId is null ||
            await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetsContractedProjectResponse>(GlobalErrors.InvalidCompany);

        var response = await _mediator.Send(new GetsContractedProjectQuery(
            request.CostCenterIds,
            request.Status,
            request.FilterData,
            request.EmployerId,
            request.ProjectTypeId,
            request.CategoryId,
            request.IsActive,
            request.OrderBy,
            request.Statuses,
            request.HaveCostCenter,
            request.IsOrganizationUnit!.Value,
            request.PageIndex,
            request.PageSize,
            companyId.Value), ct);
        if (response.IsFailure)
            return Result.Failure<GetsContractedProjectResponse>(response.Error!);
        if (response.Value is null)
            return Result.Failure<GetsContractedProjectResponse>(ProjectErrors.FilteredProjectNotFound);
        if (response.Value.Data is null)
            return Result.Failure<GetsContractedProjectResponse>(ProjectErrors.FilteredProjectNotFound);
        var projects = response.Value!.Data!;

        var companyIds = projects?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var allIds = IdsCollector(projects!);
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct); // بره سراغ متا دیتا
        var responseMeta = metaDataInfos.Adapt<List<GetUserInfo?>>();

        var data = ProjectDataCollectorForGetContractedProject(projects!, responseMeta, companies);

        return new GetsContractedProjectResponse(data ?? new List<GetsContractedProjectResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectByIdsResponse?>> GetsProjectByIds(
        GetsProjectByIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectByIds, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectByIdsValidator, GetsProjectByIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectByIdsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsProjectByIdsQuery(
            request.Ids,
            request.FilterData,
            request.Statuses,
            request.HaveCostCenter!.Value,
            request.IsOrganizationUnit!.Value,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProjectByIdsResponse>(response.Error!);
        if (response.Value is null || response.Value.Data is null)
            return Result.Failure<GetsProjectByIdsResponse>(ProjectErrors.FilteredProjectNotFound);
        var values = response.Value.Data;

        var companyIds = values?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var data = values.Adapt<List<GetsProjectByIdsResponseModel>>();
        foreach (var item in data)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetsProjectByIdsResponse(data ?? new List<GetsProjectByIdsResponseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectSortingResponse?>> GetsProjectSorting(
        GetsProjectSortingRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectSorting, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectSortingValidator, GetsProjectSortingRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectSortingResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetsProjectSortingQuery(
            request.FilterData,
            request.Status,
            request.IsActive,
            companyId,
            request.HaveCostCenter!.Value,
            request.IsOrganizationUnit!.Value,
            request.Statuses), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProjectSortingResponse>(response.Error!);
        var queryable = response.Value!.Data!;

        if (request.SortBy != null && request.SortBy.Any())
        {
            foreach (var sortInfo in request.SortBy)
            {
                switch (sortInfo.Id)
                {
                    case "name":
                        queryable = sortInfo.Desc ? queryable.OrderByDescending(x => x.ProjectName) :
                            queryable.OrderBy(x => x.ProjectName);
                        break;
                    case "code":
                        queryable = sortInfo.Desc ? queryable.OrderByDescending(x => x.ProjectCode) :
                            queryable.OrderBy(x => x.ProjectCode);
                        break;
                    case "status":
                        queryable = sortInfo.Desc ? queryable.OrderByDescending(x => x.Status) :
                            queryable.OrderBy(x => x.Status);
                        break;
                    case "costCenter":
                        queryable = sortInfo.Desc ? queryable.OrderByDescending(x => x.ProjectCostCenters.FirstOrDefault()!.CostCenter) :
                            queryable.OrderBy(x => x.ProjectCostCenters.FirstOrDefault()!.CostCenter);
                        break;
                    case "category":
                        queryable = sortInfo.Desc ? queryable.OrderByDescending(x => x.ProjectCategories.Select(x => x.Category)) :
                            queryable.OrderBy(x => x.ProjectCategories.Select(x => x.Category));
                        break;
                }
            }
        }
        else
            queryable = queryable.OrderBy(x => x.Created);

        var count = queryable.Count();
        queryable = queryable.Page(request.PageIndex, request.PageSize);

        var projects = queryable.ToList();

        var companyIds = projects?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var allIds = IdCollectors(projects!);
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct);
        var usersInfos = metaDataInfos.Adapt<List<GetUserInfo?>>();

        var cityIds = projects!.Where(x => x.CityId is not null).Select(x => x.CityId.Value).ToList();
        var cities = await _viewCityRepository.GetByIds(cityIds, ct);

        var data = await ProjectsDataCollector(projects!, usersInfos, companies, cities);

        var orgsId = data.NullListed(x => x.OrganizationId);
        List<ViewOrganization>? organizations = null;
        if (orgsId != null && orgsId.Count > 0)
            organizations = await _viewOrganizationRepository.GetByIds(orgsId, ct);

        var creatorData = await _thirdPartyRepo.GetByUserIds(data.Listed(x => x.CreatorId), ct);
        foreach (var item in data)
        {
            var org = organizations?.FirstOrDefault(x => x.Id == item.OrganizationId);
            item.Organization = org?.NameFa;

            var creator = creatorData.FirstOrDefault(x => x.UserId == item.CreatorId);
            item.Creator = creator?.FirstName + " " + creator?.LastName;
        }

        return new GetsProjectSortingResponse(data.Adapt<List<GetProjectsModel>>() ?? new List<GetProjectsModel>(0), count);
    }

    public async Task<Result<GetsProjectExcelExporterResponse?>> GetsProjectExcelExporter(
        GetsProjectExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectExcelExporter, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsProjectExcelExporterValidator, GetsProjectExcelExporterRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsProjectExcelExporterResponse>(isValidRequest.Error!);

        bool checkThirdParty = false;
        var engConfig = await _mediator.Send(new GetActiveConfigQuery());
        if (request.ThirdPartyId is not null)
            if (!engConfig.IsBad() && engConfig.Value!.ProjectThirdParties)
                checkThirdParty = true;

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        var response = await _mediator.Send(new GetProjectsQuery(
            request.Ids,
            request.FilterData,
            request.Status,
            request.CategoryIds,
            request.CostCenterId,
            request.AdvisorId,
            request.ProjectManagerId,
            request.PlanningAssistantId,
            request.ThirdPartyId,
            request.SupervisorEngineerId,
            request.ImplementationAssistantId,
            request.TechnicalAssistantId,
            request.EmployerId,
            request.ProjectTypeId,
            request.IsActive,
            companyId,
            request.OrderBy,
            request.Statuses,
            checkThirdParty,
            request.HaveCostCenter!.Value,
            request.IsOrganizationUnit!.Value,
            request.PageIndex,
            request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsProjectExcelExporterResponse>(response.Error!);
        var projects = response.Value!.Data;

        var companyIds = projects?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var allIds = IdCollectors(projects!);
        var metaDataInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allIds, null, null, _mediator, ct);


        var creatorData = await _thirdPartyRepo.GetByUserIds(projects!.Listed(x => x.CreatorId), ct);
        var cityData = await _viewCityRepository.GetByIds(projects.Where(x => x.CityId is not null).Select(x => x.CityId.Value).ToList(), ct);

        var data = projects.Adapt<List<GetsProjectExcelExporterModel>>();
        foreach (var item in data)
        {
            var creator = creatorData.FirstOrDefault(x => x.UserId == item.CreatorId);

            item.Creator = creator?.FirstName + " " + creator?.LastName;
            item.City = cityData.FirstOrDefault(x => x.Id == item.CityId)?.Name;

            item.EmployerName = metaDataInfos?.Where(x => x?.Id == item.EmployerId).FirstOrDefault()?.FullName;
            item.CompanyNameFa = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault()?.NameFa;
            if (item.SupervisorEngineer is not null)
                item.SupervisorEngineerName = metaDataInfos?.Where(x => x?.Id == item.SupervisorEngineer).FirstOrDefault()?.FullName;
            if (item.AdvisorId is not null)
                item.AdvisorName = metaDataInfos?.Where(x => x?.Id == item.AdvisorId).FirstOrDefault()?.FullName;
            if (item.ProjectManagerId is not null)
                item.ProjectManagerName = metaDataInfos?.Where(x => x?.Id == item.ProjectManagerId).FirstOrDefault()?.FullName;
            if (item.PlanningAssistantId is not null)
                item.PlanningAssistantName = metaDataInfos?.Where(x => x?.Id == item.PlanningAssistantId).FirstOrDefault()?.FullName;
        }

        var file = new FileContentResult(ProjectExcels.ProjectToExcel(data, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"Projects-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsProjectExcelExporterResponse(file);
    }

    public async Task<Result<GetContractorProjectsResponse?>> GetContractorProjects(
        GetContractorProjectsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetContractorProjects , pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetContractorProjectsValidator, GetContractorProjectsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetContractorProjectsResponse>(isValidRequest.Error!);

        var contractor = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, [request.ContractorId], null, true, null), ct);
        if (contractor.IsFailure)
            return Result.Failure<GetContractorProjectsResponse>(contractor.Error!);

        var responses = await _mediator.Send(new GetContractorProjectsQuery(
            request.ContractorId,
            request.CostCenterId,
            request.FilterData,
            request.Statuses,
            request.HaveCostCenter!.Value,
            request.IsOrganizationUnit!.Value,
            request.PageIndex,
            request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetContractorProjectsResponse>(responses.Error!);
        var values = responses.Value!.Data!;

        var data = values.Adapt<List<GetContractorProjectsModel>>();

        return new GetContractorProjectsResponse(data ?? new List<GetContractorProjectsModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsProjectStatusResponse?>> GetsProjectStatus(
        GetsProjectStatusRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectStatus");
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<ProjectStatus>());
        return new GetsProjectStatusResponse(result);
    }

    public async Task<Result<GetProjectForPdfResponse?>> GetProjectForPdf(
        GetProjectForPdfRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectForPdf");
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        var result = await _mediator.Send(new GetProjectForPdfQuery(request.Id), ct);
        if (result.IsBad()) return result.Failure<GetProjectForPdfResponse>()!;

        return result;
    }

    public async Task<Result<GetProjectPOTimelinesResponse?>> GetProjectPOTimelines(
        GetProjectPOTimelinesRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectPOTimelines");
        var result = await _mediator.Send(new GetProjectPOTimelinesQuery(request.Id), ct);
        if (result.IsBad()) return result.Failure<GetProjectPOTimelinesResponse>()!;

        return result;
    }

    public async Task<Result<GetProjectProgressResponse?>> GetProjectProgress(
        GetProjectProgressRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectPOTimelines");
        var result = await _mediator.Send(new GetProjectProgressQuery(request.Id), ct);
        if (result.IsBad()) return result.Failure<GetProjectProgressResponse>()!;

        return result;
    }

    public async Task<Result<GetUnAssignedProjectsResponse?>> GetUnAssignedProjects(
        GetUnAssignedProjectsRequest request, CT ct)
    {
        _logger.LogInformation("GetUnAssignedProjects");
        var result = await _mediator.Send(new GetUnAssignedProjectsQuery(request.FilterData,
            request.CityId,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad()) return result.Failure<GetUnAssignedProjectsResponse>()!;

        return result;
    }

    public async Task<Result<AssignProjectsToCostCenterResponse?>> AssignProjectsToCostCenter(
        AssignProjectsToCostCenterRequest request, CT ct)
    {
        _logger.LogInformation("GetUnAssignedProjects");
        var result = await _mediator.Send(new AssignProjectsToCostCenterCommand(request.CostCenterId,
            request.ProjectIds), ct);
        if (result.IsBad()) return result.Failure<AssignProjectsToCostCenterResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return result;
    }

    public async Task<Result<GetsProjectExcelEnumResponse?>> GetsProjectExcelEnum(
        GetsProjectExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsProjectExcelEnum");
        var result = await Task.Run(() => EnumExt.GetEnumObjectList<ProjectExcelEnum>());
        return new GetsProjectExcelEnumResponse(result);
    }

    public async Task<Result<GetProjectHistoryResponse?>> GetProjectHistory(
        GetProjectHistoryRequest request, CT ct)
    {
        _logger.LogInformation("GetProjectHistory");

        var projectHistories = await _projectHistoryRepository.GetProjectHistory(request.Id, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize, ct);
        if (projectHistories is null)
            return Result.Failure<GetProjectHistoryResponse>(ProjectErrors.ProjectWithIdNotFound);
        var companyIds = projectHistories?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var employerIds = projectHistories?.Where(x => x.EmployerId != null && x.EmployerId > 0).Select(x => (long)x.EmployerId!).ToList();
        var employers = await WebServicesLogic.UserDataReceiver(employerIds, null, _mediator, ct);

        var supervisorEngineerIds = projectHistories?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var supervisorEngineerNames = await WebServicesLogic.UserDataReceiver(supervisorEngineerIds, null, _mediator, ct);

        var advisorIds = projectHistories?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var advisorNames = await WebServicesLogic.UserDataReceiver(advisorIds, null, _mediator, ct);

        var projectManagerIds = projectHistories?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var projectManagerNames = await WebServicesLogic.UserDataReceiver(projectManagerIds, null, _mediator, ct);

        var planningAssistantIds = projectHistories?.Where(x => x.CompanyId != null && x.CompanyId > 0).Select(x => (long)x.CompanyId!).ToList();
        var planningAssistantNames = await WebServicesLogic.UserDataReceiver(planningAssistantIds, null, _mediator, ct);
        foreach (var item in projectHistories)
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();
            var employer = employers?.Where(x => x.Id == item.EmployerId).FirstOrDefault();
            var supervisorEngineer = supervisorEngineerNames?.Where(x => x.Id == item.SupervisorEngineer).FirstOrDefault();
            var advisor = advisorNames?.Where(x => x.Id == item.AdvisorId).FirstOrDefault();
            var projectManager = projectManagerNames?.Where(x => x.Id == item.ProjectManagerId).FirstOrDefault();
            var planningAssistantId = planningAssistantNames?.Where(x => x.Id == item.ProjectManagerId).FirstOrDefault();
            item.CompanyNameFa = company?.NameFa;
        }

        return new GetProjectHistoryResponse(projectHistories, projectHistories.Count);
    }
}
