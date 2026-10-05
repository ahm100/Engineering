namespace Engineering.Domain.Entities.RequestMachineries;

[Description(GlobalCmts.Document)]
public class RequestMachineryDocument : AuditableEntity<RequestMachineryDocument>
{
    #region Properties

    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(GlobalCmts.RequestMachinery)]
    public RequestMachinery RequestMachinery { get; private set; }

    #endregion

    public RequestMachineryDocument(RequestMachinery requestMachinery, string url)
    {
        RequestMachinery = Guard.Against.Null(requestMachinery, nameof(requestMachinery));
        Url = Guard.Against.NullOrEmpty(url, nameof(url));
    }

    #region Commands

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }
    public void SetUrl(string url)
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(url)); ;
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestMachineryDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}
