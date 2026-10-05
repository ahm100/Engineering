namespace Engineering.Domain.Entities.RequestMachineries;

public class RequestMachineryInquiryDocument : AuditableEntity<RequestMachineryInquiryDocument>
{
    #region Properties

    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = null!;

    [Description(RequestMachineryCmts.RequestMachineryInquiry)]
    public long RequestMachineryInquiryId { get; private set; }
    public RequestMachineryInquiry RequestMachineryInquiry { get; private set; }

    #endregion

    public RequestMachineryInquiryDocument(string url,
        RequestMachineryInquiry requestMachineryInquiry)
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(url));
        RequestMachineryInquiry = Guard.Against.Null(requestMachineryInquiry, nameof(requestMachineryInquiry));

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

    public void SetRequestMachineryInquiry(string url)
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(url));
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestMachineryInquiryDocument() { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    #endregion

}