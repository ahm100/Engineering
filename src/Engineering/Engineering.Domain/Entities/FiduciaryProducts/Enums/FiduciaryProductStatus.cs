
namespace Engineering.Domain.Entities.FiduciaryProducts.Enums;

public enum FiduciaryProductStatus
{
    [Description("ثبت اولیه")]
    New = 1,
    [Description("در انتظار بررسی")]
    Pending = 2,
    [Description("تایید درخواست")]
    Confirmed = 3,
    [Description("رد درخواست")]
    Rejected = 4,
    [Description("تامین کامل")]
    FullDelivery = 5,
    [Description("تامین نشده")]
    NoDelivery = 6,
    [Description("تامین ناقص")]
    IncompleteDelivery = 7,
    [Description("عودت کامل")]
    FullReturned = 8,
    [Description("عودت ناقص")]
    IncompleteReturned = 9,
    [Description("تایید ناقص درخواست")]
    IncompleteConfirmed = 10,
    [Description("عودت نشده")]
    ReturnRejected = 11,
    [Description("ارسال مجدد")]
    Resended = 12,
}

public class ValidateFiduciaryProductStatus()
{
    public static List<FiduciaryProductStatus> AllowForRejected =
    [
        FiduciaryProductStatus.Pending,
    ];

    public static List<FiduciaryProductStatus> AllowForConfirm =
    [
        FiduciaryProductStatus.Pending,
    ];

    public static List<FiduciaryProductStatus> AllowForPending =
    [
        FiduciaryProductStatus.New,
        FiduciaryProductStatus.Resended,
    ];

    public static List<FiduciaryProductStatus> AllowForUpdate =
    [
        FiduciaryProductStatus.New,
        FiduciaryProductStatus.Rejected,
    ];

    public static List<FiduciaryProductStatus> AllowForDelete =
    [
        FiduciaryProductStatus.New,
        FiduciaryProductStatus.Rejected,
    ];
}