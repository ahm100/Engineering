namespace Engineering.Domain.Entities.MachineTypes;

[Description(MachineTypeCmts.CabinType)]
public class CabinType : AuditableEntity<CabinType>
{

    [Description(GlobalCmts.Code)]
    public int CabinTypeCode { get; private set; }
    [Description(GlobalCmts.Title)]
    public string CabinTypeName { get; private set; } = string.Empty;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }
    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; }

    public CabinType(string name,
        int code,
        bool isActive,
        long? companyId) : this()
    {
        SetCode(code);
        SetName(name);
        IsActive = isActive;
        SetCompanyId(companyId);
    }

    #region Set data

    public void SetName(string value)
    {
        CabinTypeName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCode(int value)
    {
        CabinTypeCode = Guard.Against.Null(value, nameof(value));
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
    private List<MachineType> _machineTypes;
    public IReadOnlyList<MachineType> MachineTypes => _machineTypes.AsReadOnly();

    private CabinType()
    {
        _machineTypes = new List<MachineType>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
