
namespace Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

public enum GoodsSupplyManagementType
{
    [Description("خروج مصرفی")]
    InStock = 1,
    [Description("بین انباری")]
    BetweenStock = 2,
    [Description("بازرگانی")]
    Commerce = 3
}
