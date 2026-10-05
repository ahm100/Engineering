using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Domain.Entities.ProjectOperationDetails;

public class ProjectOperationDetailContractorService : AuditableEntity<ProjectOperationDetailContractorService>
{
    [Description(ProjectDetailCmts.ContractorId)]
    public long? ContractorId { get; private set; }

    [Description(ProjectDetailCmts.Volume)]
    public decimal Volume { get; private set; }

    [Description(ProjectDetailCmts.TimeSpant)]
    public long TimeSpant { get; private set; } = 0;

    [Description(ProjectDetailCmts.RemainingVolume)]
    public decimal RemainingVolume { get; private set; }

    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    [Description(ProjectDetailCmts.Status)]
    public ContractorServiceStatus Status { get; private set; } = ContractorServiceStatus.New;

    [Description(ProjectDetailCmts.Type)]
    public PODContractorServiceType Type { get; private set; } = PODContractorServiceType.ServiceBased;

    [Description(GlobalCmts.ProjectOperationDetail)]
    public long ProjectOperationDetailId { get; set; }
    public ProjectOperationDetail ProjectOperationDetail { get; set; }

    [Description(ProjectDetailCmts.OperationInfoService)]
    public long? OperationInfoServiceId { get; private set; }
    public OperationInfoService? OperationInfoService { get; private set; }

    [Description(ProjectDetailCmts.ProjectServiceDetail)]
    public long? ProjectServiceDetailId { get; set; }
    public ProjectServiceDetail? ProjectServiceDetail { get; set; }

    public ProjectOperationDetailContractorService(
    ProjectOperationDetail projectOperationDetail,
    OperationInfoService? serviceInfo,
    ProjectServiceDetail? projectServiceDetail,
    long? contractorId,
    decimal volume,
    long timeSpant,
    bool isActive,
    PODContractorServiceType type = PODContractorServiceType.ServiceBased)
    : this()
    {
        SetProjectOperationDetail(projectOperationDetail);

        IsActive = isActive;
        SetVolume(volume);
        SetTimeSpant(timeSpant);
        SetRemainingVolume(volume);

        SetType(type);
        SetProjectServiceDetail(projectServiceDetail);

        if (projectServiceDetail is not null)
        {
            SetOperationInfoService(projectServiceDetail.OperationInfoService);
            SetContractorId(projectServiceDetail.ProjectService.ContractorId);
        }
        else
        {
            SetOperationInfoService(serviceInfo);
            SetContractorId(contractorId);
        }

        ValidateOperationInfoService();
    }

    #region Set Date

    private void SetOperationInfoService(OperationInfoService? serviceInfo)
    {
        OperationInfoService = serviceInfo;
        OperationInfoServiceId = serviceInfo?.Id;
    }

    public void SetProjectOperationDetail(ProjectOperationDetail projectOperationDetail)
    {
        ProjectOperationDetail = Guard.Against.Null(projectOperationDetail, nameof(projectOperationDetail));
        ProjectOperationDetailId = Guard.Against.Null(projectOperationDetail.Id, nameof(projectOperationDetail.Id));
    }

    public void SetStatusNew()
    {
        Status = ContractorServiceStatus.New;
        if (ProjectServiceDetail is not null)
            ProjectServiceDetail.ProjectService.SetStatus(ContractorServiceStatus.Contract);
    }

    public void SetStatusContract()
    {
        Status = ContractorServiceStatus.Contract;
        if (ProjectServiceDetail is not null)
            ProjectServiceDetail.ProjectService.SetStatus(ContractorServiceStatus.Contract);
    }

    public void SetServiceInfo(OperationInfoService? value)
    {
        OperationInfoService = value;
        OperationInfoServiceId = value?.Id;

        ValidateOperationInfoService();
    }

    public void SetProjectServiceDetail(ProjectServiceDetail? value)
    {
        ProjectServiceDetail = value;
    }

    public void SetContractorId(long? value)
    {
        ContractorId = value;
    }

    public void SetVolume(decimal value)
    {
        Volume = Guard.Against.Null(value, nameof(value));
    }

    public void SetType(PODContractorServiceType type)
    {
        if (!Enum.IsDefined(type))
            throw new ArgumentOutOfRangeException(nameof(type));

        if (type == PODContractorServiceType.ServiceBased &&
            OperationInfoService is null &&
            Id != default)
        {
            throw new InvalidOperationException(
                "OperationInfoService is required for ServiceBased contractor service.");
        }

        Type = type;
    }

    public void SetActive()
    {
        IsActive = true;
    }

    public void SetInActive()
    {
        IsActive = false;
    }

    public void SetTimeSpant(long timeSpant)
    {
        TimeSpant = timeSpant;
    }

    public void SetRemainingVolume(decimal value)
    {
        RemainingVolume = value;
    }

    public void SubtractRemainingVolume(decimal value)
    {
        RemainingVolume -= value;
    }

    public void AddRemainingVolume(decimal value)
    {
        RemainingVolume += value;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetStatus(ContractorServiceStatus value)
    {
        ArgumentNullException.ThrowIfNull(value);
        Status = value;
    }

    #endregion

    #region Methods 

    private void ValidateOperationInfoService()
    {
        if (Type == PODContractorServiceType.ServiceBased &&
            !OperationInfoServiceId.HasValue)
        {
            throw new InvalidOperationException(
                "OperationInfoService is required for ServiceBased contractor service.");
        }
    }

    #endregion

    /// <summary>
    ///  For EF core, never touch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<DailyProjectOperationService> _dailyOperationServices;
    public IReadOnlyList<ContractorContractDetailService> ContractorContractDetailServices => _contractorContractDetailServices;

    private List<ContractorContractDetailService> _contractorContractDetailServices;
    public IReadOnlyList<DailyProjectOperationService> DailyOperationServices => _dailyOperationServices;

    private List<ProjectOperationDetailContractorExpert> _projectOperationDetailContractorExperts;
    public IReadOnlyList<ProjectOperationDetailContractorExpert> ProjectOperationDetailContractorExperts => _projectOperationDetailContractorExperts;

    private ProjectOperationDetailContractorService()
    {
        _contractorContractDetailServices = [];
        _dailyOperationServices = [];
        _projectOperationDetailContractorExperts = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}