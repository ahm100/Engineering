
namespace Engineering.Domain.Entities.ContractorStatusStatements.Enums;

public enum CSSStatus
{
    [Description("ثبت اولیه")]
    New = 1,
    [Description("ارسال مجدد به مدیر پروژه")]
    ProjectManagerResend = 10,

    [Description("در انتظار بررسی مدیر پروژه")]
    ProjectManagerPending = 20,
    [Description("برگشت از مدیر پروژه")]
    ProjectManagerReturned = 30,
    [Description("رد از مدیر پروژه")]
    ProjectManagerRejected = 40,
    [Description("تایید مدیر پروژه")]
    ProjectManagerConfirmed = 50,

    [Description("در انتظار بررسی کارشناس ارشد")]
    ManagementPending = 60,
    [Description("برگشت از کارشناس ارشد")]
    ManagementReturned = 70,
    [Description("برگشت به مدیرپروژه")]
    ReturnToProjectManager = 75,
    [Description("رد از کارشناس ارشد")]
    ManagementRejected = 80,
    [Description("برگشت برای بازنگری مجدد")]
    ManagementReturnForReview = 85,
    [Description("تایید کارشناس ارشد")]
    ManagementConfirmed = 90,

    [Description("رد از مدیر اولیه")]
    PrimaryManagerRejected = 93,
    [Description("تایید مدیر اولیه")]
    PrimaryManagerConfirmed = 95,

    [Description("باطل شده")]
    Invalidated = 100,

    [Description("رد از مدیر نهایی")]
    FinalManagerRejected = 103,
    [Description("تایید مدیر نهایی")]
    FinalManagerConfirmed = 105,

    [Description("صدور دستور پرداخت")]
    PaymentConfirmation = 110,
    [Description("پرداخت شده")]
    Paid = 120,
    [Description("لغو پرداخت")]
    RejectPaid = 130,
    [Description("بخشی پرداخت شده")]
    IncompletelyPaid = 140,
}

public class CSSStatusRules
{
    public static List<CSSStatus> AllowForConfigPayment =
        [
        CSSStatus.New,
        CSSStatus.ProjectManagerConfirmed,
        CSSStatus.ManagementConfirmed,
        CSSStatus.PrimaryManagerConfirmed,
        CSSStatus.FinalManagerConfirmed,
        CSSStatus.PaymentConfirmation,
        CSSStatus.Paid,
    ];

    public static List<CSSStatus> AllowForDraftable =
        [
        CSSStatus.ProjectManagerPending,
        CSSStatus.ProjectManagerConfirmed,
        CSSStatus.ManagementPending,
        CSSStatus.ManagementConfirmed,
        CSSStatus.PrimaryManagerConfirmed,
        CSSStatus.FinalManagerConfirmed,
        CSSStatus.PaymentConfirmation,
        CSSStatus.Paid,
    ];

    public static List<CSSStatus> AllowForNoInclude =
        [
        CSSStatus.ProjectManagerPending,
        CSSStatus.ProjectManagerRejected,
        CSSStatus.ProjectManagerReturned,
        CSSStatus.ManagementPending,
        CSSStatus.ManagementRejected,
        CSSStatus.ReturnToProjectManager,
        CSSStatus.ManagementReturned,
        CSSStatus.PrimaryManagerRejected,
        CSSStatus.PrimaryManagerConfirmed,
        CSSStatus.FinalManagerRejected,
        CSSStatus.FinalManagerConfirmed,
        CSSStatus.Invalidated,
        CSSStatus.Paid,
        CSSStatus.RejectPaid,
    ];

    public static List<CSSStatus> AllowForIncludeLess =
        [
        CSSStatus.ProjectManagerConfirmed,
        CSSStatus.ManagementConfirmed
    ];

    public static List<CSSStatus> AllowForFullInclude =
        [
        CSSStatus.PaymentConfirmation
    ];

    public static List<CSSStatus> AllowForUpdate =
        [
        CSSStatus.New,
        CSSStatus.ProjectManagerResend,
        CSSStatus.ProjectManagerReturned,
        CSSStatus.ManagementReturned,
        CSSStatus.ManagementReturnForReview
    ];

    public static List<CSSStatus> AllowForManagementReturnForReview =
        [
        CSSStatus.ManagementConfirmed
    ];

    public static List<CSSStatus> AllowForReturnToProjectManager =
        [
        CSSStatus.ProjectManagerConfirmed,
        CSSStatus.ManagementPending,
    ];

    public static List<CSSStatus> AllowForDelete =
        [
        CSSStatus.New,
        CSSStatus.ManagementRejected,
        CSSStatus.ProjectManagerRejected,
        CSSStatus.ManagementReturned,
        CSSStatus.ProjectManagerReturned,
        CSSStatus.Invalidated,
        CSSStatus.ProjectManagerResend,
        CSSStatus.ManagementReturnForReview,
        CSSStatus.PrimaryManagerRejected,
        CSSStatus.FinalManagerRejected,
    ];

    public static List<CSSStatus> AllowForInvalidated =
        [
        CSSStatus.New,
        CSSStatus.ManagementReturnForReview,
        CSSStatus.ProjectManagerReturned,
        CSSStatus.ProjectManagerRejected,
        CSSStatus.ManagementReturned,
        CSSStatus.ManagementRejected,
        CSSStatus.ProjectManagerResend
    ];

    public static List<CSSStatus> AllowForProjectManagerResend =
        [
        CSSStatus.New,
        CSSStatus.ProjectManagerReturned,
        CSSStatus.ManagementReturned,
    ];

    public static List<CSSStatus> AllowForProjectManagerPending =
        [
        CSSStatus.New,
        CSSStatus.ProjectManagerResend,
        CSSStatus.ReturnToProjectManager,
    ];

    public static List<CSSStatus> AllowForProjectManagerReturned =
        [
        CSSStatus.New,
        CSSStatus.ProjectManagerResend,
        CSSStatus.ProjectManagerPending,
    ];

    public static List<CSSStatus> AllowForProjectManagerRejected =
        [
        CSSStatus.New,
        CSSStatus.ProjectManagerResend,
        CSSStatus.ProjectManagerPending,
    ];

    public static List<CSSStatus> AllowForProjectManagerConfirmed =
        [
        CSSStatus.New,
        CSSStatus.ProjectManagerResend,
        CSSStatus.ProjectManagerPending,
        CSSStatus.ReturnToProjectManager,
    ];

    public static List<CSSStatus> AllowForManagementPending =
        [
        CSSStatus.ProjectManagerConfirmed,
        CSSStatus.PrimaryManagerRejected,
        CSSStatus.FinalManagerRejected,
    ];

    public static List<CSSStatus> AllowForManagementReturned =
        [
        CSSStatus.ProjectManagerConfirmed,
        CSSStatus.ManagementPending,
        CSSStatus.PrimaryManagerRejected,
        CSSStatus.FinalManagerRejected,
    ];

    public static List<CSSStatus> AllowForManagementRejected =
        [
        CSSStatus.ManagementPending,
        CSSStatus.ProjectManagerConfirmed,
        CSSStatus.PrimaryManagerRejected,
        CSSStatus.FinalManagerRejected,
    ];

    public static List<CSSStatus> AllowForManagementConfirmed =
        [
        CSSStatus.ManagementPending,
        CSSStatus.ProjectManagerConfirmed,
        CSSStatus.PrimaryManagerRejected,
        CSSStatus.FinalManagerRejected,
        CSSStatus.ManagementConfirmed,
        CSSStatus.IncompletelyPaid,
        CSSStatus.Paid,
    ];

    public static List<CSSStatus> AllowForPrimaryManagerRejected =
        [
        CSSStatus.ManagementConfirmed
    ];

    public static List<CSSStatus> AllowForPrimaryManagerConfirmed =
        [
        CSSStatus.ManagementConfirmed
    ];

    public static List<CSSStatus> AllowForFinalManagerRejected =
        [
        CSSStatus.ManagementConfirmed
    ];

    public static List<CSSStatus> AllowForFinalManagerConfirmed =
        [
        CSSStatus.ManagementConfirmed
    ];

    public static List<CSSStatus> AllowForPaymentConfirmation =
        [
        CSSStatus.ManagementConfirmed
    ];

    public static List<CSSStatus> AllowForPaid =
        [
        CSSStatus.PaymentConfirmation
    ];

    public static List<CSSStatus> AllowForRejectPaid =
        [
        CSSStatus.PaymentConfirmation
    ];

    public static List<CSSStatus> AllowForArchived =
        [
        CSSStatus.New,
        CSSStatus.ProjectManagerResend,
        CSSStatus.ManagementRejected,
        CSSStatus.ProjectManagerRejected,
    ];

    public static List<CSSStatus> AllowForIntegratedCSS =
        [
        CSSStatus.Paid,
        CSSStatus.IncompletelyPaid,
    ];

    public static List<CSSStatus> DoNotShow =
    [
        CSSStatus.FinalManagerConfirmed,
        CSSStatus.PrimaryManagerConfirmed,
    ];
}