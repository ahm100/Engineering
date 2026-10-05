namespace Engineering.Domain.Entities.ProcesVerbal.Enums;

public enum ProcesVerbalWorkStopReason
{
    /// <summary>
    /// دستور کارفرما
    /// </summary>
    [Description("دستور کارفرما")]
    EmployerOrder = 1,

    /// <summary>
    /// مشکل فنی
    /// </summary>
    [Description("مشکل فنی")]
    TechnicalIssue = 2,

    /// <summary>
    /// تأمین مصالح
    /// </summary>
    [Description("تأمین مصالح")]
    MaterialSupply = 3,

    /// <summary>
    /// شرایط کارگاه
    /// </summary>
    [Description("شرایط کارگاه")]
    SiteConditions = 4,

    /// <summary>
    /// شرایط جوی
    /// </summary>
    [Description("شرایط جوی")]
    WeatherConditions = 5,

    /// <summary>
    /// سایر
    /// </summary>
    [Description("سایر")]
    Other = 6
}