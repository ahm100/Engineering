using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ServiceInfos;

namespace Engineering.Domain.Entities.Projects;

[Description(ProjectCmts.ProjectService)]
public class ProjectService : AuditableEntity<ProjectService>
{
    [Description(GlobalCmts.ContractorId)]
    public long ContractorId { get; private set; }
    [Description(GlobalCmts.Volume)]
    public decimal Volume { get; private set; }
    [Description(ProjectCmts.DoneVolume)]
    public decimal DoneVolume { get; private set; } = 0;
    [Description(ProjectCmts.RemainderVolume)]
    public decimal RemainderVolume { get; private set; } = 0;
    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; }
    [Description(GlobalCmts.Status)]
    public ContractorServiceStatus Status { get; private set; } = ContractorServiceStatus.New;

    [Description(GlobalCmts.Project)]
    public long ProjectId { get; set; }
    public Project Project { get; set; }
    [Description(GlobalCmts.ServiceInfo)]
    public long ServiceInfoId { get; set; }
    public ServiceInfo ServiceInfo { get; set; }

    public ProjectService(
        Project project,
        ServiceInfo serviceInfo,
        long contractorId,
        decimal volume,
        decimal doneVolume,
        bool isActive) : this()
    {
        SetProject(project);
        SetServiceInfo(serviceInfo);
        SetContractorId(contractorId);
        SetVolume(volume);
        SetDoneVolume(doneVolume);
        SetRemainderVolume(volume - doneVolume);
        IsActive = isActive;
    }

    #region Set data

    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetServiceInfo(ServiceInfo value)
    {
        ServiceInfo = Guard.Against.Null(value, nameof(value));
        ServiceInfoId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetDoneVolume(decimal value)
    {
        DoneVolume = value;
    }
    public void SetVolume(decimal value)
    {
        Volume = value;
    }
    public void SetRemainderVolume()
    {
        RemainderVolume = Volume - DoneVolume;
    }
    public void SetRemainderVolume(decimal value)
    {
        RemainderVolume = value;
    }
    public void SetContractorId(long value)
    {
        ContractorId = value;
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }
    public void SetActive()
    {
        IsActive = true;
    }
    public void SetInActive()
    {
        IsActive = false;
    }

    public void SetStatus(ContractorServiceStatus value)
    {
        ArgumentNullException.ThrowIfNull(value);
        Status = value;
    }

    #endregion

    #region Methods 

    public void AddOperationInfoService(ProjectServiceDetail newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_projectServiceDetails.Any(oo => oo.OperationInfoService == newData.OperationInfoService && oo.Created == newData.Created))
            return;

        _projectServiceDetails.Add(newData);
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<ProjectServiceDetail> _projectServiceDetails;
    public IReadOnlyList<ProjectServiceDetail> ProjectServiceDetails => _projectServiceDetails;
    private ProjectService()
    {
        _projectServiceDetails = new List<ProjectServiceDetail>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
