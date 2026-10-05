namespace Engineering.Domain.Entities.Transportations;

[Description(GlobalCmts.Document)]
public class TransportationCargoDocument : AuditableEntity<TransportationCargoDocument>
{
    #region Properties

    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(TransportationCmts.TransportationRequest)]
    public long TransportationCargoId { get; private set; }
    public TransportationCargo TransportationCargo { get; private set; }

    #endregion

    public TransportationCargoDocument(string url,
        TransportationCargo cargo) : this()
    {
        SetUrl(url);
        SetTransportationCargo(cargo);
    }

    #region Commands

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetTransportationCargo(TransportationCargo? value)
    {
        TransportationCargo = Guard.Against.Null(value, nameof(value));
        TransportationCargoId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetUrl(string? value)
    {
        Url = Guard.Against.Null(value, nameof(value));
    }

    #endregion
    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TransportationCargoDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    #endregion
}