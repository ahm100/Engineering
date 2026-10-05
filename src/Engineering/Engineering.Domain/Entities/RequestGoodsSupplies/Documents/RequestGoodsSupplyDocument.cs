namespace Engineering.Domain.Entities.RequestGoodsSupplies.Documents;

[Description(RGSCmts.RequestGoodsSupplyDocument)]
public class RequestGoodsSupplyDocument : AuditableEntity<RequestGoodsSupplyDocument>
{
    [Description(GlobalCmts.Url)]
    public string Url { get; private set; }

    [Description(RGSCmts.RequestGoodsSupply)]
    public long RequestGoodsSupplyId { get; private set; }
    public RequestGoodsSupply RequestGoodsSupply { get; private set; }

    public RequestGoodsSupplyDocument(
        RequestGoodsSupply rgs,
        string url) : this()
    {
        SetRequestGoodsSupply(rgs);
        SetUrl(url);
    }

    public void SetRequestGoodsSupply(RequestGoodsSupply value)
    {
        RequestGoodsSupply = Guard.Against.Null(value, nameof(value));
        RequestGoodsSupplyId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetUrl(string value)
    {
        Url = Guard.Against.NullOrEmpty(value, nameof(value));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private RequestGoodsSupplyDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}