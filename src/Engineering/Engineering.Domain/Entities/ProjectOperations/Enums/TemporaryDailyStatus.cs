
namespace Engineering.Domain.Entities.ProjectOperations.Enums;

public enum TemporaryDailyStatus
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
    Doing = 5,
    /// <summary>
    /// پایان یافته
    /// </summary>
    [Description("پایان یافته")]
    EndOfWork = 10
}
