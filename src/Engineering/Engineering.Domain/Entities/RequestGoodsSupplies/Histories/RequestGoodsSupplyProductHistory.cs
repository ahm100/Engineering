using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

[Description(GlobalCmts.Histories)]
public class RequestGoodsSupplyProductHistory : AuditableEntity<RequestGoodsSupplyProductHistory>
{
    [Description(GlobalCmts.Status)]
    public GoodsSupplyDetailStatus Status { get; private set; } = GoodsSupplyDetailStatus.New;

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(RGSCmts.RequestGoodsSupplyProduct)]
    public long RequestGoodsSupplyProductId { get; private set; }
    public RequestGoodsSupplyProduct RequestGoodsSupplyProduct { get; private set; }

    public RequestGoodsSupplyProductHistory(RequestGoodsSupplyProduct requestGoodsSupplyProduct,
        GoodsSupplyDetailStatus status,
        string? description) : this()
    {
        SetRequestGoodsSupplyProduct(requestGoodsSupplyProduct);
        SetStatus(status);
        SetDescription(description);
    }

    public RequestGoodsSupplyProductHistory(
        RequestGoodsSupplyProduct requestGoodsSupplyProduct,
        GoodsSupplyDetailStatus status,
        string? description,
        long? userId) : this()
    {
        SetRequestGoodsSupplyProduct(requestGoodsSupplyProduct);
        SetStatus(status);
        SetDescription(description);

        CreatorId = userId ?? 0;
        CheckUser = true;
        Created = DateTime.UtcNow;
    }

    public void SetRequestGoodsSupplyProduct(RequestGoodsSupplyProduct value)
    {
        RequestGoodsSupplyProduct = Guard.Against.Null(value, nameof(value));
        RequestGoodsSupplyProductId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetStatus(GoodsSupplyDetailStatus status)
    {
        Status = Guard.Against.EnumOutOfRange(status, nameof(status));
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public static RequestGoodsSupplyProductHistory Create(RequestGoodsSupplyProduct requestGoodsSupplyProduct, GoodsSupplyDetailStatus status, string? description)
    {
        return new RequestGoodsSupplyProductHistory(requestGoodsSupplyProduct, status, description);
    }
    public static RequestGoodsSupplyProductHistory Create(RequestGoodsSupplyProduct requestGoodsSupplyProduct, GoodsSupplyDetailStatus status, string? description, long? userId)
    {
        return new RequestGoodsSupplyProductHistory(requestGoodsSupplyProduct, status, description, userId);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private RequestGoodsSupplyProductHistory() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
