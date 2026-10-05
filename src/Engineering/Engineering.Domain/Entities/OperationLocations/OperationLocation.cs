using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Domain.Entities.OperationLocations;

[Description(OperationLocationCmts.OperationLocation)]
public class OperationLocation : AuditableEntity<OperationLocation>
{
    [Description(OperationLocationCmts.PrivateName)]
    public string PrivateName { get; private set; } = string.Empty;

    [Description(OperationLocationCmts.PrivateCode)]
    public string PrivateCode { get; private set; } = string.Empty;

    [Description(OperationLocationCmts.PublicName)]
    public string PublicName { get; private set; } = string.Empty;

    [Description(OperationLocationCmts.PublicCode)]
    public string PublicCode { get; private set; } = string.Empty;

    [Description(OperationLocationCmts.Coding)]
    public string Coding { get; private set; } = string.Empty;

    [Description(OperationLocationCmts.Path)]
    public string Path { get; private set; } = string.Empty;

    [Description(OperationLocationCmts.Priority)]
    public int? Priority { get; private set; } = 1;

    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(GlobalCmts.CostCenter)]
    public long? CostCenterId { get; set; }
    public CostCenter? CostCenter { get; set; }

    [Description(GlobalCmts.Project)]
    public long? ProjectId { get; set; }
    public Project? Project { get; set; }

    [Description(OperationLocationCmts.Parent)]
    public OperationLocation? Parent { get; set; }
    public long? ParentId { get; private set; }

    public OperationLocation(
        CostCenter? costCenter,
        Project? project,
        OperationLocation? parent,
        string privateName,
        string privateCode,
        string publicName,
        string publicCode,
        string coding,
        string path,
        int? priority,
        bool isActive,
        long? companyId) : this()
    {
        SetCostCenter(costCenter);
        SetProject(project);
        SetParent(parent);
        SetPrivateName(privateName);
        SetPrivateCode(privateCode);
        SetPublicName(publicName);
        SetPublicCode(publicCode);
        SetCoding(coding);
        SetPath(path);
        SetCompanyId(companyId);
        SetPriority(priority);
        IsActive = Guard.Against.Null(isActive, nameof(isActive));
    }

    #region Set data

    public void SetCostCenter(CostCenter? value)
    {
        CostCenter = value;
        CostCenterId = value?.Id;
    }
    public void SetProject(Project? value)
    {
        Project = value;
        ProjectId = value?.Id;
    }
    public void SetPrivateName(string value)
    {
        PrivateName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetPrivateCode(string value)
    {
        PrivateCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetPublicName(string value)
    {
        PublicName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetPublicCode(string value)
    {
        PublicCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCoding(string value)
    {
        Coding = value;
    }
    public void SetPath(string value)
    {
        Path = value;
    }
    public void SetParent(OperationLocation? value)
    {
        Parent = value;
        ParentId = value?.Id;
    }
    public void SetPriority(int? value)
    {
        Priority = value ?? 1;
    }
    public void SetActive()
    {
        IsActive = true;
    }
    public void SetInActive()
    {
        IsActive = false;
    }
    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<OperationLocation> _operationLocations;
    public IReadOnlyList<OperationLocation> Children => _operationLocations;

    private List<ProjectOperationDetail> _projectOperationDetail;
    public IReadOnlyList<ProjectOperationDetail> ProjectOperationDetails => _projectOperationDetail;

    private List<ProjectOperationDetailInspection> _projectOperationDetailInspections;
    public IReadOnlyList<ProjectOperationDetailInspection> ProjectOperationDetailInspections => _projectOperationDetailInspections;
    private OperationLocation()
    {
        _operationLocations = new List<OperationLocation>();
        _projectOperationDetail = new List<ProjectOperationDetail>();
        _projectOperationDetailInspections = new List<ProjectOperationDetailInspection>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
