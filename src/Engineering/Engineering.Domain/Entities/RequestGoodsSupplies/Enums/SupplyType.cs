namespace Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

public enum SupplyType
{
    [Description("کالا")]
    Product = 1,
    [Description("خدمات")]
    Service = 2,
    [Description("تبلیغات")]
    Ads = 3,
    [Description("پروژه")]
    Project = 4
}