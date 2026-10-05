namespace Engineering.Domain.Entities.Logistics;

public class TransportationContractorDocument : ActivateEntity<TransportationContractorDocument, long>
{
    [Description(GlobalCmts.Url)]
    public string DocumentUrl { get; private set; }
    [Description(TransportationContractorCmts.TransportationContractor)]
    public long TransportationContractorId { get; set; }
    public TransportationContractor TransportationContractor { get; set; }

    public TransportationContractorDocument(
        string documentUrl,
        TransportationContractor transportationContractor)
    {
        DocumentUrl = Guard.Against.NullOrEmpty(documentUrl, nameof(documentUrl));
        TransportationContractor = Guard.Against.Null(transportationContractor, nameof(transportationContractor));
        TransportationContractorId = transportationContractor.Id;
    }

    public void SetDocumentUrl(string value)
    {
        DocumentUrl = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetTransportationContractor(TransportationContractor value)
    {
        TransportationContractor = Guard.Against.Null(value, nameof(value));
        TransportationContractorId = value.Id;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private TransportationContractorDocument()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}

