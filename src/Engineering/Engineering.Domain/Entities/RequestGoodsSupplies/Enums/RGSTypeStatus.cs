namespace Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

public enum RGSTypeStatus
{
    [Description("ارسال شده")]
    Requested = 1,
    [Description("تایید")]
    Confirmed = 2,
    [Description("تائید اول")]
    FirstConfirmed = 3,
    [Description("تائید دوم")]
    SecondConfirmed = 4,
    [Description("تائید سوم ")]
    ThirdConfirmed = 5,
    [Description("تایید نشده ")]
    Rejected = 6,
    [Description("انجام شده")]
    IsDone = 7,
}
public class RGSTypeRules
{
    public static List<RGSTypeStatus> AllowForNotif =
        [
        RGSTypeStatus.Confirmed,
        RGSTypeStatus.FirstConfirmed,
        RGSTypeStatus.SecondConfirmed,
        RGSTypeStatus.ThirdConfirmed,
    ];
    public static List<RGSTypeStatus> IsReExamination =
        [
        RGSTypeStatus.Rejected
        ];
    public static List<RGSTypeStatus> IsClosed =
        [
        RGSTypeStatus.Rejected
        ];
}