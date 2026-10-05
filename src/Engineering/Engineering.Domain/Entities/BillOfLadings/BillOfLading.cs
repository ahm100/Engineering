using Engineering.Domain.Entities.Transportations;

namespace Engineering.Domain.Entities.BillOfLadings;

[Description(BillOfLadingCmts.BillOfLading)]
public class BillOfLading : ActivateEntity<BillOfLading, long>
{
    [Description(BillOfLadingCmts.Name)]
    public string BillOfLadingName { get; private set; } = string.Empty;
    [Description(BillOfLadingCmts.Code)]
    public string BillOfLadingCode { get; private set; } = string.Empty;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    public BillOfLading(
        string name,
        string code,
        bool isActive,
        long? companyId) : this()
    {
        BillOfLadingName = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        BillOfLadingCode = Guard.Against.NullOrWhiteSpace(code, nameof(code));
        CompanyId = companyId;
        IsActive = Guard.Against.Null(isActive, nameof(isActive));
    }

    public void SetName(string value)
    {
        BillOfLadingName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetCode(string value)
    {
        BillOfLadingCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<TransportationRequest> _transportationRequest;
    public IReadOnlyList<TransportationRequest> TransportationRequests => _transportationRequest;
    private BillOfLading()
    {
        _transportationRequest = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
