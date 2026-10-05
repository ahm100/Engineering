using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.ServiceInfos;

namespace Engineering.Domain.Entities.OperationInfos;

[Description(OperationInfoCmts.OperationInfoService)]
public class OperationInfoService : AuditableEntity<OperationInfo>
{
    [Description(OperationInfoCmts.TimeSpant)]
    public long TimeSpant { get; private set; } = 0;
    [Description(GlobalCmts.OperationInfo)]
    public long OperationInfoId { get; set; }
    public OperationInfo OperationInfo { get; set; }
    [Description(GlobalCmts.Service)]
    public long ServiceInfoId { get; set; }
    public ServiceInfo ServiceInfo { get; set; }



    public OperationInfoService(OperationInfo operationInfo,
        ServiceInfo serviceInfo,
        long timeSpant) : this()
    {
        SetOperationInfo(operationInfo);
        SetServiceInfo(serviceInfo);
        SetTimeSpant(timeSpant);
    }

    #region Set data 

    public void SetOperationInfo(OperationInfo value)
    {
        OperationInfo = Guard.Against.Null(value, nameof(value));
        OperationInfoId = Guard.Against.Null(value.Id, nameof(value.Id));

    }

    public void SetServiceInfo(ServiceInfo value)
    {
        ServiceInfo = Guard.Against.Null(value, nameof(value));
        ServiceInfoId = Guard.Against.Null(value.Id, nameof(value.Id));

    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetTimeSpant(long timeSpant)
    {
        TimeSpant = timeSpant;
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<ProjectOperationDetailContractorService> _contractorServices;
    public IReadOnlyList<ProjectOperationDetailContractorService> ProjectOperationDetailContractorServices => _contractorServices;
    private List<ProjectServiceDetail> _projectServiceDetails;
    public IReadOnlyList<ProjectServiceDetail> ProjectServiceDetails => _projectServiceDetails;
    private List<EmployerOperationService> _employerOperationServices;
    public IReadOnlyList<EmployerOperationService> EmployerOperationServices => _employerOperationServices;
    private OperationInfoService()
    {
        _contractorServices = new List<ProjectOperationDetailContractorService>();
        _projectServiceDetails = new List<ProjectServiceDetail>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
