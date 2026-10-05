using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProcesVerbal;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails.Users;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.RequestContractors;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestRewards;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Domain.Entities.ProjectOperationDetails;

[Description(GlobalCmts.ProjectOperationDetail)]
public class ProjectOperationDetail : AuditableEntity<ProjectOperationDetail>
{
    [Description(GlobalCmts.Code)]
    public string Code { get; private set; } = string.Empty;

    [Description(GlobalCmts.StartDate)]
    public DateTime? StartDate { get; private set; }

    [Description(GlobalCmts.EndDate)]
    public DateTime? EndDate { get; private set; }

    [Description(ProjectDetailCmts.Length)]
    public decimal Length { get; private set; } = 1;

    [Description(ProjectDetailCmts.LengthChangeable)]
    public bool LengthChangeable { get; private set; } = true;

    [Description(ProjectDetailCmts.Width)]
    public decimal Width { get; private set; } = 1;

    [Description(ProjectDetailCmts.WidthChangeable)]
    public bool WidthChangeable { get; private set; } = true;

    [Description(ProjectDetailCmts.Height)]
    public decimal Height { get; private set; } = 1;

    [Description(ProjectDetailCmts.HeightChangeable)]
    public bool HeightChangeable { get; private set; } = true;

    [Description(ProjectDetailCmts.Weight)]
    public decimal Weight { get; private set; } = 1;

    [Description(ProjectDetailCmts.WeightChangeable)]
    public bool WeightChangeable { get; private set; } = true;

    [Description(ProjectDetailCmts.Number)]
    public decimal Number { get; private set; } = 1;

    [Description(ProjectDetailCmts.NumberChangeable)]
    public bool NumberChangeable { get; private set; } = true;

    [Description(ProjectDetailCmts.Priority)]
    public int Priority { get; private set; }

    [Description(ProjectDetailCmts.Day)]
    public int Day { get; private set; }

    [Description(ProjectDetailCmts.Hour)]
    public int Hour { get; private set; }

    [Description(ProjectDetailCmts.CreatedProductId)]
    public long? CreatedProductId { get; private set; }

    [Description(ProjectDetailCmts.FinalAmount)]
    public decimal FinalAmount { get; private set; }

    [Description(ProjectDetailCmts.Status)]
    public ProjectOperationDetailStatus? Status { get; private set; } = ProjectOperationDetailStatus.NotStarted;

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; } = string.Empty;

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(GlobalCmts.ProjectOperation)]
    public long ProjectOperationId { get; private set; }
    public ProjectOperation ProjectOperation { get; private set; }

    [Description(GlobalCmts.OperationLocation)]
    public long OperationLocationId { get; private set; }
    public OperationLocation OperationLocation { get; private set; }

    public ProjectOperationDetail(
        ProjectOperation projectOperation,
        OperationLocation operationLocation,
        string code,
        DateTime? startDate,
        DateTime? endDate,
        decimal length,
        bool lengthChangeable,
        decimal width,
        bool widthChangeable,
        decimal height,
        bool heightChangeable,
        decimal weight,
        bool weightChangeable,
        decimal number,
        bool numberChangeable,
        ProjectOperationDetailStatus? status,
        int priority,
        int day,
        int hour,
        long? createdProductId,
        string? description,
        List<string>? documentUrls,
        long? companyId) : this()
    {
        SetProjectOperation(projectOperation);
        SetOperationLocation(operationLocation);
        SetCode(code);
        SetPriority(priority);
        SetDay(day);
        SetHour(hour);
        SetLength(length);
        SetLengthChangeable(lengthChangeable);
        SetWidth(width);
        SetWidthChangeable(widthChangeable);
        SetHeight(height);
        SetHeightChangeable(heightChangeable);
        SetWeight(weight);
        SetWeightChangeable(weightChangeable);
        SetNumber(number);
        SetNumberChangeable(numberChangeable);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetCompanyId(companyId);
        SetDescription(description);
        SetCreatedProductId(createdProductId);

        SetFinalAmount();

        if (documentUrls != null && documentUrls.Any())
            AddDocuments(documentUrls);

        AddHistory();
    }

    public static ProjectOperationDetail Create(
        ProjectOperation projectOperation,
        OperationLocation operationLocation,
        string code,
        DateTime? startDate,
        DateTime? endDate,
        decimal length,
        bool lengthChangeable,
        decimal width,
        bool widthChangeable,
        decimal height,
        bool heightChangeable,
        decimal weight,
        bool weightChangeable,
        decimal number,
        bool numberChangeable,
        ProjectOperationDetailStatus? status,
        int priority,
        int day,
        int hour,
        long? createdProductId,
        string? description,
        List<string>? documentUrls,
        long? companyId)
    {
        return new ProjectOperationDetail(
            projectOperation,
            operationLocation,
            code,
            startDate,
            endDate,
            length,
            lengthChangeable,
            width,
            widthChangeable,
            height,
            heightChangeable,
            weight,
            weightChangeable,
            number,
            numberChangeable,
            status,
            priority,
            day,
            hour,
            createdProductId,
            description,
            documentUrls,
            companyId);
    }

    #region Set data

    public void SetCode(string value)
    {
        Code = value;
    }
    public void SetCreatedProductId(long? createdProductId)
    {
        CreatedProductId = createdProductId;
    }
    public void SetStartDate(DateTime? value)
    {
        StartDate = value;
    }
    public void SetEndDate(DateTime? value)
    {
        EndDate = value;
    }
    public void SetLength(decimal value)
    {
        Length = Guard.Against.Null(value, nameof(value));
    }
    public void SetLengthChangeable(bool value)
    {
        LengthChangeable = Guard.Against.Null(value, nameof(value));
    }
    public void SetWidth(decimal value)
    {
        Width = Guard.Against.Null(value, nameof(value));
    }
    public void SetWidthChangeable(bool value)
    {
        WidthChangeable = Guard.Against.Null(value, nameof(value));
    }
    public void SetHeight(decimal value)
    {
        Height = Guard.Against.Null(value, nameof(value));
    }
    public void SetHeightChangeable(bool value)
    {
        HeightChangeable = Guard.Against.Null(value, nameof(value));
    }
    public void SetWeight(decimal value)
    {
        Weight = Guard.Against.Null(value, nameof(value));
    }
    public void SetWeightChangeable(bool value)
    {
        WeightChangeable = Guard.Against.Null(value, nameof(value));
    }
    public void SetNumber(decimal value)
    {
        Number = Guard.Against.Null(value, nameof(value));
    }
    public void SetNumberChangeable(bool value)
    {
        NumberChangeable = Guard.Against.Null(value, nameof(value));
    }
    public void SetStatus(ProjectOperationDetailStatus value)
    {
        Status = value;
    }
    public void SetPriority(int value)
    {
        Priority = Guard.Against.Null(value, nameof(value));
    }
    public void SetDay(int? value)
    {
        Day = Guard.Against.Null(value, nameof(value));
    }
    public void SetHour(int? value)
    {
        Hour = Guard.Against.Null(value, nameof(value));
    }
    public void SetDescription(string? value)
    {
        Description = value;
    }
    public void SetOperationLocation(OperationLocation value)
    {
        OperationLocation = Guard.Against.Null(value, nameof(value));
        OperationLocationId = Guard.Against.Null(value.Id, nameof(value));
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }
    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    public void SetProjectOperation(ProjectOperation value)
    {
        ProjectOperation = Guard.Against.Null(value, nameof(value));
        ProjectOperationId = Guard.Against.Null(value.Id, nameof(value));
    }
    public void SetFinalAmount()
    {
        FinalAmount = Length * Width * Height * Weight * Number;
    }
    #endregion

    #region Methods 

    public void AddDailyProjectOperation()
    {
        var newData = new DailyProjectOperation(Status!.Value, DateTime.Now, DateTime.Now, 0, 0, 0, 0, 0, this, Description, null, CompanyId);
        ArgumentNullException.ThrowIfNull(newData);
        _dailyOperations.Add(newData);
    }
    public void AddImplementationAssistant(UserImplementation newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_userImplementations.Any(oo => oo.ImplementationAssistantUserId == newData.ImplementationAssistantUserId && oo.Created == newData.Created))
            return;

        _userImplementations.Add(newData);
    }
    public void AddPlaner(UserPlaner newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_userPlaners.Any(oo => oo.UserPlanerId == newData.UserPlanerId && oo.Created == newData.Created))
            return;

        _userPlaners.Add(newData);
    }
    public void AddTechnicalAssistant(UserTechnical newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_userTechnicals.Any(oo => oo.TechnicalAssistantUserId == newData.TechnicalAssistantUserId && oo.Created == newData.Created))
            return;

        _userTechnicals.Add(newData);
    }
    public void AddServiceInfo(ProjectOperationDetailContractorService newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_contractorServices.Any(oo => oo.ContractorId == newData.ContractorId && oo.OperationInfoService.ServiceInfo == newData.OperationInfoService.ServiceInfo && oo.Created == newData.Created))
            return;

        _contractorServices.Add(newData);
    }

    public void AddExpert(ConsumableVolumeExpert newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_consumableVolumeExperts.Any(oo => oo.ExpertId == newData.ExpertId && oo.Created == newData.Created))
            return;

        _consumableVolumeExperts.Add(newData);
    }
    public void AddMachinery(ConsumableVolumeMachinery newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_consumableVolumeMachineries.Any(oo => oo.Machinery.Id == newData.Machinery.Id && oo.Created == newData.Created))
            return;

        _consumableVolumeMachineries.Add(newData);
    }
    public void AddProduct(ConsumableVolumeProduct newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_consumableVolumeProducts.Any(oo => oo.ProductGroupId == newData.ProductGroupId && oo.Created == newData.Created))
            return;

        _consumableVolumeProducts.Add(newData);
    }
    public void AddDeduction(ProjectOperationDetailDeduction newData)
    {
        ArgumentNullException.ThrowIfNull(newData);

        _projectOperationDetailDeduction.Add(newData);
    }

    public void AddDocuments(List<string>? urls)
    {
        if (urls != null && urls.Count > 0)
        {
            if (_projectOperationDetailDocuments.Any())
                _projectOperationDetailDocuments.ForEach(c => c.SetIsDeleted());

            foreach (var url in urls)
                _projectOperationDetailDocuments.Add(ProjectOperationDetailDocument.Create(url, this));
        }
        else
        {
            if (_projectOperationDetailDocuments.Any())
                _projectOperationDetailDocuments.ForEach(c => c.SetIsDeleted());
        }
    }

    public void AddHistory()
    {
        _projectOperationDetailHistories.Add(ProjectOperationDetailHistory.Create(this,
            this.Code, this.StartDate, this.EndDate, this.Length, this.Width, this.Height,
            this.Weight, this.Number, this.FinalAmount, this.Status, this.Priority, this.Day,
            this.Hour, this.Description, null));
    }

    public void AddHistory(string? statusDescription)
    {
        _projectOperationDetailHistories.Add(ProjectOperationDetailHistory.Create(this,
            this.Code, this.StartDate, this.EndDate, this.Length, this.Width, this.Height,
            this.Weight, this.Number, this.FinalAmount, this.Status, this.Priority, this.Day,
            this.Hour, this.Description, statusDescription));
    }
    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Description(EContractCmts.ConsumableVolumeExpert)]
    private List<ConsumableVolumeExpert> _consumableVolumeExperts;
    public IReadOnlyList<ConsumableVolumeExpert> ConsumableVolumeExperts => _consumableVolumeExperts;

    [Description(EContractCmts.ConsumableVolumeMachinery)]
    private List<ConsumableVolumeMachinery> _consumableVolumeMachineries;
    public IReadOnlyList<ConsumableVolumeMachinery> ConsumableVolumeMachineries => _consumableVolumeMachineries;

    [Description(EContractCmts.ConsumableVolumeProduct)]
    private List<ConsumableVolumeProduct> _consumableVolumeProducts;
    public IReadOnlyList<ConsumableVolumeProduct> ConsumableVolumeProducts => _consumableVolumeProducts;

    [Description(EContractCmts.UserImplementation)]
    private List<UserImplementation> _userImplementations;
    public IReadOnlyList<UserImplementation> UserImplementations => _userImplementations;

    [Description(EContractCmts.UserPlaner)]
    private List<UserPlaner> _userPlaners;
    public IReadOnlyList<UserPlaner> UserPlaners => _userPlaners;

    [Description(EContractCmts.UserTechnical)]
    private List<UserTechnical> _userTechnicals;
    public IReadOnlyList<UserTechnical> UserTechnicals => _userTechnicals;

    [Description(EContractCmts.ProjectOperationDetailContractorService)]
    private List<ProjectOperationDetailContractorService> _contractorServices;
    public IReadOnlyList<ProjectOperationDetailContractorService> ProjectOperationDetailContractorServices => _contractorServices;

    [Description(EContractCmts.DailyProjectOperation)]
    private List<DailyProjectOperation> _dailyOperations;
    public IReadOnlyList<DailyProjectOperation> DailyOperations => _dailyOperations;

    [Description(EContractCmts.RequestReward)]
    private List<RequestReward> _requestRewards;
    public IReadOnlyList<RequestReward> RequestRewards => _requestRewards;


    [Description(EContractCmts.EmployerStatusStatementProjectOperationDetail)]
    private List<EmployerStatusStatementProjectOperationDetail> _employerStatusStatementProjectOperationDetails;
    public IReadOnlyList<EmployerStatusStatementProjectOperationDetail> EmployerStatusStatementProjectOperationDetails => _employerStatusStatementProjectOperationDetails;

    [Description(EContractCmts.RequestMachineryProjectOperationDetail)]
    private List<RequestMachineryProjectOperationDetail> _requestMachineryProjectOperationDetails;
    public IReadOnlyList<RequestMachineryProjectOperationDetail> RequestMachineryProjectOperationDetails => _requestMachineryProjectOperationDetails;

    [Description(EContractCmts.TransportationRequestProjectOperationDetail)]
    private List<TransportationRequestProjectOperationDetail> _transportationRequestProjectOperationDetail;
    public IReadOnlyList<TransportationRequestProjectOperationDetail> TransportationRequestProjectOperationDetails => _transportationRequestProjectOperationDetail;

    [Description(EContractCmts.RequestGoodsSupply)]
    private List<RequestGoodsSupply> _requestGoodsSupplies;
    public IReadOnlyList<RequestGoodsSupply> RequestGoodsSupplies => _requestGoodsSupplies;

    [Description(EContractCmts.ProjectOperationDetailDocument)]
    private List<ProjectOperationDetailDocument> _projectOperationDetailDocuments;
    public IReadOnlyList<ProjectOperationDetailDocument> ProjectOperationDetailDocuments => _projectOperationDetailDocuments;

    [Description(EContractCmts.ProjectOperationDetailDeduction)]
    private List<ProjectOperationDetailDeduction> _projectOperationDetailDeduction;
    public IReadOnlyList<ProjectOperationDetailDeduction> ProjectOperationDetailDeductions => _projectOperationDetailDeduction;

    [Description(EContractCmts.ProjectOperationDetailHistory)]
    private List<ProjectOperationDetailHistory> _projectOperationDetailHistories;
    public IReadOnlyList<ProjectOperationDetailHistory> projectOperationDetailHistories => _projectOperationDetailHistories;

    [Description(EContractCmts.ProjectOperationDetailInspection)]
    private List<ProjectOperationDetailInspection> _projectOperationDetailInspections;
    public IReadOnlyList<ProjectOperationDetailInspection> ProjectOperationDetailInspections => _projectOperationDetailInspections;

    [Description(EContractCmts.RequestMachineryStatusStatementDetailProjectOperationDetail)]
    private List<RequestMachineryStatusStatementDetailProjectOperationDetail> _requestMachineryStatusStatementDetailProjectOperationDetails;
    public IReadOnlyList<RequestMachineryStatusStatementDetailProjectOperationDetail> StatusStatementDetailProjectOperationDetails => _requestMachineryStatusStatementDetailProjectOperationDetails;

    [Description(EContractCmts.RequestContractor)]
    private List<RequestContractor> _requestContractors;
    public IReadOnlyList<RequestContractor> RequestContractors => _requestContractors;

    [Description(EContractCmts.EmployerOperationDetail)]
    private List<EmployerOperationDetail> _EmployerOperationDetails;
    public IReadOnlyList<EmployerOperationDetail> EmployerOperationDetails => _EmployerOperationDetails;

    [Description(ProcesVerbalCmts.ProcesVerbalPOD)]
    private List<ProcesVerbalPOD> _procesVerbalPODs;
    public IReadOnlyList<ProcesVerbalPOD> ProcesVerbalPODs => _procesVerbalPODs;

    private ProjectOperationDetail()
    {
        _dailyOperations = [];
        _requestRewards = [];
        _consumableVolumeExperts = [];
        _EmployerOperationDetails = [];
        _projectOperationDetailInspections = [];
        _requestMachineryStatusStatementDetailProjectOperationDetails = [];
        _requestContractors = [];
        _requestMachineryProjectOperationDetails = [];

        _consumableVolumeMachineries = [];
        _consumableVolumeProducts = [];
        _userImplementations = [];
        _userPlaners = [];
        _userTechnicals = [];
        _contractorServices = [];
        _employerStatusStatementProjectOperationDetails = [];
        _transportationRequestProjectOperationDetail = [];
        _requestMachineryProjectOperationDetails = [];
        _requestGoodsSupplies = [];
        _projectOperationDetailDocuments = [];
        _projectOperationDetailDeduction = [];
        _projectOperationDetailHistories = [];

        _procesVerbalPODs = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
