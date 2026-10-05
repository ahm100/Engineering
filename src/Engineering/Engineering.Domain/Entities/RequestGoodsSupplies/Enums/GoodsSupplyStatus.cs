
namespace Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

public enum GoodsSupplyStatus
{
    [Description("پیش نویس")]
    Draft = 1,
    [Description("ثبت اولیه")]
    Created = 10,
    [Description("ارسال نشده")]
    NotSend = 11,
    [Description("در حال بررسی")]
    Pending = 12,
    [Description("تایید")]
    Confirm = 13,
    [Description("تایید نشده")]
    Reject = 14,
    [Description("بایگانی")]
    Archive = 15,
    [Description("ابطال شده")]
    Revoke = 16,
}