namespace Engineering.Domain.Entities.RequestMachineries;

[Description(GlobalCmts.Document)]
public class RequestMachineryBillDocument : AuditableEntity<RequestMachineryBillDocument>
{
    #region Properties

    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(GlobalCmts.RequestMachinery)]
    public long RequestMachineryId { get; private set; }
    public RequestMachinery RequestMachinery { get; private set; }

    #endregion

    public RequestMachineryBillDocument(RequestMachinery requestMachinery,
        string url) : this()
    {
        SetRequestMachinery(requestMachinery);
        SetUrl(url);
    }

    #region Commands

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetUrl(string url)
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(url));
    }

    public void SetRequestMachinery(RequestMachinery value)
    {
        RequestMachinery = Guard.Against.Null(value, nameof(value));
        RequestMachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestMachineryBillDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion
}
