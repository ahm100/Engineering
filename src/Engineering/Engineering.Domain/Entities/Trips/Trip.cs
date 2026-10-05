using Engineering.Domain.Entities.Transportations;

namespace Engineering.Domain.Entities.Trips;

[Description(TripCmts.Trip)]
public class Trip : AuditableEntity<Trip>
{
    [Description(TripCmts.TripName)]
    public string TripName { get; private set; } = string.Empty;

    [Description(TripCmts.TripCode)]
    public string TripCode { get; private set; } = string.Empty;

    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(TripCmts.IsLock)]
    public bool? IsLock { get; set; } = false;

    public Trip(string name,
        string code,
        bool isActive,
        long? companyId) : this()
    {
        SetName(name);
        SetCode(code);
        IsActive = Guard.Against.Null(isActive, nameof(isActive));
        SetCompanyId(companyId);
    }

    #region Set data

    public void SetName(string value)
    {
        TripName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCode(string value)
    {
        TripCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
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

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<TransportationRequest> _transportationRequest;
    public IReadOnlyList<TransportationRequest> TransportationRequests => _transportationRequest;
    private Trip()
    {
        _transportationRequest = new List<TransportationRequest>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
