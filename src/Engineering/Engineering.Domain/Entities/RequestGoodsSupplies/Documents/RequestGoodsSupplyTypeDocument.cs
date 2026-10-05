namespace Engineering.Domain.Entities.RequestGoodsSupplies.Documents;

[Description(GlobalCmts.Document)]
public class RequestGoodsSupplyTypeDocument : AuditableEntity<RequestGoodsSupplyTypeDocument>
{
    [Description(GlobalCmts.Url)]
    public string Url { get; private set; } = string.Empty;

    [Description(RGSCmts.RequestGoodsSupplyTypeDetail)]
    public long RequestGoodsSupplyTypeId { get; private set; }
    public RequestGoodsSupplyType RequestGoodsSupplyType { get; private set; }

    public RequestGoodsSupplyTypeDocument(string url,
        RequestGoodsSupplyType requestGoodsSupply) : this()
    {
        SetUrl(url);
        SetRequestGoodsSupplyType(requestGoodsSupply);
    }

    public static RequestGoodsSupplyTypeDocument Create(string url, RequestGoodsSupplyType requestGoodsSupplyType)
    {
        return new RequestGoodsSupplyTypeDocument(url, requestGoodsSupplyType);
    }

    public void SetUrl(string value)
    {
        Url = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetRequestGoodsSupplyType(RequestGoodsSupplyType value)
    {
        RequestGoodsSupplyType = Guard.Against.Null(value, nameof(value));
        RequestGoodsSupplyTypeId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestGoodsSupplyTypeDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}