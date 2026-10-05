namespace Engineering.Domain.Entities.RequestGoodsSupplies.Documents;

[Description(GlobalCmts.Document)]
public class RequestGoodsSupplyDetailDocument : AuditableEntity<RequestGoodsSupplyDetailDocument>
{
    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(RGSCmts.RequestGoodsSupplyDetail)]
    public long RequestGoodsSupplyDetailId { get; private set; }
    public RequestGoodsSupplyDetail RequestGoodsSupplyDetail { get; private set; }

    public RequestGoodsSupplyDetailDocument(string url,
        RequestGoodsSupplyDetail requestGoodsSupplyDetail) : this()
    {
        SetUrl(url);
        SetRequestGoodsSupplyDetail(requestGoodsSupplyDetail);
    }

    public static RequestGoodsSupplyDetailDocument Create(string url, RequestGoodsSupplyDetail requestGoodsSupplyDetail)
    {
        return new RequestGoodsSupplyDetailDocument(url, requestGoodsSupplyDetail);
    }

    public void SetIsDeleted()
    {
        this.IsDeleted = true;
    }

    public void SetUrl(string value)
    {
        Url = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetRequestGoodsSupplyDetail(RequestGoodsSupplyDetail value)
    {
        RequestGoodsSupplyDetail = Guard.Against.Null(value, nameof(value));
        RequestGoodsSupplyDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestGoodsSupplyDetailDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
