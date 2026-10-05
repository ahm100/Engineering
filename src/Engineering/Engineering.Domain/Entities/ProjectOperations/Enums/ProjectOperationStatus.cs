
namespace Engineering.Domain.Entities.ProjectOperations.Enums;

public enum ProjectOperationStatus
{
    /// <summary>
    /// شروع نشده
    /// </summary>
    [Description("شروع نشده")]
    NotStarted = 1,
    /// <summary>
    /// درحال انجام
    /// </summary>
    [Description("درحال انجام")]
    Doing = 2,
    /// <summary>
    /// متوقف شده
    /// </summary>
    [Description("متوقف شده")]
    Stopped = 3,
    /// <summary>
    /// پایان کار
    /// </summary>
    [Description("پایان کار")]
    EndOfWork = 4,
    /// <summary>
    /// تحویل موقت
    /// </summary>
    [Description("تحویل موقت")]
    TemporaryDelivery = 5,
    /// <summary>
    /// تحویل قطعی
    /// </summary>
    [Description("تحویل قطعی")]
    DefiniteDelivery = 6,
}
