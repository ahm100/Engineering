namespace Engineering.Domain.Entities.SessionRecords.Enums;

public enum SessionType
{
    /// <summary>
    /// Project Category
    /// </summary>
    [Description("هماهنگی پروژه")]
    ProjectCoordination = 1,

    [Description("فنی")]
    Technical = 2,

    [Description("اجرایی")]
    Execution = 3,

    [Description("برنامه‌ریزی و کنترل پروژه")]
    PlanningAndControl = 4,

    [Description("ایمنی")]
    Safety = 5,

    [Description("تأمین و تدارکات")]
    ProcurementAndSupply = 6,

    /// <summary>
    /// Contract Category
    /// </summary>
    [Description("بررسی قرارداد")]
    Review = 7,

    [Description("بررسی تعهدات")]
    Obligations = 8,

    [Description("تغییرات قرارداد")]
    ContractChanges = 9,

    [Description("مالی و پرداخت")]
    FinanceAndPayment = 10,

    [Description("اختلافات و مسائل قراردادی")]
    DisputesAndIssues = 11,

    [Description("مذاکره با پیمانکار")]
    ContractorNegotiation = 12,

    /// <summary>
    /// Management Category
    /// </summary>
    [Description("تصمیم‌گیری مدیریتی")]
    DecisionMaking = 13,

    [Description("بررسی عملکرد")]
    PerformanceReview = 14,

    [Description("بررسی ریسک‌ها")]
    RiskAssessment = 15,

    [Description("هماهنگی بین واحدها")]
    InterUnitCoordination = 16,

    [Description("مدیریت پروژه")]
    ProjectManagement = 17
}