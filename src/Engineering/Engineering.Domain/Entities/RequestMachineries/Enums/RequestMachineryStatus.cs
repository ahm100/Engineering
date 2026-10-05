namespace Engineering.Domain.Entities.RequestMachineries.Enums;

public enum RequestMachineryStatus
{
    [Description("ثبت اولیه")]
    New = 1,
    [Description("در انتظار بررسی")]
    Pending = 2,
    [Description("رد درخواست")]
    Rejected = 3,
    [Description("تایید شده و تخصیص متصدی")]
    Confirmed = 4,
    [Description("استعلام گیری")]
    Inquiry = 6,
    [Description("در انتظار تخصیص ماشین آلات")]
    InquiryDone = 7,
    [Description("برگشت به استعلام گیری")]
    InquiryRejected = 8,
    [Description("تخصیص ماشین آلات")]
    MachineryAppoinment = 9,
    [Description("استفاده در پروژه")]
    OnProject = 10,
    [Description("پرداخت شده")]
    Done = 11,
    [Description("در انتظار تایید استعلام")]
    EndInquiry = 12,
    [Description("برگشت درخواست")]
    Returned = 13,
    [Description("ارسال مجدد")]
    Resended = 14,
    [Description("رد دستور پرداخت")]
    PaymentRejected = 15,
    [Description("بخشی پرداخت شده")]
    IncompletelyPaid = 16,
    [Description("در انتظار پرداخت")]
    PaidPending = 17,
    [Description("تایید مدیریت")]
    ManagerConfirm = 18,
    [Description("ارسال برای مدیریت")]
    SendToManager = 19,
}

public class RequestMachineryStatusValidator
{
    public static List<RequestMachineryStatus> AllowStatusForUpdate =
    [
        RequestMachineryStatus.New,
        RequestMachineryStatus.Returned,
        RequestMachineryStatus.Rejected,
        RequestMachineryStatus.Pending,
        RequestMachineryStatus.Resended,
    ];
    public static List<RequestMachineryStatus> AllowForShowInReport =
    [
        RequestMachineryStatus.OnProject,
        RequestMachineryStatus.Done,
        RequestMachineryStatus.PaymentRejected,
        RequestMachineryStatus.IncompletelyPaid,
        RequestMachineryStatus.PaidPending
    ];

    public static List<RequestMachineryStatus> AllowStatusForResended =
    [
        RequestMachineryStatus.Returned,
        RequestMachineryStatus.Rejected,
    ];

    public static List<RequestMachineryStatus> AllowStatusForReturned =
    [
        RequestMachineryStatus.Pending,
        RequestMachineryStatus.OnProject,
        RequestMachineryStatus.ManagerConfirm,
        RequestMachineryStatus.SendToManager,
    ];

    public static List<RequestMachineryStatus> AllowStatusForDelete =
    [
        RequestMachineryStatus.New,
        RequestMachineryStatus.Pending,
        RequestMachineryStatus.Returned,
        RequestMachineryStatus.Rejected,
    ];

    public static List<RequestMachineryStatus> AllowforShowInGetFiltered =
    [
        RequestMachineryStatus.ManagerConfirm,
        RequestMachineryStatus.SendToManager,
        RequestMachineryStatus.OnProject,
        RequestMachineryStatus.IncompletelyPaid,
        RequestMachineryStatus.Done,
        RequestMachineryStatus.PaymentRejected,
    ];

    public static List<RequestMachineryStatus> AllowforShowInOnProjectReport =
    [
        RequestMachineryStatus.SendToManager,
        RequestMachineryStatus.ManagerConfirm,
        RequestMachineryStatus.OnProject
    ];

    public static List<RequestMachineryStatus> AllowforShowInSendManagerReport =
    [
        RequestMachineryStatus.SendToManager,
    ];
}