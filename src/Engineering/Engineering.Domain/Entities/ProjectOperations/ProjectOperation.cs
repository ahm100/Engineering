using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.WBS;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestRewards;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Domain.Entities.ProjectOperations;

[Description(GlobalCmts.ProjectOperation)]
public class ProjectOperation : AuditableEntity<ProjectOperation>
{

    [Description(ProjectOperationCmts.Workload)]
    public decimal Workload { get; private set; } = 0;
    [Description(ProjectOperationCmts.TolerancePercentage)]
    public decimal TolerancePercentage { get; private set; }
    [Description(ProjectOperationCmts.Price)]
    public decimal? Price { get; private set; } = 0;
    [Description(ProjectOperationCmts.BasePrice)]
    public decimal BasePrice { get; private set; } = 0;
    [Description(ProjectOperationCmts.ChangedPrice)]
    public decimal ChangedPrice { get; private set; } = 0;
    [Description(ProjectOperationCmts.Priority)]
    public int? Priority { get; private set; }
    [Description(ProjectOperationCmts.IncreaseRate)]
    public decimal IncreaseRate { get; private set; } = 1;
    [Description(ProjectOperationCmts.UnitOfMeasurementId)]
    public long UnitOfMeasurementId { get; private set; }
    [Description(ProjectOperationCmts.ProjectOperationStatus)]
    public ProjectOperationStatus ProjectOperationStatus { get; private set; } = ProjectOperationStatus.NotStarted;
    [Description(ProjectOperationCmts.GoodsInProgress)]
    public bool GoodsInProgress { get; private set; } = false;
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; } = string.Empty;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(GlobalCmts.Project)]
    public Project Project { get; set; }
    public long ProjectId { get; set; }
    [Description(GlobalCmts.OperationInfo)]
    public OperationInfo OperationInfo { get; set; }
    public long OperationInfoId { get; set; }


    /// Timing

    [Description(ProjectOperationCmts.PlannedStartDate)]
    public DateTime? PlannedStartDate { get; private set; }

    [Description(ProjectOperationCmts.PlannedFinishDate)]
    public DateTime? PlannedFinishDate { get; private set; }

    [Description(ProjectOperationCmts.PlannedDuration)]
    public int? PlannedDuration { get; private set; }

    [Description(ProjectOperationCmts.ActualStartDate)]
    public DateTime? ActualStartDate { get; private set; }

    [Description(ProjectOperationCmts.ActualFinishDate)]
    public DateTime? ActualFinishDate { get; private set; }

    [Description(ProjectOperationCmts.BaselineStartDate)]
    public DateTime? BaselineStartDate { get; private set; }

    [Description(ProjectOperationCmts.BaselineFinishDate)]
    public DateTime? BaselineFinishDate { get; private set; }

    [Description(ProjectOperationCmts.BaselineDuration)]
    public int? BaselineDuration { get; private set; }

    public ProjectOperation(
        Project project,
        OperationInfo operation,
        EmployerContract? employerContract,
        decimal workload,
        decimal tolerancePercentage,
        decimal? price,
        int? priority,
        long unitOfMeasurementId,
        ProjectOperationStatus projectOperationStatus,
        bool goodsInProgress,
        DateTime? baselineStartDate,
        DateTime? baselineFinishDate,
        int? baselineDuration,
        string? description,
        List<string>? documentUrls,
        List<EmployerConsideration>? considerations,
        long? companyId) : this()
    {
        SetProject(project);
        SetOperationInfo(operation);
        SetWorkLoad(workload);
        SetTolerancePercentage(tolerancePercentage);
        SetUnitOfMeasurementId(unitOfMeasurementId);
        SetProjectOperationStatus(projectOperationStatus);
        GoodsInProgress = goodsInProgress;
        SetPriority(priority);
        SetCompanyId(companyId);
        SetPrice(price);
        SetDescription(description);
        if (documentUrls != null && documentUrls.Any())
            AddDocuments(documentUrls);

        if (employerContract is not null)
            _employerOperations.Add(new(employerContract, this, Description, considerations, null));

        SetBaselineStartDate(baselineStartDate);
        SetPlannedStartDate(baselineStartDate);

        SetBaselineFinishDate(baselineFinishDate);
        SetPlannedFinishDate(baselineFinishDate);

        SetBaselineDuration(baselineDuration);
        SetPlannedDuration(baselineDuration);

        AddHistory();
    }

    public static ProjectOperation Create(
        Project project,
        OperationInfo operation,
        EmployerContract? employerContract,
        decimal workload,
        decimal tolerancePercentage,
        decimal? price,
        int? priority,
        long unitOfMeasurementId,
        ProjectOperationStatus projectOperationStatus,
        bool goodsInProgress,
        DateTime? baselineStartDate,
        DateTime? baselineFinishDate,
        int? baselineDuration,
        string? description,
        List<string>? documentUrls,
        List<EmployerConsideration>? considerations,
        long? companyId)
    {
        return new ProjectOperation(
            project,
            operation,
            employerContract,
            workload,
            tolerancePercentage,
            price,
            priority,
            unitOfMeasurementId,
            projectOperationStatus,
            goodsInProgress,
            baselineStartDate,
            baselineFinishDate,
            baselineDuration,
            description,
            documentUrls,
            considerations,
            companyId);
    }

    public void Update(
        decimal workLoad,
        ProjectOperationStatus status,
        int? priority)
    {
        SetWorkLoad(workLoad);
        SetProjectOperationStatus(status);
        SetPriority(priority);
        AddHistory();
    }

    public void UpdateVolume(
        decimal workLoad)
    {
        SetWorkLoad(workLoad);
        AddHistory();
    }

    public void UpdateCost(
        decimal? price,
        decimal? tolerancePercentage)
    {
        SetPrice(price ?? Price);
        SetTolerancePercentage(tolerancePercentage ?? TolerancePercentage);
        AddHistory();
    }

    public void UpdatePrice(
        decimal? changedPrice,
        int? increaseRate)
    {
        ChangedPrice = changedPrice ?? ChangedPrice;
        IncreaseRate = increaseRate ?? IncreaseRate;
        AddHistory();
    }

    public void AddHistory()
    {
        _projectOperationHistory.Add(new ProjectOperationHistory(this));
    }

    #region Set data

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    public void SetWorkLoad(decimal value)
    {
        Workload = Guard.Against.Null(value, nameof(value));
    }
    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetOperationInfo(OperationInfo value)
    {
        OperationInfo = Guard.Against.Null(value, nameof(value));
        OperationInfoId = Guard.Against.Null(value.Id, nameof(value.Id));

        SetBasePrice(value.BasePrice);
        SetChangedPrice(value.BasePrice);
    }
    public void SetWorkload(decimal value)
    {
        Workload = Guard.Against.Null(value, nameof(value));
    }
    public void SetTolerancePercentage(decimal value)
    {
        TolerancePercentage = Guard.Against.Null(value, nameof(value));
    }
    public void SetUnitOfMeasurementId(long value)
    {
        UnitOfMeasurementId = Guard.Against.Null(value, nameof(value));
    }
    public void SetProjectOperationStatus(ProjectOperationStatus value)
    {
        ProjectOperationStatus = Guard.Against.Null(value, nameof(value));
    }
    public void SetPriority(int? value)
    {
        Priority = value;
    }
    public void SetPrice(decimal? value)
    {
        Price = value;
    }
    public void SetDescription(string? value)
    {
        Description = value;
    }
    public void SetGoodsInProgress()
    {
        GoodsInProgress = true;
    }
    public void SetNotGoodsInProgress()
    {
        GoodsInProgress = false;
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }
    public void SetBasePrice(decimal value)
    {
        BasePrice = Guard.Against.Null(value, nameof(value));
    }
    public void SetChangedPrice(decimal value)
    {
        ChangedPrice = Guard.Against.Null(value, nameof(value));
    }

    public void SetIncreaseRate(decimal value)
    {
        IncreaseRate = Guard.Against.Null(value, nameof(value));
    }

    public void SetPlannedStartDate(DateTime? value)
    {
        PlannedStartDate = value;
    }
    public void SetPlannedFinishDate(DateTime? value)
    {
        PlannedFinishDate = value;
    }

    public void SetPlannedDuration(int? value)
    {
        PlannedDuration = value;
    }

    public void SetActualStartDate(DateTime? value)
    {
        ActualStartDate = value;
    }

    public void SetActualFinishDate(DateTime? value)
    {
        ActualFinishDate = value;
    }

    public void SetBaselineStartDate(DateTime? value)
    {
        BaselineStartDate = value;
    }

    public void SetBaselineFinishDate(DateTime? value)
    {
        BaselineFinishDate = value;
    }

    public void SetBaselineDuration(int? value)
    {
        BaselineDuration = value;
    }

    public void SetPlannedDate(DateTime plannedStartDate,
        DateTime plannedFinishDate)
    {
        PlannedStartDate = plannedStartDate;
        PlannedFinishDate = plannedFinishDate;
    }

    public void SetPlannedSchedule(
    DateTime? plannedStartDate,
    DateTime? plannedFinishDate,
    int? plannedDuration)
    {
        if (plannedStartDate.HasValue && plannedDuration.HasValue)
        {
            PlannedStartDate = plannedStartDate;
            PlannedDuration = plannedDuration;
            PlannedFinishDate = plannedStartDate.Value.AddDays(plannedDuration.Value - 1);
        }

        else if (plannedFinishDate.HasValue && plannedDuration.HasValue)
        {
            PlannedFinishDate = plannedFinishDate;
            PlannedDuration = plannedDuration;
            PlannedStartDate = plannedFinishDate.Value.AddDays(-(plannedDuration.Value - 1));
        }

        else if (plannedStartDate.HasValue && plannedFinishDate.HasValue)
        {
            PlannedStartDate = plannedStartDate;
            PlannedFinishDate = plannedFinishDate;
            PlannedDuration =
                (plannedFinishDate.Value - plannedStartDate.Value).Days + 1;
        }
    }
    #endregion

    #region Methods 

    public void AddDocuments(List<string>? urls)
    {
        if (urls != null && urls.Count > 0)
        {
            if (_projectOperationDocuments.Any())
                _projectOperationDocuments.ForEach(c => c.SetIsDeleted());

            foreach (var url in urls)
                _projectOperationDocuments.Add(ProjectOperationDocument.Create(url, this));
        }
        else
        {
            if (_projectOperationDocuments.Any())
                _projectOperationDocuments.ForEach(c => c.SetIsDeleted());
        }
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<ProjectOperationDependency> _predecessorProjectOperationDependency;
    public IReadOnlyList<ProjectOperationDependency> PredecessorProjectOperationDependencies => _predecessorProjectOperationDependency;

    private List<ProjectOperationDependency> _successorProjectOperationDependency;
    public IReadOnlyList<ProjectOperationDependency> SuccessorProjectOperationDependencies => _successorProjectOperationDependency;

    private List<ProjectOperationDetail> _projectOperationDetail;
    public IReadOnlyList<ProjectOperationDetail> ProjectOperationDetails => _projectOperationDetail;

    private List<ContractorContractDetail> _contractorContractDetail;
    public IReadOnlyList<ContractorContractDetail> ContractorContractDetails => _contractorContractDetail;

    private List<FiduciaryProduct> _fiduciaryProduct;
    public IReadOnlyList<FiduciaryProduct> FiduciaryProducts => _fiduciaryProduct;

    private List<RequestGoodsSupply> _requestGoodsSupplies;
    public IReadOnlyList<RequestGoodsSupply> RequestGoodsSupplies => _requestGoodsSupplies;

    private List<EmployerStatusStatementProjectOperation> _employerStatusStatementProjectOperations;
    public IReadOnlyList<EmployerStatusStatementProjectOperation> EmployerStatusStatementProjectOperations => _employerStatusStatementProjectOperations;

    private List<RequestMachineryProjectOperation> _requestMachineryProjectOperations;
    public IReadOnlyList<RequestMachineryProjectOperation> RequestMachineryProjectOperations => _requestMachineryProjectOperations;

    private List<RequestReward> _requestRewards;
    public IReadOnlyList<RequestReward> RequestRewards => _requestRewards;

    private List<ProjectOperationDocument> _projectOperationDocuments;
    public IReadOnlyList<ProjectOperationDocument> ProjectOperationDocuments => _projectOperationDocuments;

    private List<TransportationRequestProjectOperation> _transportationRequestProjectOperation;
    public IReadOnlyList<TransportationRequestProjectOperation> TransportationRequestProjectOperations => _transportationRequestProjectOperation;

    private List<ProjectOperationTemporaryDaily> _projectOperationTemporaryDailies;
    public IReadOnlyList<ProjectOperationTemporaryDaily> ProjectOperationTemporaryDailies => _projectOperationTemporaryDailies;

    private List<ProjectOperationDetailInspection> _projectOperationDetailInspections;
    public IReadOnlyList<ProjectOperationDetailInspection> ProjectOperationDetailInspections => _projectOperationDetailInspections;

    private List<RequestMachineryStatusStatementDetailProjectOperation> _requestMachineryStatusStatementDetailProjectOperations;
    public IReadOnlyList<RequestMachineryStatusStatementDetailProjectOperation> StatusStatementDetailProjectOperations => _requestMachineryStatusStatementDetailProjectOperations;

    private List<EmployerConsiderationDep> _considerationDependency;
    public IReadOnlyList<EmployerConsiderationDep> ConsiderationDependencies => _considerationDependency;

    [Description(EContractCmts.EmployerOperation)]
    private List<EmployerOperation> _employerOperations;
    public IReadOnlyList<EmployerOperation> EmployerOperations => _employerOperations;

    [Description(EContractCmts.ProjectOperationPriceHistory)]
    private List<ProjectOperationHistory> _projectOperationHistory;
    public IReadOnlyList<ProjectOperationHistory> ProjectOperationHistory => _projectOperationHistory;
    [Description(EContractCmts.ProjectOperationPriceHistory)]
    private List<ProjectOperationAction> _projectOperationActions;
    public IReadOnlyList<ProjectOperationAction> ProjectOperationActions => _projectOperationActions;
    private List<ProjectOperationWbs> _projectOperationWbses;
    public IReadOnlyList<ProjectOperationWbs> ProjectOperationWbses => _projectOperationWbses;

    private List<ProjectScheduleTaskOperation> _projectScheduleTaskOperations;
    public IReadOnlyList<ProjectScheduleTaskOperation> ProjectScheduleTaskOperations => _projectScheduleTaskOperations;

    private ProjectOperation()
    {
        _projectScheduleTaskOperations = [];
        _employerOperations = [];
        _considerationDependency = [];
        _projectOperationDetail = [];
        _predecessorProjectOperationDependency = [];
        _successorProjectOperationDependency = [];
        _projectOperationDocuments = [];
        _projectOperationTemporaryDailies = [];
        _contractorContractDetail = [];
        _fiduciaryProduct = [];
        _requestGoodsSupplies = [];
        _employerStatusStatementProjectOperations = [];
        _requestMachineryProjectOperations = [];
        _transportationRequestProjectOperation = [];
        _requestRewards = [];
        _projectOperationDetailInspections = [];
        _requestMachineryStatusStatementDetailProjectOperations = [];
        _projectOperationHistory = [];
        _projectOperationActions = [];
        _projectOperationWbses = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
