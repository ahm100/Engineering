using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Domain.Entities.RequestContractors;

[Description(GlobalCmts.Histories)]
public class RequestContractorHistory : AuditableEntity<RequestContractorHistory>
{
    #region Properties  

    [Description(GlobalCmts.Status)]
    public RequestContractorStatus Status { get; private set; } = RequestContractorStatus.New;

    [Description(GlobalCmts.Volume)]
    public decimal Volume { get; private set; }

    [Description(RequestContractorCmts.StatusDescription)]
    public string? StatusDescription { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(GlobalCmts.RequestContractor)]
    public long RequestContractorId { get; private set; }
    public RequestContractor RequestContractor { get; private set; }

    #endregion

    #region Constructors

    public RequestContractorHistory(
        RequestContractorStatus status,
        decimal volume,
        string? statusDescription,
        string? description,
        RequestContractor requestContractor) : this()
    {
        SetRequestContractor(requestContractor);
        SetStatus(status);
        SetDescription(description);
        SetVolume(volume);
        SetStatusDescription(statusDescription);
        SetRequestContractor(requestContractor);
    }

    #endregion

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetStatus(RequestContractorStatus value)
    {
        Status = Guard.Against.Null(value, nameof(value));
    }

    public void SetStatusDescription(string? value)
    {
        StatusDescription = value;
    }

    public void SetVolume(decimal value)
    {
        Volume = Guard.Against.Null(value, nameof(value));
    }

    public void SetRequestContractor(RequestContractor value)
    {
        RequestContractor = Guard.Against.Null(value, nameof(value));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestContractorHistory() { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
