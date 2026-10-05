
namespace Engineering.Domain.Entities.OperationInfos;

[Description(OperationInfoCmts.OperationInfoGroup)]
public class OperationInfoGroup : AuditableEntity<OperationInfoGroup>
{

    [Description(GlobalCmts.Code)]
    public string OperationInfoGroupCode { get; private set; } = string.Empty;

    [Description(GlobalCmts.Title)]
    public string OperationInfoGroupTitle { get; private set; } = string.Empty;

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; }

    public OperationInfoGroup(string title,
        string code,
        bool isActive,
        long? companyId) : this()
    {
        SetCode(code);
        SetName(title);
        SetCompanyId(companyId);
        SetActive();
    }

    #region Set data

    public void SetName(string value)
    {
        OperationInfoGroupTitle = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCode(string value)
    {
        OperationInfoGroupCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCompanyId(long? value)
    {
        CompanyId = value;
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

    #endregion

    #region Methods 

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<OperationInfoGroupRelation> _operationInfoGroupRelations;
    public IReadOnlyList<OperationInfoGroupRelation> OperationInfoGroupRelations => _operationInfoGroupRelations.AsReadOnly();
    private OperationInfoGroup()
    {
        _operationInfoGroupRelations = new List<OperationInfoGroupRelation>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
