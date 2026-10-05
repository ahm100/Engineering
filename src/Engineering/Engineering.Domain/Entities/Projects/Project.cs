using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProcesVerbal;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.Histories;
using Engineering.Domain.Entities.Projects.Junctions;
using Engineering.Domain.Entities.Projects.ProjectCalendars;
using Engineering.Domain.Entities.Projects.ProjectUsers;
using Engineering.Domain.Entities.Projects.WBS;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestRewards;
using Engineering.Domain.Entities.SessionRecords;
using Engineering.Domain.Entities.TelegramChats;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Domain.Entities.Projects;

[Description(GlobalCmts.Project)]
public class Project : AuditableEntity<Project>
{
    [Description(ProjectCmts.ProjectName)]
    public string ProjectName { get; private set; } = string.Empty;

    [Description(ProjectCmts.ProjectName)]
    public string? ProjectEnName { get; private set; } = string.Empty;

    [Description(ProjectCmts.ProjectCode)]
    public string? ProjectCode { get; private set; } = string.Empty;

    [Description(ProjectCmts.Prefix)]
    public string? Prefix { get; private set; }

    [Description(ProjectCmts.EmployerId)]
    public long? EmployerId { get; private set; }

    [Description(ProjectCmts.SupervisorEngineer)]
    public long? SupervisorEngineer { get; private set; } = default;

    [Description(ProjectCmts.Advisor)]
    public long? Advisor { get; private set; } = default;

    [Description(ProjectCmts.ProjectManager)]
    public long? ProjectManager { get; private set; } = default;

    [Description(ProjectCmts.PlanningAssistant)]
    public long? PlanningAssistant { get; private set; } = default;

    [Description(ProjectCmts.Status)]
    public ProjectStatus Status { get; private set; }

    [Description(ProjectCmts.AddAutomated)]
    public bool AddAutomated { get; private set; } = false;

    [Description(ProjectCmts.HasProduct)]
    public bool HasProduct { get; private set; } = false;

    [Description(ProjectCmts.Contractual)]
    public bool Contractual { get; private set; } = true;

    [Description(ProjectCmts.CollectiveService)]
    public bool CollectiveService { get; private set; } = false;

    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(ProjectCmts.ApprovedBudget)]
    public decimal? ApprovedBudget { get; private set; }

    [Description(GlobalCmts.CityId)]
    public long? CityId { get; private set; }

    [Description(ProjectCmts.AddressDescription)]
    public string? AddressDescription { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? DescriptionEn { get; private set; }

    [Description(GlobalCmts.PreferentialReferenceCode)]
    public Guid PreferentialReferenceCode { get; private set; }
    [Description(ProjectCmts.ProjectType)]
    public ProjectType? ProjectType { get; set; }
    public long? ProjectTypeId { get; set; }

    [Description(GlobalCmts.CostCenter)]
    public CostCenter? CostCenter { get; set; }
    public long? CostCenterId { get; set; }

    [Description(ProjectCmts.IsOrganizationUnit)]
    public bool IsOrganizationUnit { get; private set; } = false;

    [Description(ProjectCmts.OrganizationId)]
    public long? OrganizationId { get; private set; }

    public Project(
        List<Category>? categories,
        CostCenter? costCenter,
        ProjectType? projectType,
        string projectName,
        string? projectEnName,
        string? projectCode,
        string? prefix,
        long? employerId,
        long? supervisorEngineer,
        long? advisor,
        long? projectManager,
        long? planningAssistant,
        ProjectStatus status,
        bool contractual,
        bool collectiveService,
        bool isActive,
        decimal? approvedBudget,
        long? cityId,
        string? description,
        string? descriptionEn,
        string? addressDescription,
        long? companyId,
        bool hasProduct,
        long? organizationId,
        bool isOrganizationUnit) : this()
    {
        SetProjectType(projectType);
        SetProjectName(projectName);
        SetEnName(projectEnName);
        SetDescriptionEn(descriptionEn);
        SetProjectCode(projectCode);
        SetPrefix(prefix);
        SetEmployerId(employerId);
        SetSupervisorEngineer(supervisorEngineer);
        SetAdvisor(advisor);
        SetProjectManager(projectManager);
        SetPlanningAssistant(planningAssistant);
        SetStatus(status);
        SetCompanyId(companyId);
        SetContractual(contractual);
        SetCollectiveService(collectiveService);
        IsActive = isActive;
        SetPreferentialReferenceCode(Guid.NewGuid());
        SetHasProduct(hasProduct);
        SetApprovedBudget(approvedBudget);
        SetDescription(description);
        SetAddressDescription(addressDescription);
        SetOrganizationId(organizationId);
        SetIsOrganizationUnit(isOrganizationUnit);
        SetCityId(cityId);

        if (costCenter is not null)
            _projectCostCenters.Add(new ProjectCostCenter(this, costCenter, false));

        if (categories is not null && categories.Count > 0)
            SetCategory(categories);
    }

    #region Set data

    public void SetProjectType(ProjectType? value)
    {
        ProjectType = value;
        ProjectTypeId = value?.Id;
    }

    public void SetProjectName(string value)
    {
        ProjectName = Guard.Against.Null(value, nameof(value));
    }

    public void SetDescriptionEn(string? value)
    {
        DescriptionEn = value;
    }
    public void SetEnName(string? value)
    {
        ProjectEnName = value;
    }

    public void SetPreferentialReferenceCode(Guid value)
    {
        PreferentialReferenceCode = Guard.Against.Null(value, nameof(value));
    }

    public void SetHasProduct(bool value)
    {
        HasProduct = Guard.Against.Null(value, nameof(value));
    }

    public void SetProjectCode(string? value)
    {
        ProjectCode = value;
    }

    public void SetPrefix(string? value)
    {
        Prefix = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    public void SetIsOrganizationUnit(bool value)
    {
        IsOrganizationUnit = Guard.Against.Null(value, nameof(value));
    }

    public void SetOrganizationId(long? value)
    {
        OrganizationId = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetAddressDescription(string? value)
    {
        AddressDescription = value;
    }

    public void SetCityId(long? value)
    {
        CityId = value;
    }

    public void SetApprovedBudget(decimal? value)
    {
        ApprovedBudget = value ?? ApprovedBudget;
    }

    public void SetStatus(ProjectStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    public ProjectStatus StatusChecker(Project entity)
    {
        var status = ProjectStatus.NotStarted;

        if (entity.ProjectOperations.All(x => x.ProjectOperationStatus == ProjectOperationStatus.NotStarted))
            status = ProjectStatus.NotStarted;
        else if (entity.ProjectOperations.All(x => x.ProjectOperationStatus == ProjectOperationStatus.Doing))
            status = ProjectStatus.Doing;
        else if (entity.ProjectOperations.All(x => x.ProjectOperationStatus == ProjectOperationStatus.Stopped))
            status = ProjectStatus.Stopped;
        else if (entity.ProjectOperations.All(x => x.ProjectOperationStatus == ProjectOperationStatus.EndOfWork))
            status = ProjectStatus.EndOfWork;
        else if (entity.ProjectOperations.All(x => x.ProjectOperationStatus == ProjectOperationStatus.TemporaryDelivery))
            status = ProjectStatus.TemporaryDelivery;
        else if (entity.ProjectOperations.All(x => x.ProjectOperationStatus == ProjectOperationStatus.DefiniteDelivery))
            status = ProjectStatus.DefiniteDelivery;

        else if (entity.ProjectOperations.Any(x => x.ProjectOperationStatus == ProjectOperationStatus.Doing))
            status = ProjectStatus.Doing;

        else
            status = ProjectStatus.Doing;

        return status;
    }
    public void SetName(string value)
    {
        ProjectName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCollectiveService(bool value)
    {
        CollectiveService = value;
    }
    public void SetCode(string value)
    {
        ProjectCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetEmployerId(long? value)
    {
        EmployerId = value;
    }
    public void SetCategory(List<Category> values)
    {
        foreach (var value in values)
        {
            _projectCategory.Add(new ProjectCategory(this, value));
        }
    }
    public void SetAdvisor(long? value)
    {
        Advisor = value;
    }
    public void SetContractual(bool value)
    {
        Contractual = value;
    }
    public void SetSupervisorEngineer(long? value)
    {
        SupervisorEngineer = value;
    }
    public void SetProjectManager(long? value)
    {
        ProjectManager = value;
    }
    public void SetPlanningAssistant(long? value)
    {
        PlanningAssistant = value;
    }
    public void SetActive()
    {
        IsActive = true;
    }
    public void SetInActive()
    {
        IsActive = false;
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void AddHistory()
    {
        _projectHistories.Add(new ProjectHistory(
            this));
    }

    public void SetCostCenter(CostCenter costCenter)
    {
        _projectCostCenters.Add(new ProjectCostCenter(
            this, costCenter, true));
    }
    #endregion

    #region Methods 

    public void AddTechnicalAssistant(ProjectTechnicalAssistant newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_projectTechnicalAssistants.Any(oo => oo.TechnicalAssistantUserId == newData.TechnicalAssistantUserId && oo.Created == newData.Created))
            return;

        _projectTechnicalAssistants.Add(newData);
    }

    public void AddImplementationAssistant(ProjectImplementationAssistant newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_projectImplementationAssistants.Any(oo => oo.ImplementationAssistantUserId == newData.ImplementationAssistantUserId && oo.Created == newData.Created))
            return;

        _projectImplementationAssistants.Add(newData);
    }

    public void AddWarehouse(long warehouseId, bool isDefault)
    {
        if (_projectWarehouses.Any(oo => oo.WarehouseId == warehouseId))
            return;

        _projectWarehouses.Add(new ProjectWarehouse(this, warehouseId, isDefault));
    }

    #endregion

    public string GetPreferentialName()
    {
        return $"{ProjectName}-{ProjectCode}";
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private readonly List<EmployerContract> _employerContracts;
    public IReadOnlyList<EmployerContract> EmployerContracts => _employerContracts;

    private readonly List<ProjectTechnicalAssistant> _projectTechnicalAssistants;
    public IReadOnlyList<ProjectTechnicalAssistant> ProjectTechnicalAssistants => _projectTechnicalAssistants;

    private readonly List<ProjectImplementationAssistant> _projectImplementationAssistants;
    public IReadOnlyList<ProjectImplementationAssistant> ProjectImplementationAssistants => _projectImplementationAssistants;

    private readonly List<ProjectOperation> _projectOperations;
    public IReadOnlyList<ProjectOperation> ProjectOperations => _projectOperations;

    private readonly List<FiduciaryProduct> _fiduciaryProduct;
    public IReadOnlyList<FiduciaryProduct> FiduciaryProducts => _fiduciaryProduct;

    private readonly List<EmployerStatusStatement> _employerStatusStatement;
    public IReadOnlyList<EmployerStatusStatement> EmployerStatusStatements => _employerStatusStatement;

    private readonly List<RequestMachinery> _requestMachineries;
    public IReadOnlyList<RequestMachinery> RequestMachineries => _requestMachineries;

    private readonly List<TransportationRequest> _transportationRequest;
    public IReadOnlyList<TransportationRequest> TransportationRequests => _transportationRequest;

    private readonly List<RequestReward> _requestRewards;
    public IReadOnlyList<RequestReward> RequestRewards => _requestRewards;

    private readonly List<OperationLocation> _operationLocations;
    public IReadOnlyList<OperationLocation> OperationLocations => _operationLocations;

    private readonly List<TelegramChat> _telegramChats;
    public IReadOnlyList<TelegramChat> TelegramChats => _telegramChats;

    private readonly List<ContractorStatusStatement> _contractorStatusStatements;
    public IReadOnlyList<ContractorStatusStatement> ContractorStatusStatements => _contractorStatusStatements;

    private readonly List<ProjectOperationTemporaryDaily> _projectOperationTemporaryDailies;
    public IReadOnlyList<ProjectOperationTemporaryDaily> ProjectOperationTemporaryDailies => _projectOperationTemporaryDailies;

    private readonly List<ProjectOperationDetailInspection> _projectOperationDetailInspections;
    public IReadOnlyList<ProjectOperationDetailInspection> ProjectOperationDetailInspections => _projectOperationDetailInspections;

    private readonly List<RequestMachineryStatusStatement> _requestMachineryStatusStatements;
    public IReadOnlyList<RequestMachineryStatusStatement> RequestMachineryStatusStatements => _requestMachineryStatusStatements;

    private readonly List<RequestMachineryStatusStatementDetail> _requestMachineryStatusStatementDetails;
    public IReadOnlyList<RequestMachineryStatusStatementDetail> RequestMachineryStatusStatementDetails => _requestMachineryStatusStatementDetails;

    private readonly List<TransportationRequestProject> _transportationRequestProjects;
    public IReadOnlyList<TransportationRequestProject> TransportationRequestProjects => _transportationRequestProjects;

    private readonly List<ProjectService> _projectServices;
    public IReadOnlyList<ProjectService> ProjectServices => _projectServices;

    private readonly List<ContractorContract> _contractorContracts;
    public IReadOnlyList<ContractorContract> ContractorContracts => _contractorContracts;

    private readonly List<ProjectHistory> _projectHistories;
    public IReadOnlyList<ProjectHistory> ProjectHistories => _projectHistories;

    private readonly List<ProjectProduct> _projectProducts;
    public IReadOnlyList<ProjectProduct> ProjectProducts => _projectProducts;

    private readonly List<RequestGoodsSupply> _requestGoodsSupplies;
    public IReadOnlyList<RequestGoodsSupply> RequestGoodsSupplies => _requestGoodsSupplies;

    private readonly List<ProjectCategory> _projectCategory;
    public IReadOnlyList<ProjectCategory> ProjectCategories => _projectCategory;

    private readonly List<ProjectRisk> _projectRisks;
    public IReadOnlyList<ProjectRisk> ProjectRisks => _projectRisks;

    private readonly List<ProjectThirdParty> _projectThirdParties;
    public IReadOnlyList<ProjectThirdParty> ProjectThirdParties => _projectThirdParties;

    private readonly List<ProjectWbs> _projectWbses;
    public IReadOnlyList<ProjectWbs> ProjectWbses => _projectWbses;

    private readonly List<ProjectScheduleImport> _projectScheduleImports;
    public IReadOnlyList<ProjectScheduleImport> ProjectScheduleImports => _projectScheduleImports;

    private readonly List<ProjectCostCenter> _projectCostCenters;
    public IReadOnlyList<ProjectCostCenter> ProjectCostCenters => _projectCostCenters;

    private readonly List<ProjectWarehouse> _projectWarehouses;
    public IReadOnlyList<ProjectWarehouse> ProjectWarehouses => _projectWarehouses;

    private readonly List<ProjectCalendar> _projectCalendars;
    public IReadOnlyList<ProjectCalendar> ProjectCalendars => _projectCalendars;

    private readonly List<ProcesVerbal.ProcesVerbal> _procesVerbals;
    public IReadOnlyList<ProcesVerbal.ProcesVerbal> ProcesVerbals => _procesVerbals;

    private readonly List<SessionRecord> _sessionRecords;
    public IReadOnlyList<SessionRecord> SessionRecords => _sessionRecords;

    private readonly List<SubProject> _subProjects;
    public IReadOnlyList<SubProject> SubProjects => _subProjects;

    private Project()
    {
        _projectCalendars = [];
        _subProjects = [];
        _projectScheduleImports = [];
        _employerContracts = [];
        _projectTechnicalAssistants = [];
        _projectImplementationAssistants = [];
        _projectOperations = [];
        _fiduciaryProduct = [];
        _employerStatusStatement = [];
        _requestMachineries = [];
        _transportationRequest = [];
        _requestRewards = [];
        _operationLocations = [];
        _telegramChats = [];
        _contractorStatusStatements = [];
        _projectOperationTemporaryDailies = [];
        _projectOperationDetailInspections = [];
        _requestMachineryStatusStatements = [];
        _requestMachineryStatusStatementDetails = [];
        _transportationRequestProjects = [];
        _projectServices = [];
        _contractorContracts = [];
        _projectHistories = [];
        _projectProducts = [];
        _requestGoodsSupplies = [];
        _projectCategory = [];
        _projectRisks = [];
        _projectWbses = [];
        _projectCostCenters = [];
        _projectWarehouses = [];
        _procesVerbals = [];
        _sessionRecords = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
