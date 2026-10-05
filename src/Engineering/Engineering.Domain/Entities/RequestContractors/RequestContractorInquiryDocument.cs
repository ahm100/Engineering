namespace Engineering.Domain.Entities.RequestContractors;

public class RequestContractorInquiryDocument : AuditableEntity<RequestContractorInquiryDocument>
{
    #region Properties

    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = null!;

    [Description(RequestContractorCmts.RequestContractorInquiry)]
    public long RequestContractorInquiryId { get; private set; }
    public RequestContractorInquiry RequestContractorInquiry { get; private set; } = null!;

    #endregion

    public RequestContractorInquiryDocument(string url,
        RequestContractorInquiry requestContractorInquiry) : this()
    {
        SetUrl(url);
        SetRequestContractorInquiry(requestContractorInquiry);

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

    public void SetRequestContractorInquiry(RequestContractorInquiry value)
    {
        RequestContractorInquiry = Guard.Against.Null(value, nameof(value));
        RequestContractorInquiryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    #endregion

    #region Constructors

    private RequestContractorInquiryDocument() { }

    #endregion

}