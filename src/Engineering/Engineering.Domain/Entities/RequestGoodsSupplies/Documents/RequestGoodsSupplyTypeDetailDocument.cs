namespace Engineering.Domain.Entities.RequestGoodsSupplies.Documents;

[Description(GlobalCmts.Document)]
public class RequestGoodsSupplyTypeDetailDocument : AuditableEntity<RequestGoodsSupplyTypeDetailDocument>
{
    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(RGSCmts.RequestGoodsSupplyTypeDetail)]
    public long RequestGoodsSupplyTypeDetailId { get; private set; }
    public RequestGoodsSupplyTypeDetail RequestGoodsSupplyTypeDetail { get; private set; }

    public RequestGoodsSupplyTypeDetailDocument(string url,
        RequestGoodsSupplyTypeDetail requestGoodsSupplyDetail) : this()
    {
        SetUrl(url);
        SetRequestGoodsSupplyTypeDetail(requestGoodsSupplyDetail);
    }

    public static RequestGoodsSupplyTypeDetailDocument Create(string url, RequestGoodsSupplyTypeDetail requestGoodsSupplyDetail)
    {
        return new RequestGoodsSupplyTypeDetailDocument(url, requestGoodsSupplyDetail);
    }

    public void SetUrl(string value)
    {
        Url = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetRequestGoodsSupplyTypeDetail(RequestGoodsSupplyTypeDetail value)
    {
        RequestGoodsSupplyTypeDetail = Guard.Against.Null(value, nameof(value));
        RequestGoodsSupplyTypeDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestGoodsSupplyTypeDetailDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}