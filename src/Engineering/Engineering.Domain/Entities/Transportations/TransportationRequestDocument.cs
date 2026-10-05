namespace Engineering.Domain.Entities.Transportations;

[Description(GlobalCmts.Document)]
public class TransportationRequestDocument : AuditableEntity<TransportationRequestDocument>
{
    #region Properties

    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(TransportationCmts.IsBill)]
    public bool IsBill { get; private set; } = false;

    [Description(TransportationCmts.TransportationRequest)]
    public long TransportationRequestId { get; private set; }
    public TransportationRequest TransportationRequest { get; private set; }

    #endregion

    public TransportationRequestDocument(string url,
        bool isBill,
        TransportationRequest transportationRequest) : this()
    {
        SetUrl(url);
        SetIsBill(isBill);
        SetTransportationRequest(transportationRequest);
    }

    #region Commands

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetTransportationRequest(TransportationRequest? value)
    {
        TransportationRequest = Guard.Against.Null(value, nameof(value));
        TransportationRequestId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetUrl(string? value)
    {
        Url = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsBill(bool value)
    {
        IsBill = Guard.Against.Null(value, nameof(value));
    }
    #endregion
    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TransportationRequestDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    #endregion
}