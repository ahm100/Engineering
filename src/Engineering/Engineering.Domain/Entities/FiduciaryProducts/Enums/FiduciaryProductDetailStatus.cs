namespace Engineering.Domain.Entities.FiduciaryProducts.Enums;

public enum FiduciaryProductDetailStatus
{
    [Description("ثبت اولیه")]
    New = 1,
    [Description("در انتظار بررسی")]
    Pending = 2,
    [Description("در انتظار خروج از انبار")]
    ExitForConsume = 3,
    [Description("رد درخواست")]
    Rejected = 4,
    [Description("عودت")]
    Returned = 5,
    [Description("عودت ناقص")]
    IncompleteReturned = 6,
    [Description("تامین شده")]
    Delivary = 7,
    [Description("رد تامین")]
    NoDelivary = 8,
    [Description("در انتظار خروج ناقص از انبار")]
    IncompleteDelivered = 9,
    [Description("رد عودت")]
    ReturnRejected = 10,
    [Description("ارسال مجدد")]
    DetailResened = 11,
}

public class ValidateFiduciaryProductDetailStatus()
{
    public static List<FiduciaryProductDetailStatus> AllowForPending =
    [
        FiduciaryProductDetailStatus.New,
        FiduciaryProductDetailStatus.DetailResened,
    ];

    public static List<FiduciaryProductDetailStatus> AllowForRejected =
    [
        FiduciaryProductDetailStatus.Pending,
    ];

    public static List<FiduciaryProductDetailStatus> AllowForConfirm =
    [
        FiduciaryProductDetailStatus.Pending,
    ];

    public static List<FiduciaryProductDetailStatus> AllowForUpdateOrDelete =
    [
        FiduciaryProductDetailStatus.New,
        FiduciaryProductDetailStatus.Rejected,
    ];

    public static List<FiduciaryProductDetailStatus> AllowForDeliverAndNoDeliver =
    [
        FiduciaryProductDetailStatus.IncompleteDelivered,
        FiduciaryProductDetailStatus.ExitForConsume,
    ];
}