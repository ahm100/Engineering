namespace Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

public enum GoodsSupplyDetailStatus
{
    [Description("ثبت اولیه")]
    New = 1,
    [Description("پیش نویس")]
    Draft = 5,
    [Description("ارسال مجدد برای مدیرواحد-پروژه")]
    ProjectManagerResend = 11,

    [Description("در انتظار بررسی مدیر واحد-پروژه")]
    ProjectManagerPending = 22,
    [Description("تایید مدیر واحد-پروژه")]
    ProjectManagerConfirmed = 33,
    [Description("برگشت درخواست مدیر واحد-پروژه")]
    ProjectManagerReturned = 44,
    [Description("رد مدیر واحد-پروژه")]
    ProjectManagerRejected = 55,

    [Description("در انتظار بررسی واحد ناظر")]
    GoodsManagerPending = 60,

    [Description("برگشت درخواست واحد ناظر")]
    GoodsManagerReturned = 61,

    [Description("رد واحد ناظر")]
    GoodsManagerRejected = 62,

    [Description("تایید واحد ناظر")]
    GoodsManagerConfirmed = 65,

    [Description("در انتظار بررسی مدیریت-ارشد")]
    ManagementPending = 66,
    [Description("تایید مدیریت-ارشد")]
    ManagementConfirmed = 77,
    [Description("برگشت درخواست مدیریت-ارشد")]
    ManagementReturned = 88,
    [Description("رد مدیریت-ارشد")]
    ManagementRejected = 99,

    [Description("در انتظار بررسی تامین-انبار")]
    SupplyUnitPending = 111,
    [Description("رد واحد تامین-انبار")]
    SupplyUnitRejected = 122,
    [Description("برگشت از واحد تامین-انبار")]
    SupplyUnitReturned = 133,
    [Description("در انتظار تامین-انبار")]
    PendingForSupply = 144,

    [Description("برگشت به واحد تامین-انبار")]
    ReturnToSupply = 150,
    [Description("تایید فاکتور بازرگانی")]
    CommercialInvoiceConfirmation = 153,
    [Description("تامین کامل")]
    CompleteSupply = 155,
    [Description("تامین ناقص")]
    InCompleteSupply = 166,
    [Description("تامین نشده")]
    NotCompleteSupply = 177,
    [Description("بسته شده")]
    Closed = 188,
}

public class GSDSRules
{
    public static List<GoodsSupplyDetailStatus> AllowForPM =
        [
        GoodsSupplyDetailStatus.New,
        GoodsSupplyDetailStatus.ProjectManagerPending,
        GoodsSupplyDetailStatus.ProjectManagerResend,
        GoodsSupplyDetailStatus.ManagementReturned
    ];

    public static List<GoodsSupplyDetailStatus> IsReExamination =
        [
        GoodsSupplyDetailStatus.ProjectManagerReturned,
        GoodsSupplyDetailStatus.ManagementReturned,
        GoodsSupplyDetailStatus.SupplyUnitReturned
    ];

    public static List<GoodsSupplyDetailStatus> IsClosed =
        [
        GoodsSupplyDetailStatus.ProjectManagerRejected,
        GoodsSupplyDetailStatus.GoodsManagerRejected,
        GoodsSupplyDetailStatus.ManagementRejected,
        GoodsSupplyDetailStatus.SupplyUnitRejected
    ];

    public static List<GoodsSupplyDetailStatus> PMStatuses =
        [
        GoodsSupplyDetailStatus.ProjectManagerPending,
        GoodsSupplyDetailStatus.ProjectManagerConfirmed,
        GoodsSupplyDetailStatus.ProjectManagerReturned,
        GoodsSupplyDetailStatus.ProjectManagerRejected
    ];

    public static List<GoodsSupplyDetailStatus> AllowForManagement =
        [
        GoodsSupplyDetailStatus.ProjectManagerConfirmed,
        GoodsSupplyDetailStatus.ManagementPending,
        GoodsSupplyDetailStatus.SupplyUnitReturned
    ];

    public static List<GoodsSupplyDetailStatus> AllowForGoodsManager =
    [
        GoodsSupplyDetailStatus.GoodsManagerPending
    ];

    public static List<GoodsSupplyDetailStatus> GoodsManagerStatuses =
    [
        GoodsSupplyDetailStatus.GoodsManagerPending,
        GoodsSupplyDetailStatus.GoodsManagerReturned,
        GoodsSupplyDetailStatus.GoodsManagerRejected,
        GoodsSupplyDetailStatus.GoodsManagerConfirmed
    ];

    public static List<GoodsSupplyDetailStatus> ManagementStatuses =
        [
        GoodsSupplyDetailStatus.ManagementPending,
        GoodsSupplyDetailStatus.ManagementConfirmed,
        GoodsSupplyDetailStatus.ManagementReturned,
        GoodsSupplyDetailStatus.ManagementRejected
    ];

    public static List<GoodsSupplyDetailStatus> TotalSupply =
        [
        GoodsSupplyDetailStatus.ProjectManagerReturned,
        GoodsSupplyDetailStatus.ProjectManagerRejected,
        GoodsSupplyDetailStatus.ManagementReturned,
        GoodsSupplyDetailStatus.ManagementRejected,
        GoodsSupplyDetailStatus.SupplyUnitRejected,
        GoodsSupplyDetailStatus.SupplyUnitReturned,
        GoodsSupplyDetailStatus.NotCompleteSupply,
        GoodsSupplyDetailStatus.Closed,
        GoodsSupplyDetailStatus.GoodsManagerReturned,
        GoodsSupplyDetailStatus.GoodsManagerRejected,
        GoodsSupplyDetailStatus.NotCompleteSupply,
        GoodsSupplyDetailStatus.Closed,
    ];

    public static List<GoodsSupplyDetailStatus> InProgress =
        [
        GoodsSupplyDetailStatus.ProjectManagerResend,
        GoodsSupplyDetailStatus.ProjectManagerPending,
        GoodsSupplyDetailStatus.ProjectManagerConfirmed,
        GoodsSupplyDetailStatus.ProjectManagerReturned,
        GoodsSupplyDetailStatus.ManagementConfirmed,
        GoodsSupplyDetailStatus.ManagementReturned,
        GoodsSupplyDetailStatus.SupplyUnitPending,
        GoodsSupplyDetailStatus.SupplyUnitReturned,
        GoodsSupplyDetailStatus.PendingForSupply,
        GoodsSupplyDetailStatus.ReturnToSupply,
        GoodsSupplyDetailStatus.CommercialInvoiceConfirmation,
        GoodsSupplyDetailStatus.InCompleteSupply,
        GoodsSupplyDetailStatus.NotCompleteSupply,
    ];

    public static List<GoodsSupplyDetailStatus> Completed =
        [
        GoodsSupplyDetailStatus.CompleteSupply
    ];

    public static List<GoodsSupplyDetailStatus> CanHaveWarehouse =
        [
        GoodsSupplyDetailStatus.SupplyUnitPending,
        GoodsSupplyDetailStatus.PendingForSupply,
        GoodsSupplyDetailStatus.ReturnToSupply,
        GoodsSupplyDetailStatus.CommercialInvoiceConfirmation,
        GoodsSupplyDetailStatus.CompleteSupply,
        GoodsSupplyDetailStatus.InCompleteSupply,
        GoodsSupplyDetailStatus.NotCompleteSupply,
        GoodsSupplyDetailStatus.SupplyUnitReturned,
        GoodsSupplyDetailStatus.SupplyUnitRejected
    ];

    public static List<GoodsSupplyDetailStatus> AllowStatusForRejectedMessage =
    [
        GoodsSupplyDetailStatus.ManagementRejected,
        GoodsSupplyDetailStatus.ProjectManagerRejected,
        GoodsSupplyDetailStatus.SupplyUnitRejected,
        GoodsSupplyDetailStatus.ReturnToSupply,
        GoodsSupplyDetailStatus.ManagementReturned,
        GoodsSupplyDetailStatus.ProjectManagerReturned,
        GoodsSupplyDetailStatus.SupplyUnitReturned,
        GoodsSupplyDetailStatus.GoodsManagerReturned,
        GoodsSupplyDetailStatus.GoodsManagerRejected
    ];

    public static List<GoodsSupplyDetailStatus> AllowStatusForSendNotification =
    [
        GoodsSupplyDetailStatus.ManagementRejected,
        GoodsSupplyDetailStatus.ProjectManagerRejected,
        GoodsSupplyDetailStatus.SupplyUnitRejected,
        GoodsSupplyDetailStatus.ManagementReturned,
        GoodsSupplyDetailStatus.ProjectManagerReturned,
        GoodsSupplyDetailStatus.SupplyUnitReturned,
        GoodsSupplyDetailStatus.GoodsManagerReturned,
        GoodsSupplyDetailStatus.GoodsManagerRejected,
    ];


    public static List<GoodsSupplyDetailStatus> AllowStatusForUpdate =
    [
        GoodsSupplyDetailStatus.New,
        GoodsSupplyDetailStatus.Draft,
        GoodsSupplyDetailStatus.NotCompleteSupply,
        GoodsSupplyDetailStatus.ProjectManagerResend,
        GoodsSupplyDetailStatus.SupplyUnitReturned,
        GoodsSupplyDetailStatus.ManagementReturned,
        GoodsSupplyDetailStatus.ProjectManagerReturned,
        GoodsSupplyDetailStatus.ProjectManagerReturned,
        GoodsSupplyDetailStatus.GoodsManagerReturned,
    ];

    public static List<GoodsSupplyDetailStatus> ProjectManagerChecker =
    [
        GoodsSupplyDetailStatus.ProjectManagerReturned,
        GoodsSupplyDetailStatus.ProjectManagerRejected,
        GoodsSupplyDetailStatus.ProjectManagerConfirmed
    ];

    public static List<GoodsSupplyDetailStatus> AllowForDraft =
    [
        GoodsSupplyDetailStatus.New,
        GoodsSupplyDetailStatus.Draft
    ];

    public static List<GoodsSupplyDetailStatus> ManagerChecker =
    [
        GoodsSupplyDetailStatus.ManagementReturned,
        GoodsSupplyDetailStatus.ManagementRejected,
        GoodsSupplyDetailStatus.ManagementConfirmed
    ];

    public static List<GoodsSupplyDetailStatus> AllowStatusForDelete =
    [
        GoodsSupplyDetailStatus.New,
        GoodsSupplyDetailStatus.Draft,
        GoodsSupplyDetailStatus.ProjectManagerResend,
        GoodsSupplyDetailStatus.ProjectManagerReturned,
        GoodsSupplyDetailStatus.GoodsManagerReturned,
        GoodsSupplyDetailStatus.GoodsManagerRejected,
        GoodsSupplyDetailStatus.ProjectManagerRejected,
        GoodsSupplyDetailStatus.ManagementReturned,
        GoodsSupplyDetailStatus.ManagementRejected,
        GoodsSupplyDetailStatus.SupplyUnitReturned,
        GoodsSupplyDetailStatus.NotCompleteSupply,
        GoodsSupplyDetailStatus.SupplyUnitRejected,
    ];
}