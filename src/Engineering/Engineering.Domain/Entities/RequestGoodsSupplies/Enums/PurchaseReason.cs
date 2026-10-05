namespace Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

public enum PurchaseReason
{
    [Description("مصرف")]
    Consume = 1,
    [Description("استوک")]
    Stock = 2,
    [Description("خدمات")]
    Service = 3,
    [Description("تبلیغات")]
    Ads = 4,
    [Description("پروژه ")]
    Project = 5,
}