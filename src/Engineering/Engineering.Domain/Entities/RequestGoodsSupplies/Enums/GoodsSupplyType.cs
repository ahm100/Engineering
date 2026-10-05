namespace Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

public enum GoodsSupplyType
{
    [Description("خرید از پیمانکار - صورت وضعیت")]
    Contractor = 1,
    [Description("درخواست از تامین")]
    GoodsSupply = 2,
    [Description("خرید سر پروژه ای - فاکتور")]
    Project = 3,
    [Description("خرید کالا")]
    Products = 4,
    [Description("خرید خدمات")]
    Services = 5,
    [Description("خرید تبلیغات")]
    Advertisements = 6,
    [Description("خرید آیتم های پروژه")]
    ProjectItems = 7,
    [Description("خرید برای پیمانکار")]
    PurchaseForContractor = 8,
}