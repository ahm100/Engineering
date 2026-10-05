namespace Engineering.Domain.Entities.RequestContractors.Enums;

public enum RequestContractorStatus
{
    [Description("ثبت اولیه")]
    New = 1,
    [Description("در حال بررسی")]
    Pending = 2,
    [Description("استعلام گیری")]
    Inquiry = 10,
    [Description("رد درخواست")]
    Rejected = 20,
    [Description("پایان استعلام گیری")]
    EndInquiry = 25,
    [Description("تایید استعلام")]
    Confirmed = 30,
    [Description("برگشت به استعلام گیری")]
    InquiryRejected = 40,
}

public class ValidateRequestContractorStatus
{
    public static List<RequestContractorStatus> AllowStatusForUpdate =
    [
        RequestContractorStatus.New,
        RequestContractorStatus.Pending,
        RequestContractorStatus.Rejected,
        RequestContractorStatus.InquiryRejected,
        RequestContractorStatus.Inquiry,
    ];

    public static List<RequestContractorStatus> AllowStatusForDelete =
    [
        RequestContractorStatus.New,
        RequestContractorStatus.Pending,
        RequestContractorStatus.InquiryRejected,
    ];

    public static List<RequestContractorStatus> AllowStatusForInquiry =
    [
        RequestContractorStatus.Pending,
        RequestContractorStatus.InquiryRejected,
    ];

    public static List<RequestContractorStatus> AllowStatusForEndInquiry =
    [
        RequestContractorStatus.Inquiry
    ];

    public static List<RequestContractorStatus> AllowStatusForPending =
    [
        RequestContractorStatus.New
    ];

    public static List<RequestContractorStatus> AllowStatusForRequestRejected =
    [
        RequestContractorStatus.New,
        RequestContractorStatus.Inquiry,
        RequestContractorStatus.Pending,
        RequestContractorStatus.InquiryRejected,
        RequestContractorStatus.EndInquiry,
    ];

    public static List<RequestContractorStatus> AllowStatusForInquiryRejected =
    [
        RequestContractorStatus.Inquiry,
        RequestContractorStatus.EndInquiry,
    ];

    public static List<RequestContractorStatus> AllowStatusForInquiryConfirmed =
    [
        RequestContractorStatus.Inquiry,
        RequestContractorStatus.EndInquiry,
    ];

    public static List<RequestContractorStatus> AllowStatusForCreateInquiry =
    [
        RequestContractorStatus.InquiryRejected,
        RequestContractorStatus.Inquiry,
    ];
}