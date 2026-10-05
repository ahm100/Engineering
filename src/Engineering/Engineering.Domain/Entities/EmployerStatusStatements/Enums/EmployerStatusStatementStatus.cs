
namespace Engineering.Domain.Entities.EmployerStatusStatements.Enums;

public enum EmployerStatusStatementStatus
{
    [Description("ثبت اولیه")]
    Registration = 1,
    [Description("در حال بررسی")]
    Pending = 10,
    [Description("برگشت برای بازنگری مجدد")]
    ReturnForReview = 20,
    [Description("ابطال")]
    Invalidated = 30,

    [Description("ارسال برای ناظر")]
    SendToSupervisor = 60,
    [Description("درحال بررسی ناظر")]
    SupervisorPending = 70,

    [Description("ارسال برای مشاور")]
    SendToConsultant = 80,
    [Description("درحال بررسی مشاور")]
    ConsultantPending = 90,

    [Description("ارسال برای نماینده کارفرما")]
    SendToEmployerRepresentative = 100,
    [Description("درحال بررسی نماینده کارفرما")]
    EmployerRepresentativePending = 110,


    [Description("ارسال برای بازرگانی")]
    SendForCommercial = 150,

    [Description("ارسال برای روکش مالی")]
    SendForFinancialCover = 170,

    [Description("پایان یافته")]
    Finished = 200,
}

public class EmployerStatusStatementStatusRules
{
    public static List<EmployerStatusStatementStatus> AllowForDelete =
        [
        EmployerStatusStatementStatus.Registration,
        EmployerStatusStatementStatus.Pending,
        EmployerStatusStatementStatus.Invalidated,
        EmployerStatusStatementStatus.ReturnForReview,
    ];

    public static List<EmployerStatusStatementStatus> IsPending =
        [
        EmployerStatusStatementStatus.Pending,
        EmployerStatusStatementStatus.ConsultantPending,
        EmployerStatusStatementStatus.SupervisorPending,
        EmployerStatusStatementStatus.EmployerRepresentativePending,
    ];

    public static List<EmployerStatusStatementStatus> AllowForInvalidated =
        [
        EmployerStatusStatementStatus.Registration,
        EmployerStatusStatementStatus.Pending,
        EmployerStatusStatementStatus.Invalidated,
        EmployerStatusStatementStatus.ReturnForReview,
    ];

    public static List<EmployerStatusStatementStatus> AllowForPending =
        [
        EmployerStatusStatementStatus.Registration,
        EmployerStatusStatementStatus.ReturnForReview,
    ];

    public static List<EmployerStatusStatementStatus> AllowForReturnForReview =
        [
        EmployerStatusStatementStatus.SendToSupervisor,
        EmployerStatusStatementStatus.SupervisorPending,
        EmployerStatusStatementStatus.ConsultantPending,
        EmployerStatusStatementStatus.EmployerRepresentativePending,
    ];

    public static List<EmployerStatusStatementStatus> AllowForSendToSupervisor =
        [
        EmployerStatusStatementStatus.Pending,
    ];

    public static List<EmployerStatusStatementStatus> AllowForSupervisorPending =
        [
        EmployerStatusStatementStatus.SendToSupervisor
    ];

    public static List<EmployerStatusStatementStatus> AllowForSendToConsultant =
        [
        EmployerStatusStatementStatus.SupervisorPending,
    ];

    public static List<EmployerStatusStatementStatus> AllowForConsultantPending =
        [
        EmployerStatusStatementStatus.SendToConsultant
    ];

    public static List<EmployerStatusStatementStatus> AllowForSendToEmployerRepresentative =
        [
        EmployerStatusStatementStatus.ConsultantPending,
    ];

    public static List<EmployerStatusStatementStatus> AllowForEmployerRepresentativePending =
        [
        EmployerStatusStatementStatus.SendToEmployerRepresentative
    ];

    public static List<EmployerStatusStatementStatus> AllowForSendForCommercialOneAgent =
        [
        EmployerStatusStatementStatus.Pending
    ];

    public static List<EmployerStatusStatementStatus> AllowForSendForCommercialTowAgent =
        [
        EmployerStatusStatementStatus.SendToSupervisor,
    ];

    public static List<EmployerStatusStatementStatus> AllowForSendForCommercialThreeAgent =
        [
        EmployerStatusStatementStatus.SendToConsultant,
    ];

    public static List<EmployerStatusStatementStatus> AllowForSendForCommercialFourAgent =
        [
        EmployerStatusStatementStatus.SendToEmployerRepresentative,
    ];


}