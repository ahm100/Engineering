using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

[Description(RGSCmts.RequestGoodsSupplyHistory)]
public class RequestGoodsSupplyHistory : AuditableEntity<RequestGoodsSupplyHistory>
{
    [Description(GlobalCmts.Status)]
    public GoodsSupplyStatus Status { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(RGSCmts.RequestGoodsSupply)]
    public long RequestGoodsSupplyId { get; private set; }
    public RequestGoodsSupply RequestGoodsSupply { get; private set; }

    public RequestGoodsSupplyHistory(RequestGoodsSupply requestGoodsSupply, GoodsSupplyStatus status, string? description) : this()
    {
        SetRequestGoodsSupply(requestGoodsSupply);
        SetStatus(status);
        SetDescription(description);
    }

    public RequestGoodsSupplyHistory(RequestGoodsSupply requestGoodsSupply, GoodsSupplyStatus status, string? description, long? userId) : this()
    {
        SetRequestGoodsSupply(requestGoodsSupply);
        SetStatus(status);
        SetDescription(description);

        CreatorId = userId ?? 0;
        CheckUser = true;
        Created = DateTime.UtcNow;
    }

    public void SetRequestGoodsSupply(RequestGoodsSupply requestGoodsSupply)
    {
        RequestGoodsSupply = Guard.Against.Null(requestGoodsSupply, nameof(requestGoodsSupply));
    }

    public void SetStatus(GoodsSupplyStatus status)
    {
        Status = Guard.Against.EnumOutOfRange(status, nameof(status));
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public static RequestGoodsSupplyHistory Create(RequestGoodsSupply requestGoodsSupply, GoodsSupplyStatus status, string? description)
    {
        return new RequestGoodsSupplyHistory(requestGoodsSupply, status, description);
    }
    public static RequestGoodsSupplyHistory Create(RequestGoodsSupply requestGoodsSupply, GoodsSupplyStatus status, string? description, long? userId)
    {
        return new RequestGoodsSupplyHistory(requestGoodsSupply, status, description, userId);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestGoodsSupplyHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
