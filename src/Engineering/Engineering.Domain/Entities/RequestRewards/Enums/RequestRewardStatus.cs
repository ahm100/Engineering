namespace Engineering.Domain.Entities.RequestRewards.Enums;

public enum RequestRewardStatus
{
    [Description("ثبت اولیه")]
    New = 1,
    [Description("در انتظار بررسی")]
    Pending = 2,
    [Description("تایید شده")]
    Confirmed = 3,
    [Description("رد درخواست")]
    Rejected = 4,
    [Description("بسته شده")]
    Closed = 5,
}
