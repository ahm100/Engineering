namespace Engineering.Domain.Entities.Transportations.Enums;

public enum TransportationRequestStatus
{
    /// <summary>
    /// ثبت اولیه
    /// </summary>
    [Description("ثبت اولیه")]
    InitialRegistration = 1,
    /// <summary>
    /// درحال بررسی
    /// </summary>
    [Description("درحال بررسی")]
    Pending = 2,
    /// <summary>
    /// تایید شده
    /// </summary>
    [Description("تایید شده")]
    Accepted = 3,
    /// <summary>
    /// در انتظار پرداخت
    /// </summary>
    [Description("در انتظار پرداخت")]
    Paid = 4,
    /// <summary>
    /// رد درخواست
    /// </summary>
    [Description("رد درخواست")]
    RequestRejection = 5,
    /// <summary>
    /// برگشت درخواست
    /// </summary>
    [Description("برگشت درخواست")]
    RequestReturned = 6,

    /// <summary>
    /// پرداخت شده
    /// </summary>
    [Description("پرداخت شده")]
    PaidDone = 7,

    /// <summary>
    /// رد دستور پرداخت
    /// </summary>
    [Description("رد دستور پرداخت")]
    PaymentRejected = 8,

    /// <summary>
    /// بخشی پرداخت شده
    /// </summary>
    [Description("بخشی پرداخت شده")]
    IncompletelyPaid = 9,

    /// <summary>
    /// ارسال مجدد
    /// </summary>
    [Description("ارسال مجدد")]
    RequestResended = 11,
    /// <summary>
    /// ثبت از انبار
    /// </summary>
    [Description("ثبت از انبار")]
    WarehouseInit = 12,
    /// <summary>
    /// تخصیص راننده
    /// </summary>
    [Description("تخصیص راننده")]
    DriverAssignment = 13,
    /// <summary>
    /// ارسال بار
    /// </summary>
    [Description("ارسال بار")]
    SendDone = 14,
    /// <summary>
    /// ارسال بار
    /// </summary>
    [Description("تایید حراست")]
    SecurityConfirm = 15,
}

public class ValidateTransportationRequestStatus
{
    public static List<TransportationRequestStatus> AllowStatusForUpdate =
    [
        TransportationRequestStatus.InitialRegistration,
        TransportationRequestStatus.WarehouseInit,
        TransportationRequestStatus.RequestReturned,
        TransportationRequestStatus.RequestResended,
        TransportationRequestStatus.Accepted,
    ];

    public static List<TransportationRequestStatus> AllowStatusForResend =
    [
        TransportationRequestStatus.RequestReturned,
        TransportationRequestStatus.Accepted,
    ];

    public static List<TransportationRequestStatus> AllowForPackingRelease =
    [
        TransportationRequestStatus.DriverAssignment,
    ];

    public static List<TransportationRequestStatus> AllowStatusForReturn =
    [
        TransportationRequestStatus.Pending,
        TransportationRequestStatus.Accepted,
    ];

    public static List<TransportationRequestStatus> AllowStatusForReforms =
    [
        TransportationRequestStatus.DriverAssignment,
        TransportationRequestStatus.WarehouseInit,
    ];

    public static List<TransportationRequestStatus> DontAllowForUpdate =
    [
        TransportationRequestStatus.SendDone,
        TransportationRequestStatus.SecurityConfirm
    ];

    public static List<TransportationRequestStatus> AllowStatusForSendDone =
    [
        TransportationRequestStatus.DriverAssignment
    ];

    public static List<TransportationRequestStatus> AllowStatusForSecurityConfirm =
    [
        TransportationRequestStatus.SendDone,
    ];

    public static List<TransportationRequestStatus> AllowStatusForAddBill =
    [
        TransportationRequestStatus.SecurityConfirm,
        TransportationRequestStatus.SendDone,
    ];
}