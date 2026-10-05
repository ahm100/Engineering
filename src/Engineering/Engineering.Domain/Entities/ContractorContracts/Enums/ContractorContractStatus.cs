
namespace Engineering.Domain.Entities.ContractorContracts.Enums;

public enum ContractorContractStatus
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
    ProjectManagerRejected = 35,
    [Description("تایید مدیر پروژه")]
    ProjectManagerConfirmed = 40,

    [Description("در انتظار بررسی کارشناس ارشد")]
    ManagementPending = 50,
    [Description("برگشت از کارشناس ارشد")]
    ManagementReturned = 60,
    [Description("رد از کارشناس ارشد")]
    ManagementRejected = 65,
    [Description("تایید کارشناس ارشد")]
    ManagementConfirmed = 70,

    [Description("برگشت برای بازنگری مجدد")]
    ManagementReturnForReview = 75,

    [Description("بایگانی")]
    Archived = 80,
}

public class ContractorContractStatusRules
{
    public static List<ContractorContractStatus> AllowForUpdate =
        [
        ContractorContractStatus.New,
        ContractorContractStatus.ProjectManagerResend,
        ContractorContractStatus.ProjectManagerReturned,
        ContractorContractStatus.ManagementReturned,
        ContractorContractStatus.ManagementReturnForReview,
    ];

    public static List<ContractorContractStatus> AllowForManagementReturnForReview =
        [
        ContractorContractStatus.ManagementConfirmed
    ];

    public static List<ContractorContractStatus> JustAddUrl =
        [
        ContractorContractStatus.ProjectManagerConfirmed,
        ContractorContractStatus.ManagementConfirmed
    ];

    public static List<ContractorContractStatus> AllowForDelete =
        [
        ContractorContractStatus.New,
        ContractorContractStatus.ProjectManagerResend,
        ContractorContractStatus.ProjectManagerReturned,
        ContractorContractStatus.ProjectManagerRejected,
        ContractorContractStatus.Archived,
        ContractorContractStatus.ManagementReturned,
        ContractorContractStatus.ManagementRejected,
        ContractorContractStatus.ManagementReturnForReview,
    ];

    public static List<ContractorContractStatus> AllowForProjectManagerResend =
        [
        ContractorContractStatus.New,
        ContractorContractStatus.ProjectManagerReturned,
        ContractorContractStatus.ManagementReturned,
    ];

    public static List<ContractorContractStatus> AllowForProjectManagerPending =
        [
        ContractorContractStatus.New,
        ContractorContractStatus.ProjectManagerResend,
    ];

    public static List<ContractorContractStatus> AllowForProjectManagerReturned =
        [
        ContractorContractStatus.New,
        ContractorContractStatus.ProjectManagerResend,
        ContractorContractStatus.ProjectManagerPending,
    ];

    public static List<ContractorContractStatus> AllowForProjectManagerRejected =
        [
        ContractorContractStatus.New,
        ContractorContractStatus.ProjectManagerResend,
        ContractorContractStatus.ProjectManagerPending,
    ];

    public static List<ContractorContractStatus> AllowForProjectManagerConfirmed =
        [
        ContractorContractStatus.ProjectManagerPending,
    ];

    public static List<ContractorContractStatus> AllowForManagementPending =
        [
        ContractorContractStatus.ProjectManagerConfirmed,
    ];

    public static List<ContractorContractStatus> AllowForManagementReturned =
        [
        ContractorContractStatus.ManagementPending,
        ContractorContractStatus.ProjectManagerConfirmed,
    ];

    public static List<ContractorContractStatus> AllowForManagementRejected =
        [
        ContractorContractStatus.ManagementPending,
        ContractorContractStatus.ProjectManagerConfirmed,
        ContractorContractStatus.ProjectManagerPending,
    ];

    public static List<ContractorContractStatus> AllowForManagementConfirmed =
        [
        ContractorContractStatus.ManagementPending,
        ContractorContractStatus.ProjectManagerConfirmed,
    ];

    public static List<ContractorContractStatus> AllowForArchived =
        [
        ContractorContractStatus.New,
        ContractorContractStatus.ProjectManagerResend,
        ContractorContractStatus.ManagementRejected,
        ContractorContractStatus.ProjectManagerRejected,
    ];

    public static List<ContractorContractStatus> AllowForWriteDescription =
        [
        ContractorContractStatus.New,
        ContractorContractStatus.ProjectManagerResend,
        ContractorContractStatus.ProjectManagerReturned,
        ContractorContractStatus.ManagementReturned,
        ContractorContractStatus.ManagementReturnForReview,
    ];
}