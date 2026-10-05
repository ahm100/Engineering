
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Domain.Entities.Transportations;

[Description(TransportationCmts.Transportation)]
public class Transportation : AuditableEntity<Transportation>
{
    [Description(TransportationCmts.TransportationName)]
    public string TransportationName { get; private set; } = string.Empty;

    [Description(TransportationCmts.TransportationCode)]
    public string TransportationCode { get; private set; } = string.Empty;

    [Description(TransportationCmts.IsPassenger)]
    public bool IsPassenger { get; private set; } = true;

    [Description(TransportationCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    [Description(TransportationCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(TransportationCmts.IsLock)]
    public bool? IsLock { get; set; } = false;

    [Description(TransportationCmts.TransportationType)]
    public TransportationType? TransportationType { get; private set; }

    public Transportation(string name,
        string code,
        bool isPassenger,
        bool isActive,
        long? companyId,
        TransportationType? transportationType) : this()
    {
        SetName(name);
        SetCode(code);
        SetIsPassenger(isPassenger);
        IsActive = Guard.Against.Null(isActive, nameof(isActive));
        SetCompanyId(companyId);
        SetTransportationType(transportationType);
    }

    #region Set data

    public void SetName(string value)
    {
        TransportationName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCode(string value)
    {
        TransportationCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetIsPassenger(bool value)
    {
        IsPassenger = Guard.Against.Null(value, nameof(value));
    }
    public void SetCompanyId(long? value)
    {
        CompanyId = value;
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
    public void SetTransportationType(TransportationType? transportationType)
    {
        TransportationType = transportationType;
    }

    #endregion

    #region Methods 

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<TransportationRequest> _transportationRequest;
    public IReadOnlyList<TransportationRequest> TransportationRequests => _transportationRequest;

    private Transportation()
    {
        _transportationRequest = new List<TransportationRequest>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
